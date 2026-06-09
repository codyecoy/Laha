<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from 'vue'

const props = defineProps({
  animationData: { type: Object, required: true },
  loop: { type: Boolean, default: true },
  autoplay: { type: Boolean, default: true },
})

const container = ref(null)
let instance = null
let lottieApi = null

async function mount() {
  if (!container.value) return
  if (!lottieApi) {
    const mod = await import('lottie-web')
    lottieApi = mod?.default || mod
  }
  instance?.destroy?.()
  instance = lottieApi.loadAnimation({
    container: container.value,
    renderer: 'svg',
    loop: props.loop,
    autoplay: props.autoplay,
    animationData: props.animationData,
  })
}

onMounted(mount)

watch(
  () => props.animationData,
  () => mount(),
)

onBeforeUnmount(() => {
  instance?.destroy?.()
  instance = null
})
</script>

<template>
  <div ref="container" class="h-full w-full" />
</template>
