-- Run on existing databases (seed only applies on initial migration).
INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_FOUND', 'tr', 'Parça bulunamadı (Id: {Id}, PartId: {PartId}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_FOUND' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_FOUND', 'en', 'Part not found (Id: {Id}, PartId: {PartId}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_FOUND' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_BELONG_TO_CLIENT', 'tr', 'Bu parça kaydına erişim yetkiniz yok.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_BELONG_TO_CLIENT' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_BELONG_TO_CLIENT', 'en', 'You do not have access to this part record.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_BELONG_TO_CLIENT' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_CODE_EXISTS', 'tr', '{PartCode} kodlu parça zaten mevcut.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_CODE_EXISTS' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_CODE_EXISTS', 'en', 'A part with code {PartCode} already exists.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_CODE_EXISTS' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_FOUND_BY_BARCODE', 'tr', 'Barkoda ait parça bulunamadı: {Barcode}.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_FOUND_BY_BARCODE' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NOT_FOUND_BY_BARCODE', 'en', 'No part found for barcode: {Barcode}.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NOT_FOUND_BY_BARCODE' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_ID_REQUIRED', 'tr', 'Bu kalem türü için parça seçilmelidir ({ItemType}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_ID_REQUIRED' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_ID_REQUIRED', 'en', 'A part must be selected for this item type ({ItemType}).', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_ID_REQUIRED' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_INSUFFICIENT_STOCK', 'tr', '{PartName} için yetersiz stok. İstenen: {RequestedQuantity}, Mevcut: {AvailableQuantity}.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_INSUFFICIENT_STOCK' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_INSUFFICIENT_STOCK', 'en', 'Insufficient stock for {PartName}. Requested: {RequestedQuantity}, Available: {AvailableQuantity}.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_INSUFFICIENT_STOCK' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_USED_IN_ACTIVE_WORKORDERS', 'tr', '{PartCode} kodlu parça aktif iş emirlerinde kullanılıyor.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_USED_IN_ACTIVE_WORKORDERS' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_USED_IN_ACTIVE_WORKORDERS', 'en', 'Part {PartCode} is used in active work orders.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_USED_IN_ACTIVE_WORKORDERS' AND "Language" = 'en');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NAME_REQUIRED', 'tr', 'Parça adı zorunludur.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NAME_REQUIRED' AND "Language" = 'tr');

INSERT INTO "ErrorMessages" ("ErrorCode", "Language", "Message", "Status", "CreatedDate", "CreatedBy")
SELECT 'PART_NAME_REQUIRED', 'en', 'Part name is required.', 1, NOW(), 0
WHERE NOT EXISTS (
    SELECT 1 FROM "ErrorMessages" WHERE "ErrorCode" = 'PART_NAME_REQUIRED' AND "Language" = 'en');
