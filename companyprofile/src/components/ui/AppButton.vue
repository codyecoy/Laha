<script setup>
import { computed } from 'vue'

const props = defineProps({
  variant: { type: String, default: 'primary' },
  size: { type: String, default: 'md' },
  to: { type: [String, Object], default: null },
  href: { type: String, default: null },
  loading: { type: Boolean, default: false },
  disabled: { type: Boolean, default: false },
})

const tag = computed(() => {
  if (props.to) return 'RouterLink'
  if (props.href) return 'a'
  return 'button'
})

const sizeClass = computed(() => {
  if (props.size === 'sm') return 'h-9 px-3 text-sm'
  if (props.size === 'lg') return 'h-12 px-5 text-base'
  return 'h-11 px-4 text-sm sm:text-base'
})

const variantClass = computed(() => {
  if (props.variant === 'accent')
    return 'bg-[#FDCF08] text-slate-900 hover:bg-[#F2C500] focus-visible:ring-[#FDCF08]/45'
  if (props.variant === 'soft')
    return 'bg-[#099044]/10 text-[#077A39] ring-1 ring-[#099044]/25 hover:bg-[#099044]/15'
  if (props.variant === 'ghost')
    return 'bg-transparent text-slate-700 hover:bg-slate-100 ring-1 ring-slate-200'
  if (props.variant === 'danger')
    return 'bg-rose-600 text-white hover:bg-rose-700 focus-visible:ring-rose-500/35'
  return 'bg-[#099044] text-white hover:bg-[#077A39] focus-visible:ring-[#099044]/35'
})

const common =
  'inline-flex items-center justify-center gap-2 rounded-xl font-semibold shadow-md transition duration-300 ease-in-out hover:-translate-y-0.5 hover:shadow-lg active:translate-y-0 focus:outline-none focus-visible:ring-2 focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-60 disabled:hover:translate-y-0 disabled:hover:shadow-md'
</script>

<template>
  <component
    :is="tag"
    :to="to || undefined"
    :href="href || undefined"
    :target="href ? '_blank' : undefined"
    :rel="href ? 'noreferrer' : undefined"
    class="select-none"
    :class="[common, sizeClass, variantClass]"
    :disabled="tag === 'button' ? disabled || loading : undefined"
    :aria-busy="loading ? 'true' : 'false'"
  >
    <span v-if="loading" class="h-4 w-4 animate-spin rounded-full border-2 border-white/50 border-t-white" />
    <slot />
  </component>
</template>
