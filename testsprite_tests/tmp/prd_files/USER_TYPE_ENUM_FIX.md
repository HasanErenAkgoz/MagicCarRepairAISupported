# Backend Güncelleme Gereksinimi - UserType Enum Serialization

## Sorun

Frontend'de `userType` alanı **number** olarak bekleniyor, ancak backend'den **string** olarak geliyor. Bu durum frontend'deki enum karşılaştırmalarının çalışmamasına neden oluyor.

### Mevcut Durum

**Backend Response (Yanlış):**
```json
{
  "user": {
    "id": 1006,
    "email": "hasanerenakgz@gmail.com",
    "firstName": "Hasan Eren",
    "lastName": "Akgoz",
    "userType": "SystemAdmin",  // ❌ String olarak geliyor
    "roles": ["System Admin"],
    ...
  }
}
```

**Frontend Beklentisi:**
```typescript
export enum UserType {
  SystemAdmin = 1,
  Manager = 2,
  Employee = 3,
  Customer = 4,
}
```

Frontend'de `userType` değeri `UserType.SystemAdmin` (yani `1`) ile karşılaştırılıyor, ancak backend'den `"SystemAdmin"` string'i geldiği için karşılaştırma başarısız oluyor.

### Beklenen Durum

**Backend Response (Doğru):**
```json
{
  "user": {
    "id": 1006,
    "email": "hasanerenakgz@gmail.com",
    "firstName": "Hasan Eren",
    "lastName": "Akgoz",
    "userType": 1,  // ✅ Number olarak gelmeli
    "roles": ["System Admin"],
    ...
  }
}
```

## Yapılması Gereken Değişiklikler

### 1. UserType Enum Serialization

Backend'de `UserType` enum'ının JSON serialization'ında **number** olarak serialize edilmesi gerekiyor.

#### .NET (C#) Örneği

Eğer .NET kullanıyorsanız, enum'ın number olarak serialize edilmesi için:

**Seçenek 1: JsonConverter Attribute (Önerilen)**
```csharp
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    SystemAdmin = 1,
    Manager = 2,
    Employee = 3,
    Customer = 4
}
```

**Seçenek 2: Global JSON Options**
```csharp
// Program.cs veya Startup.cs
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
```

**Seçenek 3: Manuel Serialization (Eğer yukarıdakiler çalışmazsa)**
```csharp
public class UserTypeJsonConverter : JsonConverter<UserType>
{
    public override UserType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return (UserType)reader.GetInt32();
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            return Enum.Parse<UserType>(stringValue);
        }
        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, UserType value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((int)value);
    }
}

[JsonConverter(typeof(UserTypeJsonConverter))]
public enum UserType
{
    SystemAdmin = 1,
    Manager = 2,
    Employee = 3,
    Customer = 4
}
```

#### Node.js/TypeScript Örneği

Eğer Node.js/TypeScript kullanıyorsanız:

```typescript
enum UserType {
  SystemAdmin = 1,
  Manager = 2,
  Employee = 3,
  Customer = 4
}

// Response gönderirken
const userResponse = {
  ...user,
  userType: user.userType as number, // Enum değerini number olarak gönder
};
```

### 2. Tüm Endpoint'lerde Kontrol

Aşağıdaki endpoint'lerde `userType` alanının number olarak gönderildiğinden emin olun:

- ✅ `POST /api/Auth/login` - Login response'unda user objesi
- ✅ `POST /api/Auth/register` - Register response'unda user objesi
- ✅ `POST /api/Auth/register-customer` - Register response'unda user objesi
- ✅ `POST /api/Auth/register-shop` - Register response'unda user objesi
- ✅ `POST /api/Auth/complete-2fa-login` - 2FA response'unda user objesi
- ✅ Diğer user döndüren tüm endpoint'ler

### 3. Veritabanı Kontrolü

Veritabanında `userType` alanının **integer/number** tipinde olduğundan emin olun:

```sql
-- Örnek: Users tablosu
ALTER TABLE users 
MODIFY COLUMN userType INT NOT NULL;

-- Veya yeni tablo oluştururken
CREATE TABLE users (
    id INT PRIMARY KEY AUTO_INCREMENT,
    ...
    userType INT NOT NULL,  -- ✅ Integer olmalı
    ...
);
```

## Test Senaryoları

### Test 1: Login Response
```http
POST /api/Auth/login
Content-Type: application/json

{
  "email": "admin@example.com",
  "password": "password123"
}
```

**Beklenen Response:**
```json
{
  "success": true,
  "data": {
    "token": "...",
    "refreshToken": "...",
    "user": {
      "id": 1006,
      "userType": 1,  // ✅ Number olmalı, "SystemAdmin" değil
      ...
    }
  }
}
```

### Test 2: SystemAdmin Login
- SystemAdmin olarak login yapın
- Response'da `userType: 1` olduğunu doğrulayın
- Frontend'de AdminDashboardScreen'e yönlendirildiğini kontrol edin

### Test 3: Manager Login
- Manager olarak login yapın
- Response'da `userType: 2` olduğunu doğrulayın
- Frontend'de AdminDashboardScreen'e yönlendirildiğini kontrol edin

### Test 4: Employee Login
- Employee olarak login yapın
- Response'da `userType: 3` olduğunu doğrulayın
- Frontend'de AdminDashboardScreen'e yönlendirildiğini kontrol edin

### Test 5: Customer Login
- Customer olarak login yapın
- Response'da `userType: 4` olduğunu doğrulayın
- Frontend'de HomeScreen'e yönlendirildiğini kontrol edin

## UserType Enum Değerleri

Frontend'deki enum değerleri ile backend'in göndermesi gereken değerler:

| Rol | Enum Değeri | Backend'den Gönderilmesi Gereken |
|-----|-------------|----------------------------------|
| SystemAdmin | 1 | `1` (number) |
| Manager | 2 | `2` (number) |
| Employee | 3 | `3` (number) |
| Customer | 4 | `4` (number) |

## Önemli Notlar

1. **Backward Compatibility**: Eğer mevcut sistemde string olarak kullanılıyorsa, geçiş sürecinde hem string hem number'ı kabul edebilirsiniz, ancak response'da mutlaka number gönderin.

2. **Type Safety**: Number göndermek type safety açısından daha iyidir ve frontend'deki enum karşılaştırmaları doğru çalışır.

3. **Test**: Değişiklikten sonra tüm user rolleri ile login test edin ve frontend'in doğru ekrana yönlendirdiğini doğrulayın.

## Geçici Çözüm (Frontend'de)

Şu anda frontend'de geçici bir parse fonksiyonu ekledik, ancak backend düzeltildikten sonra bu fonksiyon kaldırılabilir. Backend düzeltmesi yapıldıktan sonra frontend'deki `parseUserType` fonksiyonunu kaldıracağız.

## Sorular?

Eğer backend teknolojiniz farklıysa (Python, Java, vb.) veya ek yardıma ihtiyacınız varsa, lütfen iletişime geçin.

---

**Öncelik:** Yüksek  
**Etkilenen Endpoint'ler:** Tüm authentication endpoint'leri  
**Tahmini Süre:** 15-30 dakika
