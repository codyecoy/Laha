<script setup>
import AppButton from '../ui/AppButton.vue'
import AppIcon from '../ui/AppIcon.vue'

const links = [
  { to: '/tentang', label: 'Tentang' },
  { to: '/layanan', label: 'Layanan' },
  { to: '/artikel', label: 'Artikel' },
  { to: '/galeri', label: 'Galeri' },
  { to: '/donasi', label: 'Donasi' },
  { to: '/kontak', label: 'Kontak' },
]
</script>

<template>
  <footer class="border-t border-slate-200/70 bg-white">
    <div class="mx-auto w-full max-w-7xl px-4 py-12 md:px-6 lg:px-8">
      <div class="grid gap-10 lg:grid-cols-12">
        <div class="lg:col-span-5">
          <div class="flex items-center gap-3">
            <span class="grid h-10 w-10 place-items-center rounded-md bg-[#285858]/10 ring-1 ring-[#285858]/20">
              <AppIcon name="shield" class="h-5 w-5 text-[#285858]" />
            </span>
            <div class="leading-tight">
              <div class="text-base font-extrabold tracking-tight text-slate-900">LAHA</div>
              <div class="text-sm text-slate-500">Aman, dipercaya, peduli, profesional.</div>
            </div>
          </div>

          <p class="mt-4 max-w-md text-sm leading-relaxed text-slate-600">
            Kami mendampingi pelaporan dan proses pemulihan dengan pendekatan berperspektif anak. Setiap langkah kami
            utamakan kerahasiaan dan keselamatan.
          </p>

          <div class="mt-6 flex flex-wrap gap-2">
            <AppButton variant="primary" size="sm" to="/lapor">Laporkan Kasus</AppButton>
            <AppButton variant="accent" size="sm" to="/donasi">Donasi</AppButton>
            <AppButton variant="ghost" size="sm" to="/admin/login">Admin</AppButton>
          </div>
        </div>

        <div class="grid gap-8 sm:grid-cols-2 lg:col-span-7 lg:grid-cols-3">
          <div>
            <div class="text-sm font-extrabold text-slate-900">Hotline</div>
            <div class="mt-3 rounded-md bg-slate-50 p-4 ring-1 ring-slate-200/70">
              <div class="flex items-start gap-3">
                <span class="mt-0.5 grid h-9 w-9 place-items-center rounded-md bg-white ring-1 ring-slate-200/70">
                  <AppIcon name="phone" class="h-4.5 w-4.5 text-[#285858]" />
                </span>
                <div>
                  <div class="text-sm font-semibold text-slate-900">(022) 720 7023</div>
                  <div class="mt-0.5 text-xs text-slate-500">Setiap hari 08.00–20.00 WIB</div>
                  <div class="mt-2 text-xs text-slate-600">Jika keadaan darurat, segera hubungi layanan darurat setempat.</div>
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
                class="text-sm font-semibold text-slate-600 transition hover:text-[#285858]"
              >
                {{ l.label }}
              </RouterLink>
              <RouterLink to="/lapor" class="text-sm font-semibold text-[#285858] transition hover:text-slate-900">
                Laporan Kasus
              </RouterLink>
            </div>
          </div>

          <div>
            <div class="text-sm font-extrabold text-slate-900">Transparansi</div>
            <div class="mt-3 grid gap-2 text-sm text-slate-600">
              <div class="rounded-md bg-slate-50 p-4 ring-1 ring-slate-200/70">
                <div class="text-sm font-semibold text-slate-900">Penggunaan dana</div>
                <div class="mt-1 text-xs leading-relaxed text-slate-600">
                  Donasi digunakan untuk pendampingan kasus, edukasi komunitas, dukungan hotline, dan biaya operasional yang
                  diaudit internal.
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="mt-12 flex flex-col gap-3 border-t border-slate-200/70 pt-6 text-xs text-slate-500 sm:flex-row sm:items-center sm:justify-between">
        <div>© {{ new Date().getFullYear() }} LAHA. Semua hak dilindungi.</div>
              </div>
    </div>
  </footer>
</template>
