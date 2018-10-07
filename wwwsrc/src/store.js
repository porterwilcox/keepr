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

export default new Vuex.Store({
  state: {
    user: {},
    keeps: []
  },
  mutations: {
    //
    //USER MUTATIONS
    //
    setUser(state, user) {
      state.user = user
      console.log(user)
    },
    //
    //KEEP MUTATIONS
    //
    setKeeps(state, keepArr) {
      state.keeps = keepArr
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
    logout({}) {
      auth.delete("logout")
        .then(res => {
          if (!res.data) { return }
          router.push({name: 'login'})
        })
    },
    //
    //Keeps
    //
    getKeeps({dispatch, commit}, num){
      api.get(`keeps/${num}`)
        .then(res => {
          commit('setKeeps', res.data)
        })
        .catch(e => console.log(e))
    },
    deleteKeep({dispatch, commit}, payload){
      api.delete("keeps", payload)
        .then(res => {
          console.log(res)
        })
        .catch(e => {
          console.log('error:', e)
        })
    }
  }
})