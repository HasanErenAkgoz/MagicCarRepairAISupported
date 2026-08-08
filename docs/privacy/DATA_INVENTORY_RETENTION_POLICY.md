# Data inventory, retention and deletion policy (pending legal approval)

Status: engineering baseline only. Retention periods below are assumptions and require KVKK/GDPR, tax-law and contractual review before production enforcement.

| Data set / implementation source | Class | Purpose | Access roles | Proposed retention (pending approval) | Deletion / backup | Audit evidence |
| --- | --- | --- | --- | --- | --- | --- |
| Customer, User, Employee; customer portal/profile APIs | PII | account, service communication | subject, authorised shop staff, SystemAdmin support | account life + 90 days | soft-delete/anonymise subject data; backup expires after 35 days | auth, profile-change, DSAR log |
| Vehicle (plate, VIN), VehiclePhoto; vehicle APIs | PII / vehicle identifier | service history and repair | owner, assigned shop staff | service relationship + 2 years | remove photo object then metadata; backup 35 days | media read/delete, vehicle change |
| WorkOrder, WorkOrderPhoto, timeline | operational + possible PII | repair execution, disputes | owner, tenant staff, authorised support | 5 years unless tax/contract rule supersedes | policy-controlled deletion/anonymisation; media object first; backup 35 days | status, photo access, deletion request |
| Invoice, InvoiceItem, Income/Expense/Tax | financial / PII | accounting, statutory records | tenant finance roles, SystemAdmin support | 10 years assumption, legal approval required | legal-hold aware purge only; backup 35 days | invoice/export/change log |
| Appointment/location/address fields | PII / location | booking and service routing | subject, tenant staff | appointment + 2 years | scheduled purge/anonymisation; backup 35 days | booking/change/deletion log |
| Payment, MobilePayments | financial sensitive | payment reconciliation | tenant finance, payment provider | provider/tax schedule pending approval | tokenised provider references only; no card data; backup 35 days | payment status/refund/audit event |
| MediaAsset AI drafts and storage keys | sensitive image | user-requested AI diagnosis | owning user, tenant/service only | 24 hours (implemented) | retention worker: storage first, then DB; retry failures | upload, validation, retention logs |

## DSAR flow

1. Privacy/Support verifies requester identity and tenant/customer relationship; records request ID and scope.
2. Engineering exports all applicable records and media metadata, redacting third-party and legal-hold data.
3. Privacy approves correction, deletion, restriction or export; Finance/Legal decides statutory retention and holds.
4. Engineering performs approved action, runs media deletion before metadata where applicable, and records result, exceptions and backup-expiry date.
5. Support tells requester outcome and remaining lawful retention grounds; no raw storage keys or secrets are exposed.

## Engineering acceptance criteria

- Every new PII/media entity has classification, purpose, owner role, retention owner and deletion behaviour in this document.
- Media endpoints authorise tenant and resource/participant ownership; responses never expose storage keys.
- Purge jobs are idempotent, log counts and failures without PII, and retry storage failures before metadata removal.
- Backups have documented encryption, access, expiry and restore-test evidence; deletion requests state the backup expiry.
- QA covers cross-tenant denial, authorised deletion, expired-media purge retry, legal-hold skip and DSAR audit event.

## Owners and operational evidence

BA owns data-purpose mapping; Security/AppSec owns classification and access review; CTO approves engineering controls; Privacy/Legal approves periods and holds; DevOps owns backup/restore and purge observability; QA owns negative isolation and deletion regression tests. Release evidence: reviewed inventory, legal approval record, purge runbook, dashboard/alerts, and backup restore test.
