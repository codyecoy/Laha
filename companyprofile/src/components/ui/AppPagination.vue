<script setup>
import AppButton from './AppButton.vue'

const props = defineProps({
  page: { type: Number, default: 1 },
  totalPages: { type: Number, default: 1 },
})

const emit = defineEmits(['update:page'])
</script>

<template>
  <div class="flex items-center justify-between gap-3">
    <div class="text-sm text-slate-600">Halaman {{ page }} dari {{ totalPages }}</div>
    <div class="flex items-center gap-2">
      <AppButton variant="ghost" size="sm" :disabled="page <= 1" @click="emit('update:page', page - 1)">
        Sebelumnya
      </AppButton>
      <AppButton
        variant="ghost"
        size="sm"
        :disabled="page >= totalPages"
        @click="emit('update:page', page + 1)"
      >
        Berikutnya
      </AppButton>
    </div>
  </div>
</template>

