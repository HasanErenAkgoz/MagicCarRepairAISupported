# API Response Contract

## Decision

New JSON API endpoints use the existing `IDataResult<T>` envelope:

```json
{ "success": true, "data": {}, "message": "optional" }
```

Validation or business failures retain the same envelope with `success: false` and a suitable HTTP 4xx status. This is the mobile `ApiDataResult<T>` contract. New endpoint responses must be documented in OpenAPI with success and failure status codes before release.

## Exceptions

File download/stream endpoints deliberately return binary content (or 404/401/403) and must not be JSON-wrapped. Health checks return their standard health payload. Both are explicit transport contracts, not legacy JSON exceptions.

## Enforcement

`scripts/verify_production_readiness.py` guards the new `POST /api/AI/diagnosis-assets` contract: it must return its `IDataResult` for both success and validation failure. The CI `production-readiness-source-check` job runs this guard.

## Legacy migration

Do not rewrite existing raw endpoints in place. Mark an endpoint legacy in its controller/OpenAPI description, add a new versioned or opt-in enveloped endpoint, migrate the mobile service through `ApiDataResult<T>`, then remove the legacy endpoint only after a published deprecation window and usage evidence. Customer and vehicle raw responses remain compatibility paths until that work is scheduled.
