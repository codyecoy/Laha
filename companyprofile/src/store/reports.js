import { defineStore } from 'pinia'
import { api } from '../services/mockApi'

export const useReportsStore = defineStore('reports', {
  state: () => ({
    loading: false,
    list: [],
    page: 1,
    pageSize: 10,
    total: 0,
    totalPages: 1,
    statusFilter: '',
  }),
  actions: {
    async fetch({ page, pageSize, status } = {}) {
      this.loading = true
      try {
        const res = await api.listReports({
          page: page ?? this.page,
          pageSize: pageSize ?? this.pageSize,
          status: status ?? this.statusFilter,
        })
        this.list = res.data
        this.page = res.page
        this.pageSize = res.pageSize
        this.total = res.total
        this.totalPages = res.totalPages
      } finally {
        this.loading = false
      }
    },
    async updateStatus(id, status) {
      await api.updateReportStatus(id, status)
      await this.fetch()
    },
    async submit(payload) {
      return api.submitReport(payload)
    },
  },
})

