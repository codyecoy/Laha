<script setup>
import { useRoute } from 'vue-router'
import AppIcon from '../ui/AppIcon.vue'

const route = useRoute()

const items = [
  { to: '/admin/dashboard', label: 'Dashboard', icon: 'chart' },
  { to: '/admin/artikel', label: 'Artikel', icon: 'book' },
  { to: '/admin/galeri', label: 'Galeri', icon: 'image' },
  { to: '/admin/donasi', label: 'Donasi', icon: 'donate' },
  { to: '/admin/layanan', label: 'Layanan', icon: 'spark' },
  { to: '/admin/tim', label: 'Tim', icon: 'users' },
  { to: '/admin/laporan', label: 'Laporan Kasus', icon: 'report' },
  { to: '/admin/pesan', label: 'Pesan Masuk', icon: 'inbox' },
]
</script>

<template>
  <aside class="hidden w-72 flex-none border-r border-slate-200/70 bg-white lg:sticky lg:top-0 lg:flex lg:h-svh lg:flex-col">
    <div class="flex h-16 items-center gap-3 px-5">
      <div class="grid h-10 w-10 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
        <AppIcon name="shield" class="h-5 w-5 text-emerald-700" />
      </div>
      <div class="leading-tight">
        <div class="text-sm font-extrabold tracking-tight text-slate-900">Admin</div>
        <div class="text-xs text-slate-500">LAHA</div>
      </div>
    </div>

    <nav class="min-h-0 flex-1 overflow-y-auto px-3 pb-6">
      <div class="mt-2 grid gap-1">
        <RouterLink
          v-for="item in items"
          :key="item.to"
          :to="item.to"
          class="group relative flex items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-semibold text-slate-700 transition hover:bg-slate-100"
          :class="route.path === item.to ? 'bg-emerald-50 text-emerald-900 ring-1 ring-emerald-100' : ''"
        >
          <span
            class="absolute left-0 top-1/2 h-8 w-1 -translate-y-1/2 rounded-r-full bg-emerald-600 opacity-0 transition"
            :class="route.path === item.to ? 'opacity-100' : 'group-hover:opacity-30'"
          />
          <span
            class="grid h-9 w-9 place-items-center rounded-lg bg-white ring-1 ring-slate-200/70 transition group-hover:bg-slate-50"
            :class="route.path === item.to ? 'bg-emerald-100 ring-emerald-200/80' : ''"
          >
            <AppIcon :name="item.icon" class="h-4.5 w-4.5" :class="route.path === item.to ? 'text-emerald-800' : 'text-slate-700'" />
          </span>
          <span>{{ item.label }}</span>
        </RouterLink>
      </div>
    </nav>
  </aside>
</template>
