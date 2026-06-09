<script setup>
import { computed, onMounted, reactive, ref, watch } from 'vue'
import { api } from '../../services/mockApi'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppPagination from '../../components/ui/AppPagination.vue'
import AppSelect from '../../components/ui/AppSelect.vue'

const loading = ref(false)
const items = ref([])
const page = ref(1)
const totalPages = ref(1)
const category = ref('')

const editorOpen = ref(false)
const saving = ref(false)
const errors = reactive({})

const form = reactive({
  id: '',
  title: '',
  alt: '',
  category: '',
  imageUrl: '',
  sortOrder: 0,
  file: null,
})

const categoryOptions = computed(() => {
  const set = new Set(items.value.map((x) => x.category).filter(Boolean))
  const list = Array.from(set)
  list.sort((a, b) => a.localeCompare(b))
  return [{ label: 'Semua', value: '' }, ...list.map((x) => ({ label: x, value: x }))]
})

function resetForm() {
  form.id = ''
  form.title = ''
  form.alt = ''
  form.category = ''
  form.imageUrl = ''
  form.sortOrder = 0
  form.file = null
  errors.imageUrl = ''
}

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listGalleryAdmin({ category: category.value, page: page.value, pageSize: 12 })
    items.value = res.data
    totalPages.value = res.totalPages
  } finally {
    loading.value = false
  }
}

function createNew() {
  editorOpen.value = true
  resetForm()
}

function edit(item) {
  editorOpen.value = true
  form.id = item.id
  form.title = item.title || ''
  form.alt = item.alt || ''
  form.category = item.category || ''
  form.imageUrl = item.imageUrl || item.src || ''
  form.sortOrder = Number(item.sortOrder) || 0
  form.file = null
  errors.imageUrl = ''
}

async function onFileChange(e) {
  const input = e.target
  const file = input.files?.[0] || null
  form.file = file
}

async function save() {
  errors.imageUrl = ''
  saving.value = true
  try {
    if (form.file) {
      const urls = await api.uploadAdminFiles([form.file])
      if (urls?.length) form.imageUrl = urls[0]
    }

    if (!String(form.imageUrl || '').trim()) {
      errors.imageUrl = 'Gambar wajib diisi (URL atau upload).'
      return
    }

    await api.upsertGalleryPhoto({
      id: form.id || undefined,
      title: form.title,
      alt: form.alt,
      category: form.category,
      imageUrl: form.imageUrl,
      sortOrder: Number(form.sortOrder) || 0,
    })
    await fetchList()
    editorOpen.value = false
  } catch (e) {
    errors.imageUrl = e?.message || 'Terjadi kesalahan.'
  } finally {
    saving.value = false
  }
}

async function remove(id) {
  if (!confirm('Hapus foto ini?')) return
  await api.deleteGalleryPhoto(id)
  await fetchList()
}

watch(category, () => {
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
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Galeri</h1>
    </div>
    <AppButton variant="primary" @click="createNew">Tambah Foto</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-8">
      <AppCard class="p-5">
        <div class="grid gap-3 md:grid-cols-12 md:items-end">
          <div class="md:col-span-6">
            <AppSelect v-model="category" label="Kategori" :options="categoryOptions" />
          </div>
        </div>

        <div class="mt-5 grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
          <div v-if="loading" class="rounded-xl bg-white p-5 text-sm text-slate-600 ring-1 ring-slate-200/70">Memuat…</div>
          <div
            v-for="g in items"
            :key="g.id"
            class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-0.5 hover:shadow-lg"
          >
            <div class="overflow-hidden">
              <img :src="g.imageUrl || g.src" :alt="g.alt || g.title" class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105" />
            </div>
            <div class="p-4">
              <div class="text-sm font-extrabold text-slate-900">{{ g.title || 'Foto' }}</div>
              <div class="mt-1 text-xs text-slate-500">{{ g.category || 'Tanpa kategori' }} · Sort {{ g.sortOrder || 0 }}</div>
              <div class="mt-3 flex gap-2">
                <AppButton size="sm" variant="ghost" @click="edit(g)">Edit</AppButton>
                <AppButton size="sm" variant="danger" @click="remove(g.id)">Hapus</AppButton>
              </div>
            </div>
          </div>
          <div v-if="!loading && !items.length" class="rounded-xl bg-white p-5 text-sm text-slate-600 ring-1 ring-slate-200/70">Belum ada foto.</div>
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
            <div class="text-sm font-extrabold text-slate-900">{{ form.id ? 'Edit Foto' : 'Foto Baru' }}</div>
            <div class="mt-1 text-xs text-slate-500">Upload atau isi URL gambar.</div>
          </div>
          <button type="button" class="rounded-md p-2 text-slate-500 hover:bg-slate-100" @click="editorOpen = false">
            <svg viewBox="0 0 24 24" class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>

        <div class="mt-5 grid gap-4">
          <AppInput v-model="form.title" label="Judul (opsional)" placeholder="Contoh: Edukasi komunitas" />
          <AppInput v-model="form.alt" label="Alt text (opsional)" placeholder="Deskripsi singkat foto" />
          <AppInput v-model="form.category" label="Kategori (opsional)" placeholder="Contoh: Edukasi" />
          <AppInput v-model="form.sortOrder" label="Urutan (sort)" type="number" />
          <AppInput v-model="form.imageUrl" label="Image URL" placeholder="https://..." :error="errors.imageUrl" />
          <label class="grid gap-2 text-sm font-semibold text-slate-700">
            Upload gambar
            <input type="file" accept="image/*" class="block w-full rounded-xl bg-white p-3 shadow-md ring-1 ring-slate-200/70" @change="onFileChange" />
          </label>

          <div class="grid gap-2">
            <AppButton :loading="saving" variant="primary" @click="save">Simpan</AppButton>
            <AppButton variant="ghost" @click="resetForm">Reset</AppButton>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Editor</div>
        <div class="mt-2 text-sm text-slate-600">Klik Edit pada foto, atau tambah foto baru.</div>
        <div class="mt-4">
          <AppButton variant="primary" @click="createNew">Tambah Foto</AppButton>
        </div>
      </AppCard>
    </div>
  </div>
</template>
