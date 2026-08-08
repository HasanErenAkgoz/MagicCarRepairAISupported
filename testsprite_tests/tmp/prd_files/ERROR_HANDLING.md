# Error handling (mobile)

## API errors

Use `handleApiError(err, t)` from [`src/utils/handleApiError.ts`](../src/utils/handleApiError.ts) in screen `catch` blocks:

```typescript
import { handleApiError } from '../utils/handleApiError';

try {
  // ...
} catch (err) {
  const { title, message } = handleApiError(err, t);
  AppToast.error(message); // or showBanner('error', title, message)
}
```

`NetworkError` and `ApiError` from [`src/utils/http.ts`](../src/utils/http.ts) are mapped to i18n via `common` and `translateBackendError`.

## Render crashes

[`AppErrorBoundary`](../src/components/AppErrorBoundary.tsx) wraps the app in `App.tsx`. Uncaught React errors are reported to Sentry when `EXPO_PUBLIC_SENTRY_DSN` is set.

## Observability

- Initialize: [`src/utils/sentry.ts`](../src/utils/sentry.ts) (`initSentry` in `App.tsx`)
- HTTP 5xx: breadcrumbs + `captureHttpError` in `http.ts`
