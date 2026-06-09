<script setup>
import { onMounted, reactive, ref, watch } from 'vue'
import { api } from '../../services/mockApi'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import AppPagination from '../../components/ui/AppPagination.vue'

const loading = ref(false)
const items = ref([])
const q = ref('')
const page = ref(1)
const totalPages = ref(1)

const editorOpen = ref(false)
const saving = ref(false)
const errors = reactive({})

const form = reactive({
  id: '',
  title: '',
  description: '',
  icon: 'spark',
  imageUrl: '',
  highlightsText: '',
})

const iconOptions = [
  { label: 'Spark', value: 'spark' },
  { label: 'Scale', value: 'scale' },
  { label: 'Heart', value: 'heart' },
  { label: 'Phone', value: 'phone' },
  { label: 'Shield', value: 'shield' },
  { label: 'Users', value: 'users' },
  { label: 'Book', value: 'book' },
]

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listAdminServices({ q: q.value, page: page.value, pageSize: 10 })
    items.value = res.data || []
    totalPages.value = res.totalPages || 1
  } finally {
    loading.value = false
  }
}

function resetForm() {
  form.id = ''
  form.title = ''
  form.description = ''
  form.icon = 'spark'
  form.imageUrl = ''
  form.highlightsText = ''
  errors.title = ''
}

function createNew() {
  editorOpen.value = true
  resetForm()
}

function edit(item) {
  editorOpen.value = true
  form.id = item.id
  form.title = item.title
  form.description = item.description
  form.icon = item.icon
  form.imageUrl = item.imageUrl || ''
  form.highlightsText = (item.highlights || []).join('\n')
}

async function save() {
  errors.title = ''
  if (!String(form.title || '').trim()) {
    errors.title = 'Nama layanan wajib diisi.'
    return
  }
  saving.value = true
  try {
    const highlights = String(form.highlightsText || '')
      .split('\n')
      .map((x) => x.trim())
      .filter(Boolean)
      .slice(0, 6)
    await api.upsertService({
      id: form.id || undefined,
      title: form.title,
      description: form.description,
      icon: form.icon,
      imageUrl: form.imageUrl,
      highlights,
    })
    await fetchList()
    editorOpen.value = false
  } catch (e) {
    errors.title = e?.message || 'Terjadi kesalahan.'
  } finally {
    saving.value = false
  }
}

async function remove(id) {
  if (!confirm('Hapus layanan ini?')) return
  await api.deleteService(id)
  await fetchList()
}

watch(q, () => {
  page.value = 1
  fetchList()
})

watch(page, () => fetchList())

onMounted(fetchList)
</script>

<template>
  <div class="flex flex-wrap items-end justify-between gap-4">
    <div>
      <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Manajemen</div>
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Layanan</h1>
    </div>
    <AppButton variant="primary" @click="createNew">Tambah Layanan</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-7">
      <AppCard class="p-5">
        <div class="mb-4">
          <AppInput v-model="q" label="Cari" placeholder="Nama / deskripsi layanan" />
        </div>
        <div class="overflow-hidden rounded-xl ring-1 ring-slate-200/70">
          <table class="w-full text-left text-sm">
            <thead class="bg-slate-50 text-xs font-extrabold uppercase tracking-wider text-slate-600">
              <tr>
                <th class="px-4 py-3">Layanan</th>
                <th class="px-4 py-3">Highlight</th>
                <th class="px-4 py-3 text-right">Aksi</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-200/70 bg-white">
              <tr v-if="loading">
                <td colspan="3" class="px-4 py-6 text-slate-600">Memuat…</td>
              </tr>
              <tr v-for="s in items" :key="s.id" class="hover:bg-slate-50 even:bg-slate-50/40">
                <td class="px-4 py-3">
                  <div class="flex items-start gap-3">
                    <div class="grid h-10 w-10 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
                      <AppIcon :name="s.icon" class="h-5 w-5 text-emerald-700" />
                    </div>
                    <div>
                      <div class="font-semibold text-slate-900">{{ s.title }}</div>
                      <div class="mt-0.5 text-xs text-slate-500">{{ s.description }}</div>
                    </div>
                  </div>
                </td>
                <td class="px-4 py-3">
                  <div class="grid gap-1 text-xs text-slate-600">
                    <div v-for="h in s.highlights" :key="h" class="flex items-center gap-2">
                      <span class="h-1.5 w-1.5 rounded-full bg-amber-400" />
                      <span>{{ h }}</span>
                    </div>
                  </div>
                </td>
                <td class="px-4 py-3 text-right">
                  <div class="flex justify-end gap-2">
                    <AppButton variant="ghost" size="sm" @click="edit(s)">Edit</AppButton>
                    <AppButton variant="danger" size="sm" @click="remove(s.id)">Hapus</AppButton>
                  </div>
                </td>
              </tr>
              <tr v-if="!loading && !items.length">
                <td colspan="3" class="px-4 py-6 text-slate-600">Belum ada layanan.</td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="mt-4">
          <AppPagination v-model:page="page" :total-pages="totalPages" />
        </div>
      </AppCard>
    </div>

    <div class="lg:col-span-5">
      <AppCard v-if="editorOpen" class="p-5">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-sm font-extrabold text-slate-900">{{ form.id ? 'Edit Layanan' : 'Layanan Baru' }}</div>
            <div class="mt-1 text-xs text-slate-500">Pisahkan highlight per baris.</div>
          </div>
          <button type="button" class="rounded-md p-2 text-slate-500 hover:bg-slate-100" @click="editorOpen = false">
            <svg viewBox="0 0 24 24" class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>

        <div class="mt-5 grid gap-4">
          <AppInput v-model="form.title" label="Nama layanan" :error="errors.title" />
          <AppSelect v-model="form.icon" label="Icon" :options="iconOptions" />
          <AppInput v-model="form.imageUrl" label="Gambar URL (opsional)" placeholder="https://..." />
          <AppTextarea v-model="form.description" label="Deskripsi" :rows="3" />
          <AppTextarea v-model="form.highlightsText" label="Highlight" :rows="5" placeholder="Contoh:\nKonsultasi awal\nPendampingan pelaporan" />

          <div class="grid gap-2">
            <AppButton :loading="saving" variant="primary" @click="save">Simpan</AppButton>
            <AppButton variant="ghost" @click="resetForm">Reset</AppButton>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Editor</div>
        <div class="mt-2 text-sm text-slate-600">Pilih layanan untuk edit, atau tambah layanan baru.</div>
        <div class="mt-4">
          <AppButton variant="primary" @click="createNew">Tambah Layanan</AppButton>
        </div>
      </AppCard>
    </div>
  </div>
</template>
