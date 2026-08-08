# MagicCarRepair — Canlı Ekip ve Production Takip Kaydı

> Bu dosya ekip için tek durum kaynağıdır. Her agent tesliminden sonra kendi kaydını günceller; Yönetici son karar, öncelik ve release durumunu günceller.

## Release durumu

- **Karar:** NO-GO
- **Son güncelleme:** 2026-08-09
- **Release sahibi:** CTO / Yönetici
- **GO için zorunlu:** GitHub CI'da PostgreSQL servisli tam kanıtın remote üzerinde alınması, chat ekleri için kaynak-özel yetki ve mobilde kalan yüksek önemli bağımlılık bulguları için kabul ya da düzeltme kararı.

## Aktif iş tablosu

| Alan | Sahip | Durum | Sonraki somut adım | Blokaj |
| --- | --- | --- | --- | --- |
| AI MediaAsset | Backend + Security | Tamamlandı | CI'da upload/owner/expiry/retention entegrasyon akışını doğrulamak | Byte adaptörü ve saatlik retention job uygulandı; CI E2E kanıtı bekliyor |
| AI draft asset mobil entegrasyonu | Frontend + QA | Tamamlandı | CI'da gerçek backend akışıyla E2E doğrulamak | Temiz lockfile kurulumu, Jest, lint ve TypeScript doğrulaması geçti |
| Büyük ekran refaktörü | Frontend + QA | Devam ediyor | WorkOrderCreate modal/step bölümlerini, ardından WorkOrderEdit kalem/işçilik alanlarını ayırmak | Yerel test ortamı hazır; davranış korunarak bileşen sınırları tamamlanmalı |
| Create/Edit ayrıştırma ayrıntısı | Frontend + QA | Devam ediyor | Create: AI Diagnosis modalı, müşteri/araç seçimi. Edit: parça, işçilik, maliyet özeti ve AI modalları | Create ekranında parça/işçilik alanı yok; bu alan yalnız Edit ekranında. Müşteri/araç taslağı entegre edilmeden kaldırıldı; davranış korunacak şekilde yeniden kapsamlanmalı |
| Chat ekleri | Backend + Security + QA | Tasarım hazır | ADR kabul kriterleriyle tam, atomik vertical slice | ChatAttachment partial implementation güvenlik nedeniyle rollback edildi |
| CI / test kanıtı | QA + DevOps | Devam ediyor | GitHub CI'da .NET derleme+test, API başlangıcıyla PostgreSQL migration uygulaması ve `AddAiDiagnosisMediaAssets` geçmiş kaydı; ardından MediaAsset yetki/expiry/retention entegrasyon testleri | Application.Tests 110/110 ve CI eşdeğeri PostgreSQL 16 ile WebAPI.Tests 23/23 yeşil; GitHub CI çalıştırma kanıtı ve MediaAsset E2E kapsamı bekliyor |
| Git geçmiş temizliği | DevOps / Yönetici | Tamamlandı | Ekip clone'larını temiz geçmişe göre yeniden kurmak | `origin/master` force-with-lease ile güncellendi; upload tarihçesi doğrulamada 0 |

## Tamamlanan işler

| Tarih | Sahip | Teslim | Kanıt / not |
| --- | --- | --- | --- |
| 2026-08-08 | Security + Backend | Production anonim `/uploads` erişimi kapatıldı; path traversal engellendi | Production varsayılanı kapalı, migration flag yalnız geçici kullanım için |
| 2026-08-08 | Backend + Frontend | Araç, iş emri, teklif, customer portal ve parça görselleri kaynak-özel `mediaUrl` akışına taşındı | Tenant ve kaynak sahipliği backend'de doğrulanıyor; mobil Bearer header kullanıyor |
| 2026-08-08 | Security | Anonim AI hasar fotoğraf analizi kapatıldı | Production: anonim `401`, yetkili çağrı güvenli asset akışına kadar `410` |
| 2026-08-09 | BA + Security + CTO | Veri envanteri ve retention/DSAR engineering policy hazırlandı | Süreler hukuk onayı bekleyen varsayımlardır; silme uygulanmadı |
| 2026-08-08 | Backend | İş emri sohbet sorgusunda tenant filtresi ve sayfalama eklendi | Chat ek yetkisi ayrı iş olarak açık |
| 2026-08-08 | Backend + Security | AI teşhis taslakları için MediaAsset, yetkili upload, byte-adaptörü ve 24 saatlik retention job eklendi | En fazla 5 görsel / görsel başına 10 MB; job, storage silme başarısız olursa DB kaydını koruyup sonraki turda tekrar dener |
| 2026-08-08 | Backend + Security | AI draft purpose uyumsuzluğu düzeltildi | Upload ve teşhis doğrulaması ortak `AiDiagnosisDraft` sabitini kullanır; yeni upload asset ID'leri geçerli filtreyle eşleşir |
| 2026-08-08 | Backend + QA | AI MediaAsset yetki regresyon testleri eklendi | Tenant dışı, başka kullanıcıya ait ve süresi geçmiş asset storage okunmadan reddedilir; geçerli owner asset binary olarak sağlayıcıya iletilir |
| 2026-08-08 | Backend | P0 Application.Tests derleme sözleşmesi düzeltildi | AI upload success sonucu açık DTO tipi kullanır; iş emri detayındaki vehicle photo URL'si gerçek vehicle kaynağına yönelir |
| 2026-08-08 | QA | Application test HTTP context referansı düzeltildi | `DefaultHttpContext` kullanan yetki testleri için Microsoft.AspNetCore.App framework referansı eklendi |
| 2026-08-08 | Backend + QA | EF async IQueryable test provider ve tenant/sahiplik fixture'ları düzeltildi | .NET 10 Application.Tests: 110/110 başarılı |
| 2026-08-08 | Security + Backend | Chat attachment ADR ve kabul matrisi hazırlandı | Mevcut client-controlled FilePath ve ShopStaff-only policy nedeniyle güvenli bounded implementation için customer participant policy + ChatAttachment modeli gerekir |
| 2026-08-08 | Backend + DevOps | Çift MediaController kaynak dosyası kaldırıldı | Tüm private/public media route'larını içeren tek controller sözleşmesi korundu; WebAPI.Tests derleme çakışması giderildi |
| 2026-08-08 | Frontend | Work order hesaplama/arama, durum seçici, detay sekmeleri ve fotoğraf seçici ayrıldı | Create ekranı 2224 → 2163 satır |
| 2026-08-08 | Frontend + QA | AI draft asset sözleşmesi için istemci tipleri, raw URL/path sızıntısını önleyen test hazırlığı ve entegrasyon kaydı eklendi | Gerçek endpoint tüketimi: asset yükleme + `mediaAssetIds` gönderimi; yerel Jest bağımlılığı yok |
| 2026-08-08 | QA | AI draft asset payload hijyeni gözden geçirildi; geçersiz/tekrarlı asset ID'leri istemcide elendi ve raw `photoUrls` alanı testle yasaklandı | Önceki `Purpose` P1'i backendde ortak sabitle düzeltildi; upload handler birim testiyle doğrulanmış, CI kanıtı bekliyor. |
| 2026-08-08 | QA | AI draft asset sınır testleri tamamlandı: invalid/duplicate ID, raw path sızıntısı ve başarısız upload yanıtı | `git diff --check` geçti; yerel Jest bağımlılığı olmadığı için test çalıştırması CI'ya bağlı. |
| 2026-08-08 | Frontend + QA | `WorkOrderPhotoPicker` tipli, kontrollü bileşen olarak ayrıldı | İzin, upload ve API akışı Create ekranında kaldı; `git diff --check` geçti; mobil test için Node bağımlılıkları bekleniyor |
| 2026-08-08 | Frontend + QA | `TechnicianPickerModal` tipli, kontrollü bileşen olarak ayrıldı | Create ekranı 2224 → 2096 satır; sıradaki en büyük bölüm AI Diagnosis modalı ve müşteri/araç seçimi |
| 2026-08-09 | Frontend + QA | AI Diagnosis modalındaki `DiagnosisSummaryBand` tipli sunum bileşenine ayrıldı | Create ekranı 2224 → 2076 satır; `tsc --noEmit` ve odaklı Jest testi geçti |
| 2026-08-08 | QA + DevOps | Production kaynak kontrolü ve release runbook eklendi | Yerel kaynak kontrolü geçti; tam runtime testler bekliyor |
| 2026-08-08 | QA + DevOps | CI, API health sonrası `AddAiDiagnosisMediaAssets` migration kaydını PostgreSQL'de zorunlu doğrular | Derleme/test kapıları mevcut; GO için CI'da MediaAsset upload/tenant sahibi/expiry-retention entegrasyon senaryoları da yeşil olmalı |
| 2026-08-08 | QA + DevOps | .NET 10 test SDK/adapter uyumluluğu ve AutoMapper güvenlik güncellemesi yapıldı | Application test projesi derlendi; `dotnet test <csproj>` adapterı geçerli çalıştı. 88/110 test başarılı, 22 mevcut davranış/fixture testi başarısız. |
| 2026-08-08 | QA + DevOps | ClosedXML/OpenXML üzerinden gelen `System.IO.Packaging` 8.0.0, doğrudan 8.0.1 override ile güvenli sürüme yükseltildi | Restore/build çıktısında AutoMapper veya System.IO.Packaging için NU1903 kalmamalı; uygulama davranış test başarısızlıkları ayrı release blokajıdır. |
| 2026-08-08 | QA + DevOps | WebAPI integration test host'unun JWT test yapılandırması doğrulandı | Geçici PostgreSQL 16 (`magiccar` / CI ile aynı test şifresi) ile 23/23 geçti; health DB kontrolü production davranışı gevşetilmeden doğrulandı. |
| 2026-08-08 | Privacy + DevOps | İzlenen upload geçmişi yerelde temizlendi | 47 dosya; yerel Git geçmişinde kalan upload commit'i: 0 |
| 2026-08-08 | Frontend + QA + DevOps | Mobil bağımlılık ağacı bozuk kurulum silinmeden karantinaya alındı ve lockfile v3 ile temiz `npm ci --legacy-peer-deps` kuruldu; web proxy yardımcı adı hook kuralıyla çakışmayacak biçimde düzeltildi | `npm run test:ci`: 16 suite / 78 test geçti, global line coverage %43.07. `npm run lint`: 0 error / 188 mevcut warning. `npx tsc --noEmit` kırmızı: `metro.config.js` implicit-any (6) ve `FormInput.tsx` `outlineStyle: 'none'` tip uyumsuzluğu (2). Yerel Node 22.12, CI Node 20 ile aynı değil. `npm audit --omit=dev`: 34 bulgu (1 critical); critical `shell-quote@1.8.3` React Native devtools transitifinde, GHSA-w7jw-789q-3m8p; otomatik yükseltme uygulanmadı. |
| 2026-08-08 | Frontend + QA + DevOps | Mobil TypeScript P1 kapatıldı; Metro proxy için JSDoc HTTP tipleri eklendi, FormInput web outline stili typed `outlineWidth: 0` ile eşdeğerleştirildi. `shell-quote` transitive critical bulgusu React Native yükseltmeden npm override ile 1.10.0'a sabitlendi | `npm run test:ci`: 16 suite / 78 test geçti. `npm run lint`: 0 error / 188 warning. `npx tsc --noEmit`: geçti. `npm audit --omit=dev`: 33 bulgu, 0 critical (22 high). Kalan high bulgular ayrı, uyumluluk değerlendirmeli remediation işidir. |
| 2026-08-09 | QA + DevOps | Mobil production dependency risk kararı kayda alındı | Eski audit toplamı 22 high ancak paket/advisory ayrıntısı repo dışında; güvenli override uygulanmadı. `shell-quote@1.10.0` sabit. Yeni audit için explicit npm metadata gönderim onayı, ardından advisory-bazlı erişilebilirlik sınıflaması ve gerekiyorsa Security+CTO süreli risk kabulü gerekir. |
| 2026-08-09 | Backend + DevOps + QA | Yeni JSON endpointleri için `IDataResult<T>` response envelope kararı ve CI source guard eklendi | `POST /api/AI/diagnosis-assets` başarı/validation yanıtını aynı envelope içinde zorunlu tutar; binary media ve health bilinçli istisnadır. Legacy raw endpointler için kırıcı olmayan migration planı kayda alındı. |
| 2026-08-09 | DevOps + QA | WorkOrderParticipant migration tooling ve WebAPI authorization fixture kalıbı belgelendi | EF Core 9.0.2 ile eşleşen local `dotnet-ef` komutu, startup/persistence proje parametreleri ve CI migration-history kontrolü kayda alındı; participant domain kodu değiştirilmedi. |
| 2026-08-09 | AppSec + Frontend + QA | AI teşhis Markdown render girişi 8.000 karakter ile sınırlandı; otomatik URL linkification açıkça kapatıldı | `react-native-markdown-display → markdown-it → linkify-it` doğrudan runtime zincirinde audit için uyumlu otomatik düzeltme yok (`fixAvailable: false`); zorlayıcı override uygulanmadı. 17 Jest suite / 81 test, lint (0 error / 188 warning) ve `tsc --noEmit` geçti. Destekli renderer yükseltmesi/değişimi için P1 takip gereklidir. |
| 2026-08-09 | AppSec + Frontend + QA | AI Markdown renderer, Expo SDK 55 uyumlu `react-native-markdown-renderer@4.1.1` ile değiştirildi | React 19.2/RN 0.83.2 peer uyumu ve lockfile dry-run doğrulandı; `markdown-it@14.3.0`/`linkify-it@5.0.2` eski yüksek bulguları kaldırdı. Production audit: 31 toplam, 21 high, 0 critical (önce 33/22/0). Renderer, mevcut root React Native bulgusunun peer-effect zincirinde görünür; ek RN kopyası veya renderer özel advisory yok. 17/17 Jest suite, 81/81 test, lint 0 error/188 warning, `tsc --noEmit` geçti. |
| 2026-08-09 | Frontend + QA | WorkOrderEdit maliyet özeti tam, tipli `WorkOrderCostSummary` sunum bileşenine ayrıldı | Toplamlar ve KDV state/callback'i ekran sahipliğinde kaldı; 17/17 Jest suite, lint ve `tsc --noEmit` geçti. |
| 2026-08-09 | Frontend + QA | WorkOrderEdit teknisyen seçici tam, tipli `WorkOrderTechnicianSelector` bileşenine ayrıldı | Picker state ve seçim callback'leri ekran sahipliğinde kaldı; 17/17 Jest suite, lint ve `tsc --noEmit` geçti. |

## Açık riskler ve kararlar

1. **P0 kapalı:** AI `analyze-damage-photos` endpointi artık URL/dosya yolu okumuyor ve production'da çalıştırılamıyor.
2. **P1 kapalı:** AI görsel teşhisi yalnız owner/tenant doğrulanmış MediaAsset byte'larıyla çalışıyor; 24 saatlik retention job uygulandı. CI E2E kanıtı yine de gereklidir.
3. **P1:** Chat ekleri; message-owned asset, katılımcı yetkisi, expiry ve indirme kontrolü olmadan açılmayacak.
4. **P1:** Tam test kanıtı yoktur; .NET 10 SDK ve temiz Node bağımlılıkları ile CI'da yeşil sonuç zorunludur.
5. **P1:** Temizlenmiş Git geçmişinin remote'a force-push edilmesi ve ekiplerin yeniden clone alması gerekir.

## Agent güncelleme kuralı

Her teslimde aşağıdaki biçimde yeni bir satır ekleyin:

`| YYYY-MM-DD | Rol | Tamamlanan somut iş | Test/kanıt, açık risk ve sonraki adım |`

Aktif iş bittiğinde ilgili satırı **Tamamlandı** yapın ve takip eden işi/bloğu aynı güncellemede yazın. Başka agentın satırını veya release kararını değiştirmeyin.
