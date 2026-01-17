/**
 * API Configuration
 * Backend API base URL ve konfigürasyon ayarları
 */

export const API_CONFIG = {
  // Development
  BASE_URL: 'http://localhost:5000/api',
  
  // Production (değiştirilecek)
  // BASE_URL: 'https://api.magiccarrepair.com/api',
  
  TIMEOUT: 30000, // 30 seconds
  RETRY_ATTEMPTS: 3,
  RETRY_DELAY: 1000, // 1 second
};

// Default headers
export const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
};
