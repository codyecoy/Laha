<script setup>
import { computed } from 'vue'

const props = defineProps({
  points: { type: Array, default: () => [] },
})

const max = computed(() => Math.max(1, ...props.points.map((p) => Number(p.count) || 0)))
const bars = computed(() =>
  props.points.map((p) => ({
    key: p.key,
    count: Number(p.count) || 0,
    h: Math.round(((Number(p.count) || 0) / max.value) * 64),
  })),
)
</script>

<template>
  <div class="rounded-md bg-white p-6 shadow-soft ring-1 ring-slate-200/70">
    <div class="flex items-center justify-between gap-4">
      <div>
        <div class="text-sm font-extrabold text-slate-900">Tren laporan (6 bulan)</div>
        <div class="mt-1 text-xs text-slate-500">Grafik sederhana berbasis data demo.</div>
      </div>
      <div class="text-xs font-semibold text-slate-500">Max: {{ max }}</div>
    </div>

    <div class="mt-5 flex items-end gap-2">
      <div v-for="b in bars" :key="b.key" class="flex w-full flex-col items-center gap-2">
        <div class="h-20 w-full rounded-md bg-slate-50 ring-1 ring-slate-200/70">
          <div
            class="mx-auto mt-auto flex h-full w-full items-end justify-center"
            :title="`${b.key}: ${b.count}`"
          >
            <div
              class="w-full rounded-md bg-emerald-600/80"
              :style="{ height: `${Math.max(6, b.h)}px` }"
            />
          </div>
        </div>
        <div class="text-[10px] font-semibold text-slate-500">{{ b.key.slice(5) }}</div>
      </div>
    </div>
  </div>
</template>

