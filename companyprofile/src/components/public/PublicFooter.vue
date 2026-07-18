<script setup>
import AppButton from '../ui/AppButton.vue'
import AppIcon from '../ui/AppIcon.vue'
import logoUrl from '../../assets/images/laha.png'

const links = [
  { to: '/tentang', label: 'Tentang Kami' },
  { to: '/layanan', label: 'Program Kami' },
  { to: '/artikel', label: 'Artikel & Kegiatan' },
  { to: '/galeri', label: 'Galeri' },
  { to: '/donasi', label: 'Donasi' },
  { to: '/kontak', label: 'Kontak' },
]
</script>

<template>
  <footer class="border-t border-slate-200/70 bg-slate-50 pt-20 pb-10">
    <div class="mx-auto w-full max-w-7xl px-4 md:px-6 lg:px-8">
      <div class="grid gap-12 lg:grid-cols-12">
        <div class="lg:col-span-5">
          <div class="flex items-center gap-4">
            <img :src="logoUrl" alt="LAHA Logo" class="h-11 w-auto object-contain">
            <div class="leading-tight">
              <div class="text-base font-extrabold tracking-tight text-slate-900">LAHA</div>
              <div class="text-xs text-slate-500">Lembaga Advokasi Hak Anak</div>
            </div>
          </div>

          <p class="mt-5 max-w-md text-sm leading-relaxed text-slate-600">
            Lembaga Advokasi Hak Anak (LAHA) adalah organisasi non-profit yang bergerak dalam bidang kajian kebijakan pembangunan anak dan advokasi hak-hak anak.
          </p>

          <div class="mt-7 flex flex-wrap gap-3">
            <AppButton variant="primary" size="sm" to="/lapor">Laporkan Kasus</AppButton>
            <AppButton variant="accent" size="sm" to="/donasi">Donasi</AppButton>
            <AppButton variant="ghost" size="sm" to="/admin/login">Admin</AppButton>
          </div>
        </div>

        <div class="grid gap-8 sm:grid-cols-2 lg:col-span-7 lg:grid-cols-3">
          <div>
            <div class="text-sm font-extrabold text-slate-900">Kontak</div>
            <div class="mt-3 grid gap-3 text-sm text-slate-600">
              <div class="flex items-start gap-2">
                <AppIcon name="phone" class="h-4 w-4 text-brand-primary mt-0.5"></AppIcon>
                <div>
                  <div class="font-semibold text-slate-900">(022) 720 7021</div>
                  <div class="text-xs">09.00–20.00 WIB</div>
                </div>
              </div>
              <div class="flex items-start gap-2">
                <AppIcon name="report" class="h-4 w-4 text-brand-primary mt-0.5"></AppIcon>
                <div>
                  <div class="font-semibold text-slate-900">email@laha.or.id</div>
                </div>
              </div>
              <div class="flex items-start gap-2">
                <AppIcon name="users" class="h-4 w-4 text-brand-accent mt-0.5"></AppIcon>
                <div>
                  <div class="font-semibold text-slate-900">Alamat Kantor</div>
                  <div class="text-xs">Bandung, Jawa Barat</div>
                </div>
              </div>
            </div>
          </div>

          <div>
            <div class="text-sm font-extrabold text-slate-900">Navigasi</div>
            <div class="mt-3 grid gap-2">
              <RouterLink
                v-for="l in links"
                :key="l.to"
                :to="l.to"
                class="text-sm font-semibold text-slate-600 hover:text-brand-primary transition"
              >
                {{ l.label }}
              </RouterLink>
              <RouterLink to="/lapor" class="text-sm font-semibold text-brand-primary hover:text-brand-primary transition">
                Laporan Kasus
              </RouterLink>
            </div>
          </div>

          <div>
            <div class="text-sm font-extrabold text-slate-900">Media Sosial</div>
            <div class="mt-3 flex gap-3">
              <a href="#" class="grid h-10 w-10 place-items-center rounded-xl bg-white shadow-sm ring-1 ring-slate-200 hover:bg-brand-primary/5 hover:ring-brand-primary/25 transition">
                <AppIcon name="shield" class="h-4 w-4 text-slate-700"></AppIcon>
              </a>
              <a href="#" class="grid h-10 w-10 place-items-center rounded-xl bg-white shadow-sm ring-1 ring-slate-200 hover:bg-brand-primary/5 hover:ring-brand-primary/25 transition">
                <AppIcon name="users" class="h-4 w-4 text-slate-700"></AppIcon>
              </a>
              <a href="#" class="grid h-10 w-10 place-items-center rounded-xl bg-white shadow-sm ring-1 ring-slate-200 hover:bg-brand-accent/5 hover:ring-brand-accent/35 transition">
                <AppIcon name="heart" class="h-4 w-4 text-slate-700"></AppIcon>
              </a>
            </div>
          </div>
        </div>
      </div>

      <div class="mt-12 flex flex-col gap-3 border-t border-slate-200/70 pt-6 text-xs text-slate-500 sm:flex-row sm:items-center sm:justify-between">
        <div>© 2026 Lembaga Advokasi Hak Anak (LAHA). Seluruh Hak Cipta Dilindungi.</div>
      </div>
    </div>
  </footer>
</template>
