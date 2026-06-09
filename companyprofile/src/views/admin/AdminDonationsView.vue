<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppPagination from '../../components/ui/AppPagination.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import { api } from '../../services/mockApi'

const loading = ref(false)
const list = ref([])
const page = ref(1)
const totalPages = ref(1)
const status = ref('')
const selectedId = ref('')

const statusOptions = [
  { label: 'Semua', value: '' },
  { label: 'Baru', value: 'baru' },
  { label: 'Diverifikasi', value: 'diverifikasi' },
  { label: 'Ditolak', value: 'ditolak' },
]

const selected = computed(() => list.value.find((x) => x.id === selectedId.value) || null)

const idr = new Intl.NumberFormat('id-ID', { style: 'currency', currency: 'IDR', maximumFractionDigits: 0 })
function formatIdr(value) {
  const n = Number(value)
  return Number.isFinite(n) ? idr.format(n) : '—'
}

function formatDate(value) {
  const d = value ? new Date(value) : null
  return d && !Number.isNaN(d.getTime()) ? d.toLocaleString('id-ID') : '—'
}

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listDonations({ status: status.value, page: page.value, pageSize: 10 })
    list.value = res.data || []
    totalPages.value = res.totalPages || 1
    if (selectedId.value && !list.value.some((x) => x.id === selectedId.value)) selectedId.value = ''
  } finally {
    loading.value = false
  }
}

async function setStatus(id, next) {
  await api.updateDonationStatus(id, next)
  await fetchList()
}

watch(status, () => {
  page.value = 1
  selectedId.value = ''
  fetchList()
})

watch(page, fetchList)

onMounted(fetchList)
</script>

<template>
  <div class="flex flex-wrap items-end justify-between gap-4">
    <div>
      <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Manajemen</div>
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Donasi</h1>
    </div>
    <AppButton variant="ghost" :disabled="loading" @click="fetchList">Refresh</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-8">
      <AppCard class="p-5">
        <div class="grid gap-3 md:grid-cols-12 md:items-end">
          <div class="md:col-span-6">
            <AppSelect v-model="status" label="Status" :options="statusOptions" />
          </div>
        </div>

        <div class="mt-5 overflow-hidden rounded-xl ring-1 ring-slate-200/70">
          <table class="w-full text-left text-sm">
            <thead class="bg-slate-50 text-xs font-extrabold uppercase tracking-wider text-slate-600">
              <tr>
                <th class="px-4 py-3">Donatur</th>
                <th class="px-4 py-3">Nominal</th>
                <th class="px-4 py-3">Rekening</th>
                <th class="px-4 py-3">Status</th>
                <th class="px-4 py-3">Tanggal</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-200/70 bg-white">
              <tr v-if="loading">
                <td colspan="5" class="px-4 py-6 text-slate-600">Memuat…</td>
              </tr>
              <tr
                v-for="d in list"
                :key="d.id"
                class="cursor-pointer hover:bg-slate-50 even:bg-slate-50/40"
                :class="selectedId === d.id ? '!bg-emerald-50/70' : ''"
                @click="selectedId = d.id"
              >
                <td class="px-4 py-3">
                  <div class="font-semibold text-slate-900">{{ d.donorName || d.name || '—' }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ d.donorEmail || d.email || '—' }}</div>
                </td>
                <td class="px-4 py-3">
                  <div class="font-extrabold text-slate-900">{{ formatIdr(d.amount) }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ d.method }}</div>
                </td>
                <td class="px-4 py-3">
                  <div class="text-xs font-semibold text-slate-700">{{ d.recipientBank || d.recipientBank }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ d.recipientAccountNumber }}</div>
                </td>
                <td class="px-4 py-3">
                  <AppBadge :tone="d.status === 'diverifikasi' ? 'emerald' : d.status === 'ditolak' ? 'rose' : 'amber'">
                    {{ d.status }}
                  </AppBadge>
                </td>
                <td class="px-4 py-3 text-xs text-slate-600">{{ formatDate(d.createdAt) }}</td>
              </tr>
              <tr v-if="!loading && !list.length">
                <td colspan="5" class="px-4 py-6 text-slate-600">Belum ada data donasi.</td>
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
      <AppCard class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Detail Donasi</div>
        <div v-if="!selected" class="mt-3 text-sm text-slate-600">Klik salah satu donasi untuk melihat rincian.</div>
        <div v-else class="mt-4 grid gap-3 text-sm text-slate-700">
          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Donatur</div>
            <div class="mt-1 font-semibold text-slate-900">{{ selected.donorName || selected.name || '—' }}</div>
            <div class="mt-1 text-xs text-slate-500">{{ selected.donorEmail || selected.email || '—' }}</div>
            <div v-if="selected.donorPhone || selected.phone" class="mt-1 text-xs text-slate-500">{{ selected.donorPhone || selected.phone }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Rekening penerima</div>
            <div class="mt-1 font-semibold text-slate-900">
              {{ selected.recipientBank }} · {{ selected.recipientAccountNumber }}
            </div>
            <div class="mt-1 text-xs text-slate-500">a/n {{ selected.recipientAccountName }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Rincian</div>
            <div class="mt-1 font-extrabold text-slate-900">{{ formatIdr(selected.amount) }}</div>
            <div class="mt-1 text-xs text-slate-500">Metode: {{ selected.method }}</div>
            <div v-if="selected.referenceCode" class="mt-1 text-xs text-slate-500">Ref: {{ selected.referenceCode }}</div>
          </div>

          <div v-if="selected.proofUrl" class="rounded-xl bg-white p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Bukti</div>
            <a class="mt-2 block break-all text-sm font-semibold text-emerald-700 hover:underline" :href="selected.proofUrl" target="_blank" rel="noreferrer">
              {{ selected.proofUrl }}
            </a>
          </div>

          <div v-if="selected.note" class="rounded-xl bg-white p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Catatan</div>
            <div class="mt-2 whitespace-pre-wrap text-sm text-slate-700">{{ selected.note }}</div>
          </div>

          <div class="rounded-xl bg-white p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Update status</div>
            <div class="mt-3 grid gap-2">
              <AppButton variant="soft" @click="setStatus(selected.id, 'diverifikasi')">Verifikasi</AppButton>
              <AppButton variant="danger" @click="setStatus(selected.id, 'ditolak')">Tolak</AppButton>
              <AppButton variant="ghost" @click="setStatus(selected.id, 'baru')">Kembalikan ke baru</AppButton>
            </div>
          </div>
        </div>
      </AppCard>
    </div>
  </div>
</template>

