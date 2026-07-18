<script setup>
import { computed, ref } from 'vue'
import { useContentStore } from '../../store/content'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppIcon from '../../components/ui/AppIcon.vue'

import gambar1 from '../../assets/images/image9.JPG'
import gambar2 from '../../assets/images/image2.JPG'
import fotoade from '../../assets/images/ade.jpg'
import fotoandi from '../../assets/images/andi.jpg'
import fotodadang from '../../assets/images/dadang.jpg'

const content = useContentStore()
const team = computed(() => content.team)

const activeTab = ref('dewan') // default tab
const selectedMember = ref(null) // for modal

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

const tabs = [
  { id: 'dewan', label: 'Dewan' },
  { id: 'pengurus', label: 'Pengurus' },
  { id: 'eksekutif', label: 'Eksekutif' },
]

const structureData = {
  dewan: [
    { role: 'Ketua Dewan Pembina', name: 'Eko Kriswanto', photo: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=600&q=80', bio: 'Eko Kriswanto memiliki pengalaman lebih dari 20 tahun di bidang advokasi hak anak, dengan fokus pada kebijakan publik dan perlindungan anak.' },
    { role: 'Anggota Dewan Pembina', name: 'Distia Aviandari', photo: 'https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?auto=format&fit=crop&w=600&q=80', bio: 'Distia Aviandari adalah spesialis juvenile justice yang telah membantu ratusan anak mendapatkan akses keadilan yang adil.' },
    { role: 'Anggota Dewan Pembina', name: 'Andi Akbar', photo: fotoandi, bio: 'Andi Akbar membawa pengalaman di bidang manajemen organisasi nirlaba dan pengembangan program.' },
    { role: 'Dewan Pengawas', name: 'Dan Satriana', photo: 'https://images.unsplash.com/photo-1527980965255-d3b416303d12?auto=format&fit=crop&w=600&q=80', bio: 'Dan Satriana bertanggung jawab dalam memastikan transparansi dan akuntabilitas organisasi.' },
  ],
  pengurus: [
    { role: 'Ketua', name: 'Ahmad Faozie Zain', photo: 'https://images.unsplash.com/photo-1527980965255-d3b416303d12?auto=format&fit=crop&w=600&q=80', bio: 'Ahmad Faozie Zain memimpin dewan pengurus dengan visi untuk memperluas jangkauan layanan LAHA ke lebih banyak wilayah.' },
    { role: 'Sekretaris', name: 'Ade Mulyadi', photo: fotoade, bio: 'Ade Mulyadi mengelola administrasi dan dokumentasi organisasi dengan cermat dan terorganisir.' },
    { role: 'Bendahara', name: 'Lia Masyrurokh', photo: 'https://images.unsplash.com/photo-1580489944761-15a19d654956?auto=format&fit=crop&w=600&q=80', bio: 'Lia Masyrurokh memastikan keuangan organisasi dikelola dengan transparan dan akuntabel.' },
  ],
  eksekutif: [
    { role: 'Direktur Eksekutif', name: 'Andi Akbar', photo: fotoandi, bio: 'Andi Akbar memimpin tim eksekutif dalam mengimplementasikan program-program LAHA untuk perlindungan anak.' },
    { role: 'Manager Administrasi & Keuangan', name: 'Lia Masyrurokh', photo: 'https://images.unsplash.com/photo-1580489944761-15a19d654956?auto=format&fit=crop&w=600&q=80', bio: 'Lia Masyrurokh mengelola operasional administrasi dan keuangan sehari-hari organisasi.' },
    { role: 'Admin & Financial Support', name: 'Tanti Rianti H', photo: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?auto=format&fit=crop&w=600&q=80', bio: 'Tanti Rianti H membantu dalam pengelolaan data keuangan dan administrasi harian.' },
    { role: 'Spesialis Juvenile Justice', name: 'Distia Aviandari', photo: 'https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?auto=format&fit=crop&w=600&q=80', bio: 'Distia Aviandari adalah ahli dalam sistem peradilan anak dan telah mendampingi banyak kasus perlindungan anak.' },
    { role: 'Divisi Konsultasi dan Bantuan Hukum', name: 'Dadang Sukmawijaya, S.H., M.H.', photo: fotodadang, bio: 'Dadang Sukmawijaya adalah pengacara yang fokus pada hukum perlindungan anak dan advokasi.' },
    { role: 'Divisi Konsultasi dan Bantuan Hukum', name: 'Asep Permana, S.H., M.H.', photo: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=600&q=80', bio: 'Asep Permana memberikan bantuan hukum dan pendampingan kepada anak dan keluarga yang membutuhkan.' },
    { role: 'Program', name: 'Ade Mulyadi', photo: fotoade, bio: 'Ade Mulyadi mengembangkan dan mengimplementasikan program edukasi dan pencegahan kekerasan terhadap anak.' },
    { role: 'Program', name: 'Ida Farida Kh', photo: 'https://images.unsplash.com/photo-1580489944761-15a19d654956?auto=format&fit=crop&w=600&q=80', bio: 'Ida Farida Kh fokus pada program pendampingan keluarga dan pengasuhan positif.' },
    { role: 'Program', name: 'Amelya Nur Fauzi', photo: 'https://images.unsplash.com/photo-1494790108377-be9c29b29330?auto=format&fit=crop&w=600&q=80', bio: 'Amelya Nur Fauzi mengoordinasikan program komunitas dan pelatihan kader.' },
    { role: 'Program', name: 'Eka Desti Ramadhanita', photo: 'https://images.unsplash.com/photo-1489424731084-a5d8b219a5bb?auto=format&fit=crop&w=600&q=80', bio: 'Eka Desti Ramadhanita mengembangkan materi edukasi dan modul pelatihan.' },
    { role: 'Program', name: 'Lanang Dwi Wicaksono', photo: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=600&q=80', bio: 'Lanang Dwi Wicaksono mengelola program outreach dan kolaborasi mitra.' },
    // { role: 'Media, Informasi & Teknologi', name: 'Dodi', photo: 'https://images.unsplash.com/photo-1527980965255-d3b416303d12?auto=format&fit=crop&w=600&q=80', bio: 'Dodi mengelola media sosial dan teknologi informasi organisasi.' },
    { role: 'Media, Informasi & Teknologi', name: 'Alma Aksela A', photo: 'https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?auto=format&fit=crop&w=600&q=80', bio: 'Alma Aksela A membuat konten edukatif dan desain komunikasi.' },
  ],
}
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
     
      </div>

      <!-- Tabs -->
      <div class="flex justify-center mb-10" v-motion :initial="{ opacity: 0, y: 12 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 600, ease: [0.25, 1, 0.5, 1] } }">
        <div class="inline-flex p-1 rounded-xl bg-slate-100">
          <button
            v-for="tab in tabs"
            :key="tab.id"
            @click="activeTab = tab.id"
            class="px-6 py-3 rounded-lg text-sm font-semibold transition-all duration-300"
            :class="activeTab === tab.id
              ? 'bg-brand-primary text-white shadow-md'
              : 'text-slate-600 hover:bg-white hover:text-slate-900'"
          >
            {{ tab.label }}
          </button>
        </div>
      </div>

      <!-- Tab Content -->
      <div class="grid gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4" v-motion :initial="{ opacity: 0, y: 20 }" :visible-once="{ opacity: 1, y: 0, transition: { type: 'tween', duration: 600, ease: [0.16, 1, 0.3, 1] } }">
        <div
          v-for="(member, idx) in structureData[activeTab]"
          :key="member.name"
          @click="selectedMember = member"
          class="group overflow-hidden rounded-2xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-2 hover:shadow-xl cursor-pointer"
        >
          <div class="overflow-hidden">
            <img
              :src="member.photo"
              :alt="member.name"
              class="aspect-square w-full object-cover transition duration-500 ease-in-out group-hover:scale-110"
              loading="lazy"
            />
          </div>
          <div class="p-5 text-center">
            <div class="text-sm font-extrabold tracking-tight text-slate-900">{{ member.name }}</div>
            <p class="mt-2 text-xs font-medium text-slate-500">{{ member.role }}</p>
          </div>
        </div>
      </div>

      <!-- Modal -->
      <div v-if="selectedMember" class="fixed inset-0 z-50 flex items-center justify-center p-4" @click.self="selectedMember = null">
        <div class="absolute inset-0 bg-black/60 backdrop-blur-sm"></div>
        <div class="relative w-full max-w-2xl rounded-2xl bg-white shadow-2xl overflow-hidden animate-fadeIn">
          <button
            @click="selectedMember = null"
            class="absolute top-4 right-4 z-10 flex h-10 w-10 items-center justify-center rounded-full bg-slate-100 text-slate-600 hover:bg-slate-200 transition-all"
          >
            <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
              <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
          </button>
          <div class="grid md:grid-cols-2">
            <div class="aspect-square overflow-hidden">
              <img :src="selectedMember.photo" :alt="selectedMember.name" class="h-full w-full object-cover" />
            </div>
            <div class="p-8 flex flex-col justify-center">
              <div class="text-sm font-semibold uppercase tracking-wider text-brand-primary">{{ selectedMember.role }}</div>
              <h3 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">{{ selectedMember.name }}</h3>
              <p class="mt-4 text-sm leading-relaxed text-slate-600">{{ selectedMember.bio }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
@keyframes fadeIn {
  from {
    opacity: 0;
    transform: scale(0.95);
  }
  to {
    opacity: 1;
    transform: scale(1);
  }
}
.animate-fadeIn {
  animation: fadeIn 0.3s ease-out forwards;
}
</style>