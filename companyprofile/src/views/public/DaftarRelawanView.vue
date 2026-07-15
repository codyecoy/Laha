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
  phone: '',
  motivation: '',
})

const errors = reactive({})
const submitting = ref(false)
const success = ref(false)

function validate() {
  errors.name = validators.required(form.name, 'Nama wajib diisi.')
  errors.email = validators.required(form.email, 'Email wajib diisi.') || validators.email(form.email)
  errors.phone = validators.required(form.phone, 'Nomor HP wajib diisi.')
  errors.motivation = validators.required(form.motivation, 'Motivasi wajib diisi.') || validators.minLen(form.motivation, 20)
  return !errors.name && !errors.email && !errors.phone && !errors.motivation
}

async function submit() {
  success.value = false
  if (!validate()) return
  submitting.value = true
  try {
    await messages.submit({
      name: form.name,
      email: form.email,
      subject: 'Pendaftaran Relawan',
      message: form.motivation + (form.phone ? `\n\nNomor HP: ${form.phone}` : ''),
    })
    success.value = true
    form.name = ''
    form.email = ''
    form.phone = ''
    form.motivation = ''
  } catch (e) {
    errors.motivation = e?.message || 'Terjadi kesalahan.'
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
          <AppBadge tone="emerald">Daftar Relawan</AppBadge>
          <h1 class="mt-4 text-balance text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Bergabunglah bersama kami untuk melindungi anak-anak.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Setiap kontribusi relawan sangat berarti untuk pendampingan, edukasi, dan advokasi hak anak.
          </p>
        </div>
        <div class="lg:col-span-5">
          <div
            class="group overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70 transition duration-400 ease-in-out hover:-translate-y-1 hover:shadow-lg"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 420, delay: 120, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <img
              src="https://images.unsplash.com/photo-1529333166447-444d0101a8a8?auto=format&fit=crop&w=1600&q=80"
              alt="Relawan bersama anak-anak"
              class="aspect-[4/3] w-full object-cover transition duration-400 ease-in-out group-hover:scale-105"
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
            <div class="text-sm font-extrabold text-slate-900">Form pendaftaran</div>
            <div class="mt-1 text-sm text-slate-600">Isi data berikut untuk bergabung sebagai relawan.</div>

            <form class="mt-6 grid gap-4" @submit.prevent="submit">
              <div class="grid gap-4 sm:grid-cols-2">
                <AppInput v-model="form.name" label="Nama Lengkap" placeholder="Nama lengkap Anda" :error="errors.name" />
                <AppInput v-model="form.email" label="Email" placeholder="email@contoh.com" :error="errors.email" />
              </div>
              <div class="grid gap-4 sm:grid-cols-2">
                <AppInput v-model="form.phone" label="Nomor HP" placeholder="08xx-xxxx-xxxx" :error="errors.phone" />
              </div>

              <AppTextarea
                v-model="form.motivation"
                label="Motivasi Bergabung"
                :rows="6"
                placeholder="Ceritakan alasan Anda ingin menjadi relawan, pengalaman terkait (jika ada), dan area yang Anda minati (pendampingan, edukasi, media, dll)."
                :error="errors.motivation"
                hint="Minimal 20 karakter."
              />

              <AppButton :loading="submitting" variant="primary" type="submit">Kirim Pendaftaran</AppButton>

              <div v-if="success" class="rounded-xl bg-emerald-50 p-4 text-sm text-emerald-800 ring-1 ring-emerald-100">
                Pendaftaran terkirim. Tim kami akan menghubungi Anda segera.
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
                transition: { type: 'tween', duration: 380, delay: 80, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="flex items-start gap-3">
                <div class="grid h-11 w-11 place-items-center rounded-xl bg-emerald-50 ring-1 ring-emerald-100">
                  <AppIcon name="users" class="h-5 w-5 text-emerald-700" />
                </div>
                <div>
                  <div class="text-sm font-extrabold text-slate-900">Program Relawan</div>
                  <div class="mt-2 text-sm leading-relaxed text-slate-600">
                    Bergabung dalam program seperti pendampingan, edukasi, media, atau administrasi sesuai minat dan ketersediaan waktu Anda.
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
                transition: { type: 'tween', duration: 360, delay: 140, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Persyaratan</div>
              <div class="mt-2 text-sm leading-relaxed text-slate-600">
                <ul class="mt-2 space-y-2">
                  <li class="flex items-start gap-2">
                    <span class="h-1.5 w-1.5 rounded-full bg-brand-primary mt-2"></span>
                    Memiliki komitmen terhadap perlindungan anak
                  </li>
                  <li class="flex items-start gap-2">
                    <span class="h-1.5 w-1.5 rounded-full bg-brand-primary mt-2"></span>
                    Bersedia mengikuti orientasi dan pelatihan
                  </li>
                  <li class="flex items-start gap-2">
                    <span class="h-1.5 w-1.5 rounded-full bg-brand-primary mt-2"></span>
                    Memiliki waktu minimal 4 jam/minggu
                  </li>
                </ul>
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
                transition: { type: 'tween', duration: 360, delay: 200, ease: [0.16, 1, 0.3, 1] },
              }"
            >
              <div class="text-sm font-extrabold text-slate-900">Ada pertanyaan?</div>
              <div class="mt-2 text-sm leading-relaxed text-slate-600">
                Hubungi kami via kontak yang tersedia untuk informasi lebih lanjut.
              </div>
              <div class="mt-4">
                <AppButton variant="ghost" to="/kontak">Hubungi Kami</AppButton>
              </div>
            </AppCard>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>