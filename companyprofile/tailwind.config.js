/** @type {import('tailwindcss').Config} */
import typography from '@tailwindcss/typography'

export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        brand: {
          primary: '#16A34A',
          accent: '#FACC15',
          ink: '#0F172A',
          muted: '#64748B',
          surface: '#FFFFFF',
          soft: '#F8FAFC',
        },
      },
      fontFamily: {
        sans: ['Inter', 'ui-sans-serif', 'system-ui', 'Segoe UI', 'Roboto', 'Arial', 'sans-serif'],
      },
      boxShadow: {
        soft: '0 10px 30px rgba(15, 23, 42, 0.08)',
        softSm: '0 8px 20px rgba(15, 23, 42, 0.07)',
      },
      borderRadius: {
        md: '12px',
        lg: '16px',
      },
    },
  },
  plugins: [typography],
}
