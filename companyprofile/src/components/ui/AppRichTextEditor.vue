<script setup>
import { computed, nextTick, onMounted, ref, watch } from 'vue'

const props = defineProps({
  modelValue: { type: String, default: '' },
  label: { type: String, default: '' },
  hint: { type: String, default: '' },
  error: { type: String, default: '' },
})

const emit = defineEmits(['update:modelValue'])

const editorEl = ref(null)

const hasError = computed(() => Boolean(props.error))

function focusEditor() {
  editorEl.value?.focus?.()
}

function syncFromDom() {
  const el = editorEl.value
  if (!el) return
  emit('update:modelValue', el.innerHTML)
}

function applyCommand(command, value) {
  focusEditor()
  document.execCommand(command, false, value)
  syncFromDom()
}

function setBlock(tag) {
  applyCommand('formatBlock', tag)
}

function createLink() {
  const url = window.prompt('Masukkan URL:', 'https://')
  if (!url) return
  applyCommand('createLink', url)
}

function insertImage() {
  const url = window.prompt('Masukkan URL gambar:', 'https://')
  if (!url) return
  applyCommand('insertImage', url)
}

onMounted(async () => {
  await nextTick()
  if (editorEl.value) editorEl.value.innerHTML = props.modelValue || ''
})

watch(
  () => props.modelValue,
  (v) => {
    const el = editorEl.value
    if (!el) return
    const next = v || ''
    if (el.innerHTML !== next) el.innerHTML = next
  },
)
</script>

<template>
  <div class="grid gap-2">
    <div v-if="label" class="text-sm font-extrabold text-slate-900">{{ label }}</div>

    <div class="overflow-hidden rounded-xl bg-white shadow-md ring-1 ring-slate-200/70" :class="hasError ? 'ring-rose-200' : ''">
      <div class="flex flex-wrap items-center gap-1 border-b border-slate-100 bg-slate-50/60 p-2">
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('bold')">Bold</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('italic')">Italic</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('underline')">Underline</button>

        <div class="mx-1 h-5 w-px bg-slate-200" />

        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="setBlock('p')">P</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="setBlock('h2')">H2</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="setBlock('h3')">H3</button>

        <div class="mx-1 h-5 w-px bg-slate-200" />

        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('insertUnorderedList')">Bullets</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('insertOrderedList')">Number</button>

        <div class="mx-1 h-5 w-px bg-slate-200" />

        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="createLink">Link</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('unlink')">Unlink</button>
        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="insertImage">Image</button>

        <div class="mx-1 h-5 w-px bg-slate-200" />

        <button type="button" class="rounded-lg bg-white px-2 py-1 text-xs font-semibold text-slate-700 ring-1 ring-slate-200 hover:bg-slate-100" @click="applyCommand('removeFormat')">Clear</button>
      </div>

      <div
        ref="editorEl"
        class="min-h-56 w-full p-4 text-sm leading-relaxed text-slate-800 outline-none"
        contenteditable="true"
        @input="syncFromDom"
        @blur="syncFromDom"
      />
    </div>

    <div v-if="hasError" class="text-xs font-semibold text-rose-600">{{ error }}</div>
    <div v-else-if="hint" class="text-xs text-slate-500">{{ hint }}</div>
  </div>
</template>

