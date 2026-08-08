# Production Release Runbook

Bu runbook, MagicCarRepair API'sinin production yayını için platformdan bağımsız operasyon standardıdır. Platforma ait komutlar ilgili ortamın runbook'unda tutulur; bu belge sadece doğrulanmış işlem adımlarını tanımlar.

## Sahiplik ve stop-ship yetkisi

| Alan | Sahip | Stop-ship nedeni |
| --- | --- | --- |
| Kapsam ve müşteri etkisi | PM | Kabul kriteri veya kritik kullanıcı akışı eksik |
| Teknik yayın | CTO / Backend | Geri dönüş planı ya da migration doğrulaması yok |
| Kalite | QA | Kritik test başarısız veya flaky |
| Güvenlik ve gizlilik | Security/AppSec | Secret, PII erişimi veya yetki ihlali |
| Operasyon | DevOps | Sağlık, gözlemlenebilirlik, backup ya da rollback kanıtı yok |

Bir stop-ship ancak PM, CTO, QA ve Security tarafından süreli yazılı risk kabulü ile kaldırılabilir.

## Release öncesi kalite kapısı

- [ ] `main` üzerindeki CI: build, application/API testleri, API module testleri ve configuration contract başarılı.
- [ ] Kritik E2E: giriş, rol/tenant sınırı, müşteri-araç-iş emri oluşturma, ödeme hata yolu ve yetkisiz medya erişimi doğrulandı.
- [ ] Migration planı gözden geçirildi: ileri uyumlu, veri kaybı yaratmıyor ve geri dönüş stratejisi var.
- [ ] Değişiklik notu, etkilenen API sözleşmeleri ve bilinen riskler hazır.
- [ ] QA, production verisi yerine sentetik/veri maskeleme kurallı test verisi kullandığını doğruladı.
- [ ] Runtime upload dizininde müşteri/araç görseli bulunmadığı ve `git ls-files Core.Packages.WebAPI/wwwroot/uploads` çıktısının boş olduğu doğrulandı. Önceden commit edilmiş medya için repository geçmişi, Security onaylı bir temizleme planıyla ayrıca ele alındı.

## Production yapılandırma doğrulaması

Secret değerlerini chat, ticket, CI logu veya shell history içinde paylaşmayın. Değerlerin kendisini değil, yalnızca doğrulama sonucunu kaydedin.

1. Runtime ortamında şu değerlerin bulunduğunu doğrulayın: `ConnectionStrings__DefaultConnection`, `TokenOptions__SecurityKey`, `TokenOptions__Issuer`, `TokenOptions__Audience`, `Cors__AllowedOrigins__0`.
2. JWT anahtarının en az 32 karakter, benzersiz ve rotasyona uygun olduğunu; connection string'in TLS/host erişim politikasına uyduğunu doğrulayın.
3. `SuperPassword__Enabled=false` olmalı. Geliştirme `AllowedHosts=*`, localhost CORS, `Include Error Detail=true` veya örnek şifreleri production'a taşınmamalı.
4. Kullanılan entegrasyonlar için yalnız gerekli secret'ları tanımlayın: AI, SMTP, SMS/WhatsApp, FCM ve Iyzico. Kullanılmayan kanalları devre dışı bırakın.
5. Deploy identity'sinin least-privilege yetkileri, secret okuma erişimi ve audit logları doğrulansın.

## Yayın ve sağlık doğrulaması

1. Onaylı commit SHA ve sürüm numarasını release kaydına yazın.
2. Uygulamayı önce staging/canary ortamına alın; migration çalıştırılıyorsa başlangıç/bitiş ve sonuç kaydını saklayın.
3. `/health` endpoint'i HTTP 200 ve `Healthy` dönene kadar trafiği açmayın. Bu denetim PostgreSQL erişimini kapsar.
4. Authenticated smoke test ile giriş, tenant izolasyonu ve temel iş emri okuma akışını doğrulayın. Test hesabı sentetik olmalı.
5. Uygulama hata logları, HTTP 5xx, PostgreSQL bağlantı hataları, CPU/bellek ve API gecikmesi için dashboard/alertlerin veri aldığını kontrol edin.
6. Canary'de en az 15 dakika hata bütçesi tüketimi olmadan gözlemleyin; sonra kademeli trafiğe açın.

## SLO / SLI başlangıç hedefleri

Bu değerler PM+CTO tarafından ilk 30 günlük baz ölçümden sonra onaylanmalı ve izleme platformunda dashboard olarak uygulanmalıdır.

| Kullanıcı etkisi | SLI | İlk SLO | Ölçüm penceresi | Sahip |
| --- | --- | --- | --- | --- |
| API erişilebilirliği | Başarılı istek / geçerli istek | %99.9 | 30 gün | DevOps |
| API hatası | 5xx / tüm istekler | <%0.5 | 30 gün | Backend |
| Kritik API gecikmesi | `p95` yanıt süresi | <800 ms | 7 gün | CTO |
| Health | `/health` başarılı kontrol oranı | %99.9 | 30 gün | DevOps |
| Veri geri yükleme | Son başarılı restore doğrulaması | En fazla 30 gün eski | aylık | DevOps |

4xx hataları erişilebilirlik hatası sayılmaz; 429, yanlış yapılandırılmış rate limit ya da hatalı yetkilendirme işaret ediyorsa ayrı incelenir. Hata bütçesi tükendiğinde feature yayını durdurulur ve güvenilirlik işi önceliklendirilir.

## Rollback

Rollback tetikleyicileri: health başarısızlığı, 5xx artışı, tenant/veri erişim ihlali, kritik iş akışında veri bütünlüğü sorunu veya canary SLO ihlali.

1. Yeni dağıtımı durdurun ve yeni trafiği son bilinen iyi sürüme yönlendirin.
2. Önceki onaylı image/artifact ve commit SHA'ya dönün; rollback öncesi/sonrası `/health` ve authenticated smoke test çalıştırın.
3. Migration geri alınamıyorsa uygulama rollback'ini migration ile uyumlu sürümle yapın; doğrulanmamış yıkıcı down-migration çalıştırmayın.
4. Etki, karar zamanı, sorumlu ve doğrulama sonucunu incident kaydına ekleyin.

## Backup ve restore tatbikatı

- Production PostgreSQL için şifreli otomatik backup, erişim sınırı ve saklama politikası platformda tanımlı olmalı.
- Her release öncesi son backup'ın başarılı olduğu doğrulanır; en az ayda bir ayrı bir staging ortamına restore tatbikatı yapılır.
- Tatbikatta restore süresi (RTO), kabul edilen veri kaybı penceresi (RPO), şema uyumluluğu ve sentetik smoke test sonucu kaydedilir.
- Backup'ta PII bulunduğu için erişim yalnız yetkili operasyon personeliyle sınırlıdır; export'lar süre sonunda güvenli biçimde silinir.

## Incident iletişimi

| Seviye | Etki | İlk aksiyon |
| --- | --- | --- |
| SEV1 | Yaygın kesinti, veri kaybı veya ihlal | DevOps incident komutası ve CTO anında devrede; trafiği azalt/rollback et |
| SEV2 | Ana akış önemli kullanıcı grubunda bozuk | Hızlı müdahale ve düzenli durum güncellemesi |
| SEV3 | Sınırlı etki, workaround var | Planlı düzeltme ve takip |

SEV1/SEV2 sonrası suçlamasız postmortem; zaman çizelgesi, kullanıcı etkisi, kök neden, düzeltici aksiyon, sahip ve vade ile tamamlanır.
