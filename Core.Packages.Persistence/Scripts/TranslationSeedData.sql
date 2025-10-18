-- Translation Seed Data SQL Script
-- Çoklu dil desteği için translation verilerini ekler

-- DomainException Messages - Turkish
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('DomainException.VEHICLE_KM_LOWER_THAN_CURRENT', 'tr', 'Yeni kilometre ({NewKilometers}), mevcut kilometreden ({CurrentKilometers}) düşük olamaz. Plaka: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_KM_NEGATIVE', 'tr', 'Kilometre değeri negatif olamaz. Girilen değer: {NewKilometers}. Plaka: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.USER_EMAIL_EXISTS', 'tr', 'Bu e-posta adresi zaten kullanılıyor: {Email}', 1, GETUTCDATE()),
('DomainException.USER_AGE_INVALID', 'tr', 'Kullanıcı yaşı 18''den küçük olamaz. Mevcut yaş: {Age}', 1, GETUTCDATE()),
('DomainException.PERMISSION_NOT_FOUND', 'tr', 'İzin bulunamadı: {PermissionName}', 1, GETUTCDATE()),
('DomainException.VEHICLE_NOT_FOUND', 'tr', 'Araç bulunamadı. ID: {VehicleId}', 1, GETUTCDATE()),
('DomainException.CUSTOMER_NOT_FOUND', 'tr', 'Müşteri bulunamadı. ID: {CustomerId}', 1, GETUTCDATE()),
('DomainException.INVALID_LICENSE_PLATE', 'tr', 'Geçersiz plaka formatı: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_ALREADY_EXISTS', 'tr', 'Bu plaka zaten kayıtlı: {LicensePlate}', 1, GETUTCDATE());

-- DomainException Messages - English
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('DomainException.VEHICLE_KM_LOWER_THAN_CURRENT', 'en', 'New kilometers ({NewKilometers}) cannot be lower than current kilometers ({CurrentKilometers}). License Plate: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_KM_NEGATIVE', 'en', 'Kilometers value cannot be negative. Entered value: {NewKilometers}. License Plate: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.USER_EMAIL_EXISTS', 'en', 'This email address is already in use: {Email}', 1, GETUTCDATE()),
('DomainException.USER_AGE_INVALID', 'en', 'User age cannot be less than 18. Current age: {Age}', 1, GETUTCDATE()),
('DomainException.PERMISSION_NOT_FOUND', 'en', 'Permission not found: {PermissionName}', 1, GETUTCDATE()),
('DomainException.VEHICLE_NOT_FOUND', 'en', 'Vehicle not found. ID: {VehicleId}', 1, GETUTCDATE()),
('DomainException.CUSTOMER_NOT_FOUND', 'en', 'Customer not found. ID: {CustomerId}', 1, GETUTCDATE()),
('DomainException.INVALID_LICENSE_PLATE', 'en', 'Invalid license plate format: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_ALREADY_EXISTS', 'en', 'This license plate is already registered: {LicensePlate}', 1, GETUTCDATE());

-- DomainException Messages - German
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('DomainException.VEHICLE_KM_LOWER_THAN_CURRENT', 'de', 'Neue Kilometer ({NewKilometers}) können nicht niedriger sein als aktuelle Kilometer ({CurrentKilometers}). Kennzeichen: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_KM_NEGATIVE', 'de', 'Kilometerwert kann nicht negativ sein. Eingegebener Wert: {NewKilometers}. Kennzeichen: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.USER_EMAIL_EXISTS', 'de', 'Diese E-Mail-Adresse wird bereits verwendet: {Email}', 1, GETUTCDATE()),
('DomainException.USER_AGE_INVALID', 'de', 'Benutzeralter kann nicht unter 18 sein. Aktuelles Alter: {Age}', 1, GETUTCDATE()),
('DomainException.PERMISSION_NOT_FOUND', 'de', 'Berechtigung nicht gefunden: {PermissionName}', 1, GETUTCDATE()),
('DomainException.VEHICLE_NOT_FOUND', 'de', 'Fahrzeug nicht gefunden. ID: {VehicleId}', 1, GETUTCDATE()),
('DomainException.CUSTOMER_NOT_FOUND', 'de', 'Kunde nicht gefunden. ID: {CustomerId}', 1, GETUTCDATE()),
('DomainException.INVALID_LICENSE_PLATE', 'de', 'Ungültiges Kennzeichenformat: {LicensePlate}', 1, GETUTCDATE()),
('DomainException.VEHICLE_ALREADY_EXISTS', 'de', 'Dieses Kennzeichen ist bereits registriert: {LicensePlate}', 1, GETUTCDATE());

-- General Exception Messages - Turkish
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Exception.ValidationException', 'tr', 'Doğrulama hatası oluştu', 1, GETUTCDATE()),
('Exception.UnauthorizedAccessException', 'tr', 'Bu işlem için yetkiniz bulunmamaktadır', 1, GETUTCDATE()),
('Exception.NotFoundException', 'tr', 'Aranan kayıt bulunamadı', 1, GETUTCDATE()),
('Exception.ArgumentNullException', 'tr', 'Gerekli parametre eksik', 1, GETUTCDATE()),
('Exception.ArgumentException', 'tr', 'Geçersiz parametre değeri', 1, GETUTCDATE()),
('Exception.TimeoutException', 'tr', 'İşlem zaman aşımına uğradı', 1, GETUTCDATE()),
('Exception.SqlException', 'tr', 'Veritabanı hatası oluştu', 1, GETUTCDATE()),
('Exception.HttpRequestException', 'tr', 'Ağ bağlantı hatası', 1, GETUTCDATE());

-- General Exception Messages - English
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Exception.ValidationException', 'en', 'Validation error occurred', 1, GETUTCDATE()),
('Exception.UnauthorizedAccessException', 'en', 'You do not have permission for this operation', 1, GETUTCDATE()),
('Exception.NotFoundException', 'en', 'The requested record was not found', 1, GETUTCDATE()),
('Exception.ArgumentNullException', 'en', 'Required parameter is missing', 1, GETUTCDATE()),
('Exception.ArgumentException', 'en', 'Invalid parameter value', 1, GETUTCDATE()),
('Exception.TimeoutException', 'en', 'Operation timed out', 1, GETUTCDATE()),
('Exception.SqlException', 'en', 'Database error occurred', 1, GETUTCDATE()),
('Exception.HttpRequestException', 'en', 'Network connection error', 1, GETUTCDATE());

-- General Exception Messages - German
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Exception.ValidationException', 'de', 'Validierungsfehler aufgetreten', 1, GETUTCDATE()),
('Exception.UnauthorizedAccessException', 'de', 'Sie haben keine Berechtigung für diese Operation', 1, GETUTCDATE()),
('Exception.NotFoundException', 'de', 'Der angeforderte Datensatz wurde nicht gefunden', 1, GETUTCDATE()),
('Exception.ArgumentNullException', 'de', 'Erforderlicher Parameter fehlt', 1, GETUTCDATE()),
('Exception.ArgumentException', 'de', 'Ungültiger Parameterwert', 1, GETUTCDATE()),
('Exception.TimeoutException', 'de', 'Vorgang ist abgelaufen', 1, GETUTCDATE()),
('Exception.SqlException', 'de', 'Datenbankfehler aufgetreten', 1, GETUTCDATE()),
('Exception.HttpRequestException', 'de', 'Netzwerkverbindungsfehler', 1, GETUTCDATE());

-- Success Messages - Turkish
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Success.VehicleUpdated', 'tr', 'Araç bilgileri başarıyla güncellendi', 1, GETUTCDATE()),
('Success.VehicleCreated', 'tr', 'Araç başarıyla oluşturuldu', 1, GETUTCDATE()),
('Success.VehicleDeleted', 'tr', 'Araç başarıyla silindi', 1, GETUTCDATE()),
('Success.UserCreated', 'tr', 'Kullanıcı başarıyla oluşturuldu', 1, GETUTCDATE()),
('Success.UserUpdated', 'tr', 'Kullanıcı bilgileri başarıyla güncellendi', 1, GETUTCDATE()),
('Success.UserDeleted', 'tr', 'Kullanıcı başarıyla silindi', 1, GETUTCDATE()),
('Success.CustomerCreated', 'tr', 'Müşteri başarıyla oluşturuldu', 1, GETUTCDATE()),
('Success.CustomerUpdated', 'tr', 'Müşteri bilgileri başarıyla güncellendi', 1, GETUTCDATE()),
('Success.CustomerDeleted', 'tr', 'Müşteri başarıyla silindi', 1, GETUTCDATE()),
('Success.PermissionAssigned', 'tr', 'İzin başarıyla atandı', 1, GETUTCDATE()),
('Success.RoleCreated', 'tr', 'Rol başarıyla oluşturuldu', 1, GETUTCDATE()),
('Success.RoleUpdated', 'tr', 'Rol başarıyla güncellendi', 1, GETUTCDATE()),
('Success.RoleDeleted', 'tr', 'Rol başarıyla silindi', 1, GETUTCDATE());

-- Success Messages - English
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Success.VehicleUpdated', 'en', 'Vehicle information updated successfully', 1, GETUTCDATE()),
('Success.VehicleCreated', 'en', 'Vehicle created successfully', 1, GETUTCDATE()),
('Success.VehicleDeleted', 'en', 'Vehicle deleted successfully', 1, GETUTCDATE()),
('Success.UserCreated', 'en', 'User created successfully', 1, GETUTCDATE()),
('Success.UserUpdated', 'en', 'User information updated successfully', 1, GETUTCDATE()),
('Success.UserDeleted', 'en', 'User deleted successfully', 1, GETUTCDATE()),
('Success.CustomerCreated', 'en', 'Customer created successfully', 1, GETUTCDATE()),
('Success.CustomerUpdated', 'en', 'Customer information updated successfully', 1, GETUTCDATE()),
('Success.CustomerDeleted', 'en', 'Customer deleted successfully', 1, GETUTCDATE()),
('Success.PermissionAssigned', 'en', 'Permission assigned successfully', 1, GETUTCDATE()),
('Success.RoleCreated', 'en', 'Role created successfully', 1, GETUTCDATE()),
('Success.RoleUpdated', 'en', 'Role updated successfully', 1, GETUTCDATE()),
('Success.RoleDeleted', 'en', 'Role deleted successfully', 1, GETUTCDATE());

-- Success Messages - German
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Success.VehicleUpdated', 'de', 'Fahrzeuginformationen erfolgreich aktualisiert', 1, GETUTCDATE()),
('Success.VehicleCreated', 'de', 'Fahrzeug erfolgreich erstellt', 1, GETUTCDATE()),
('Success.VehicleDeleted', 'de', 'Fahrzeug erfolgreich gelöscht', 1, GETUTCDATE()),
('Success.UserCreated', 'de', 'Benutzer erfolgreich erstellt', 1, GETUTCDATE()),
('Success.UserUpdated', 'de', 'Benutzerinformationen erfolgreich aktualisiert', 1, GETUTCDATE()),
('Success.UserDeleted', 'de', 'Benutzer erfolgreich gelöscht', 1, GETUTCDATE()),
('Success.CustomerCreated', 'de', 'Kunde erfolgreich erstellt', 1, GETUTCDATE()),
('Success.CustomerUpdated', 'de', 'Kundeninformationen erfolgreich aktualisiert', 1, GETUTCDATE()),
('Success.CustomerDeleted', 'de', 'Kunde erfolgreich gelöscht', 1, GETUTCDATE()),
('Success.PermissionAssigned', 'de', 'Berechtigung erfolgreich zugewiesen', 1, GETUTCDATE()),
('Success.RoleCreated', 'de', 'Rolle erfolgreich erstellt', 1, GETUTCDATE()),
('Success.RoleUpdated', 'de', 'Rolle erfolgreich aktualisiert', 1, GETUTCDATE()),
('Success.RoleDeleted', 'de', 'Rolle erfolgreich gelöscht', 1, GETUTCDATE());

-- Business Logic Messages - Turkish
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Business.VehicleInUse', 'tr', 'Araç şu anda kullanımda olduğu için silinemez', 1, GETUTCDATE()),
('Business.CustomerHasVehicles', 'tr', 'Müşterinin kayıtlı araçları olduğu için silinemez', 1, GETUTCDATE()),
('Business.InvalidEmailFormat', 'tr', 'Geçersiz e-posta formatı', 1, GETUTCDATE()),
('Business.PasswordTooWeak', 'tr', 'Şifre çok zayıf. En az 8 karakter, büyük harf, küçük harf, rakam ve özel karakter içermelidir', 1, GETUTCDATE()),
('Business.PhoneNumberInvalid', 'tr', 'Geçersiz telefon numarası formatı', 1, GETUTCDATE()),
('Business.IdentityNumberInvalid', 'tr', 'Geçersiz kimlik numarası formatı', 1, GETUTCDATE()),
('Business.LicensePlateInvalid', 'tr', 'Geçersiz plaka formatı. Örnek: 34ABC123', 1, GETUTCDATE()),
('Business.VehicleYearInvalid', 'tr', 'Geçersiz araç yılı. Yıl 1900 ile 2025 arasında olmalıdır', 1, GETUTCDATE());

-- Business Logic Messages - English
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Business.VehicleInUse', 'en', 'Vehicle cannot be deleted as it is currently in use', 1, GETUTCDATE()),
('Business.CustomerHasVehicles', 'en', 'Customer cannot be deleted as they have registered vehicles', 1, GETUTCDATE()),
('Business.InvalidEmailFormat', 'en', 'Invalid email format', 1, GETUTCDATE()),
('Business.PasswordTooWeak', 'en', 'Password is too weak. Must contain at least 8 characters, uppercase, lowercase, number and special character', 1, GETUTCDATE()),
('Business.PhoneNumberInvalid', 'en', 'Invalid phone number format', 1, GETUTCDATE()),
('Business.IdentityNumberInvalid', 'en', 'Invalid identity number format', 1, GETUTCDATE()),
('Business.LicensePlateInvalid', 'en', 'Invalid license plate format. Example: 34ABC123', 1, GETUTCDATE()),
('Business.VehicleYearInvalid', 'en', 'Invalid vehicle year. Year must be between 1900 and 2025', 1, GETUTCDATE());

-- Business Logic Messages - German
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Business.VehicleInUse', 'de', 'Fahrzeug kann nicht gelöscht werden, da es derzeit in Gebrauch ist', 1, GETUTCDATE()),
('Business.CustomerHasVehicles', 'de', 'Kunde kann nicht gelöscht werden, da er registrierte Fahrzeuge hat', 1, GETUTCDATE()),
('Business.InvalidEmailFormat', 'de', 'Ungültiges E-Mail-Format', 1, GETUTCDATE()),
('Business.PasswordTooWeak', 'de', 'Passwort ist zu schwach. Muss mindestens 8 Zeichen, Großbuchstaben, Kleinbuchstaben, Zahlen und Sonderzeichen enthalten', 1, GETUTCDATE()),
('Business.PhoneNumberInvalid', 'de', 'Ungültiges Telefonnummernformat', 1, GETUTCDATE()),
('Business.IdentityNumberInvalid', 'de', 'Ungültiges Ausweisnummernformat', 1, GETUTCDATE()),
('Business.LicensePlateInvalid', 'de', 'Ungültiges Kennzeichenformat. Beispiel: 34ABC123', 1, GETUTCDATE()),
('Business.VehicleYearInvalid', 'de', 'Ungültiges Fahrzeugjahr. Jahr muss zwischen 1900 und 2025 liegen', 1, GETUTCDATE());

-- API Response Messages - Turkish
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Api.Success', 'tr', 'İşlem başarıyla tamamlandı', 1, GETUTCDATE()),
('Api.Error', 'tr', 'İşlem sırasında hata oluştu', 1, GETUTCDATE()),
('Api.NotFound', 'tr', 'Aranan kayıt bulunamadı', 1, GETUTCDATE()),
('Api.Unauthorized', 'tr', 'Bu işlem için yetkiniz bulunmamaktadır', 1, GETUTCDATE()),
('Api.Forbidden', 'tr', 'Bu işlemi gerçekleştirme yetkiniz bulunmamaktadır', 1, GETUTCDATE()),
('Api.BadRequest', 'tr', 'Geçersiz istek', 1, GETUTCDATE()),
('Api.InternalServerError', 'tr', 'Sunucu hatası oluştu', 1, GETUTCDATE()),
('Api.ValidationError', 'tr', 'Doğrulama hatası', 1, GETUTCDATE());

-- API Response Messages - English
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Api.Success', 'en', 'Operation completed successfully', 1, GETUTCDATE()),
('Api.Error', 'en', 'An error occurred during the operation', 1, GETUTCDATE()),
('Api.NotFound', 'en', 'The requested record was not found', 1, GETUTCDATE()),
('Api.Unauthorized', 'en', 'You do not have permission for this operation', 1, GETUTCDATE()),
('Api.Forbidden', 'en', 'You do not have permission to perform this operation', 1, GETUTCDATE()),
('Api.BadRequest', 'en', 'Invalid request', 1, GETUTCDATE()),
('Api.InternalServerError', 'en', 'Server error occurred', 1, GETUTCDATE()),
('Api.ValidationError', 'en', 'Validation error', 1, GETUTCDATE());

-- API Response Messages - German
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Api.Success', 'de', 'Vorgang erfolgreich abgeschlossen', 1, GETUTCDATE()),
('Api.Error', 'de', 'Während des Vorgangs ist ein Fehler aufgetreten', 1, GETUTCDATE()),
('Api.NotFound', 'de', 'Der angeforderte Datensatz wurde nicht gefunden', 1, GETUTCDATE()),
('Api.Unauthorized', 'de', 'Sie haben keine Berechtigung für diesen Vorgang', 1, GETUTCDATE()),
('Api.Forbidden', 'de', 'Sie haben keine Berechtigung, diesen Vorgang auszuführen', 1, GETUTCDATE()),
('Api.BadRequest', 'de', 'Ungültige Anfrage', 1, GETUTCDATE()),
('Api.InternalServerError', 'de', 'Serverfehler aufgetreten', 1, GETUTCDATE()),
('Api.ValidationError', 'de', 'Validierungsfehler', 1, GETUTCDATE());

-- Örnek kullanım için test verileri
INSERT INTO Translations (Key, Language, Value, Status, CreatedDate) VALUES 
('Test.Welcome', 'tr', 'Hoş geldiniz!', 1, GETUTCDATE()),
('Test.Welcome', 'en', 'Welcome!', 1, GETUTCDATE()),
('Test.Welcome', 'de', 'Willkommen!', 1, GETUTCDATE()),
('Test.Goodbye', 'tr', 'Güle güle!', 1, GETUTCDATE()),
('Test.Goodbye', 'en', 'Goodbye!', 1, GETUTCDATE()),
('Test.Goodbye', 'de', 'Auf Wiedersehen!', 1, GETUTCDATE());

-- Veri ekleme işlemi tamamlandı
PRINT 'Translation seed data başarıyla eklendi!';
PRINT 'Toplam ' + CAST(@@ROWCOUNT AS VARCHAR(10)) + ' kayıt eklendi.';
