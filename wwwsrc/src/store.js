import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "//localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "//localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

const initialState = {
  user: {},
  keeps: [],
  author: {},
  lastGetCount: 0
}

export default new Vuex.Store({
  state: {
    user: {},
    keeps: [],
    author: {},
    lastGetCount: 0
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
      console.log(user)
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
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({commit}) {
      auth.delete("logout")
        .then(res => {
          if (!res.data) { return }
          commit('resetState', initialState)
          router.push({name: 'login'})
        })
    },
    createAccount({commit}) {
      auth.delete("logout")
        .then(res => {
          if (!res.data) { return }
          commit('resetState', initialState)
          router.push({name: 'login', params: {signUp: "true"}})
        })
    },
    //GET AUTHOR INFO
    getAuthor({commit}, userId) {
      auth.get(`${userId}`)
        .then(res => {
          commit('setAuthor', res.data)
        })
        .catch(e => console.log(e))
    },
    //
    //Keeps
    //
    getKeeps({dispatch, commit, state}, id){
      if (id == "first" && !state.keeps.length) {
        api.get('keeps')
          .then(res => {
            commit('setKeeps', res.data)
          })
          .catch(e => console.log(e))
      }
      // else if (typeof id == "number") {
      else {
        api.get(`keeps/${id}`)
          .then(res => {
            commit('setKeeps', res.data)
          })
          .catch(e => console.log(e))
      }
    },
    deleteKeep({dispatch, commit}, payload){
      console.log(payload)
      api.delete('keeps', payload)
        .then(res => {
          console.log(res)
        })
        .catch(e => {
          console.log('error:', e)
        })
    },
    postKeep({commit}, keep) {
      api.post('keeps', keep)
        .then(res => {
          console.log("inserted keep into db!")
          commit("addKeep", res.data)
        })
        .catch(e => console.log("ERROR", e))
    }
  }
})