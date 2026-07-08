<script setup>
import { computed } from 'vue'
import { useContentStore } from '../../store/content'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'

import gambar1 from '../../assets/images/image9.JPG'
import gambar2 from '../../assets/images/image2.JPG'
const content = useContentStore()
const team = computed(() => content.team)

const values = [
  {
    title: 'Keselamatan & Kerahasiaan',
    desc: 'Menjaga privasi pelapor dan korban adalah prioritas. Data demo di proyek ini hanya contoh.',
    tone: 'emerald',
  },
  { title: 'Berperspektif Anak', desc: 'Setiap keputusan mempertimbangkan kebutuhan, kondisi, dan suara anak.', tone: 'amber' },
  { title: 'Kolaboratif', desc: 'Bekerja dengan keluarga, sekolah, komunitas, dan jejaring layanan profesional.', tone: 'slate' },
]

const milestones = [
  { year: '2018', title: 'Inisiatif komunitas', desc: 'Berawal dari relawan yang mendampingi kasus di lingkungan sekitar.' },
  { year: '2020', title: 'Program edukasi rutin', desc: 'Menyusun modul edukasi perlindungan anak dan pola asuh positif.' },
  { year: '2022', title: 'Jejaring rujukan', desc: 'Membangun kolaborasi dengan konselor, advokat, dan layanan kesehatan.' },
  { year: '2025', title: 'Pelaporan digital', desc: 'Mempermudah pelaporan dan pemantauan status pendampingan secara aman.' },
]
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
          <AppBadge tone="emerald">Tentang Kami</AppBadge>
          <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
            Lembaga advokasi Hak Anak, tegas terhadap kekerasan anak.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            LAHA adalah lembaga non-profit yang fokus pada perlindungan hak anak, pendampingan kasus, edukasi, dan kampanye
            sosial. Kami hadir untuk membantu masyarakat mengambil langkah yang tepat.
          </p>
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
              src="../../assets/images/image9.JPG"
              alt="Kegiatan edukasi bersama orang tua"
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
      <div class="grid gap-6 lg:grid-cols-12">
        <div class="lg:col-span-4">
          <AppCard
            class="p-6"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Visi</div>
            <div class="mt-2 text-xl font-extrabold tracking-tight text-slate-900">Setiap anak merasa aman dan dihargai.</div>
            <div class="mt-5 text-xs font-semibold uppercase tracking-wider text-slate-500">Misi</div>
            <ul class="mt-3 grid gap-3 text-sm leading-relaxed text-slate-600">
              <li class="flex items-start gap-2">
                <span class="mt-2 h-1.5 w-1.5 flex-none rounded-full bg-amber-400" />
                Mempermudah akses pelaporan dan pendampingan
              </li>
              <li class="flex items-start gap-2">
                <span class="mt-2 h-1.5 w-1.5 flex-none rounded-full bg-amber-400" />
                Menguatkan edukasi pencegahan kekerasan
              </li>
              <li class="flex items-start gap-2">
                <span class="mt-2 h-1.5 w-1.5 flex-none rounded-full bg-amber-400" />
                Menghubungkan keluarga dengan jejaring layanan
              </li>
              <li class="flex items-start gap-2">
                <span class="mt-2 h-1.5 w-1.5 flex-none rounded-full bg-amber-400" />
                Mengadvokasi kebijakan dan praktik yang ramah anak
              </li>
            </ul>
          </AppCard>
        </div>

        <div class="grid gap-6 lg:col-span-8 md:grid-cols-3">
          <AppCard
            v-for="(v, idx) in values"
            :key="v.title"
            class="p-6"
            interactive
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: idx * 65, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <AppBadge :tone="v.tone">{{ v.title }}</AppBadge>
            <p class="mt-3 text-sm leading-relaxed text-slate-600">{{ v.desc }}</p>
          </AppCard>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div>
        <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Sejarah</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Langkah yang berkembang</h2>
      </div>
      <div class="mt-8 grid gap-6 lg:grid-cols-4">
        <AppCard
          v-for="(m, idx) in milestones"
          :key="m.year"
          class="p-6"
          interactive
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <div class="text-xs font-semibold text-emerald-700">{{ m.year }}</div>
          <div class="mt-2 text-base font-extrabold tracking-tight text-slate-900">{{ m.title }}</div>
          <div class="mt-2 text-sm leading-relaxed text-slate-600">{{ m.desc }}</div>
        </AppCard>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div>
        <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Tim</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Orang-orang di balik layanan</h2>
      </div>

      <div class="mt-8 grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
        <div
          v-for="(t, idx) in team"
          :key="t.id"
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
              :src="t.photoUrl || 'https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=1200&q=80'"
              :alt="t.name"
              class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
              loading="lazy"
            />
          </div>
          <div class="p-5">
            <AppBadge :tone="t.tone === 'amber' ? 'amber' : t.tone === 'slate' ? 'slate' : 'emerald'">{{ t.role }}</AppBadge>
            <div class="mt-3 text-base font-extrabold tracking-tight text-slate-900">{{ t.name }}</div>
            <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ t.bio }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
