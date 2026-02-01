 import AsyncStorage from '@react-native-async-storage/async-storage';

const KEYS = {
  token: 'auth.token',
  user: 'auth.user',
  rawLoginResponse: 'auth.rawLoginResponse',
};

export async function saveAuthSession({ token, user, raw }) {
  if (token) await AsyncStorage.setItem(KEYS.token, String(token));
  if (user) await AsyncStorage.setItem(KEYS.user, JSON.stringify(user));
  if (raw) await AsyncStorage.setItem(KEYS.rawLoginResponse, JSON.stringify(raw));
}

export async function getToken() {
  return AsyncStorage.getItem(KEYS.token);
}

export async function getUser() {
  try {
    const userJson = await AsyncStorage.getItem(KEYS.user);
    if (userJson) {
      return JSON.parse(userJson);
    }
    
    // Eğer user yoksa, rawLoginResponse'dan çıkarmayı dene
    const rawJson = await AsyncStorage.getItem(KEYS.rawLoginResponse);
    if (rawJson) {
      const raw = JSON.parse(rawJson);
      return raw?.data?.user || raw?.data?.User || raw?.user || raw?.User || null;
    }
    
    return null;
  } catch (error) {
    console.error('Error getting user from storage:', error);
    return null;
  }
}

export async function clearAuthSession() {
  await AsyncStorage.multiRemove([KEYS.token, KEYS.user, KEYS.rawLoginResponse]);
}

