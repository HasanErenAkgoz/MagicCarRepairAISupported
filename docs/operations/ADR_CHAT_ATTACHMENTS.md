# ADR — Chat attachment ownership and delivery

## Decision

Do not enable chat file uploads or downloads through the current `SendChatMessageCommand`. It accepts client-controlled `FilePath`, while `ChatMessage` has no owned asset identifier and the current chat controller is ShopStaff-only. A minimal patch would either expose storage paths or incorrectly exclude customer participants.

## Required minimal model

Add a `ChatAttachment` record: `Id`, `ChatMessageId`, `ClientId`, `OwnerUserId`, `StorageKey`, `ContentType`, `Length`, `ExpiresAt`, `CreatedDate`. `StorageKey` is server-only and never appears in DTOs. The upload endpoint creates a draft attachment owned by the authenticated user; send-message accepts only draft attachment IDs and atomically binds them to the newly created message.

## Authorization matrix

| Operation | Required rule |
| --- | --- |
| Upload draft | Authenticated sender; tenant resolved; MIME/size allow-list; no path or URL input |
| Bind to message | Sender owns every draft; same tenant; drafts unexpired; one-time bind |
| Download | Requester is message sender or receiver; same tenant; SystemAdmin only through explicit audited support policy |
| Conversation/work-order list | Tenant filter plus participant check; attachment DTO only exposes `mediaUrl` and display metadata |
| Delete/retention | Sender may delete own unbound draft; retention removes unbound/expired and deleted-message attachments from storage then DB |

## Acceptance criteria

1. `SendChatMessageCommand` has no `FilePath`, `FileName`, or caller-controlled size/path fields; it accepts `attachmentIds` only.
2. A non-participant, a different tenant, and an expired or already-bound draft receive no attachment bytes and no storage metadata.
3. Valid sender/receiver download returns content only from a message-owned asset ID endpoint.
4. Negative integration tests cover anonymous upload/download, cross-tenant draft binding, non-participant download, and traversal/URL payloads.
5. Audit events record upload, bind, download denial, delete, and retention deletion without logging storage keys.

## Implementation checklist (bounded)

1. Add `ChatAttachment` migration with an FK to `ChatMessages`, a unique draft/binding constraint, and an owner/tenant/expiry index.
2. Add an `IChatConversationAuthorizationService` that loads both users and proves same-tenant ShopStaff participation before upload, bind, list, or download.
3. Use a Unit of Work transaction: create message, validate all drafts, bind all drafts, and commit together; remove orphaned storage if the commit fails.
4. Add `POST /api/chat/attachments` and `GET /api/chat/messages/{messageId}/attachments/{attachmentId}` only after the authorization service exists. DTOs expose `mediaUrl`, filename, MIME and length—never paths.
5. Add integration tests for anonymous, cross-tenant, nonparticipant, expired, already-bound, traversal/URL payload, rollback, and valid sender/receiver download cases.

## Blocker

The existing `ChatController` policy excludes customers, while customer-to-shop conversation authorization needs a shared participant policy and a trustworthy customer/user relationship lookup. This must be designed with the Customer Portal owner before implementation.
