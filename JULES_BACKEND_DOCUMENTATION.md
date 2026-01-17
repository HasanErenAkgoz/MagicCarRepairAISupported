# JULES BACKEND API DOCUMENTATION - MagicCarRepairAI

> **Bu dokümantasyon Google Jules için hazırlanmıştır.**  
> **Amaç**: React Native mobil uygulama geliştirirken backend API'lerini entegre etmek

---

## 📋 İçindekiler

1. [Genel Bilgiler](#genel-bilgiler)
2. [TypeScript Type Definitions](#typescript-type-definitions)
3. [API Client Setup](#api-client-setup)
4. [Authentication](#authentication)
5. [Multi-Tenant Yapı](#multi-tenant-yapı)
6. [Error Handling](#error-handling)
7. [API Endpoints](#api-endpoints)
8. [Örnek Kodlar](#örnek-kodlar)

---

## 🔧 Genel Bilgiler

### Base Configuration

```typescript
// config/api.ts
export const API_CONFIG = {
  BASE_URL: 'http://localhost:5000/api', // Development
  // BASE_URL: 'https://api.magiccarrepair.com/api', // Production
  TIMEOUT: 30000, // 30 seconds
  RETRY_ATTEMPTS: 3,
  RETRY_DELAY: 1000, // 1 second
};

// Headers
export const DEFAULT_HEADERS = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
};
```

### Response Format

Tüm API yanıtları standart format kullanır:

```typescript
// Success Response
interface ApiSuccessResponse<T> {
  success: true;
  data: T;
}

// Error Response
interface ApiErrorResponse {
  success: false;
  errorCode: string;
  message: string;
  details?: Record<string, any>;
}

// Paginated Response
interface PaginatedResponse<T> {
  success: true;
  data: {
    items: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
  };
}
```

---

## 📝 TypeScript Type Definitions

```typescript
// types/api.ts

// ========== Authentication Types ==========
export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  expiration: string;
  user: User;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  clientId: number;
  userType: UserType;
}

export interface RegisterResponse {
  token: string;
  expiration: string;
  user: User;
}

export interface ForgotPasswordRequest {
  email: string;
}

export interface ResetPasswordRequest {
  email: string;
  token: string;
  newPassword: string;
}

// ========== User Types ==========
export interface User {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  userType: UserType;
  clientId: number;
  isActive: boolean;
}

export enum UserType {
  Admin = 'Admin',
  Manager = 'Manager',
  Mechanic = 'Mechanic',
  Customer = 'Customer',
}

// ========== Customer Types ==========
export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateCustomerRequest {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address?: string;
}

export interface UpdateCustomerRequest {
  firstName?: string;
  lastName?: string;
  email?: string;
  phone?: string;
  address?: string;
}

// ========== Vehicle Types ==========
export interface Vehicle {
  id: number;
  customerId: number;
  licensePlate: string;
  brand: string;
  model: string;
  year: number;
  color?: string;
  kilometers?: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateVehicleRequest {
  customerId: number;
  licensePlate: string;
  brand: string;
  model: string;
  year: number;
  color?: string;
  kilometers?: number;
}

export interface UpdateVehicleRequest {
  licensePlate?: string;
  brand?: string;
  model?: string;
  year?: number;
  color?: string;
  kilometers?: number;
}

// ========== Work Order Types ==========
export enum WorkOrderStatus {
  VehicleEntered = 'VehicleEntered',
  InProgress = 'InProgress',
  WaitingForParts = 'WaitingForParts',
  QualityControl = 'QualityControl',
  Completed = 'Completed',
  Delivered = 'Delivered',
  Cancelled = 'Cancelled',
}

export enum WorkOrderPriority {
  Low = 'Low',
  Normal = 'Normal',
  Urgent = 'Urgent',
}

export interface WorkOrder {
  id: number;
  vehicleId: number;
  customerId: number;
  assignedEmployeeId?: number;
  status: WorkOrderStatus;
  priority: WorkOrderPriority;
  customerComplaints?: string;
  estimatedCompletionDate?: string;
  actualCompletionDate?: string;
  deliveryDate?: string;
  totalAmount: number;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateWorkOrderRequest {
  vehicleId: number;
  customerId: number;
  assignedEmployeeId?: number;
  priority: WorkOrderPriority;
  description?: string;
  customerComplaints?: string;
  estimatedCompletionDate?: string;
}

export interface UpdateWorkOrderStatusRequest {
  status: WorkOrderStatus;
  notes?: string;
}

// ========== Part Types ==========
export enum PartCategory {
  Engine = 'Engine',
  Brake = 'Brake',
  Suspension = 'Suspension',
  Electrical = 'Electrical',
  Body = 'Body',
  Transmission = 'Transmission',
  Cooling = 'Cooling',
  Exhaust = 'Exhaust',
  Other = 'Other',
}

export enum BrandType {
  Original = 'Original',
  Equivalent = 'Equivalent',
}

export interface Part {
  id: number;
  partCode: string;
  name: string;
  category: PartCategory;
  brandType: BrandType;
  purchasePrice: number;
  salePrice: number;
  taxRate: number;
  stockQuantity: number;
  minimumStockLevel: number;
  barcode?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreatePartRequest {
  partCode: string;
  name: string;
  category: PartCategory;
  brandType: BrandType;
  purchasePrice: number;
  salePrice: number;
  taxRate: number;
  minimumStockLevel: number;
  barcode?: string;
}

export interface UpdatePartRequest {
  name?: string;
  salePrice?: number;
  minimumStockLevel?: number;
}

export interface UpdatePartStockRequest {
  quantity: number;
  notes?: string;
}

// ========== Employee Types ==========
export enum EmployeePosition {
  Manager = 'Manager',
  Mechanic = 'Mechanic',
  Electrician = 'Electrician',
  BodyWorker = 'BodyWorker',
  Painter = 'Painter',
  ServiceAdvisor = 'ServiceAdvisor',
  Other = 'Other',
}

export enum EmploymentStatus {
  Active = 'Active',
  OnLeave = 'OnLeave',
  Terminated = 'Terminated',
  Suspended = 'Suspended',
}

export interface Employee {
  id: number;
  employeeNo: string;
  firstName: string;
  lastName: string;
  position: EmployeePosition;
  phone?: string;
  email?: string;
  hireDate: string;
  employmentStatus: EmploymentStatus;
  biography?: string;
  profilePhotoUrl?: string;
  specializations?: string[];
  isPublic: boolean;
  displayOrder?: number;
}

export interface CreateEmployeeRequest {
  employeeNo: string;
  firstName: string;
  lastName: string;
  position: EmployeePosition;
  phone?: string;
  email?: string;
  hireDate: string;
  employmentStatus: EmploymentStatus;
}

export interface UpdateEmployeeRequest {
  firstName?: string;
  lastName?: string;
  position?: EmployeePosition;
  phone?: string;
  email?: string;
  employmentStatus?: EmploymentStatus;
}

// ========== Invoice Types ==========
export enum InvoiceStatus {
  Pending = 'Pending',
  Paid = 'Paid',
  Overdue = 'Overdue',
  Cancelled = 'Cancelled',
}

export enum InvoiceType {
  Sales = 'Sales',
  Purchase = 'Purchase',
}

export interface Invoice {
  id: number;
  invoiceNumber: string;
  invoiceType: InvoiceType;
  workOrderId?: number;
  customerId: number;
  invoiceDate: string;
  dueDate: string;
  status: InvoiceStatus;
  subtotal: number;
  taxAmount: number;
  totalAmount: number;
  paidAmount: number;
  remainingAmount: number;
  description?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateInvoiceRequest {
  invoiceType: InvoiceType;
  workOrderId?: number;
  customerId: number;
  invoiceDate: string;
  dueDate: string;
  description?: string;
  items: InvoiceItemRequest[];
}

export interface InvoiceItemRequest {
  description: string;
  quantity: number;
  unitPrice: number;
  taxRate: number;
}

// ========== Appointment Types ==========
export enum AppointmentStatus {
  Scheduled = 'Scheduled',
  Confirmed = 'Confirmed',
  InProgress = 'InProgress',
  Completed = 'Completed',
  Cancelled = 'Cancelled',
}

export interface Appointment {
  id: number;
  customerId: number;
  vehicleId: number;
  appointmentDate: string;
  status: AppointmentStatus;
  description?: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateAppointmentRequest {
  customerId: number;
  vehicleId: number;
  appointmentDate: string;
  description?: string;
}

// ========== Dashboard Types ==========
export interface DashboardStats {
  todayWorkOrders: number;
  pendingWorkOrders: number;
  dailyRevenue: number;
  criticalStockItems: number;
  overdueInvoices: number;
}

export interface IncomeExpenseChartData {
  date: string;
  income: number;
  expense: number;
}

export interface WorkOrderStatusChartData {
  status: WorkOrderStatus;
  count: number;
}

export interface RecentActivity {
  id: number;
  type: string;
  description: string;
  createdAt: string;
}

export interface TopCustomer {
  customerId: number;
  customerName: string;
  totalSpent: number;
  orderCount: number;
}

// ========== Public Client Profile Types ==========
export interface PublicProfile {
  clientId: number;
  clientCode: string;
  name: string;
  logoUrl?: string;
  websiteUrl?: string;
  aboutUs?: string;
  workingHours?: string;
  services?: string[];
  socialMediaLinks?: Record<string, string>;
  isPublicProfileEnabled: boolean;
}

export interface PublicPortfolioItem {
  id: number;
  workOrderId: number;
  title: string;
  description?: string;
  category?: string;
  photos: string[];
  isApproved: boolean;
  createdAt: string;
}

export interface PublicCertificate {
  id: number;
  title: string;
  issuer?: string;
  issueDate?: string;
  expiryDate?: string;
  certificateFileUrl?: string;
  displayOrder?: number;
}

export interface PublicTeamMember {
  id: number;
  firstName: string;
  lastName: string;
  position: EmployeePosition;
  biography?: string;
  profilePhotoUrl?: string;
  specializations?: string[];
  displayOrder?: number;
}

export interface PublicFacilityPhoto {
  id: number;
  photoUrl: string;
  category?: string;
  description?: string;
  displayOrder?: number;
}

export interface PublicStatistics {
  totalWorkOrders: number;
  completedWorkOrders: number;
  averageRating: number;
  totalReviews: number;
  yearsInBusiness?: number;
}

export interface PublicReview {
  id: number;
  customerName: string;
  rating: number;
  serviceQuality: number;
  priceValue: number;
  onTimeDelivery: number;
  staffBehavior: number;
  comment?: string;
  photos?: string[];
  createdAt?: string;
  reply?: string;
  replyDate?: string;
}

// ========== Error Types ==========
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
```

---

## 🔌 API Client Setup

### Axios Instance

```typescript
// services/api/client.ts
import axios, { AxiosInstance, AxiosError, AxiosRequestConfig } from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { API_CONFIG, DEFAULT_HEADERS } from '../config/api';
import { ApiErrorResponse } from '../types/api';

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

        // ClientId'yi AsyncStorage'dan al veya header'dan al
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
          // Error toast göster
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
    const response = await this.client.get<ApiSuccessResponse<T>>(url, config);
    return response.data.data;
  }

  // POST request
  async post<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.post<ApiSuccessResponse<T>>(url, data, config);
    return response.data.data;
  }

  // PUT request
  async put<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.put<ApiSuccessResponse<T>>(url, data, config);
    return response.data.data;
  }

  // PATCH request
  async patch<T>(url: string, data?: any, config?: AxiosRequestConfig) {
    const response = await this.client.patch<ApiSuccessResponse<T>>(url, data, config);
    return response.data.data;
  }

  // DELETE request
  async delete<T>(url: string, config?: AxiosRequestConfig) {
    const response = await this.client.delete<ApiSuccessResponse<T>>(url, config);
    return response.data.data;
  }

  // File upload
  async upload<T>(url: string, file: FormData, config?: AxiosRequestConfig) {
    const response = await this.client.post<ApiSuccessResponse<T>>(url, file, {
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
```

---

## 🔐 Authentication

### Auth Service

```typescript
// services/api/auth.service.ts
import { apiClient } from './client';
import AsyncStorage from '@react-native-async-storage/async-storage';
import {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RegisterResponse,
  ForgotPasswordRequest,
  ResetPasswordRequest,
} from '../../types/api';

class AuthService {
  private readonly TOKEN_KEY = 'auth_token';
  private readonly USER_KEY = 'user_data';
  private readonly CLIENT_ID_KEY = 'client_id';

  async login(request: LoginRequest): Promise<LoginResponse> {
    const response = await apiClient.post<LoginResponse>('/auth/login', request);
    
    // Token'ı kaydet
    await AsyncStorage.setItem(this.TOKEN_KEY, response.token);
    await AsyncStorage.setItem(this.USER_KEY, JSON.stringify(response.user));
    await AsyncStorage.setItem(this.CLIENT_ID_KEY, response.user.clientId.toString());
    
    // ClientId'yi API client'a set et
    apiClient.setClientId(response.user.clientId);
    
    return response;
  }

  async register(request: RegisterRequest): Promise<RegisterResponse> {
    const response = await apiClient.post<RegisterResponse>('/auth/register', request);
    
    // Token'ı kaydet
    await AsyncStorage.setItem(this.TOKEN_KEY, response.token);
    await AsyncStorage.setItem(this.USER_KEY, JSON.stringify(response.user));
    await AsyncStorage.setItem(this.CLIENT_ID_KEY, response.user.clientId.toString());
    
    // ClientId'yi API client'a set et
    apiClient.setClientId(response.user.clientId);
    
    return response;
  }

  async forgotPassword(request: ForgotPasswordRequest): Promise<void> {
    await apiClient.post('/auth/forgot-password', request);
  }

  async resetPassword(request: ResetPasswordRequest): Promise<void> {
    await apiClient.post('/auth/reset-password', request);
  }

  async logout(): Promise<void> {
    await AsyncStorage.removeItem(this.TOKEN_KEY);
    await AsyncStorage.removeItem(this.USER_KEY);
    await AsyncStorage.removeItem(this.CLIENT_ID_KEY);
  }

  async getToken(): Promise<string | null> {
    return await AsyncStorage.getItem(this.TOKEN_KEY);
  }

  async getUser(): Promise<any | null> {
    const userJson = await AsyncStorage.getItem(this.USER_KEY);
    return userJson ? JSON.parse(userJson) : null;
  }

  async isAuthenticated(): Promise<boolean> {
    const token = await this.getToken();
    return token !== null;
  }
}

export const authService = new AuthService();
```

---

## 🏢 Multi-Tenant Yapı

### Önemli Notlar

1. **Her istekte `X-Client-Id` header'ı gönderilmeli**
2. **Token içinde `ClientId` claim'i varsa header'a gerek yok**
3. **Veri izolasyonu otomatik olarak yapılır**

### ClientId Yönetimi

```typescript
// services/api/client.service.ts
import AsyncStorage from '@react-native-async-storage/async-storage';

class ClientService {
  private readonly CLIENT_ID_KEY = 'client_id';

  async setClientId(clientId: number): Promise<void> {
    await AsyncStorage.setItem(this.CLIENT_ID_KEY, clientId.toString());
    apiClient.setClientId(clientId);
  }

  async getClientId(): Promise<number | null> {
    const clientIdStr = await AsyncStorage.getItem(this.CLIENT_ID_KEY);
    return clientIdStr ? parseInt(clientIdStr, 10) : null;
  }

  async clearClientId(): Promise<void> {
    await AsyncStorage.removeItem(this.CLIENT_ID_KEY);
  }
}

export const clientService = new ClientService();
```

---

## ⚠️ Error Handling

### Error Handler Utility

```typescript
// utils/errorHandler.ts
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
```

---

## 📡 API Endpoints

### Authentication Endpoints

```typescript
// services/api/auth.endpoints.ts

POST /api/auth/login
Request: LoginRequest
Response: ApiSuccessResponse<LoginResponse>

POST /api/auth/register
Request: RegisterRequest
Response: ApiSuccessResponse<RegisterResponse>

POST /api/auth/forgot-password
Request: ForgotPasswordRequest
Response: ApiSuccessResponse<{ message: string }>

POST /api/auth/reset-password
Request: ResetPasswordRequest
Response: ApiSuccessResponse<{ message: string }>
```

### Customer Endpoints

```typescript
// services/api/customer.service.ts
import { apiClient } from './client';
import { Customer, CreateCustomerRequest, UpdateCustomerRequest, PaginatedResponse } from '../../types/api';

class CustomerService {
  async getAll(searchTerm?: string, pageNumber?: number, pageSize?: number): Promise<PaginatedResponse<Customer>> {
    const params = new URLSearchParams();
    if (searchTerm) params.append('searchTerm', searchTerm);
    if (pageNumber) params.append('pageNumber', pageNumber.toString());
    if (pageSize) params.append('pageSize', pageSize.toString());
    
    return await apiClient.get<PaginatedResponse<Customer>>(`/customers?${params.toString()}`);
  }

  async getById(id: number): Promise<Customer> {
    return await apiClient.get<Customer>(`/customers/${id}`);
  }

  async create(request: CreateCustomerRequest): Promise<Customer> {
    return await apiClient.post<Customer>('/customers', request);
  }

  async update(id: number, request: UpdateCustomerRequest): Promise<Customer> {
    return await apiClient.put<Customer>(`/customers/${id}`, request);
  }

  async delete(id: number): Promise<void> {
    return await apiClient.delete<void>(`/customers/${id}`);
  }
}

export const customerService = new CustomerService();
```

### Vehicle Endpoints

```typescript
// services/api/vehicle.service.ts
import { apiClient } from './client';
import { Vehicle, CreateVehicleRequest, UpdateVehicleRequest, PaginatedResponse } from '../../types/api';

class VehicleService {
  async getAll(customerId?: number, searchTerm?: string, pageNumber?: number, pageSize?: number): Promise<PaginatedResponse<Vehicle>> {
    const params = new URLSearchParams();
    if (customerId) params.append('customerId', customerId.toString());
    if (searchTerm) params.append('searchTerm', searchTerm);
    if (pageNumber) params.append('pageNumber', pageNumber.toString());
    if (pageSize) params.append('pageSize', pageSize.toString());
    
    return await apiClient.get<PaginatedResponse<Vehicle>>(`/vehicles?${params.toString()}`);
  }

  async getById(id: number): Promise<Vehicle> {
    return await apiClient.get<Vehicle>(`/vehicles/${id}`);
  }

  async getByCustomer(customerId: number): Promise<Vehicle[]> {
    return await apiClient.get<Vehicle[]>(`/vehicles/customer/${customerId}`);
  }

  async create(request: CreateVehicleRequest): Promise<Vehicle> {
    return await apiClient.post<Vehicle>('/vehicles', request);
  }

  async update(id: number, request: UpdateVehicleRequest): Promise<Vehicle> {
    return await apiClient.put<Vehicle>(`/vehicles/${id}`, request);
  }

  async delete(id: number): Promise<void> {
    return await apiClient.delete<void>(`/vehicles/${id}`);
  }
}

export const vehicleService = new VehicleService();
```

### Work Order Endpoints

```typescript
// services/api/workorder.service.ts
import { apiClient } from './client';
import {
  WorkOrder,
  CreateWorkOrderRequest,
  UpdateWorkOrderStatusRequest,
  WorkOrderStatus,
  PaginatedResponse,
} from '../../types/api';

class WorkOrderService {
  async getAll(
    status?: WorkOrderStatus,
    customerId?: number,
    vehicleId?: number,
    employeeId?: number,
    startDate?: string,
    endDate?: string
  ): Promise<PaginatedResponse<WorkOrder>> {
    const params = new URLSearchParams();
    if (status) params.append('status', status);
    if (customerId) params.append('customerId', customerId.toString());
    if (vehicleId) params.append('vehicleId', vehicleId.toString());
    if (employeeId) params.append('employeeId', employeeId.toString());
    if (startDate) params.append('startDate', startDate);
    if (endDate) params.append('endDate', endDate);
    
    return await apiClient.get<PaginatedResponse<WorkOrder>>(`/workorders?${params.toString()}`);
  }

  async getActive(): Promise<WorkOrder[]> {
    return await apiClient.get<WorkOrder[]>('/workorders/active');
  }

  async getById(id: number): Promise<WorkOrder> {
    return await apiClient.get<WorkOrder>(`/workorders/${id}`);
  }

  async getByEmployee(employeeId: number): Promise<WorkOrder[]> {
    return await apiClient.get<WorkOrder[]>(`/workorders/employee/${employeeId}`);
  }

  async create(request: CreateWorkOrderRequest): Promise<WorkOrder> {
    return await apiClient.post<WorkOrder>('/workorders', request);
  }

  async updateStatus(id: number, request: UpdateWorkOrderStatusRequest): Promise<WorkOrder> {
    return await apiClient.put<WorkOrder>(`/workorders/${id}/status`, request);
  }

  async complete(id: number, completionNotes?: string, actualCompletionDate?: string): Promise<WorkOrder> {
    return await apiClient.post<WorkOrder>(`/workorders/${id}/complete`, {
      completionNotes,
      actualCompletionDate,
    });
  }

  async deliver(id: number, deliveryNotes?: string, deliveryDate?: string): Promise<WorkOrder> {
    return await apiClient.post<WorkOrder>(`/workorders/${id}/deliver`, {
      deliveryNotes,
      deliveryDate,
    });
  }
}

export const workOrderService = new WorkOrderService();
```

### Dashboard Endpoints

```typescript
// services/api/dashboard.service.ts
import { apiClient } from './client';
import {
  DashboardStats,
  IncomeExpenseChartData,
  WorkOrderStatusChartData,
  RecentActivity,
  TopCustomer,
} from '../../types/api';

class DashboardService {
  async getStats(): Promise<DashboardStats> {
    return await apiClient.get<DashboardStats>('/dashboard/stats');
  }

  async getIncomeExpenseChart(startDate?: string, endDate?: string): Promise<IncomeExpenseChartData[]> {
    const params = new URLSearchParams();
    if (startDate) params.append('startDate', startDate);
    if (endDate) params.append('endDate', endDate);
    
    return await apiClient.get<IncomeExpenseChartData[]>(`/dashboard/income-expense-chart?${params.toString()}`);
  }

  async getWorkOrderStatusChart(): Promise<WorkOrderStatusChartData[]> {
    return await apiClient.get<WorkOrderStatusChartData[]>('/dashboard/workorder-status-chart');
  }

  async getRecentActivities(limit?: number): Promise<RecentActivity[]> {
    const params = new URLSearchParams();
    if (limit) params.append('limit', limit.toString());
    
    return await apiClient.get<RecentActivity[]>(`/dashboard/recent-activities?${params.toString()}`);
  }

  async getTopCustomers(limit?: number): Promise<TopCustomer[]> {
    const params = new URLSearchParams();
    if (limit) params.append('limit', limit.toString());
    
    return await apiClient.get<TopCustomer[]>(`/dashboard/top-customers?${params.toString()}`);
  }
}

export const dashboardService = new DashboardService();
```

### Public Client Profile Endpoints

```typescript
// services/api/public-client.service.ts
import { apiClient } from './client';
import {
  PublicProfile,
  PublicPortfolioItem,
  PublicCertificate,
  PublicTeamMember,
  PublicFacilityPhoto,
  PublicStatistics,
  PublicReview,
  PaginatedResponse,
} from '../../types/api';

class PublicClientService {
  async getProfile(clientId: number): Promise<PublicProfile> {
    return await apiClient.get<PublicProfile>(`/public/clients/${clientId}`);
  }

  async getProfileByCode(clientCode: string): Promise<PublicProfile> {
    return await apiClient.get<PublicProfile>(`/public/clients/code/${clientCode}`);
  }

  async getPortfolio(
    clientId: number,
    category?: string,
    pageNumber?: number,
    pageSize?: number
  ): Promise<PaginatedResponse<PublicPortfolioItem>> {
    const params = new URLSearchParams();
    if (category) params.append('category', category);
    if (pageNumber) params.append('pageNumber', pageNumber.toString());
    if (pageSize) params.append('pageSize', pageSize.toString());
    
    return await apiClient.get<PaginatedResponse<PublicPortfolioItem>>(
      `/public/clients/${clientId}/portfolio?${params.toString()}`
    );
  }

  async getCertificates(clientId: number): Promise<PublicCertificate[]> {
    return await apiClient.get<PublicCertificate[]>(`/public/clients/${clientId}/certificates`);
  }

  async getTeam(clientId: number): Promise<PublicTeamMember[]> {
    return await apiClient.get<PublicTeamMember[]>(`/public/clients/${clientId}/team`);
  }

  async getFacilities(clientId: number, category?: string): Promise<PublicFacilityPhoto[]> {
    const params = new URLSearchParams();
    if (category) params.append('category', category);
    
    return await apiClient.get<PublicFacilityPhoto[]>(
      `/public/clients/${clientId}/facilities?${params.toString()}`
    );
  }

  async getStatistics(clientId: number): Promise<PublicStatistics> {
    return await apiClient.get<PublicStatistics>(`/public/clients/${clientId}/statistics`);
  }

  async getReviews(
    clientId: number,
    pageNumber?: number,
    pageSize?: number
  ): Promise<PaginatedResponse<PublicReview>> {
    const params = new URLSearchParams();
    if (pageNumber) params.append('pageNumber', pageNumber.toString());
    if (pageSize) params.append('pageSize', pageSize.toString());
    
    return await apiClient.get<PaginatedResponse<PublicReview>>(
      `/public/clients/${clientId}/reviews?${params.toString()}`
    );
  }
}

export const publicClientService = new PublicClientService();
```

---

## 💻 Örnek Kodlar

### Login Screen Example

```typescript
// screens/Auth/LoginScreen.tsx
import React, { useState } from 'react';
import { View, TextInput, Button, Alert } from 'react-native';
import { authService } from '../../services/api/auth.service';
import { showErrorAlert } from '../../utils/errorHandler';

export const LoginScreen = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);

  const handleLogin = async () => {
    try {
      setLoading(true);
      const response = await authService.login({ email, password });
      // Navigation to dashboard
      // navigation.navigate('Dashboard');
    } catch (error) {
      showErrorAlert(error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <View>
      <TextInput
        value={email}
        onChangeText={setEmail}
        placeholder="Email"
        keyboardType="email-address"
        autoCapitalize="none"
      />
      <TextInput
        value={password}
        onChangeText={setPassword}
        placeholder="Password"
        secureTextEntry
      />
      <Button title="Login" onPress={handleLogin} disabled={loading} />
    </View>
  );
};
```

### Dashboard Screen Example

```typescript
// screens/Dashboard/DashboardScreen.tsx
import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, RefreshControl } from 'react-native';
import { dashboardService } from '../../services/api/dashboard.service';
import { DashboardStats } from '../../types/api';
import { showErrorAlert } from '../../utils/errorHandler';

export const DashboardScreen = () => {
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(false);

  const loadStats = async () => {
    try {
      setLoading(true);
      const data = await dashboardService.getStats();
      setStats(data);
    } catch (error) {
      showErrorAlert(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadStats();
  }, []);

  return (
    <View>
      {stats && (
        <>
          <Text>Bugünkü İş Emirleri: {stats.todayWorkOrders}</Text>
          <Text>Bekleyen İş Emirleri: {stats.pendingWorkOrders}</Text>
          <Text>Günlük Gelir: {stats.dailyRevenue} TL</Text>
          <Text>Kritik Stok: {stats.criticalStockItems}</Text>
          <Text>Geciken Faturalar: {stats.overdueInvoices}</Text>
        </>
      )}
    </View>
  );
};
```

### Work Order List Example

```typescript
// screens/WorkOrders/WorkOrderListScreen.tsx
import React, { useEffect, useState } from 'react';
import { View, FlatList, RefreshControl } from 'react-native';
import { workOrderService } from '../../services/api/workorder.service';
import { WorkOrder } from '../../types/api';
import { showErrorAlert } from '../../utils/errorHandler';
import { WorkOrderCard } from '../../components/WorkOrderCard';

export const WorkOrderListScreen = () => {
  const [workOrders, setWorkOrders] = useState<WorkOrder[]>([]);
  const [loading, setLoading] = useState(false);
  const [refreshing, setRefreshing] = useState(false);

  const loadWorkOrders = async () => {
    try {
      setLoading(true);
      const response = await workOrderService.getActive();
      setWorkOrders(response);
    } catch (error) {
      showErrorAlert(error);
    } finally {
      setLoading(false);
    }
  };

  const onRefresh = async () => {
    setRefreshing(true);
    await loadWorkOrders();
    setRefreshing(false);
  };

  useEffect(() => {
    loadWorkOrders();
  }, []);

  return (
    <FlatList
      data={workOrders}
      renderItem={({ item }) => <WorkOrderCard workOrder={item} />}
      keyExtractor={(item) => item.id.toString()}
      refreshControl={
        <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
      }
    />
  );
};
```

---

## 📝 Önemli Notlar

### 1. Authentication
- Login/Register sonrası token AsyncStorage'a kaydedilmeli
- Her istekte token header'a eklenmeli
- 401 hatası alındığında logout yapılmalı

### 2. Multi-Tenant
- Her istekte `X-Client-Id` header'ı gönderilmeli
- Login sonrası ClientId AsyncStorage'a kaydedilmeli
- Token içinde ClientId claim'i varsa header'a gerek yok

### 3. Error Handling
- Tüm API çağrıları try-catch ile sarılmalı
- Error mesajları kullanıcıya gösterilmeli
- 401 hatası için otomatik logout yapılmalı

### 4. Loading States
- Her API çağrısı için loading state kullanılmalı
- Pull-to-refresh için RefreshControl kullanılmalı

### 5. Pagination
- Liste endpoint'leri pagination destekler
- `pageNumber` ve `pageSize` parametreleri kullanılmalı
- Infinite scroll için FlatList `onEndReached` kullanılmalı

### 6. File Upload
- Multipart/form-data kullanılmalı
- FormData ile dosya gönderilmeli

---

## 🚀 Sonuç

Bu dokümantasyon Jules'in backend API'lerini entegre etmesi için gerekli tüm bilgileri içerir:

- ✅ TypeScript type definitions
- ✅ API client setup
- ✅ Authentication flow
- ✅ Multi-tenant yapı
- ✅ Error handling
- ✅ Tüm endpoint'ler
- ✅ Örnek kodlar

**Jules bu dokümantasyonu kullanarak React Native mobil uygulamayı geliştirebilir!**

---

**Son Güncelleme**: 2024
