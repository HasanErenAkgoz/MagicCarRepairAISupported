-- Anchor customer Id=1 for TestSprite / loyalty tests (Users.Id=1 is admin, not a customer).
INSERT INTO "Customers" (
    "Id", "ClientId", "IdentityNo", "FirstName", "LastName", "Email", "PhoneNumber",
    "Address", "DateTimeOfBirth", "Language", "IsVip", "Status", "CreatedDate", "CreatedBy"
)
SELECT
    1, 1, 'TSANCHOR01', 'TestSprite', 'Anchor', 'testsprite_anchor@example.com', '5550000100',
    '', '1990-01-01T00:00:00Z', 'tr', false, 1, NOW(), 1
WHERE NOT EXISTS (SELECT 1 FROM "Customers" WHERE "Id" = 1);

SELECT setval('"Customers_Id_seq"', (SELECT COALESCE(MAX("Id"), 1) FROM "Customers"));
