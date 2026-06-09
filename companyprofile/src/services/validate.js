export const validators = {
  required(value, message = 'Wajib diisi.') {
    const v = typeof value === 'string' ? value.trim() : value
    return v ? '' : message
  },
  email(value, message = 'Format email tidak valid.') {
    const v = String(value || '').trim()
    if (!v) return ''
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) ? '' : message
  },
  minLen(value, min, message) {
    const v = String(value || '').trim()
    if (v.length >= min) return ''
    return message || `Minimal ${min} karakter.`
  },
}

