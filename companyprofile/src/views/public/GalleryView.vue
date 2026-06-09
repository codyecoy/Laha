<script setup>
import { computed, onMounted } from 'vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppButton from '../../components/ui/AppButton.vue'
import { useContentStore } from '../../store/content'

const content = useContentStore()

const photos = computed(() => {
  return (content.gallery || [])
    .map((p) => ({
      src: p.imageUrl || p.src,
      alt: p.alt || p.title || 'Foto kegiatan',
    }))
    .filter((p) => Boolean(p.src))
})

const heroImage = computed(() => photos.value[0]?.src || 'https://images.unsplash.com/photo-1523240795612-9a054b0db644?auto=format&fit=crop&w=1600&q=80')

onMounted(async () => {
  if (!content.articles.length && !content.services.length && !content.team.length && !content.gallery.length) {
    await content.hydrate()
  }
})
</script>

<template>
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid items-center gap-8 lg:grid-cols-12 lg:gap-10">
        <div
          class="lg:col-span-7"
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 420, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <AppBadge tone="amber">Galeri</AppBadge>
          <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
            Dokumentasi kegiatan advokasi & edukasi.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Visual yang humanis membantu membangun kepercayaan publik—tanpa narasi sensasional. Semua foto di halaman ini
            bersifat contoh.
          </p>
          <div class="mt-7 flex flex-col gap-3 sm:flex-row">
            <AppButton variant="primary" to="/lapor">Laporkan Kasus</AppButton>
            <AppButton variant="ghost" to="/kontak">Kolaborasi & Relawan</AppButton>
          </div>
        </div>
        <div class="lg:col-span-5">
          <div
            class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 420, delay: 120, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <img
              :src="heroImage"
              alt="Kegiatan edukasi dan pelatihan"
              class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
              loading="lazy"
            />
          </div>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <div
          v-for="(p, idx) in photos"
          :key="p.src"
          class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <img :src="p.src" :alt="p.alt" class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105" loading="lazy" />
          <div class="p-4">
            <div class="text-sm font-semibold text-slate-700">{{ p.alt }}</div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
