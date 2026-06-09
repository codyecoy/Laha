<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { api } from '../../services/mockApi'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import AppRichTextEditor from '../../components/ui/AppRichTextEditor.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppPagination from '../../components/ui/AppPagination.vue'
import { useContentStore } from '../../store/content'

const content = useContentStore()

const loading = ref(false)
const items = ref([])
const page = ref(1)
const totalPages = ref(1)
const q = ref('')
const category = ref('')

const editorOpen = ref(false)
const saving = ref(false)
const loadingEditor = ref(false)
const errors = reactive({})

const form = reactive({
  id: '',
  title: '',
  slug: '',
  category: 'Edukasi',
  thumbnailUrl: '',
  excerpt: '',
  content: '',
  isPublished: true,
  readingTime: 6,
  coverTone: 'emerald',
  featured: false,
})

const coverOptions = [
  { label: 'Emerald', value: 'emerald' },
  { label: 'Amber', value: 'amber' },
  { label: 'Slate', value: 'slate' },
]

const categoryOptions = computed(() => {
  const set = new Set(content.articleCategories)
  const list = Array.from(set)
  if (!list.includes('Edukasi')) list.unshift('Edukasi')
  if (!list.includes('Perlindungan')) list.push('Perlindungan')
  if (!list.includes('Pendampingan')) list.push('Pendampingan')
  return list.map((x) => ({ label: x, value: x }))
})

function resetForm() {
  form.id = ''
  form.title = ''
  form.slug = ''
  form.category = 'Edukasi'
  form.thumbnailUrl = ''
  form.excerpt = ''
  form.content = ''
  form.isPublished = true
  form.readingTime = 6
  form.coverTone = 'emerald'
  form.featured = false
  errors.title = ''
}

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listAdminArticles({ q: q.value, category: category.value, page: page.value, pageSize: 10 })
    items.value = res.data
    totalPages.value = res.totalPages
  } finally {
    loading.value = false
  }
}

async function edit(item) {
  editorOpen.value = true
  loadingEditor.value = true
  errors.title = ''
  try {
    const detail = await api.getAdminArticle(item.id)
    if (!detail) return
    form.id = detail.id
    form.title = detail.title
    form.slug = detail.slug
    form.category = detail.category
    form.thumbnailUrl = detail.thumbnailUrl || ''
    form.excerpt = detail.excerpt
    form.content = detail.content
    form.isPublished = detail.isPublished !== false
    form.readingTime = detail.readingTime
    form.coverTone = detail.coverTone
    form.featured = detail.featured
  } finally {
    loadingEditor.value = false
  }
}

function createNew() {
  editorOpen.value = true
  resetForm()
}

async function save() {
  errors.title = ''
  if (!String(form.title || '').trim()) {
    errors.title = 'Judul wajib diisi.'
    return
  }
  saving.value = true
  try {
    await api.upsertArticle({ ...form, readingTime: Number(form.readingTime) })
    await content.refreshArticles()
    await fetchList()
    editorOpen.value = false
  } catch (e) {
    errors.title = e?.message || 'Terjadi kesalahan.'
  } finally {
    saving.value = false
  }
}

async function remove(id) {
  if (!confirm('Hapus artikel ini?')) return
  await api.deleteArticle(id)
  await content.refreshArticles()
  await fetchList()
}

watch([q, category], () => {
  page.value = 1
  fetchList()
})

watch(page, () => fetchList())

onMounted(async () => {
  if (!content.articles.length) await content.hydrate()
  await fetchList()
})
</script>

<template>
  <div class="flex flex-wrap items-end justify-between gap-4">
    <div>
      <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Manajemen</div>
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Artikel</h1>
    </div>
    <AppButton variant="primary" @click="createNew">Tambah Artikel</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-8">
      <AppCard class="p-5">
        <div class="grid gap-3 md:grid-cols-12 md:items-end">
          <div class="md:col-span-7">
            <AppInput v-model="q" label="Cari" placeholder="Judul / ringkasan" />
          </div>
          <div class="md:col-span-5">
            <AppSelect v-model="category" label="Kategori" :options="[{ label: 'Semua', value: '' }, ...categoryOptions]" />
          </div>
        </div>

        <div class="mt-5 overflow-hidden rounded-xl ring-1 ring-slate-200/70">
          <table class="w-full text-left text-sm">
            <thead class="bg-slate-50 text-xs font-extrabold uppercase tracking-wider text-slate-600">
              <tr>
                <th class="px-4 py-3">Judul</th>
                <th class="px-4 py-3">Kategori</th>
                <th class="px-4 py-3">Status</th>
                <th class="px-4 py-3 text-right">Aksi</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-200/70 bg-white">
              <tr v-if="loading">
                <td colspan="4" class="px-4 py-6 text-slate-600">Memuat…</td>
              </tr>
              <tr v-for="a in items" :key="a.id" class="hover:bg-slate-50 even:bg-slate-50/40">
                <td class="px-4 py-3">
                  <div class="font-semibold text-slate-900">{{ a.title }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ a.slug }}</div>
                </td>
                <td class="px-4 py-3">
                  <AppBadge :tone="a.coverTone === 'amber' ? 'amber' : a.coverTone === 'slate' ? 'slate' : 'emerald'">
                    {{ a.category }}
                  </AppBadge>
                </td>
                <td class="px-4 py-3">
                  <div class="grid gap-1">
                    <AppBadge v-if="a.isPublished" tone="emerald">Published</AppBadge>
                    <AppBadge v-else tone="amber">Draft</AppBadge>
                    <AppBadge v-if="a.featured" tone="slate">Featured</AppBadge>
                  </div>
                </td>
                <td class="px-4 py-3 text-right">
                  <div class="flex justify-end gap-2">
                    <AppButton variant="ghost" size="sm" @click="edit(a)">Edit</AppButton>
                    <AppButton variant="danger" size="sm" @click="remove(a.id)">Hapus</AppButton>
                  </div>
                </td>
              </tr>
              <tr v-if="!loading && !items.length">
                <td colspan="4" class="px-4 py-6 text-slate-600">Belum ada artikel.</td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="mt-4">
          <AppPagination v-model:page="page" :total-pages="totalPages" />
        </div>
      </AppCard>
    </div>

    <div class="lg:col-span-4">
      <AppCard v-if="editorOpen" class="p-5">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-sm font-extrabold text-slate-900">{{ form.id ? 'Edit Artikel' : 'Artikel Baru' }}</div>
            <div class="mt-1 text-xs text-slate-500">Editor rich text untuk menulis seperti dokumen.</div>
          </div>
          <button type="button" class="rounded-md p-2 text-slate-500 hover:bg-slate-100" @click="editorOpen = false">
            <svg viewBox="0 0 24 24" class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>

        <div class="mt-5 grid gap-4">
          <AppInput v-model="form.title" label="Judul" :error="errors.title" />
          <AppInput v-model="form.slug" label="Slug (opsional)" placeholder="otomatis dari judul" />
          <AppInput v-model="form.thumbnailUrl" label="Thumbnail URL (opsional)" placeholder="https://..." />
          <AppSelect v-model="form.category" label="Kategori" :options="categoryOptions" />
          <div class="grid gap-4 sm:grid-cols-2">
            <AppSelect v-model="form.coverTone" label="Tone" :options="coverOptions" />
            <AppInput v-model="form.readingTime" label="Waktu baca (menit)" type="number" />
          </div>
          <label class="inline-flex items-center gap-2 rounded-md bg-slate-50 p-3 text-sm font-semibold text-slate-700 ring-1 ring-slate-200/70">
            <input v-model="form.isPublished" type="checkbox" class="h-4 w-4 rounded border-slate-300 text-emerald-600 focus:ring-emerald-500/25" />
            Publish (tampil di publik)
          </label>
          <label class="inline-flex items-center gap-2 rounded-md bg-slate-50 p-3 text-sm font-semibold text-slate-700 ring-1 ring-slate-200/70">
            <input v-model="form.featured" type="checkbox" class="h-4 w-4 rounded border-slate-300 text-emerald-600 focus:ring-emerald-500/25" />
            Jadikan featured (muncul di Home)
          </label>
          <AppTextarea v-model="form.excerpt" label="Ringkasan" :rows="3" />
          <AppRichTextEditor v-model="form.content" label="Konten" hint="Konten disimpan sebagai HTML." />

          <div class="grid gap-2">
            <AppButton :loading="saving || loadingEditor" variant="primary" @click="save">Simpan</AppButton>
            <AppButton variant="ghost" @click="resetForm">Reset</AppButton>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Editor</div>
        <div class="mt-2 text-sm text-slate-600">Pilih artikel untuk edit, atau tambah artikel baru.</div>
        <div class="mt-4">
          <AppButton variant="primary" @click="createNew">Tambah Artikel</AppButton>
        </div>
      </AppCard>
    </div>
  </div>
</template>
