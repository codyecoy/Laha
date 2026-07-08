import { storage } from './storage'

const DB_KEY = 'sa_db_v1'

function uid(prefix) {
  return `${prefix}_${Math.random().toString(16).slice(2)}${Date.now().toString(16)}`
}

function slugify(input) {
  return String(input || '')
    .trim()
    .toLowerCase()
    .replace(/[^a-z0-9]+/g, '-')
    .replace(/(^-|-$)+/g, '')
}

function seed() {
  const now = new Date()
  const daysAgo = (d) => new Date(now.getTime() - d * 24 * 60 * 60 * 1000).toISOString()

  return {
    articles: [
      {
        id: uid('art'),
        title: 'Tanda Kekerasan pada Anak yang Sering Terlewat',
        slug: 'tanda-kekerasan-pada-anak-yang-sering-terlewat',
        category: 'Perlindungan',
        excerpt: 'Kenali sinyal fisik dan perilaku yang perlu diwaspadai, serta langkah aman untuk bertindak.',
        coverTone: 'emerald',
        isPublished: true,
        thumbnailUrl:
          'https://images.unsplash.com/photo-1529070538774-1843cb3265df?auto=format&fit=crop&w=1600&q=80',
        readingTime: 6,
        publishedAt: daysAgo(12),
        featured: true,
        content: `
<p>Kekerasan pada anak sering tidak terlihat jelas. Perubahan kecil pada perilaku bisa menjadi sinyal.</p>
<h2>Yang perlu diperhatikan</h2>
<ul>
  <li>Memar berulang di lokasi tidak wajar</li>
  <li>Perubahan emosi mendadak (takut, menarik diri, mudah marah)</li>
  <li>Ketakutan terhadap orang/ruang tertentu</li>
  <li>Penurunan prestasi atau gangguan tidur</li>
  <li>Cerita yang tidak konsisten</li>
</ul>
<h2>Langkah aman</h2>
<ol>
  <li>Pastikan anak berada di lingkungan aman.</li>
  <li>Dengarkan tanpa menghakimi dan jangan memaksa detail.</li>
  <li>Catat kronologi dan simpan bukti (bila ada).</li>
  <li>Hubungi layanan pendampingan/hotline.</li>
</ol>
<p>Anda bisa menggunakan fitur <strong>Laporan Kasus</strong> di situs ini untuk meminta pendampingan.</p>
        `.trim(),
      },
      {
        id: uid('art'),
        title: 'Panduan Bicara tentang Batasan Tubuh (Body Safety)',
        slug: 'panduan-bicara-tentang-batasan-tubuh-body-safety',
        category: 'Edukasi',
        excerpt: 'Cara sederhana mengajarkan anak tentang batasan tubuh dan meminta bantuan dengan aman.',
        coverTone: 'amber',
        isPublished: true,
        thumbnailUrl:
          'https://images.unsplash.com/photo-1523240795612-9a054b0db644?auto=format&fit=crop&w=1600&q=80',
        readingTime: 7,
        publishedAt: daysAgo(26),
        featured: false,
        content: `
<p>Pendidikan body safety membantu anak memahami hak atas tubuhnya dan mengenali situasi tidak aman.</p>
<h2>Prinsip inti</h2>
<ul>
  <li>Nama bagian tubuh sesuai usia</li>
  <li>Aturan sentuhan aman dan tidak aman</li>
  <li>Rahasia baik vs rahasia buruk</li>
  <li>Hak berkata “tidak” dan mencari orang dewasa tepercaya</li>
  <li>Ulangi secara berkala dengan bahasa hangat</li>
 </ul>
        `.trim(),
      },
      {
        id: uid('art'),
        title: 'Apa yang Harus Dilakukan Saat Menerima Laporan Kekerasan?',
        slug: 'apa-yang-harus-dilakukan-saat-menerima-laporan-kekerasan',
        category: 'Pendampingan',
        excerpt: 'Checklist respon awal: aman, tenang, catat, rujuk, dan jaga kerahasiaan.',
        coverTone: 'slate',
        isPublished: true,
        thumbnailUrl:
          'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1600&q=80',
        readingTime: 5,
        publishedAt: daysAgo(40),
        featured: false,
        content: `
<p>Respon awal menentukan keselamatan korban dan kualitas pendampingan.</p>
<h2>Checklist singkat</h2>
<ul>
  <li>Utamakan keselamatan</li>
  <li>Validasi emosi korban</li>
  <li>Hindari menyebarkan informasi</li>
  <li>Koordinasi dengan layanan profesional</li>
  <li>Simpan bukti secara aman</li>
</ul>
        `.trim(),
      },
    ],
    services: [
      {
        id: uid('srv'),
        title: 'Pendampingan Hukum',
        description: 'Pendampingan berperspektif anak untuk memastikan hak korban terlindungi di setiap tahap.',
        icon: 'scale',
        imageUrl: '../../assets/images/image9.JPG',
        highlights: ['Konsultasi awal', 'Pendampingan pelaporan', 'Koordinasi aparat & layanan'],
      },
      {
        id: uid('srv'),
        title: 'Konseling & Pemulihan',
        description: 'Dukungan psikososial bersama mitra profesional untuk membantu pemulihan yang aman dan bertahap.',
        icon: 'heart',
        imageUrl: '../../assets/images/image2.JPG',
        highlights: ['Skrining kebutuhan', 'Rujukan terverifikasi', 'Pendampingan keluarga'],
      },
      {
        id: uid('srv'),
        title: 'Edukasi & Pelatihan',
        description: 'Materi edukasi hak anak, pencegahan kekerasan, dan pola asuh positif untuk komunitas.',
        icon: 'spark',
        imageUrl: '../../assets/images/image3.JPG',
        highlights: ['Sekolah & komunitas', 'Materi praktis', 'Modul & toolkit'],
      },
      {
        id: uid('srv'),
        title: 'Hotline Darurat',
        description: 'Akses cepat untuk konsultasi awal dan rujukan layanan pendampingan sesuai kebutuhan.',
        icon: 'phone',
        imageUrl: '../../assets/images/image4.JPG',
        highlights: ['Jam layanan jelas', 'Respon empatik', 'Privasi terjaga'],
      },
    ],
    team: [
      {
        id: uid('tm'),
        name: 'Alya Putri',
        role: 'Koordinator Advokasi',
        bio: 'Mengawal pendampingan hukum dan jejaring layanan.',
        tone: 'emerald',
        photoUrl: '../../assets/images/image5.JPG',
      },
      {
        id: uid('tm'),
        name: 'Raka Pratama',
        role: 'Konselor Mitra',
        bio: 'Fokus pada dukungan psikososial berperspektif anak.',
        tone: 'amber',
        photoUrl: '../../assets/images/image6.JPG',
      },
      {
        id: uid('tm'),
        name: 'Sinta Wulandari',
        role: 'Edukator Komunitas',
        bio: 'Merancang program edukasi dan kampanye sosial.',
        tone: 'slate',
        photoUrl: '../../assets/images/image7.JPG',
      },
      {
        id: uid('tm'),
        name: 'Dimas Kurnia',
        role: 'Manajer Program',
        bio: 'Mengelola operasional dan dampak program secara terukur.',
        tone: 'emerald',
        photoUrl: '../../assets/images/image8.JPG',
      },
    ],
    gallery: [
      {
        id: uid('gal'),
        title: 'Edukasi komunitas',
        imageUrl: '../../assets/images/image9.JPG',
        alt: 'Kegiatan edukasi dan pelatihan komunitas',
        category: 'Edukasi',
        sortOrder: 1,
        createdAt: daysAgo(8),
      },
      {
        id: uid('gal'),
        title: 'Kolaborasi pendampingan',
        imageUrl: '../../assets/images/image7.JPG',
        alt: 'Kolaborasi pendampingan dan advokasi',
        category: 'Pendampingan',
        sortOrder: 2,
        createdAt: daysAgo(10),
      },
      {
        id: uid('gal'),
        title: 'Sesi diskusi',
        imageUrl: '../../assets/images/image4.JPG',
        alt: 'Diskusi dan konseling keluarga',
        category: 'Pemulihan',
        sortOrder: 3,
        createdAt: daysAgo(14),
      },
      {
        id: uid('gal'),
        title: 'Relawan',
        imageUrl: '../../assets/images/image2.JPG',
        alt: 'Kegiatan relawan dan komunitas',
        category: 'Komunitas',
        sortOrder: 4,
        createdAt: daysAgo(20),
      },
    ],
    visitsDaily: {},
    reports: [
      {
        id: uid('rpt'),
        isAnonymous: true,
        name: '',
        contact: '',
        chronology: 'Terdapat dugaan perundungan di lingkungan sekolah yang berulang.',
        evidence: [],
        status: 'baru',
        createdAt: daysAgo(4),
      },
      {
        id: uid('rpt'),
        isAnonymous: false,
        name: 'Orang Tua (inisial)',
        contact: '08xx-xxxx-xxxx',
        chronology: 'Anak mengaku mengalami kekerasan verbal dan ancaman.',
        evidence: [],
        status: 'ditinjau',
        createdAt: daysAgo(18),
      },
    ],
    messages: [
      {
        id: uid('msg'),
        name: 'Rina',
        email: 'rina@email.com',
        subject: 'Ingin jadi relawan edukasi',
        message: 'Saya tertarik membantu program edukasi. Bagaimana proses pendaftarannya?',
        status: 'baru',
        createdAt: daysAgo(2),
      },
    ],
    donations: [
      {
        id: uid('don'),
        amount: 150000,
        method: 'QRIS',
        name: 'Donatur (demo)',
        email: 'donatur@contoh.com',
        phone: '0812xxxxxxx',
        recipientBank: 'BCA',
        recipientAccountNumber: '1234567890',
        recipientAccountName: 'Yayasan Sahabat Anak',
        referenceCode: 'INV-DEMO-001',
        proofUrl: '',
        note: 'Semoga bermanfaat untuk program edukasi.',
        status: 'baru',
        createdAt: daysAgo(1),
      },
    ],
  }
}

export const mockDb = {
  uid,
  slugify,
  ensure() {
    const existing = storage.get(DB_KEY, null)
    if (existing) return existing
    const seeded = seed()
    storage.set(DB_KEY, seeded)
    return seeded
  },
  read() {
    return storage.get(DB_KEY, seed())
  },
  write(next) {
    storage.set(DB_KEY, next)
    return next
  },
}
