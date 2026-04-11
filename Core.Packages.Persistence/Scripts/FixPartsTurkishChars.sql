-- Fix Turkish character encoding in Parts table
-- Migration file 20251129124249_AddPartManagement.cs was ISO-8859-9 encoded
-- causing ğ,Ğ,ı,İ,ş,Ş,ö,Ö,ü,Ü to be stored as garbled characters.
-- This script restores all affected rows to correct Unicode values.

UPDATE Parts SET
    Name        = N'Fren Balata Seti (Ön)',
    Description = N'Ön fren balata seti - Bosch marka',
    Notes       = N'Yüksek kaliteli fren balata seti'
WHERE Id = 1;

UPDATE Parts SET
    Name        = N'Fren Balata Seti (Arka)',
    Description = N'Arka fren balata seti - Bosch marka'
WHERE Id = 2;

UPDATE Parts SET
    Name        = N'Hava Filtresi',
    Description = N'Standart hava filtresi'
WHERE Id = 3;

UPDATE Parts SET
    Name        = N'Yağ Filtresi',
    Description = N'Motor yağ filtresi'
WHERE Id = 4;

UPDATE Parts SET
    Name        = N'Motor Yağı 5W-30 (5L)',
    Description = N'Sentetik motor yağı',
    Notes       = N'Yüksek performanslı sentetik yağ'
WHERE Id = 5;

UPDATE Parts SET
    Name        = N'Ön Amortisör',
    Description = N'Ön amortisör - sol'
WHERE Id = 6;

UPDATE Parts SET
    Name        = N'Akü 12V 70Ah',
    Description = N'Otomotiv aküsü',
    Notes       = N'Yüksek kapasiteli akü'
WHERE Id = 7;

UPDATE Parts SET
    Name        = N'Far Ampulü H7',
    Description = N'Halojen far ampulü'
WHERE Id = 8;

UPDATE Parts SET
    Name        = N'Buji Seti (4''lü)',
    Description = N'Iridium buji seti',
    Notes       = N'Uzun ömürlü iridium buji'
WHERE Id = 9;

UPDATE Parts SET
    Name        = N'Lastik 215/60 R16',
    Description = N'Yaz lastiği',
    Notes       = N'Yüksek performanslı yaz lastiği'
WHERE Id = 10;

-- Verify results
SELECT Id, Name, Description, Notes
FROM Parts
WHERE Id BETWEEN 1 AND 10
ORDER BY Id;
