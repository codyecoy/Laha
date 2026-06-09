<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useMessagesStore } from '../../store/messages'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppPagination from '../../components/ui/AppPagination.vue'

const store = useMessagesStore()
const selectedId = ref('')

const selected = computed(() => store.list.find((m) => m.id === selectedId.value) || null)

const statusOptions = [
  { label: 'Semua status', value: '' },
  { label: 'baru', value: 'baru' },
  { label: 'dibalas', value: 'dibalas' },
]

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
      <h1 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Pesan Masuk</h1>
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
                <th class="px-4 py-3">Pengirim</th>
                <th class="px-4 py-3">Subjek</th>
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
                v-for="m in store.list"
                :key="m.id"
                class="cursor-pointer hover:bg-slate-50 even:bg-slate-50/40"
                :class="selectedId === m.id ? '!bg-amber-50/70' : ''"
                @click="selectedId = m.id"
              >
                <td class="px-4 py-3">
                  <div class="font-semibold text-slate-900">{{ m.name }}</div>
                  <div class="mt-0.5 text-xs text-slate-500">{{ m.email }}</div>
                </td>
                <td class="px-4 py-3">
                  <div class="text-sm font-semibold text-slate-800">{{ m.subject || 'Tanpa subjek' }}</div>
                </td>
                <td class="px-4 py-3">
                  <AppBadge :tone="m.status === 'baru' ? 'amber' : 'emerald'">{{ m.status }}</AppBadge>
                </td>
                <td class="px-4 py-3 text-xs text-slate-500">
                  {{ new Date(m.createdAt).toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' }) }}
                </td>
                <td class="px-4 py-3 text-right">
                  <AppButton variant="ghost" size="sm" @click.stop="selectedId = m.id">Detail</AppButton>
                </td>
              </tr>
              <tr v-if="!store.loading && !store.list.length">
                <td colspan="5" class="px-4 py-6 text-slate-600">Belum ada pesan.</td>
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
            <div class="text-sm font-extrabold text-slate-900">Detail Pesan</div>
            <div class="mt-1 text-xs text-slate-500">{{ selected.email }}</div>
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
            <div class="text-xs font-semibold text-slate-500">Pengirim</div>
            <div class="mt-1 font-semibold text-slate-900">{{ selected.name }}</div>
            <div class="mt-1 text-xs text-slate-500">{{ selected.email }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Subjek</div>
            <div class="mt-1 font-semibold text-slate-900">{{ selected.subject || 'Tanpa subjek' }}</div>
          </div>

          <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Pesan</div>
            <div class="mt-2 whitespace-pre-wrap text-sm text-slate-700">{{ selected.message }}</div>
          </div>

          <div class="rounded-xl bg-white p-4 ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold text-slate-500">Update status</div>
            <div class="mt-3 grid gap-2">
              <AppButton variant="soft" @click="store.updateStatus(selected.id, 'dibalas')">Tandai dibalas</AppButton>
              <AppButton variant="ghost" @click="store.updateStatus(selected.id, 'baru')">Kembalikan ke baru</AppButton>
            </div>
          </div>
        </div>
      </AppCard>

      <AppCard v-else class="p-5">
        <div class="text-sm font-extrabold text-slate-900">Detail</div>
        <div class="mt-2 text-sm text-slate-600">Klik baris pesan untuk melihat detail dan update status.</div>
      </AppCard>
    </div>
  </div>
</template>
