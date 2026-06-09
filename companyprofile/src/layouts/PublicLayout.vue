<script setup>
import { onMounted } from 'vue'
import PublicNavbar from '../components/public/PublicNavbar.vue'
import PublicFooter from '../components/public/PublicFooter.vue'
import { useContentStore } from '../store/content'

const content = useContentStore()

onMounted(async () => {
  if (!content.articles.length && !content.services.length) await content.hydrate()
})
</script>

<template>
  <div class="min-h-svh bg-white">
    <PublicNavbar />
    <main class="bg-gradient-to-b from-white via-white to-slate-50">
      <RouterView />
    </main>
    <PublicFooter />
  </div>
</template>

