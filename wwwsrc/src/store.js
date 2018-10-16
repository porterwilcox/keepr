import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let production = !window.location.host.includes('localhost');
let baseUrl = production ? '//keepr208.herokuapp.com/' : '//localhost:5000/';

let api = Axios.create({
  baseURL: baseUrl + 'api/',
  timeout: 2000,
  withCredentials: true
})

let auth = Axios.create({
  baseURL: baseUrl + 'account/',
  timeout: 2000,
  withCredentials: true
})

const initialState = {
  user: {},
  keeps: [],
  usersKeeps: [],
  author: {},
  lastGetCount: 0,
  usersVaults: [],
  vaultsKeeps: []
}

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    usersKeeps: [],
    author: {},
    lastGetCount: 0,
    usersVaults: [],
    vaultsKeeps: []
  },
  mutations: {
    resetState(state, payload) {
      let keys = Object.keys(payload)
      let values = Object.values(payload)
      for (let i = 0; i < values.length; i++) {
        state[keys[i]] = values[i]
      }
    },
    //
    //USER MUTATIONS
    //
    setUser(state, user) {
      state.user = user
    },
    setAuthor(state, author) {
      state.author = author
    },
    //
    //KEEP MUTATIONS
    //
    setKeeps(state, keepArr) {
      let arr = state.keeps.concat(keepArr)
      state.keeps = arr
      state.lastGetCount = keepArr.length
    },
    addKeep(state, keep) {
      state.keeps.unshift(keep)
    },
    setUsersKeeps(state, usersKeeps) {
      state.usersKeeps = usersKeeps
    },
    removeKeep(state, id) {
      for (let i = 0; i < state.keeps.length; i++) {
        let keep = state.keeps[i]
        if (keep.id == id) {
          state.keeps.splice(i, 1)
          break;
        }
      }
      for (let i = 0; i < state.usersKeeps.length; i++) {
        let keep = state.usersKeeps[i]
        if (keep.id == id) {
          state.usersKeeps.splice(i, 1)
          break;
        }
      }
    },
    //
    //VAULTS MUTATIONS
    //
    setUsersVaults(state, vaultsArr) {
      state.usersVaults = vaultsArr
    },
    addVault(state, vault) {
      state.usersVaults.push(vault)
    },
    removeVault(state, id) {
      for (let i = 0; i < state.usersVaults.length; i++) {
        let vault = state.usersVaults[i]
        if (vault.id == id) {
          state.usersVaults.splice(i, 1)
          break;
        }
      }
    },
    //
    //VAULTKEEPS MUTATIONS
    //
    setVaultsKeeps(state, vaultsKeeps) {
      state.vaultsKeeps = vaultsKeeps
    },
    removeKeepFromVault(state, keepId) {
      for (let i = 0; i < state.vaultsKeeps.length; i++) {
        let keep = state.vaultsKeeps[i]
        if (keep.id == keepId) {
          state.vaultsKeeps.splice(i, 1)
          break;
        }
      }
    }
  },
  actions: {
    //
    //USER ACTIONS
    //
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
          dispatch('getUsersVaults', res.data.id)
          dispatch("getUsersKeeps", res.data.id);
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
          dispatch('getUsersVaults', res.data.id)
          dispatch("getUsersKeeps", res.data.id);
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({ commit }) {
      auth.delete("logout")
        .then(res => {
          if (!res.data) { return }
          commit('resetState', initialState)
          router.push({ name: 'login' })
        })
    },
    createAccount({ commit }) {
      auth.delete("logout")
        .then(res => {
          if (!res.data) { return }
          commit('resetState', initialState)
          router.push({ name: 'login', params: { signUp: "true" } })
        })
    },
    //GET AUTHOR INFO
    getAuthor({ commit }, userId) {
      auth.get(`${userId}`)
        .then(res => {
          commit('setAuthor', res.data)
        })
        .catch(e => console.log(e))
    },
    //
    //Keeps
    //
    getKeeps({ dispatch, commit, state }, id) {
      if (id == "first" && !state.keeps.length && !state.user.id) {
        let id = -1
        api.get(`keeps/${id}`)
          .then(res => {
            commit('setKeeps', res.data)
          })
          .catch(e => console.log(e))
      }
      else if (id == "first" && !state.keeps.length) {
        let id = 0
        api.get(`keeps/${id}`)
          .then(res => {
            commit('setKeeps', res.data)
          })
          .catch(e => console.log(e))
      }
      else {
        api.get(`keeps/${id}`)
          .then(res => {
            commit('setKeeps', res.data)
          })
          .catch(e => console.log(e))
      }
    },
    getUsersKeeps({ commit }, userId) {
      api.get(`keeps/byUser/${userId}`)
        .then(res => {
          commit("setUsersKeeps", res.data)
        })
        .catch(e => console.log(e))
    },
    deleteKeep({ commit }, payload) {
      api.delete('keeps', { data: payload })
        .then(res => {
          if (res.data) {
            return commit("removeKeep", payload.id)
          }
          console.log("error: unsuccessful delete")
        })
        .catch(e => {
          console.log('error:', e)
        })
    },
    postKeep({ commit }, keep) {
      api.post('keeps', keep)
        .then(res => {
          console.log("inserted keep into db!")
          commit("addKeep", res.data)
        })
        .catch(e => console.log("ERROR", e))
    },
    incrementViews({ }, id) {
      api.put(`keeps/${id}`)
        .then(res => {
          if (res.data) {
            console.log('success')
          }
        })
        .catch(e => console.log(e))
    },
    //
    //VAULTS ACTIONS
    //
    getUsersVaults({ commit }, userId) {
      api.get(`vaults/${userId}`)
        .then(res => {
          commit("setUsersVaults", res.data)
        })
        .catch(e => console.log(e))
    },
    createVault({ commit }, vault) {
      api.post('vaults', vault)
        .then(res => {
          commit("addVault", res.data)
        })
    },
    deleteVault({ commit }, vault) {
      api.delete('vaults', { data: vault })
        .then(res => {
          if (res.data) {
            return commit("removeVault", vault.id)
          }
          console.log("error: unsuccessful delete")
        })
        .catch(e => {
          console.log('error:', e)
        })
    },
    //
    //VAULTKEEPS ACTIONS
    //
    getVaultsKeeps({ commit }, vaultId) {
      api.get(`vaultKeeps/${vaultId}`)
        .then(res => {
          commit("setVaultsKeeps", res.data)
        })
        .catch(e => console.log(e))
    },
    removeKeepFromVault({ commit }, vk) {
      api.delete('vaultKeeps', { data: vk })
        .then(res => {
          if (res.data) {
            return commit("removeKeepFromVault", vk.keepId)
          }
          console.log("error: unsuccessful delete")
        })
        .catch(e => console.log(e))
    },
    addKeepToVault({ commit }, vk) {
      api.post('vaultKeeps', vk)
        .then(res => {
          if (res.data) {
            return console.log("success")
          }
          console.log("already in your database")
        })
        .catch(e => console.log(e))
    }
  }
})