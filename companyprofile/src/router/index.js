import { createRouter, createWebHistory } from 'vue-router'
import { applySeo } from './seo'
import { useAuthStore } from '../store/auth'
import { api } from '../services/mockApi'

import PublicLayout from '../layouts/PublicLayout.vue'
import AdminLayout from '../layouts/AdminLayout.vue'

import HomeView from '../views/public/HomeView.vue'
import AboutView from '../views/public/AboutView.vue'
import ServicesView from '../views/public/ServicesView.vue'
import ArticlesListView from '../views/public/ArticlesListView.vue'
import ArticleDetailView from '../views/public/ArticleDetailView.vue'
import GalleryView from '../views/public/GalleryView.vue'
import DonateView from '../views/public/DonateView.vue'
import ContactView from '../views/public/ContactView.vue'
import ReportCaseView from '../views/public/ReportCaseView.vue'

import AdminLoginView from '../views/admin/AdminLoginView.vue'
import AdminDashboardView from '../views/admin/AdminDashboardView.vue'
import AdminArticlesView from '../views/admin/AdminArticlesView.vue'
import AdminServicesView from '../views/admin/AdminServicesView.vue'
import AdminTeamView from '../views/admin/AdminTeamView.vue'
import AdminReportsView from '../views/admin/AdminReportsView.vue'
import AdminMessagesView from '../views/admin/AdminMessagesView.vue'
import AdminGalleryView from '../views/admin/AdminGalleryView.vue'
import AdminDonationsView from '../views/admin/AdminDonationsView.vue'

const routes = [
  {
    path: '/',
    component: PublicLayout,
    children: [
      {
        path: '',
        name: 'home',
        component: HomeView,
        meta: {
          title: 'LAHA • Perlindungan & Advokasi',
          description:
            'Lembaga non-profit untuk pendampingan hukum, edukasi, konseling, hotline, dan pelaporan kasus perlindungan anak.',
        },
      },
      {
        path: 'tentang',
        name: 'about',
        component: AboutView,
        meta: { title: 'Tentang Kami • LAHA', description: 'Visi, misi, sejarah, dan tim lembaga.' },
      },
      {
        path: 'layanan',
        name: 'services',
        component: ServicesView,
        meta: { title: 'Layanan • LAHA', description: 'Pendampingan hukum, konseling, edukasi, dan hotline.' },
      },
      {
        path: 'artikel',
        name: 'articles',
        component: ArticlesListView,
        meta: {
          title: 'Artikel & Edukasi • LAHA',
          description: 'Konten edukasi tentang hak anak, parenting, dan pencegahan kekerasan.',
        },
      },
      {
        path: 'artikel/:slug',
        name: 'article-detail',
        component: ArticleDetailView,
        meta: { title: 'Artikel • LAHA', description: 'Detail artikel edukasi perlindungan anak.' },
      },
      {
        path: 'galeri',
        name: 'gallery',
        component: GalleryView,
        meta: { title: 'Galeri Kegiatan • LAHA', description: 'Dokumentasi kegiatan advokasi, edukasi, dan kampanye.' },
      },
      {
        path: 'donasi',
        name: 'donate',
        component: DonateView,
        meta: { title: 'Donasi • LAHA', description: 'Dukung pendampingan, edukasi, dan layanan hotline.' },
      },
      {
        path: 'kontak',
        name: 'contact',
        component: ContactView,
        meta: { title: 'Kontak • LAHA', description: 'Hubungi kami, hotline, dan lokasi.' },
      },
      {
        path: 'lapor',
        name: 'report',
        component: ReportCaseView,
        meta: { title: 'Laporan Kasus • LAHA', description: 'Form pelaporan kasus perlindungan anak (bisa anonim).' },
      },
    ],
  },
  {
    path: '/admin/login',
    name: 'admin-login',
    component: AdminLoginView,
    meta: { title: 'Login Admin • LAHA', description: 'Akses admin panel.' },
  },
  {
    path: '/admin',
    component: AdminLayout,
    meta: { requiresAuth: true, title: 'Admin • LAHA', description: 'Admin panel.' },
    children: [
      { path: '', redirect: { name: 'admin-dashboard' } },
      { path: 'dashboard', name: 'admin-dashboard', component: AdminDashboardView },
      { path: 'artikel', name: 'admin-articles', component: AdminArticlesView },
      { path: 'galeri', name: 'admin-gallery', component: AdminGalleryView },
      { path: 'donasi', name: 'admin-donations', component: AdminDonationsView },
      { path: 'layanan', name: 'admin-services', component: AdminServicesView },
      { path: 'tim', name: 'admin-team', component: AdminTeamView },
      { path: 'laporan', name: 'admin-reports', component: AdminReportsView },
      { path: 'pesan', name: 'admin-messages', component: AdminMessagesView },
    ],
  },
  { path: '/:pathMatch(.*)*', redirect: { name: 'home' } },
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) return savedPosition
    if (to.hash) return { el: to.hash, behavior: 'smooth' }
    if (to.path !== from.path) return { top: 0 }
    return {}
  },
})

router.beforeEach((to) => {
  const auth = useAuthStore()
  if (to.meta?.requiresAuth && !auth.isAuthenticated) {
    return { name: 'admin-login', query: { redirect: to.fullPath } }
  }
  if (to.name === 'admin-login' && auth.isAuthenticated) return { name: 'admin-dashboard' }
  return true
})

router.afterEach((to) => {
  applySeo(to)
  void api.trackVisit()
})

