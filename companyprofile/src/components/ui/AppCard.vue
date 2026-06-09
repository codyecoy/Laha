<script setup>
defineProps({
  interactive: { type: Boolean, default: false },
})
</script>

<template>
  <div
    class="rounded-xl bg-white p-5 shadow-md ring-1 ring-slate-200/70"
    :class="interactive ? 'transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg' : ''"
  >
    <slot />
  </div>
</template>
