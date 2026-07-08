import { mockDb } from './mockDb'
import axios from 'axios'
import { storage } from './storage'

mockDb.ensure()

const AUTH_KEY = 'sa_auth_v1'
const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ||
  (import.meta.env.DEV && typeof window !== 'undefined' ? `http://${window.location.hostname}:39744` : 'http://localhost:39744')

const http = axios.create({
  baseURL: API_BASE_URL,
  timeout: 8000,
})

http.interceptors.request.use((config) => {
  const session = storage.get(AUTH_KEY, null)
  const token = session?.token
  if (token) config.headers.Authorization = `Bearer ${token}`
  return config
})

function wait(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

async function simulate() {
  await wait(250 + Math.round(Math.random() * 250))
}

function byDateDesc(a, b) {
  return new Date(b.publishedAt || b.createdAt).getTime() - new Date(a.publishedAt || a.createdAt).getTime()
}

function paginate(items, { page = 1, pageSize = 10 }) {
  const safePage = Math.max(1, Number(page) || 1)
  const safeSize = Math.max(1, Math.min(50, Number(pageSize) || 10))
  const total = items.length
  const totalPages = Math.max(1, Math.ceil(total / safeSize))
  const clampedPage = Math.min(safePage, totalPages)
  const start = (clampedPage - 1) * safeSize
  const data = items.slice(start, start + safeSize)
  return { data, page: clampedPage, pageSize: safeSize, total, totalPages }
}

export const api = {
  async loginAdmin({ email, password }) {
    try {
      const res = await http.post('/api/auth/login', { email, password })
      return { ok: true, token: res.data.token, user: res.data.user }
    } catch (e) {
      const normalizedEmail = String(email || '').trim().toLowerCase()
      const normalizedPassword = String(password || '')
      if (normalizedEmail === 'admin@sahabatanak.org' && normalizedPassword === 'admin123') {
        return { ok: true, token: 'mock', user: { email: normalizedEmail, name: 'Admin LAHA', role: 'Admin' } }
      }
      return { ok: false, message: e?.response?.data?.message || 'Email atau password salah.' }
    }
  },

  async trackVisit() {
    const d = new Date()
    const dayKey = `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}`
    const key = `sa_visit_ping_${dayKey}`
    if (storage.get(key, false)) return { ok: true }

    try {
      await http.post('/api/visits/ping')
      storage.set(key, true)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      next.visitsDaily = next.visitsDaily || {}
      next.visitsDaily[dayKey] = (next.visitsDaily[dayKey] || 0) + 1
      mockDb.write(next)
      storage.set(key, true)
      return { ok: true }
    }
  },

  async getPublicSnapshot() {
    try {
      const [articles, services, team, gallery] = await Promise.all([
        http.get('/api/articles', { params: { page: 1, pageSize: 50 } }),
        http.get('/api/services'),
        http.get('/api/teams'),
        http.get('/api/gallery'),
      ])
      return { articles: articles.data.data, services: services.data, team: team.data, gallery: gallery.data }
    } catch {
      await simulate()
      const db = mockDb.read()
      return { articles: [...db.articles].sort(byDateDesc), services: db.services, team: db.team, gallery: db.gallery || [] }
    }
  },

  async listArticles({ category, q, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/articles', { params: { category, q, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalizedQ = String(q || '').trim().toLowerCase()
      const normalizedCategory = String(category || '').trim()
      let items = [...db.articles].filter((a) => a.isPublished !== false).sort(byDateDesc)
      if (normalizedCategory) items = items.filter((a) => a.category === normalizedCategory)
      if (normalizedQ) items = items.filter((a) => `${a.title} ${a.excerpt}`.toLowerCase().includes(normalizedQ))
      return paginate(items, { page, pageSize })
    }
  },

  async getArticleBySlug(slug) {
    try {
      const res = await http.get(`/api/articles/${encodeURIComponent(slug)}`)
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      return db.articles.find((a) => a.slug === slug && a.isPublished !== false) || null
    }
  },

  async listAdminArticles({ category, q, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/articles', { params: { category, q, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalizedQ = String(q || '').trim().toLowerCase()
      const normalizedCategory = String(category || '').trim()
      let items = [...db.articles].sort(byDateDesc)
      if (normalizedCategory) items = items.filter((a) => a.category === normalizedCategory)
      if (normalizedQ) items = items.filter((a) => `${a.title} ${a.excerpt}`.toLowerCase().includes(normalizedQ))
      return paginate(items, { page, pageSize })
    }
  },

  async getAdminArticle(id) {
    try {
      const res = await http.get(`/api/admin/articles/${encodeURIComponent(id)}`)
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      return db.articles.find((a) => a.id === id) || null
    }
  },

  async upsertArticle(payload) {
    const incoming = { ...payload }
    try {
      const dto = {
        title: incoming.title,
        slug: incoming.slug,
        category: incoming.category,
        excerpt: incoming.excerpt,
        content: incoming.content,
        thumbnailUrl: incoming.thumbnailUrl,
        featured: Boolean(incoming.featured),
        isPublished: incoming.isPublished !== false,
        readingTime: Number(incoming.readingTime) || 5,
        publishedAt: incoming.publishedAt,
        coverTone: incoming.coverTone,
      }
      if (incoming.id) {
        await http.put(`/api/admin/articles/${incoming.id}`, dto)
      } else {
        await http.post('/api/admin/articles', dto)
      }
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }

      if (!incoming.title) throw new Error('Judul wajib diisi.')
      incoming.slug = incoming.slug || mockDb.slugify(incoming.title)
      incoming.excerpt = incoming.excerpt || ''
      incoming.category = incoming.category || 'Edukasi'
      incoming.coverTone = incoming.coverTone || 'emerald'
      incoming.thumbnailUrl = incoming.thumbnailUrl || ''
      incoming.readingTime = Number(incoming.readingTime) || 5
      incoming.publishedAt = incoming.publishedAt || new Date().toISOString()
      incoming.featured = Boolean(incoming.featured)
      incoming.isPublished = incoming.isPublished !== false
      incoming.content = incoming.content || ''

      if (incoming.id) {
        next.articles = next.articles.map((a) => (a.id === incoming.id ? { ...a, ...incoming } : a))
      } else {
        next.articles = [{ id: mockDb.uid('art'), ...incoming }, ...next.articles]
      }

      mockDb.write(next)
      return { ok: true }
    }
  },

  async deleteArticle(id) {
    try {
      await http.delete(`/api/admin/articles/${id}`)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      mockDb.write({ ...db, articles: db.articles.filter((a) => a.id !== id) })
      return { ok: true }
    }
  },

  async listServices() {
    try {
      const res = await http.get('/api/services')
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      return db.services
    }
  },

  async listAdminServices({ q, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/services', { params: { q, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const needle = String(q || '').trim().toLowerCase()
      let items = [...db.services]
      if (needle) items = items.filter((s) => `${s.title} ${s.description}`.toLowerCase().includes(needle))
      items.sort((a, b) => String(a.title || '').localeCompare(String(b.title || '')))
      return paginate(items, { page, pageSize })
    }
  },

  async upsertService(payload) {
    const incoming = { ...payload }
    try {
      const dto = {
        title: incoming.title,
        description: incoming.description,
        icon: incoming.icon,
        imageUrl: incoming.imageUrl,
        highlights: incoming.highlights,
      }
      if (incoming.id) await http.put(`/api/admin/services/${incoming.id}`, dto)
      else await http.post('/api/admin/services', dto)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }

      if (!incoming.title) throw new Error('Nama layanan wajib diisi.')
      incoming.description = incoming.description || ''
      incoming.icon = incoming.icon || 'spark'
      incoming.imageUrl = incoming.imageUrl || ''
      incoming.highlights = Array.isArray(incoming.highlights) ? incoming.highlights.filter(Boolean) : []

      if (incoming.id) {
        next.services = next.services.map((s) => (s.id === incoming.id ? { ...s, ...incoming } : s))
      } else {
        next.services = [{ id: mockDb.uid('srv'), ...incoming }, ...next.services]
      }

      mockDb.write(next)
      return { ok: true }
    }
  },

  async deleteService(id) {
    try {
      await http.delete(`/api/admin/services/${id}`)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      mockDb.write({ ...db, services: db.services.filter((s) => s.id !== id) })
      return { ok: true }
    }
  },

  async listTeam() {
    try {
      const res = await http.get('/api/teams')
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      return db.team
    }
  },

  async listAdminTeam({ q, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/teams', { params: { q, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const needle = String(q || '').trim().toLowerCase()
      let items = [...db.team]
      if (needle) items = items.filter((t) => `${t.name} ${t.role} ${t.bio}`.toLowerCase().includes(needle))
      items.sort((a, b) => String(a.name || '').localeCompare(String(b.name || '')))
      return paginate(items, { page, pageSize })
    }
  },

  async upsertTeamMember(payload) {
    const incoming = { ...payload }
    try {
      const dto = {
        name: incoming.name,
        role: incoming.role,
        bio: incoming.bio,
        tone: incoming.tone,
        photoUrl: incoming.photoUrl,
      }
      if (incoming.id) await http.put(`/api/admin/teams/${incoming.id}`, dto)
      else await http.post('/api/admin/teams', dto)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }

      if (!incoming.name) throw new Error('Nama wajib diisi.')
      incoming.role = incoming.role || ''
      incoming.bio = incoming.bio || ''
      incoming.tone = incoming.tone || 'emerald'
      incoming.photoUrl = incoming.photoUrl || ''

      if (incoming.id) {
        next.team = next.team.map((t) => (t.id === incoming.id ? { ...t, ...incoming } : t))
      } else {
        next.team = [{ id: mockDb.uid('tm'), ...incoming }, ...next.team]
      }

      mockDb.write(next)
      return { ok: true }
    }
  },

  async deleteTeamMember(id) {
    try {
      await http.delete(`/api/admin/teams/${id}`)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      mockDb.write({ ...db, team: db.team.filter((t) => t.id !== id) })
      return { ok: true }
    }
  },

  async submitReport(payload) {
    const incoming = { ...payload }
    try {
      const files = Array.isArray(incoming.evidence)
        ? incoming.evidence
            .map((e) => (e instanceof File ? e : e?.file instanceof File ? e.file : null))
            .filter(Boolean)
            .slice(0, 3)
        : []

      if (files.length) {
        const fd = new FormData()
        fd.set('isAnonymous', String(Boolean(incoming.isAnonymous)))
        fd.set('name', incoming.name || '')
        fd.set('contact', incoming.contact || '')
        fd.set('chronology', incoming.chronology || '')
        for (const f of files) fd.append('evidenceFiles', f)
        const res = await http.post('/api/reports', fd)
        return { ok: true, id: res.data.id }
      }

      const evidenceUrls = Array.isArray(incoming.evidence)
        ? incoming.evidence.map((e) => e?.dataUrl || e).filter(Boolean).slice(0, 3)
        : []
      const res = await http.post('/api/reports', {
        isAnonymous: Boolean(incoming.isAnonymous),
        name: incoming.name,
        contact: incoming.contact,
        chronology: incoming.chronology,
        evidenceUrls,
      })
      return { ok: true, id: res.data.id }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }

      const isAnonymous = Boolean(incoming.isAnonymous)
      const chronology = String(incoming.chronology || '').trim()
      if (!chronology) throw new Error('Kronologi wajib diisi.')

      const report = {
        id: mockDb.uid('rpt'),
        isAnonymous,
        name: isAnonymous ? '' : String(incoming.name || '').trim(),
        contact: isAnonymous ? '' : String(incoming.contact || '').trim(),
        chronology,
        evidence: Array.isArray(incoming.evidence) ? incoming.evidence : [],
        status: 'baru',
        createdAt: new Date().toISOString(),
      }

      next.reports = [report, ...next.reports]
      mockDb.write(next)
      return { ok: true, id: report.id }
    }
  },

  async listReports({ status, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/reports', { params: { status, page, pageSize } })
      const mapped = res.data.data.map((r) => ({
        id: r.id,
        isAnonymous: r.isAnonymous,
        name: r.name,
        contact: r.contact,
        chronology: r.chronology,
        evidence: (r.evidenceUrls || []).map((u) => ({ name: 'Bukti', dataUrl: u })),
        status: r.status,
        createdAt: r.createdAt,
      }))
      return { ...res.data, data: mapped }
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalized = String(status || '').trim()
      let items = [...db.reports].sort(byDateDesc)
      if (normalized) items = items.filter((r) => r.status === normalized)
      return paginate(items, { page, pageSize })
    }
  },

  async updateReportStatus(id, status) {
    try {
      await http.put(`/api/admin/reports/${id}/status`, { status: String(status) })
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      next.reports = next.reports.map((r) => (r.id === id ? { ...r, status: String(status) } : r))
      mockDb.write(next)
      return { ok: true }
    }
  },

  async listDonations({ status, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/donations', { params: { status, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalized = String(status || '').trim()
      let items = [...(db.donations || [])].sort(byDateDesc)
      if (normalized) items = items.filter((d) => d.status === normalized)
      return paginate(items, { page, pageSize })
    }
  },

  async updateDonationStatus(id, status) {
    try {
      await http.put(`/api/admin/donations/${id}/status`, { status: String(status) })
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      next.donations = (next.donations || []).map((d) => (d.id === id ? { ...d, status: String(status) } : d))
      mockDb.write(next)
      return { ok: true }
    }
  },

  async uploadAdminFiles(files) {
    const list = Array.isArray(files) ? files : []
    try {
      const fd = new FormData()
      for (const f of list) fd.append('files', f)
      const res = await http.post('/api/admin/uploads', fd)
      return res.data.urls || []
    } catch {
      await simulate()
      return []
    }
  },

  async listGalleryAdmin({ category, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/gallery', { params: { category, page, pageSize } })
      return res.data
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalized = String(category || '').trim()
      let items = [...(db.gallery || [])]
      if (normalized) items = items.filter((g) => g.category === normalized)
      items.sort((a, b) => (a.sortOrder || 0) - (b.sortOrder || 0))
      return paginate(items, { page, pageSize })
    }
  },

  async upsertGalleryPhoto(payload) {
    const incoming = { ...payload }
    try {
      const dto = {
        title: incoming.title,
        alt: incoming.alt,
        imageUrl: incoming.imageUrl,
        category: incoming.category,
        sortOrder: Number(incoming.sortOrder) || 0,
      }
      if (incoming.id) await http.put(`/api/admin/gallery/${incoming.id}`, dto)
      else await http.post('/api/admin/gallery', dto)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      if (!incoming.imageUrl) throw new Error('URL gambar wajib diisi.')
      const item = {
        id: incoming.id || mockDb.uid('gal'),
        title: incoming.title || '',
        alt: incoming.alt || incoming.title || '',
        imageUrl: incoming.imageUrl,
        category: incoming.category || '',
        sortOrder: Number(incoming.sortOrder) || 0,
        createdAt: incoming.createdAt || new Date().toISOString(),
      }
      if (incoming.id) next.gallery = (next.gallery || []).map((g) => (g.id === incoming.id ? { ...g, ...item } : g))
      else next.gallery = [item, ...(next.gallery || [])]
      mockDb.write(next)
      return { ok: true }
    }
  },

  async deleteGalleryPhoto(id) {
    try {
      await http.delete(`/api/admin/gallery/${id}`)
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      mockDb.write({ ...db, gallery: (db.gallery || []).filter((g) => g.id !== id) })
      return { ok: true }
    }
  },

  async submitDonation(payload) {
    const incoming = { ...payload }
    try {
      const amount = Number(incoming.amount)
      const res = await http.post('/api/donations', {
        amount: Number.isFinite(amount) ? amount : 0,
        method: incoming.method || '',
        name: incoming.name || '',
        email: incoming.email || '',
        phone: incoming.phone || '',
        recipientBank: incoming.recipientBank || '',
        recipientAccountNumber: incoming.recipientAccountNumber || '',
        recipientAccountName: incoming.recipientAccountName || '',
        referenceCode: incoming.referenceCode || '',
        proofUrl: incoming.proofUrl || '',
        note: incoming.note || '',
      })
      return { ok: true, id: res.data.id }
    } catch (e) {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      next.donations = next.donations || []
      const amount = Number(incoming.amount)
      if (!Number.isFinite(amount) || amount <= 0) throw new Error('Nominal donasi tidak valid.')
      const recipientBank = String(incoming.recipientBank || '').trim()
      const recipientAccountNumber = String(incoming.recipientAccountNumber || '').trim()
      const recipientAccountName = String(incoming.recipientAccountName || '').trim()
      if (!recipientBank || !recipientAccountNumber || !recipientAccountName) throw new Error('Rekening penerima wajib diisi.')
      const item = {
        id: mockDb.uid('don'),
        amount,
        method: String(incoming.method || ''),
        name: String(incoming.name || ''),
        email: String(incoming.email || ''),
        phone: String(incoming.phone || ''),
        recipientBank,
        recipientAccountNumber,
        recipientAccountName,
        referenceCode: String(incoming.referenceCode || ''),
        proofUrl: String(incoming.proofUrl || ''),
        note: String(incoming.note || ''),
        status: 'baru',
        createdAt: new Date().toISOString(),
      }
      next.donations = [item, ...next.donations]
      mockDb.write(next)
      return { ok: true, id: item.id }
    }
  },

  async submitMessage(payload) {
    const incoming = { ...payload }
    try {
      await http.post('/api/contacts', {
        name: incoming.name,
        email: incoming.email,
        subject: incoming.subject,
        message: incoming.message,
      })
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      const name = String(incoming.name || '').trim()
      const email = String(incoming.email || '').trim()
      const subject = String(incoming.subject || '').trim()
      const message = String(incoming.message || '').trim()

      if (!name) throw new Error('Nama wajib diisi.')
      if (!email) throw new Error('Email wajib diisi.')
      if (!message) throw new Error('Pesan wajib diisi.')

      next.messages = [
        { id: mockDb.uid('msg'), name, email, subject, message, status: 'baru', createdAt: new Date().toISOString() },
        ...next.messages,
      ]
      mockDb.write(next)
      return { ok: true }
    }
  },

  async listMessages({ status, page, pageSize } = {}) {
    try {
      const res = await http.get('/api/admin/contacts', { params: { status, page, pageSize } })
      const mapped = res.data.data.map((m) => ({
        id: m.id,
        name: m.name,
        email: m.email,
        subject: m.subject,
        message: m.message,
        status: m.status,
        createdAt: m.createdAt,
      }))
      return { ...res.data, data: mapped }
    } catch {
      await simulate()
      const db = mockDb.read()
      const normalized = String(status || '').trim()
      let items = [...db.messages].sort(byDateDesc)
      if (normalized) items = items.filter((m) => m.status === normalized)
      return paginate(items, { page, pageSize })
    }
  },

  async updateMessageStatus(id, status) {
    try {
      await http.put(`/api/admin/contacts/${id}/status`, { status: String(status) })
      return { ok: true }
    } catch {
      await simulate()
      const db = mockDb.read()
      const next = { ...db }
      next.messages = next.messages.map((m) => (m.id === id ? { ...m, status: String(status) } : m))
      mockDb.write(next)
      return { ok: true }
    }
  },

  async getAdminStats() {
    try {
      const res = await http.get('/api/admin/dashboard')
      return {
        reports: { total: res.data.totalReports, byStatus: res.data.reportsByStatus },
        messagesNew: res.data.newContacts,
        visitors: {
          daily: res.data.visitorsDaily,
          monthly: res.data.visitorsMonthly,
          yearly: res.data.visitorsYearly,
        },
        donations: {
          total: res.data.donationsTotal,
          monthly: res.data.donationsMonthly,
        },
        chart: res.data.chart,
        recentReports: (res.data.recentReports || []).map((r) => ({
          id: r.id,
          chronology: r.chronology,
          status: r.status,
          createdAt: r.createdAt,
        })),
        recentMessages: (res.data.recentContacts || []).map((m) => ({
          id: m.id,
          name: m.name,
          email: m.email,
          subject: m.subject,
          message: '',
          status: m.status,
          createdAt: m.createdAt,
        })),
      }
    } catch {
      await simulate()
      const db = mockDb.read()
      const counts = db.reports.reduce(
        (acc, r) => {
          acc.total += 1
          acc.byStatus[r.status] = (acc.byStatus[r.status] || 0) + 1
          return acc
        },
        { total: 0, byStatus: {} },
      )

      const now = new Date()
      const monthKey = (d) => `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}`
      const buckets = []
      for (let i = 5; i >= 0; i -= 1) {
        const d = new Date(now.getFullYear(), now.getMonth() - i, 1)
        buckets.push({ key: monthKey(d), count: 0 })
      }
      const map = new Map(buckets.map((b) => [b.key, b]))
      for (const r of db.reports) {
        const d = new Date(r.createdAt)
        const k = monthKey(new Date(d.getFullYear(), d.getMonth(), 1))
        const bucket = map.get(k)
        if (bucket) bucket.count += 1
      }

      const now2 = new Date()
      const dayKey = `${now2.getFullYear()}-${String(now2.getMonth() + 1).padStart(2, '0')}-${String(now2.getDate()).padStart(2, '0')}`
      const monthPrefix = `${now2.getFullYear()}-${String(now2.getMonth() + 1).padStart(2, '0')}`
      const yearPrefix = `${now2.getFullYear()}-`
      const visitsDaily = db.visitsDaily || {}
      const visitorsDaily = visitsDaily[dayKey] || 0
      const visitorsMonthly = Object.entries(visitsDaily)
        .filter(([k]) => k.startsWith(monthPrefix))
        .reduce((acc, [, v]) => acc + Number(v || 0), 0)
      const visitorsYearly = Object.entries(visitsDaily)
        .filter(([k]) => k.startsWith(yearPrefix))
        .reduce((acc, [, v]) => acc + Number(v || 0), 0)

      const donations = db.donations || []
      const donationsTotal = donations.reduce((acc, d) => acc + Number(d.amount || 0), 0)
      const donationsMonthly = donations
        .filter((d) => String(d.createdAt || '').startsWith(monthPrefix))
        .reduce((acc, d) => acc + Number(d.amount || 0), 0)

      return {
        reports: counts,
        messagesNew: db.messages.filter((m) => m.status === 'baru').length,
        visitors: { daily: visitorsDaily, monthly: visitorsMonthly, yearly: visitorsYearly },
        donations: { total: donationsTotal, monthly: donationsMonthly },
        chart: buckets,
        recentReports: [...db.reports].sort(byDateDesc).slice(0, 5),
        recentMessages: [...db.messages].sort(byDateDesc).slice(0, 5),
      }
    }
  },
}
