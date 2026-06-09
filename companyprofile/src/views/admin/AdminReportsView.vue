<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useReportsStore } from '../../store/reports'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppPagination from '../../components/ui/AppPagination.vue'

const store = useReportsStore()
const selectedId = ref('')

const selected = computed(() => store.list.find((r) => r.id === selectedId.value) || null)

const statusOptions = [
  { label: 'Semua status', value: '' },
  { label: 'baru', value: 'baru' },
  { label: 'ditinjau', value: 'ditinjau' },
  { label: 'diproses', value: 'diproses' },
  { label: 'selesai', value: 'selesai' },
]

const statusTone = (s) => (s === 'baru' ? 'emerald' : s === 'ditinjau' ? 'amber' : s === 'selesai' ? 'rose' : 'slate')

watch(
  () => store.list,
  () => {
    if (selectedId.value && !store.list.some((x) => x.id === selectedId.value)) selectedId.value = ''
  },
)

onMounted(async () => {
  await store.fetch({ page: 1 })
})
</script>

<template>
  <div class="flex flex-wrap items-end justify-between gap-4">
    <div>
      <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Manajemen</div>
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Laporan Kasus</h1>
    </div>
    <div class="w-full sm:w-64">
      <AppSelect v-model="store.statusFilter" label="Filter status" :options="statusOptions" @update:modelValue="store.fetch({ page: 1 })" />
    </div>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="lg:col-span-8">
      <AppCard class="p-5">
        <div class="overflow-hidden rounded-xl ring-1 ring-slate-200/70">
          <table class="w-full text-left text-sm">
            <thead class="bg-slate-50 text-xs font-extrabold uppercase tracking-wider text-slate-600">
              <tr>
                <th class="px-4 py-3">ID</th>
                <th class="px-4 py-3">Kronologi</th>
                <th class="px-4 py-3">Status</th>
                <th class="px-4 py-3">Tanggal</th>
                <th class="px-4 py-3 text-right">Aksi</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-slate-200/70 bg-white">
              <tr v-if="store.loading">
                <td colspan="5" class="px-4 py-6 text-slate-600">Memuat…</td>
              </tr>
              <tr
                v-for="r in store.list"
                :key="r.id"
                class="cursor-pointer hover:bg-slate-50 even:bg-slate-50/40"
                :class="selectedId === r.id ? '!bg-emerald-50/70' : ''"
                @click="selectedId = r.id"
              >
                <td class="px-4 py-3">
                  <div class="font-semibold text-slate-900">{{ r.id }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ r.isAnonymous ? 'Anonim' : 'Tidak anonim' }}</div>
                </td>
                <td class="px-4 py-3">
                  <div class="text-sm text-slate-700">{{ r.chronology }}</div>
                </td>
                <td class="px-4 py-3">
                  <AppBadge :tone="statusTone(r.status)">{{ r.status }}</AppBadge>
                </td>
                <td class="px-4 py-3 text-xs text-slate-500">
                  {{ new Date(r.createdAt).toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' }) }}
                </td>
                <td class="px-4 py-3 text-right">
                  <AppButton variant="ghost" size="sm" @click.stop="selectedId = r.id">Detail</AppButton>
                </td>
              </tr>
              <tr v-if="!store.loading && !store.list.length">
                <td colspan="5" class="px-4 py-6 text-slate-600">Belum ada laporan.</td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="mt-4">
          <AppPagination v-model:page="store.page" :total-pages="store.totalPages" @update:page="store.fetch({ page: store.page })" />
        </div>
      </AppCard>
    </div>

    <div class="lg:col-span-4">
      <AppCard v-if="selected" class="p-5">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-sm font-extrabold text-slate-900">Detail Laporan</div>
            <div class="mt-1 text-xs text-slate-500">ID: {{ selected.id }}</div>
          </div>
          <button type="button" class="rounded-md p-2 text-slate-500 hover:bg-slate-100" @click="selectedId = ''">
            <svg viewBox="0 0 24 24" class="h-5 w-5" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>

        <div class="mt-4 grid gap-3 text-sm text-slate-700">
          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Pelapor</div>
            <div class="mt-1 font-semibold text-slate-900">{{ selected.isAnonymous ? 'Anonim' : selected.name || '—' }}</div>
            <div v-if="!selected.isAnonymous" class="mt-1 text-xs text-slate-500">{{ selected.contact || '—' }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Kronologi</div>
            <div class="mt-2 whitespace-pre-wrap text-sm text-slate-700">{{ selected.chronology }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Bukti</div>
            <div v-if="!selected.evidence?.length" class="mt-2 text-sm text-slate-600">Tidak ada.</div>
            <div v-else class="mt-3 grid gap-2">
              <a
                v-for="f in selected.evidence"
                :key="f.name"
                class="rounded-xl bg-white px-3 py-2 text-sm font-semibold text-emerald-700 ring-1 ring-slate-200/70 transition hover:bg-slate-100"
                :href="f.dataUrl"
                target="_blank"
                rel="noreferrer"
              >
                {{ f.name }}
              </a>
            </div>
          </div>

          <div class="rounded-xl bg-white p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Update status</div>
            <div class="mt-3 grid gap-2">
              <AppButton variant="soft" @click="store.updateStatus(selected.id, 'ditinjau')">Tandai ditinjau</AppButton>
              <AppButton variant="ghost" @click="store.updateStatus(selected.id, 'diproses')">Tandai diproses</AppButton>
              <AppButton variant="danger" @click="store.updateStatus(selected.id, 'selesai')">Tandai selesai</AppButton>
            </div>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Detail</div>
        <div class="mt-2 text-sm text-slate-600">Klik baris laporan untuk melihat detail dan update status.</div>
      </AppCard>
    </div>
  </div>
</template>
