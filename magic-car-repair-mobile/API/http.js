import { API_BASE_URL } from './config';
import { getToken } from './storage';

async function safeReadJson(res) {
  const text = await res.text();
  if (!text) return null;
  try {
    return JSON.parse(text);
  } catch {
    return { _raw: text };
  }
}

export async function apiGet(path, options = {}) {
  const token = await getToken();
  const url = `${API_BASE_URL}${path.startsWith('/') ? '' : '/'}${path}`;

  console.log('========================================');
  console.log('[API] GET Request');
  console.log('[API] URL:', url);
  console.log('[API] Token:', token ? 'Present' : 'Missing');
  console.log('========================================');

  try {
    const headers = {
      'Content-Type': 'application/json',
      'X-Client-Id': '1', // Default client ID for multi-tenant support
      ...(options.headers || {}),
    };

    if (token && !options.skipAuth) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const res = await fetch(url, {
      method: 'GET',
      headers,
      signal: options.signal,
    });

    const data = await safeReadJson(res);

    console.log('[API] Response Status:', res.status);
    console.log('[API] Response Data:', JSON.stringify(data, null, 2));

    if (!res.ok) {
      const message =
        data?.message ||
        data?.error ||
        data?.title ||
        data?.Message ||
        `İstek başarısız (${res.status})`;

      const err = new Error(message);
      err.status = res.status;
      err.data = data;
      throw err;
    }

    // Handle CreatedAtAction response (201 Created)
    // CreatedAtAction returns the result object directly in the response body
    // Also handle wrapped responses (data.Data pattern)
    if (res.status === 201 || res.status === 200) {
      // If response is wrapped in a Data property, unwrap it
      if (data && data.Data) {
        return data.Data;
      }
      // If response has Success property (IDataResult pattern), return Data
      if (data && typeof data.Success !== 'undefined' && data.Data) {
        return data.Data;
      }
      // Otherwise return data as is (CreatedAtAction returns the object directly)
      return data;
    }

    return data;
  } catch (error) {
    console.error('[API] GET Error:', error);
    throw error;
  }
}

export async function apiPost(path, body, options = {}) {
  const token = await getToken();
  const url = `${API_BASE_URL}${path.startsWith('/') ? '' : '/'}${path}`;

  console.log('========================================');
  console.log('[API] POST Request');
  console.log('[API] URL:', url);
  console.log('[API] Base URL:', API_BASE_URL);
  console.log('[API] Path:', path);
  console.log('[API] Token:', token ? 'Present' : 'Missing');
  console.log('[API] Body:', JSON.stringify(body, null, 2));
  console.log('========================================');

  try {
    console.log('[API] Starting fetch request...');
    const startTime = Date.now();
    
    // Timeout süresini 30 saniyeye çıkar (backend yavaş yanıt verebilir)
    const TIMEOUT_MS = 30000;
    const controller = new AbortController();
    const timeoutId = setTimeout(() => {
      console.error(`[API] ⚠️ Request timeout after ${TIMEOUT_MS}ms`);
      controller.abort();
    }, TIMEOUT_MS);
    
    const headers = {
      'Content-Type': 'application/json',
      'X-Client-Id': '1', // Default client ID for multi-tenant support
      ...(options.headers || {}),
    };

    // Token varsa Authorization header'ı ekle (login endpoint'i için token yok, diğerleri için var)
    if (token && !options.skipAuth) {
      headers['Authorization'] = `Bearer ${token}`;
    }
    
    const res = await fetch(url, {
      method: 'POST',
      headers,
      body: JSON.stringify(body ?? {}),
      signal: controller.signal,
    });
    
    clearTimeout(timeoutId);
    const duration = Date.now() - startTime;
    console.log(`[API] ✅ Fetch completed in ${duration}ms`);

    console.log('[API] Response Status:', res.status);
    console.log('[API] Response OK:', res.ok);
    console.log('[API] Response Headers:', JSON.stringify([...res.headers.entries()]));

    console.log('[API] Reading response body...');
    const data = await safeReadJson(res);
    console.log('[API] Response Data:', JSON.stringify(data, null, 2));

    if (!res.ok) {
      // Backend'den dönen hata mesajını çıkar
      const message =
        data?.message ||
        data?.error ||
        data?.title ||
        data?.Message || // Backend IDataResult formatı için
        `İstek başarısız (${res.status})`;
      
      console.log('[API] Error Response - Status:', res.status);
      console.log('[API] Error Response - Data:', JSON.stringify(data, null, 2));
      console.log('[API] Error Response - Extracted Message:', message);
      
      const err = new Error(message);
      err.status = res.status;
      err.data = data;
      throw err;
    }

    // Handle successful responses (200, 201)
    // CreatedAtAction (201) returns the object directly
    // Ok (200) also returns the object directly
    // Some endpoints might wrap in Data property (IDataResult pattern)
    if (data && data.Data && typeof data.Success !== 'undefined') {
      // Wrapped in IDataResult format
      return data.Data;
    }
    // Direct object response (most common - CreatedAtAction, Ok)
    return data;
  } catch (error) {
    console.error('========================================');
    console.error('[API] ERROR OCCURRED');
    console.error('[API] URL:', url);
    console.error('[API] Error Message:', error.message);
    console.error('[API] Error Name:', error.name);
    console.error('[API] Error Type:', error.constructor.name);
    
    // AbortError (timeout) durumu
    if (error.name === 'AbortError' || error.message === 'Aborted') {
      console.error('[API] ⚠️ Request timed out after 10 seconds');
      throw new Error(
        'İstek zaman aşımına uğradı. Backend yanıt vermiyor.\n\n' +
        'Lütfen tekrar deneyin veya bağlantınızı kontrol edin.'
      );
    }
    
    // Backend'den dönen hata mesajı varsa onu kullan
    if (error.status && error.data) {
      const backendMessage = 
        error.data?.message || 
        error.data?.error || 
        error.data?.title ||
        error.message;
      
      console.error('[API] Backend Error Data:', JSON.stringify(error.data, null, 2));
      throw new Error(backendMessage);
    }
    
    if (error.stack) {
      console.error('[API] Error Stack:', error.stack);
    }
    console.error('========================================');
    
    // Network errors (connection refused, SSL, etc.)
    if (
      error.message === 'Network request failed' || 
      error.message.includes('Network') ||
      error.message.includes('Failed to fetch') ||
      error.message.includes('ERR_CONNECTION') ||
      error.name === 'TypeError'
    ) {
      const isHTTPS = url.startsWith('https://');
      const suggestedURL = isHTTPS 
        ? url.replace('https://', 'http://').replace(':7216', ':5169')
        : url;
      
      throw new Error(
        `🌐 Bağlantı Hatası\n\n` +
        `Backend'e ulaşılamıyor.\n\n` +
        `📡 İstek URL: ${url}\n` +
        `❌ Hata: ${error.message}\n\n` +
        `✅ Kontrol Listesi:\n` +
        `  1. Backend çalışıyor mu? (http://localhost:5169/swagger/index.html)\n` +
        `  2. Telefon ve bilgisayar aynı WiFi ağında mı?\n` +
        `  3. Windows Firewall 5169 portunu engelliyor mu?\n` +
        `  4. IP adresi doğru mu? (192.168.1.12)\n\n` +
        `💡 Metro bundler terminal'deki log'lara bakın.`
      );
    }
    
    // Diğer hatalar için orijinal mesajı kullan
    throw error;
  }
}

export async function apiPut(path, body, options = {}) {
  const token = await getToken();
  const url = `${API_BASE_URL}${path.startsWith('/') ? '' : '/'}${path}`;

  console.log('========================================');
  console.log('[API] PUT Request');
  console.log('[API] URL:', url);
  console.log('[API] Token:', token ? 'Present' : 'Missing');
  console.log('[API] Body:', JSON.stringify(body, null, 2));
  console.log('========================================');

  try {
    const headers = {
      'Content-Type': 'application/json',
      'X-Client-Id': '1',
      ...(options.headers || {}),
    };

    if (token && !options.skipAuth) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const res = await fetch(url, {
      method: 'PUT',
      headers,
      body: JSON.stringify(body ?? {}),
      signal: options.signal,
    });

    const data = await safeReadJson(res);

    console.log('[API] Response Status:', res.status);
    console.log('[API] Response Data:', JSON.stringify(data, null, 2));

    if (!res.ok) {
      const message =
        data?.message ||
        data?.error ||
        data?.title ||
        data?.Message ||
        `İstek başarısız (${res.status})`;

      const err = new Error(message);
      err.status = res.status;
      err.data = data;
      throw err;
    }

    // Handle wrapped responses
    if (data && data.Data && typeof data.Success !== 'undefined') {
      return data.Data;
    }
    return data;
  } catch (error) {
    console.error('[API] PUT Error:', error);
    throw error;
  }
}
