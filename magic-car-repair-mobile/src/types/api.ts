/**
 * API Type Definitions
 * Backend API için TypeScript type definitions
 * 
 * Bu dosya JULES_BACKEND_DOCUMENTATION.md dosyasındaki type definitions'a göre
 * oluşturulacak. Şimdilik temel yapı hazırlandı.
 */

// Response format
export interface ApiSuccessResponse<T> {
  success: true;
  data: T;
}

export interface ApiErrorResponse {
  success: false;
  errorCode: string;
  message: string;
  details?: Record<string, any>;
}

export interface PaginatedResponse<T> {
  success: true;
  data: {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
  };
}

// Authentication types
export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiration: string;
  user: User;
}

export interface User {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  userType: string;
  clientId: number;
  isActive: boolean;
}

// Error codes
export enum ErrorCode {
  CLIENT_ID_REQUIRED = 'CLIENT_ID_REQUIRED',
  VEHICLE_NOT_FOUND = 'VEHICLE_NOT_FOUND',
  CUSTOMER_NOT_FOUND = 'CUSTOMER_NOT_FOUND',
  WORKORDER_NOT_FOUND = 'WORKORDER_NOT_FOUND',
  INSUFFICIENT_STOCK = 'INSUFFICIENT_STOCK',
  UNAUTHORIZED_ACCESS = 'UNAUTHORIZED_ACCESS',
  INVALID_CREDENTIALS = 'INVALID_CREDENTIALS',
  EMAIL_ALREADY_EXISTS = 'EMAIL_ALREADY_EXISTS',
  INVALID_TOKEN = 'INVALID_TOKEN',
  TOKEN_EXPIRED = 'TOKEN_EXPIRED',
}

// Not: Detaylı type definitions JULES_BACKEND_DOCUMENTATION.md dosyasından
// kopyalanacak ve buraya eklenecek.
