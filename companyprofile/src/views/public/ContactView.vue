<script setup>
import { reactive, ref } from 'vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import { validators } from '../../services/validate'
import { useMessagesStore } from '../../store/messages'

const messages = useMessagesStore()

const form = reactive({
  name: '',
  email: '',
  subject: '',
  message: '',
})

const errors = reactive({})
const submitting = ref(false)
const success = ref(false)

function validate() {
  errors.name = validators.required(form.name, 'Nama wajib diisi.')
  errors.email = validators.required(form.email, 'Email wajib diisi.') || validators.email(form.email)
  errors.message = validators.required(form.message, 'Pesan wajib diisi.') || validators.minLen(form.message, 10)
  return !errors.name && !errors.email && !errors.message
}

async function submit() {
  success.value = false
  if (!validate()) return
  submitting.value = true
  try {
    await messages.submit(form)
    success.value = true
    form.subject = ''
    form.message = ''
  } catch (e) {
    errors.message = e?.message || 'Terjadi kesalahan.'
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
          <AppBadge tone="emerald">Kontak</AppBadge>
          <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
            Hubungi kami untuk kolaborasi, relawan, atau konsultasi awal.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Kami merespon dengan empatik dan menjaga kerahasiaan. Untuk pelaporan kasus, gunakan form khusus agar lebih terstruktur.
          </p>
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
              src="/src/assets/images/image8.JPG"
              alt="Tim dan relawan berkolaborasi"
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
            <div class="text-sm font-extrabold text-slate-900">Form kontak</div>
            <form class="mt-6 grid gap-4" @submit.prevent="submit">
              <div class="grid gap-4 sm:grid-cols-2">
                <AppInput v-model="form.name" label="Nama" placeholder="Nama lengkap" :error="errors.name" />
                <AppInput v-model="form.email" label="Email" placeholder="email@contoh.com" :error="errors.email" />
              </div>
              <AppInput v-model="form.subject" label="Subjek (opsional)" placeholder="Misal: Relawan edukasi" />
              <AppTextarea
                v-model="form.message"
                label="Pesan"
                placeholder="Ceritakan kebutuhan Anda secara singkat."
                :error="errors.message"
                :rows="6"
              />
              <AppButton :loading="submitting" variant="primary" type="submit">Kirim Pesan</AppButton>
              <div v-if="success" class="rounded-xl bg-emerald-50 p-4 text-sm text-emerald-800 ring-1 ring-emerald-100">
                Pesan terkirim. Tim kami akan merespon secepatnya.
              </div>
            </form>
          </AppCard>
        </div>

        <div class="lg:col-span-5">
          <div class="grid gap-4">
            <AppCard
              class="p-6"
              interactive
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 360, delay: 80, ease: [0.16, 1, 0.3, 1] },
              }"
            >
            <div class="flex items-start gap-3">
              <div class="grid h-11 w-11 place-items-center rounded-xl bg-amber-50 ring-1 ring-amber-100">
                <AppIcon name="phone" class="h-5 w-5 text-amber-600" />
              </div>
              <div>
                <div class="text-sm font-extrabold text-slate-900">Hotline</div>
                <div class="mt-1 text-sm font-semibold text-slate-700">(022) 7207023</div>
                <div class="mt-1 text-xs text-slate-500">09.00–20.00 WIB</div>
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
                transition: { type: 'tween', duration: 360, delay: 140, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Lokasi</div>
              <div class="mt-3 overflow-hidden rounded-xl shadow-md ring-1 ring-slate-200/70">
                <iframe
                  title="Map"
                  class="h-56 w-full"
                  loading="lazy"
                  referrerpolicy="no-referrer-when-downgrade"
                  src="https://www.openstreetmap.org/way/754154858#map=14/-6.92553/107.66018"
                />
              </div>
              <div class="mt-3 text-xs text-slate-500">Alamat dan koordinat dapat disesuaikan.</div>
            </AppCard>

            <AppCard
              class="p-6"
              interactive
              v-motion
              :initial="{ opacity: 0, y: 14 }"
              :visible-once="{
                opacity: 1,
                y: 0,
                transition: { type: 'tween', duration: 360, delay: 200, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Butuh pendampingan?</div>
              <div class="mt-2 text-sm leading-relaxed text-slate-600">
                Gunakan fitur laporan kasus untuk mencatat kronologi dan bukti dengan rapi.
              </div>
              <div class="mt-4">
                <AppButton variant="primary" to="/lapor">Buka Form Laporan</AppButton>
              </div>
            </AppCard>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
