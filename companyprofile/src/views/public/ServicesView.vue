<script setup>
import { computed } from 'vue'
import { useContentStore } from '../../store/content'
import AppCard from '../../components/ui/AppCard.vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import AppButton from '../../components/ui/AppButton.vue'

const content = useContentStore()
const services = computed(() => content.services)

const steps = [
  { title: 'Konsultasi awal', desc: 'Ceritakan ringkas situasi dan kebutuhan. Tim kami membantu memetakan langkah aman.' },
  { title: 'Pendampingan', desc: 'Pendampingan sesuai kebutuhan: hukum, rujukan konseling, edukasi, atau koordinasi layanan.' },
  { title: 'Pemantauan', desc: 'Status pendampingan diperbarui secara berkala. Anda tetap memegang kendali atas informasi.' },
]
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
        <AppBadge tone="emerald">Layanan</AppBadge>
        <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
          Pendampingan yang terstruktur, responsif, dan profesional.
        </h1>
        <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
          Fokus kami adalah membantu masyarakat dan keluarga mengambil langkah yang tepat — tanpa mengorbankan keselamatan dan privasi.
        </p>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-8">
        <div
          v-for="(s, idx) in services"
          :key="s.id"
          class="rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
          v-motion
          :initial="{ opacity: 0, y: 14 }"
          :visible-once="{
            opacity: 1,
            y: 0,
            transition: { type: 'tween', duration: 380, delay: idx * 65, ease: [0.16, 1, 0.3, 1] },
          }"
        >
          <div class="grid gap-0 lg:grid-cols-12">
            <div class="overflow-hidden lg:col-span-5" :class="idx % 2 === 1 ? 'lg:order-last' : ''">
              <img
                :src="s.imageUrl || '../../assets/images/image9.JPG'"
                :alt="s.title"
                class="h-full w-full object-cover transition duration-300 ease-in-out hover:scale-105"
                style="aspect-ratio: 4 / 3"
                loading="lazy"
              />
            </div>

            <div class="p-6 lg:col-span-7 lg:p-8">
              <div class="flex items-start gap-4">
                <div class="grid h-12 w-12 place-items-center rounded-xl bg-emerald-50 ring-1 ring-emerald-100">
                  <AppIcon :name="s.icon" class="h-5 w-5 text-emerald-700" />
                </div>
                <div>
                  <div class="text-xl font-extrabold tracking-tight text-slate-900">{{ s.title }}</div>
                  <p class="mt-2 text-sm leading-relaxed text-slate-600">{{ s.description }}</p>
                </div>
              </div>

              <div class="mt-6 grid gap-2 text-sm text-slate-600 sm:grid-cols-2">
                <div v-for="h in s.highlights" :key="h" class="flex items-center gap-2 rounded-xl bg-slate-50 px-3 py-2 ring-1 ring-slate-200/70">
                  <span class="h-1.5 w-1.5 rounded-full bg-amber-400" />
                  <span class="font-semibold text-slate-700">{{ h }}</span>
                </div>
              </div>

              <div class="mt-7 flex flex-col gap-3 sm:flex-row">
                <AppButton variant="primary" to="/lapor">Laporkan Kasus</AppButton>
                <AppButton variant="ghost" to="/kontak">Konsultasi</AppButton>
              </div>
            </div>
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
            <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Alur layanan</div>
            <h2 class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">Bagaimana prosesnya?</h2>
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
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: idx * 70, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-xs font-extrabold text-emerald-700">Step {{ idx + 1 }}</div>
            <div class="mt-2 text-base font-extrabold tracking-tight text-slate-900">{{ st.title }}</div>
            <div class="mt-2 text-sm leading-relaxed text-slate-600">{{ st.desc }}</div>
          </AppCard>
        </div>
      </div>
    </div>
  </section>

  <section class="py-16">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div
        class="rounded-2xl bg-white p-6 shadow-md ring-1 ring-slate-200/70 sm:p-8"
        v-motion
        :initial="{ opacity: 0, y: 14 }"
        :visible-once="{
          opacity: 1,
          y: 0,
          transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
        }"
      >
        <div class="grid gap-6 lg:grid-cols-12 lg:items-center">
          <div class="lg:col-span-8">
            <div class="flex items-center gap-2 text-sm font-semibold text-slate-600">
              <AppIcon name="phone" class="h-4.5 w-4.5 text-amber-500" />
              Hotline
            </div>
            <div class="mt-2 text-2xl font-extrabold tracking-tight text-slate-900">(022) 720 7021</div>
            <div class="mt-2 text-sm leading-relaxed text-slate-600">
              Jam layanan: 09.00–20.00 WIB. Jika situasi darurat, segera hubungi layanan darurat setempat.
            </div>
          </div>
          <div class="flex flex-col gap-3 lg:col-span-4 lg:items-end">
            <AppButton variant="primary" to="/lapor">Laporkan Kasus</AppButton>
            <AppButton variant="ghost" to="/kontak">Konsultasi & Relawan</AppButton>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
