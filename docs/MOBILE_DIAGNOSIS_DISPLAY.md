# Mobil: AI teşhis ekranı — `mobileDisplayMarkdown`

Bu doküman, backend’e eklenen **chat tarzı Markdown özet** alanının mobil uygulamada nasıl kullanılacağını anlatır.

## Mobil geliştirici değişiklik yapmak zorunda mı?

**Evet, ama sınırlı.** Aşağıdakilerden biri geçerli:

| Durum | Gerekli iş |
|--------|------------|
| Teşhis sonucunu zaten tek bir metin / WebView / Markdown ile gösteriyorsanız | Sadece **yeni alanı** (`mobileDisplayMarkdown`) bağlamanız yeterli. |
| Sadece liste kartlarıyla (`possibleIssues`, `damagedParts` vb.) UI kurduysanız | **Yeni bir blok** ekleyip bu alanı Markdown olarak render etmeniz gerekir. |
| Eski API’lerle uyumlu kalmak istiyorsanız | Alan **opsiyonel**; yoksa veya boşsa mevcut ekranı göstermeye devam edin. |

Backend, alanı dolduramazsa sunucu tarafında **yedek metin** üretmeye çalışır; yine de mobilde **null/boş** kontrolü önerilir.

## API sözleşmesi

- **DTO özelliği (C#):** `MobileDisplayMarkdown`
- **JSON (varsayılan ASP.NET Core camelCase):** `mobileDisplayMarkdown`
- **Tip:** `string | null`
- **İçerik:** Markdown metin (emoji, `**kalın**`, madde işaretleri, `---` ayırıcı, `→` okları)

### Ne zaman dolu?

- Öncelikle **kaza / hasar** senaryosunda (`diagnosisType === 1` / `Accident`) ve model bu alanı ürettiğinde.
- Model üretmezse backend, hasar listesinden **otomatik yedek** üretmeye çalışır; bu durumda da alan genelde dolu olur.

### `sceneDescription` (opsiyonel, debug / güven)

- JSON: **`sceneDescription`**
- Modelin fotoğrafta gördüğü **açı + kadraj** özeti (Türkçe). Hasar listesiyle tutarsızlık şüphesinde bu alanı göstermek veya loglamak faydalıdır.

### Örnek JSON parçası

```json
{
  "data": {
    "diagnosisType": 1,
    "confidenceScore": 82,
    "mobileDisplayMarkdown": "Kısa net analiz:\n\n🔧 **Hasar Durumu**\n- ...",
    "damagedParts": [ ],
    "criticalChecks": [ ],
    "estimatedRepairRange": { "min": 80000, "max": 180000, "currency": "TRY" }
  }
}
```

Gerçek endpoint ve sarmalayıcı (`SuccessDataResult`, `IDataResult` vb.) projenizdeki API kontratına göre `data` altında veya kökte olabilir; **alan adı `mobileDisplayMarkdown`** kalır.

## Önerilen UI davranışı

1. Teşhis detay ekranında **üstte veya ana gövdede** `mobileDisplayMarkdown` varsa bunu gösterin.
2. **Markdown renderer** kullanın (başlık, kalın, liste, yatay çizgi desteklenmeli).
3. Koyu tema için mevcut tasarım token’larınızla (`backgroundColor`, `body` rengi) stilleri verin.
4. Alan **yok veya boş** ise:
   - Mevcut davranışınızı koruyun (parça listesi, öneriler vb.), veya
   - Sadece `recommendations` metnini göstermeye devam edin.

## React Native (Expo) — örnek

Paket örneği: `react-native-markdown-display` (veya ekip içi tercih ettiğiniz Markdown kütüphanesi).

```tsx
import Markdown from 'react-native-markdown-display';

function DiagnosisBody({ result }: { result: DiagnosisResult }) {
  const md = result.mobileDisplayMarkdown?.trim();
  if (md) {
    return (
      <Markdown
        style={{
          body: { color: '#f0f0f0', fontSize: 15, lineHeight: 22 },
          heading2: { color: '#fff', marginTop: 12 },
          bullet_list: { marginTop: 4 },
          hr: { backgroundColor: '#333', marginVertical: 16 },
        }}
      >
        {md}
      </Markdown>
    );
  }
  // Geri dönüş: eski liste UI
  return <LegacyDiagnosisLists result={result} />;
}
```

TypeScript tipi (projeye göre uyarlayın):

```ts
interface DiagnosisResult {
  diagnosisType: number;
  mobileDisplayMarkdown?: string | null;
  // ... diğer alanlar
}
```

## Test checklist (mobil QA)

- [ ] Kaza içeren şikayet metni + mümkünse foto ile teşhis: `mobileDisplayMarkdown` dolu mu?
- [ ] Sadece mekanik şikayet: alan boş olabilir; uygulama çökmeden eski UI’a düşüyor mu?
- [ ] Uzun metin: scroll ve performans kabul edilebilir mi?
- [ ] Emoji ve `→` karakteri cihazlarda doğru görünüyor mu?

## Özet

- **Zorunlu mobil iş:** Response modeline `mobileDisplayMarkdown` eklemek ve uygunsa Markdown ile göstermek.
- **İsteğe bağlı:** Tasarım ince ayarı, eski sürümlerle uyumluluk, hata/boş durum metni.

Sorular için backend tarafında alan `Core.Packages.Application` içindeki `DiagnosisResultDto.MobileDisplayMarkdown` ile tanımlıdır.
