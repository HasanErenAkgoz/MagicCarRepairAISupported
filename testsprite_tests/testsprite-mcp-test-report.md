# TestSprite Backend Test Report — 2026-05-28 (Run 3)

## 1️⃣ Document Metadata

| Alan | Değer |
|------|--------|
| **Project** | MagicCarRepairAISupported |
| **API** | http://localhost:5169 |
| **DB seed** | `ensure_db_seed.py` (manager + portal + manifest hedefleri) |
| **Yerel doğrulama** | `python run_all_tc.py` → **40/40 passed** (TC032–TC040 dahil) |
| **TestSprite bulut** | **20/40 passed (50%)** — [Dashboard](https://www.testsprite.com/dashboard/mcp/tests/f888914f-f36e-4247-baa2-cee73cf5a0d7/) |

## 2️⃣ Yapılan düzeltmeler

### Manager seed (`ensure_db_seed.py`)

- `manager@magiccar.com` / `Manager123!` — yoksa `POST /api/user` ile `userType: 2`, `clientId: 1`
- `scenario_seed_manifest.json` → `manager_test_users: 1`, modül `M03_rbac`

### TC dosyaları (`generate_mcp_tc_files.py`)

40 TC dosyası standart şablonla yenilendi:

- JWT: yalnızca `response.json()["data"]["token"]`
- Doğru payload’lar (Customers, Vehicles, Parts, Employees)
- TC007: `PUT .../status` + `newStatus`
- TC011: `POST /api/Invoices/from-workorder/{id}`
- TC027/028: gerçek manager login
- TC031: ayrı dashboard chart endpoint’leri
- TC035: `/api/accounting-reports/cash-flow`
- TC021/038: `register-customer`
- Timeout: 90s (tünel gecikmesi için)

`testsprite_backend_test_plan.json` açıklamaları aynı kurallarla güncellendi.

## 3️⃣ Sonuç özeti

| Ortam | Geçen | Oran | Not |
|-------|-------|------|-----|
| **Yerel** (`run_all_tc.py`) | 40/40 | 100% | Düzeltilmiş TC + seed |
| **TestSprite bulut** | 20/40 | 50% | Tünel `ReadTimeout`, AI test kodu üretimi |

TestSprite MCP, testleri **bulutta yeniden üretir**; yerel `TC*.py` dosyalarını çalıştırmaz. Bu yüzden bulut koşusu önceki %65’ten düşebilir (tünel kapandı uyarısı + 30s timeout).

Bulutta başarısız olanların çoğu: `proxy.tun.testsprite.com` üzerinden **ReadTimeout** veya AI’nın hâlâ yanlış ürettiği kod — API regresyonu değil.

## 4️⃣ TC032–TC040 (önceki 9 yerel hata)

| TC | Senaryo | Durum |
|----|---------|--------|
| TC032 | `GET /api/Customers/99999999` → 404 | Geçiyor; VS **DomainException** break açıksa istek askıda kalır |
| TC033–TC040 | Parts, suppliers, accounting, sync, portal, RBAC, invoices | Geçiyor (seed + doğru route) |

`run_all_tc.py` test başına **150s** subprocess timeout kullanır; API debugger’da takılırsa tüm suite çökmez.

## 5️⃣ Key Gaps / Risks

1. **Güvenilir doğrulama:** `scripts\run-testsprite.cmd` veya `ensure_db_seed.py` + `run_all_tc.py`.
2. **Visual Studio:** TC032 sırasında `CUSTOMER_NOT_FOUND` için “break when thrown” kapatın veya Continue.
3. **TestSprite bulut:** Tünel `ReadTimeout` ve AI’nın `accessToken` / yanlış URL üretimi; `tmp/config.json` `additionalInstruction` güncellendi.
4. **TC dosyalarını koruma:** Bulut koşusundan sonra `python generate_mcp_tc_files.py`.

## Komutlar

```powershell
cd c:\Projects\MagicCarRepairAISupported\testsprite_tests
python ensure_db_seed.py
python generate_mcp_tc_files.py
python run_all_tc.py
```
