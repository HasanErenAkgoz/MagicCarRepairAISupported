/**
 * Error Handler Utility
 * API hatalarını handle etmek için utility fonksiyonlar
 */

import { ApiErrorResponse, ErrorCode } from '../types/api';
import { Alert } from 'react-native';

export const handleApiError = (error: any): string => {
  if (error.response?.data) {
    const apiError = error.response.data as ApiErrorResponse;
    
    // Error code'a göre mesaj göster
    switch (apiError.errorCode) {
      case ErrorCode.CLIENT_ID_REQUIRED:
        return 'Client ID gerekli';
      case ErrorCode.VEHICLE_NOT_FOUND:
        return 'Araç bulunamadı';
      case ErrorCode.CUSTOMER_NOT_FOUND:
        return 'Müşteri bulunamadı';
      case ErrorCode.WORKORDER_NOT_FOUND:
        return 'İş emri bulunamadı';
      case ErrorCode.INSUFFICIENT_STOCK:
        return 'Yetersiz stok';
      case ErrorCode.UNAUTHORIZED_ACCESS:
        return 'Yetkisiz erişim';
      case ErrorCode.INVALID_CREDENTIALS:
        return 'Geçersiz kullanıcı adı veya şifre';
      case ErrorCode.EMAIL_ALREADY_EXISTS:
        return 'Bu e-posta adresi zaten kullanılıyor';
      case ErrorCode.INVALID_TOKEN:
        return 'Geçersiz token';
      case ErrorCode.TOKEN_EXPIRED:
        return 'Token süresi dolmuş';
      default:
        return apiError.message || 'Bir hata oluştu';
    }
  }
  
  if (error.message) {
    return error.message;
  }
  
  return 'Bilinmeyen bir hata oluştu';
};

export const showErrorAlert = (error: any) => {
  const message = handleApiError(error);
  Alert.alert('Hata', message);
};
