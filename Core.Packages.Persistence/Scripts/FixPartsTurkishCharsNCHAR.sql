-- Fix Turkish character encoding in Parts table using NCHAR() to avoid encoding issues.
-- NCHAR(214)=O-uml, NCHAR(246)=o-uml, NCHAR(252)=u-uml, NCHAR(220)=U-uml
-- NCHAR(287)=g-breve, NCHAR(286)=G-breve, NCHAR(305)=dotless-i, NCHAR(304)=I-dot

-- Id=1: Fren Balata Seti (On), On fren..., Yuksek kaliteli...
UPDATE Parts SET
    Name        = N'Fren Balata Seti (' + NCHAR(214) + N'n)',
    Description = NCHAR(214) + N'n fren balata seti - Bosch marka',
    Notes       = N'Y' + NCHAR(252) + N'ksek kaliteli fren balata seti'
WHERE Id = 1;

-- Id=4: Yag Filtresi, Motor yag filtresi
UPDATE Parts SET
    Name        = N'Ya' + NCHAR(287) + N' Filtresi',
    Description = N'Motor ya' + NCHAR(287) + N' filtresi'
WHERE Id = 4;

-- Id=5: Motor Yagi 5W-30 (5L), Sentetik motor yagi, Yuksek performansli sentetik yag
UPDATE Parts SET
    Name        = N'Motor Ya' + NCHAR(287) + NCHAR(305) + N' 5W-30 (5L)',
    Description = N'Sentetik motor ya' + NCHAR(287) + NCHAR(305),
    Notes       = N'Y' + NCHAR(252) + N'ksek performansl' + NCHAR(305) + N' sentetik ya' + NCHAR(287)
WHERE Id = 5;

-- Id=6: On Amortisor, On amortisor - sol
UPDATE Parts SET
    Name        = NCHAR(214) + N'n Amortis' + NCHAR(246) + N'r',
    Description = NCHAR(214) + N'n amortis' + NCHAR(246) + N'r - sol'
WHERE Id = 6;

-- Id=7: Aku 12V 70Ah, Otomotiv akusu, Yuksek kapasiteli aku
UPDATE Parts SET
    Name        = N'Ak' + NCHAR(252) + N' 12V 70Ah',
    Description = N'Otomotiv ak' + NCHAR(252) + N's' + NCHAR(252),
    Notes       = N'Y' + NCHAR(252) + N'ksek kapasiteli ak' + NCHAR(252)
WHERE Id = 7;

-- Id=8: Far Ampulu H7, Halojen far ampulu
UPDATE Parts SET
    Name        = N'Far Ampul' + NCHAR(252) + N' H7',
    Description = N'Halojen far ampul' + NCHAR(252)
WHERE Id = 8;

-- Id=9: Buji Seti (4'lu), Iridium buji seti, Uzun omurlu iridium buji
UPDATE Parts SET
    Name        = N'Buji Seti (4' + NCHAR(39) + N'l' + NCHAR(252) + N')',
    Description = N'Iridium buji seti',
    Notes       = N'Uzun ' + NCHAR(246) + N'm' + NCHAR(252) + N'rl' + NCHAR(252) + N' iridium buji'
WHERE Id = 9;

-- Id=10: Lastik 215/60 R16, Yaz lastigi, Yuksek performansli yaz lastigi
UPDATE Parts SET
    Name        = N'Lastik 215/60 R16',
    Description = N'Yaz lasti' + NCHAR(287) + NCHAR(305),
    Notes       = N'Y' + NCHAR(252) + N'ksek performansl' + NCHAR(305) + N' yaz lasti' + NCHAR(287) + NCHAR(305)
WHERE Id = 10;

-- Verify
SELECT Id, Name, Description FROM Parts WHERE Id BETWEEN 1 AND 10 ORDER BY Id;
