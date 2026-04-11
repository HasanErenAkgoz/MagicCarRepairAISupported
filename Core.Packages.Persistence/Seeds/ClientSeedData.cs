using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class ClientSeedData
    {
        public static List<Client> GetClients()
        {
            var workingHoursWeekdays = """{"Pazartesi":"08:00-18:00","Salı":"08:00-18:00","Çarşamba":"08:00-18:00","Perşembe":"08:00-18:00","Cuma":"08:00-18:00","Cumartesi":"09:00-14:00","Pazar":"Kapalı"}""";
            var workingHours247 = """{"Pazartesi":"07:00-20:00","Salı":"07:00-20:00","Çarşamba":"07:00-20:00","Perşembe":"07:00-20:00","Cuma":"07:00-20:00","Cumartesi":"08:00-18:00","Pazar":"09:00-15:00"}""";
            var workingHoursLux = """{"Pazartesi":"09:00-18:00","Salı":"09:00-18:00","Çarşamba":"09:00-18:00","Perşembe":"09:00-18:00","Cuma":"09:00-18:00","Cumartesi":"10:00-16:00","Pazar":"Kapalı"}""";

            return new List<Client>
            {
                // ── ID 1 & 2: internal/dev tenants ──────────────────────────────────────
                new Client
                {
                    Id = 1,
                    Name = "Demo Oto Servis",
                    Code = "DEMO001",
                    Description = "Demo servis — geliştirici testi için",
                    ContactEmail = "demo@example.com",
                    ContactPhone = "+90 555 123 4567",
                    Address = "Atatürk Cad. No:1, Kadıköy, İstanbul",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 40.9833,
                    Longitude = 29.0333,
                    AboutUs = "Demo servis tenantı.",
                    Services = """["Motor Bakımı","Lastik","Fren"]""",
                    WorkingHours = workingHoursWeekdays,
                    SocialMediaLinks = """{}""",
                    SubscriptionStartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },
                new Client
                {
                    Id = 2,
                    Name = "Test Oto Servis",
                    Code = "TEST001",
                    Description = "Test tenant — geliştirme ortamı için",
                    ContactEmail = "test@example.com",
                    ContactPhone = "+90 555 987 6543",
                    Address = "Bağcılar Mah. 15. Sok No:5, Bağcılar, İstanbul",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 41.0369,
                    Longitude = 28.8561,
                    AboutUs = "Test servis tenantı.",
                    Services = """["Bakım","Yedek Parça"]""",
                    WorkingHours = workingHoursWeekdays,
                    SocialMediaLinks = """{}""",
                    SubscriptionStartDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },

                // ── ID 3: Yıldız Oto Tamir & Bakım — Kadıköy ────────────────────────────
                new Client
                {
                    Id = 3,
                    Name = "Yıldız Oto Tamir & Bakım",
                    Code = "YILDIZ001",
                    Description = "Kadıköy'ün köklü tamir ustası — 25 yıllık deneyim",
                    ContactEmail = "info@yildizoto.com.tr",
                    ContactPhone = "+90 216 348 7755",
                    Address = "Moda Cad. No:42, Kadıköy, İstanbul",
                    TaxOfficeNo = "1234567890",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 40.9886,
                    Longitude = 29.0289,
                    WebsiteUrl = "https://yildizoto.com.tr",
                    AboutUs = "1999 yılından bu yana Kadıköy'de hizmet veren Yıldız Oto Tamir, müşteri memnuniyetini her zaman ön planda tutar. Avrupa ve Japon marka araçlarda uzman ekibimiz ile hızlı ve güvenilir servis sunuyoruz. Modern teşhis cihazlarımız sayesinde arızanızı kısa sürede tespit ediyor, orijinal yedek parça garantisiyle tamir ediyoruz.",
                    WorkingHours = workingHoursWeekdays,
                    Services = """["Motor Revizyonu","Şanzıman Bakımı","Fren Sistemi","Süspansiyon & Rot Balans","Elektrik & Elektronik","Periyodik Bakım","Yağ Değişimi","Lastik Montaj","Klima Bakımı","Egzoz Sistemi"]""",
                    SocialMediaLinks = """{"Instagram":"https://instagram.com/yildizoto","Facebook":"https://facebook.com/yildizoto"}""",
                    SubscriptionStartDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },

                // ── ID 4: Anadolu Oto Merkezi — Ümraniye ───────────────────────────────
                new Client
                {
                    Id = 4,
                    Name = "Anadolu Oto Merkezi",
                    Code = "ANADOLU001",
                    Description = "Ümraniye'nin en büyük filo servis merkezi",
                    ContactEmail = "servis@anadoluoto.com.tr",
                    ContactPhone = "+90 216 523 9900",
                    Address = "Alemdağ Cad. No:118, Ümraniye, İstanbul",
                    TaxOfficeNo = "9876543210",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 41.0163,
                    Longitude = 29.1122,
                    WebsiteUrl = "https://anadoluoto.com.tr",
                    AboutUs = "Anadolu Oto Merkezi, 2008 yılından itibaren Ümraniye ve çevresinde araç sahiplerine ve filo şirketlerine kapsamlı oto servis hizmeti vermektedir. 1500 m² kapalı alanda 12 lift, son teknoloji araç teşhis sistemleri ve 18 kişilik uzman kadrosuyla faaliyet gösteriyoruz. Her marka ve modele özel bakım paketlerimizle hem bireysel hem kurumsal müşterilerimize en iyi deneyimi sunuyoruz.",
                    WorkingHours = workingHours247,
                    Services = """["Tüm Marka Araç Servisi","Filo Yönetimi","Motor & Şanzıman","Kaporta & Boya","Ön Cam Değişimi","Periyodik Bakım","Yedek Parça Satışı","Araç Muayene Hazırlığı","Sigorta Hasarı","Klima Dolumu & Bakımı","LPG Montaj & Bakım","Lastik & Jant"]""",
                    SocialMediaLinks = """{"Instagram":"https://instagram.com/anadoluotomerkezi","Facebook":"https://facebook.com/anadoluotomerkezi","YouTube":"https://youtube.com/anadoluoto"}""",
                    SubscriptionStartDate = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },

                // ── ID 5: Boğaz Oto Servis — Beşiktaş ──────────────────────────────────
                new Client
                {
                    Id = 5,
                    Name = "Boğaz Oto Servis",
                    Code = "BOGAZ001",
                    Description = "Beşiktaş'ta Alman araçları için uzman servis",
                    ContactEmail = "info@bogazoto.com.tr",
                    ContactPhone = "+90 212 261 4433",
                    Address = "Barbaros Blv. No:77, Beşiktaş, İstanbul",
                    TaxOfficeNo = "4561237890",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 41.0425,
                    Longitude = 29.0070,
                    WebsiteUrl = "https://bogazoto.com.tr",
                    AboutUs = "BMW, Mercedes-Benz, Volkswagen, Audi ve Porsche başta olmak üzere tüm Alman marka araçlarda uzmanlaşmış Boğaz Oto Servis, 2012'den bu yana Beşiktaş'ta hizmet vermektedir. Yetkili servis kalitesinde ama çok daha uygun fiyatlarla bakım, onarım ve tuning hizmetleri sunuyoruz. TEXA ve Bosch teşhis sistemleri ile aracınızı fabrika ayarlarında tutuyoruz.",
                    WorkingHours = workingHoursLux,
                    Services = """["BMW Uzmanı","Mercedes-Benz Uzmanı","VW & Audi Uzmanı","Porsche Bakım","Motor Yazılım Güncelleme","Dizel Enjektör Temizleme","DSG Şanzıman Bakımı","Turbo & Kompresör","Elektronik Arıza Tespit","Periyodik Bakım"]""",
                    SocialMediaLinks = """{"Instagram":"https://instagram.com/bogazoto","Twitter":"https://twitter.com/bogazoto"}""",
                    SubscriptionStartDate = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },

                // ── ID 6: Güneş Oto Elektrik & Mekanik — Gaziosmanpaşa ───────────────
                new Client
                {
                    Id = 6,
                    Name = "Güneş Oto Elektrik & Mekanik",
                    Code = "GUNES001",
                    Description = "Elektrik ve elektronik sorunlarında Gaziosmanpaşa'nın adresi",
                    ContactEmail = "gunesoto@gmail.com",
                    ContactPhone = "+90 212 594 8822",
                    Address = "Fevzi Çakmak Mah. Güneş Sok. No:8, Gaziosmanpaşa, İstanbul",
                    TaxOfficeNo = "3217654098",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 41.0701,
                    Longitude = 28.9108,
                    WebsiteUrl = null,
                    AboutUs = "Güneş Oto Elektrik & Mekanik, 2015'ten bu yana araç elektriği ve mekanik onarım konularında uzmanlaşmış bir aile işletmesidir. Akü ve şarj sistemi, enjeksiyon arızaları, kısa devre tespiti gibi zorlu elektrik problemlerini sahip olduğumuz osiloskop ve multimetre setleri ile hızla çözüyoruz. Fiyatlarımız her zaman şeffaf ve belgelidir.",
                    WorkingHours = workingHoursWeekdays,
                    Services = """["Araç Elektriği","Akü Değişimi","Marş & Alternatör","Enjeksiyon Sistemi","Kısa Devre Tespiti","Far & Aydınlatma","OBD Okuma & Sıfırlama","Alarm & Kilit Sistemi","Android Multimedya Montaj","Genel Mekanik Bakım"]""",
                    SocialMediaLinks = """{"Instagram":"https://instagram.com/gunesotoelektrik"}""",
                    SubscriptionStartDate = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },

                // ── ID 7: Prestij Auto — Maslak ────────────────────────────────────────
                new Client
                {
                    Id = 7,
                    Name = "Prestij Auto",
                    Code = "PRESTIJ001",
                    Description = "Lüks ve premium araçlerde konsept servis deneyimi",
                    ContactEmail = "info@prestijautomotive.com",
                    ContactPhone = "+90 212 286 0001",
                    Address = "Büyükdere Cad. No:201 Kat:2, Maslak, İstanbul",
                    TaxOfficeNo = "7890123456",
                    IsActive = true,
                    IsPublicProfileEnabled = true,
                    Latitude = 41.1087,
                    Longitude = 29.0194,
                    WebsiteUrl = "https://prestijautomotive.com",
                    AboutUs = "Prestij Auto, Maslak'ta lüks ve üst segment araçlere özel konsept bir servis deneyimi sunar. Ferrari, Lamborghini, McLaren, Bentley ve diğer egzotik markalar dahil her premium araca dokunuyoruz. İsviçre saatçiliği titizliğiyle çalışan uzman ekibimiz, 4000 m²'lik tesisimizde aracınıza kapsamlı bakım ve restorasyon hizmetleri sunar. Randevu bazlı çalışıyoruz, her araç tek birer kişiye atanıyor.",
                    WorkingHours = workingHoursLux,
                    Services = """["Egzotik & Lüks Araç Servisi","Motor Sporları Hazırlık","Performans Tuning","Karoser Restorasyonu","Seramik Kaplama","PPF Folyo","Detaylı İç Temizlik","OEM & Aftermarket Parça","Özel Boya Uygulamaları","VIP Transfer Hizmeti"]""",
                    SocialMediaLinks = """{"Instagram":"https://instagram.com/prestijautomotive","Facebook":"https://facebook.com/prestijautomotive","YouTube":"https://youtube.com/prestijautomotive","Twitter":"https://twitter.com/prestijautomotive"}""",
                    SubscriptionStartDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    SubscriptionEndDate = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedDate = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    Status = Status.Active
                },
            };
        }
    }
}

