<script setup>
import { computed } from 'vue'

const props = defineProps({
  tone: { type: String, default: 'emerald' },
})

const toneClass = computed(() => {
  if (props.tone === 'amber') return 'bg-[#FACC15]/25 text-slate-900 ring-1 ring-[#FACC15]/35'
  if (props.tone === 'slate') return 'bg-slate-100 text-slate-800 ring-1 ring-slate-200'
  if (props.tone === 'rose') return 'bg-rose-100 text-rose-900 ring-1 ring-rose-200'
  return 'bg-[#16A34A]/12 text-[#16A34A] ring-1 ring-[#16A34A]/25'
})
</script>

<template>
  <span class="inline-flex items-center gap-1 rounded-full px-2.5 py-1 text-xs font-semibold" :class="toneClass">
    <slot />
  </span>
</template>
