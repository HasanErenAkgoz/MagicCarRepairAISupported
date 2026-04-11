using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class ShopProfileSeedData
    {
        public static List<Certificate> GetCertificates()
        {
            return new List<Certificate>
            {
                // ── Yıldız Oto (ClientId=3) ──────────────────────────────────────────────
                new Certificate
                {
                    Id = 1, ClientId = 3,
                    CertificateName = "TSE Hizmet Yeterlilik Belgesi",
                    IssuingOrganization = "Türk Standartları Enstitüsü (TSE)",
                    CertificateNumber = "TSE-2019-KD-04821",
                    IssueDate = new DateTime(2019, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Otomotiv servis hizmetlerinde TSE Hizmet Yeterlilik belgesi.",
                    IsPublic = true, DisplayOrder = 1,
                    CreatedDate = new DateTime(2019, 6, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 2, ClientId = 3,
                    CertificateName = "Bosch Car Service Yetkili Üyelik",
                    IssuingOrganization = "Bosch Otomotiv Ürünleri",
                    CertificateNumber = "BCS-TR-2021-00234",
                    IssueDate = new DateTime(2021, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = null,
                    Description = "Bosch Car Service ağına dahil yetkili servis sertifikası.",
                    IsPublic = true, DisplayOrder = 2,
                    CreatedDate = new DateTime(2021, 3, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 3, ClientId = 3,
                    CertificateName = "Mesleki Yeterlilik Belgesi — Oto Tamircisi Seviye 4",
                    IssuingOrganization = "Mesleki Yeterlilik Kurumu (MYK)",
                    CertificateNumber = "MYK-2020-4AT-7743",
                    IssueDate = new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2025, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                    Description = "MYK tarafından verilen Oto Tamircisi Seviye 4 yeterlilik belgesi.",
                    IsPublic = true, DisplayOrder = 3,
                    CreatedDate = new DateTime(2020, 9, 10, 0, 0, 0, DateTimeKind.Utc)
                },

                // ── Anadolu Oto Merkezi (ClientId=4) ─────────────────────────────────────
                new Certificate
                {
                    Id = 4, ClientId = 4,
                    CertificateName = "ISO 9001:2015 Kalite Yönetim Sistemi",
                    IssuingOrganization = "Bureau Veritas",
                    CertificateNumber = "BV-IST-2022-QMS-00812",
                    IssueDate = new DateTime(2022, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2025, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Uluslararası ISO 9001:2015 Kalite Yönetim Sistemi sertifikası.",
                    IsPublic = true, DisplayOrder = 1,
                    CreatedDate = new DateTime(2022, 1, 20, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 5, ClientId = 4,
                    CertificateName = "Araç Muayene Hattı Yetki Belgesi",
                    IssuingOrganization = "Ulaştırma ve Altyapı Bakanlığı",
                    CertificateNumber = "UAB-34-2020-MH-5519",
                    IssueDate = new DateTime(2020, 7, 5, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = null,
                    Description = "Araç ön muayene hattı işletme yetki belgesi.",
                    IsPublic = true, DisplayOrder = 2,
                    CreatedDate = new DateTime(2020, 7, 5, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 6, ClientId = 4,
                    CertificateName = "Klima Gazı Geri Kazanım Yetkisi (F-Gaz)",
                    IssuingOrganization = "Çevre ve Şehircilik Bakanlığı",
                    CertificateNumber = "FGAZ-2021-IST-1123",
                    IssueDate = new DateTime(2021, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2026, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Araç kliması florlu gazlarının geri kazanımı ve dolumu yetkisi.",
                    IsPublic = true, DisplayOrder = 3,
                    CreatedDate = new DateTime(2021, 5, 12, 0, 0, 0, DateTimeKind.Utc)
                },

                // ── Boğaz Oto (ClientId=5) ────────────────────────────────────────────────
                new Certificate
                {
                    Id = 7, ClientId = 5,
                    CertificateName = "BMW Group Teknik Eğitim Sertifikası",
                    IssuingOrganization = "BMW Group Türkiye",
                    CertificateNumber = "BMW-TR-TRN-2023-0091",
                    IssueDate = new DateTime(2023, 2, 14, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2026, 2, 14, 0, 0, 0, DateTimeKind.Utc),
                    Description = "BMW araçlarında arıza tespit ve onarım teknik eğitim sertifikası.",
                    IsPublic = true, DisplayOrder = 1,
                    CreatedDate = new DateTime(2023, 2, 14, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 8, ClientId = 5,
                    CertificateName = "Mercedes-Benz Servis Uzmanlık Belgesi",
                    IssuingOrganization = "Mercedes-Benz Türk A.Ş.",
                    CertificateNumber = "MBT-SVC-2022-IS-0447",
                    IssueDate = new DateTime(2022, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2025, 8, 30, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Mercedes-Benz araçlarında teşhis ve onarım uzmanlık belgesi.",
                    IsPublic = true, DisplayOrder = 2,
                    CreatedDate = new DateTime(2022, 8, 30, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 9, ClientId = 5,
                    CertificateName = "TEXA Araç Teşhis Sistemi Kullanıcı Sertifikası",
                    IssuingOrganization = "TEXA S.p.A.",
                    CertificateNumber = "TEXA-2023-TR-003812",
                    IssueDate = new DateTime(2023, 11, 1, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = null,
                    Description = "TEXA multi-brand araç teşhis sistemleri üretici eğitim sertifikası.",
                    IsPublic = true, DisplayOrder = 3,
                    CreatedDate = new DateTime(2023, 11, 1, 0, 0, 0, DateTimeKind.Utc)
                },

                // ── Güneş Oto Elektrik (ClientId=6) ──────────────────────────────────────
                new Certificate
                {
                    Id = 10, ClientId = 6,
                    CertificateName = "Mesleki Yeterlilik Belgesi — Araç Elektriki Seviye 4",
                    IssuingOrganization = "Mesleki Yeterlilik Kurumu (MYK)",
                    CertificateNumber = "MYK-2021-4AE-9981",
                    IssueDate = new DateTime(2021, 4, 22, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2026, 4, 22, 0, 0, 0, DateTimeKind.Utc),
                    Description = "MYK onaylı Araç Elektriki Seviye 4 yeterlilik belgesi.",
                    IsPublic = true, DisplayOrder = 1,
                    CreatedDate = new DateTime(2021, 4, 22, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 11, ClientId = 6,
                    CertificateName = "HV/EV Araç Güvenlik Eğitimi",
                    IssuingOrganization = "TAYSAD Eğitim Merkezi",
                    CertificateNumber = "TAYSAD-HV-2024-0156",
                    IssueDate = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2027, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Hibrit ve elektrikli araçlerde güvenli çalışma eğitim sertifikası.",
                    IsPublic = true, DisplayOrder = 2,
                    CreatedDate = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },

                // ── Prestij Auto (ClientId=7) ─────────────────────────────────────────────
                new Certificate
                {
                    Id = 12, ClientId = 7,
                    CertificateName = "Ferrari Approved Technician",
                    IssuingOrganization = "Ferrari S.p.A.",
                    CertificateNumber = "FAT-2022-EMEA-TR-00011",
                    IssueDate = new DateTime(2022, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Ferrari onaylı teknik uzman sertifikası — EMEA bölgesi.",
                    IsPublic = true, DisplayOrder = 1,
                    CreatedDate = new DateTime(2022, 5, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 13, ClientId = 7,
                    CertificateName = "3M Authorized Installer — Paint Protection Film",
                    IssuingOrganization = "3M Turkey",
                    CertificateNumber = "3M-PPF-TR-2023-AUT-0044",
                    IssueDate = new DateTime(2023, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = null,
                    Description = "3M Scotchgard Paint Protection Film yetkili uygulayıcı belgesi.",
                    IsPublic = true, DisplayOrder = 2,
                    CreatedDate = new DateTime(2023, 3, 20, 0, 0, 0, DateTimeKind.Utc)
                },
                new Certificate
                {
                    Id = 14, ClientId = 7,
                    CertificateName = "ISO 14001:2015 Çevre Yönetim Sistemi",
                    IssuingOrganization = "SGS",
                    CertificateNumber = "SGS-ISO14-TR-2023-0388",
                    IssueDate = new DateTime(2023, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    ExpiryDate = new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                    Description = "Çevre dostu atık ve kimyasal yönetimi ISO 14001 sertifikası.",
                    IsPublic = true, DisplayOrder = 3,
                    CreatedDate = new DateTime(2023, 7, 1, 0, 0, 0, DateTimeKind.Utc)
                },
            };
        }

        public static List<FacilityPhoto> GetFacilityPhotos()
        {
            var uploadDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return new List<FacilityPhoto>
            {
                // ── Yıldız Oto (ClientId=3) ──────────────────────────────────────────────
                new FacilityPhoto { Id = 1,  ClientId = 3, Title = "Dış Görünüm", Category = "Dış Görünüm", PhotoPath = "https://images.unsplash.com/photo-1619642751034-765dfdf7c58e?w=800", IsPublic = true, DisplayOrder = 1, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 2,  ClientId = 3, Title = "Tamir Atölyesi", Category = "Atölye", PhotoPath = "https://images.unsplash.com/photo-1607860108855-64acf2078ed9?w=800", IsPublic = true, DisplayOrder = 2, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 3,  ClientId = 3, Title = "Teşhis Ekipmanları", Category = "Ekipman", PhotoPath = "https://images.unsplash.com/photo-1617531653332-bd46c16f7d82?w=800", IsPublic = true, DisplayOrder = 3, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 4,  ClientId = 3, Title = "Bekleme Salonu", Category = "Bekleme Alanı", PhotoPath = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=800", IsPublic = true, DisplayOrder = 4, UploadDate = uploadDate, CreatedDate = uploadDate },

                // ── Anadolu Oto Merkezi (ClientId=4) ─────────────────────────────────────
                new FacilityPhoto { Id = 5,  ClientId = 4, Title = "Ana Giriş", Category = "Dış Görünüm", PhotoPath = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800", IsPublic = true, DisplayOrder = 1, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 6,  ClientId = 4, Title = "Büyük Servis Alanı", Category = "Atölye", PhotoPath = "https://images.unsplash.com/photo-1493238792000-8113da705763?w=800", IsPublic = true, DisplayOrder = 2, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 7,  ClientId = 4, Title = "Lift Sistemleri", Category = "Ekipman", PhotoPath = "https://images.unsplash.com/photo-1591955506264-3f5a6834570a?w=800", IsPublic = true, DisplayOrder = 3, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 8,  ClientId = 4, Title = "Yedek Parça Deposu", Category = "Depo", PhotoPath = "https://images.unsplash.com/photo-1530046339160-ce3e530c7d2f?w=800", IsPublic = true, DisplayOrder = 4, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 9,  ClientId = 4, Title = "VIP Müşteri Bekleme", Category = "Bekleme Alanı", PhotoPath = "https://images.unsplash.com/photo-1497366216548-37526070297c?w=800", IsPublic = true, DisplayOrder = 5, UploadDate = uploadDate, CreatedDate = uploadDate },

                // ── Boğaz Oto (ClientId=5) ────────────────────────────────────────────────
                new FacilityPhoto { Id = 10, ClientId = 5, Title = "Showroom Girişi", Category = "Dış Görünüm", PhotoPath = "https://images.unsplash.com/photo-1610467595958-1c9d9dc51e8b?w=800", IsPublic = true, DisplayOrder = 1, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 11, ClientId = 5, Title = "Alman Araç Servisi", Category = "Atölye", PhotoPath = "https://images.unsplash.com/photo-1568605117036-5fe5e7bab0b7?w=800", IsPublic = true, DisplayOrder = 2, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 12, ClientId = 5, Title = "Bosch Teşhis İstasyonu", Category = "Ekipman", PhotoPath = "https://images.unsplash.com/photo-1603796846097-bee99e4a601f?w=800", IsPublic = true, DisplayOrder = 3, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 13, ClientId = 5, Title = "Lounge & Bekleme", Category = "Bekleme Alanı", PhotoPath = "https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=800", IsPublic = true, DisplayOrder = 4, UploadDate = uploadDate, CreatedDate = uploadDate },

                // ── Güneş Oto (ClientId=6) ────────────────────────────────────────────────
                new FacilityPhoto { Id = 14, ClientId = 6, Title = "Servis Binası", Category = "Dış Görünüm", PhotoPath = "https://images.unsplash.com/photo-1569356871665-50e2a5e8eb2c?w=800", IsPublic = true, DisplayOrder = 1, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 15, ClientId = 6, Title = "Elektrik Atölyesi", Category = "Atölye", PhotoPath = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800", IsPublic = true, DisplayOrder = 2, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 16, ClientId = 6, Title = "Osiloskop & Test Ekipmanı", Category = "Ekipman", PhotoPath = "https://images.unsplash.com/photo-1581092580497-e0d23cbdf1dc?w=800", IsPublic = true, DisplayOrder = 3, UploadDate = uploadDate, CreatedDate = uploadDate },

                // ── Prestij Auto (ClientId=7) ─────────────────────────────────────────────
                new FacilityPhoto { Id = 17, ClientId = 7, Title = "Prestij Showroom", Category = "Dış Görünüm", PhotoPath = "https://images.unsplash.com/photo-1544636331-e26879cd4d9b?w=800", IsPublic = true, DisplayOrder = 1, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 18, ClientId = 7, Title = "Exotic Car Bay", Category = "Atölye", PhotoPath = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", IsPublic = true, DisplayOrder = 2, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 19, ClientId = 7, Title = "Seramik & PPF Kabini", Category = "Detay Alanı", PhotoPath = "https://images.unsplash.com/photo-1625047509168-a7026f36de04?w=800", IsPublic = true, DisplayOrder = 3, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 20, ClientId = 7, Title = "Müşteri Lounge", Category = "Bekleme Alanı", PhotoPath = "https://images.unsplash.com/photo-1560472354-b33ff0c44a43?w=800", IsPublic = true, DisplayOrder = 4, UploadDate = uploadDate, CreatedDate = uploadDate },
                new FacilityPhoto { Id = 21, ClientId = 7, Title = "Ferrari & Lamborghini Alanı", Category = "Özel Alan", PhotoPath = "https://images.unsplash.com/photo-1592198084033-aade902d1aae?w=800", IsPublic = true, DisplayOrder = 5, UploadDate = uploadDate, CreatedDate = uploadDate },
            };
        }
    }
}
