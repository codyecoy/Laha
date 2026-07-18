<script setup>
import { computed, ref, onMounted } from 'vue'
import { useContentStore } from '../../store/content'
import AppButton from '../../components/ui/AppButton.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppIcon from '../../components/ui/AppIcon.vue'

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

import logokemnekum from '../../assets/images/kemenkum.png'
import logosavethechildern from '../../assets/images/savethechildern.png'
import logounpar from '../../assets/images/unpar.jpg'
import logostia from '../../assets/images/stia.jpg'
import logokmperlin from '../../assets/images/kmperlin.png'

const content = useContentStore()

const services = computed(() => content.services.slice(0, 4))
const latestArticles = computed(() => [...content.articles].slice(0, 3))

// Animated stats
const stats = [
  { label: 'Penerima Manfaat', value: 1000, suffix: '+' },
  { label: 'Penyuluhan Hukum', value: 300, suffix: '+' },
  { label: 'Kasus Pendampingan', value: 200, suffix: '+' },
  { label: 'Desa Binaan', value: 50, suffix: '+' },
]

const animatedValues = ref(stats.map(() => 0))

const animateStats = () => {
  stats.forEach((stat, index) => {
    let start = 0
    const duration = 2000
    const increment = stat.value / (duration / 16)
    
    const timer = setInterval(() => {
      start += increment
      if (start >= stat.value) {
        animatedValues.value[index] = stat.value
        clearInterval(timer)
      } else {
        animatedValues.value[index] = Math.floor(start)
      }
    }, 16)
  })
}

const testimonies = [
  {
    quote: 'Responnya cepat dan empatik. Kami dibantu memahami langkah hukum yang aman tanpa membuat anak tertekan.',
    name: 'Orang tua korban',
    role: 'Penerima manfaat',
  },
  {
    quote: 'Materi edukasinya jelas dan praktis. Komunitas kami jadi lebih peka dan siap bertindak.',
    name: 'Ketua RT',
    role: 'Mitra komunitas',
  },
  {
    quote: 'Admin pelaporan mudah digunakan. Privasi terjaga, dan status pendampingan transparan.',
    name: 'Relawan',
    role: 'Tim pendamping',
  },
]

const galleryItems = [
  { src: gambar3, alt: 'Kegiatan edukasi bersama orang tua' },
  { src: gambar4, alt: 'Anak dan keluarga' },
  { src: gambar5, alt: 'Sesi pendampingan komunitas' },
  { src: gambar6, alt: 'Kegiatan edukasi di ruang kelas' },
  { src: gambar7, alt: 'Tim relawan berdiskusi' },
  { src: gambar8, alt: 'Sesi kerja tim dan koordinasi' },
]

const partners = {
  pemerintah: [
    { name: 'Biro Hukum Provinsi Jawa Barat', logo: gambar5 },
    { name: 'DP2KBP3A Kabupaten Bandung', logo: null },
    { name: 'Dinas Pendidikan Kabupaten Bandung', logo: null },
    { name: 'Desa Cibiru Wetan Kabupaten Bandung', logo: null },
    { name: 'Desa Kertamulya Kabupaten Bandung Barat', logo: null },
    { name: 'Kelurahan Mekarjaya Kota Bandung', logo: null },
  ],
  kementerian: [
    { name: 'Kementerian Hukum Republik Indonesia', logo: logokemnekum },
    { name: 'Kementerian Pemberdayaan Perempuan dan Perlindungan Anak Republik Indonesia', logo: logokmperlin },
    { name: 'Lembaga Pembinaan Khusus Anak (LPKA) Kelas II A Bandung', logo: null },
  ],
  lembaga: [
    { name: 'Rumah Zakat', logo: null },
    { name: 'Save the Children', logo: logosavethechildern },
    { name: 'Lembaga Bantuan Hukum Pengayoman Universitas Katolik Parahyangan Bandung', logo: null },
  ],
  perguruanTinggi: [
    { name: 'Universitas Katolik Parahyangan (UNPAR) Bandung', logo: logounpar },
    { name: 'Sekolah Tinggi Ilmu Administrasi Negara (STIA LAN) Bandung', logo: logostia },
  ],
}

onMounted(() => {
  animateStats()
})
</script>

<template>
  <!-- Hero Section -->
  <section class="relative overflow-hidden pt-16 pb-24 sm:pt-20 sm:pb-28">
    <div class="pointer-events-none absolute inset-0">
        <div class="absolute -top-24 left-1/2 h-80 w-80 -translate-x-1/2 rounded-full bg-brand-primary/20 blur-3xl"></div>
        <div class="absolute -top-20 right-[-4rem] h-96 w-96 rounded-full bg-brand-primary/20 blur-3xl"></div>
        <div class="absolute -top-6 left-[-5rem] h-80 w-80 rounded-full bg-brand-accent/15 blur-3xl"></div>
        <div class="absolute inset-0 bg-[radial-gradient(circle_at_1px_1px,rgba(22,163,74,0.05) 1px,transparent_0)] [background-size:22px_22px]"></div>
      </div>

    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8 relative z-10">
      <div class="grid items-center gap-8 lg:grid-cols-12 lg:gap-12">
        <div class="lg:col-span-6" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
          <AppBadge tone="emerald" class="mb-6">Lembaga Advokasi Hak Anak</AppBadge>
          <h1 class="text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl lg:text-5xl">
            Mewujudkan Perlindungan Anak dan Akses Keadilan untuk Semua
          </h1>
          <div class="mt-4 h-1.5 w-24 rounded-full bg-gradient-to-r from-brand-primary via-brand-primary to-brand-accent"></div>
          <p class="mt-4 max-w-xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Lembaga Advokasi Hak Anak (LAHA) adalah organisasi non-profit yang bergerak dalam bidang kajian kebijakan pembangunan anak, serta kebijakan pembangunan yang memiliki dampak terhadap anak dan mengembangkan program advokasi terhadap hak-hak anak, khususnya anak yang membutuhkan perlindungan khusus.
          </p>

          <div class="mt-8 flex flex-col gap-3 sm:flex-row sm:items-center">
            <AppButton variant="primary" size="lg" to="/layanan">
              <AppIcon name="users" class="h-5 w-5 mr-2"></AppIcon>
              Program Kami
            </AppButton>
            <AppButton variant="soft" size="lg" to="/kontak">
              <AppIcon name="phone" class="h-5 w-5 mr-2"></AppIcon>
              Hubungi Kami
            </AppButton>
          </div>

          <!-- Stats -->
          <div class="mt-10 grid grid-cols-2 gap-4 rounded-2xl bg-white/70 backdrop-blur-sm p-6 shadow-md ring-1 ring-slate-200/70">
            <div v-for="(stat, index) in stats" :key="index" class="text-center">
              <div class="text-3xl font-extrabold tracking-tight text-brand-primary">
                {{ animatedValues[index] }}{{ stat.suffix }}
              </div>
              <div class="mt-2 text-sm font-medium text-slate-600">{{ stat.label }}</div>
            </div>
          </div>
        </div>

        <div class="lg:col-span-6" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, delay: 150, ease: [0.25, 1, 0.5, 1] } }">
          <div class="grid gap-4">
            <div class="relative overflow-hidden rounded-2xl bg-white p-6 shadow-lg ring-1 ring-slate-200/70 sm:p-8">
              <div class="absolute inset-0 bg-gradient-to-tr from-brand-primary/15 via-white to-brand-accent/10"></div>
              <div class="absolute -left-10 -top-10 h-40 w-40 rounded-full bg-brand-primary/10 blur-2xl"></div>
              <div class="pointer-events-none absolute inset-0 opacity-80">
                <AppLottie :animation-data="heroLottie"></AppLottie>
              </div>
              <div class="relative aspect-[4/3] w-full overflow-hidden rounded-xl">
                <img src="https://images.unsplash.com/photo-1503676260728-1c00da094a0b?q=80&w=1470&auto=format&fit=crop" alt="Kegiatan advokasi anak" class="h-full w-full object-cover transition duration-700 ease-out group-hover:scale-105" loading="lazy">
              </div>
            </div>

            <div class="grid gap-4 sm:grid-cols-2">
              <div class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
                <img :src="gambar1" alt="Anak dan keluarga" class="h-44 w-full object-cover transition duration-400 ease-in-out group-hover:scale-105 sm:h-48" loading="lazy">
              </div>
              <div class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
                <img :src="gambar2" alt="Tim relawan berdiskusi" class="h-44 w-full object-cover transition duration-400 ease-in-out group-hover:scale-105 sm:h-48" loading="lazy">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Services Section -->
  <section class="py-20 bg-slate-50 sm:py-24">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Program Kami</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Apa yang Kami Lakukan</h2>
        </div>
        <RouterLink to="/layanan" class="text-sm font-semibold text-brand-primary hover:text-brand-primary flex items-center gap-2">
          Lihat semua program
          <svg viewBox="0 0 20 20" fill="none" class="h-4 w-4" stroke="currentColor" stroke-width="2">
            <path d="M6 15L15 6M9 6h6v6" stroke-linecap="round" stroke-linejoin="round"></path>
          </svg>
        </RouterLink>
      </div>

      <div class="mt-12 grid gap-7 md:grid-cols-2 lg:grid-cols-4">
        <AppCard v-for="(s, idx) in services" :key="s.id" class="p-7" interactive v-motion :initial="{ opacity: 0, y: 15 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, delay: idx * 100, ease: [0.25, 1, 0.5, 1] } }">
          <div class="flex items-start gap-4">
            <div class="grid h-12 w-12 place-items-center rounded-xl bg-brand-primary/10 ring-1 ring-brand-primary/25">
              <AppIcon :name="s.icon" class="h-6 w-6 text-brand-primary"></AppIcon>
            </div>
            <div class="flex-1">
              <div class="text-base font-extrabold tracking-tight text-slate-900">{{ s.title }}</div>
              <p class="mt-1 text-sm leading-relaxed text-slate-600">{{ s.description }}</p>
            </div>
          </div>
          <ul class="mt-5 grid gap-2 text-sm text-slate-600">
            <li v-for="h in s.highlights" :key="h" class="flex items-center gap-2">
              <span class="h-1.5 w-1.5 rounded-full bg-brand-accent"></span>
              <span>{{ h }}</span>
            </li>
          </ul>
        </AppCard>
      </div>
    </div>
  </section>

  <!-- Gallery Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Berita & Kegiatan</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Galeri Kegiatan</h2>
        </div>
        <RouterLink to="/galeri" class="text-sm font-semibold text-brand-primary hover:text-brand-primary flex items-center gap-2">
          Buka galeri
          <svg viewBox="0 0 20 20" fill="none" class="h-4 w-4" stroke="currentColor" stroke-width="2">
            <path d="M6 15L15 6M9 6h6v6" stroke-linecap="round" stroke-linejoin="round"></path>
          </svg>
        </RouterLink>
      </div>

      <div class="mt-10 grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        <div v-for="(item, idx) in galleryItems" :key="item.src" class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, delay: idx * 90, ease: [0.25, 1, 0.5, 1] } }">
          <img :src="item.src" :alt="item.alt" class="aspect-[4/3] w-full object-cover transition duration-400 ease-in-out group-hover:scale-105" loading="lazy">
          <div class="p-4">
            <div class="text-sm font-semibold text-slate-700">{{ item.alt }}</div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Testimonials Section -->
  <section class="py-16 bg-slate-50 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-8 lg:grid-cols-12">
        <div class="lg:col-span-4" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
          <div class="rounded-2xl bg-white p-6 shadow-md ring-1 ring-slate-200/70">
            <div class="text-xs font-semibold uppercase tracking-wider text-brand-accent">Testimoni</div>
            <h2 class="mt-2 text-3xl font-extrabold tracking-tight text-slate-900">Suara dari Mereka</h2>
            <p class="mt-4 text-sm leading-relaxed text-slate-600">
              Kami menjaga privasi. Testimoni berikut adalah ringkasan pengalaman yang mewakili pola layanan kami.
            </p>
          </div>
        </div>
        <div class="grid gap-6 lg:col-span-8 md:grid-cols-3">
          <AppCard v-for="(t, idx) in testimonies" :key="t.name + t.role" class="p-6" interactive v-motion :initial="{ opacity: 0, y: 15 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 500, delay: idx * 100, ease: [0.16, 1, 0.3, 1] } }">
            <AppBadge :tone="idx === 0 ? 'emerald' : idx === 1 ? 'emerald' : 'amber'">{{ t.role }}</AppBadge>
            <p class="mt-4 text-sm leading-relaxed text-slate-700">“{{ t.quote }}”</p>
            <div class="mt-4 text-sm font-semibold text-slate-900">{{ t.name }}</div>
          </AppCard>
        </div>
      </div>
    </div>
  </section>

  <!-- Partners Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="text-center" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Mitra Kami</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Bersama Membangun Perlindungan Anak</h2>
      </div>

      <div class="mt-12 grid gap-8">
        <!-- Pemerintah -->
        <div>
          <h3 class="text-lg font-bold text-slate-700 mb-4">Pemerintah Daerah</h3>
          <div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            <div v-for="partner in partners.pemerintah" :key="partner.name" class="group flex items-center gap-4 p-5 rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
              <div v-if="partner.logo" class="flex-shrink-0">
                <img :src="partner.logo" :alt="partner.name" class="h-20 w-20 object-contain">
              </div>
              <div v-else class="flex h-20 w-20 flex-shrink-0 items-center justify-center rounded-lg bg-brand-primary/10">
                <AppIcon name="building" class="h-10 w-10 text-brand-primary"></AppIcon>
              </div>
              <span class="text-sm font-semibold text-slate-700 group-hover:text-brand-primary transition duration-300">{{ partner.name }}</span>
            </div>
          </div>
        </div>

        <!-- Kementerian -->
        <div>
          <h3 class="text-lg font-bold text-slate-700 mb-4">Kementerian & Lembaga Negara</h3>
          <div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            <div v-for="partner in partners.kementerian" :key="partner.name" class="group flex items-center gap-4 p-5 rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
              <div v-if="partner.logo" class="flex-shrink-0">
                <img :src="partner.logo" :alt="partner.name" class="h-20 w-20 object-contain">
              </div>
              <div v-else class="flex h-20 w-20 flex-shrink-0 items-center justify-center rounded-lg bg-brand-primary/10">
                <AppIcon name="government" class="h-10 w-10 text-brand-primary"></AppIcon>
              </div>
              <span class="text-sm font-semibold text-slate-700 group-hover:text-brand-primary transition duration-300">{{ partner.name }}</span>
            </div>
          </div>
        </div>

        <!-- Lembaga Non Pemerintah -->
        <div>
          <h3 class="text-lg font-bold text-slate-700 mb-4">Lembaga Non Pemerintah</h3>
          <div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            <div v-for="partner in partners.lembaga" :key="partner.name" class="group flex items-center gap-4 p-5 rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
              <div v-if="partner.logo" class="flex-shrink-0">
                <img :src="partner.logo" :alt="partner.name" class="h-20 w-20 object-contain">
              </div>
              <div v-else class="flex h-20 w-20 flex-shrink-0 items-center justify-center rounded-lg bg-brand-accent/10">
                <AppIcon name="heart" class="h-10 w-10 text-brand-accent"></AppIcon>
              </div>
              <span class="text-sm font-semibold text-slate-700 group-hover:text-brand-primary transition duration-300">{{ partner.name }}</span>
            </div>
          </div>
        </div>

        <!-- Perguruan Tinggi -->
        <div>
          <h3 class="text-lg font-bold text-slate-700 mb-4">Perguruan Tinggi</h3>
          <div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            <div v-for="partner in partners.perguruanTinggi" :key="partner.name" class="group flex items-center gap-4 p-5 rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg">
              <div v-if="partner.logo" class="flex-shrink-0">
                <img :src="partner.logo" :alt="partner.name" class="h-20 w-20 object-contain">
              </div>
              <div v-else class="flex h-20 w-20 flex-shrink-0 items-center justify-center rounded-lg bg-brand-primary/10">
                <AppIcon name="book" class="h-10 w-10 text-brand-primary"></AppIcon>
              </div>
              <span class="text-sm font-semibold text-slate-700 group-hover:text-brand-primary transition duration-300">{{ partner.name }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- CTA Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="mt-8 rounded-3xl bg-gradient-to-r from-brand-primary via-brand-primary to-brand-primary p-8 text-white shadow-xl sm:p-10" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
        <div class="grid gap-8 lg:grid-cols-12 lg:items-center">
          <div class="lg:col-span-8">
            <div class="text-sm font-semibold text-white/90">Butuh Bantuan atau Ingin Berdonasi?</div>
            <div class="mt-2 text-3xl font-extrabold tracking-tight">Laporkan Kasus atau Dukung Kami</div>
            <div class="mt-3 text-sm leading-relaxed text-white/85">
              Anda bisa anonim. Tim kami memprioritaskan keselamatan dan kerahasiaan. Donasi membantu pendampingan kasus dan edukasi.
            </div>
          </div>
          <div class="flex flex-col gap-3 lg:col-span-4 lg:items-end">
            <div class="flex flex-wrap gap-3">
              <AppButton variant="accent" size="lg" to="/lapor">Laporkan Sekarang</AppButton>
              <AppButton variant="white" size="lg" to="/donasi">Donasi</AppButton>
            </div>
            <div class="text-sm font-semibold text-white/90">Hotline: (022) 720 7023</div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Articles Section -->
  <section class="py-16 bg-slate-50 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 700, ease: [0.25, 1, 0.5, 1] } }">
        <div>
          <div class="text-xs font-semibold uppercase tracking-wider text-brand-accent">Artikel & Edukasi</div>
          <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Artikel Terbaru</h2>
        </div>
        <RouterLink to="/artikel" class="text-sm font-semibold text-brand-accent hover:text-brand-accent flex items-center gap-2">
          Baca semua artikel
          <svg viewBox="0 0 20 20" fill="none" class="h-4 w-4" stroke="currentColor" stroke-width="2">
            <path d="M6 15L15 6M9 6h6v6" stroke-linecap="round" stroke-linejoin="round"></path>
          </svg>
        </RouterLink>
      </div>

      <div class="mt-10 grid gap-8 lg:grid-cols-3">
        <RouterLink v-for="(a, idx) in latestArticles" :key="a.id" :to="`/artikel/${a.slug}`" class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg" v-motion :initial="{ opacity: 0, y: 15 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 500, delay: idx * 100, ease: [0.16, 1, 0.3, 1] } }">
          <div class="overflow-hidden">
            <img :src="a.thumbnailUrl || 'https://images.unsplash.com/photo-1529390079861-591de354faf5?auto=format&fit=crop&w=1400&q=80'" :alt="a.title" class="aspect-[16/9] w-full object-cover transition duration-400 ease-in-out group-hover:scale-105" loading="lazy">
          </div>
          <div class="p-6">
            <div class="flex items-center justify-between gap-3">
              <AppBadge :tone="a.coverTone === 'amber' ? 'amber' : a.coverTone === 'slate' ? 'slate' : 'emerald'">{{ a.category }}</AppBadge>
              <div class="text-xs font-semibold text-slate-500">{{ a.readingTime }} menit</div>
            </div>
            <div class="mt-3 text-xl font-extrabold tracking-tight text-slate-900 group-hover:text-brand-primary">{{ a.title }}</div>
            <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ a.excerpt }}</p>
            <div class="mt-4 text-sm font-semibold text-brand-primary flex items-center gap-2">
              Baca selengkapnya
              <svg viewBox="0 0 20 20" fill="none" class="h-4 w-4" stroke="currentColor" stroke-width="2">
                <path d="M6 15L15 6M9 6h6v6" stroke-linecap="round" stroke-linejoin="round"></path>
              </svg>
            </div>
          </div>
        </RouterLink>
      </div>
    </div>
  </section>
</template>
