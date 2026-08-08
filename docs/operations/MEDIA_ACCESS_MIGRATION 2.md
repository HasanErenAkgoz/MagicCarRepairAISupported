# Production media access migration

`/uploads/**` is disabled in production by default because stored media can contain customer, vehicle, quote and work-order data. Development keeps static files available for local workflows.

## Temporary compatibility gate

Set `Media__EnableLegacyAnonymousStaticFiles=true` only as a short, audited rollout exception for clients that still render bare `/uploads/...` URLs. It re-enables anonymous access to **every** upload and must be removed once clients use an authorized media contract. Do not use this flag for a general production launch.

## Current scope and remaining migration

The generic `UploadedFiles` entity has no `ClientId`, owner, visibility classification, or mandatory association to a domain aggregate. Some `FilePath` values are written directly on vehicle, work-order, quote, part, chat and public-portfolio records. Therefore `GET /api/Files/download?containerName=&fileName=` cannot safely prove that the caller may read a given file and must not be adopted as the production private-media endpoint.

Vehicle photos are now covered without a data migration because `VehiclePhoto` already contains `ClientId`, `VehicleId`, and `FilePath`. The server resolves the physical file only after validating those database-owned values. Other private media types remain blocked until each gets the same source-owned endpoint or is migrated to the new media record described below.

## Target contract

- Private media: a resource-specific authorized endpoint returns a short-lived `mediaUrl` (or the app performs an authenticated download and local cache). Authorization must derive from the owning aggregate and tenant, never from a client-supplied file path.
- Public media: expose only a separate, explicitly published portfolio/media record. It must never share the private upload directory or generic file endpoint.
- Add `ClientId`, owner aggregate/type/id, and `Visibility` to the new media record before issuing URLs. Migrate old `FilePath` values in a controlled job.

Mobile clients must treat legacy `/uploads/...` paths as development/migration-only. Vehicle photo responses now include `mediaUrl` (`/api/media/vehicles/{vehicleId}/photos/{photoId}`); send the normal bearer token and use this field in preference to `url`/`filePath`.

Published portfolio media is deliberately separate: `featuredPhotoUrls` returns `/api/public-media/clients/{clientId}/portfolios/{portfolioId}/photos/{photoId}`. This endpoint is anonymous only after the portfolio is published and explicitly references that photo. It must never be used for work-order or vehicle media.
