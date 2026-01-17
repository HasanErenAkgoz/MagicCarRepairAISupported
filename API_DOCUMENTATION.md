# MagicCarRepairAISupported - API Dokümantasyonu

> **Base URL**: `http://localhost:5000/api`  
> **API Version**: 1.0  
> **Son Güncelleme**: 2024

---

## 📋 İçindekiler

1. [Authentication](#authentication)
2. [Multi-Tenant](#multi-tenant)
3. [Hata Yönetimi](#hata-yönetimi)
4. [Endpoint'ler](#endpointler)
5. [Request/Response Örnekleri](#requestresponse-örnekleri)

---

## 🔐 Authentication

### JWT Token Alımı

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2024-01-02T10:00:00Z",
    "user": {
      "id": 1,
      "email": "user@example.com",
      "firstName": "Ahmet",
      "lastName": "Yılmaz"
    }
  }
}
```

### Token Kullanımı

Tüm korumalı endpoint'lerde `Authorization` header'ı gerekir:

```
Authorization: Bearer {token}
```

### Token Yenileme

Şu anda token yenileme endpoint'i yok. Token süresi dolduğunda tekrar login yapılmalıdır.

---

## 🏢 Multi-Tenant

### Client ID Belirtme

Her istekte `X-Client-Id` header'ı gönderilmelidir:

```
X-Client-Id: 1
```

**Alternatif**: JWT token içinde `ClientId` claim'i varsa header'a gerek yoktur.

### Veri İzolasyonu

Her Client (oto servis) sadece kendi verilerini görebilir. Global query filter'lar otomatik olarak `WHERE ClientId = X` ekler.

---

## ⚠️ Hata Yönetimi

### Hata Yanıt Formatı

```json
{
  "success": false,
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "Araç bulunamadı",
  "details": {
    "VehicleId": 999
  }
}
```

### HTTP Status Kodları

- `200 OK` - Başarılı
- `201 Created` - Kayıt oluşturuldu
- `400 Bad Request` - Geçersiz istek
- `401 Unauthorized` - Yetkisiz erişim
- `403 Forbidden` - İzin yok
- `404 Not Found` - Kayıt bulunamadı
- `500 Internal Server Error` - Sunucu hatası

### Yaygın Hata Kodları

- `CLIENT_ID_REQUIRED` - ClientId belirtilmemiş
- `VEHICLE_NOT_FOUND` - Araç bulunamadı
- `CUSTOMER_NOT_FOUND` - Müşteri bulunamadı
- `WORKORDER_NOT_FOUND` - İş emri bulunamadı
- `INSUFFICIENT_STOCK` - Yetersiz stok
- `UNAUTHORIZED_ACCESS` - Yetkisiz erişim

---

## 🔌 Endpoint'ler

### Authentication

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "string",
  "password": "string"
}
```

#### Register
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "string",
  "password": "string",
  "firstName": "string",
  "lastName": "string",
  "clientId": 0,
  "userType": "Manager"
}
```

#### Forgot Password
```http
POST /api/auth/forgot-password
Content-Type: application/json

{
  "email": "string"
}
```

#### Reset Password
```http
POST /api/auth/reset-password
Content-Type: application/json

{
  "email": "string",
  "token": "string",
  "newPassword": "string"
}
```

---

### Clients (Tenant Yönetimi)

#### Create Client
```http
POST /api/clients
Authorization: Bearer {token}

{
  "name": "string",
  "code": "string",
  "description": "string",
  "contactEmail": "string",
  "contactPhone": "string",
  "subscriptionStartDate": "2024-01-01",
  "subscriptionEndDate": "2025-01-01"
}
```

#### Get Client By Id
```http
GET /api/clients/{id}
Authorization: Bearer {token}
```

---

### Customers (Müşteri Yönetimi)

#### Get All Customers
```http
GET /api/customers?searchTerm={term}&pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Customer By Id
```http
GET /api/customers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Customer
```http
POST /api/customers
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string",
  "address": "string"
}
```

#### Update Customer
```http
PUT /api/customers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phone": "string"
}
```

#### Delete Customer
```http
DELETE /api/customers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Vehicles (Araç Yönetimi)

#### Get All Vehicles
```http
GET /api/vehicles?customerId={id}&searchTerm={term}&pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Vehicle By Id
```http
GET /api/vehicles/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Vehicles By Customer
```http
GET /api/vehicles/customer/{customerId}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Vehicle
```http
POST /api/vehicles
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "customerId": 0,
  "licensePlate": "string",
  "brand": "string",
  "model": "string",
  "year": 0,
  "color": "string",
  "kilometers": 0
}
```

#### Update Vehicle
```http
PUT /api/vehicles/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "licensePlate": "string",
  "brand": "string",
  "model": "string",
  "year": 0,
  "kilometers": 0
}
```

#### Delete Vehicle
```http
DELETE /api/vehicles/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Employees (Personel Yönetimi)

#### Get All Employees
```http
GET /api/employees?employmentStatus={status}&position={position}&pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

**Query Parameters:**
- `employmentStatus`: `Active`, `OnLeave`, `Terminated`, `Suspended`
- `position`: `Manager`, `Mechanic`, `Electrician`, vb.

#### Get Employee By Id
```http
GET /api/employees/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Employees By Position
```http
GET /api/employees/by-position/{position}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Employee
```http
POST /api/employees
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "employeeNo": "string",
  "firstName": "string",
  "lastName": "string",
  "position": "Mechanic",
  "phone": "string",
  "email": "string",
  "hireDate": "2024-01-01",
  "employmentStatus": "Active"
}
```

#### Update Employee
```http
PUT /api/employees/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "firstName": "string",
  "lastName": "string",
  "position": "Mechanic",
  "phone": "string"
}
```

#### Delete Employee
```http
DELETE /api/employees/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### WorkOrders (İş Emri Yönetimi)

#### Get All WorkOrders
```http
GET /api/workorders?status={status}&customerId={id}&vehicleId={id}&employeeId={id}&startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

**Query Parameters:**
- `status`: `VehicleEntered`, `InProgress`, `WaitingForParts`, `QualityControl`, `Completed`, `Delivered`, `Cancelled`
- `customerId`: Müşteri ID
- `vehicleId`: Araç ID
- `employeeId`: Personel ID
- `startDate`: Başlangıç tarihi (ISO 8601)
- `endDate`: Bitiş tarihi (ISO 8601)

#### Get Active WorkOrders
```http
GET /api/workorders/active
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get WorkOrder By Id
```http
GET /api/workorders/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get WorkOrder Timeline
```http
GET /api/workorders/{id}/timeline
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create WorkOrder
```http
POST /api/workorders
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "vehicleId": 0,
  "customerId": 0,
  "assignedEmployeeId": 0,
  "priority": "Normal",
  "description": "string",
  "customerComplaints": "string",
  "estimatedCompletionDate": "2024-01-15T17:00:00Z"
}
```

**Priority Values:** `Low`, `Normal`, `Urgent`

#### Update WorkOrder Status
```http
PUT /api/workorders/{id}/status
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "status": "InProgress",
  "notes": "string"
}
```

#### Complete WorkOrder
```http
POST /api/workorders/{id}/complete
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "completionNotes": "string",
  "actualCompletionDate": "2024-01-12T16:00:00Z"
}
```

#### Deliver WorkOrder
```http
POST /api/workorders/{id}/deliver
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "deliveryNotes": "string",
  "deliveryDate": "2024-01-12T17:00:00Z"
}
```

#### Add WorkOrder Item
```http
POST /api/workorders/{id}/items
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "itemType": "Part",
  "partId": 0,
  "quantity": 0,
  "unitPrice": 0,
  "discount": 0,
  "description": "string"
}
```

**ItemType Values:** `Part`, `Labor`, `ExternalService`

#### Remove WorkOrder Item
```http
DELETE /api/workorders/{id}/items/{itemId}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Add WorkOrder Labor
```http
POST /api/workorders/{id}/labors
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "employeeId": 0,
  "workDescription": "string",
  "startTime": "2024-01-10T14:00:00Z",
  "endTime": "2024-01-10T16:00:00Z",
  "hourlyRate": 0
}
```

#### Add WorkOrder Photo
```http
POST /api/workorders/{id}/photos
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: multipart/form-data

{
  "file": [binary],
  "description": "string",
  "photoType": "Process"
}
```

**PhotoType Values:** `Entry`, `Process`, `Exit`

---

### Parts (Parça Yönetimi)

#### Get All Parts
```http
GET /api/parts?pageNumber={page}&pageSize={size}&searchTerm={term}&category={category}&brandType={type}&lowStockOnly={bool}
Authorization: Bearer {token}
X-Client-Id: 1
```

**Query Parameters:**
- `category`: `Engine`, `Brake`, `Suspension`, `Electrical`, `Body`, vb.
- `brandType`: `Original`, `Equivalent`
- `lowStockOnly`: `true` / `false`

#### Get Part By Id
```http
GET /api/parts/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Low Stock Parts
```http
GET /api/parts/low-stock
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Part
```http
POST /api/parts
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "partCode": "string",
  "name": "string",
  "category": "Brake",
  "brandType": "Original",
  "purchasePrice": 0,
  "salePrice": 0,
  "taxRate": 0,
  "minimumStockLevel": 0,
  "barcode": "string"
}
```

#### Update Part
```http
PUT /api/parts/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "name": "string",
  "salePrice": 0,
  "minimumStockLevel": 0
}
```

#### Update Part Stock
```http
PATCH /api/parts/{id}/stock
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "quantity": 0,
  "notes": "string"
}
```

#### Delete Part
```http
DELETE /api/parts/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Stock Movements (Stok Hareketleri)

#### Record Stock Movement
```http
POST /api/stock-movements
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "partId": 0,
  "movementType": "In",
  "quantity": 0,
  "notes": "string"
}
```

**MovementType Values:** `In`, `Out`, `Adjustment`, `Return`

#### Get Stock Movement History
```http
GET /api/stock-movements/history?partId={id}&movementType={type}&startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Stock Alerts (Stok Alarmları)

#### Get Active Stock Alerts
```http
GET /api/stock-alerts/active
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Auto Orders (Otomatik Siparişler)

#### Create Auto Order
```http
POST /api/auto-orders
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "partId": 0,
  "supplierId": 0,
  "quantity": 0,
  "notes": "string"
}
```

#### Approve Auto Order
```http
POST /api/auto-orders/{id}/approve
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Auto Order History
```http
GET /api/auto-orders/history?partId={id}&status={status}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Invoices (Faturalama)

#### Get All Invoices
```http
GET /api/invoices?pageNumber={page}&pageSize={size}&status={status}&customerId={id}
Authorization: Bearer {token}
X-Client-Id: 1
```

**Status Values:** `Pending`, `Paid`, `Overdue`, `Cancelled`

#### Get Invoice By Id
```http
GET /api/invoices/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Overdue Invoices
```http
GET /api/invoices/overdue?pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Invoice
```http
POST /api/invoices
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "invoiceType": "Sales",
  "workOrderId": 0,
  "customerId": 0,
  "invoiceDate": "2024-01-12",
  "dueDate": "2024-02-12",
  "description": "string",
  "items": [
    {
      "description": "string",
      "quantity": 0,
      "unitPrice": 0,
      "taxRate": 0
    }
  ]
}
```

**InvoiceType Values:** `Sales`, `Purchase`

#### Generate Invoice From WorkOrder
```http
POST /api/invoices/from-workorder/{workOrderId}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "invoiceDate": "2024-01-12",
  "dueDate": "2024-02-12"
}
```

#### Update Invoice Status
```http
PUT /api/invoices/{id}/status
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "status": "Paid"
}
```

#### Generate Invoice PDF
```http
GET /api/invoices/{id}/pdf
Authorization: Bearer {token}
X-Client-Id: 1
```

**Response:** PDF file (application/pdf)

#### Generate Invoice QR Code
```http
GET /api/invoices/{id}/qrcode?size=300
Authorization: Bearer {token}
X-Client-Id: 1
```

**Response:** PNG image (image/png)

#### Send Invoice By Email
```http
POST /api/invoices/{id}/send-email
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "email": "string",
  "subject": "string",
  "message": "string"
}
```

#### Export Invoice To Excel
```http
GET /api/invoices/{id}/excel
Authorization: Bearer {token}
X-Client-Id: 1
```

**Response:** Excel file (application/vnd.openxmlformats-officedocument.spreadsheetml.sheet)

---

### Payments (Ödeme Yönetimi)

#### Initialize Payment
```http
POST /api/payments/initialize
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "invoiceId": 0,
  "amount": 0,
  "paymentMethod": "CreditCard",
  "installmentCount": 0
}
```

**PaymentMethod Values:** `Cash`, `CreditCard`, `BankTransfer`, `Check`

#### Handle Payment Callback
```http
POST /api/payments/callback
Content-Type: application/json

{
  "paymentId": "string",
  "status": "Success",
  "transactionId": "string"
}
```

**Not:** Bu endpoint `[AllowAnonymous]` - İyzico'dan gelen callback için

#### Refund Payment
```http
POST /api/payments/refund
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "paymentId": 0,
  "amount": 0,
  "reason": "string"
}
```

#### Get Installment Options
```http
GET /api/payments/installment-options?amount={amount}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Payment History
```http
GET /api/payments/history?invoiceId={id}&startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Income (Gelir Yönetimi)

#### Get All Incomes
```http
GET /api/incomes?pageNumber={page}&pageSize={size}&startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Income By Id
```http
GET /api/incomes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Income
```http
POST /api/incomes
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "workOrderId": 0,
  "incomeType": "WorkOrder",
  "amount": 0,
  "paymentMethod": "CreditCard",
  "date": "2024-01-12",
  "description": "string",
  "invoiceNumber": "string"
}
```

#### Update Income
```http
PUT /api/incomes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "amount": 0,
  "description": "string"
}
```

#### Delete Income
```http
DELETE /api/incomes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Expenses (Gider Yönetimi)

#### Get All Expenses
```http
GET /api/expenses?pageNumber={page}&pageSize={size}&startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Expense By Id
```http
GET /api/expenses/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Expense
```http
POST /api/expenses
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "expenseType": "PartPurchase",
  "amount": 0,
  "paymentMethod": "BankTransfer",
  "date": "2024-01-10",
  "supplierId": 0,
  "description": "string",
  "invoiceNumber": "string"
}
```

#### Update Expense
```http
PUT /api/expenses/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "amount": 0,
  "description": "string"
}
```

#### Delete Expense
```http
DELETE /api/expenses/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Accounting Reports (Muhasebe Raporları)

#### Get Income Report
```http
GET /api/accounting-reports/income?startDate={date}&endDate={date}&incomeType={type}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Expense Report
```http
GET /api/accounting-reports/expense?startDate={date}&endDate={date}&expenseType={type}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Profit Loss Report
```http
GET /api/accounting-reports/profit-loss?startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Salary Payments (Maaş Ödemeleri)

#### Get All Salary Payments
```http
GET /api/salarypayments?employeeId={id}&year={year}&month={month}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Salary Payment By Id
```http
GET /api/salarypayments/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Salary Payment
```http
POST /api/salarypayments
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "employeeId": 1,
  "month": 1,
  "year": 2024,
  "grossSalary": 10000,
  "socialSecurityDeduction": 1400,
  "unemploymentInsuranceDeduction": 100,
  "incomeTaxDeduction": 1500,
  "stampTax": 50,
  "otherDeductions": 0,
  "paymentDate": "2024-01-31T00:00:00Z",
  "paymentMethod": "Cash",
  "description": "Ocak 2024 maaş ödemesi",
  "paymentReferenceNumber": "REF-001"
}
```

**PaymentMethod Values:** `Cash`, `BankTransfer`, `CreditCard`, `Check`

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "employeeId": 1,
    "employeeName": "Ahmet Yılmaz",
    "month": 1,
    "year": 2024,
    "grossSalary": 10000,
    "socialSecurityDeduction": 1400,
    "unemploymentInsuranceDeduction": 100,
    "incomeTaxDeduction": 1500,
    "stampTax": 50,
    "otherDeductions": 0,
    "netSalary": 6950,
    "paymentDate": "2024-01-31T00:00:00Z",
    "paymentMethod": "Cash",
    "paymentMethodName": "Cash",
    "description": "Ocak 2024 maaş ödemesi",
    "paymentReferenceNumber": "REF-001",
    "createdDate": "2024-01-31T10:00:00Z"
  }
}
```

**Not:** Maaş ödemesi yapıldığında otomatik olarak bir gider kaydı oluşturulur.

#### Update Salary Payment
```http
PUT /api/salarypayments/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "grossSalary": 11000,
  "socialSecurityDeduction": 1540,
  "description": "Güncellenmiş maaş ödemesi"
}
```

#### Delete Salary Payment
```http
DELETE /api/salarypayments/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Taxes (Vergi Yönetimi)

#### Get All Taxes
```http
GET /api/taxes?taxType={type}&status={status}&year={year}&month={month}
Authorization: Bearer {token}
X-Client-Id: 1
```

**TaxType Values:** `VAT` (KDV), `IncomeTax` (Gelir Vergisi), `CorporateTax` (Kurumlar Vergisi), `StampTax` (Damga Vergisi), `Other`

**TaxStatus Values:** `Pending`, `Paid`, `Overdue`, `Cancelled`

#### Get Tax By Id
```http
GET /api/taxes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Tax
```http
POST /api/taxes
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "taxType": "VAT",
  "month": 1,
  "year": 2024,
  "amount": 10000,
  "dueDate": "2024-02-15T00:00:00Z",
  "description": "Ocak 2024 KDV",
  "taxOffice": "Kadıköy Vergi Dairesi",
  "taxNumber": "TAX-001"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "taxType": "VAT",
    "taxTypeName": "VAT",
    "month": 1,
    "year": 2024,
    "amount": 10000,
    "dueDate": "2024-02-15T00:00:00Z",
    "status": "Pending",
    "statusName": "Pending",
    "description": "Ocak 2024 KDV",
    "taxOffice": "Kadıköy Vergi Dairesi",
    "taxNumber": "TAX-001",
    "createdDate": "2024-01-31T10:00:00Z"
  }
}
```

#### Update Tax
```http
PUT /api/taxes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "amount": 12000,
  "dueDate": "2024-02-20T00:00:00Z",
  "description": "Güncellenmiş KDV"
}
```

#### Pay Tax
```http
POST /api/taxes/{id}/pay
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "paymentDate": "2024-02-10T00:00:00Z",
  "paymentMethod": "BankTransfer",
  "paymentReferenceNumber": "BANK-REF-001"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "taxType": "VAT",
    "taxTypeName": "VAT",
    "status": "Paid",
    "statusName": "Paid",
    "paymentDate": "2024-02-10T00:00:00Z",
    "paymentMethod": "BankTransfer",
    "paymentMethodName": "BankTransfer",
    "paymentReferenceNumber": "BANK-REF-001"
  }
}
```

**Not:** Vergi ödendiğinde otomatik olarak bir gider kaydı oluşturulur.

#### Delete Tax
```http
DELETE /api/taxes/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Insurance (Sigorta Yönetimi)

#### Get All Insurance Companies
```http
GET /api/insurance/companies?isActive={bool}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Insurance Company
```http
POST /api/insurance/companies
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "companyName": "string",
  "companyCode": "string",
  "contactPhone": "string",
  "contactEmail": "string",
  "address": "string",
  "isActive": true
}
```

#### Update Insurance Company
```http
PUT /api/insurance/companies/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "companyName": "string",
  "contactPhone": "string",
  "isActive": true
}
```

#### Create Insurance Policy
```http
POST /api/insurance/policies
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "policyNumber": "string",
  "vehicleId": 0,
  "customerId": 0,
  "insuranceCompanyId": 0,
  "insuranceType": "Comprehensive",
  "startDate": "2024-01-01",
  "endDate": "2025-01-01",
  "premiumAmount": 0,
  "coverageAmount": 0,
  "deductiblePercentage": 0
}
```

**InsuranceType Values:** `Comprehensive`, `ThirdParty`, `Traffic`, `Exemption`

#### Renew Insurance Policy
```http
POST /api/insurance/policies/{id}/renew
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "newEndDate": "2026-01-01",
  "premiumAmount": 0
}
```

#### Get Expiring Policies
```http
GET /api/insurance/policies/expiring?daysBeforeExpiration=30
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Insurance Policies By Vehicle
```http
GET /api/insurance/policies/vehicle/{vehicleId}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Insurance Policies By Customer
```http
GET /api/insurance/policies/customer/{customerId}?activeOnly={bool}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Insurance Claim
```http
POST /api/insurance/claims
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "claimNumber": "string",
  "insurancePolicyId": 0,
  "workOrderId": 0,
  "incidentDate": "2024-01-10",
  "description": "string",
  "claimAmount": 0,
  "photos": "[]"
}
```

#### Update Insurance Claim Status
```http
PUT /api/insurance/claims/{id}/status
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "status": "Approved",
  "approvedAmount": 0,
  "notes": "string"
}
```

**Status Values:** `Submitted`, `UnderReview`, `Approved`, `Rejected`, `Paid`

#### Get Insurance Claims By WorkOrder
```http
GET /api/insurance/claims/workorder/{workOrderId}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Insurance Claim By Id
```http
GET /api/insurance/claims/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Ratings (Değerlendirme Sistemi)

#### Create Rating
```http
POST /api/ratings
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "workOrderId": 0,
  "rating": 5,
  "serviceQuality": 5,
  "priceValue": 4,
  "onTimeDelivery": 5,
  "staffBehavior": 5,
  "comment": "string",
  "photos": "[]"
}
```

**Not:** Sadece `Delivered` durumundaki iş emirleri için değerlendirme yapılabilir.

#### Get Ratings By Client
```http
GET /api/ratings?pageNumber={page}&pageSize={size}&status={status}
Authorization: Bearer {token}
X-Client-Id: 1
```

**Status Values:** `Pending`, `Approved`, `Rejected`

#### Get Average Rating
```http
GET /api/ratings/average
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Reply To Rating
```http
POST /api/ratings/{id}/reply
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "reply": "string"
}
```

#### Moderate Rating
```http
PUT /api/ratings/{id}/moderate
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "status": "Approved",
  "rejectionReason": "string"
}
```

---

### Quote Requests (Teklif Talepleri)

#### Create Quote Request
```http
POST /api/quote-requests
Authorization: Bearer {token}
Content-Type: application/json

{
  "customerId": 0,
  "vehicleId": 0,
  "problemDescription": "string",
  "damageType": "Breakdown",
  "urgencyLevel": "High",
  "desiredDateRange": "2024-01-15",
  "contactEmail": "string",
  "contactPhone": "string"
}
```

**DamageType Values:** `Accident`, `Breakdown`, `Maintenance`, `Other`  
**UrgencyLevel Values:** `Low`, `Normal`, `High`, `Urgent`

#### Get Quote Request By Id
```http
GET /api/quote-requests/{id}
Authorization: Bearer {token}
```

#### Get All Quote Requests
```http
GET /api/quote-requests?status={status}&pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Open Quote Requests
```http
GET /api/quote-requests/open?pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Submit Quote Response
```http
POST /api/quote-requests/{quoteRequestId}/quotes
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "description": "string",
  "estimatedDays": 0,
  "quoteAmount": 0,
  "discountPercentage": 0,
  "warrantyMonths": 0,
  "notes": "string"
}
```

#### Accept Quote Response
```http
POST /api/quote-requests/{quoteRequestId}/quotes/{quoteResponseId}/accept
Authorization: Bearer {token}
```

#### Reject Quote Response
```http
POST /api/quote-requests/{quoteRequestId}/quotes/{quoteResponseId}/reject
Authorization: Bearer {token}
Content-Type: application/json

{
  "reason": "string"
}
```

---

### Customer Portal (Müşteri Paneli)

#### Get My Vehicles
```http
GET /api/customer-portal/vehicles
Authorization: Bearer {token}
```

#### Get My WorkOrders
```http
GET /api/customer-portal/work-orders?activeOnly={bool}
Authorization: Bearer {token}
```

#### Get My WorkOrder Details
```http
GET /api/customer-portal/work-orders/{id}
Authorization: Bearer {token}
```

#### Get Maintenance History
```http
GET /api/customer-portal/maintenance-history?vehicleId={id}
Authorization: Bearer {token}
```

#### Update My Profile
```http
PUT /api/customer-portal/profile
Authorization: Bearer {token}
Content-Type: application/json

{
  "firstName": "string",
  "lastName": "string",
  "phone": "string",
  "address": "string"
}
```

---

### Dashboard

#### Get Dashboard Stats
```http
GET /api/dashboard/stats
Authorization: Bearer {token}
X-Client-Id: 1
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "todayWorkOrders": 5,
    "pendingWorkOrders": 3,
    "dailyRevenue": 25000.00,
    "criticalStockItems": 2,
    "overdueInvoices": 1
  }
}
```

#### Get Income Expense Chart
```http
GET /api/dashboard/income-expense-chart?startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get WorkOrder Status Chart
```http
GET /api/dashboard/workorder-status-chart
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Recent Activities
```http
GET /api/dashboard/recent-activities?limit=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Top Customers
```http
GET /api/dashboard/top-customers?limit=10
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Reports (Raporlar)

#### Get WorkOrder Statistics
```http
GET /api/reports/workorder-statistics?startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Part Usage Report
```http
GET /api/reports/part-usage?startDate={date}&endDate={date}&partId={id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Customer Analytics
```http
GET /api/reports/customer-analytics?customerId={id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Financial Charts
```http
GET /api/reports/financial-charts?startDate={date}&endDate={date}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### AI Services

#### Diagnose (Arıza Tespiti)
```http
POST /api/ai/diagnose
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "description": "string",
  "vehicleInfo": {
    "brand": "string",
    "model": "string",
    "year": 0
  }
}
```

#### Analyze Photo (Fotoğraf Analizi)
```http
POST /api/ai/analyze-photo
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: multipart/form-data

{
  "file": [binary],
  "analysisType": "Damage"
}
```

**AnalysisType Values:** `Damage`, `Part`, `General`

#### Estimate Price (Fiyat Tahmini)
```http
POST /api/ai/estimate-price
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "workOrderId": 0,
  "description": "string"
}
```

#### Get Maintenance Suggestions
```http
GET /api/ai/maintenance-suggestions/{vehicleId}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Estimate Part Lifespan
```http
GET /api/ai/estimate-part-lifespan?partId={id}&vehicleId={id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Appointments (Randevular)

#### Get All Appointments
```http
GET /api/appointments?startDate={date}&endDate={date}&status={status}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Appointment
```http
POST /api/appointments
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "customerId": 0,
  "vehicleId": 0,
  "appointmentDate": "2024-01-15T10:00:00Z",
  "description": "string"
}
```

#### Cancel Appointment
```http
POST /api/appointments/{id}/cancel
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "reason": "string"
}
```

---

### Part Suppliers (Tedarikçiler)

#### Get All Part Suppliers
```http
GET /api/part-suppliers?pageNumber={page}&pageSize={size}&searchTerm={term}&isActiveOnly={bool}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Get Part Supplier By Id
```http
GET /api/part-suppliers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Create Part Supplier
```http
POST /api/part-suppliers
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "companyName": "string",
  "contactPerson": "string",
  "phone": "string",
  "email": "string",
  "address": "string",
  "isActive": true
}
```

#### Update Part Supplier
```http
PUT /api/part-suppliers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "companyName": "string",
  "phone": "string",
  "isActive": true
}
```

#### Delete Part Supplier
```http
DELETE /api/part-suppliers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### Notifications

#### Send Notification
```http
POST /api/notifications
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: application/json

{
  "userId": 0,
  "title": "string",
  "message": "string",
  "type": "Info"
}
```

**Type Values:** `Info`, `Warning`, `Error`, `Success`

#### Get Notifications
```http
GET /api/notifications?isRead={bool}&pageNumber={page}&pageSize={size}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### Mark Notification As Read
```http
PUT /api/notifications/{id}/read
Authorization: Bearer {token}
X-Client-Id: 1
```

---

## 📝 Request/Response Örnekleri

### Başarılı Yanıt Formatı

```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "Example"
  }
}
```

### Hata Yanıt Formatı

```json
{
  "success": false,
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "Araç bulunamadı",
  "details": {
    "VehicleId": 999
  }
}
```

### Pagination Yanıt Formatı

```json
{
  "success": true,
  "data": {
    "items": [...],
    "totalCount": 100,
    "pageNumber": 1,
    "pageSize": 20,
    "totalPages": 5
  }
}
```

---

## 🌍 Dil Desteği

Hata mesajları için dil belirtmek:

```
Accept-Language: tr
Accept-Language: en
Accept-Language: ar
```

**Desteklenen Diller:**
- `tr` - Türkçe (varsayılan)
- `en` - İngilizce
- `ar` - Arapça

---

## 📊 Rate Limiting

Şu anda rate limiting yok. İleride eklenebilir.

---

## 🔒 Güvenlik Notları

1. Tüm endpoint'ler (login ve register hariç) authentication gerektirir
2. Multi-tenant yapı nedeniyle her istekte ClientId belirtilmelidir
3. JWT token'lar belirli bir süre sonra expire olur
4. HTTPS kullanımı production'da zorunludur

---

**Son Güncelleme**: 2024
