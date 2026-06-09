<script setup>
import { computed, onMounted, ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { api } from '../../services/mockApi'
import { useContentStore } from '../../store/content'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppPagination from '../../components/ui/AppPagination.vue'

const route = useRoute()
const router = useRouter()
const content = useContentStore()

const loading = ref(false)
const items = ref([])
const page = ref(1)
const totalPages = ref(1)

const q = ref(String(route.query.q || ''))
const category = ref(String(route.query.kategori || ''))

const categoryOptions = computed(() => {
  const base = [{ label: 'Semua kategori', value: '' }]
  const cats = content.articleCategories.map((c) => ({ label: c, value: c }))
  return base.concat(cats)
})

async function fetchList() {
  loading.value = true
  try {
    const res = await api.listArticles({
      q: q.value,
      category: category.value,
      page: page.value,
      pageSize: 9,
    })
    items.value = res.data
    totalPages.value = res.totalPages
  } finally {
    loading.value = false
  }
}

function syncQuery() {
  const next = { ...route.query }
  if (q.value) next.q = q.value
  else delete next.q
  if (category.value) next.kategori = category.value
  else delete next.kategori
  router.replace({ query: next })
}

watch(
  () => route.query,
  () => {
    q.value = String(route.query.q || '')
    category.value = String(route.query.kategori || '')
    page.value = 1
    fetchList()
  },
)

watch([q, category], () => {
  page.value = 1
  syncQuery()
})

watch(page, () => fetchList())

onMounted(async () => {
  if (!content.articles.length) await content.hydrate()
  await fetchList()
})
</script>

<template>
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        v-motion
        :initial="{ opacity: 0, y: 14 }"
        :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 420, ease: [0.16, 1, 0.3, 1] },
        }"
      >
        <AppBadge tone="amber">Artikel & Edukasi</AppBadge>
        <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
          Konten praktis untuk orang tua, relawan, dan masyarakat.
        </h1>
        <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
          Cari materi tentang pencegahan kekerasan, hak anak, serta panduan mengambil langkah aman.
        </p>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        class="grid gap-6 rounded-xl bg-white p-5 shadow-md ring-1 ring-slate-200/70 md:grid-cols-12 md:items-end"
        v-motion
        :initial="{ opacity: 0, y: 14 }"
        :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
        }"
      >
        <div class="md:col-span-7">
          <AppInput v-model="q" label="Cari artikel" placeholder="Misal: body safety, perundungan, hak anak" />
        </div>
        <div class="md:col-span-5">
          <AppSelect v-model="category" label="Kategori" :options="categoryOptions" />
        </div>
      </div>

      <div class="mt-8 grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <div
          v-if="loading"
          v-for="n in 6"
          :key="n"
          class="h-64 animate-pulse rounded-xl bg-slate-100 ring-1 ring-slate-200/70"
        />

        <RouterLink
          v-for="(a, idx) in items"
          :key="a.id"
          :to="`/artikel/${a.slug}`"
          class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <div class="overflow-hidden">
            <img
              :src="a.thumbnailUrl || 'https://images.unsplash.com/photo-1529390079861-591de354faf5?auto=format&fit=crop&w=1400&q=80'"
              :alt="a.title"
              class="aspect-[16/9] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
              loading="lazy"
            />
          </div>
          <div class="p-5">
            <div class="flex items-center justify-between gap-3">
              <AppBadge :tone="a.coverTone === 'amber' ? 'amber' : a.coverTone === 'slate' ? 'slate' : 'emerald'">
                {{ a.category }}
              </AppBadge>
              <div class="text-xs font-semibold text-slate-500">{{ a.readingTime }} menit</div>
            </div>
            <div class="mt-3 text-lg font-extrabold tracking-tight text-slate-900 group-hover:text-emerald-800">
              {{ a.title }}
            </div>
            <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ a.excerpt }}</p>
            <div class="mt-4 text-sm font-semibold text-emerald-700">Baca selengkapnya</div>
          </div>
        </RouterLink>
      </div>

      <div
        v-if="!loading && !items.length"
        class="mt-10 rounded-xl bg-white p-6 text-sm text-slate-600 shadow-md ring-1 ring-slate-200/70"
      >
        Tidak ada artikel yang cocok. Coba ubah kata kunci atau kategori.
      </div>

      <div class="mt-8">
        <AppPagination v-model:page="page" :total-pages="totalPages" />
      </div>
    </div>
  </section>
</template>
