using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SahabatAnak.Api.Models;

namespace SahabatAnak.Api.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (!await db.Users.AnyAsync())
        {
            var hasher = new PasswordHasher<AppUser>();
            var admin = new AppUser
            {
                Email = "admin@sahabatanak.org",
                Name = "Admin Sahabat Anak",
                Role = "Admin",
            };
            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            db.Users.Add(admin);
        }

        if (!await db.Services.AnyAsync())
        {
            db.Services.AddRange(
                new ServiceItem
                {
                    Title = "Pendampingan Hukum",
                    Description = "Pendampingan berperspektif anak untuk memastikan hak korban terlindungi di setiap tahap.",
                    Icon = "scale",
                    ImageUrl = "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1600&q=80",
                    Highlights = new List<string> { "Konsultasi awal", "Pendampingan pelaporan", "Koordinasi aparat & layanan" },
                },
                new ServiceItem
                {
                    Title = "Konseling & Pemulihan",
                    Description = "Dukungan psikososial bersama mitra profesional untuk membantu pemulihan yang aman dan bertahap.",
                    Icon = "heart",
                    ImageUrl = "https://images.unsplash.com/photo-1522881193457-37ae97c90523?auto=format&fit=crop&w=1600&q=80",
                    Highlights = new List<string> { "Skrining kebutuhan", "Rujukan terverifikasi", "Pendampingan keluarga" },
                },
                new ServiceItem
                {
                    Title = "Edukasi & Pelatihan",
                    Description = "Materi edukasi hak anak, pencegahan kekerasan, dan pola asuh positif untuk komunitas.",
                    Icon = "spark",
                    ImageUrl = "https://images.unsplash.com/photo-1523240795612-9a054b0db644?auto=format&fit=crop&w=1600&q=80",
                    Highlights = new List<string> { "Sekolah & komunitas", "Materi praktis", "Modul & toolkit" },
                },
                new ServiceItem
                {
                    Title = "Hotline Darurat",
                    Description = "Akses cepat untuk konsultasi awal dan rujukan layanan pendampingan sesuai kebutuhan.",
                    Icon = "phone",
                    ImageUrl = "https://images.unsplash.com/photo-1496307042754-b4aa456c4a2d?auto=format&fit=crop&w=1600&q=80",
                    Highlights = new List<string> { "Jam layanan jelas", "Respon empatik", "Privasi terjaga" },
                }
            );
        }

        if (!await db.Teams.AnyAsync())
        {
            db.Teams.AddRange(
                new TeamMember
                {
                    Name = "Alya Putri",
                    Role = "Koordinator Advokasi",
                    Bio = "Mengawal pendampingan hukum dan jejaring layanan.",
                    Tone = "emerald",
                    PhotoUrl = "https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=1200&q=80",
                },
                new TeamMember
                {
                    Name = "Raka Pratama",
                    Role = "Konselor Mitra",
                    Bio = "Fokus pada dukungan psikososial berperspektif anak.",
                    Tone = "amber",
                    PhotoUrl = "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1200&q=80",
                },
                new TeamMember
                {
                    Name = "Sinta Wulandari",
                    Role = "Edukator Komunitas",
                    Bio = "Merancang program edukasi dan kampanye sosial.",
                    Tone = "slate",
                    PhotoUrl = "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1200&q=80",
                },
                new TeamMember
                {
                    Name = "Dimas Kurnia",
                    Role = "Manajer Program",
                    Bio = "Mengelola operasional dan dampak program secara terukur.",
                    Tone = "emerald",
                    PhotoUrl = "https://images.unsplash.com/photo-1529390079861-591de354faf5?auto=format&fit=crop&w=1200&q=80",
                }
            );
        }

        if (!await db.GalleryPhotos.AnyAsync())
        {
            db.GalleryPhotos.AddRange(
                new GalleryPhoto
                {
                    Title = "Edukasi komunitas",
                    Alt = "Kegiatan edukasi dan pelatihan",
                    ImageUrl = "https://images.unsplash.com/photo-1523240795612-9a054b0db644?auto=format&fit=crop&w=1600&q=80",
                    Category = "Edukasi",
                    SortOrder = 1,
                },
                new GalleryPhoto
                {
                    Title = "Kolaborasi pendampingan",
                    Alt = "Kolaborasi pendampingan dan advokasi",
                    ImageUrl = "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1600&q=80",
                    Category = "Pendampingan",
                    SortOrder = 2,
                },
                new GalleryPhoto
                {
                    Title = "Sesi diskusi",
                    Alt = "Diskusi dan konseling keluarga",
                    ImageUrl = "https://images.unsplash.com/photo-1522881193457-37ae97c90523?auto=format&fit=crop&w=1600&q=80",
                    Category = "Pemulihan",
                    SortOrder = 3,
                },
                new GalleryPhoto
                {
                    Title = "Relawan",
                    Alt = "Kegiatan relawan dan komunitas",
                    ImageUrl = "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1600&q=80",
                    Category = "Komunitas",
                    SortOrder = 4,
                }
            );
        }

        if (!await db.Articles.AnyAsync())
        {
            db.Articles.AddRange(
                new Article
                {
                    Title = "Tanda Kekerasan pada Anak yang Sering Terlewat",
                    Slug = "tanda-kekerasan-pada-anak-yang-sering-terlewat",
                    Category = "Perlindungan",
                    Excerpt = "Kenali sinyal fisik dan perilaku yang perlu diwaspadai, serta langkah aman untuk bertindak.",
                    CoverTone = "emerald",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1529070538774-1843cb3265df?auto=format&fit=crop&w=1600&q=80",
                    ReadingTimeMinutes = 6,
                    IsFeatured = true,
                    PublishedAt = DateTime.UtcNow.AddDays(-12),
                    ContentHtml =
                        "<p>Kekerasan pada anak sering tidak terlihat jelas. Perubahan kecil pada perilaku bisa menjadi sinyal.</p>" +
                        "<h2>Yang perlu diperhatikan</h2>" +
                        "<ul><li>Memar berulang di lokasi tidak wajar</li><li>Perubahan emosi mendadak</li><li>Ketakutan terhadap orang/ruang tertentu</li></ul>",
                },
                new Article
                {
                    Title = "Panduan Bicara tentang Batasan Tubuh (Body Safety)",
                    Slug = "panduan-bicara-tentang-batasan-tubuh-body-safety",
                    Category = "Edukasi",
                    Excerpt = "Cara sederhana mengajarkan anak tentang batasan tubuh dan meminta bantuan dengan aman.",
                    CoverTone = "amber",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1523240795612-9a054b0db644?auto=format&fit=crop&w=1600&q=80",
                    ReadingTimeMinutes = 7,
                    IsFeatured = false,
                    PublishedAt = DateTime.UtcNow.AddDays(-26),
                    ContentHtml =
                        "<p>Pendidikan body safety membantu anak memahami hak atas tubuhnya dan mengenali situasi tidak aman.</p>" +
                        "<h2>Prinsip inti</h2>" +
                        "<ul><li>Aturan sentuhan aman dan tidak aman</li><li>Rahasia baik vs rahasia buruk</li><li>Hak berkata “tidak”</li></ul>",
                },
                new Article
                {
                    Title = "Apa yang Harus Dilakukan Saat Menerima Laporan Kekerasan?",
                    Slug = "apa-yang-harus-dilakukan-saat-menerima-laporan-kekerasan",
                    Category = "Pendampingan",
                    Excerpt = "Checklist respon awal: aman, tenang, catat, rujuk, dan jaga kerahasiaan.",
                    CoverTone = "slate",
                    ThumbnailUrl = "https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1600&q=80",
                    ReadingTimeMinutes = 5,
                    IsFeatured = false,
                    PublishedAt = DateTime.UtcNow.AddDays(-40),
                    ContentHtml =
                        "<p>Respon awal menentukan keselamatan korban dan kualitas pendampingan.</p>" +
                        "<h2>Checklist singkat</h2>" +
                        "<ul><li>Utamakan keselamatan</li><li>Validasi emosi korban</li><li>Koordinasi dengan layanan profesional</li></ul>",
                }
            );
        }

        await db.SaveChangesAsync();
    }
}
