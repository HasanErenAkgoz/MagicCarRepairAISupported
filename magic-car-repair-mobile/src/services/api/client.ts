/**
 * API Client
 * Axios instance ve interceptor'lar
 * 
 * Bu dosya JULES_BACKEND_DOCUMENTATION.md dosyasındaki API client
 * implementasyonuna göre tamamlanacak.
 */

import axios, { AxiosInstance, AxiosError, AxiosRequestConfig } from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { API_CONFIG, DEFAULT_HEADERS } from '../../config/api';
import { ApiErrorResponse } from '../../types/api';

class ApiClient {
  private client: AxiosInstance;
  private clientId: number | null = null;

  constructor() {
    this.client = axios.create({
      baseURL: API_CONFIG.BASE_URL,
      timeout: API_CONFIG.TIMEOUT,
      headers: DEFAULT_HEADERS,
    });

    this.setupInterceptors();
  }

  private setupInterceptors() {
    // Request Interceptor - Token ve ClientId ekle
    this.client.interceptors.request.use(
      async (config) => {
        // Token'ı AsyncStorage'dan al
        const token = await AsyncStorage.getItem('auth_token');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }

        // ClientId'yi AsyncStorage'dan al
        const clientId = await AsyncStorage.getItem('client_id');
        if (clientId) {
          config.headers['X-Client-Id'] = clientId;
        } else if (this.clientId) {
          config.headers['X-Client-Id'] = this.clientId.toString();
        }

        return config;
      },
      (error) => Promise.reject(error)
    );

    // Response Interceptor - Error handling
    this.client.interceptors.response.use(
      (response) => response,
      async (error: AxiosError<ApiErrorResponse>) => {
        // 401 Unauthorized - Token expired veya invalid
        if (error.response?.status === 401) {
          // Token'ı temizle ve login ekranına yönlendir
          await AsyncStorage.removeItem('auth_token');
          await AsyncStorage.removeItem('client_id');
          // Navigation to login screen (React Navigation)
          // navigationRef.navigate('Login');
        }

        // 403 Forbidden - Yetkisiz erişim
        if (error.response?.status === 403) {
          console.error('Forbidden: You do not have permission to access this resource');
        }

        return Promise.reject(error);
      }
    );
  }

  // ClientId setter
  setClientId(clientId: number) {
    this.clientId = clientId;
    AsyncStorage.setItem('client_id', clientId.toString());
  }

  // GET request
  async get<T>(url: string, config?: AxiosRequestConfig) {
    const response = await this.client.get<{ success: true; data: T }>(url, config);
    return response.data.data;
  }

  // POST request
  async post<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.post<{ success: true; data: T }>(url, data, config);
    return response.data.data;
  }

  // PUT request
  async put<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.put<{ success: true; data: T }>(url, data, config);
    return response.data.data;
  }

  // PATCH request
  async patch<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.patch<{ success: true; data: T }>(url, data, config);
    return response.data.data;
  }

  // DELETE request
  async delete<T>(url: string, config?: AxiosRequestConfig) {
    const response = await this.client.delete<{ success: true; data: T }>(url, config);
    return response.data.data;
  }

  // File upload
  async upload<T>(url: string, file: FormData, config?: AxiosRequestConfig) {
    const response = await this.client.post<{ success: true; data: T }>(url, file, {
      ...config,
      headers: {
        ...config?.headers,
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data.data;
  }
}

export const apiClient = new ApiClient();
