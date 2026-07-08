<script setup>
import { reactive, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore } from '../../store/auth'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppIcon from '../../components/ui/AppIcon.vue'
import { validators } from '../../services/validate'

const route = useRoute()
const router = useRouter()
const auth = useAuthStore()

const form = reactive({
  email: 'admin@sahabatanak.org',
  password: 'admin123',
})

const errors = reactive({})
const submitting = ref(false)

function validate() {
  errors.email = validators.required(form.email, 'Email wajib diisi.') || validators.email(form.email)
  errors.password = validators.required(form.password, 'Password wajib diisi.')
  return !errors.email && !errors.password
}

async function submit() {
  if (!validate()) return
  submitting.value = true
  try {
    const res = await auth.login(form)
    if (!res.ok) {
      errors.password = res.message
      return
    }
    const redirect = typeof route.query.redirect === 'string' ? route.query.redirect : '/admin/dashboard'
    router.replace(redirect)
  } finally {
    submitting.value = false
  }
}
</script>

<template>
  <div class="min-h-svh bg-slate-50">
    <div class="mx-auto flex min-h-svh max-w-6xl items-center px-4 py-10 sm:px-6 lg:px-8">
      <div class="grid w-full gap-8 lg:grid-cols-12 lg:items-center">
        <div class="lg:col-span-6">
          <div class="flex items-center gap-3">
            <div class="grid h-12 w-12 place-items-center rounded-md bg-emerald-50 ring-1 ring-emerald-100">
              <AppIcon name="shield" class="h-6 w-6 text-emerald-700" />
            </div>
            <div class="leading-tight">
              <div class="text-base font-extrabold tracking-tight text-slate-900">Admin LAHA</div>
              <div class="text-sm text-slate-500">Panel manajemen konten & laporan</div>
            </div>
          </div>

          <h1 class="mt-6 text-balance text-3xl font-extrabold tracking-tight text-slate-900 sm:text-4xl">
            Masuk untuk mengelola artikel, layanan, tim, dan laporan kasus.
          </h1>
          <p class="mt-4 max-w-xl text-sm leading-relaxed text-slate-600">
            Ini adalah demo. Autentikasi bersifat dummy untuk kebutuhan prototyping.
          </p>
          <div class="mt-6 rounded-md bg-white p-4 text-xs text-slate-600 shadow-soft ring-1 ring-slate-200/70">
            Kredensial demo: <span class="font-semibold">admin@sahabatanak.org</span> / <span class="font-semibold">admin123</span>
          </div>
        </div>

        <div class="lg:col-span-6">
          <AppCard class="p-6 sm:p-8">
            <div class="text-sm font-extrabold text-slate-900">Login</div>
            <form class="mt-6 grid gap-4" @submit.prevent="submit">
              <AppInput v-model="form.email" label="Email" placeholder="admin@sahabatanak.org" :error="errors.email" autocomplete="email" />
              <AppInput
                v-model="form.password"
                label="Password"
                type="password"
                placeholder="••••••••"
                :error="errors.password"
                autocomplete="current-password"
              />
              <AppButton :loading="submitting" variant="primary" type="submit">Masuk</AppButton>
              <AppButton variant="ghost" type="button" to="/">Kembali ke Website</AppButton>
            </form>
          </AppCard>
        </div>
      </div>
    </div>
  </div>
</template>
