<script setup>
import { onMounted, reactive, ref, watch } from 'vue'
import { api } from '../../services/mockApi'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
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
  name: '',
  role: '',
  bio: '',
  tone: 'emerald',
  photoUrl: '',
})

const toneOptions = [
  { label: 'Emerald', value: 'emerald' },
  { label: 'Amber', value: 'amber' },
  { label: 'Slate', value: 'slate' },
]

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listAdminTeam({ q: q.value, page: page.value, pageSize: 10 })
    items.value = res.data || []
    totalPages.value = res.totalPages || 1
  } finally {
    loading.value = false
  }
}

function resetForm() {
  form.id = ''
  form.name = ''
  form.role = ''
  form.bio = ''
  form.tone = 'emerald'
  form.photoUrl = ''
  errors.name = ''
}

function createNew() {
  editorOpen.value = true
  resetForm()
}

function edit(item) {
  editorOpen.value = true
  form.id = item.id
  form.name = item.name
  form.role = item.role
  form.bio = item.bio
  form.tone = item.tone || 'emerald'
  form.photoUrl = item.photoUrl || ''
}

async function save() {
  errors.name = ''
  if (!String(form.name || '').trim()) {
    errors.name = 'Nama wajib diisi.'
    return
  }
  saving.value = true
  try {
    await api.upsertTeamMember({ ...form })
    await fetchList()
    editorOpen.value = false
  } catch (e) {
    errors.name = e?.message || 'Terjadi kesalahan.'
  } finally {
    saving.value = false
  }
}

async function remove(id) {
  if (!confirm('Hapus anggota tim ini?')) return
  await api.deleteTeamMember(id)
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
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Tim</h1>
    </div>
    <AppButton variant="primary" @click="createNew">Tambah Anggota</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-7">
      <AppCard class="p-5">
        <div class="mb-4">
          <AppInput v-model="q" label="Cari" placeholder="Nama / jabatan / bio" />
        </div>
        <div class="overflow-hidden rounded-xl ring-1 ring-slate-200/70">
          <table class="w-full text-left text-sm">
            <thead class="bg-slate-50 text-xs font-extrabold uppercase tracking-wider text-slate-600">
              <tr>
                <th class="px-4 py-3">Nama</th>
                <th class="px-4 py-3">Peran</th>
                <th class="px-4 py-3">Tone</th>
                <th class="px-4 py-3 text-right">Aksi</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-200/70 bg-white">
              <tr v-if="loading">
                <td colspan="4" class="px-4 py-6 text-slate-600">Memuat…</td>
              </tr>
              <tr v-for="t in items" :key="t.id" class="hover:bg-slate-50 even:bg-slate-50/40">
                <td class="px-4 py-3">
                  <div class="font-semibold text-slate-900">{{ t.name }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ t.bio }}</div>
                </td>
                <td class="px-4 py-3 text-sm font-semibold text-slate-700">{{ t.role }}</td>
                <td class="px-4 py-3">
                  <AppBadge :tone="t.tone === 'amber' ? 'amber' : t.tone === 'slate' ? 'slate' : 'emerald'">{{ t.tone }}</AppBadge>
                </td>
                <td class="px-4 py-3 text-right">
                  <div class="flex justify-end gap-2">
                    <AppButton variant="ghost" size="sm" @click="edit(t)">Edit</AppButton>
                    <AppButton variant="danger" size="sm" @click="remove(t.id)">Hapus</AppButton>
                  </div>
                </td>
              </tr>
              <tr v-if="!loading && !items.length">
                <td colspan="4" class="px-4 py-6 text-slate-600">Belum ada anggota tim.</td>
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
            <div class="text-sm font-extrabold text-slate-900">{{ form.id ? 'Edit Anggota' : 'Anggota Baru' }}</div>
          </div>
          <button type="button" class="rounded-md p-2 text-slate-500 hover:bg-slate-100" @click="editorOpen = false">
            <svg viewBox="0 0 24 24" class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>

        <div class="mt-5 grid gap-4">
          <AppInput v-model="form.name" label="Nama" :error="errors.name" />
          <AppInput v-model="form.role" label="Jabatan" placeholder="Misal: Koordinator Advokasi" />
          <AppInput v-model="form.photoUrl" label="Foto URL (opsional)" placeholder="https://..." />
          <AppSelect v-model="form.tone" label="Tone" :options="toneOptions" />
          <AppTextarea v-model="form.bio" label="Bio singkat" :rows="4" />

          <div class="grid gap-2">
            <AppButton :loading="saving" variant="primary" @click="save">Simpan</AppButton>
            <AppButton variant="ghost" @click="resetForm">Reset</AppButton>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Editor</div>
        <div class="mt-2 text-sm text-slate-600">Pilih anggota untuk edit, atau tambah anggota baru.</div>
        <div class="mt-4">
          <AppButton variant="primary" @click="createNew">Tambah Anggota</AppButton>
        </div>
      </AppCard>
    </div>
  </div>
</template>
