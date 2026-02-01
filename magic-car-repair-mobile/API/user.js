import { API_BASE_URL } from './config';
import { getToken } from './storage';

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

    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const res = await fetch(url, {
      method: 'GET',
      headers,
      signal: options.signal,
    });

    const text = await res.text();
    let data = null;
    
    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = { _raw: text };
      }
    }

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

    return data;
  } catch (error) {
    console.error('[API] GET Error:', error.message);
    throw error;
  }
}

async function apiPut(path, body, options = {}) {
  const token = await getToken();
  const url = `${API_BASE_URL}${path.startsWith('/') ? '' : '/'}${path}`;

  console.log('========================================');
  console.log('[API] PUT Request');
  console.log('[API] URL:', url);
  console.log('[API] Body:', JSON.stringify(body, null, 2));
  console.log('========================================');

  try {
    const headers = {
      'Content-Type': 'application/json',
      'X-Client-Id': '1', // Default client ID for multi-tenant support
      ...(options.headers || {}),
    };

    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const res = await fetch(url, {
      method: 'PUT',
      headers,
      body: JSON.stringify(body ?? {}),
      signal: options.signal,
    });

    const text = await res.text();
    let data = null;
    
    if (text) {
      try {
        data = JSON.parse(text);
      } catch {
        data = { _raw: text };
      }
    }

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

    return data;
  } catch (error) {
    console.error('[API] PUT Error:', error.message);
    throw error;
  }
}

/**
 * Kullanıcı profil bilgilerini getirir
 * Admin/Manager/Employee için: /api/user/profile
 * Customer için: /api/customer-portal/profile
 */
export async function getMyProfile() {
  // Önce genel user endpoint'ini dene (Admin/Manager/Employee için)
  try {
    const res = await apiGet('/user/profile');
    
    // Response formatı: UpdateMyProfileResponse
    return {
      id: res?.data?.id || res?.id,
      firstName: res?.data?.firstName || res?.firstName,
      lastName: res?.data?.lastName || res?.lastName,
      fullName: res?.data?.fullName || res?.fullName,
      email: res?.data?.email || res?.email,
      phoneNumber: res?.data?.phoneNumber || res?.phoneNumber,
      address: res?.data?.address || res?.address,
      language: res?.data?.language || res?.language,
    };
  } catch (error) {
    // Eğer user endpoint'i başarısız olursa (404 veya başka bir hata), customer portal'ı dene
    if (error.status === 404 || error.status === 401) {
      const res = await apiGet('/customer-portal/profile');
      
      return {
        id: res?.data?.id || res?.id,
        firstName: res?.data?.firstName || res?.firstName,
        lastName: res?.data?.lastName || res?.lastName,
        fullName: res?.data?.fullName || res?.fullName,
        email: res?.data?.email || res?.email,
        phoneNumber: res?.data?.phoneNumber || res?.phoneNumber,
        address: res?.data?.address || res?.address,
        language: res?.data?.language || res?.language,
      };
    }
    throw error;
  }
}

export async function updateMyProfile(profileData) {
  const payload = {
    firstName: profileData.firstName,
    lastName: profileData.lastName,
    phoneNumber: profileData.phone,
    address: profileData.address,
    language: profileData.language || 'tr',
  };

  const res = await apiPut('/customer-portal/profile', payload);
  
  // Response formatı: UpdateMyProfileResponse
  return {
    id: res?.data?.id || res?.id,
    firstName: res?.data?.firstName || res?.firstName,
    lastName: res?.data?.lastName || res?.lastName,
    fullName: res?.data?.fullName || res?.fullName,
    email: res?.data?.email || res?.email,
    phoneNumber: res?.data?.phoneNumber || res?.phoneNumber,
    address: res?.data?.address || res?.address,
    language: res?.data?.language || res?.language,
  };
}
