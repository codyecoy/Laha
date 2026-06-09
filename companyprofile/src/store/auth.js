import { defineStore } from 'pinia'
import { storage } from '../services/storage'
import { api } from '../services/mockApi'

const STORAGE_KEY = 'sa_auth_v1'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    session: storage.get(STORAGE_KEY, { token: '', user: null }),
  }),
  getters: {
    isAuthenticated(state) {
      return Boolean(state.session?.token && state.session?.user?.email)
    },
    adminUser(state) {
      return state.session?.user || null
    },
    token(state) {
      return state.session?.token || ''
    },
  },
  actions: {
    async login({ email, password }) {
      const res = await api.loginAdmin({ email, password })
      if (!res.ok) return res
      this.session = { token: res.token, user: res.user }
      storage.set(STORAGE_KEY, this.session)
      return { ok: true }
    },
    logout() {
      this.session = { token: '', user: null }
      storage.remove(STORAGE_KEY)
    },
  },
})
