<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '../../services/mockApi'
import { applySeo } from '../../router/seo'
import { useContentStore } from '../../store/content'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppButton from '../../components/ui/AppButton.vue'

const route = useRoute()
const contentStore = useContentStore()

const loading = ref(false)
const article = ref(null)

const related = computed(() => {
  if (!article.value) return []
  return contentStore.articles.filter((a) => a.slug !== article.value.slug).slice(0, 3)
})

async function fetchDetail() {
  loading.value = true
  try {
    const found = await api.getArticleBySlug(route.params.slug)
    article.value = found
    if (found) {
      applySeo({
        meta: {
          title: `${found.title} • LAHA`,
          description: found.excerpt || 'Artikel edukasi perlindungan anak.',
        },
      })
    }
  } finally {
    loading.value = false
  }
}

watch(
  () => route.params.slug,
  () => fetchDetail(),
)

onMounted(async () => {
  if (!contentStore.articles.length) await contentStore.hydrate()
  await fetchDetail()
})
</script>

<template>
  <section class="py-10 sm:py-14">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        class="flex flex-wrap items-center justify-between gap-3"
        v-motion
        :initial="{ opacity: 0, y: 10 }"
        :enter="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 320, ease: [0.16, 1, 0.3, 1] },
        }"
      >
        <AppButton variant="ghost" size="sm" to="/artikel">Kembali</AppButton>
        <div v-if="article" class="text-xs font-semibold text-slate-500">{{ article.readingTime }} menit</div>
      </div>
    </div>
  </section>

  <section class="pb-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div v-if="loading" class="rounded-xl bg-white p-6 shadow-md ring-1 ring-slate-200/70">
        <div class="h-6 w-2/3 animate-pulse rounded bg-slate-100" />
        <div class="mt-4 h-4 w-1/2 animate-pulse rounded bg-slate-100" />
        <div class="mt-8 space-y-3">
          <div class="h-4 w-full animate-pulse rounded bg-slate-100" />
          <div class="h-4 w-11/12 animate-pulse rounded bg-slate-100" />
          <div class="h-4 w-10/12 animate-pulse rounded bg-slate-100" />
        </div>
      </div>

      <div v-else-if="!article" class="rounded-xl bg-white p-6 text-sm text-slate-600 shadow-md ring-1 ring-slate-200/70">
        Artikel tidak ditemukan.
      </div>

      <div v-else class="grid gap-6 lg:grid-cols-12">
        <article
          class="overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 lg:col-span-8"
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <div class="overflow-hidden">
            <img
              :src="article.thumbnailUrl || 'https://images.unsplash.com/photo-1529390079861-591de354faf5?auto=format&fit=crop&w=1600&q=80'"
              :alt="article.title"
              class="aspect-[16/9] w-full object-cover"
              loading="lazy"
            />
          </div>

          <div class="p-6 sm:p-8">
            <AppBadge :tone="article.coverTone === 'amber' ? 'amber' : article.coverTone === 'slate' ? 'slate' : 'emerald'">
              {{ article.category }}
            </AppBadge>
            <h1 class="mt-4 text-balance text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
              {{ article.title }}
            </h1>
            <div class="mt-3 text-sm text-slate-500">
              Dipublikasikan: {{ new Date(article.publishedAt).toLocaleDateString('id-ID', { year: 'numeric', month: 'long', day: 'numeric' }) }}
            </div>

            <div class="prose prose-slate mt-8 max-w-none prose-h2:font-extrabold prose-h2:tracking-tight prose-a:text-emerald-700">
              <div v-html="article.content" />
            </div>
          </div>
        </article>

        <aside class="lg:col-span-4">
          <div
            class="rounded-xl bg-white p-6 shadow-md ring-1 ring-slate-200/70"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: 120, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-sm font-extrabold text-slate-900">Butuh bantuan?</div>
            <div class="mt-2 text-sm leading-relaxed text-slate-600">
              Jika Anda menghadapi situasi yang mengkhawatirkan, gunakan fitur pelaporan untuk pendampingan awal.
            </div>
            <div class="mt-4 grid gap-2">
              <AppButton variant="primary" to="/lapor">Laporkan Kasus</AppButton>
              <AppButton variant="soft" to="/kontak">Konsultasi</AppButton>
            </div>
            <div class="mt-4 rounded-xl bg-amber-50 p-4 text-xs text-slate-700 ring-1 ring-amber-100">
              Hotline (08.00–20.00): <span class="font-extrabold">(022) 720 7023</span>
            </div>
          </div>

          <div
            v-if="related.length"
            class="mt-6 rounded-xl bg-white p-6 shadow-md ring-1 ring-slate-200/70"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: 200, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-sm font-extrabold text-slate-900">Artikel lain</div>
            <div class="mt-4 grid gap-3">
              <RouterLink
                v-for="(a, idx) in related"
                :key="a.id"
                :to="`/artikel/${a.slug}`"
                class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-0.5 hover:bg-white hover:shadow-md"
                v-motion
                :initial="{ opacity: 0, y: 8 }"
                :visible-once="{
                  opacity: 1,
                  y: 0,
                  transition: { type: 'tween', duration: 300, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
                }"
              >
                <div class="text-xs font-semibold text-slate-500">{{ a.category }}</div>
                <div class="mt-1 text-sm font-extrabold text-slate-900">{{ a.title }}</div>
                <div class="mt-1 text-xs text-slate-500">{{ a.readingTime }} menit</div>
              </RouterLink>
            </div>
          </div>
        </aside>
      </div>
    </div>
  </section>
</template>
