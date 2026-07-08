<script setup>
import { computed, reactive, ref } from 'vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import { validators } from '../../services/validate'
import { useReportsStore } from '../../store/reports'

const reports = useReportsStore()

const form = reactive({
  isAnonymous: true,
  name: '',
  contact: '',
  chronology: '',
  evidenceFiles: [],
})

const errors = reactive({})
const submitting = ref(false)
const successId = ref('')

const evidenceLabel = computed(() => {
  if (!form.evidenceFiles.length) return 'Belum ada file.'
  if (form.evidenceFiles.length === 1) return form.evidenceFiles[0].name
  return `${form.evidenceFiles.length} file dipilih`
})

function validate() {
  errors.chronology = validators.required(form.chronology, 'Kronologi wajib diisi.') || validators.minLen(form.chronology, 30)
  errors.name = form.isAnonymous ? '' : validators.required(form.name, 'Nama wajib diisi jika tidak anonim.')
  errors.contact = form.isAnonymous ? '' : validators.required(form.contact, 'Kontak wajib diisi jika tidak anonim.')
  return !errors.chronology && !errors.name && !errors.contact
}

function onFilesChange(event) {
  const input = event.target
  const files = Array.from(input.files || [])
  const mapped = []
  for (const f of files.slice(0, 3)) mapped.push({ name: f.name, type: f.type, size: f.size, file: f })
  form.evidenceFiles = mapped
}

async function submit() {
  successId.value = ''
  if (!validate()) return
  submitting.value = true
  try {
    const res = await reports.submit({
      isAnonymous: form.isAnonymous,
      name: form.name,
      contact: form.contact,
      chronology: form.chronology,
      evidence: form.evidenceFiles,
    })
    successId.value = res.id
    form.chronology = ''
    form.evidenceFiles = []
    if (form.isAnonymous) {
      form.name = ''
      form.contact = ''
    }
  } catch (e) {
    errors.chronology = e?.message || 'Terjadi kesalahan.'
  } finally {
    submitting.value = false
  }
}
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
          <AppBadge tone="emerald">Laporan Kasus</AppBadge>
          <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
            Laporkan dengan aman. Kami jaga kerahasiaan.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Anda boleh anonim. Ceritakan kronologi sejelas mungkin agar tim dapat menyusun langkah pendampingan yang tepat.
          </p>
          <div class="mt-6 rounded-xl bg-amber-50 p-4 text-sm text-slate-800 ring-1 ring-amber-100">
            Jika situasi darurat dan ada ancaman langsung, segera hubungi layanan darurat setempat.
          </div>
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
              src="https://images.unsplash.com/photo-1529070538774-1843cb3265df?auto=format&fit=crop&w=1600&q=80"
              alt="Anak dan keluarga (humanis)"
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
        <div class="lg:col-span-7">
          <AppCard
            class="p-6 sm:p-8"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="flex items-start justify-between gap-4">
              <div>
                <div class="text-sm font-extrabold text-slate-900">Form pelaporan</div>
                <div class="mt-1 text-sm text-slate-600">Minimal data, maksimal keselamatan.</div>
              </div>
              <label class="inline-flex cursor-pointer items-center gap-2 rounded-full bg-slate-50 px-3 py-2 text-sm font-semibold text-slate-700 ring-1 ring-slate-200/70">
                <input v-model="form.isAnonymous" type="checkbox" class="h-4 w-4 rounded border-slate-300 text-emerald-600 focus:ring-emerald-500/25" />
                Anonim
              </label>
            </div>

            <form class="mt-6 grid gap-4" @submit.prevent="submit">
            <div class="grid gap-4 sm:grid-cols-2">
              <AppInput v-model="form.name" :disabled="form.isAnonymous" label="Nama" placeholder="Nama pelapor" :error="errors.name" />
              <AppInput
                v-model="form.contact"
                :disabled="form.isAnonymous"
                label="Kontak (HP/Email)"
                placeholder="08xx / email@contoh.com"
                :error="errors.contact"
              />
            </div>

            <AppTextarea
              v-model="form.chronology"
              label="Kronologi"
              :rows="7"
              placeholder="Tuliskan: kapan/di mana kejadian, pihak yang terlibat, kondisi anak, dan langkah yang sudah dilakukan."
              :error="errors.chronology"
              hint="Minimal 30 karakter. Hindari menyebutkan identitas lengkap anak di publik; cukup inisial."
            />

            <div class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70">
              <div class="flex flex-wrap items-center justify-between gap-3">
                <div class="flex items-center gap-2 text-sm font-semibold text-slate-800">
                  <AppIcon name="report" class="h-4.5 w-4.5 text-amber-500" />
                  Upload bukti (opsional)
                </div>
                <label class="inline-flex cursor-pointer items-center gap-2 rounded-xl bg-white px-3 py-2 text-sm font-semibold text-slate-700 ring-1 ring-slate-200 transition hover:bg-slate-100">
                  <input type="file" class="hidden" multiple accept="image/*,application/pdf" @change="onFilesChange" />
                  Pilih file
                </label>
              </div>
              <div class="mt-2 text-xs text-slate-600">
                {{ evidenceLabel }} · Maksimal 3 file. Data disimpan sebagai mock (localStorage).
              </div>
            </div>

            <AppButton :loading="submitting" variant="primary" type="submit">Kirim Laporan</AppButton>

            <div v-if="successId" class="rounded-xl bg-emerald-50 p-4 text-sm text-emerald-800 ring-1 ring-emerald-100">
              Laporan terkirim. ID laporan: <span class="font-extrabold">{{ successId }}</span>
            </div>
          </form>
          </AppCard>
        </div>

        <div class="lg:col-span-5">
          <div class="grid gap-4">
            <div
              class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-1 hover:shadow-lg"
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 380, delay: 80, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <img
                src="https://images.unsplash.com/photo-1604881984065-32b9c2a0b7ba?auto=format&fit=crop&w=1600&q=80"
                alt="Kegiatan edukasi (demo)"
                class="aspect-[16/9] w-full object-cover transition duration-300 ease-in-out group-hover:scale-105"
                loading="lazy"
              />
            </div>

            <AppCard
              class="p-6"
              interactive
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 360, delay: 140, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="flex items-start gap-3">
                <div class="grid h-11 w-11 place-items-center rounded-xl bg-emerald-50 ring-1 ring-emerald-100">
                  <AppIcon name="shield" class="h-5 w-5 text-emerald-700" />
                </div>
                <div>
                  <div class="text-sm font-extrabold text-slate-900">Privasi & keamanan</div>
                  <div class="mt-2 text-sm leading-relaxed text-slate-600">
                    Kami tidak menampilkan data laporan di publik. Anda dapat memantau status melalui admin panel (demo).
                  </div>
                </div>
              </div>
            </AppCard>

            <AppCard
              class="p-6"
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 360, delay: 200, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Jika keadaan darurat</div>
              <div class="mt-2 text-sm leading-relaxed text-slate-600">
                Pastikan korban berada di tempat aman. Hubungi layanan darurat setempat jika ada ancaman langsung.
              </div>
              <div class="mt-4 rounded-xl bg-amber-50 p-4 text-sm text-slate-800 ring-1 ring-amber-100">
                Hotline: <span class="font-extrabold">(022) 720 7023</span> (08.00–20.00 WIB)
              </div>
            </AppCard>

            <AppCard
              class="p-6"
              interactive
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 360, delay: 260, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Butuh edukasi?</div>
              <div class="mt-2 text-sm leading-relaxed text-slate-600">
                Baca artikel edukasi tentang pencegahan kekerasan dan panduan respon awal.
              </div>
              <div class="mt-4">
                <AppButton variant="ghost" to="/artikel">Buka Artikel</AppButton>
              </div>
            </AppCard>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
