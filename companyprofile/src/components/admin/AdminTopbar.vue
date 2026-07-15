<script setup>
import AppButton from '../ui/AppButton.vue'
import AppIcon from '../ui/AppIcon.vue'
import { useAuthStore } from '../../store/auth'
import logoUrl from '../../assets/images/laha.png'

defineProps({
  onToggleMobile: { type: Function, default: null },
})

const auth = useAuthStore()
</script>

<template>
  <header class="sticky top-0 z-30 border-b border-slate-200/70 bg-white/85 backdrop-blur">
    <div class="px-4 sm:px-6">
      <div class="mx-auto flex h-16 w-full max-w-7xl items-center justify-between gap-4">
        <div class="flex items-center gap-3">
          <button
            type="button"
            class="inline-flex h-10 w-10 items-center justify-center rounded-md ring-1 ring-slate-200 transition hover:bg-slate-100 lg:hidden"
            @click="onToggleMobile && onToggleMobile()"
          >
            <span class="sr-only">Menu</span>
            <svg viewBox="0 0 24 24" class="h-5 w-5 text-slate-800" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M4 6h16" />
              <path d="M4 12h16" />
              <path d="M4 18h16" />
            </svg>
          </button>

          <div class="hidden items-center gap-3 sm:flex">
            <img :src="logoUrl" alt="LAHA Logo" class="h-9 w-auto object-contain">
            <div class="leading-tight">
              <div class="text-sm font-extrabold text-slate-900">Admin Panel</div>
              <div class="text-xs text-slate-500">LAHA</div>
            </div>
          </div>
        </div>

        <div class="flex items-center gap-2">
          <div class="hidden items-center gap-2 rounded-xl bg-white px-3 py-2 ring-1 ring-slate-200/70 sm:flex">
            <span class="grid h-9 w-9 place-items-center rounded-lg bg-slate-50 ring-1 ring-slate-200/70">
              <AppIcon name="users" class="h-4.5 w-4.5 text-slate-700" />
            </span>
            <div class="leading-tight">
              <div class="text-sm font-semibold text-slate-900">{{ auth.adminUser?.name || 'Admin' }}</div>
              <div class="text-xs text-slate-500">{{ auth.adminUser?.email }}</div>
            </div>
          </div>

          <AppButton variant="ghost" size="sm" to="/">Lihat Website</AppButton>
          <AppButton variant="danger" size="sm" @click="auth.logout()">Logout</AppButton>
        </div>
      </div>
    </div>
  </header>
</template>
