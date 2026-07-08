<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import AppButton from '../ui/AppButton.vue'
import AppIcon from '../ui/AppIcon.vue'

const route = useRoute()
const mobileOpen = ref(false)

const links = [
  { to: '/', label: 'Home' },
  { to: '/tentang', label: 'Tentang' },
  { to: '/layanan', label: 'Layanan' },
  { to: '/artikel', label: 'Artikel' },
  { to: '/galeri', label: 'Galeri' },
  { to: '/donasi', label: 'Donasi' },
  { to: '/kontak', label: 'Kontak' },
]

function isActive(to) {
  if (to === '/') return route.path === '/'
  return route.path === to || route.path.startsWith(`${to}/`)
}
</script>

<template>
  <header class="sticky top-0 z-40 border-b border-slate-200/70 bg-white/85 backdrop-blur">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="flex h-16 items-center justify-between gap-4">
        <RouterLink
          to="/"
          class="flex items-center gap-3 rounded-xl px-2 py-1.5 transition duration-300 ease-in-out hover:-translate-y-0.5 focus:outline-none focus-visible:ring-2 focus-visible:ring-[#285858]/25"
        >
          <span class="grid h-9 w-9 place-items-center rounded-xl bg-[#285858]/10 ring-1 ring-[#285858]/20">
            <AppIcon name="shield" class="h-5 w-5 text-[#285858]" />
          </span>
          <div class="leading-tight">
            <div class="text-sm font-extrabold tracking-tight text-slate-900">LAHA</div>
            <div class="text-xs text-slate-500">Lembaga advokasi Hak Anak</div>
          </div>
        </RouterLink>

        <nav class="hidden items-center gap-1 lg:flex">
          <RouterLink
            v-for="l in links"
            :key="l.to"
            :to="l.to"
            class="rounded-xl px-3 py-2 text-sm font-semibold text-slate-700 transition duration-300 ease-in-out hover:-translate-y-0.5 hover:bg-slate-100"
            :class="isActive(l.to) ? 'bg-[#285858]/10 text-[#285858] ring-1 ring-[#285858]/20' : ''"
          >
            {{ l.label }}
          </RouterLink>
        </nav>

        <div class="hidden items-center gap-2 lg:flex">
          <AppButton variant="ghost" size="sm" to="/lapor">Laporkan Kasus</AppButton>
          <AppButton variant="soft" size="sm" to="/kontak">Jadi Relawan</AppButton>
          <AppButton variant="primary" size="sm" to="/donai">Donasi</AppButton>
        </div>

        <button
          class="inline-flex h-10 w-10 items-center justify-center rounded-xl ring-1 ring-slate-200 transition duration-300 ease-in-out hover:bg-slate-100 lg:hidden"
          type="button"
          :aria-expanded="mobileOpen ? 'true' : 'false'"
          @click="mobileOpen = !mobileOpen"
        >
          <span class="sr-only">Menu</span>
          <svg v-if="!mobileOpen" viewBox="0 0 24 24" class="h-5 w-5 text-slate-800" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
            <path d="M4 6h16" />
            <path d="M4 12h16" />
            <path d="M4 18h16" />
          </svg>
          <svg v-else viewBox="0 0 24 24" class="h-5 w-5 text-slate-800" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
            <path d="M6 6l12 12" />
            <path d="M18 6L6 18" />
          </svg>
        </button>
      </div>

      <div v-if="mobileOpen" class="pb-4 lg:hidden">
        <div class="rounded-xl bg-white p-3 shadow-md ring-1 ring-slate-200/70">
          <div class="grid gap-1">
            <RouterLink
              v-for="l in links"
              :key="l.to"
              :to="l.to"
              class="rounded-xl px-3 py-2 text-sm font-semibold text-slate-700 transition duration-300 ease-in-out hover:bg-slate-100"
              :class="isActive(l.to) ? 'bg-[#285858]/10 text-[#285858] ring-1 ring-[#285858]/20' : ''"
              @click="mobileOpen = false"
            >
              {{ l.label }}
            </RouterLink>
          </div>
          <div class="mt-3 grid gap-2">
            <AppButton variant="ghost" to="/lapor" @click="mobileOpen = false">Laporkan Kasus</AppButton>
            <AppButton variant="soft" to="/kontak" @click="mobileOpen = false">Jadi Relawan</AppButton>
            <AppButton variant="primary" to="/donasi" @click="mobileOpen = false">Donasi</AppButton>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>
