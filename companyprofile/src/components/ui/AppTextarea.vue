<script setup>
const props = defineProps({
  modelValue: { type: String, default: '' },
  label: { type: String, default: '' },
  hint: { type: String, default: '' },
  error: { type: String, default: '' },
  placeholder: { type: String, default: '' },
  rows: { type: Number, default: 5 },
  name: { type: String, default: '' },
  disabled: { type: Boolean, default: false },
})

const emit = defineEmits(['update:modelValue'])
</script>

<template>
  <label class="block">
    <span v-if="label" class="mb-1.5 block text-sm font-semibold text-slate-800">{{ label }}</span>
    <textarea
      :value="modelValue"
      :rows="rows"
      :name="name || undefined"
      :placeholder="placeholder || undefined"
      :disabled="disabled"
      class="w-full resize-none rounded-xl bg-white px-3 py-2.5 text-slate-900 shadow-md ring-1 ring-slate-200/70 transition duration-300 ease-in-out placeholder:text-slate-400 focus:outline-none focus:ring-2 focus:ring-[#099044]/25"
      :class="[
        disabled ? 'cursor-not-allowed bg-slate-50 text-slate-500' : '',
        error ? 'ring-rose-200 focus:ring-rose-500/25' : '',
      ]"
      @input="emit('update:modelValue', $event.target.value)"
    />
    <span v-if="hint && !error" class="mt-1.5 block text-xs text-slate-500">{{ hint }}</span>
    <span v-if="error" class="mt-1.5 block text-xs font-semibold text-rose-600">{{ error }}</span>
  </label>
</template>
