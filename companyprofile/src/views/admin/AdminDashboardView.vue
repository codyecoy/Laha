<script setup>
import { onMounted, ref } from 'vue'
import { api } from '../../services/mockApi'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import AppButton from '../../components/ui/AppButton.vue'
import ReportsBarChart from '../../components/admin/ReportsBarChart.vue'

const loading = ref(false)
const stats = ref(null)

const idr = new Intl.NumberFormat('id-ID', { style: 'currency', currency: 'IDR', maximumFractionDigits: 0 })
function formatIdr(value) {
  const n = Number(value)
  return Number.isFinite(n) ? idr.format(n) : '—'
}

async function fetchStats() {
  loading.value = true
  try {
    stats.value = await api.getAdminStats()
  } finally {
    loading.value = false
  }
}

onMounted(fetchStats)
</script>

<template>
  <div class="flex flex-wrap items-end justify-between gap-4">
    <div>
      <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Dashboard</div>
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Ringkasan operasional</h1>
    </div>
    <AppButton variant="ghost" :disabled="loading" @click="fetchStats">Refresh</AppButton>
  </div>

  <div class="mt-6 grid gap-4 lg:grid-cols-12">
    <div class="grid gap-4 lg:col-span-7 md:grid-cols-2">
      <AppCard class="p-6">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Total laporan</div>
            <div class="mt-2 text-3xl font-extrabold tracking-tight text-slate-900">
              {{ stats?.reports?.total ?? '—' }}
            </div>
          </div>
          <div class="grid h-11 w-11 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
            <AppIcon name="report" class="h-5 w-5 text-emerald-700" />
          </div>
        </div>
        <div class="mt-4 flex flex-wrap gap-2">
          <AppBadge tone="emerald">Baru: {{ stats?.reports?.byStatus?.baru ?? 0 }}</AppBadge>
          <AppBadge tone="amber">Ditinjau: {{ stats?.reports?.byStatus?.ditinjau ?? 0 }}</AppBadge>
          <AppBadge tone="slate">Diproses: {{ stats?.reports?.byStatus?.diproses ?? 0 }}</AppBadge>
          <AppBadge tone="rose">Selesai: {{ stats?.reports?.byStatus?.selesai ?? 0 }}</AppBadge>
        </div>
      </AppCard>

      <AppCard class="p-6">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Pesan masuk baru</div>
            <div class="mt-2 text-3xl font-extrabold tracking-tight text-slate-900">
              {{ stats?.messagesNew ?? '—' }}
            </div>
          </div>
          <div class="grid h-11 w-11 place-items-center rounded-md bg-amber-50 ring-1 ring-amber-100">
            <AppIcon name="inbox" class="h-5 w-5 text-amber-700" />
          </div>
        </div>
        <div class="mt-4 rounded-md bg-slate-50 p-4 text-sm text-slate-600 ring-1 ring-slate-200/70">
          Gunakan menu Pesan Masuk untuk menandai status menjadi “dibalas”.
        </div>
      </AppCard>

      <AppCard class="p-6 md:col-span-2">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Pengunjung</div>
            <div class="mt-2 grid gap-1 text-sm font-semibold text-slate-700">
              <div>Harian: <span class="font-extrabold text-slate-900">{{ stats?.visitors?.daily ?? '—' }}</span></div>
              <div>Bulanan: <span class="font-extrabold text-slate-900">{{ stats?.visitors?.monthly ?? '—' }}</span></div>
              <div>Tahunan: <span class="font-extrabold text-slate-900">{{ stats?.visitors?.yearly ?? '—' }}</span></div>
            </div>
          </div>
          <div class="grid h-11 w-11 place-items-center rounded-md bg-slate-100 ring-1 ring-slate-200/70">
            <AppIcon name="users" class="h-5 w-5 text-slate-700" />
          </div>
        </div>
        <div class="mt-4 rounded-md bg-slate-50 p-4 text-xs text-slate-600 ring-1 ring-slate-200/70">
          Hit dihitung dari ping harian per perangkat (demo).
        </div>
      </AppCard>

      <AppCard class="p-6 md:col-span-2">
        <div class="flex items-start justify-between gap-4">
          <div>
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Donasi</div>
            <div class="mt-2 grid gap-1 text-sm font-semibold text-slate-700">
              <div>Total: <span class="font-extrabold text-slate-900">{{ formatIdr(stats?.donations?.total) }}</span></div>
              <div>Bulan ini: <span class="font-extrabold text-slate-900">{{ formatIdr(stats?.donations?.monthly) }}</span></div>
            </div>
          </div>
          <div class="grid h-11 w-11 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
            <AppIcon name="donate" class="h-5 w-5 text-emerald-700" />
          </div>
        </div>
        <div class="mt-4 rounded-md bg-slate-50 p-4 text-xs text-slate-600 ring-1 ring-slate-200/70">
          Menampilkan akumulasi donasi yang tercatat di sistem.
        </div>
      </AppCard>

      <div class="md:col-span-2">
        <ReportsBarChart :points="stats?.chart || []" />
      </div>
    </div>

    <div class="grid gap-4 lg:col-span-5">
      <AppCard class="p-6">
        <div class="text-sm font-extrabold text-slate-900">Laporan terbaru</div>
        <div class="mt-4 grid gap-3">
          <div
            v-for="r in stats?.recentReports || []"
            :key="r.id"
            class="rounded-md bg-slate-50 p-4 ring-1 ring-slate-200/70"
          >
            <div class="flex items-start justify-between gap-3">
              <div>
                <div class="text-xs font-semibold text-slate-500">ID: {{ r.id }}</div>
                <div class="mt-1 text-sm font-semibold text-slate-800">{{ r.chronology }}</div>
              </div>
              <AppBadge :tone="r.status === 'baru' ? 'emerald' : r.status === 'ditinjau' ? 'amber' : r.status === 'selesai' ? 'rose' : 'slate'">
                {{ r.status }}
              </AppBadge>
            </div>
            <div class="mt-2 text-xs text-slate-500">
              {{ new Date(r.createdAt).toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' }) }}
            </div>
          </div>
        </div>
      </AppCard>

      <AppCard class="p-6">
        <div class="text-sm font-extrabold text-slate-900">Pesan terbaru</div>
        <div class="mt-4 grid gap-3">
          <div
            v-for="m in stats?.recentMessages || []"
            :key="m.id"
            class="rounded-md bg-slate-50 p-4 ring-1 ring-slate-200/70"
          >
            <div class="flex items-start justify-between gap-3">
              <div>
                <div class="text-xs font-semibold text-slate-500">{{ m.name }} • {{ m.email }}</div>
                <div class="mt-1 text-sm font-semibold text-slate-800">
                  {{ m.subject || 'Tanpa subjek' }}
                </div>
              </div>
              <AppBadge :tone="m.status === 'baru' ? 'amber' : 'emerald'">{{ m.status }}</AppBadge>
            </div>
          </div>
        </div>
      </AppCard>
    </div>
  </div>
</template>
