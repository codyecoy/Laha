<script setup>
import { computed } from 'vue'

const props = defineProps({
  tone: { type: String, default: 'emerald' },
})

const toneClass = computed(() => {
  if (props.tone === 'amber') return 'bg-[#FDCF08]/25 text-slate-900 ring-1 ring-[#FDCF08]/35'
  if (props.tone === 'slate') return 'bg-slate-100 text-slate-800 ring-1 ring-slate-200'
  if (props.tone === 'rose') return 'bg-rose-100 text-rose-900 ring-1 ring-rose-200'
  return 'bg-[#099044]/12 text-[#077A39] ring-1 ring-[#099044]/25'
})
</script>

<template>
  <span class="inline-flex items-center gap-1 rounded-full px-2.5 py-1 text-xs font-semibold" :class="toneClass">
    <slot />
  </span>
</template>
