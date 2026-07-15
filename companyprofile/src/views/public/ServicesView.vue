<script setup>
import { computed } from 'vue'
import { useContentStore } from '../../store/content'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import AppButton from '../../components/ui/AppButton.vue'

import gambar1 from '../../assets/images/image9.JPG'
import gambar2 from '../../assets/images/image2.JPG'
import gambar3 from '../../assets/images/image3.JPG'
import gambar4 from '../../assets/images/image4.JPG'

const content = useContentStore()
const services = computed(() => content.services)

const programs = [
  {
    id: 1,
    title: 'Bantuan Hukum Litigasi',
    description: 'Pendampingan Anak yang Berhadapan dengan Hukum (ABH) di proses peradilan.',
    highlights: ['Pendampingan ABH', 'Advokasi hukum', 'Konsultasi hukum'],
    icon: 'scale',
    image: gambar1,
    tone: 'emerald',
  },
  {
    id: 2,
    title: 'Bantuan Hukum Non Litigasi',
    description: 'Layanan hukum di luar proses pengadilan untuk perlindungan hak anak.',
    highlights: [
      'Penyuluhan Hukum',
      'Konsultasi Hukum',
      'Mediasi',
      'Pendampingan Kasus di Luar Pengadilan',
      'Pemberdayaan Masyarakat',
    ],
    icon: 'shield',
    image: gambar2,
    tone: 'emerald',
  },
  {
    id: 3,
    title: 'Pelatihan Paralegal',
    description: 'Pelatihan bagi masyarakat agar mampu memberikan bantuan hukum dasar.',
    highlights: ['Pelatihan paralegal', 'Modul pelatihan', 'Praktik pendampingan'],
    icon: 'book',
    image: gambar3,
    tone: 'amber',
  },
  {
    id: 4,
    title: 'Pendampingan Anak di LPKA',
    description: 'Pendampingan dan dukungan untuk anak di Lembaga Pembinaan Khusus Anak.',
    highlights: ['Kunjungan rutin', 'Dukungan psikososial', 'Reintegration program'],
    icon: 'users',
    image: gambar4,
    tone: 'emerald',
  },
]

const steps = [
  { title: 'Konsultasi awal', desc: 'Ceritakan ringkas situasi dan kebutuhan. Tim kami membantu memetakan langkah aman.' },
  { title: 'Pendampingan', desc: 'Pendampingan sesuai kebutuhan: hukum, rujukan konseling, edukasi, atau koordinasi layanan.' },
  { title: 'Pemantauan', desc: 'Status pendampingan diperbarui secara berkala. Anda tetap memegang kendali atas informasi.' },
]
</script>

<template>
  <section class="py-16 sm:py-20 bg-slate-50">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        v-motion
        :initial="{ opacity: 0, y: 16 }"
        :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] },
        }"
      >
        <AppBadge tone="emerald">Program Kami</AppBadge>
        <h1 class="mt-4 text-balance text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
          Program Advokasi & Perlindungan Anak
        </h1>
        <div class="mt-4 h-1.5 w-24 rounded-full bg-gradient-to-r from-brand-primary via-brand-primary to-brand-accent"></div>
        <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
          Lembaga Advokasi Hak Anak (LAHA) menyediakan berbagai program untuk mendukung perlindungan dan pemenuhan hak-hak anak di Indonesia.
        </p>
      </div>
    </div>
  </section>

  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-8">
        <div
          v-for="(s, idx) in programs"
          :key="s.id"
          class="rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg overflow-hidden"
          v-motion
          :initial="{ opacity: 0, y: 12 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 700, delay: idx * 100, ease: [0.25, 1, 0.5, 1] },
          }"
        >
          <div class="grid gap-0 lg:grid-cols-12">
            <div class="overflow-hidden lg:col-span-5" :class="idx % 2 === 1 ? 'lg:order-last' : ''">
              <img
                :src="s.image"
                :alt="s.title"
                class="h-full w-full object-cover transition duration-300 ease-in-out hover:scale-105"
                style="aspect-ratio: 4 / 3"
                loading="lazy"
              />
            </div>

            <div class="p-6 lg:col-span-7 lg:p-8">
              <div class="flex items-start gap-4">
                <div class="grid h-12 w-12 place-items-center rounded-xl" :class="s.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : s.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : 'bg-brand-accent/25 text-brand-accent'">
                  <AppIcon :name="s.icon" class="h-6 w-6"></AppIcon>
                </div>
                <div class="flex-1">
                  <div class="text-lg font-extrabold tracking-tight text-slate-900">{{ s.title }}</div>
                  <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ s.description }}</p>
                </div>
              </div>

              <div v-if="s.highlights.length > 0" class="mt-6 grid gap-2 text-sm text-slate-600 sm:grid-cols-2">
                <div v-for="h in s.highlights" :key="h" class="flex items-center gap-2 rounded-xl bg-slate-50 px-3 py-2 ring-1 ring-slate-200/70">
                  <span class="h-1.5 w-1.5 rounded-full" :class="s.tone === 'emerald' ? 'bg-brand-primary' : s.tone === 'emerald' ? 'bg-brand-primary' : 'bg-brand-accent'"></span>
                  <span class="font-semibold text-slate-700">{{ h }}</span>
                </div>
              </div>

              <div class="mt-7 flex flex-col gap-3 sm:flex-row">
                <AppButton variant="primary" to="/kontak">Hubungi Kami</AppButton>
                <AppButton variant="ghost" to="/lapor">Laporkan Kasus</AppButton>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16 sm:py-20 bg-slate-50">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-6 lg:grid-cols-12">
        <div class="lg:col-span-4">
          <AppCard
            class="p-6"
            v-motion
            :initial="{ opacity: 0, y: 20 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 600, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-xs font-semibold uppercase tracking-wider text-brand-accent">Alur Layanan</div>
            <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Bagaimana Prosesnya?</h2>
            <p class="mt-3 text-sm leading-relaxed text-slate-600">
              Kami menjaga komunikasi tetap jelas dan realistis. Tidak ada janji berlebihan — yang ada adalah langkah yang aman.
            </p>
          </AppCard>
        </div>

        <div class="grid gap-6 lg:col-span-8 md:grid-cols-3">
          <AppCard
            v-for="(st, idx) in steps"
            :key="st.title"
            class="p-6"
            interactive
            v-motion
            :initial="{ opacity: 0, y: 20 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 600, delay: idx * 80, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-xs font-extrabold" :class="idx === 0 ? 'text-brand-primary' : idx === 1 ? 'text-brand-primary' : 'text-brand-accent'">Step {{ idx + 1 }}</div>
            <div class="mt-2 text-base font-extrabold tracking-tight text-slate-900">{{ st.title }}</div>
            <div class="mt-2 text-sm leading-relaxed text-slate-600">{{ st.desc }}</div>
          </AppCard>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        class="rounded-3xl bg-gradient-to-r from-brand-primary via-brand-primary to-brand-primary p-8 text-white shadow-xl sm:p-10"
        v-motion
        :initial="{ opacity: 0, y: 16 }"
        :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] },
        }"
      >
        <div class="grid gap-6 lg:grid-cols-12 lg:items-center">
          <div class="lg:col-span-8">
            <div class="flex items-center gap-2 text-sm font-semibold text-white/90">
              <AppIcon name="phone" class="h-4.5 w-4.5"></AppIcon>
              Hotline
            </div>
            <div class="mt-2 text-2xl font-extrabold tracking-tight">(022) 720 7021</div>
            <div class="mt-2 text-sm leading-relaxed text-white/85">
              Jam layanan: 09.00–20.00 WIB. Jika situasi darurat, segera hubungi layanan darurat setempat.
            </div>
          </div>
          <div class="flex flex-col gap-3 lg:col-span-4 lg:items-end">
            <AppButton variant="accent" to="/lapor">Laporkan Kasus</AppButton>
            <AppButton variant="ghost" class="border-white/20 text-white hover:bg-white/10" to="/kontak">Konsultasi & Relawan</AppButton>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
