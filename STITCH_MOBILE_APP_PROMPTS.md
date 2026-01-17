# Stitch - MagicCarRepairAI Mobil Uygulama Tasarım Prompt'ları

> **Proje**: MagicCarRepairAISupported - Oto Servis Yönetim Sistemi  
> **Platform**: React Native / Flutter (Stitch'in desteklediği platform)  
> **Tasarım Stili**: 🎨 **MODERN, KULLANICI DOSTU, PROFESYONEL** - 2024-2025 Trendleri  
> **Renk Paleti**: Modern gradient'ler, glassmorphism, vibrant colors  
> **Özellikler**: Smooth animations, micro-interactions, haptic feedback, accessibility-first

## 🎯 Tasarım Felsefesi

Bu prompt dosyası, **modern ve kullanıcı dostu** bir mobil uygulama tasarımı için hazırlanmıştır. Tüm sayfalar ve bileşenler:

- ✨ **Modern Tasarım Trendleri**: Glassmorphism, neumorphism, gradient backgrounds, bold typography
- 👥 **Kullanıcı Dostu**: Intuitive navigation, clear visual hierarchy, helpful error messages, smart defaults
- 🎭 **Micro-interactions**: Her etkileşimde animasyon, haptic feedback, smooth transitions
- ♿ **Accessibility**: WCAG 2.1 AA uyumlu, screen reader desteği, yüksek kontrast
- 🌙 **Dark Mode**: Tam dark mode desteği, otomatik geçiş
- 📱 **Responsive**: Tüm ekran boyutlarına uyumlu, adaptive layout
- ⚡ **Performance**: 60fps animasyonlar, lazy loading, smooth scrolling

---

## 📱 Genel Tasarım Prensipleri - MODERN & KULLANICI DOSTU

### 🎨 Modern Tasarım Yaklaşımı:
- **Material Design 3** veya **iOS Human Interface Guidelines** uyumlu
- **Glassmorphism** efektleri (şeffaf, bulanık arka planlar)
- **Neumorphism** (yumuşak gölgeler, 3D efektler)
- **Bold Typography** (büyük, okunabilir yazılar)
- **Gradient Backgrounds** (renk geçişleri)
- **Micro-interactions** (dokunmatik geri bildirimler)
- **Smooth Animations** (60fps, yumuşak geçişler)
- **Card-based Design** (kart tabanlı düzen)
- **Spacing & Breathing Room** (geniş boşluklar, nefes alan tasarım)

### 👥 Kullanıcı Dostu Özellikler:
- **Intuitive Navigation** (sezgisel navigasyon)
- **Clear Visual Hierarchy** (net görsel hiyerarşi)
- **Consistent Design Language** (tutarlı tasarım dili)
- **Progressive Disclosure** (aşamalı bilgi sunumu)
- **Contextual Actions** (bağlamsal işlemler)
- **Smart Defaults** (akıllı varsayılanlar)
- **Error Prevention** (hata önleme)
- **Helpful Error Messages** (yardımcı hata mesajları)
- **Onboarding** (ilk kullanım rehberi)
- **Tooltips & Hints** (ipuçları)

### 📐 Teknik Özellikler:
- **Dark Mode** desteği (tam uyumlu)
- **Responsive** tasarım (tüm ekran boyutları)
- **Accessibility** (WCAG 2.1 AA uyumlu, screen reader desteği)
- **Pull-to-Refresh** özelliği
- **Infinite Scroll** veya **Pagination** desteği
- **Loading States** (skeleton screens, shimmer effects)
- **Error States** (hata durumları, retry butonları)
- **Empty States** (boş durumlar, aksiyon önerileri)
- **Offline Support** (çevrimdışı çalışabilme)
- **Haptic Feedback** (dokunsal geri bildirim)

---

## 🔐 1. AUTHENTICATION MODÜLÜ

### 1.1 Login (Giriş) Sayfası

**Prompt:**
```
Tasarım: MODERN, KULLANICI DOSTU ve PROFESYONEL bir giriş sayfası oluştur. 

Görsel Öğeler:
- Üst kısımda büyük, modern bir logo alanı (oto servis teması, glassmorphism efekti ile)
- Gradient arka plan (mavi tonlarından turuncuya geçiş, subtle pattern overlay)
- Email input alanı:
  * Modern, yuvarlatılmış köşeli (12px radius)
  * İkon: @ simgesi (sol tarafta, renkli)
  * Placeholder: "E-posta adresiniz" (hint text, yukarıda)
  * Focus state: Turuncu border, subtle shadow, label yukarı kayar
  * Error state: Kırmızı border, shake animation
- Şifre input alanı:
  * İkon: kilit simgesi (sol tarafta)
  * Placeholder: "Şifreniz"
  * Göz ikonu (sağ tarafta, toggle ile göster/gizle)
  * Password strength indicator (real-time, altında progress bar)
- "Beni Hatırla" checkbox (modern, custom styled)
- "Şifremi Unuttum" linki (sağa hizalı, küçük font, turuncu renk)
- Büyük, yuvarlatılmış köşeli "Giriş Yap" butonu:
  * Turuncu gradient arka plan
  * Beyaz yazı, bold
  * Shadow efekti (elevation)
  * Loading state: Spinner + "Giriş yapılıyor..." text
  * Success animation: Checkmark + scale
- Alt kısımda "Hesabınız yok mu? Kayıt Ol" linki (merkezde, büyük)

Modern Özellikler:
- Smooth animations (fade in, slide up)
- Micro-interactions (buton press feedback, input focus)
- Haptic feedback (buton basımlarında)
- Auto-focus (email input'a otomatik odak)
- Smart keyboard (email için @ klavyesi)
- Biometric authentication seçeneği:
  * Touch ID / Face ID butonu (üstte, büyük ikon)
  * "Hızlı Giriş" metni
- Social login seçenekleri (opsiyonel):
  * Google, Apple, Facebook butonları (küçük, alt kısımda)

Kullanıcı Dostu Özellikler:
- Form validation (real-time, input altında küçük mesajlar)
- Loading state (buton disabled, spinner, progress indicator)
- Error message (kırmızı alert box, üstte, dismissible, shake animation)
- Success state (yeşil checkmark, "Hoş geldiniz!" mesajı)
- Keyboard handling (klavye açıldığında form yukarı kayar, smooth scroll)
- Remember me (otomatik doldurma, güvenli saklama)
- Password visibility toggle (göz ikonu, smooth transition)
- Help text (input altında ipuçları)

Renkler:
- Primary: #FF6B35 (Turuncu, gradient)
- Secondary: #004E89 (Koyu Mavi)
- Background: Linear gradient (#F5F7FA to #E8ECF1)
- Surface: #FFFFFF (Beyaz, %95 opacity, glassmorphism)
- Text: #1A1A1A (Koyu, yüksek kontrast)
- Text Secondary: #6B7280 (Orta gri)
- Error: #EF4444 (Kırmızı, modern)
- Success: #10B981 (Yeşil)
- Border: #E5E7EB (Açık gri, subtle)

Tipografi:
- Başlık: Bold, 32px, #1A1A1A
- Input labels: Medium, 14px, #6B7280 (yukarı kayar animasyon)
- Buton text: Bold, 18px, #FFFFFF
- Link text: Medium, 14px, #FF6B35
- Hint text: Regular, 12px, #9CA3AF

Spacing:
- Geniş padding (24px)
- Elementler arası boşluk (16px)
- Breathing room (nefes alan tasarım)

Animations:
- Page load: Fade in (300ms)
- Input focus: Scale up + border color change (200ms)
- Button press: Scale down (100ms) + haptic
- Error: Shake animation (400ms)
- Success: Checkmark scale + fade (500ms)
```

### 1.2 Register (Kayıt) Sayfası

**Prompt:**
```
Tasarım: Çok adımlı kayıt formu (Step-by-step registration)

Adım 1 - Kişisel Bilgiler:
- Ad input
- Soyad input
- E-posta input
- Telefon input (ülke kodu seçici ile)
- Şifre input (güçlülük göstergesi ile)
- Şifre tekrar input
- "Kullanım Şartları" checkbox
- "Gizlilik Politikası" checkbox

Adım 2 - Kullanıcı Tipi Seçimi:
- Büyük kartlar halinde seçenekler:
  * Müşteri (Customer) - Araç sahibi
  * Personel (Employee) - Servis çalışanı
  * Yönetici (Manager) - Servis yöneticisi
- Her kart: İkon + Başlık + Açıklama

Adım 3 - Doğrulama:
- E-posta doğrulama kodu input (6 haneli)
- "Kod Gönder" butonu
- "Kodu tekrar gönder" linki (30 saniye countdown ile)

Özellikler:
- Progress indicator (üstte 3 adım göstergesi)
- Geri butonu (her adımda)
- Form validation (her adımda)
- Password strength meter (zayıf/orta/güçlü)
- Auto-focus (sıradaki input'a otomatik odaklanma)

Renkler ve Stil: Login sayfası ile aynı
```

### 1.3 Forgot Password (Şifre Sıfırlama) Sayfası

**Prompt:**
```
Tasarım: Minimalist şifre sıfırlama sayfası

Görsel Öğeler:
- Üstte büyük kilit ikonu (merkezde)
- "Şifrenizi mi unuttunuz?" başlığı
- Açıklama metni: "E-posta adresinize şifre sıfırlama linki göndereceğiz"
- E-posta input alanı
- "Şifre Sıfırlama Linki Gönder" butonu
- "Giriş sayfasına dön" linki

Özellikler:
- Email validation
- Success message (yeşil alert: "E-posta gönderildi!")
- Loading state
- Countdown timer (tekrar gönderme için)

Renkler: Login sayfası ile aynı
```

### 1.4 Reset Password (Yeni Şifre Belirleme) Sayfası

**Prompt:**
```
Tasarım: Yeni şifre belirleme formu

Görsel Öğeler:
- "Yeni Şifrenizi Belirleyin" başlığı
- Yeni şifre input (güçlülük göstergesi ile)
- Şifre tekrar input
- Şifre kuralları listesi (checkbox'lar):
  * En az 8 karakter
  * Büyük harf içermeli
  * Küçük harf içermeli
  * Rakam içermeli
  * Özel karakter içermeli
- "Şifreyi Güncelle" butonu

Özellikler:
- Real-time password validation (her kural için checkbox işaretlenir)
- Password strength indicator (progress bar)
- Match validation (şifreler eşleşiyor mu?)

Renkler: Login sayfası ile aynı
```

---

## 📊 2. DASHBOARD MODÜLÜ

### 2.1 Admin/Manager Dashboard

**Prompt:**
```
Tasarım: MODERN, KULLANICI DOSTU dashboard sayfası, kart tabanlı layout, glassmorphism efektleri

Üst Kısım - Hoş Geldiniz Bölümü:
- Kişiselleştirilmiş karşılama:
  * "Merhaba, [İsim]!" (büyük, bold, 28px)
  * Bugünün tarihi (küçük, gri)
  * Hava durumu widget (opsiyonel, modern)
- Quick stats badge (sağ üst):
  * Bugünkü iş emri sayısı
  * Bekleyen onaylar (kırmızı badge)

İstatistik Kartları (4 adet, horizontal scrollable, modern card design):
Her kart:
- Glassmorphism efekti (şeffaf arka plan, blur)
- Gradient border (subtle)
- Shadow (soft elevation)
- Hover/press animation (scale + haptic)

1. Toplam İş Emri
   - Büyük sayı (bold, 36px, gradient text)
   - "İş Emri" label (küçük, gri)
   - Trend göstergesi (↑ %12 artış, yeşil, animated arrow)
   - İkon: clipboard (büyük, renkli, sol üst)
   - Background: Subtle gradient (mavi tonları)

2. Aktif İş Emirleri
   - Büyük sayı
   - "Aktif" label
   - Durum badge (sarı, pulsing animation)
   - İkon: wrench
   - Background: Subtle gradient (turuncu tonları)

3. Bugünkü Gelir
   - Büyük sayı (TL formatında, bold)
   - "Bugün" label
   - Trend göstergesi (animated chart icon)
   - İkon: money (büyük)
   - Background: Subtle gradient (yeşil tonları)

4. Bekleyen Onaylar
   - Büyük sayı
   - "Onay Bekliyor" label
   - Kırmızı badge (pulsing, attention-grabbing)
   - İkon: clock
   - Background: Subtle gradient (kırmızı tonları)

Orta Kısım - Grafikler (Modern, Interactive):
- Gelir-Gider Grafiği (Line Chart, modern design):
  * Son 7 gün (tarih seçici ile değiştirilebilir)
  * Smooth line (bezier curves)
  * Mavi çizgi (gelir, gradient fill)
  * Kırmızı çizgi (gider, gradient fill)
  * Touch ile değer gösterimi (tooltip, animated)
  * Zoom özelliği (pinch to zoom)
  * Legend (interactive, toggle)
  * Animation (line draw, 1s)

- İş Emri Durum Dağılımı (Pie Chart, modern):
  * Renkli dilimler (gradient fills)
  * Her dilimde yüzde ve sayı (hover/touch)
  * Center circle (toplam sayı, büyük)
  * Touch ile detay (modal açılır)
  * Animation (slice by slice, 1.5s)

Alt Kısım - Son Aktiviteler (Modern Timeline):
- Liste formatında (card-based):
  * Her aktivite kartı:
    - Avatar (sol, yuvarlak, border)
    - Aktivite açıklaması (bold başlık, normal açıklama)
    - Zaman (sağ üst, küçük, gri, "2 saat önce")
    - İkon (renkli, sol üst köşe)
    - Background: Beyaz, subtle shadow
    - Swipe action (sağ: detay, sol: arşivle)
  * Divider (subtle, gri çizgi)

Özellikler:
- Pull-to-refresh (smooth animation, haptic feedback)
- Swipe gestures (kartlar arası geçiş, smooth)
- Filter button (floating, tarih aralığı seçimi, modal)
- Export button (PDF/Excel, share sheet)
- Empty state (güzel ikon, "Henüz veri yok" mesajı)
- Loading state (skeleton screens, shimmer effect)
- Error state (retry button, helpful message)

Kullanıcı Dostu Özellikler:
- Smart defaults (bugün, bu hafta, bu ay quick filters)
- Contextual help (tooltips, "?" butonları)
- Progressive disclosure (detaylar expandable)
- Quick actions (swipe gestures)
- Haptic feedback (tüm etkileşimlerde)

Renkler:
- Primary: #FF6B35 (Turuncu, gradient)
- Success: #10B981 (Yeşil, modern)
- Warning: #F59E0B (Sarı/Amber, modern)
- Danger: #EF4444 (Kırmızı, modern)
- Info: #3B82F6 (Mavi, modern)
- Background: #F9FAFB (Açık gri, subtle)
- Card: #FFFFFF (Beyaz, %95 opacity)

Animations:
- Card entrance: Stagger animation (her kart sırayla, 100ms delay)
- Chart animation: Smooth draw (1-1.5s)
- Refresh: Smooth pull + release
- Swipe: Smooth, spring animation
```

### 2.2 Employee Dashboard

**Prompt:**
```
Tasarım: Personel odaklı basitleştirilmiş dashboard

Üst Kısım - Kişisel İstatistikler:
- "Bugünkü İşlerim" kartı
  * Atanan iş emri sayısı
  * Tamamlanan sayısı
  * Kalan sayısı
  * Progress bar

- "Bu Ay Kazanç" kartı
  * Toplam işçilik ücreti
  * Grafik (mini bar chart)

Orta Kısım - Hızlı Erişim Butonları:
- Grid layout (2x2):
  1. "Yeni İş Emri" (büyük buton, turuncu)
  2. "Stok Sorgula" (mavi)
  3. "Müşteri Ara" (yeşil)
  4. "Fotoğraf Çek" (mor)

Alt Kısım - Bugünkü İşlerim:
- Liste formatında:
  * İş emri numarası
  * Müşteri adı
  * Araç plakası
  * Durum badge
  * Öncelik göstergesi
  * Touch ile detay sayfasına git

Özellikler:
- Quick actions (swipe left/right)
- Notification badge (üstte)
- Calendar view toggle (liste/aylık takvim)

Renkler: Admin dashboard ile aynı
```

### 2.3 Customer Dashboard

**Prompt:**
```
Tasarım: Müşteri odaklı dashboard

Üst Kısım - Araç Özeti:
- Ana araç kartı (büyük)
  * Araç fotoğrafı (placeholder)
  * Marka, Model, Yıl
  * Plaka
  * Son bakım tarihi
  * Sonraki bakım km'si
  * Progress bar (km takibi)

Orta Kısım - Hızlı Erişim:
- 4 büyük buton (grid 2x2):
  1. "Aktif İş Emirlerim" (turuncu, badge ile sayı)
  2. "Randevu Al" (mavi)
  3. "Teklif İste" (yeşil)
  4. "Bakım Geçmişi" (mor)

Alt Kısım - Son İşlemler:
- Timeline formatında:
  * Tarih başlığı
  * İş emri kartı:
    - Durum badge
    - Açıklama
    - Tutar
    - Fotoğraf (varsa)
  * Touch ile detay

Özellikler:
- Real-time updates (SignalR)
- Push notifications
- Quick status check

Renkler: Admin dashboard ile aynı
```

---

## 🔧 3. WORK ORDER (İŞ EMRİ) MODÜLÜ

### 3.1 Work Order List (İş Emri Listesi)

**Prompt:**
```
Tasarım: MODERN, KULLANICI DOSTU, filtreleme ve arama özellikli liste sayfası

Üst Kısım - Modern Arama ve Filtreler:
- Arama bar (üstte, sticky, glassmorphism):
  * Modern, yuvarlatılmış (16px radius)
  * Placeholder: "İş emri no, müşteri, plaka ara..."
  * Arama ikonu (sol, renkli)
  * Mikrofon ikonu (sesli arama, sağ)
  * Clear button (X, sağ, görünür sadece yazı varken)
  * Focus state: Turuncu border, subtle shadow
  * Real-time search (debounced, 300ms)

- Filtre butonları (scrollable horizontal, modern chips):
  * Her chip: Yuvarlatılmış, renkli border
  * Active state: Filled, turuncu
  * Inactive: Outline, gri
  * Badge (sayı gösterimi, sağ üst köşe)
  * Smooth scroll, snap to center
  * Chips: Tümü, Aktif, Tamamlandı, Beklemede, İptal, Bugün, Bu Hafta

- Sıralama dropdown (modern, bottom sheet):
  * "Sırala" butonu (sağ üst)
  * Seçenekler: Tarih, Tutar, Öncelik, Müşteri
  * Ascending/Descending toggle

Ana Liste - Modern Kart Formatında:
Her kart (elevated card, glassmorphism):
- Sol tarafta renkli durum çubuğu (dikey, 4px genişlik, gradient)
- İş emri numarası (bold, 18px, gradient text)
- Müşteri bilgisi:
  * Avatar (yuvarlak, border, sol)
  * Müşteri adı (bold, 16px)
  * Telefon (küçük, gri, tıklanabilir)
- Araç bilgisi:
  * Marka, Model (bold)
  * Plaka (badge, renkli)
  * Yıl (küçük, gri)
- Durum badge (renkli, yuvarlak, pulsing animation)
- Öncelik göstergesi:
  * Yıldız ikonları (1-4 yıldız)
  * Renk kodlu (düşük: yeşil, normal: mavi, yüksek: sarı, acil: kırmızı)
- Tarih bilgisi:
  * Giriş tarihi (küçük)
  * Tahmini teslimat (bold, renkli)
  * Geç kaldı mı? (kırmızı uyarı)
- Toplam tutar (sağ alt, büyük, bold, gradient text)
- Quick actions (sağ üst, 3 nokta menü):
  * Hızlı durum güncelle
  * Paylaş
  * PDF
  * Sil

Swipe Actions (modern, smooth):
- Sol swipe: Hızlı durum güncelleme (renkli butonlar)
- Sağ swipe: Sil/Arşivle (kırmızı buton)

Özellikler:
- Pull-to-refresh (smooth, haptic feedback)
- Infinite scroll (smooth loading, skeleton)
- Empty state:
  * Büyük, modern ikon (merkezde)
  * "Henüz iş emri yok" başlığı
  * "Yeni İş Emri Oluştur" butonu (büyük, turuncu)
- Loading skeleton (shimmer effect, 3-4 kart)
- FAB (Floating Action Button):
  * Sağ alt, büyük, turuncu
  * "+" ikonu
  * Shadow (elevation)
  * Press animation (scale + haptic)
  * "Yeni İş Emri" tooltip (hover)

Kullanıcı Dostu Özellikler:
- Smart filters (sık kullanılanlar önerilir)
- Recent searches (arama geçmişi)
- Quick filters (bugün, bu hafta, bu ay)
- Batch actions (çoklu seçim, checkbox mode)
- Sort suggestions (akıllı sıralama önerileri)
- Contextual menu (uzun basma ile)

Renkler:
- Durum renkleri (modern, gradient):
  * Randevu Alındı: #9CA3AF (Gri, modern)
  * Araç Girişi: #3B82F6 (Mavi, modern)
  * İşlemde: #F59E0B (Sarı/Amber, modern)
  * Hazır: #10B981 (Yeşil, modern)
  * Teslim Edildi: #6366F1 (Mor, modern)
  * İptal: #EF4444 (Kırmızı, modern)
- Card background: #FFFFFF (Beyaz)
- Card shadow: Subtle, soft elevation

Animations:
- Card entrance: Stagger (100ms delay)
- Swipe: Spring animation
- Filter change: Smooth transition
- Search: Debounced, smooth
- Pull-to-refresh: Smooth, elastic
```

### 3.2 Work Order Detail (İş Emri Detay)

**Prompt:**
```
Tasarım: Detaylı iş emri görüntüleme sayfası (scrollable)

Üst Kısım - Header (Sticky):
- İş emri numarası (büyük, bold)
- Durum badge
- Öncelik göstergesi
- Menü butonu (3 nokta) - Paylaş, PDF, QR Kod

Tab Navigation (4 tab):
1. Genel Bilgiler
2. Parçalar & İşçilik
3. Timeline
4. Fotoğraflar

Tab 1 - Genel Bilgiler:
- Müşteri Bilgileri Kartı:
  * Avatar + İsim
  * Telefon (tıklanabilir)
  * E-posta (tıklanabilir)
  * Adres

- Araç Bilgileri Kartı:
  * Fotoğraf (varsa)
  * Marka, Model, Yıl
  * Plaka
  * Km bilgisi
  * Yakıt seviyesi (progress bar)

- İş Emri Bilgileri:
  * Giriş tarihi
  * Tahmini teslimat
  * Gerçek teslimat
  * Sorumlu personel
  * Müşteri şikayetleri (expandable)
  * Özel istekler (expandable)
  * Notlar

- Özet Kartı:
  * Ara toplam
  * İndirim
  * KDV
  * Toplam tutar (büyük, bold)
  * Ödenen tutar
  * Kalan tutar

Tab 2 - Parçalar & İşçilik:
- Parçalar Listesi:
  * Her parça:
    - Parça adı
    - Miktar x Birim fiyat
    - Toplam
    - Stok durumu badge

- İşçilik Listesi:
  * Her işçilik:
    - Personel adı
    - İşlem adı
    - Süre (saat)
    - Saat ücreti
    - Toplam

- Toplam özet (alt kısımda sticky)

Tab 3 - Timeline:
- Dikey timeline görünümü:
  * Her olay:
    - Tarih/saat
    - Durum değişikliği
    - Yapan personel
    - Açıklama
    - Fotoğraflar (varsa)
  * Zaman çizgisi (sol tarafta)

Tab 4 - Fotoğraflar:
- Grid layout (3 sütun):
  * Her fotoğraf:
    - Thumbnail
    - Tip badge (Giriş, İşlem, Çıkış)
    - Tarih
  * Touch ile fullscreen görüntüleme
  * Swipe ile gezinme

Alt Kısım - Action Buttons (Sticky):
- Durum güncelle butonu
- Parça ekle butonu
- İşçilik ekle butonu
- Fotoğraf ekle butonu
- Tamamla butonu (yeşil, büyük)

Özellikler:
- Real-time updates (SignalR)
- Share functionality
- PDF export
- QR code generation
- Photo viewer (zoom, pan)

Renkler: Liste sayfası ile aynı
```

### 3.3 Create Work Order (Yeni İş Emri Oluştur)

**Prompt:**
```
Tasarım: Çok adımlı form (wizard style)

Adım 1 - Müşteri & Araç Seçimi:
- Müşteri arama/seçim:
  * Arama bar
  * Son kullanılan müşteriler (chips)
  * Müşteri listesi (avatar + isim)
  * "Yeni müşteri ekle" butonu

- Araç seçimi:
  * Müşterinin araçları listesi
  * "Yeni araç ekle" butonu
  * Araç detayları (marka, model, yıl, plaka)

Adım 2 - İş Emri Bilgileri:
- Öncelik seçimi (radio buttons):
  * Düşük (yeşil)
  * Normal (mavi)
  * Yüksek (sarı)
  * Acil (kırmızı)

- Tahmini teslimat tarihi (date picker)
- Sorumlu personel seçimi (dropdown)
- Müşteri şikayetleri (multi-line text)
- Özel istekler (multi-line text)
- Km bilgisi (number input)
- Yakıt seviyesi (slider, 0-100%)

Adım 3 - Giriş Fotoğrafları:
- Fotoğraf çekme/ekleme:
  * Kamera butonu
  * Galeri butonu
  * Fotoğraf grid (eklenenler)
  * Her fotoğraf için tip seçimi (Giriş, Hasar, vb.)
  * Fotoğraf silme (swipe to delete)

Adım 4 - Özet ve Onay:
- Tüm bilgilerin özeti
- "İş Emri Oluştur" butonu (büyük, turuncu)

Özellikler:
- Progress indicator (üstte 4 adım)
- Geri butonu
- Form validation
- Auto-save (draft kaydetme)
- Camera integration
- Image compression

Renkler: Genel renk paleti
```

### 3.4 Update Work Order Status (Durum Güncelleme)

**Prompt:**
```
Tasarım: Modal/Bottom sheet formatında durum güncelleme

Görsel Öğeler:
- Mevcut durum gösterimi (büyük badge)
- "Yeni Durum Seçin" başlığı
- Durum seçenekleri (liste):
  * Her durum:
    - İkon
    - Durum adı
    - Açıklama
    - Radio button
- Not ekleme alanı (opsiyonel)
- Fotoğraf ekleme (opsiyonel)
- "Durumu Güncelle" butonu

Özellikler:
- Durum geçiş kuralları (bazı durumlar geçersiz)
- Confirmation dialog
- Success animation

Renkler: Durum renkleri
```

---

## 👥 4. CUSTOMER (MÜŞTERİ) MODÜLÜ

### 4.1 Customer List (Müşteri Listesi)

**Prompt:**
```
Tasarım: MODERN, KULLANICI DOSTU, arama ve filtreleme özellikli liste

Üst Kısım - Modern Arama ve Filtreler:
- Arama bar (sticky, glassmorphism):
  * Modern, yuvarlatılmış (16px radius)
  * Placeholder: "Müşteri adı, telefon, e-posta ara..."
  * Arama ikonu (sol, renkli)
  * Mikrofon ikonu (sesli arama, sağ)
  * Clear button (X, sağ, görünür sadece yazı varken)
  * Focus state: Turuncu border, subtle shadow
  * Real-time search (debounced, 300ms)

- Filtre butonları (scrollable horizontal, modern chips):
  * Her chip: Yuvarlatılmış, renkli border
  * Active state: Filled, turuncu
  * Inactive: Outline, gri
  * Badge (sayı gösterimi, sağ üst köşe)
  * Smooth scroll, snap to center
  * Chips: Tümü, VIP, Yeni, Aktif, Bugün, Bu Hafta

- Sıralama dropdown (modern, bottom sheet):
  * "Sırala" butonu (sağ üst)
  * Seçenekler: İsim, Harcama, Son Ziyaret, İş Emri Sayısı
  * Ascending/Descending toggle

Ana Liste - Modern Kart Formatında:
Her kart (elevated card, glassmorphism):
- Sol: Avatar (büyük, yuvarlak, border, gradient background):
  * Harf veya fotoğraf
  * VIP badge (alt sağ, altın renk)
- İsim (bold, 18px, gradient text)
- Telefon (tıklanabilir, renkli, ikon, arama yapar):
  * Quick action (touch: arama, long press: menü)
- E-posta (tıklanabilir, renkli, ikon, mail açar):
  * Quick action (touch: mail, long press: kopyala)
- İstatistikler (modern badges):
  * Toplam iş emri sayısı (badge, mavi)
  * Toplam harcama (badge, yeşil, TL formatında)
  * Son ziyaret (küçük, gri, "2 gün önce")
- VIP badge (varsa, altın renk, pulsing animation)
- Quick actions (sağ üst, 3 nokta menü):
  * Arama
  * Mesaj (WhatsApp)
  * E-posta
  * Düzenle
  * Sil

Swipe Actions (modern, smooth):
- Sol swipe: Hızlı arama (mavi buton)
- Sağ swipe: Sil/Arşivle (kırmızı buton)

Özellikler:
- Pull-to-refresh (smooth, haptic feedback)
- Infinite scroll (smooth loading, skeleton)
- Empty state:
  * Büyük, modern ikon (merkezde)
  * "Henüz müşteri yok" başlığı
  * "Yeni Müşteri Ekle" butonu (büyük, turuncu)
- Loading skeleton (shimmer effect, 3-4 kart)
- FAB (Floating Action Button):
  * Sağ alt, büyük, turuncu
  * "+" ikonu
  * Shadow (elevation)
  * Press animation (scale + haptic)
  * "Yeni Müşteri" tooltip (hover)

Kullanıcı Dostu Özellikler:
- Smart filters (sık kullanılanlar önerilir)
- Recent searches (arama geçmişi)
- Quick filters (bugün, bu hafta, bu ay)
- Batch actions (çoklu seçim, checkbox mode)
- Sort suggestions (akıllı sıralama önerileri)
- Contextual menu (uzun basma ile)
- Quick contact (telefon, mesaj, e-posta direkt)

Renkler: Modern renk paleti (gradient, glassmorphism)

Animations:
- Card entrance: Stagger (100ms delay)
- Swipe: Spring animation
- Filter change: Smooth transition
- Search: Debounced, smooth
- Pull-to-refresh: Smooth, elastic
```

### 4.2 Customer Detail (Müşteri Detay)

**Prompt:**
```
Tasarım: Detaylı müşteri profili

Üst Kısım - Header:
- Büyük avatar (merkezde)
- İsim (bold, büyük)
- Telefon, E-posta (ikonlar ile)
- Menü butonu (düzenle, sil, paylaş)

Tab Navigation (4 tab):
1. Bilgiler
2. Araçlar
3. İş Emirleri
4. İstatistikler

Tab 1 - Bilgiler:
- Kişisel Bilgiler:
  * Ad, Soyad
  * TC Kimlik No
  * Doğum Tarihi
  * Cinsiyet
  * Adres (harita ile göster)

- İletişim:
  * Telefon (tıklanabilir)
  * E-posta (tıklanabilir)
  * WhatsApp (varsa)

- Notlar:
  * Müşteri notları (expandable)
  * Özel bilgiler

Tab 2 - Araçlar:
- Araç listesi (kart formatında):
  * Fotoğraf
  * Marka, Model
  * Plaka
  * Yıl
  * Toplam iş emri sayısı
  * Touch ile araç detayına git

Tab 3 - İş Emirleri:
- İş emri listesi (timeline formatında):
  * Tarih
  * İş emri numarası
  * Durum
  * Tutar
  * Touch ile detay

Tab 4 - İstatistikler:
- Grafikler:
  * Aylık harcama grafiği (bar chart)
  * İş emri sayısı trendi (line chart)
  * Parça kategorisi dağılımı (pie chart)

- Özet kartlar:
  * Toplam harcama
  * Ortalama iş emri tutarı
  * En çok kullanılan parça
  * Son ziyaret

Alt Kısım - Quick Actions:
- "Yeni İş Emri" butonu
- "Araç Ekle" butonu
- "Mesaj Gönder" butonu

Renkler: Genel renk paleti
```

### 4.3 Create/Edit Customer (Müşteri Ekleme/Düzenleme)

**Prompt:**
```
Tasarım: Form sayfası (scrollable)

Form Alanları:
- Kişisel Bilgiler:
  * Ad (required)
  * Soyad (required)
  * TC Kimlik No (validation)
  * Doğum Tarihi (date picker)
  * Cinsiyet (radio buttons)

- İletişim:
  * Telefon (required, format: +90 5XX XXX XX XX)
  * E-posta (required, email validation)
  * WhatsApp (opsiyonel)
  * Adres (multi-line, harita entegrasyonu)

- Ek Bilgiler:
  * Notlar (multi-line)
  * VIP müşteri (toggle switch)
  * İndirim oranı (slider, 0-50%)

- Fotoğraf:
  * Avatar seçimi (kamera/galeri)
  * Crop özelliği

Alt Kısım:
- "Kaydet" butonu (sticky)
- Validation mesajları (real-time)

Özellikler:
- Form validation
- Auto-save (draft)
- Address autocomplete (Google Maps API)
- Phone number formatting

Renkler: Genel renk paleti
```

---

## 🚗 5. VEHICLE (ARAÇ) MODÜLÜ

### 5.1 Vehicle List (Araç Listesi)

**Prompt:**
```
Tasarım: Grid/List toggle ile görüntüleme

Üst Kısım:
- Arama bar (plaka, marka, model)
- Filtreler:
  * Marka
  * Model
  * Yıl
  * Müşteri
- Görünüm toggle (grid/list)

Grid Görünümü:
- 2 sütun grid:
  * Her kart:
    - Araç fotoğrafı (placeholder)
    - Marka, Model
    - Plaka (bold)
    - Müşteri adı
    - Son iş emri tarihi
    - Durum badge

List Görünümü:
- Dikey liste:
  * Her satır:
    - Küçük fotoğraf (sol)
    - Marka, Model, Yıl
    - Plaka
    - Müşteri adı
    - Son iş emri (sağ)

Özellikler:
- Pull-to-refresh
- Infinite scroll
- FAB: "Yeni Araç"
- QR code gösterimi (her araç için)

Renkler: Genel renk paleti
```

### 5.2 Vehicle Detail (Araç Detay)

**Prompt:**
```
Tasarım: Detaylı araç profili

Üst Kısım - Hero Section:
- Büyük araç fotoğrafı (carousel, birden fazla fotoğraf)
- Plaka (büyük, bold, üstte overlay)
- Marka, Model, Yıl
- Menü butonu (QR kod, paylaş, düzenle)

Tab Navigation (3 tab):
1. Bilgiler
2. İş Emirleri
3. Bakım Geçmişi

Tab 1 - Bilgiler:
- Araç Özellikleri:
  * Marka, Model, Yıl
  * Renk
  * Motor No
  * Şasi No
  * Yakıt Tipi
  * Vites Tipi

- Müşteri Bilgileri:
  * Müşteri adı (tıklanabilir)
  * Telefon

- Mevcut Durum:
  * Son km bilgisi
  * Son yakıt seviyesi
  * Son iş emri tarihi
  * Sonraki bakım km'si (progress bar)

Tab 2 - İş Emirleri:
- İş emri listesi (timeline):
  * Tarih
  * İş emri numarası
  * Durum
  * Tutar
  * Touch ile detay

Tab 3 - Bakım Geçmişi:
- Bakım kayıtları:
  * Tarih
  * Bakım tipi
  * Yapılan işlemler
  * Kullanılan parçalar
  * Km bilgisi
  * Tutar

- Bakım takvimi (calendar view):
  * Gelecek bakımlar
  * Geçmiş bakımlar

Alt Kısım:
- "Yeni İş Emri" butonu
- "Bakım Ekle" butonu

Özellikler:
- Photo carousel
- QR code generation
- Maintenance reminders
- Mileage tracking

Renkler: Genel renk paleti
```

---

## 🔩 6. PARTS (PARÇA) MODÜLÜ

### 6.1 Parts List (Parça Listesi)

**Prompt:**
```
Tasarım: Arama ve filtreleme özellikli liste

Üst Kısım:
- Arama bar (parça kodu, ad)
- Barcode scanner butonu (kamera ikonu)
- Filtreler:
  * Kategori (dropdown)
  * Marka tipi (Orijinal/Emsal)
  * Stok durumu (Tümü, Düşük, Stokta Yok)
- Sıralama (ad, fiyat, stok)

Ana Liste:
- Her parça kartı:
  * Sol: Parça ikonu/kategori ikonu
  * Parça kodu (bold)
  * Parça adı
  * Marka (küçük)
  * Stok miktarı (sağ üst, badge)
  * Birim fiyat (sağ alt)
  * Düşük stok uyarısı (kırmızı badge, varsa)
  * Barcode ikonu (varsa)

Özellikler:
- Barcode scanner (kamera ile okuma)
- Quick add to cart (swipe action)
- FAB: "Yeni Parça"
- Stock alert indicators

Renkler:
- Stokta: Yeşil
- Düşük stok: Sarı
- Stokta yok: Kırmızı
```

### 6.2 Part Detail (Parça Detay)

**Prompt:**
```
Tasarım: Detaylı parça bilgisi

Üst Kısım:
- Parça adı (büyük, bold)
- Parça kodu
- Kategori badge
- Menü butonu (düzenle, sil, paylaş)

İçerik:
- Parça Bilgileri:
  * Kategori
  * Marka tipi (Orijinal/Emsal)
  * Marka
  * OEM Numarası
  * Barcode (görsel + string)
  * Birim

- Fiyat Bilgileri:
  * Alış fiyatı
  * Satış fiyatı (büyük, bold)
  * KDV oranı
  * Kar marjı (hesaplanmış)

- Stok Bilgileri:
  * Mevcut stok (büyük sayı)
  * Minimum stok seviyesi
  * Konum (raf no)
  * Stok durumu (progress bar)

- Tedarikçi:
  * Tedarikçi adı (tıklanabilir)
  * İletişim bilgileri

- Stok Hareketleri:
  * Son hareketler listesi:
    - Tarih
    - Tip (Giriş/Çıkış)
    - Miktar
    - Açıklama

Alt Kısım:
- "Stok Güncelle" butonu
- "Fiyat Güncelle" butonu
- "Barcode Oluştur" butonu

Özellikler:
- Barcode görseli (QR code)
- Stock movement history
- Price history (grafik)
- Quick stock update

Renkler: Genel renk paleti
```

### 6.3 Scan Barcode (Barcode Okuma)

**Prompt:**
```
Tasarım: Kamera tabanlı barcode scanner

Görsel Öğeler:
- Fullscreen kamera görünümü
- Ortada tarama çerçevesi (köşeleri vurgulu)
- "Barcode'u çerçeveye hizalayın" metni
- Flash toggle butonu (sağ üst)
- Galeri butonu (manuel giriş için)
- Geri butonu (sol üst)

Özellikler:
- Real-time barcode detection
- Beep sound (başarılı okuma)
- Haptic feedback
- Auto-focus
- Multiple format support (Code128, Code39, EAN13)

Sonuç Ekranı:
- Parça bilgileri (modal):
  * Parça adı
  * Stok durumu
  * Fiyat
  * "Detayları Gör" butonu
  * "İş Emrine Ekle" butonu

Renkler: Koyu tema (kamera için)
```

---

## 👨‍💼 7. EMPLOYEE (PERSONEL) MODÜLÜ

### 7.1 Employee List (Personel Listesi)

**Prompt:**
```
Tasarım: Filtrelenebilir personel listesi

Üst Kısım:
- Arama bar
- Filtreler:
  * Pozisyon (Mekanik, Kaporta, Boyacı, vb.)
  * Durum (Aktif, İzinli, İşten Ayrıldı)
  * Tarih aralığı

Ana Liste:
- Her personel kartı:
  * Sol: Avatar (fotoğraf veya harf)
  * Personel No
  * Ad Soyad (bold)
  * Pozisyon (badge)
  * Telefon (tıklanabilir)
  * E-posta (tıklanabilir)
  * Durum badge
  * Atanan iş emri sayısı
  * Swipe actions (düzenle, sil)

Özellikler:
- Pull-to-refresh
- FAB: "Yeni Personel"
- Quick call/email actions

Renkler: Genel renk paleti
```

### 7.2 Employee Detail (Personel Detay)

**Prompt:**
```
Tasarım: Detaylı personel profili

Üst Kısım - Header:
- Büyük avatar
- Ad Soyad (bold)
- Pozisyon (badge)
- Durum badge
- Menü butonu

Tab Navigation (3 tab):
1. Bilgiler
2. İş Emirleri
3. Performans

Tab 1 - Bilgiler:
- Kişisel:
  * Personel No
  * TC Kimlik No
  * Ad, Soyad
  * Doğum Tarihi
  * Telefon
  * E-posta
  * Adres

- İş Bilgileri:
  * Pozisyon
  * Maaş
  * İşe Giriş Tarihi
  * Durum
  * Uzmanlık Alanları (chips)

Tab 2 - İş Emirleri:
- Atanan iş emirleri listesi
- İstatistikler:
  * Toplam iş emri
  * Tamamlanan
  * Devam eden
  * Toplam gelir

Tab 3 - Performans:
- Grafikler:
  * Aylık iş emri sayısı
  * Ortalama tamamlanma süresi
  * Müşteri memnuniyeti (rating)
- Özet kartlar

Alt Kısım:
- "İş Emri Ata" butonu
- "Maaş Öde" butonu

Renkler: Genel renk paleti
```

---

## 💰 8. INVOICE (FATURA) MODÜLÜ

### 8.1 Invoice List (Fatura Listesi)

**Prompt:**
```
Tasarım: Filtrelenebilir fatura listesi

Üst Kısım:
- Arama bar
- Filtreler:
  * Durum (Beklemede, Ödendi, Vadesi Geçmiş)
  * Tarih aralığı
  * Müşteri

Ana Liste:
- Her fatura kartı:
  * Fatura numarası (bold)
  * Müşteri adı
  * Tarih
  * Vade tarihi (kırmızı, vadesi geçmişse)
  * Tutar (büyük, bold)
  * Durum badge
  * Ödeme durumu (progress bar)
  * QR kod ikonu
  * Swipe actions (ödeme, PDF, e-posta gönder)

Özellikler:
- Pull-to-refresh
- FAB: "Yeni Fatura"
- Overdue indicator (vadesi geçmişler için)

Renkler:
- Ödendi: Yeşil
- Beklemede: Sarı
- Vadesi Geçmiş: Kırmızı
```

### 8.2 Invoice Detail (Fatura Detay)

**Prompt:**
```
Tasarım: Fatura görüntüleme ve yönetim

Üst Kısım:
- Fatura numarası
- Durum badge
- Menü butonu (PDF, QR, paylaş, e-posta)

İçerik:
- Müşteri Bilgileri:
  * Ad, Soyad
  * Adres
  * Telefon, E-posta

- Fatura Bilgileri:
  * Tarih
  * Vade tarihi
  * Fatura tipi

- Kalemler:
  * Liste formatında:
    - Açıklama
    - Miktar
    - Birim fiyat
    - Toplam

- Özet:
  * Ara toplam
  * İndirim
  * KDV
  * Toplam (büyük, bold)
  * Ödenen
  * Kalan

- Ödeme Geçmişi:
  * Ödeme listesi:
    - Tarih
    - Tutar
    - Yöntem

Alt Kısım:
- "Ödeme Al" butonu
- "PDF İndir" butonu
- "E-posta Gönder" butonu
- "QR Kod Göster" butonu

Özellikler:
- PDF preview
- QR code display
- Payment integration
- Email sending

Renkler: Genel renk paleti
```

---

## 📅 9. APPOINTMENT (RANDEVU) MODÜLÜ

### 9.1 Appointment List (Randevu Listesi)

**Prompt:**
```
Tasarım: Takvim ve liste görünümü

Üst Kısım:
- Tarih seçici (date picker)
- Görünüm toggle (Takvim/Liste)

Takvim Görünümü:
- Monthly calendar:
  * Randevu olan günler (nokta ile işaretli)
  * Bugün (vurgulu)
  * Seçili gün (mavi arka plan)
  * Randevu sayısı (her günde)

- Alt kısımda seçili günün randevuları:
  * Liste formatında
  * Saat, müşteri, araç, durum

Liste Görünümü:
- Randevu kartları:
  * Tarih/Saat (sol, büyük)
  * Müşteri adı
  * Araç bilgisi
  * Randevu tipi
  * Durum badge
  * Swipe actions (iptal, ertele, onayla)

Özellikler:
- Pull-to-refresh
- FAB: "Yeni Randevu"
- Notification reminders
- Calendar sync

Renkler: Genel renk paleti
```

### 9.2 Create Appointment (Randevu Oluştur)

**Prompt:**
```
Tasarım: Randevu oluşturma formu

Form Alanları:
- Müşteri Seçimi:
  * Arama/seçim
  * "Yeni müşteri" butonu

- Araç Seçimi:
  * Müşterinin araçları
  * "Yeni araç" butonu

- Randevu Bilgileri:
  * Tarih (date picker)
  * Saat (time picker)
  * Süre (dropdown: 30dk, 1sa, 2sa, vb.)
  * Randevu tipi (dropdown)
  * Açıklama (multi-line)

- Personel Atama:
  * Personel seçimi (dropdown)
  * Müsaitlik kontrolü (yeşil/kırmızı)

- Müsaitlik Kontrolü:
  * Seçilen tarih/saat için müsaitlik durumu
  * Çakışan randevular (varsa uyarı)

Alt Kısım:
- "Randevu Oluştur" butonu

Özellikler:
- Availability check
- Conflict detection
- Auto-suggestions (AI destekli)

Renkler: Genel renk paleti
```

---

## 💬 10. CHAT (MESAJLAŞMA) MODÜLÜ

### 10.1 Chat List (Sohbet Listesi)

**Prompt:**
```
Tasarım: WhatsApp benzeri sohbet listesi

Ana Liste:
- Her sohbet kartı:
  * Sol: Avatar (müşteri/personel)
  * İsim (bold)
  * Son mesaj (kısaltılmış)
  * Zaman (sağ üst)
  * Okunmamış mesaj sayısı (badge, kırmızı)
  * Durum göstergesi (çevrimiçi/çevrimdışı)

Özellikler:
- Real-time updates (SignalR)
- Unread count
- Last message preview
- Search functionality
- FAB: "Yeni Sohbet"

Renkler: Genel renk paleti
```

### 10.2 Chat Detail (Sohbet Detay)

**Prompt:**
```
Tasarım: Mesajlaşma ekranı

Üst Kısım - Header:
- Avatar + İsim
- Durum (çevrimiçi/çevrimdışı)
- Menü butonu (profil, arama, bilgi)

Mesaj Alanı:
- Mesaj balonları:
  * Gönderen (sağ, mavi):
    - Mesaj metni
    - Zaman (sağ alt)
    - Okundu işareti (çift tik)
  * Alıcı (sol, gri):
    - Mesaj metni
    - Zaman (sol alt)

- Mesaj tipleri:
  * Metin
  * Fotoğraf (thumbnail + fullscreen)
  * Dosya (ikon + ad)
  * Konum (harita preview)

Alt Kısım - Input:
- Mesaj yazma alanı (expandable)
- Ekleme butonu (fotoğraf, dosya, konum)
- Gönder butonu

Özellikler:
- Real-time messaging (SignalR)
- Typing indicator
- Read receipts
- Image preview
- File sharing
- Location sharing
- Message search

Renkler:
- Gönderen: #118AB2 (Mavi)
- Alıcı: #E5E5E5 (Açık Gri)
```

---

## 🔔 11. NOTIFICATION (BİLDİRİM) MODÜLÜ

### 11.1 Notification List (Bildirim Listesi)

**Prompt:**
```
Tasarım: Bildirim listesi

Üst Kısım:
- "Tümünü Okundu İşaretle" butonu
- Filtreler (Tümü, Okunmamış, Okundu)

Ana Liste:
- Her bildirim kartı:
  * Sol: İkon (renkli, tipine göre)
  * Başlık (bold)
  * Mesaj (kısaltılmış)
  * Zaman (sağ üst)
  * Okunmamış göstergesi (mavi nokta)
  * Swipe to delete
  * Touch ile ilgili sayfaya git

Bildirim Tipleri:
- İş Emri (mavi ikon)
- Fatura (yeşil ikon)
- Randevu (turuncu ikon)
- Mesaj (mor ikon)
- Sistem (gri ikon)

Özellikler:
- Pull-to-refresh
- Mark as read
- Delete
- Deep linking (bildirimden ilgili sayfaya git)

Renkler: Tip bazlı renkler
```

---

## 📊 12. REPORTS (RAPOR) MODÜLÜ

### 12.1 Reports List (Rapor Listesi)

**Prompt:**
```
Tasarım: Rapor kategorileri ve seçenekleri

Kategoriler (grid 2x2):
1. İş Emri Raporları
   * İkon: clipboard
   * Alt kategoriler:
     - Günlük özet
     - Aylık özet
     - Personel bazlı
     - Müşteri bazlı

2. Finansal Raporlar
   * İkon: money
   * Alt kategoriler:
     - Gelir-Gider
     - Kar-Zarar
     - Nakit akışı
     - Vergi raporu

3. Stok Raporları
   * İkon: box
   * Alt kategoriler:
     - Stok durumu
     - Parça kullanımı
     - Düşük stok

4. Müşteri Raporları
   * İkon: users
   * Alt kategoriler:
     - Müşteri analizi
     - Sadakat analizi
     - Churn analizi

Her Rapor Sayfası:
- Tarih aralığı seçici
- Filtreler
- Grafikler (chart.js benzeri)
- Tablo görünümü
- Export butonları (PDF, Excel)

Özellikler:
- Date range picker
- Chart interactions
- Export functionality
- Share functionality

Renkler: Genel renk paleti
```

---

## ⭐ 13. RATINGS (DEĞERLENDİRME) MODÜLÜ

### 13.1 Ratings List (Değerlendirme Listesi)

**Prompt:**
```
Tasarım: Müşteri değerlendirmeleri listesi

Üst Kısım:
- Ortalama puan (büyük, bold, yıldızlı)
- Toplam değerlendirme sayısı
- Puan dağılımı (bar chart):
  * 5 yıldız: X adet
  * 4 yıldız: X adet
  * vb.

Ana Liste:
- Her değerlendirme kartı:
  * Üst: Avatar + İsim
  * Yıldız puanı (görsel yıldızlar)
  * Tarih
  * Yorum (expandable)
  * İş emri bilgisi (tıklanabilir)
  * Yanıt (varsa, gri arka plan)
  * "Yanıtla" butonu (admin için)

Özellikler:
- Filter by rating (1-5 yıldız)
- Sort by date/rating
- Reply functionality
- Moderation (onayla/reddet)

Renkler: Genel renk paleti
```

---

## 🏢 14. CLIENT PORTAL (TAMİRHANE YÖNETİMİ) MODÜLÜ

### 14.1 Public Profile Management (Halka Açık Profil Yönetimi)

**Prompt:**
```
Tasarım: Profil yönetim sayfası

Üst Kısım:
- Profil görünümü toggle (Açık/Kapalı)
- Önizleme butonu (halka açık görünümü göster)

Tab Navigation (5 tab):
1. Genel Bilgiler
2. Portföy
3. Sertifikalar
4. Tesis Fotoğrafları
5. Ekip

Tab 1 - Genel Bilgiler:
- Logo yükleme (crop özelliği)
- Firma adı
- Hakkımızda (rich text editor)
- Çalışma saatleri (haftalık takvim)
- Hizmetler (chips, ekle/çıkar)
- İletişim:
  * Telefon
  * E-posta
  * Website
  * Adres
- Sosyal medya:
  * Facebook
  * Instagram
  * Twitter
  * LinkedIn

Tab 2 - Portföy:
- Portföy listesi:
  * Her öğe:
    - Fotoğraf (öncesi/sonrası)
    - Başlık
    - Açıklama
    - Durum (Onay Bekliyor, Yayında)
    - Düzenle/Sil butonları
- "Yeni Portföy Ekle" butonu

Tab 3 - Sertifikalar:
- Sertifika listesi:
  * Her sertifika:
    - Sertifika adı
    - Veren kurum
    - Tarih
    - Geçerlilik
    - Dosya (PDF/image)
    - Düzenle/Sil
- "Yeni Sertifika Ekle" butonu

Tab 4 - Tesis Fotoğrafları:
- Fotoğraf grid (3 sütun):
  * Her fotoğraf:
    - Thumbnail
    - Kategori
    - Başlık
    - Düzenle/Sil
- "Yeni Fotoğraf Ekle" butonu

Tab 5 - Ekip:
- Ekip üyeleri listesi:
  * Her üye:
    - Fotoğraf
    - Ad Soyad
    - Pozisyon
    - Uzmanlıklar
    - Halka açık mı? (toggle)
    - Düzenle

Özellikler:
- Image upload/crop
- Rich text editor
- Drag & drop sorting
- Preview functionality

Renkler: Genel renk paleti
```

---

## 👤 15. CUSTOMER PORTAL (MÜŞTERİ PANELİ) MODÜLÜ

### 15.1 My Work Orders (İş Emirlerim)

**Prompt:**
```
Tasarım: Müşterinin kendi iş emirleri

Üst Kısım:
- Filtreler (Aktif, Tamamlandı, Tümü)
- Arama

Ana Liste:
- İş emri kartları:
  * İş emri numarası
  * Araç bilgisi
  * Durum (büyük badge)
  * Tarih
  * Tutar
  * Real-time durum güncellemesi (SignalR)
  * Touch ile detay

Özellikler:
- Real-time updates
- Status notifications
- Photo gallery
- Timeline view

Renkler: Genel renk paleti
```

### 15.2 My Work Order Detail (İş Emrim Detay)

**Prompt:**
```
Tasarım: Müşteri için iş emri detay sayfası

Üst Kısım:
- İş emri numarası
- Durum badge (büyük)
- Real-time indicator (canlı güncelleme)

Tab Navigation (4 tab):
1. Genel
2. Parçalar & İşçilik
3. Timeline
4. Fotoğraflar

Tab 1 - Genel:
- Araç bilgileri
- Müşteri şikayetleri
- Yapılan işlemler
- Özet (tutar bilgileri)

Tab 2 - Parçalar & İşçilik:
- Kullanılan parçalar listesi
- İşçilik listesi
- Toplam tutar

Tab 3 - Timeline:
- Durum değişiklikleri
- Real-time updates
- Fotoğraflar (timeline'da)

Tab 4 - Fotoğraflar:
- Fotoğraf galerisi
- Fullscreen görüntüleme

Alt Kısım:
- "Onayla" butonu (teslimatta)
- "Reddet" butonu (teslimatta)
- "Soru Sor" butonu (chat)

Özellikler:
- Real-time updates (SignalR)
- Photo viewer
- Approval workflow
- Chat integration

Renkler: Genel renk paleti
```

### 15.3 Request Quote (Teklif İste)

**Prompt:**
```
Tasarım: Teklif talebi oluşturma formu

Form Alanları:
- Araç Seçimi:
  * Mevcut araçlar
  * "Yeni araç ekle" butonu

- Sorun Açıklaması:
  * Multi-line text
  * Hasar tipi (dropdown)
  * Aciliyet (radio buttons)

- Fotoğraflar:
  * Fotoğraf ekleme (kamera/galeri)
  * Grid görünümü
  * Açıklama ekleme (her fotoğraf için)

- İstenen Tarih:
  * Tarih aralığı seçici

- Notlar:
  * Ek bilgiler

Alt Kısım:
- "Teklif İste" butonu

Özellikler:
- Photo upload
- Multiple service selection
- Auto-save draft

Renkler: Genel renk paleti
```

### 15.4 My Quotes (Tekliflerim)

**Prompt:**
```
Tasarım: Gelen teklifler listesi

Ana Liste:
- Her teklif kartı:
  * Teklif talebi bilgisi
  * Servis adı
  * Teklif tutarı (büyük, bold)
  * Tahmini süre
  * Garanti bilgisi
  * Durum (Beklemede, Kabul Edildi, Reddedildi)
  * "Detayları Gör" butonu
  * "Kabul Et" / "Reddet" butonları

Özellikler:
- Comparison view (teklifleri karşılaştır)
- Accept/Reject actions
- Notification updates

Renkler: Genel renk paleti
```

### 15.5 Maintenance History (Bakım Geçmişi)

**Prompt:**
```
Tasarım: Araç bakım geçmişi

Üst Kısım:
- Araç seçici (dropdown)
- Filtreler (Tarih, Bakım tipi)

Ana Liste - Timeline Formatında:
- Her bakım kaydı:
  * Tarih (büyük, bold)
  * Bakım tipi (badge)
  * Yapılan işlemler (liste)
  * Kullanılan parçalar (liste)
  * Km bilgisi
  * Tutar
  * Fotoğraflar (varsa)
  * Touch ile detay

Özellikler:
- Calendar view
- Statistics (toplam harcama, bakım sıklığı)
- Export (PDF)

Renkler: Genel renk paleti
```

---

## 🌐 16. PUBLIC CLIENT (HALKA AÇIK PROFİL) MODÜLÜ

### 16.1 Public Profile View (Halka Açık Profil Görünümü)

**Prompt:**
```
Tasarım: Halka açık tamirhane profili (müşteri görünümü)

Üst Kısım - Hero Section:
- Büyük logo
- Firma adı (bold, büyük)
- Kısa açıklama
- Ortalama puan (yıldızlı)
- Toplam değerlendirme sayısı
- "Randevu Al" butonu (büyük, turuncu)
- "Ara" butonu (mavi)

Tab Navigation (6 tab):
1. Hakkımızda
2. Hizmetler
3. Portföy
4. Sertifikalar
5. Tesisler
6. Ekip
7. Değerlendirmeler

Tab 1 - Hakkımızda:
- Logo
- Detaylı açıklama (rich text)
- Çalışma saatleri (haftalık tablo)
- İletişim bilgileri:
  * Telefon (tıklanabilir)
  * E-posta (tıklanabilir)
  * Adres (harita ile)
  * Website (tıklanabilir)
- Sosyal medya linkleri (ikonlar)

Tab 2 - Hizmetler:
- Hizmet listesi (chips veya kartlar):
  * Her hizmet:
    - İkon
    - Hizmet adı
    - Kısa açıklama

Tab 3 - Portföy:
- Portföy grid (2 sütun):
  * Her öğe:
    - Öncesi/Sonrası fotoğrafları (slider)
    - Başlık
    - Açıklama
    - Tarih
    - Touch ile fullscreen

Tab 4 - Sertifikalar:
- Sertifika grid:
  * Her sertifika:
    - Sertifika adı
    - Veren kurum
    - Tarih
    - Dosya (PDF/image, tıklanabilir)

Tab 5 - Tesisler:
- Fotoğraf grid (3 sütun):
  * Kategorilere göre:
    - Atölye
    - Bekleme salonu
    - Ofis
    - Dış görünüm
  * Touch ile fullscreen

Tab 6 - Ekip:
- Ekip grid (2 sütun):
  * Her üye:
    - Fotoğraf
    - Ad Soyad
    - Pozisyon
    - Uzmanlıklar (chips)
    - Biyografi (expandable)

Tab 7 - Değerlendirmeler:
- Ortalama puan (büyük, yıldızlı)
- Puan dağılımı (bar chart)
- Değerlendirme listesi:
  * Her değerlendirme:
    - Avatar + İsim (anonim)
    - Yıldız puanı
    - Yorum
    - Tarih
    - Yanıt (varsa)

Alt Kısım - Sticky:
- "Randevu Al" butonu
- "Ara" butonu
- "Mesaj Gönder" butonu

Özellikler:
- No authentication required
- Share functionality
- Map integration
- Photo gallery
- Rating display

Renkler: Genel renk paleti
```

---

## 🎨 17. GENEL TASARIM ELEMENTLERİ

### 17.0 Renk Ağırlığı Analizi ve Öneriler

**Prompt:**
```
RENK AĞIRLIĞI ANALİZİ - Oto Servis Yönetim Sistemi İçin:

📊 Sektör ve Kullanıcı Profili:
- Profesyonel ve güvenilir görünüm gerektirir
- Teknik ve iş odaklı bir alan
- Günlük operasyonel kullanım (yoğun kullanım)
- Çoklu kullanıcı rolleri: Yönetici, Personel, Müşteri
- Veri yoğun ekranlar (tablolar, listeler, formlar)

🎯 ÖNERİLEN RENK AĞIRLIĞI: Mavi Ağırlıklı + Turuncu Vurgular

Renk Dağılımı (ÖNERİLEN):
- %55 Mavi (Primary): Güven, profesyonellik, sakinlik
  * Kullanım: Arka planlar, navigasyon, kartlar, genel UI
  * Renkler: #3B82F6 → #2563EB (Gradient)
  
- %20 Turuncu (Accent): Enerji, aksiyon, dikkat çekme
  * Kullanım: Butonlar, aksiyonlar, vurgular, FAB
  * Renkler: #FF6B35 → #FF8C42 (Gradient)
  
- %20 Gri/Beyaz (Neutral): Temizlik, minimal, okunabilirlik
  * Kullanım: Metinler, borderlar, dividerlar, arka planlar
  * Renkler: #F9FAFB (Background), #FFFFFF (Surface)
  
- %5 Status Colors: Başarı/Hata durumları
  * Yeşil: #10B981 (Başarı, onaylar)
  * Kırmızı: #EF4444 (Hatalar, uyarılar)
  * Amber: #F59E0B (Bekleyen durumlar)

Neden Bu Kombinasyon?
1. Güvenilir: Mavi, oto servis sektöründe güven verir
2. Profesyonel: İşletmeler için ciddi görünüm
3. Enerjik: Turuncu vurgular dinamizm katıyor
4. Göz Dostu: Uzun süreli kullanım için ideal
5. Modern: 2024-2025 trendlerine uygun
6. Dengeli: Hem işletme hem müşteri için uygun

🌈 Renk Psikolojisi (Oto Servis İçin):

Mavi (Primary):
- Güven: Müşteriler güvenilir bir servis görür
- Profesyonellik: Ciddi, iş odaklı
- Sakinlik: Uzun süreli kullanım için ideal
- Teknoloji: Modern, teknolojik görünüm

Turuncu (Accent):
- Enerji: Hızlı, dinamik iş akışı
- Dikkat: Önemli aksiyonları vurgular
- Sıcaklık: Dostane, yakın görünüm
- Aksiyon: Butonlar, FAB, vurgular

Gri/Beyaz (Neutral):
- Temizlik: Profesyonel, düzenli
- Okunabilirlik: Metinler için ideal
- Minimal: Dikkat dağıtmaz
- Versatil: Her renkle uyumlu

Yeşil (Success):
- Başarı: Tamamlanan işler, onaylar
- Pozitif: İyi haberler, başarılar

Kırmızı (Error):
- Uyarı: Kritik durumlar, hatalar
- Dikkat: Acil aksiyon gerektiren durumlar

🎨 Ekran Tipine Göre Renk Kullanımı:

1. Dashboard / Ana Sayfa:
   - Arka Plan: Açık Gri (#F9FAFB) veya Beyaz
   - Kartlar: Beyaz, subtle shadow
   - İstatistikler: Mavi gradient kartlar
   - Aksiyon Butonları: Turuncu gradient
   - Grafikler: Mavi, Turuncu, Yeşil (çeşitli)

2. Liste Ekranları (İş Emri, Müşteri, vb.):
   - Arka Plan: Açık Gri (#F9FAFB)
   - Kartlar: Beyaz, subtle border
   - Durum Badge'leri: Renkli (Mavi, Turuncu, Yeşil, Kırmızı)
   - Aksiyon Butonları: Turuncu
   - FAB: Turuncu gradient

3. Form Ekranları:
   - Arka Plan: Beyaz
   - Input Fields: Beyaz, gri border, focus: Turuncu
   - Submit Butonu: Turuncu gradient
   - Cancel Butonu: Gri, outline

4. Detay Ekranları:
   - Header: Mavi gradient (güven)
   - İçerik: Beyaz kartlar
   - Aksiyonlar: Turuncu butonlar
   - Status: Renkli badge'ler

5. Müşteri Portalı (Public):
   - Daha Canlı: Turuncu ağırlıklı
   - Güven: Mavi vurgular
   - Modern: Gradient'ler, glassmorphism

🌙 Dark Mode Renk Dağılımı:
- Arka Plan: Koyu gri (#0F172A)
- Surface: Orta koyu (#1E293B)
- Primary (Mavi): Açık mavi tonları (#60A5FA)
- Accent (Turuncu): Yumuşak turuncu (#FB923C)
- Text: Açık gri (#F8FAFC)
- Not: Dark mode'da renk oranları aynı kalmalı, sadece tonlar değişmeli

📱 Platform Farkları:
- iOS: Daha yumuşak renkler, subtle gradient'ler, glassmorphism efektleri
- Android: Daha canlı renkler, Material Design 3 uyumlu, bold gradient'ler

✅ SONUÇ:
En İyi Seçenek: Mavi Ağırlıklı (%55) + Turuncu Vurgular (%20)

Bu kombinasyon:
- Profesyonel ve güvenilir
- Modern ve enerjik
- Göz dostu
- Hem işletme hem müşteri için uygun
- Sektöre uygun

Alternatif: Eğer daha genç ve dinamik bir görünüm isteniyorsa, Turuncu Ağırlıklı seçenek de kullanılabilir, özellikle müşteri portalı için.
```

---

### 17.1 Color Palette (Renk Paleti) - MODERN

**Prompt:**
```
MODERN Renk Paleti Tanımı (2024-2025 Trendleri):

Primary Colors (Gradient Support):
⚠️ ÖNEMLİ: Renk ağırlığı analizine göre, Mavi Primary, Turuncu Accent olmalı!

- Primary: #3B82F6 (Modern Mavi) - Ana renk, güven ve profesyonellik
  * Gradient: #3B82F6 → #2563EB (Mavi tonları)
  * Kullanım: Navigasyon, arka planlar, kartlar, genel UI (%55 ağırlık)
  
- Accent: #FF6B35 (Turuncu) - Aksiyon butonları, vurgular, FAB
  * Gradient: #FF6B35 → #FF8C42 (Turuncu tonları)
  * Kullanım: Butonlar, aksiyonlar, vurgular (%20 ağırlık)
  
- Secondary: #6366F1 (Modern Indigo) - İkincil butonlar, linkler
  * Gradient: #6366F1 → #4F46E5 (Indigo tonları)
- Accent: #10B981 (Modern Yeşil) - Başarı mesajları, onaylar
  * Gradient: #10B981 → #059669 (Yeşil tonları)
- Warning: #F59E0B (Modern Amber) - Uyarılar, bekleyen durumlar
  * Gradient: #F59E0B → #D97706 (Amber tonları)
- Danger: #EF4444 (Modern Kırmızı) - Hatalar, iptaller
  * Gradient: #EF4444 → #DC2626 (Kırmızı tonları)
- Info: #6366F1 (Modern Indigo) - Bilgilendirme
  * Gradient: #6366F1 → #4F46E5 (Indigo tonları)

Neutral Colors (Modern Grays):
- Background: #F9FAFB (Açık Gri, subtle)
- Surface: #FFFFFF (Beyaz, %95 opacity for glassmorphism)
- Text Primary: #1A1A1A (Koyu, yüksek kontrast)
- Text Secondary: #6B7280 (Orta Gri, modern)
- Text Tertiary: #9CA3AF (Açık Gri, subtle)
- Border: #E5E7EB (Açık Gri, subtle)
- Divider: #D1D5DB (Gri, modern)

Status Colors (Modern, Vibrant):
- Success: #10B981 (Yeşil, modern)
- Warning: #F59E0B (Amber, modern)
- Error: #EF4444 (Kırmızı, modern)
- Info: #3B82F6 (Mavi, modern)
- Neutral: #9CA3AF (Gri, modern)

Dark Mode Colors (Modern, Accessible):
- Background: #0F172A (Koyu, modern slate)
- Surface: #1E293B (Orta Koyu, modern)
- Text Primary: #F8FAFC (Beyaz, yüksek kontrast)
- Text Secondary: #CBD5E1 (Açık Gri, modern)
- Text Tertiary: #94A3B8 (Orta Gri, subtle)
- Border: #334155 (Koyu Gri, subtle)
- Divider: #475569 (Gri, modern)

Glassmorphism Colors:
- Glass Background: rgba(255, 255, 255, 0.1) (Light mode)
- Glass Background: rgba(0, 0, 0, 0.2) (Dark mode)
- Glass Border: rgba(255, 255, 255, 0.2)
- Blur: 20px (backdrop-filter)

Gradient Presets:
- Primary Gradient: linear-gradient(135deg, #FF6B35 0%, #FF8C42 100%)
- Success Gradient: linear-gradient(135deg, #10B981 0%, #059669 100%)
- Info Gradient: linear-gradient(135deg, #3B82F6 0%, #2563EB 100%)
- Danger Gradient: linear-gradient(135deg, #EF4444 0%, #DC2626 100%)
- Sunset Gradient: linear-gradient(135deg, #FF6B35 0%, #3B82F6 100%)
- Ocean Gradient: linear-gradient(135deg, #3B82F6 0%, #10B981 100%)
```

### 17.2 Typography (Tipografi) - MODERN

**Prompt:**
```
MODERN Tipografi Sistemi (2024-2025):

Font Family (Modern, Readable):
- Primary: 'Inter' (Android, modern, clean)
- Primary: 'SF Pro Display' (iOS, native, modern)
- Fallback: 'Roboto' (Android), 'San Francisco' (iOS)
- Monospace: 'JetBrains Mono' (kod, teknik bilgiler)

Font Sizes (Scalable, Accessible):
- H1 (Hero): 40px, Bold, Line Height: 48px
- H2 (Başlık): 32px, Bold, Line Height: 40px
- H3 (Alt Başlık): 24px, SemiBold, Line Height: 32px
- H4 (Bölüm): 20px, SemiBold, Line Height: 28px
- Body Large: 18px, Regular, Line Height: 28px
- Body: 16px, Regular, Line Height: 24px
- Body Small: 14px, Regular, Line Height: 20px
- Caption: 12px, Regular, Line Height: 16px
- Tiny: 10px, Regular, Line Height: 14px

Font Weights (Modern Scale):
- Black: 900 (çok nadir, hero text)
- Bold: 700 (başlıklar, vurgular)
- SemiBold: 600 (alt başlıklar, önemli text)
- Medium: 500 (butonlar, labels)
- Regular: 400 (body text, normal)
- Light: 300 (dekoratif, nadir)

Letter Spacing (Tracking):
- H1: -0.5px (tight, modern)
- H2: -0.25px (tight)
- H3: 0px (normal)
- Body: 0px (normal)
- Small: 0.25px (loose, readability)
- Caption: 0.5px (loose)

Text Styles (Modern):
- Gradient Text: linear-gradient(135deg, #FF6B35 0%, #3B82F6 100%)
- Shadow Text: text-shadow: 0 2px 4px rgba(0,0,0,0.1)
- Outline Text: -webkit-text-stroke: 1px color
- Truncate: ellipsis, max 2 lines

Accessibility:
- Minimum font size: 14px (body)
- Contrast ratio: 4.5:1 (WCAG AA)
- Line height: 1.5x font size (readability)
- Letter spacing: adjustable (accessibility settings)
```

### 17.3 Components (Bileşenler) - MODERN

**Prompt:**
```
MODERN Standart Bileşenler (2024-2025):

Buttons (Modern, Interactive):
- Primary Button:
  * Turuncu gradient arka plan (#FF6B35 → #FF8C42)
  * Beyaz yazı, bold, 16px
  * Yuvarlatılmış köşeler (12px radius)
  * Padding: 16px vertical, 24px horizontal
  * Shadow (elevation 4)
  * Press animation (scale 0.95, haptic feedback)
  * Loading state (spinner + text)
  * Disabled state (opacity 0.5, no interaction)

- Secondary Button:
  * Beyaz arka plan, %95 opacity (glassmorphism)
  * Turuncu border (2px), turuncu yazı
  * Yuvarlatılmış köşeler (12px)
  * Padding: 16px vertical, 24px horizontal
  * Press animation (scale 0.95)

- Text Button:
  * Sadece yazı, renkli (turuncu)
  * No background, no border
  * Press animation (opacity 0.7)

- Icon Button:
  * Sadece ikon, yuvarlak (40px)
  * Renkli arka plan (subtle)
  * Press animation (scale 0.9)

- FAB (Floating Action Button):
  * Büyük, yuvarlak (56px)
  * Turuncu gradient
  * Shadow (elevation 8)
  * "+" ikonu, beyaz
  * Press animation (scale 0.9)

Input Fields (Modern, Accessible):
- Text Input:
  * Beyaz arka plan, %95 opacity (glassmorphism)
  * Gri border (1px), yuvarlatılmış köşeler (12px)
  * Padding: 16px
  * Label (floating, animated)
  * Icon support (sol/sağ)
  * Focus state: Turuncu border (2px), subtle shadow
  * Error state: Kırmızı border, shake animation
  * Success state: Yeşil border, checkmark icon
  * Helper text (altında, küçük)
  * Character counter (sağ alt, küçük)

- Search Input:
  * Arama ikonu (sol)
  * Clear button (sağ, görünür sadece yazı varken)
  * Mikrofon ikonu (sağ, sesli arama)

Cards (Modern, Elevated):
- Standard Card:
  * Beyaz arka plan, %95 opacity (glassmorphism)
  * Shadow (elevation 2, soft)
  * Yuvarlatılmış köşeler (16px)
  * Padding: 20px
  * Press animation (scale 0.98, haptic)

- Elevated Card:
  * Shadow (elevation 4, medium)
  * Hover/press: elevation 8

- Glassmorphism Card:
  * Şeffaf arka plan (rgba(255,255,255,0.1))
  * Blur (backdrop-filter: blur(20px))
  * Border (rgba(255,255,255,0.2))

Badges (Modern, Vibrant):
- Standard Badge:
  * Yuvarlatılmış (8px radius)
  * Renkli arka plan (gradient)
  * Beyaz yazı, bold, 12px
  * Padding: 4px 8px

- Pulsing Badge:
  * Pulsing animation (attention-grabbing)
  * Kırmızı (uyarılar için)

- Icon Badge:
  * İkon + text
  * Renkli arka plan

Loading States (Modern, Smooth):
- Skeleton Screens:
  * Shimmer effect (animated gradient)
  * Placeholder shapes (card, text, image)
  * Smooth animation (2s loop)

- Spinner:
  * Circular progress (modern, smooth)
  * Renkli (turuncu gradient)
  * Size variants (small, medium, large)

- Progress Bars:
  * Linear progress (gradient fill)
  * Smooth animation
  * Percentage indicator

Empty States (Modern, Helpful):
- Layout:
  * Büyük ikon (merkezde, 120px, renkli)
  * Başlık (bold, 20px, gri)
  * Açıklama (regular, 14px, gri, 2-3 satır)
  * Aksiyon butonu (turuncu, büyük)
  * Illustration (opsiyonel, modern)

Error States (Modern, Recoverable):
- Layout:
  * Büyük ikon (merkezde, kırmızı)
  * Başlık (bold, 20px)
  * Açıklama (regular, 14px, helpful message)
  * Retry butonu (turuncu, büyük)
  * Alternative action (opsiyonel)

Success States (Modern, Celebratory):
- Layout:
  * Checkmark animation (scale + fade)
  * Başlık (bold, 20px, yeşil)
  * Açıklama (regular, 14px)
  * Confetti animation (opsiyonel)

Animations (Modern, Smooth):
- Entrance: Fade in (300ms)
- Exit: Fade out (200ms)
- Scale: 0.95 → 1.0 (200ms, spring)
- Slide: Smooth slide (300ms, ease-out)
- Shake: Error shake (400ms)
- Pulse: Attention pulse (1s loop)
```

---

## 📱 18. NAVIGATION & LAYOUT

### 18.1 Bottom Navigation (Alt Navigasyon)

**Prompt:**
```
Alt Navigasyon Çubuğu (5 sekme):

1. Ana Sayfa (Home)
   - İkon: ev
   - Badge: bildirim sayısı (varsa)

2. İş Emirleri (Work Orders)
   - İkon: clipboard
   - Badge: aktif iş emri sayısı

3. Müşteriler (Customers) / Araçlar (Vehicles)
   - İkon: users / car
   - Rol bazlı değişir

4. Stok (Parts) / Randevular (Appointments)
   - İkon: box / calendar
   - Rol bazlı değişir

5. Profil (Profile)
   - İkon: user
   - Avatar (küçük, üstte)

Özellikler:
- Sticky (her zaman görünür)
- Badge support
- Active state (renkli vurgu)
- Haptic feedback

Renkler:
- Active: Turuncu (#FF6B35)
- Inactive: Gri (#9B9B9B)
- Background: Beyaz
```

### 18.2 Drawer Navigation (Yan Menü)

**Prompt:**
```
Yan Menü (Drawer):

Üst Kısım:
- Avatar (büyük)
- İsim (bold)
- E-posta (küçük)
- Rol badge

Menü Öğeleri:
- Dashboard (ikon: chart)
- İş Emirleri (ikon: clipboard)
- Müşteriler (ikon: users)
- Araçlar (ikon: car)
- Parçalar (ikon: box)
- Personel (ikon: user-group)
- Faturalar (ikon: invoice)
- Ödemeler (ikon: money)
- Randevular (ikon: calendar)
- Raporlar (ikon: chart-bar)
- Ayarlar (ikon: settings)
- Çıkış (ikon: logout, kırmızı)

Alt Kısım:
- Versiyon bilgisi
- Destek linki

Özellikler:
- Swipe to open/close
- Active state
- Badge support
- Divider sections

Renkler: Genel renk paleti
```

---

## ⚙️ 19. SETTINGS (AYARLAR) MODÜLÜ

### 19.1 Settings Page (Ayarlar Sayfası)

**Prompt:**
```
Tasarım: Ayarlar listesi

Bölümler:

1. Hesap:
   - Profil bilgileri
   - Şifre değiştir
   - E-posta değiştir
   - Telefon değiştir

2. Bildirimler:
   - Push notifications (toggle)
   - E-posta bildirimleri (toggle)
   - SMS bildirimleri (toggle)
   - Bildirim sesi (dropdown)
   - Bildirim titreşimi (toggle)

3. Görünüm:
   - Tema (Açık/Koyu/Otomatik)
   - Dil (TR/EN/AR)
   - Font boyutu (slider)

4. Uygulama:
   - Önbelleği temizle
   - Veri kullanımı
   - Versiyon bilgisi
   - Hakkında

5. Güvenlik:
   - İki faktörlü kimlik doğrulama (toggle)
   - Oturum yönetimi
   - Giriş geçmişi

6. Çıkış:
   - Çıkış yap butonu (kırmızı)

Özellikler:
- Toggle switches
- Dropdowns
- Sliders
- Confirmation dialogs

Renkler: Genel renk paleti
```

---

## 🎯 20. ÖZEL ÖZELLİKLER

### 20.1 Offline Mode (Çevrimdışı Mod)

**Prompt:**
```
Çevrimdışı Mod Desteği:

Özellikler:
- Veri senkronizasyonu (background sync)
- Offline indicator (üstte banner)
- Local storage (önbellek)
- Queue system (çevrimdışı işlemler)
- Conflict resolution (çakışma çözümü)

Görsel:
- Çevrimdışı banner (sarı, üstte)
- Sync indicator (spinner)
- "Senkronize ediliyor..." mesajı
```

### 20.2 Search (Arama)

**Prompt:**
```
Global Arama Özelliği:

Arama Bar (üstte, sticky):
- Placeholder: "Ara..."
- Mikrofon ikonu (sesli arama)
- Filtre butonu

Arama Sonuçları:
- Kategorilere göre gruplandırılmış:
  * İş Emirleri
  * Müşteriler
  * Araçlar
  * Parçalar
  * Personel
- Her sonuç:
  * İkon
  * Başlık
  * Alt başlık
  * Vurgulanmış arama terimi
  * Touch ile ilgili sayfaya git

Özellikler:
- Real-time search
- Search history
- Recent searches
- Voice search (opsiyonel)
- Filter by category
```

### 20.3 Quick Actions (Hızlı İşlemler)

**Prompt:**
```
Hızlı İşlemler (Floating Action Button - FAB):

Ana FAB (sağ alt):
- "+" ikonu
- Touch ile menü açılır

Hızlı İşlemler Menüsü:
- Yeni İş Emri (büyük, turuncu)
- Yeni Müşteri (mavi)
- Yeni Araç (yeşil)
- Yeni Parça (mor)
- Yeni Randevu (turuncu)

Özellikler:
- Expandable menu
- Animation (scale + fade)
- Haptic feedback
- Quick access

Renkler: Her işlem için farklı renk
```

---

## 📝 SON NOTLAR

### Genel Tasarım Kuralları (MODERN & KULLANICI DOSTU):
1. **Consistency**: Tüm sayfalarda tutarlı, modern tasarım dili
2. **Accessibility**: WCAG 2.1 AA uyumlu, screen reader desteği
3. **Performance**: Smooth 60fps animasyonlar, lazy loading
4. **Responsive**: Tüm ekran boyutlarına uyumlu, adaptive layout
5. **Localization**: TR/EN/AR dil desteği, RTL desteği
6. **Dark Mode**: Tam dark mode desteği, otomatik geçiş
7. **Offline First**: Çevrimdışı çalışabilme, sync indicator
8. **Real-time**: SignalR entegrasyonu, live updates
9. **Push Notifications**: Bildirim desteği, rich notifications
10. **Analytics**: Kullanım analitiği, heatmaps
11. **Micro-interactions**: Her etkileşimde animasyon, haptic feedback
12. **Progressive Enhancement**: Temel özellikler her zaman çalışır
13. **Error Recovery**: Hata durumlarında retry, helpful messages
14. **Smart Defaults**: Akıllı varsayılanlar, öneriler
15. **Contextual Help**: Bağlamsal yardım, tooltips, onboarding

### Teknik Detaylar:
- **Platform**: React Native veya Flutter
- **State Management**: Redux/MobX veya Provider/Bloc
- **Navigation**: React Navigation veya Flutter Navigation
- **API**: RESTful API (mevcut backend)
- **Real-time**: SignalR client
- **Storage**: AsyncStorage veya Hive
- **Image**: React Native Image Picker veya Flutter Image Picker
- **Camera**: React Native Camera veya Flutter Camera
- **Charts**: Victory Charts veya Flutter Charts
- **Maps**: React Native Maps veya Flutter Maps

---

## 🎨 TASARIM ÖRNEKLERİ & MODERN TRENDLER

### Referans Uygulamalar (Modern, Kullanıcı Dostu):
- **WhatsApp**: Mesajlaşma arayüzü, smooth animations
- **Uber**: Harita ve takip, real-time updates
- **Instagram**: Fotoğraf galerisi, swipe gestures
- **Banking Apps**: Güvenli, profesyonel, güvenilir tasarım
- **E-commerce Apps**: Ürün listesi, filtreleme, arama
- **Notion**: Temiz, minimal, kullanıcı dostu
- **Linear**: Modern, hızlı, smooth interactions
- **Stripe Dashboard**: Profesyonel, data visualization

### Modern Tasarım Trendleri (2024-2025):
- **Glassmorphism**: Şeffaf, bulanık arka planlar, modern depth
- **Neumorphism**: Yumuşak gölgeler, 3D efektler, subtle
- **Bold Typography**: Büyük, okunabilir, vurgulu yazılar
- **Micro-interactions**: Her etkileşimde animasyon, haptic feedback
- **Gradient Backgrounds**: Renk geçişleri, modern, canlı
- **Card-based Design**: Kart tabanlı, nefes alan düzen
- **Spacing & Breathing Room**: Geniş boşluklar, minimal clutter
- **Dark Mode First**: Dark mode öncelikli tasarım
- **Accessibility First**: Erişilebilirlik odaklı
- **Skeleton Screens**: Loading states, shimmer effects
- **Smooth Animations**: 60fps, spring animations
- **Contextual Actions**: Bağlamsal işlemler, smart defaults

### Kullanıcı Dostu Prensipler:
- **Progressive Disclosure**: Bilgiyi aşamalı göster
- **Error Prevention**: Hataları önle, doğrulama yap
- **Helpful Messages**: Yardımcı, açıklayıcı mesajlar
- **Clear Feedback**: Her aksiyonda geri bildirim
- **Consistent Patterns**: Tutarlı desenler, öğrenilebilir
- **Forgiving Design**: Hataları düzeltilebilir yap
- **Onboarding**: İlk kullanım rehberi, tooltips
- **Empty States**: Boş durumlar için aksiyon önerileri
- **Loading States**: Yükleme durumları, progress göstergeleri
- **Success States**: Başarı animasyonları, confirmation

---

Bu prompt'ları Stitch'e vererek her sayfa için ayrı ayrı tasarım oluşturabilirsiniz. Her prompt, sayfanın işlevselliğini, görsel öğelerini, renklerini ve özelliklerini detaylı olarak açıklamaktadır.
