-- Run on existing databases (seed only applies on initial migration).
INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NOT_FOUND', 'tr', 'Müşteri bulunamadı (Id: {Id}, CustomerId: {CustomerId}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NOT_FOUND' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NOT_FOUND', 'en', 'Customer not found (Id: {Id}, CustomerId: {CustomerId}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NOT_FOUND' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NOT_BELONG_TO_CLIENT', 'tr', 'Bu müşteri kaydına erişim yetkiniz yok.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NOT_BELONG_TO_CLIENT' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NOT_BELONG_TO_CLIENT', 'en', 'You do not have access to this customer record.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NOT_BELONG_TO_CLIENT' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_EMAIL_EXISTS', 'tr', '{Email} e-posta adresi ile kayıtlı müşteri zaten mevcut.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_EMAIL_EXISTS' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_EMAIL_EXISTS', 'en', 'A customer with email {Email} already exists.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_EMAIL_EXISTS' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_IDENTITY_NO_EXISTS', 'tr', '{IdentityNo} kimlik numarası ile kayıtlı müşteri zaten mevcut.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_IDENTITY_NO_EXISTS' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_IDENTITY_NO_EXISTS', 'en', 'A customer with identity number {IdentityNo} already exists.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_IDENTITY_NO_EXISTS' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NAME_REQUIRED', 'tr', 'Müşteri adı zorunludur.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NAME_REQUIRED' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_NAME_REQUIRED', 'en', 'Customer name is required.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_NAME_REQUIRED' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_PHONE_REQUIRED', 'tr', 'Müşteri telefonu zorunludur.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_PHONE_REQUIRED' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'CUSTOMER_PHONE_REQUIRED', 'en', 'Customer phone number is required.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'CUSTOMER_PHONE_REQUIRED' AND "Language" = 'en');
