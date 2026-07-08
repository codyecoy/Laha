<script setup>
import { ref } from 'vue'
import AdminSidebar from '../components/admin/AdminSidebar.vue'
import AdminTopbar from '../components/admin/AdminTopbar.vue'
import AppIcon from '../components/ui/AppIcon.vue'

const mobileOpen = ref(false)
</script>

<template>
  <div class="min-h-svh bg-gradient-to-b from-slate-50 via-slate-50 to-white">
    <div class="flex">
      <AdminSidebar />

      <div class="min-w-0 flex-1">
        <AdminTopbar :on-toggle-mobile="() => (mobileOpen = true)" />

        <div class="px-4 py-6 sm:px-6">
          <div class="mx-auto w-full max-w-7xl">
            <RouterView />
          </div>
        </div>
      </div>
    </div>

    <div v-if="mobileOpen" class="fixed inset-0 z-40 lg:hidden">
      <div class="absolute inset-0 bg-slate-900/35 backdrop-blur-sm" @click="mobileOpen = false" />
      <div class="absolute inset-y-0 left-0 w-80 max-w-[85vw] bg-white shadow-soft">
        <div class="flex h-16 items-center justify-between px-4">
          <div class="flex items-center gap-3">
            <div class="grid h-10 w-10 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
              <AppIcon name="shield" class="h-5 w-5 text-emerald-700" />
            </div>
            <div class="leading-tight">
              <div class="text-sm font-extrabold tracking-tight text-slate-900">Admin</div>
              <div class="text-xs text-slate-500">LAHA</div>
            </div>
          </div>
          <button
            type="button"
            class="inline-flex h-10 w-10 items-center justify-center rounded-md ring-1 ring-slate-200 transition hover:bg-slate-100"
            @click="mobileOpen = false"
          >
            <svg viewBox="0 0 24 24" class="h-5 w-5 text-slate-800" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round">
              <path d="M6 6l12 12" />
              <path d="M18 6L6 18" />
            </svg>
          </button>
        </div>
        <div class="px-3 pb-4">
          <AdminSidebar class="!block !w-full !border-r-0" />
        </div>
      </div>
    </div>
  </div>
</template>
