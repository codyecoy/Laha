<script setup>
import { computed } from 'vue'
import { useContentStore } from '../../store/content'
import AppButton from '../../components/ui/AppButton.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import HeroIllustration from '../../components/public/HeroIllustration.vue'
import AppLottie from '../../components/ui/AppLottie.vue'
import heroLottie from '../../assets/lottie/home-hero.json'

import gambar1 from '../../assets/images/image1.JPG'
import gambar2 from '../../assets/images/image2.JPG'
import gambar3 from '../../assets/images/image3.JPG'
import gambar4 from '../../assets/images/image4.JPG'
import gambar5 from '../../assets/images/image5.JPG'
import gambar6 from '../../assets/images/image6.JPG'
import gambar7 from '../../assets/images/image7.JPG'
import gambar8 from '../../assets/images/image8.JPG'

const content = useContentStore()

const services = computed(() => content.services.slice(0, 4))
const latestArticles = computed(() => [...content.articles].slice(0, 3))
const stats = [
  { label: 'Kasus didampingi', value: '1.240+' },
  { label: 'Sesi edukasi komunitas', value: '320+' },
  { label: 'Relawan terlibat', value: '180+' },
  { label: 'Kota/daerah cakupan', value: '24' },
]

const testimonials = [
  {
    quote:
      'Responnya cepat dan empatik. Kami dibantu memahami langkah hukum yang aman tanpa membuat anak tertekan.',
    name: 'Orang tua korban',
    role: 'Pendamping keluarga',
    tone: 'emerald',
  },
  {
    quote: 'Materi edukasinya jelas dan praktis. Komunitas kami jadi lebih peka dan siap bertindak.',
    name: 'Koordinator RT/RW',
    role: 'Mitra komunitas',
    tone: 'amber',
  },
  {
    quote: 'Admin pelaporan mudah digunakan. Privasi terjaga, dan status pendampingan transparan.',
    name: 'Relawan',
    role: 'Support hotline',
    tone: 'slate',
  },
]

const gallery = [
  {
    src: gambar3,
    alt: 'Kegiatan edukasi bersama orang tua',
  },
  {
    src: gambar4,
    alt: 'Anak dan keluarga (humanis)',
  },
  {
    src: gambar5,
    alt: 'Sesi pendampingan komunitas',
  },
  {
    src: gambar6,
    alt: 'Kegiatan edukasi di ruang kelas',
  },
  {
    src: gambar7,
    alt: 'Tim relawan berdiskusi',
  },
  {
    src: gambar8,
    alt: 'Sesi kerja tim dan koordinasi',
  },
]
</script>

<template>
  <section class="relative overflow-hidden pt-10 pb-16 sm:pt-12 sm:pb-20">
    <div class="pointer-events-none absolute inset-0">
      <div class="absolute -top-24 left-1/2 h-72 w-72 -translate-x-1/2 rounded-full bg-[#285858]/35 blur-3xl" />
      <div class="absolute -top-20 right-[-4rem] h-80 w-80 rounded-full bg-[#77B209]/30 blur-3xl" />
      <div class="absolute -top-6 left-[-5rem] h-72 w-72 rounded-full bg-[#FDCF08]/25 blur-3xl" />
      <div class="absolute inset-0 bg-[radial-gradient(circle_at_1px_1px,rgba(40,88,88,0.07)_1px,transparent_0)] [background-size:22px_22px]" />
    </div>

    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid items-center gap-7 lg:grid-cols-12 lg:gap-10">
        <div class="lg:col-span-6" v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 420, ease: [0.16, 1, 0.3, 1] },
        }">
          <AppBadge tone="amber">Aman • Terpercaya • Peduli</AppBadge>
          <h1 class="mt-3 text-balance text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Perlindungan anak dimulai dari langkah yang tepat.
          </h1>
          <div class="mt-3 h-1 w-20 rounded-full bg-gradient-to-r from-[#285858] via-[#099044] to-[#FDCF08]" />
          <p class="mt-3 max-w-xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            LAHA mendampingi pelaporan, pendampingan hukum, konseling, edukasi, dan kampanye sosial — dengan
            pendekatan yang humanis dan profesional.
          </p>

          <div class="mt-6 flex flex-col gap-3 sm:flex-row sm:items-center">
            <AppButton variant="primary" to="/lapor">
              <AppIcon name="report" class="h-4.5 w-4.5" />
              Laporkan Kasus
            </AppButton>
            <AppButton variant="soft" to="/kontak">
              <AppIcon name="users" class="h-4.5 w-4.5" />
              Jadi Relawan
            </AppButton>
            <AppButton variant="accent" to="/donasi">
              <AppIcon name="donate" class="h-4.5 w-4.5" />
              Donasi
            </AppButton>
          </div>

          <div
            class="mt-6 grid grid-cols-2 gap-4 rounded-xl bg-white p-4 shadow-md ring-1 ring-slate-200/70 sm:grid-cols-4">
            <div v-for="s in stats" :key="s.label"
              class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-0.5 hover:bg-white hover:shadow-md">
              <div class="text-lg font-extrabold tracking-tight text-slate-900">{{ s.value }}</div>
              <div class="mt-1 text-xs leading-snug text-slate-600">{{ s.label }}</div>
            </div>
          </div>
        </div>

        <div class="lg:col-span-6">
          <div class="grid gap-4">
            <div class="relative overflow-hidden rounded-xl bg-white p-5 shadow-md ring-1 ring-slate-200/70 sm:p-6"
              v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 420, delay: 80, ease: [0.16, 1, 0.3, 1] },
              }">
              <div class="absolute inset-0 bg-gradient-to-tr from-[#285858]/18 via-white to-[#FDCF08]/20" />
              <div class="absolute -left-10 -top-10 h-40 w-40 rounded-full bg-[#285858]/10 blur-2xl" />
              <div class="pointer-events-none absolute inset-0 opacity-80">
                <AppLottie :animation-data="heroLottie" />
              </div>
              <div class="relative aspect-[72/52] w-full">
                <HeroIllustration />
              </div>
            </div>

            <div class="grid gap-4 sm:grid-cols-2">
              <div
                class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
                v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
                  opacity: 1,
                  y: 0,
                  transition: { type: 'tween', duration: 420, delay: 140, ease: [0.16, 1, 0.3, 1] },
                }">
                <img src="../../assets/images/image1.JPG"
                  alt="Anak dan keluarga"
                  class="h-44 w-full object-cover transition duration-300 ease-in-out group-hover:scale-105 sm:h-48"
                  loading="lazy" />
              </div>
              <div
                class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
                v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
                  opacity: 1,
                  y: 0,
                  transition: { type: 'tween', duration: 420, delay: 200, ease: [0.16, 1, 0.3, 1] },
                }">
                <img src="../../assets/images/image2.JPG"
                  alt="Tim relawan berdiskusi"
                  class="h-44 w-full object-cover transition duration-300 ease-in-out group-hover:scale-105 sm:h-48"
                  loading="lazy" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex items-end justify-between gap-6" v-motion :initial="{ opacity: 0, y: 10 }" :visible-once="{
        opacity: 1,
        y: 0,
        transition: { type: 'tween', duration: 360, ease: [0.16, 1, 0.3, 1] },
      }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Layanan inti</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Apa yang kami lakukan</h2>
        </div>
        <RouterLink to="/layanan" class="hidden text-sm font-semibold text-[#099044] hover:text-[#077A39] sm:block">
          Lihat semua layanan
        </RouterLink>
      </div>

      <div class="mt-8 grid gap-6 md:grid-cols-2 lg:grid-cols-4">
        <AppCard v-for="(s, idx) in services" :key="s.id" class="p-6" interactive v-motion
          :initial="{ opacity: 0, y: 14 }" :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 60, ease: [0.16, 1, 0.3, 1] },
          }">
          <div class="flex items-start gap-4">
            <div class="grid h-12 w-12 place-items-center rounded-xl bg-[#099044]/10 ring-1 ring-[#099044]/20">
              <AppIcon :name="s.icon" class="h-5 w-5 text-[#099044]" />
            </div>
            <div>
              <div class="text-base font-extrabold tracking-tight text-slate-900">{{ s.title }}</div>
              <p class="mt-1 text-sm leading-relaxed text-slate-600">{{ s.description }}</p>
            </div>
          </div>
          <ul class="mt-5 grid gap-2 text-sm text-slate-600">
            <li v-for="h in s.highlights" :key="h" class="flex items-center gap-2">
              <span class="h-1.5 w-1.5 rounded-full bg-amber-400" />
              <span>{{ h }}</span>
            </li>
          </ul>
        </AppCard>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex items-end justify-between gap-6" v-motion :initial="{ opacity: 0, y: 10 }" :visible-once="{
        opacity: 1,
        y: 0,
        transition: { type: 'tween', duration: 360, ease: [0.16, 1, 0.3, 1] },
      }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Kegiatan</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Galeri kegiatan advokasi & edukasi</h2>
        </div>
        <RouterLink to="/galeri" class="hidden text-sm font-semibold text-emerald-700 hover:text-emerald-800 sm:block">
          Buka galeri
        </RouterLink>
      </div>

      <div class="mt-8 grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <div v-for="(g, idx) in gallery" :key="g.src"
          class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
          }">
          <img :src="g.src" :alt="g.alt"
            class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
            loading="lazy" />
          <div class="p-4">
            <div class="text-sm font-semibold text-slate-700">{{ g.alt }}</div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-6 lg:grid-cols-12">
        <div class="lg:col-span-4">
          <div class="rounded-xl bg-white p-6 shadow-md ring-1 ring-slate-200/70" v-motion
            :initial="{ opacity: 0, y: 14 }" :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
            }">
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Kepercayaan publik</div>
            <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Suara dari mereka</h2>
            <p class="mt-3 text-sm leading-relaxed text-slate-600">
              Kami menjaga privasi. Testimoni berikut adalah ringkasan pengalaman (demo) yang mewakili pola layanan
              kami.
            </p>
          </div>
        </div>
        <div class="grid gap-6 lg:col-span-8 md:grid-cols-3">
          <AppCard v-for="(t, idx) in testimonials" :key="t.name + t.role" class="p-6" interactive v-motion
            :initial="{ opacity: 0, y: 14 }" :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: idx * 65, ease: [0.16, 1, 0.3, 1] },
            }">
            <AppBadge :tone="t.tone">{{ t.role }}</AppBadge>
            <p class="mt-3 text-sm leading-relaxed text-slate-700">“{{ t.quote }}”</p>
            <div class="mt-4 text-sm font-semibold text-slate-900">{{ t.name }}</div>
          </AppCard>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex items-end justify-between gap-6" v-motion :initial="{ opacity: 0, y: 10 }" :visible-once="{
        opacity: 1,
        y: 0,
        transition: { type: 'tween', duration: 360, ease: [0.16, 1, 0.3, 1] },
      }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Edukasi</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Artikel terbaru</h2>
        </div>
        <RouterLink to="/artikel" class="hidden text-sm font-semibold text-[#099044] hover:text-[#077A39] sm:block">
          Buka semua artikel
        </RouterLink>
      </div>

      <div class="mt-8 grid gap-6 lg:grid-cols-3">
        <RouterLink v-for="(a, idx) in latestArticles" :key="a.id" :to="`/artikel/${a.slug}`"
          class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 70, ease: [0.16, 1, 0.3, 1] },
          }">
          <div class="overflow-hidden">
            <img
              :src="a.thumbnailUrl || 'https://images.unsplash.com/photo-1529390079861-591de354faf5?auto=format&fit=crop&w=1400&q=80'"
              :alt="a.title"
              class="aspect-[16/9] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
              loading="lazy" />
          </div>
          <div class="p-5">
            <div class="flex items-center justify-between gap-3">
              <AppBadge :tone="a.coverTone === 'amber' ? 'amber' : a.coverTone === 'slate' ? 'slate' : 'emerald'">
                {{ a.category }}
              </AppBadge>
              <div class="text-xs font-semibold text-slate-500">{{ a.readingTime }} menit</div>
            </div>
            <div class="mt-3 text-lg font-extrabold tracking-tight text-slate-900 group-hover:text-[#077A39]">
              {{ a.title }}
            </div>
            <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ a.excerpt }}</p>
            <div class="mt-4 text-sm font-semibold text-[#099044]">Baca selengkapnya</div>
          </div>
        </RouterLink>
      </div>

      <div
        class="mt-10 rounded-2xl bg-gradient-to-r from-[#099044] via-[#099044] to-[#077A39] p-6 text-white shadow-md sm:p-8"
        v-motion :initial="{ opacity: 0, y: 14 }" :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
        }">
        <div class="grid gap-6 lg:grid-cols-12 lg:items-center">
          <div class="lg:col-span-8">
            <div class="text-sm font-semibold text-white/80">Butuh pendampingan segera?</div>
            <div class="mt-2 text-2xl font-extrabold tracking-tight">Laporkan kasus atau konsultasi awal via hotline.
            </div>
            <div class="mt-2 text-sm leading-relaxed text-white/85">
              Anda bisa anonim. Tim kami memprioritaskan keselamatan dan kerahasiaan.
            </div>
          </div>
          <div class="flex flex-col gap-3 lg:col-span-4 lg:items-end">
            <AppButton variant="accent" to="/lapor">Laporkan Sekarang</AppButton>
            <div class="text-sm font-semibold text-white/90">Hotline: (022) 720 7023</div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
