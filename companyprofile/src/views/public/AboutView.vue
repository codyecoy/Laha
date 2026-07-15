<script setup>
import { computed } from 'vue'
import { useContentStore } from '../../store/content'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppIcon from '../../components/ui/AppIcon.vue'

import gambar1 from '../../assets/images/image9.JPG'
import gambar2 from '../../assets/images/image2.JPG'
const content = useContentStore()
const team = computed(() => content.team)

const values = [
  {
    title: 'Non Diskriminasi',
    desc: 'Tidak membedakan perlakuan berdasarkan latar belakang apapun.',
    tone: 'emerald',
    icon: 'shield',
  },
  {
    title: 'Kepentingan Terbaik bagi Anak',
    desc: 'Setiap keputusan selalu memprioritaskan kebutuhan dan kesejahteraan anak.',
    tone: 'emerald',
    icon: 'users',
  },
  {
    title: 'Hak Hidup & Tumbuh Kembang',
    desc: 'Menjamin kelangsungan hidup dan perkembangan anak secara optimal.',
    tone: 'amber',
    icon: 'report',
  },
  {
    title: 'Penghargaan terhadap Pendapat Anak',
    desc: 'Mendengarkan dan menghargai suara anak dalam setiap proses.',
    tone: 'emerald',
    icon: 'donate',
  },
]

const missions = [
  {
    title: 'Program Pelayanan',
    desc: 'Mengembangkan program pelayanan bagi perlindungan hak-hak anak.',
    icon: 'users',
    tone: 'emerald',
  },
  {
    title: 'Media Edukasi',
    desc: 'Mengembangkan media sebagai sarana pendidikan, informasi, sosialisasi dan advokasi hak anak.',
    icon: 'report',
    tone: 'emerald',
  },
  {
    title: 'Jejaring Kolaborasi',
    desc: 'Mengembangkan jaringan bersama masyarakat, LSM, pemerintah dan lembaga lainnya.',
    icon: 'donate',
    tone: 'amber',
  },
  {
    title: 'Pemantauan Kebijakan',
    desc: 'Melakukan pemantauan terhadap implementasi kebijakan yang berkaitan dan berdampak terhadap anak.',
    icon: 'shield',
    tone: 'emerald',
  },
  {
    title: 'Advokasi Perubahan',
    desc: 'Mendorong terjadinya perubahan kebijakan yang lebih berpihak kepada anak.',
    icon: 'report',
    tone: 'emerald',
  },
]

const structure = [
  {
    title: 'Dewan Pembina',
    members: [
      { role: 'Ketua', name: 'Eko Kriswanto' },
      { role: 'Anggota', name: 'Distia Aviandari' },
      { role: 'Anggota', name: 'Andi Akbar' },
    ],
    tone: 'emerald',
  },
  {
    title: 'Dewan Pengawas',
    members: [
      { role: '', name: 'Dan Satriana' },
    ],
    tone: 'emerald',
  },
  {
    title: 'Dewan Pengurus',
    members: [
      { role: 'Ketua', name: 'Ahmad Faozie Zain' },
      { role: 'Sekretaris', name: 'Ade Mulyadi' },
      { role: 'Bendahara', name: 'Lia Masyrurokh' },
    ],
    tone: 'amber',
  },
  {
    title: 'Eksekutif',
    members: [
      { role: 'Direktur Eksekutif', name: 'Andi Akbar' },
      { role: 'Manager Administrasi & Keuangan', name: 'Lia Masyrurokh' },
      { role: 'Admin & Financial Support', name: 'Tanti Rianti H' },
      { role: 'Spesialis Juvenile Justice', name: 'Distia Aviandari' },
      { role: 'Divisi Konsultasi dan Bantuan Hukum', name: 'Dadang Sukmawijaya, S.H., M.H.' },
      { role: 'Divisi Konsultasi dan Bantuan Hukum', name: 'Asep Permana, S.H., M.H.' },
      { role: 'Divisi Konsultasi dan Bantuan Hukum', name: 'Distia Aviandari' },
      { role: 'Program', name: 'Ade Mulyadi' },
      { role: 'Program', name: 'Ida Farida Kh' },
      { role: 'Program', name: 'Amelya Nur Fauzi' },
      { role: 'Program', name: 'Eka Desti Ramadhanita' },
      { role: 'Program', name: 'Lanang Dwi Wicaksono' },
      { role: 'Media, Informasi & Teknologi', name: 'Dodi' },
      { role: 'Media, Informasi & Teknologi', name: 'Alma Aksela A' },
    ],
    tone: 'emerald',
  },
]
</script>

<template>
  <!-- Hero Section -->
  <section class="py-16 sm:py-20 bg-slate-50">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid items-center gap-8 lg:grid-cols-12 lg:gap-10">
        <div
          class="lg:col-span-7"
          v-motion
          :initial="{ opacity: 0, y: 16 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] },
          }"
        >
          <AppBadge tone="emerald">Tentang Kami</AppBadge>
          <h1 class="mt-4 text-balance text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">
            Sejarah LAHA
          </h1>
          <div class="mt-4 h-1.5 w-24 rounded-full bg-gradient-to-r from-brand-primary via-brand-primary to-brand-accent"></div>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-justify">
            Kami adalah organisasi non-profit yang bergerak dalam bidang kajian kebijakan pembangunan anak serta kebijakan pembangunan yang memiliki dampak terhadap anak. Untuk kepentingan itu, kami mengembangkan berbagai program dan layanan sebagai bentuk advokasi terhadap hak-hak anak, khususnya anak yang membutuhkan perlindungan khusus.
          </p>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-justify">
            Kami berdiri pada tanggal 12 Juli 2001 melalui Akta Nomor 25 yang dibuat oleh Notaris Dr. Wiratni Ahmadi, S.H.
          </p>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-justify">
            Menyesuaikan dengan UU Nomor 16 Tahun 2001 tentang Yayasan dan UU Nomor 28 Tahun 2004 tentang Perubahan atas UU Nomor 16 Tahun 2001, LAHA melakukan perubahan status hukum melalui Akta No.1 tanggal 1 Agustus 2024 yang dibuat oleh Notaris Yanti Yulianti, S.H., M.Kn.
          </p>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-justify">
            Dalam menjalankan kegiatan, LAHA berpedoman pada prinsip non diskriminasi, kepentingan terbaik bagi anak, hak hidup dan tumbuh kembang anak, serta penghargaan terhadap pendapat anak.
          </p>
        </div>
        <div class="lg:col-span-5">
          <div
            class="group overflow-hidden rounded-2xl bg-white shadow-lg ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-xl"
            v-motion
            :initial="{ opacity: 0, y: 16 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 800, delay: 150, ease: [0.25, 1, 0.5, 1] },
            }"
          >
            <img
              :src="gambar1"
              alt="Kegiatan LAHA"
              class="aspect-[4/3] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
              loading="lazy"
            />
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Visi Section -->
  <section class="py-16 sm:py-20 bg-slate-50">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="rounded-3xl bg-white p-8 sm:p-12 shadow-xl ring-1 ring-brand-primary/25" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-sm font-semibold uppercase tracking-wider text-brand-primary">Visi</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">
          Mendorong kehidupan bernegara dan bermasyarakat yang memberikan penghargaan, perlindungan dan pemenuhan terhadap hak-hak anak dengan memperhatikan prinsip kepentingan terbaik bagi anak, tanpa diskriminasi, memberikan perlindungan serta menghargai pendapat anak.
        </h2>
      </div>
    </div>
  </section>

  <!-- Misi Section -->
  <section class="py-16 sm:py-20 bg-slate-50">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="text-center mb-12" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Misi</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Apa yang Kami Lakukan</h2>
      </div>

      <div class="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
        <AppCard
          v-for="(m, idx) in missions"
          :key="m.title"
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
          <div class="grid h-12 w-12 place-items-center rounded-xl" :class="m.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : m.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : 'bg-brand-accent/25 text-brand-accent'">
            <AppIcon :name="m.icon" class="h-6 w-6"></AppIcon>
          </div>
          <div class="mt-4 text-lg font-extrabold tracking-tight text-slate-900">{{ m.title }}</div>
          <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ m.desc }}</p>
        </AppCard>
      </div>
    </div>
  </section>

  <!-- Prinsip Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="text-center mb-12" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-xs font-semibold uppercase tracking-wider text-brand-accent">Prinsip Kami</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Landasan Kerja LAHA</h2>
      </div>

      <div class="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
        <AppCard
          v-for="(v, idx) in values"
          :key="v.title"
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
          <div class="grid h-12 w-12 place-items-center rounded-xl" :class="v.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : v.tone === 'emerald' ? 'bg-brand-primary/10 text-brand-primary' : 'bg-brand-accent/25 text-brand-accent'">
            <AppIcon :name="v.icon" class="h-6 w-6"></AppIcon>
          </div>
          <AppBadge :tone="v.tone" class="mt-4">{{ v.title }}</AppBadge>
          <p class="mt-3 text-sm leading-relaxed text-slate-600">{{ v.desc }}</p>
        </AppCard>
      </div>
    </div>
  </section>

  <!-- Struktur Organisasi Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="text-center mb-12" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Struktur Organisasi</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Struktur Kepengurusan LAHA</h2>
        <p class="mt-3 max-w-2xl mx-auto text-slate-600">
          Susunan pengurus dan struktur organisasi Lembaga Advokasi Hak Anak (LAHA).
        </p>
      </div>

      <div class="space-y-8">
        <!-- Level 1: Dewan Pembina & Pengawas -->
        <div class="grid gap-6 md:grid-cols-2" v-motion :initial="{ opacity: 0, y: 20 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 600, delay: 0, ease: [0.16, 1, 0.3, 1] } }">
          <div class="rounded-2xl bg-white p-6 shadow-lg ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-xl">
            <div class="flex items-center gap-3 mb-4">
              <span class="grid h-10 w-10 place-items-center rounded-xl bg-brand-primary/10 ring-1 ring-brand-primary/25">
                <AppIcon name="shield" class="h-5 w-5 text-brand-primary"></AppIcon>
              </span>
              <h3 class="text-lg font-extrabold tracking-tight text-slate-900">Dewan Pembina</h3>
            </div>
            <div class="space-y-3">
              <div v-for="member in structure[0].members" :key="member.name" class="flex items-start gap-3">
                <span class="h-2 w-2 rounded-full bg-brand-primary mt-2"></span>
                <div>
                  <p v-if="member.role" class="font-semibold text-brand-primary">{{ member.role }}</p>
                  <p class="text-slate-600">{{ member.name }}</p>
                </div>
              </div>
            </div>
          </div>

          <div class="rounded-2xl bg-white p-6 shadow-lg ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-xl">
            <div class="flex items-center gap-3 mb-4">
              <span class="grid h-10 w-10 place-items-center rounded-xl bg-brand-primary/10 ring-1 ring-brand-primary/25">
                <AppIcon name="report" class="h-5 w-5 text-brand-primary"></AppIcon>
              </span>
              <h3 class="text-lg font-extrabold tracking-tight text-slate-900">Dewan Pengawas</h3>
            </div>
            <div class="space-y-3">
              <div v-for="member in structure[1].members" :key="member.name" class="flex items-start gap-3">
                <span class="h-2 w-2 rounded-full bg-brand-primary mt-2"></span>
                <div>
                  <p v-if="member.role" class="font-semibold text-brand-primary">{{ member.role }}</p>
                  <p class="text-slate-600">{{ member.name }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Connection Line -->
        <div class="flex justify-center">
          <div class="w-0.5 h-8 bg-slate-300"></div>
        </div>

        <!-- Level 2: Dewan Pengurus -->
        <div class="max-w-3xl mx-auto" v-motion :initial="{ opacity: 0, y: 20 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 600, delay: 200, ease: [0.16, 1, 0.3, 1] } }">
          <div class="rounded-2xl bg-white p-6 shadow-lg ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-xl">
            <div class="flex items-center gap-3 mb-4">
              <span class="grid h-10 w-10 place-items-center rounded-xl bg-brand-accent/25 ring-1 ring-brand-accent/35">
                <AppIcon name="users" class="h-5 w-5 text-brand-accent"></AppIcon>
              </span>
              <h3 class="text-lg font-extrabold tracking-tight text-slate-900">Dewan Pengurus</h3>
            </div>
            <div class="grid gap-4 sm:grid-cols-3">
              <div v-for="member in structure[2].members" :key="member.name" class="text-center p-3 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <p class="font-semibold text-brand-accent">{{ member.role }}</p>
                <p class="text-slate-600 text-sm mt-1">{{ member.name }}</p>
              </div>
            </div>
          </div>
        </div>

        <!-- Connection Line -->
        <div class="flex justify-center">
          <div class="w-0.5 h-8 bg-slate-300"></div>
        </div>

        <!-- Level 3: Eksekutif -->
        <div v-motion :initial="{ opacity: 0, y: 20 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 600, delay: 400, ease: [0.16, 1, 0.3, 1] } }">
          <div class="rounded-2xl bg-white p-6 sm:p-8 shadow-lg ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-xl">
            <div class="flex items-center gap-3 mb-6">
              <span class="grid h-10 w-10 place-items-center rounded-xl bg-brand-primary/10 ring-1 ring-brand-primary/25">
                <AppIcon name="spark" class="h-5 w-5 text-brand-primary"></AppIcon>
              </span>
              <h3 class="text-lg font-extrabold tracking-tight text-slate-900">Eksekutif</h3>
            </div>
            
            <!-- Direktur & Manager -->
            <div class="grid gap-4 sm:grid-cols-3 mb-6">
              <div v-for="member in structure[3].members.slice(0,3)" :key="member.name" class="p-4 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <p class="font-semibold text-brand-primary">{{ member.role }}</p>
                <p class="text-slate-600 text-sm mt-1">{{ member.name }}</p>
              </div>
            </div>

            <!-- Divisions -->
            <div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
              <!-- Spesialis Juvenile Justice -->
              <div class="p-4 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <div class="font-semibold text-slate-800 mb-2">Spesialis Juvenile Justice</div>
                <div class="text-xs text-slate-600 space-y-1">
                  <p>{{ structure[3].members[3].name }}</p>
                </div>
              </div>

              <!-- Divisi Konsultasi dan Bantuan Hukum -->
              <div class="p-4 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <div class="font-semibold text-slate-800 mb-2">Konsultasi & Bantuan Hukum</div>
                <div class="text-xs text-slate-600 space-y-1">
                  <p v-for="member in structure[3].members.slice(4,7)" :key="member.name">{{ member.name }}</p>
                </div>
              </div>
              
              <!-- Divisi Program -->
              <div class="p-4 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <div class="font-semibold text-slate-800 mb-2">Program</div>
                <div class="text-xs text-slate-600 space-y-1">
                  <p v-for="member in structure[3].members.slice(7,13)" :key="member.name">{{ member.name }}</p>
                </div>
              </div>
              
              <!-- Divisi Media, Informasi & Teknologi -->
              <div class="p-4 rounded-xl bg-slate-50 ring-1 ring-slate-200/70">
                <div class="font-semibold text-slate-800 mb-2">Media, Informasi & Teknologi</div>
                <div class="text-xs text-slate-600 space-y-1">
                  <p v-for="member in structure[3].members.slice(13,15)" :key="member.name">{{ member.name }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

  <!-- Team Section -->
  <section class="py-16 sm:py-20">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="text-center mb-12" v-motion :initial="{ opacity: 0, y: 16 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 800, ease: [0.25, 1, 0.5, 1] } }">
        <div class="text-xs font-semibold uppercase tracking-wider text-brand-primary">Tim Kami</div>
        <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900 sm:text-3xl">Orang-orang di Balik Layanan</h2>
      </div>

      <div class="grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
        <div
          v-for="(t, idx) in team"
          :key="t.id"
          class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion
          :initial="{ opacity: 0, y: 20 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 600, delay: idx * 60, ease: [0.16, 1, 0.3, 1] },
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
