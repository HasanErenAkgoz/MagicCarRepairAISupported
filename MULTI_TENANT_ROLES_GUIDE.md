# Multi-Tenant Role & Permission Sistemi

## 🎯 Genel Bakış

Artık **her oto servisin kendi rol ve izin yapısı** var! İki farklı servis tamamen farklı rollere sahip olabilir.

---

## 📊 Yapı

### 1. Permission (İzinler)
- **ClientId = 0**: Global izinler (tüm servisler kullanabilir)
- **ClientId > 0**: Servise özel izinler

#### Global İzinler (Tüm Servisler İçin)
```
Customers.View, Customers.Create, Customers.Update, Customers.Delete
Vehicles.View, Vehicles.Create, Vehicles.Update, Vehicles.Delete
Users.View, Users.Create, Users.Update, Users.Delete
Roles.View, Roles.Create, Roles.Update, Roles.Delete
```

### 2. Role (Roller)
Her servis kendi rollerini oluşturur:

#### Demo Client (ClientId = 1) Rolleri:
- **Admin**: Tam yetki
- **Manager**: Kullanıcı/rol silme hariç çoğu işlem
- **Technician**: Müşterileri görüntüleme, araçları yönetme
- **Receptionist**: Müşteri ve araç ekleme/güncelleme

#### Test Client (ClientId = 2) Rolleri:
- **Owner**: Tam yetki
- **ServiceAdvisor**: Müşteri ve araç yönetimi
- **Mechanic**: Müşteri görüntüleme, araç yönetimi

---

## 🔥 Önemli: Her Client Farklı Roller Kullanır

### Örnek Senaryo

**ABC Oto Servis (ClientId = 1)**
```json
Roller:
- Admin (Tam yetki)
- Manager (Üst düzey işlemler)
- Technician (Teknik işler)
- Receptionist (Ön büro)

Admin kullanıcısı:
- 16 izne sahip (her şeyi yapabilir)
```

**XYZ Oto Servis (ClientId = 2)**
```json
Roller:
- Owner (Sahibi - tam yetki)
- ServiceAdvisor (Servis danışmanı)
- Mechanic (Tamirci)

Owner kullanıcısı:
- 16 izne sahip (her şeyi yapabilir)
- Ama farklı bir role structure
```

---

## 💡 Kullanım Örnekleri

### 1. Kullanıcıya Rol Atama

```csharp
// ABC Oto Servis'te kullanıcı oluşturma
var user = new User
{
    UserName = "ahmet.yilmaz",
    Email = "ahmet@abcotoservis.com",
    ClientId = 1, // ABC Oto Servis
    Language = "tr"
};

await _userManager.CreateAsync(user, "Password123!");

// ABC Oto Servis'in "Manager" rolünü atama
await _userManager.AddToRoleAsync(user, "Manager");
```

### 2. İzin Kontrolü

```csharp
public class VehicleService
{
    public async Task<bool> CanUserCreateVehicle(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        // Kullanıcının rolleri otomatik olarak kendi client'ına göre filtrelenir
        var roles = await _userManager.GetRolesAsync(user);
        
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            
            // Bu role'ün izinleri de otomatik olarak ClientId'ye göre filtrelenir
            var permissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == role.Id)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission.Name)
                .ToListAsync();
            
            if (permissions.Contains("Vehicles.Create"))
                return true;
        }
        
        return false;
    }
}
```

### 3. Yeni Rol Oluşturma (Servise Özel)

```csharp
// ABC Oto Servis için özel rol oluşturma
var role = new Role
{
    Name = "SeniorTechnician",
    NormalizedName = "SENIORTECHNICIAN",
    ClientId = 1 // ABC Oto Servis
};

await _roleManager.CreateAsync(role);

// Bu role izin atama
var permission = await _context.Permissions
    .FirstOrDefaultAsync(p => p.Name == "Vehicles.Update" && p.ClientId == 0);

var rolePermission = new RolePermission
{
    RoleId = role.Id,
    PermissionId = permission.Id,
    ClientId = 1 // ABC Oto Servis
};

_context.RolePermissions.Add(rolePermission);
await _context.SaveChangesAsync();
```

---

## 🔒 Güvenlik

### Otomatik Filtreleme
```csharp
// ❌ YANLIŞ - Manuel filtreleme gerekmez
var roles = await _context.Roles
    .Where(r => r.ClientId == currentClientId)
    .ToListAsync();

// ✅ DOĞRU - Global query filter otomatik çalışır
var roles = await _context.Roles.ToListAsync();
```

### Sadece Kendi Client'ının Verilerini Görebilir

**ABC Oto Servis kullanıcısı (ClientId = 1)**
```sql
SELECT * FROM Roles WHERE ClientId = 1
-- Sonuç: Admin, Manager, Technician, Receptionist
```

**XYZ Oto Servis kullanıcısı (ClientId = 2)**
```sql
SELECT * FROM Roles WHERE ClientId = 2
-- Sonuç: Owner, ServiceAdvisor, Mechanic
```

---

## 📝 Veritabanı Yapısı

### Roles Tablosu
```sql
Id | Name      | NormalizedName | ClientId | ConcurrencyStamp
1  | Admin     | ADMIN          | 1        | guid...
2  | Manager   | MANAGER        | 1        | guid...
5  | Owner     | OWNER          | 2        | guid...
6  | Mechanic  | MECHANIC       | 2        | guid...

-- Unique Index: (Name, ClientId)
-- Her client'ta aynı isimde rol olabilir ama farklı client'lar
```

### Permissions Tablosu
```sql
Id | Name              | ClientId | Description
1  | Customers.View    | 0        | Global permission
2  | Customers.Create  | 0        | Global permission
50 | CustomPermission  | 1        | ABC Servis'e özel

-- ClientId = 0 → Global (herkes kullanabilir)
-- ClientId > 0 → Servise özel
```

### RolePermissions Tablosu
```sql
Id | RoleId | PermissionId | ClientId
1  | 1      | 1            | 1        -- ABC Admin → Customers.View
2  | 1      | 2            | 1        -- ABC Admin → Customers.Create
100| 5      | 1            | 2        -- XYZ Owner → Customers.View

-- Unique Index: (RoleId, PermissionId, ClientId)
```

---

## 🚀 Migration

```powershell
# Yeni migration oluştur
dotnet ef migrations add AddMultiTenantRoles --project MagicCarRepairAISupported.Persistence --startup-project MagicCarRepairAISupported.WebAPI

# Veritabanını güncelle (seed data otomatik yüklenecek)
dotnet ef database update --project MagicCarRepairAISupported.Persistence --startup-project MagicCarRepairAISupported.WebAPI
```

---

## 🎨 Seed Data

Sistem başlatıldığında otomatik olarak:
- 16 global permission oluşturulur
- Client 1 için 4 rol (Admin, Manager, Technician, Receptionist)
- Client 2 için 3 rol (Owner, ServiceAdvisor, Mechanic)
- Her rol için uygun izinler atanır

---

## 🔧 Özel Kullanım Senaryoları

### Senaryo 1: Servise Özel İzin Oluşturma

```csharp
// ABC Oto Servis özel bir izin tanımlamak istiyor
var customPermission = new Permission
{
    Name = "Invoices.CreateAdvanced",
    Description = "Gelişmiş fatura oluşturma",
    ClientId = 1 // Sadece ABC Oto Servis
};

_context.Permissions.Add(customPermission);
await _context.SaveChangesAsync();

// Bu izin sadece ClientId = 1 olan kullanıcılar tarafından görülebilir
```

### Senaryo 2: Global İzin Ekleme (Tüm Servisler İçin)

```csharp
// Tüm servisler için yeni bir izin
var globalPermission = new Permission
{
    Name = "Reports.View",
    Description = "Raporları görüntüleme",
    ClientId = 0 // Global
};

_context.Permissions.Add(globalPermission);
await _context.SaveChangesAsync();

// Bu izin tüm client'lar tarafından kullanılabilir
```

---

## ⚡ Performans İpuçları

1. **Role-Permission cache**: Sık kullanılan izinleri cache'leyin
2. **Global Permissions**: Mümkün olduğunca global permission kullanın
3. **Index'ler**: (Name, ClientId) unique index'leri performansı artırır

---

## 🎯 Özet

✅ Her servis kendi rollerini tanımlayabilir  
✅ Her servis kendi izinlerini oluşturabilir  
✅ Global izinler tüm servisler için kullanılabilir  
✅ Otomatik veri izolasyonu (query filters)  
✅ Güvenli multi-tenant yapı  

**Artık ABC Oto Servis ve XYZ Oto Servis tamamen farklı rol yapılarına sahip olabilir! 🚀**

