# Production media access migration

`/uploads/**` is disabled in production by default because stored media can contain customer, vehicle, quote and work-order data. Development keeps static files available for local workflows.

## Temporary compatibility gate

Set `Media__EnableLegacyAnonymousStaticFiles=true` only as a short, audited rollout exception for clients that still render bare `/uploads/...` URLs. It re-enables anonymous access to **every** upload and must be removed once clients use an authorized media contract. Do not use this flag for a general production launch.

## Current scope and remaining migration

The generic `UploadedFiles` entity has no `ClientId`, owner, visibility classification, or mandatory association to a domain aggregate. Some `FilePath` values are written directly on vehicle, work-order, quote, part, chat and public-portfolio records. Therefore `GET /api/Files/download?containerName=&fileName=` cannot safely prove that the caller may read a given file and must not be adopted as the production private-media endpoint.

Vehicle photos are now covered without a data migration because `VehiclePhoto` already contains `ClientId`, `VehicleId`, and `FilePath`. The server resolves the physical file only after validating those database-owned values. Other private media types remain blocked until each gets the same source-owned endpoint or is migrated to the new media record described below.

Work-order photos are covered by `/api/media/work-orders/{workOrderId}/photos/{photoId}`. Quote-request photos are covered by `/api/media/quote-requests/{quoteRequestId}/photos/{photoId}`; because legacy quote media is stored as an ordered `PhotoPaths` JSON list, `photoId` is its one-based server-resolved list position, not a storage ID or path. Both endpoints require the tenant's normal bearer token.

Customer Portal has a separate contract: `GET /api/customer-media/customers/{customerId}/work-orders/{workOrderId}/photos/{photoId}`. It accepts CustomerOnly or SystemAdmin tokens. A customer token must resolve to the route customer and tenant; both customer and SystemAdmin calls verify that the work order belongs to the route customer and that the photo belongs to that work order before reading storage. Customer work-order detail photos expose this as `mediaUrl` while retaining `url` only for migration.

Part photos use `GET /api/media/parts/{partId}/photos/{photoId}` under ShopStaff authorization. The endpoint confirms the tenant owns both the part and its associated photo. Part detail and add-photo responses expose `mediaUrl` while retaining `filePath` for migration.

AI damage-photo analysis is production-disabled (`410 Gone`) until an authorized MediaAsset ID flow exists. The legacy `PhotoPaths` command no longer dereferences URLs or local filesystem paths in any environment. A future implementation must authorize each asset against the requesting customer/tenant, limit MIME type and size, and use a bounded server-side stream.

AI diagnosis drafts use `POST /api/AI/diagnosis-assets` (multipart `file`) and return an `assetId` with a 24-hour expiry. `POST /api/AI/diagnose` accepts `mediaAssetIds`, never URLs or file paths. Assets are scoped to the authenticated user and tenant with purpose `AiDiagnosisDraft`; a scheduled retention worker must delete expired rows and their storage keys. AI drafts are stored through `IPrivateMediaStorage`: its keys begin with `private-media/`, are never returned by the API, and resolve under `App_Data/private-media` (outside `wwwroot`) by default. The optional `PrivateMedia:RootPath` must also be outside the web root. The caller validates MIME type and size before storage; the store uses generated names and rejects traversal in both scope and key. This does not alter legacy uploads.

## Target contract

- Private media: a resource-specific authorized endpoint returns a short-lived `mediaUrl` (or the app performs an authenticated download and local cache). Authorization must derive from the owning aggregate and tenant, never from a client-supplied file path.
- Public media: expose only a separate, explicitly published portfolio/media record. It must never share the private upload directory or generic file endpoint.
- Add `ClientId`, owner aggregate/type/id, and `Visibility` to the new media record before issuing URLs. Migrate old `FilePath` values in a controlled job.

Mobile clients must treat legacy `/uploads/...` paths as development/migration-only. Vehicle photo responses now include `mediaUrl` (`/api/media/vehicles/{vehicleId}/photos/{photoId}`); send the normal bearer token and use this field in preference to `url`/`filePath`.

Published portfolio media is deliberately separate: `featuredPhotoUrls` returns `/api/public-media/clients/{clientId}/portfolios/{portfolioId}/photos/{photoId}`. This endpoint is anonymous only after the portfolio is published and explicitly references that photo. It must never be used for work-order or vehicle media.
