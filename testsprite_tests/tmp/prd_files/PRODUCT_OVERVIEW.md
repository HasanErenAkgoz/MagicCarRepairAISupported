# Magic Car Repair — Ürün Tanıtım Dokümanı

> **Araç sahiplerini doğru servisle buluşturan, servisleri dijitale taşıyan AI destekli platform.**

---

## Sorun

Türkiye'de 20 milyonun üzerinde araç var. Her araç sahibi eninde sonunda şu soruyla karşılaşıyor:

- *"Arabam ne kadar tutacak, soyulacak mıyım?"*
- *"Hangi servise gideyim, güvenir miyim?"*
- *"İş emrim ne aşamada, neden kimse aramıyor?"*

Servis sahipleri içinse:
- İş emirleri kağıtta ya da eski bir Excel'de
- Müşteri takibi telefon hafızasında
- Teklif, fatura, randevu → hepsi ayrı sistemlerde (ya da hiç sistemsiz)

**Magic Car Repair, her iki tarafın sorununu tek platformda çözüyor.**

---

## Ürün Nedir?

Magic Car Repair; araç sahipleri ve oto servisler için geliştirilmiş, **AI destekli, iki taraflı bir mobil platformdur.**

```
┌─────────────────────────────────────────────────────────────┐
│                     MAGIC CAR REPAIR                        │
│                                                             │
│   👤 Araç Sahibi           🔧 Servis (Oto Tamirci)          │
│   ─────────────────        ──────────────────────           │
│   • AI ile ön tanı         • İş emri yönetimi               │
│   • Fiyat karşılaştırma    • Müşteri & araç kaydı           │
│   • Teklif isteme          • Randevu takvimi                 │
│   • Süreç takibi           • Stok & parça takibi            │
│   • Canlı mesajlaşma       • Fatura & tahsilat              │
│   • Bakım geçmişi          • Çalışan yönetimi               │
└─────────────────────────────────────────────────────────────┘
```

---

## Ana Özellikler

### 1. AI Destekli Ön Tanı

Araç sahibi fotoğraf çeker, şikayetini yazar. Yapay zeka:
- Olası arızaları ve hasar tespitini yapar
- Parça bazlı tahmini maliyet aralığı üretir
- Kaza analizinde panel bazlı hasar haritası çıkarır
- Servis envanterlerinde ilgili parçaları arar ve stok durumunu gösterir

**Kullanıcı servise gitmeden önce ne bekleyeceğini bilir.**

---

### 2. Gerçek Fiyat Karşılaştırma

AI tanısının ardından kullanıcı "Gerçek Fiyat Karşılaştır" diyebilir. Platform:
- Yakın çevredeki servislerin envanterini tarar
- Parça bazlı fiyat teklifleri listeler
- Servisleri **güven skoru** (rating, tamamlanma oranı, yanıt hızı) ile sıralar
- Seçilen servislere tek tıkla teklif talebi gönderir

---

### 3. Teklif & İş Emri Akışı

```
Araç Sahibi                          Servis
────────────────                     ──────────────
Teklif Talebi Oluştur          →     Teklif Geldi (bildirim)
                               ←     Teklif Gönder
Teklifi Onayla                 →     İş Emri Açılır
                               ←     Aşama Güncellemeleri
                               ←     Fatura Kesildi
Faturayı İncele & Öde          →     Tahsilat Tamamlandı
```

Her adımda push notification. Araç sahibi servisi aramak zorunda kalmaz.

---

### 4. Servis Operasyon Paneli

Servis yöneticisi tek ekrandan tüm operasyonu görür:

| Modül | Özellikler |
|-------|-----------|
| **İş Emirleri** | 11 aşamalı durum takibi, fotoğraf kayıtları (giriş/çıkış/hasar), müşteri onay akışı |
| **Müşteriler** | Araç geçmişi, iletişim, toplu import |
| **Envanter** | Parça ekleme/düzenleme, minimum stok uyarısı, OEM kodu eşleştirme |
| **Randevular** | Takvim görünümü, müşteri & araç ile ilişkilendirilmiş |
| **Faturalar** | Fatura oluştur, tahsilat takibi, ödeme geçmişi |
| **Çalışanlar** | Rol bazlı yetki yönetimi, 2FA desteği |
| **Sigorta** | Hasar dosyası oluşturma, poliçe yönetimi |
| **Raporlar** | Gelir analizi, verimlilik özeti |

---

### 5. Canlı Mesajlaşma

Araç sahibi ile servis arasında gerçek zamanlı chat. Ek ücret yok, WhatsApp'a gerek yok. İletişim platform içinde kalır, kayıt altında olur.

---

### 6. Sadakat Sistemi

Servis müşterilerine puan kazandırabilir. Araç sahipleri biriktirdikleri puanları indirim olarak kullanabilir. Müşteri bağlılığı artar.

---

## Farklılaştırıcılar

### AI Tanı Derinliği
Piyasadaki uygulamaların büyük çoğunluğu "servis bul" ya da "randevu al"dan öteye geçmiyor. Magic Car Repair, kullanıcıya **servise gitmeden önce bilgi gücü** veriyor.

### Cross-Tenant Envanter Eşleştirme
AI parça önerisini yaparken sadece bir servisin envanterini değil, **platforma kayıtlı tüm servislerin** stoğunu tarıyor. Araç sahibi hangi serviste parça var, kaç liraya — bunu görebiliyor.

### Güven Skoru
Servisler gizli bir güven skoru ile sıralanıyor:
- Ortalama kullanıcı puanı
- Son 90 günlük iş emri tamamlama oranı
- Ortalama teklif yanıt süresi

**Yeni müşteri servisi seçerken körü körüne seçmiyor.**

### Multi-Tenant Mimari
Her servis tamamen izole bir tenant. Veriler birbirine karışmaz. Büyük servis zincirleri de aynı platformda çalışabilir.

---

## Kullanıcı Profilleri

### Araç Sahibi
> *"Servise gidince ne kadar tutacağını bilmek isteyorum. Beni soymasınlar."*

- Mobil uygulama, ücretsiz
- Fotoğraf çek → AI tanı → fiyat karşılaştır → teklif al

### Bağımsız Oto Servis Sahibi
> *"Kağıt kalemle iş takip etmekten bıktım, WhatsApp mesajlarında kayboluyorum."*

- Aylık abonelik modeli
- Tüm operasyon tek uygulamada

### Zincir Servis / Filo Yönetimi
> *"10 şubemi tek yerden yönetmek istiyorum."*

- Multi-tenant yönetici paneli
- Şube bazlı raporlama

---

## Teknik Altyapı

| Katman | Teknoloji |
|--------|-----------|
| Mobil | React Native (Expo), TypeScript |
| Backend | .NET 10, Clean Architecture, CQRS |
| AI | OpenAI GPT-4o (vision + text) |
| Realtime | SignalR (chat & bildirimler) |
| Cache | Redis |
| Dil Desteği | Türkçe & İngilizce |
| Güvenlik | JWT + Refresh Token, 2FA, HTTPS |

---

## Gelir Modeli

```
Araç Sahibi          → Ücretsiz (platform büyütür)
Servis (Temel)       → Aylık ₺X  (iş emri + müşteri + randevu)
Servis (Pro)         → Aylık ₺XX (+ AI tanı + envanter + fatura)
Servis (Kurumsal)    → Özel fiyat (multi-branch + API entegrasyon)
```

Ek gelir: Teklif akışında servis başına başarı komisyonu (opsiyonel).

---

## Mevcut Durum

- Mobil uygulama: **iOS & Android — geliştirme tamamlandı**
- Backend API: **Production-ready, .NET Clean Architecture**
- AI entegrasyonu: **Aktif (OpenAI GPT-4o vision)**
- Dil desteği: **TR & EN**
- Test: **Beta aşaması**

---

## Özet

Magic Car Repair, oto servis sektörünün dijitalleşme açığını kapatırken araç sahiplerine de bilgi asimetrisi avantajı veriyor. Piyasadaki tek ürün; **AI ön tanı + fiyat karşılaştırma + servis operasyon yönetimini** tek platformda birleştiriyor.

> **Araç sahibi güçlenir. Servis dijitalleşir. Platform büyür.**
