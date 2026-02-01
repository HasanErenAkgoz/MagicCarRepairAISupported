// API base url
// Backend ports:
//   - HTTPS: https://localhost:7216
//   - HTTP:  http://localhost:5169
// Android Emulator:    http://10.0.2.2:5169/api  (HTTP - localhost on your machine)
// iOS Simulator:       http://localhost:5169/api
// Physical device:     http://192.168.1.12:5169/api  (Telefon için - bilgisayarın LAN IP'si)
// Note: Telefon ve bilgisayar aynı WiFi ağında olmalı!
// Note: HTTP kullanıyoruz çünkü HTTPS self-signed certificate sorunları olabilir
import { Platform } from 'react-native';
import Constants from 'expo-constants';

// Platform'a göre base URL belirleme
const getBaseUrl = () => {
  // Bilgisayarın LAN IP'si (telefon için)
  const LAN_IP = '192.168.1.12';
  
  // Debug: Device bilgilerini logla
  const deviceName = Constants.deviceName || 'unknown';
  const isDeviceFromConstants = Constants.isDevice;
  const envUrl = process.env.EXPO_PUBLIC_API_BASE_URL;
  
  // Device name'e göre emulator tespiti (Constants.isDevice bazen undefined döner)
  const deviceNameLower = deviceName.toLowerCase();
  const isEmulatorByName = 
    deviceNameLower.includes('emulator') ||
    deviceNameLower.includes('sdk') ||
    deviceNameLower.includes('simulator') ||
    deviceNameLower.includes('google_sdk') ||
    deviceNameLower.includes('generic');
  
  // Fiziksel cihaz tespiti: isDevice true ise veya device name emulator değilse
  const isPhysicalDevice = isDeviceFromConstants === true || 
    (isDeviceFromConstants === undefined && !isEmulatorByName);
  
  console.log('[API Config] Platform:', Platform.OS);
  console.log('[API Config] Device Name:', deviceName);
  console.log('[API Config] Constants.isDevice:', isDeviceFromConstants);
  console.log('[API Config] Is Emulator (by name):', isEmulatorByName);
  console.log('[API Config] Is Physical Device (final):', isPhysicalDevice);
  console.log('[API Config] EXPO_PUBLIC_API_BASE_URL:', envUrl || 'not set');
  
  // ÖNEMLİ: Fiziksel cihazda 10.0.2.2 çalışmaz! Environment variable'ı override et
  if (isPhysicalDevice && envUrl && envUrl.includes('10.0.2.2')) {
    console.log('[API Config] ⚠️ Overriding environment variable: 10.0.2.2 does not work on physical devices');
    console.log('[API Config] Using LAN IP for physical device:', `http://${LAN_IP}:5169/api`);
    return `http://${LAN_IP}:5169/api`;
  }
  
  // Environment variable varsa ve emulator/simulator ise onu kullan
  if (envUrl && !isPhysicalDevice) {
    console.log('[API Config] Using EXPO_PUBLIC_API_BASE_URL (emulator/simulator):', envUrl);
    return envUrl;
  }
  
  if (Platform.OS === 'android') {
    if (!isPhysicalDevice) {
      // Android Emulator için 10.0.2.2 kullan (HTTP)
      console.log('[API Config] Detected: Android Emulator -> using 10.0.2.2');
      return 'http://10.0.2.2:5169/api';
    } else {
      // Android fiziksel cihaz için LAN IP
      console.log('[API Config] Detected: Android Physical Device -> using LAN IP');
      return `http://${LAN_IP}:5169/api`;
    }
  }

  if (Platform.OS === 'ios') {
    if (!isPhysicalDevice) {
      // iOS Simulator için localhost kullan
      console.log('[API Config] Detected: iOS Simulator -> using localhost');
      return 'http://localhost:5169/api';
    } else {
      // iOS fiziksel cihaz için LAN IP
      console.log('[API Config] Detected: iOS Physical Device -> using LAN IP');
      return `http://${LAN_IP}:5169/api`;
    }
  }

  // Varsayılan: LAN IP (fiziksel cihaz varsayımı)
  console.log('[API Config] Default: Using LAN IP');
  return `http://${LAN_IP}:5169/api`;
};

export const API_BASE_URL = getBaseUrl();

