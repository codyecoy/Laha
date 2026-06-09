import { defineStore } from 'pinia'
import { api } from '../services/mockApi'

export const useContentStore = defineStore('content', {
  state: () => ({
    loading: false,
    articles: [],
    services: [],
    team: [],
    gallery: [],
  }),
  getters: {
    featuredArticles(state) {
      return state.articles.filter((a) => a.featured).slice(0, 3)
    },
    articleCategories(state) {
      const set = new Set(state.articles.map((a) => a.category).filter(Boolean))
      return Array.from(set)
    },
  },
  actions: {
    async hydrate() {
      this.loading = true
      try {
        const data = await api.getPublicSnapshot()
        this.articles = data.articles
        this.services = data.services
        this.team = data.team
        this.gallery = data.gallery || []
      } finally {
        this.loading = false
      }
    },
    async refreshArticles() {
      this.loading = true
      try {
        const { data } = await api.listArticles({ page: 1, pageSize: 50 })
        this.articles = data
      } finally {
        this.loading = false
      }
    },
  },
})
