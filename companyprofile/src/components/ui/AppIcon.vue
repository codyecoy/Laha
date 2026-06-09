<script setup>
const props = defineProps({
  name: { type: String, required: true },
  class: { type: String, default: '' },
})
</script>

<template>
  <svg
    v-if="name === 'shield'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M12 2l7 4v6c0 5-3 9-7 10-4-1-7-5-7-10V6l7-4z" />
    <path d="M9 12l2 2 4-5" />
  </svg>

  <svg
    v-else-if="name === 'scale'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M12 3v18" />
    <path d="M7 6h10" />
    <path d="M7 6l-3 6h6l-3-6z" />
    <path d="M17 6l-3 6h6l-3-6z" />
    <path d="M7 21h10" />
  </svg>

  <svg
    v-else-if="name === 'heart'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path
      d="M20.5 11.5c-1.2 4.9-8.5 9-8.5 9s-7.3-4.1-8.5-9C2.6 7.5 5.2 5 8 5c1.7 0 3.1.9 4 2.1C12.9 5.9 14.3 5 16 5c2.8 0 5.4 2.5 4.5 6.5z"
    />
  </svg>

  <svg
    v-else-if="name === 'spark'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M12 2l1.5 6.5L20 10l-6.5 1.5L12 18l-1.5-6.5L4 10l6.5-1.5L12 2z" />
    <path d="M5 19l.8-3.2L9 15l-3.2-.8L5 11l-.8 3.2L1 15l3.2.8L5 19z" />
  </svg>

  <svg
    v-else-if="name === 'phone'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M22 16.9v3a2 2 0 0 1-2.2 2A19.9 19.9 0 0 1 3 5.2 2 2 0 0 1 5 3h3a2 2 0 0 1 2 1.7c.1.9.3 1.8.6 2.6a2 2 0 0 1-.4 2.1L9 10.6a16 16 0 0 0 4.4 4.4l1.2-1.2a2 2 0 0 1 2.1-.4c.8.3 1.7.5 2.6.6A2 2 0 0 1 22 16.9z" />
  </svg>

  <svg
    v-else-if="name === 'book'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M4 19.5A2.5 2.5 0 0 1 6.5 17H20" />
    <path d="M6.5 2H20v20H6.5A2.5 2.5 0 0 1 4 19.5v-15A2.5 2.5 0 0 1 6.5 2z" />
  </svg>

  <svg
    v-else-if="name === 'image'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <rect x="3" y="5" width="18" height="14" rx="2" ry="2" />
    <path d="M8 11l2.5 3 3.5-4 4 5" />
    <circle cx="9" cy="10" r="1" />
  </svg>

  <svg
    v-else-if="name === 'users'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2" />
    <circle cx="9" cy="7" r="4" />
    <path d="M23 21v-2a4 4 0 0 0-3-3.9" />
    <path d="M16 3.1a4 4 0 0 1 0 7.8" />
  </svg>

  <svg
    v-else-if="name === 'donate'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M12 1v22" />
    <path d="M17 5H9.5a3.5 3.5 0 0 0 0 7H14a3.5 3.5 0 0 1 0 7H6" />
  </svg>

  <svg
    v-else-if="name === 'report'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M6 2h9l3 3v17a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2z" />
    <path d="M14 2v4h4" />
    <path d="M8 13h8" />
    <path d="M8 17h6" />
    <path d="M8 9h3" />
  </svg>

  <svg
    v-else-if="name === 'chart'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M3 3v18h18" />
    <path d="M7 14l4-4 4 3 5-7" />
  </svg>

  <svg
    v-else-if="name === 'inbox'"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <path d="M22 12h-6l-2 3h-4l-2-3H2" />
    <path d="M5 7l2-4h10l2 4" />
    <path d="M2 12V7a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5" />
    <path d="M2 12v7a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2v-7" />
  </svg>

  <svg
    v-else
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    stroke-width="2"
    stroke-linecap="round"
    stroke-linejoin="round"
    :class="props.class"
  >
    <circle cx="12" cy="12" r="10" />
    <path d="M12 8v4" />
    <path d="M12 16h.01" />
  </svg>
</template>
