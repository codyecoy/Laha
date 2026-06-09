<script setup>
import { computed, reactive, ref, watch } from 'vue'
import AppBadge from '../../components/ui/AppBadge.vue'
import AppCard from '../../components/ui/AppCard.vue'
import AppButton from '../../components/ui/AppButton.vue'
import AppInput from '../../components/ui/AppInput.vue'
import AppSelect from '../../components/ui/AppSelect.vue'
import AppTextarea from '../../components/ui/AppTextarea.vue'
import { validators } from '../../services/validate'
import { api } from '../../services/mockApi'

const form = reactive({
  amount: '150000',
  name: '',
  email: '',
  phone: '',
  method: 'Virtual Account',
  recipientKey: 'bca',
  referenceCode: '',
  proofUrl: '',
  note: '',
})

const errors = reactive({})
const submitting = ref(false)
const success = ref(false)
const successId = ref('')

const methodOptions = [
  { label: 'Virtual Account', value: 'Virtual Account' },
  { label: 'QRIS', value: 'QRIS' },
  { label: 'Kartu (dummy)', value: 'Kartu' },
]

const recipients = [
  {
    key: 'bca',
    bank: 'BCA',
    accountNumber: '1234567890',
    accountName: 'Yayasan Sahabat Anak',
  },
  {
    key: 'bni',
    bank: 'BNI',
    accountNumber: '0987654321',
    accountName: 'Yayasan Sahabat Anak',
  },
  {
    key: 'bsi',
    bank: 'BSI',
    accountNumber: '777000111222',
    accountName: 'Yayasan Sahabat Anak',
  },
]

const recipientOptions = recipients.map((r) => ({
  label: `${r.bank} • ${r.accountNumber} • a/n ${r.accountName}`,
  value: r.key,
}))

const selectedRecipient = computed(() => recipients.find((r) => r.key === form.recipientKey) || recipients[0])

const allocations = [
  { label: 'Pendampingan kasus', value: '55%' },
  { label: 'Edukasi & kampanye', value: '25%' },
  { label: 'Hotline & respon awal', value: '12%' },
  { label: 'Operasional minimal', value: '8%' },
]

function validate() {
  errors.amount = validators.required(form.amount, 'Nominal wajib diisi.')
  errors.email = validators.email(form.email)
  errors.recipient = validators.required(form.recipientKey, 'Pilih rekening penerima.')
  return !errors.amount && !errors.email && !errors.recipient
}

async function submit() {
  success.value = false
  successId.value = ''
  if (!validate()) return
  submitting.value = true
  try {
    const r = selectedRecipient.value
    const res = await api.submitDonation({
      amount: form.amount,
      name: form.name,
      email: form.email,
      phone: form.phone,
      method: form.method,
      recipientBank: r.bank,
      recipientAccountNumber: r.accountNumber,
      recipientAccountName: r.accountName,
      referenceCode: form.referenceCode,
      proofUrl: form.proofUrl,
      note: form.note,
    })
    success.value = true
    successId.value = res?.id || ''
  } finally {
    submitting.value = false
  }
}

watch(
  () => form.recipientKey,
  () => {
    errors.recipient = ''
  },
)
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
          <AppBadge tone="amber">Donasi</AppBadge>
          <h1 class="mt-4 text-balance text-4xl font-extrabold tracking-tight text-slate-900 sm:text-5xl">
            Jadikan perlindungan anak lebih mudah diakses.
          </h1>
          <p class="mt-4 max-w-3xl text-pretty text-base leading-relaxed text-slate-600 sm:text-lg">
            Donasi membantu pendampingan kasus, edukasi pencegahan, serta layanan hotline. Halaman ini menggunakan pembayaran dummy
            untuk demo.
          </p>
          <div class="mt-6 grid gap-3 sm:grid-cols-3">
            <div class="rounded-xl bg-white p-4 shadow-md ring-1 ring-slate-200/70">
              <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Fokus</div>
              <div class="mt-1 text-sm font-extrabold text-slate-900">Pendampingan kasus</div>
            </div>
            <div class="rounded-xl bg-white p-4 shadow-md ring-1 ring-slate-200/70">
              <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Pendekatan</div>
              <div class="mt-1 text-sm font-extrabold text-slate-900">Humanis & aman</div>
            </div>
            <div class="rounded-xl bg-white p-4 shadow-md ring-1 ring-slate-200/70">
              <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Transparansi</div>
              <div class="mt-1 text-sm font-extrabold text-slate-900">Alokasi jelas</div>
            </div>
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
              src="https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1600&q=80"
              alt="Kolaborasi pendampingan dan advokasi"
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
            <div class="text-sm font-extrabold text-slate-900">Transparansi penggunaan dana</div>
            <p class="mt-2 text-sm leading-relaxed text-slate-600">
              Berikut alokasi rata-rata (contoh) agar publik memahami prioritas program. Realisasi dapat berbeda sesuai kebutuhan
              kasus dan situasi lapangan.
            </p>

            <div class="mt-6 grid gap-4">
              <div
                v-for="(a, idx) in allocations"
                :key="a.label"
                class="rounded-xl bg-slate-50 p-4 ring-1 ring-slate-200/70 transition duration-300 ease-in-out hover:-translate-y-0.5 hover:bg-white hover:shadow-md"
                v-motion
                :initial="{ opacity: 0, y: 12 }"
                :visible-once="{
                  opacity: 1,
                  y: 0,
                  transition: { type: 'tween', duration: 300, delay: idx * 55, ease: [0.16, 1, 0.3, 1] },
                }"
              >
                <div class="flex items-center justify-between gap-3">
                  <div class="text-sm font-semibold text-slate-700">{{ a.label }}</div>
                  <div class="text-sm font-extrabold text-slate-900">{{ a.value }}</div>
                </div>
                <div class="mt-3 h-2 rounded-full bg-white ring-1 ring-slate-200/70">
                  <div class="h-2 rounded-full bg-emerald-600" :style="{ width: a.value }" />
                </div>
              </div>
            </div>

            <div class="mt-6 rounded-xl bg-amber-50 p-4 text-xs leading-relaxed text-slate-700 ring-1 ring-amber-100">
              Kami menghindari narasi sensasional. Donasi fokus pada perlindungan dan pemulihan yang aman serta bertahap.
            </div>
          </AppCard>
        </div>

        <div class="lg:col-span-5">
          <AppCard
            class="p-6 sm:p-8"
            v-motion
            :initial="{ opacity: 0, y: 14 }"
            :visible-once="{
              opacity: 1,
              y: 0,
              transition: { type: 'tween', duration: 380, delay: 120, ease: [0.16, 1, 0.3, 1] },
            }"
          >
            <div class="text-sm font-extrabold text-slate-900">Rincian donasi</div>
            <div class="mt-2 text-sm text-slate-600">Data donasi masuk ke admin untuk verifikasi.</div>

            <form class="mt-6 grid gap-4" @submit.prevent="submit">
              <AppInput v-model="form.amount" label="Nominal" placeholder="Contoh: 150000" :error="errors.amount" />
              <AppSelect v-model="form.method" label="Metode pembayaran" :options="methodOptions" />
              <AppSelect v-model="form.recipientKey" label="Rekening penerima" :options="recipientOptions" :error="errors.recipient" />

              <div class="rounded-xl bg-slate-50 p-4 text-xs text-slate-700 ring-1 ring-slate-200/70">
                <div class="text-xs font-semibold uppercase tracking-wider text-slate-500">Detail rekening</div>
                <div class="mt-2 grid gap-1">
                  <div>Bank: <span class="font-extrabold text-slate-900">{{ selectedRecipient.bank }}</span></div>
                  <div>No. Rek: <span class="font-extrabold text-slate-900">{{ selectedRecipient.accountNumber }}</span></div>
                  <div>Atas Nama: <span class="font-extrabold text-slate-900">{{ selectedRecipient.accountName }}</span></div>
                </div>
              </div>
              <div class="grid gap-4 sm:grid-cols-2">
                <AppInput v-model="form.name" label="Nama (opsional)" placeholder="Nama donatur" />
                <AppInput v-model="form.email" label="Email (opsional)" placeholder="email@contoh.com" :error="errors.email" />
              </div>
              <div class="grid gap-4 sm:grid-cols-2">
                <AppInput v-model="form.phone" label="No. HP (opsional)" placeholder="08xxxxxxxxxx" />
                <AppInput v-model="form.referenceCode" label="Kode/Referensi (opsional)" placeholder="Contoh: INV-001 / kode transfer" />
              </div>
              <AppInput v-model="form.proofUrl" label="Link bukti transfer (opsional)" placeholder="https://..." />
              <AppTextarea v-model="form.note" label="Catatan (opsional)" :rows="3" placeholder="Contoh: untuk program edukasi / bantuan darurat" />

              <AppButton :loading="submitting" variant="primary" type="submit">Lanjutkan Donasi</AppButton>

            <div v-if="success" class="rounded-xl bg-emerald-50 p-4 text-sm text-emerald-800 ring-1 ring-emerald-100">
              Donasi tercatat. Terima kasih sudah mendukung perlindungan anak.
              <div v-if="successId" class="mt-2 text-xs text-emerald-800">
                ID: <span class="font-extrabold">{{ successId }}</span>
              </div>
              </div>

              <div class="rounded-xl bg-slate-50 p-4 text-xs leading-relaxed text-slate-600 ring-1 ring-slate-200/70">
                Kredensial admin (dummy): <span class="font-semibold">admin@sahabatanak.org</span> /
                <span class="font-semibold">admin123</span>
              </div>
            </form>
          </AppCard>
        </div>
      </div>
    </div>
  </section>
</template>
