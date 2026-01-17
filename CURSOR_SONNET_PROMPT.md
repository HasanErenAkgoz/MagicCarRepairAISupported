# CURSOR + CLAUDE SONNET 4.5 - MagicCarRepairAI Mobil Uygulama Geliştirme Prompt'u

> **Bu dosya Cursor IDE'de Claude Sonnet 4.5 modeli ile kullanılacak kapsamlı prompt'tur.**  
> **Amaç**: Stitch tasarımlarına göre React Native (Expo) mobil uygulama geliştirmek

---

## 📎 STITCH TASARIMLARINI EKLEME YÖNTEMLERİ

### Yöntem 1: Dosya Olarak Ekleme (ÖNERİLEN) ✅

1. **Stitch'ten tasarımları indir** (PNG, JPG veya Figma dosyası)
2. **Projeye ekle:**
   ```
   magic-car-repair-mobile/
   ├── assets/
   │   ├── designs/
   │   │   ├── login-screen.png
   │   │   ├── dashboard-screen.png
   │   │   ├── work-order-list.png
   │   │   └── ...
   │   └── ...
   ```
3. **Cursor'da dosyaları aç** ve Claude Sonnet'e şunu söyle:
   ```
   "Bu tasarım dosyalarına bakarak (assets/designs/ klasöründeki) 
   React Native ekranlarını oluştur. Tasarımları birebir takip et."
   ```

### Yöntem 2: Cursor'a Sürükle-Bırak (HIZLI) ✅

1. **Stitch tasarımlarını indir**
2. **Cursor IDE'ye sürükle-bırak yap**
3. **Claude Sonnet'e şunu söyle:**
   ```
   "Bu tasarım dosyasına bakarak React Native ekranını oluştur. 
   Tasarımı birebir takip et, renkleri, spacing'i, typography'yi 
   aynen uygula."
   ```

### Yöntem 3: Figma Dosyası (EN İYİSİ) ✅

1. **Stitch'ten Figma dosyasını indir**
2. **Figma'yı aç ve ekran görüntüleri al** (her ekran için)
3. **PNG olarak export et** ve projeye ekle
4. **Veya Figma link'ini paylaş** (Claude Sonnet Figma linklerini okuyabilir)

### Yöntem 4: Prompt'a Referans Ekleme ✅

1. **Tasarım dosyalarını projeye ekle**
2. **Claude Sonnet'e şunu söyle:**
   ```
   "assets/designs/login-screen.png dosyasındaki tasarıma göre 
   LoginScreen.tsx oluştur. Tasarımı birebir takip et."
   ```

### Yöntem 5: Base64 veya URL (ALTERNATİF) ✅

1. **Tasarımı base64'e çevir** veya **cloud'a yükle**
2. **Prompt'a ekle:**
   ```
   "Bu tasarım görseline göre ekranı oluştur: [base64 veya URL]"
   ```

---

## 🎯 ÖNERİLEN YÖNTEM: Dosya + Prompt Kombinasyonu

**En İyi Yaklaşım:**

1. **Tasarımları projeye ekle:**
   ```
   assets/designs/
   ├── 01-login-screen.png
   ├── 02-register-screen.png
   ├── 03-dashboard-screen.png
   ├── 04-work-order-list.png
   └── ...
   ```

2. **Cursor'da Claude Sonnet'e şunu söyle:**
   ```
   "assets/designs/ klasöründeki tasarım dosyalarına bakarak 
   React Native ekranlarını oluştur. Her ekran için ilgili tasarım 
   dosyasını referans al. Tasarımları birebir takip et:
   
   - Renkleri aynen kullan
   - Spacing'i aynen uygula
   - Typography'yi aynen kullan
   - Component'leri aynen yerleştir
   - Animasyonları ekle
   
   Önce 01-login-screen.png'den başla."
   ```

3. **Her ekran için tek tek:**
   ```
   "Şimdi 02-register-screen.png'ye göre RegisterScreen.tsx oluştur"
   ```

---

## 📝 TASARIM DOSYALARI İÇİN ÖRNEK PROMPT

Claude Sonnet'e verebileceğiniz örnek prompt:

```
Bu tasarım dosyasına (assets/designs/login-screen.png) bakarak 
LoginScreen.tsx oluştur.

TASARIM GEREKSİNİMLERİ:
- Tasarımı birebir takip et
- Renkleri aynen kullan (#3B82F6 mavi, #FF6B35 turuncu)
- Spacing'i aynen uygula
- Typography'yi aynen kullan
- Glassmorphism efektlerini ekle
- Gradient background'ları ekle
- Animasyonları ekle (fade in, slide up)
- Haptic feedback ekle

BACKEND API:
- POST /api/auth/login endpoint'ini kullan
- Redux slice ile state yönetimi yap
- Token'ı AsyncStorage'a kaydet
- Error handling ekle

KOD STANDARTLARI:
- TypeScript kullan
- Functional component
- Hooks kullan (useState, useEffect)
- Modern React Native patterns
```

---

## ✅ SONUÇ

**Evet, sürükle-bırak yapabilirsin!** 

En kolay yol:
1. Stitch tasarımlarını indir
2. Cursor'a sürükle-bırak yap (assets/designs/ klasörüne)
3. Claude Sonnet'e: "Bu tasarım dosyalarına bakarak ekranları oluştur"

Veya:
1. Tasarımları projeye ekle
2. Prompt'a referans ekle: "assets/designs/login-screen.png'ye göre..."

Her iki yöntem de çalışır! 🚀

---

## 🎯 PROJE GENEL BİLGİSİ

**Proje Adı**: MagicCarRepairAI - Oto Servis Yönetim Sistemi  
**Platform**: React Native (Expo SDK 50+)  
**Backend API**: .NET Core Web API  
**Base URL**: `http://localhost:5000/api` (development), production'da değiştirilebilir  
**Authentication**: JWT Bearer Token  
**Multi-Tenant**: Her istekte `X-Client-Id` header'ı gerekli (token'dan da alınabilir)  
**Dil Desteği**: TR, EN, AR (Accept-Language header ile)

---

## 📁 PROJE YAPISI (ÖNEMLİ - BU YAPIDA OLUŞTUR)

```
magic-car-repair-mobile/
├── src/
│   ├── screens/              # Ekranlar
│   │   ├── Auth/
│   │   │   ├── LoginScreen.tsx
│   │   │   ├── RegisterScreen.tsx
│   │   │   ├── ForgotPasswordScreen.tsx
│   │   │   └── ResetPasswordScreen.tsx
│   │   ├── Dashboard/
│   │   │   ├── AdminDashboardScreen.tsx
│   │   │   ├── EmployeeDashboardScreen.tsx
│   │   │   └── CustomerDashboardScreen.tsx
│   │   ├── WorkOrders/
│   │   │   ├── WorkOrderListScreen.tsx
│   │   │   ├── WorkOrderDetailScreen.tsx
│   │   │   └── CreateWorkOrderScreen.tsx
│   │   ├── Customers/
│   │   │   ├── CustomerListScreen.tsx
│   │   │   ├── CustomerDetailScreen.tsx
│   │   │   └── CreateCustomerScreen.tsx
│   │   ├── Vehicles/
│   │   │   ├── VehicleListScreen.tsx
│   │   │   ├── VehicleDetailScreen.tsx
│   │   │   └── CreateVehicleScreen.tsx
│   │   ├── Parts/
│   │   │   ├── PartsListScreen.tsx
│   │   │   ├── PartDetailScreen.tsx
│   │   │   └── BarcodeScannerScreen.tsx
│   │   ├── Appointments/
│   │   │   ├── AppointmentCalendarScreen.tsx
│   │   │   ├── AppointmentListScreen.tsx
│   │   │   └── CreateAppointmentScreen.tsx
│   │   ├── Invoices/
│   │   │   ├── InvoiceListScreen.tsx
│   │   │   └── InvoiceDetailScreen.tsx
│   │   ├── AI/
│   │   │   ├── ChatScreen.tsx
│   │   │   └── PhotoAnalysisScreen.tsx
│   │   └── Profile/
│   │       ├── ProfileScreen.tsx
│   │       └── SettingsScreen.tsx
│   ├── components/           # Reusable component'ler
│   │   ├── common/
│   │   │   ├── Button.tsx
│   │   │   ├── Input.tsx
│   │   │   ├── Card.tsx
│   │   │   ├── Badge.tsx
│   │   │   ├── LoadingSpinner.tsx
│   │   │   ├── EmptyState.tsx
│   │   │   └── ErrorState.tsx
│   │   ├── charts/
│   │   │   ├── LineChart.tsx
│   │   │   ├── PieChart.tsx
│   │   │   └── BarChart.tsx
│   │   └── forms/
│   │       ├── FormInput.tsx
│   │       ├── FormSelect.tsx
│   │       └── FormDatePicker.tsx
│   ├── navigation/           # Navigation yapısı
│   │   ├── AppNavigator.tsx
│   │   ├── AuthNavigator.tsx
│   │   ├── MainNavigator.tsx
│   │   └── types.ts
│   ├── services/             # API servisleri
│   │   ├── api/
│   │   │   ├── client.ts          # Axios instance
│   │   │   ├── auth.service.ts
│   │   │   ├── workOrder.service.ts
│   │   │   ├── customer.service.ts
│   │   │   ├── vehicle.service.ts
│   │   │   ├── part.service.ts
│   │   │   ├── appointment.service.ts
│   │   │   ├── invoice.service.ts
│   │   │   └── ai.service.ts
│   │   └── storage/
│   │       └── asyncStorage.ts
│   ├── store/                # Redux store
│   │   ├── index.ts
│   │   ├── slices/
│   │   │   ├── authSlice.ts
│   │   │   ├── workOrderSlice.ts
│   │   │   ├── customerSlice.ts
│   │   │   └── appSlice.ts
│   │   └── hooks.ts
│   ├── theme/                # Tema ve stil
│   │   ├── colors.ts
│   │   ├── typography.ts
│   │   ├── spacing.ts
│   │   └── index.ts
│   ├── utils/                # Yardımcı fonksiyonlar
│   │   ├── formatters.ts
│   │   ├── validators.ts
│   │   └── helpers.ts
│   ├── types/                # TypeScript type'ları
│   │   ├── api.types.ts
│   │   ├── navigation.types.ts
│   │   └── entities.types.ts
│   └── constants/            # Sabitler
│       ├── api.constants.ts
│       └── app.constants.ts
├── App.tsx                   # Ana component
├── app.json                  # Expo config
├── package.json
├── tsconfig.json
└── babel.config.js
```

---

## 🎨 TASARIM SİSTEMİ (KESINLIKLE UYGULA)

### Renk Paleti (ÖNEMLİ - %55 Mavi, %20 Turuncu):

```typescript
// src/theme/colors.ts
export const colors = {
  // Primary (Mavi) - %55 ağırlık
  primary: {
    main: '#3B82F6',
    dark: '#2563EB',
    light: '#60A5FA',
    gradient: ['#3B82F6', '#2563EB'], // Linear gradient
  },
  
  // Accent (Turuncu) - %20 ağırlık
  accent: {
    main: '#FF6B35',
    dark: '#FF8C42',
    light: '#FB923C',
    gradient: ['#FF6B35', '#FF8C42'], // Linear gradient
  },
  
  // Neutral - %20 ağırlık
  background: '#F9FAFB',
  surface: '#FFFFFF',
  text: {
    primary: '#1A1A1A',
    secondary: '#6B7280',
    tertiary: '#9CA3AF',
  },
  border: '#E5E7EB',
  divider: '#D1D5DB',
  
  // Status Colors - %5 ağırlık
  success: '#10B981',
  error: '#EF4444',
  warning: '#F59E0B',
  info: '#3B82F6',
  
  // Dark Mode
  dark: {
    background: '#0F172A',
    surface: '#1E293B',
    text: {
      primary: '#F8FAFC',
      secondary: '#CBD5E1',
      tertiary: '#94A3B8',
    },
    border: '#334155',
    divider: '#475569',
  },
};
```

### Tipografi:

```typescript
// src/theme/typography.ts
export const typography = {
  h1: {
    fontSize: 40,
    fontWeight: '700',
    lineHeight: 48,
    letterSpacing: -0.5,
  },
  h2: {
    fontSize: 32,
    fontWeight: '700',
    lineHeight: 40,
    letterSpacing: -0.25,
  },
  h3: {
    fontSize: 24,
    fontWeight: '600',
    lineHeight: 32,
  },
  bodyLarge: {
    fontSize: 18,
    fontWeight: '400',
    lineHeight: 28,
  },
  body: {
    fontSize: 16,
    fontWeight: '400',
    lineHeight: 24,
  },
  bodySmall: {
    fontSize: 14,
    fontWeight: '400',
    lineHeight: 20,
  },
  caption: {
    fontSize: 12,
    fontWeight: '400',
    lineHeight: 16,
    letterSpacing: 0.5,
  },
};
```

### Spacing:

```typescript
// src/theme/spacing.ts
export const spacing = {
  xs: 4,
  sm: 8,
  md: 16,
  lg: 24,
  xl: 32,
  xxl: 48,
};
```

---

## 🔧 TEKNİK GEREKSİNİMLER

### Paketler (package.json):

```json
{
  "dependencies": {
    "react": "18.2.0",
    "react-native": "0.73.0",
    "expo": "~50.0.0",
    "@react-navigation/native": "^6.1.9",
    "@react-navigation/stack": "^6.3.20",
    "@react-navigation/bottom-tabs": "^6.5.11",
    "@react-navigation/drawer": "^6.6.6",
    "react-native-gesture-handler": "~2.14.0",
    "react-native-reanimated": "~3.6.1",
    "react-native-screens": "~3.29.0",
    "@reduxjs/toolkit": "^2.0.1",
    "react-redux": "^9.0.4",
    "axios": "^1.6.2",
    "@react-native-async-storage/async-storage": "1.21.0",
    "react-native-paper": "^5.11.3",
    "react-native-vector-icons": "^10.0.3",
    "react-native-chart-kit": "^6.12.0",
    "react-native-svg": "13.14.0",
    "expo-image-picker": "~14.7.1",
    "expo-camera": "~14.0.1",
    "expo-barcode-scanner": "~12.7.0",
    "expo-linear-gradient": "~12.7.0",
    "react-native-safe-area-context": "4.8.2",
    "date-fns": "^3.0.6",
    "react-hook-form": "^7.49.2",
    "@hookform/resolvers": "^3.3.2",
    "zod": "^3.22.4"
  },
  "devDependencies": {
    "@types/react": "~18.2.45",
    "@types/react-native": "~0.73.0",
    "typescript": "~5.3.3"
  }
}
```

---

## 🔐 1. AUTHENTICATION MODÜLÜ

### 1.1 API Service Oluştur

**Dosya**: `src/services/api/auth.service.ts`

```typescript
import apiClient from '../api/client';
import { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from '../../types/api.types';

export const authService = {
  login: async (data: LoginRequest): Promise<LoginResponse> => {
    const response = await apiClient.post('/auth/login', data);
    return response.data;
  },

  register: async (data: RegisterRequest): Promise<RegisterResponse> => {
    const response = await apiClient.post('/auth/register', data);
    return response.data;
  },

  forgotPassword: async (email: string): Promise<void> => {
    await apiClient.post('/auth/forgot-password', { email });
  },

  resetPassword: async (email: string, token: string, newPassword: string): Promise<void> => {
    await apiClient.post('/auth/reset-password', { email, token, newPassword });
  },
};
```

### 1.2 Redux Slice Oluştur

**Dosya**: `src/store/slices/authSlice.ts`

```typescript
import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { authService } from '../../services/api/auth.service';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '../../types/entities.types';

interface AuthState {
  user: User | null;
  token: string | null;
  clientId: number | null;
  isAuthenticated: boolean;
  loading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  user: null,
  token: null,
  clientId: null,
  isAuthenticated: false,
  loading: false,
  error: null,
};

export const login = createAsyncThunk(
  'auth/login',
  async ({ email, password }: { email: string; password: string }, { rejectWithValue }) => {
    try {
      const response = await authService.login({ email, password });
      if (response.success && response.data) {
        await AsyncStorage.setItem('token', response.data.token);
        await AsyncStorage.setItem('clientId', response.data.user.clientId.toString());
        await AsyncStorage.setItem('user', JSON.stringify(response.data.user));
        return response.data;
      }
      return rejectWithValue('Login failed');
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Login failed');
    }
  }
);

export const register = createAsyncThunk(
  'auth/register',
  async (data: RegisterRequest, { rejectWithValue }) => {
    try {
      const response = await authService.register(data);
      if (response.success && response.data) {
        await AsyncStorage.setItem('token', response.data.token);
        await AsyncStorage.setItem('clientId', response.data.user.clientId.toString());
        await AsyncStorage.setItem('user', JSON.stringify(response.data.user));
        return response.data;
      }
      return rejectWithValue('Registration failed');
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Registration failed');
    }
  }
);

export const logout = createAsyncThunk('auth/logout', async () => {
  await AsyncStorage.removeItem('token');
  await AsyncStorage.removeItem('clientId');
  await AsyncStorage.removeItem('user');
});

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    clearError: (state) => {
      state.error = null;
    },
    restoreSession: (state, action: PayloadAction<{ user: User; token: string; clientId: number }>) => {
      state.user = action.payload.user;
      state.token = action.payload.token;
      state.clientId = action.payload.clientId;
      state.isAuthenticated = true;
    },
  },
  extraReducers: (builder) => {
    builder
      // Login
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload.user;
        state.token = action.payload.token;
        state.clientId = action.payload.user.clientId;
        state.isAuthenticated = true;
        state.error = null;
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.isAuthenticated = false;
      })
      // Register
      .addCase(register.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(register.fulfilled, (state, action) => {
        state.loading = false;
        state.user = action.payload.user;
        state.token = action.payload.token;
        state.clientId = action.payload.user.clientId;
        state.isAuthenticated = true;
        state.error = null;
      })
      .addCase(register.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
        state.isAuthenticated = false;
      })
      // Logout
      .addCase(logout.fulfilled, (state) => {
        state.user = null;
        state.token = null;
        state.clientId = null;
        state.isAuthenticated = false;
      });
  },
});

export const { clearError, restoreSession } = authSlice.actions;
export default authSlice.reducer;
```

### 1.3 Login Screen Oluştur

**Dosya**: `src/screens/Auth/LoginScreen.tsx`

**TASARIM GEREKSİNİMLERİ:**
- Modern, glassmorphism efektli
- Gradient arka plan (mavi → turuncu)
- Email input (ikon, placeholder, focus state: turuncu border)
- Password input (göz ikonu ile göster/gizle)
- "Beni Hatırla" checkbox
- "Şifremi Unuttum" linki
- Büyük "Giriş Yap" butonu (turuncu gradient)
- Loading state (spinner + disabled button)
- Error message (kırmızı alert, shake animation)
- Biometric authentication seçeneği (Touch ID/Face ID)
- Smooth animations (fade in, slide up)
- Haptic feedback

**KOD YAPISI:**

```typescript
import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  Alert,
} from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import { useDispatch, useSelector } from 'react-redux';
import { login } from '../../store/slices/authSlice';
import { AppDispatch, RootState } from '../../store';
import { colors } from '../../theme/colors';
import { typography } from '../../theme/typography';
import { spacing } from '../../theme/spacing';
import Button from '../../components/common/Button';
import Input from '../../components/common/Input';
import LoadingSpinner from '../../components/common/LoadingSpinner';

const LoginScreen = ({ navigation }: any) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [rememberMe, setRememberMe] = useState(false);
  
  const dispatch = useDispatch<AppDispatch>();
  const { loading, error } = useSelector((state: RootState) => state.auth);

  const handleLogin = async () => {
    if (!email || !password) {
      Alert.alert('Hata', 'Lütfen tüm alanları doldurun');
      return;
    }

    try {
      await dispatch(login({ email, password })).unwrap();
      // Navigation handled by AppNavigator based on auth state
    } catch (err: any) {
      Alert.alert('Giriş Hatası', err || 'Giriş yapılamadı');
    }
  };

  return (
    <KeyboardAvoidingView
      style={styles.container}
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
    >
      <LinearGradient
        colors={[colors.primary.main, colors.accent.main]}
        style={styles.gradientBackground}
      />
      
      <ScrollView
        contentContainerStyle={styles.scrollContent}
        keyboardShouldPersistTaps="handled"
      >
        {/* Logo Area */}
        <View style={styles.logoContainer}>
          <Text style={styles.logoText}>MagicCarRepair</Text>
        </View>

        {/* Email Input */}
        <Input
          label="E-posta"
          value={email}
          onChangeText={setEmail}
          placeholder="E-posta adresiniz"
          keyboardType="email-address"
          autoCapitalize="none"
          leftIcon="email"
          style={styles.input}
        />

        {/* Password Input */}
        <Input
          label="Şifre"
          value={password}
          onChangeText={setPassword}
          placeholder="Şifreniz"
          secureTextEntry={!showPassword}
          rightIcon={showPassword ? 'eye-off' : 'eye'}
          onRightIconPress={() => setShowPassword(!showPassword)}
          style={styles.input}
        />

        {/* Remember Me & Forgot Password */}
        <View style={styles.optionsRow}>
          <TouchableOpacity
            style={styles.checkboxContainer}
            onPress={() => setRememberMe(!rememberMe)}
          >
            {/* Custom Checkbox */}
            <View style={[styles.checkbox, rememberMe && styles.checkboxChecked]} />
            <Text style={styles.checkboxLabel}>Beni Hatırla</Text>
          </TouchableOpacity>
          
          <TouchableOpacity onPress={() => navigation.navigate('ForgotPassword')}>
            <Text style={styles.forgotPasswordText}>Şifremi Unuttum</Text>
          </TouchableOpacity>
        </View>

        {/* Error Message */}
        {error && (
          <View style={styles.errorContainer}>
            <Text style={styles.errorText}>{error}</Text>
          </View>
        )}

        {/* Login Button */}
        <Button
          title="Giriş Yap"
          onPress={handleLogin}
          loading={loading}
          style={styles.loginButton}
          gradient={colors.accent.gradient}
        />

        {/* Register Link */}
        <TouchableOpacity
          style={styles.registerLink}
          onPress={() => navigation.navigate('Register')}
        >
          <Text style={styles.registerLinkText}>
            Hesabınız yok mu? <Text style={styles.registerLinkBold}>Kayıt Ol</Text>
          </Text>
        </TouchableOpacity>

        {/* Biometric Auth */}
        <TouchableOpacity style={styles.biometricButton}>
          {/* Touch ID / Face ID Icon */}
          <Text style={styles.biometricText}>Hızlı Giriş</Text>
        </TouchableOpacity>
      </ScrollView>
    </KeyboardAvoidingView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  gradientBackground: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
  },
  scrollContent: {
    flexGrow: 1,
    padding: spacing.lg,
    justifyContent: 'center',
  },
  logoContainer: {
    alignItems: 'center',
    marginBottom: spacing.xl * 2,
  },
  logoText: {
    ...typography.h1,
    color: colors.surface,
    fontWeight: 'bold',
  },
  input: {
    marginBottom: spacing.md,
  },
  optionsRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: spacing.md,
  },
  checkboxContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  checkbox: {
    width: 20,
    height: 20,
    borderWidth: 2,
    borderColor: colors.border,
    borderRadius: 4,
    marginRight: spacing.sm,
  },
  checkboxChecked: {
    backgroundColor: colors.accent.main,
    borderColor: colors.accent.main,
  },
  checkboxLabel: {
    ...typography.bodySmall,
    color: colors.text.secondary,
  },
  forgotPasswordText: {
    ...typography.bodySmall,
    color: colors.accent.main,
    fontWeight: '500',
  },
  errorContainer: {
    backgroundColor: colors.error + '20',
    padding: spacing.md,
    borderRadius: 12,
    marginBottom: spacing.md,
  },
  errorText: {
    ...typography.bodySmall,
    color: colors.error,
  },
  loginButton: {
    marginTop: spacing.md,
    marginBottom: spacing.lg,
  },
  registerLink: {
    alignItems: 'center',
    marginBottom: spacing.lg,
  },
  registerLinkText: {
    ...typography.body,
    color: colors.text.secondary,
  },
  registerLinkBold: {
    fontWeight: '600',
    color: colors.accent.main,
  },
  biometricButton: {
    alignItems: 'center',
    padding: spacing.md,
  },
  biometricText: {
    ...typography.bodySmall,
    color: colors.surface,
  },
});

export default LoginScreen;
```

---

## 📊 2. DASHBOARD MODÜLÜ

### 2.1 Dashboard API Service

**Dosya**: `src/services/api/dashboard.service.ts`

```typescript
import apiClient from '../api/client';

export interface DashboardStatistics {
  totalWorkOrders: number;
  activeWorkOrders: number;
  todayRevenue: number;
  pendingApprovals: number;
  totalCustomers: number;
  totalVehicles: number;
}

export const dashboardService = {
  getStatistics: async (): Promise<DashboardStatistics> => {
    const response = await apiClient.get('/dashboard/statistics');
    return response.data;
  },
};
```

### 2.2 Admin Dashboard Screen

**Dosya**: `src/screens/Dashboard/AdminDashboardScreen.tsx`

**TASARIM GEREKSİNİMLERİ:**
- Hoş geldiniz bölümü ("Merhaba, [İsim]!")
- 4 istatistik kartı (horizontal scrollable):
  1. Toplam İş Emri (mavi gradient)
  2. Aktif İş Emirleri (turuncu gradient)
  3. Bugünkü Gelir (yeşil gradient)
  4. Bekleyen Onaylar (kırmızı gradient)
- Grafikler: Gelir-Gider (line chart), İş Emri Durum Dağılımı (pie chart)
- Son Aktiviteler (timeline formatında)
- Pull-to-refresh
- Loading skeleton screens

**KOD YAPISI:**

```typescript
import React, { useEffect, useState } from 'react';
import {
  View,
  Text,
  ScrollView,
  StyleSheet,
  RefreshControl,
  Dimensions,
} from 'react-native';
import { useDispatch, useSelector } from 'react-redux';
import { LineChart, PieChart } from 'react-native-chart-kit';
import { dashboardService, DashboardStatistics } from '../../services/api/dashboard.service';
import { colors } from '../../theme/colors';
import { typography } from '../../theme/typography';
import { spacing } from '../../theme/spacing';
import StatCard from '../../components/common/StatCard';
import LoadingSkeleton from '../../components/common/LoadingSkeleton';
import ActivityTimeline from '../../components/common/ActivityTimeline';

const { width } = Dimensions.get('window');

const AdminDashboardScreen = () => {
  const [statistics, setStatistics] = useState<DashboardStatistics | null>(null);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);
  
  const { user } = useSelector((state: RootState) => state.auth);

  const loadStatistics = async () => {
    try {
      const data = await dashboardService.getStatistics();
      setStatistics(data);
    } catch (error) {
      console.error('Failed to load statistics:', error);
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  useEffect(() => {
    loadStatistics();
  }, []);

  const onRefresh = () => {
    setRefreshing(true);
    loadStatistics();
  };

  if (loading) {
    return <LoadingSkeleton />;
  }

  return (
    <ScrollView
      style={styles.container}
      refreshControl={
        <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
      }
    >
      {/* Welcome Section */}
      <View style={styles.welcomeSection}>
        <Text style={styles.welcomeText}>
          Merhaba, {user?.firstName}!
        </Text>
        <Text style={styles.dateText}>
          {new Date().toLocaleDateString('tr-TR', {
            weekday: 'long',
            year: 'numeric',
            month: 'long',
            day: 'numeric',
          })}
        </Text>
      </View>

      {/* Statistics Cards */}
      <ScrollView
        horizontal
        showsHorizontalScrollIndicator={false}
        style={styles.statsContainer}
        contentContainerStyle={styles.statsContent}
      >
        <StatCard
          title="Toplam İş Emri"
          value={statistics?.totalWorkOrders || 0}
          icon="clipboard"
          gradient={colors.primary.gradient}
          trend={{ value: 12, isPositive: true }}
        />
        <StatCard
          title="Aktif İş Emirleri"
          value={statistics?.activeWorkOrders || 0}
          icon="wrench"
          gradient={colors.accent.gradient}
        />
        <StatCard
          title="Bugünkü Gelir"
          value={`${statistics?.todayRevenue?.toLocaleString('tr-TR') || 0} ₺`}
          icon="money"
          gradient={['#10B981', '#059669']}
        />
        <StatCard
          title="Bekleyen Onaylar"
          value={statistics?.pendingApprovals || 0}
          icon="clock"
          gradient={[colors.error, '#DC2626']}
          badge={statistics?.pendingApprovals || 0}
        />
      </ScrollView>

      {/* Charts Section */}
      <View style={styles.chartsSection}>
        {/* Revenue Chart */}
        <View style={styles.chartCard}>
          <Text style={styles.chartTitle}>Gelir - Gider Grafiği</Text>
          <LineChart
            data={{
              labels: ['Pzt', 'Sal', 'Çar', 'Per', 'Cum', 'Cmt', 'Paz'],
              datasets: [
                {
                  data: [12000, 15000, 18000, 14000, 20000, 16000, 22000],
                  color: (opacity = 1) => `rgba(59, 130, 246, ${opacity})`,
                },
                {
                  data: [8000, 10000, 12000, 9000, 13000, 11000, 15000],
                  color: (opacity = 1) => `rgba(239, 68, 68, ${opacity})`,
                },
              ],
            }}
            width={width - spacing.lg * 2}
            height={220}
            chartConfig={{
              backgroundColor: colors.surface,
              backgroundGradientFrom: colors.surface,
              backgroundGradientTo: colors.surface,
              decimalPlaces: 0,
              color: (opacity = 1) => `rgba(26, 26, 26, ${opacity})`,
              labelColor: (opacity = 1) => `rgba(107, 114, 128, ${opacity})`,
              style: {
                borderRadius: 16,
              },
            }}
            bezier
            style={styles.chart}
          />
        </View>

        {/* Work Order Status Pie Chart */}
        <View style={styles.chartCard}>
          <Text style={styles.chartTitle}>İş Emri Durum Dağılımı</Text>
          <PieChart
            data={[
              { name: 'Aktif', population: statistics?.activeWorkOrders || 0, color: colors.accent.main },
              { name: 'Tamamlandı', population: 50, color: colors.success },
              { name: 'Beklemede', population: statistics?.pendingApprovals || 0, color: colors.warning },
            ]}
            width={width - spacing.lg * 2}
            height={220}
            chartConfig={{
              color: (opacity = 1) => `rgba(26, 26, 26, ${opacity})`,
            }}
            accessor="population"
            backgroundColor="transparent"
            paddingLeft="15"
            style={styles.chart}
          />
        </View>
      </View>

      {/* Recent Activities */}
      <View style={styles.activitiesSection}>
        <Text style={styles.sectionTitle}>Son Aktiviteler</Text>
        <ActivityTimeline />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },
  welcomeSection: {
    padding: spacing.lg,
    backgroundColor: colors.surface,
    marginBottom: spacing.md,
  },
  welcomeText: {
    ...typography.h2,
    color: colors.text.primary,
    marginBottom: spacing.xs,
  },
  dateText: {
    ...typography.bodySmall,
    color: colors.text.secondary,
  },
  statsContainer: {
    marginBottom: spacing.lg,
  },
  statsContent: {
    paddingHorizontal: spacing.lg,
    gap: spacing.md,
  },
  chartsSection: {
    padding: spacing.lg,
    gap: spacing.lg,
  },
  chartCard: {
    backgroundColor: colors.surface,
    borderRadius: 16,
    padding: spacing.md,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 8,
    elevation: 4,
  },
  chartTitle: {
    ...typography.h3,
    color: colors.text.primary,
    marginBottom: spacing.md,
  },
  chart: {
    borderRadius: 16,
  },
  activitiesSection: {
    padding: spacing.lg,
  },
  sectionTitle: {
    ...typography.h3,
    color: colors.text.primary,
    marginBottom: spacing.md,
  },
});

export default AdminDashboardScreen;
```

---

## 🔧 3. WORK ORDERS MODÜLÜ

### 3.1 Work Order API Service

**Dosya**: `src/services/api/workOrder.service.ts`

```typescript
import apiClient from '../api/client';

export interface WorkOrder {
  id: number;
  workOrderNumber: string;
  customerId: number;
  customerName: string;
  vehicleId: number;
  vehicleInfo: string;
  status: 'AppointmentScheduled' | 'VehicleEntry' | 'InProgress' | 'Ready' | 'Delivered' | 'Cancelled';
  priority: 'Low' | 'Normal' | 'High' | 'Urgent';
  totalAmount: number;
  entryDate: string;
  estimatedDeliveryDate: string;
}

export interface WorkOrderListResponse {
  items: WorkOrder[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

export interface CreateWorkOrderRequest {
  customerId: number;
  vehicleId: number;
  priority: string;
  customerComplaints: string;
  estimatedDeliveryDate: string;
}

export const workOrderService = {
  getAll: async (params?: {
    pageNumber?: number;
    pageSize?: number;
    status?: string;
    searchTerm?: string;
  }): Promise<WorkOrderListResponse> => {
    const response = await apiClient.get('/workorders', { params });
    return response.data;
  },

  getById: async (id: number): Promise<WorkOrder> => {
    const response = await apiClient.get(`/workorders/${id}`);
    return response.data;
  },

  create: async (data: CreateWorkOrderRequest): Promise<WorkOrder> => {
    const response = await apiClient.post('/workorders', data);
    return response.data;
  },

  updateStatus: async (id: number, status: string, notes?: string): Promise<void> => {
    await apiClient.put(`/workorders/${id}/status`, { status, notes });
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/workorders/${id}`);
  },
};
```

### 3.2 Work Order List Screen

**Dosya**: `src/screens/WorkOrders/WorkOrderListScreen.tsx`

**TASARIM GEREKSİNİMLERİ:**
- Modern arama bar (sticky, glassmorphism)
- Filtre chips (Tümü, Aktif, Tamamlandı, Beklemede, İptal)
- Her kart: Durum çubuğu (sol, renkli), iş emri no, müşteri bilgisi, araç bilgisi, durum badge, öncelik, tutar
- Swipe actions (sol: durum güncelle, sağ: sil)
- Pull-to-refresh
- Infinite scroll veya pagination
- Empty state (büyük ikon + aksiyon butonu)
- FAB: "Yeni İş Emri" (turuncu gradient)

**KOD YAPISI:**

```typescript
import React, { useState, useEffect, useCallback } from 'react';
import {
  View,
  Text,
  FlatList,
  StyleSheet,
  TouchableOpacity,
  RefreshControl,
} from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { FAB } from 'react-native-paper';
import { workOrderService, WorkOrder } from '../../services/api/workOrder.service';
import { colors } from '../../theme/colors';
import { typography } from '../../theme/typography';
import { spacing } from '../../theme/spacing';
import WorkOrderCard from '../../components/workorders/WorkOrderCard';
import SearchBar from '../../components/common/SearchBar';
import FilterChips from '../../components/common/FilterChips';
import EmptyState from '../../components/common/EmptyState';

const WorkOrderListScreen = () => {
  const navigation = useNavigation();
  const [workOrders, setWorkOrders] = useState<WorkOrder[]>([]);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedFilter, setSelectedFilter] = useState('All');
  const [pageNumber, setPageNumber] = useState(1);
  const [hasMore, setHasMore] = useState(true);

  const filters = [
    { id: 'All', label: 'Tümü', count: 0 },
    { id: 'Active', label: 'Aktif', count: 0 },
    { id: 'Completed', label: 'Tamamlandı', count: 0 },
    { id: 'Pending', label: 'Beklemede', count: 0 },
    { id: 'Cancelled', label: 'İptal', count: 0 },
  ];

  const loadWorkOrders = async (page: number = 1, reset: boolean = false) => {
    try {
      const params: any = {
        pageNumber: page,
        pageSize: 20,
      };

      if (selectedFilter !== 'All') {
        params.status = selectedFilter;
      }

      if (searchTerm) {
        params.searchTerm = searchTerm;
      }

      const response = await workOrderService.getAll(params);
      
      if (reset) {
        setWorkOrders(response.items);
      } else {
        setWorkOrders((prev) => [...prev, ...response.items]);
      }

      setHasMore(response.items.length === 20);
    } catch (error) {
      console.error('Failed to load work orders:', error);
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  useEffect(() => {
    loadWorkOrders(1, true);
  }, [selectedFilter, searchTerm]);

  const onRefresh = () => {
    setRefreshing(true);
    setPageNumber(1);
    loadWorkOrders(1, true);
  };

  const loadMore = () => {
    if (!loading && hasMore) {
      const nextPage = pageNumber + 1;
      setPageNumber(nextPage);
      loadWorkOrders(nextPage, false);
    }
  };

  const renderWorkOrder = ({ item }: { item: WorkOrder }) => (
    <WorkOrderCard
      workOrder={item}
      onPress={() => navigation.navigate('WorkOrderDetail', { id: item.id })}
      onSwipeLeft={() => {/* Status update */}}
      onSwipeRight={() => {/* Delete */}}
    />
  );

  if (loading && workOrders.length === 0) {
    return <LoadingSkeleton />;
  }

  return (
    <View style={styles.container}>
      {/* Search Bar */}
      <SearchBar
        value={searchTerm}
        onChangeText={setSearchTerm}
        placeholder="İş emri no, müşteri, plaka ara..."
        style={styles.searchBar}
      />

      {/* Filter Chips */}
      <FilterChips
        filters={filters}
        selected={selectedFilter}
        onSelect={setSelectedFilter}
        style={styles.filters}
      />

      {/* Work Orders List */}
      <FlatList
        data={workOrders}
        renderItem={renderWorkOrder}
        keyExtractor={(item) => item.id.toString()}
        contentContainerStyle={styles.listContent}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        onEndReached={loadMore}
        onEndReachedThreshold={0.5}
        ListEmptyComponent={
          <EmptyState
            icon="clipboard-outline"
            title="Henüz iş emri yok"
            description="Yeni bir iş emri oluşturmak için + butonuna basın"
            actionLabel="Yeni İş Emri Oluştur"
            onAction={() => navigation.navigate('CreateWorkOrder')}
          />
        }
      />

      {/* FAB */}
      <FAB
        icon="plus"
        style={styles.fab}
        onPress={() => navigation.navigate('CreateWorkOrder')}
        color={colors.surface}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },
  searchBar: {
    margin: spacing.lg,
    marginBottom: spacing.md,
  },
  filters: {
    marginHorizontal: spacing.lg,
    marginBottom: spacing.md,
  },
  listContent: {
    padding: spacing.lg,
    paddingBottom: 100, // FAB için alan
  },
  fab: {
    position: 'absolute',
    right: spacing.lg,
    bottom: spacing.lg,
    backgroundColor: colors.accent.main,
  },
});

export default WorkOrderListScreen;
```

---

## 📝 4. API CLIENT SETUP

### 4.1 Axios Client Configuration

**Dosya**: `src/services/api/client.ts`

```typescript
import axios, { AxiosInstance, AxiosError } from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';

const API_BASE_URL = __DEV__ 
  ? 'http://localhost:5000/api' 
  : 'https://api.magiccarrepair.com/api';

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
    'Accept-Language': 'tr',
  },
});

// Request Interceptor - Token ve ClientId ekle
apiClient.interceptors.request.use(
  async (config) => {
    const token = await AsyncStorage.getItem('token');
    const clientId = await AsyncStorage.getItem('clientId');

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    if (clientId) {
      config.headers['X-Client-Id'] = clientId;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response Interceptor - Error handling
apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    if (error.response?.status === 401) {
      // Token expired - Clear storage and redirect to login
      await AsyncStorage.removeItem('token');
      await AsyncStorage.removeItem('clientId');
      await AsyncStorage.removeItem('user');
      // Navigate to login (handled by AppNavigator)
    }

    return Promise.reject(error);
  }
);

export default apiClient;
```

---

## 🎨 5. COMPONENT ÖRNEKLERİ

### 5.1 Button Component

**Dosya**: `src/components/common/Button.tsx`

```typescript
import React from 'react';
import { TouchableOpacity, Text, StyleSheet, ActivityIndicator } from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import { colors } from '../../theme/colors';
import { typography } from '../../theme/typography';

interface ButtonProps {
  title: string;
  onPress: () => void;
  loading?: boolean;
  disabled?: boolean;
  variant?: 'primary' | 'secondary' | 'outline';
  gradient?: string[];
  style?: any;
}

const Button: React.FC<ButtonProps> = ({
  title,
  onPress,
  loading = false,
  disabled = false,
  variant = 'primary',
  gradient,
  style,
}) => {
  const isGradient = variant === 'primary' || gradient;

  const buttonContent = (
    <>
      {loading ? (
        <ActivityIndicator color={colors.surface} />
      ) : (
        <Text style={[styles.text, variant === 'outline' && styles.outlineText]}>
          {title}
        </Text>
      )}
    </>
  );

  if (isGradient && !disabled) {
    return (
      <TouchableOpacity
        onPress={onPress}
        disabled={disabled || loading}
        style={[styles.container, style]}
        activeOpacity={0.8}
      >
        <LinearGradient
          colors={gradient || colors.accent.gradient}
          start={{ x: 0, y: 0 }}
          end={{ x: 1, y: 0 }}
          style={styles.gradient}
        >
          {buttonContent}
        </LinearGradient>
      </TouchableOpacity>
    );
  }

  return (
    <TouchableOpacity
      onPress={onPress}
      disabled={disabled || loading}
      style={[
        styles.container,
        variant === 'outline' && styles.outline,
        variant === 'secondary' && styles.secondary,
        disabled && styles.disabled,
        style,
      ]}
      activeOpacity={0.8}
    >
      {buttonContent}
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    height: 56,
    borderRadius: 12,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: spacing.lg,
  },
  gradient: {
    flex: 1,
    width: '100%',
    borderRadius: 12,
    justifyContent: 'center',
    alignItems: 'center',
  },
  text: {
    ...typography.body,
    fontWeight: '600',
    color: colors.surface,
  },
  outline: {
    backgroundColor: 'transparent',
    borderWidth: 2,
    borderColor: colors.accent.main,
  },
  outlineText: {
    color: colors.accent.main,
  },
  secondary: {
    backgroundColor: colors.surface,
    borderWidth: 1,
    borderColor: colors.border,
  },
  disabled: {
    opacity: 0.5,
  },
});

export default Button;
```

### 5.2 Input Component

**Dosya**: `src/components/common/Input.tsx`

```typescript
import React, { useState } from 'react';
import {
  View,
  TextInput,
  Text,
  StyleSheet,
  TouchableOpacity,
  Animated,
} from 'react-native';
import { colors } from '../../theme/colors';
import { typography } from '../../theme/typography';
import { spacing } from '../../theme/spacing';

interface InputProps {
  label?: string;
  value: string;
  onChangeText: (text: string) => void;
  placeholder?: string;
  secureTextEntry?: boolean;
  keyboardType?: 'default' | 'email-address' | 'numeric' | 'phone-pad';
  autoCapitalize?: 'none' | 'sentences' | 'words' | 'characters';
  leftIcon?: string;
  rightIcon?: string;
  onRightIconPress?: () => void;
  error?: string;
  style?: any;
}

const Input: React.FC<InputProps> = ({
  label,
  value,
  onChangeText,
  placeholder,
  secureTextEntry = false,
  keyboardType = 'default',
  autoCapitalize = 'sentences',
  leftIcon,
  rightIcon,
  onRightIconPress,
  error,
  style,
}) => {
  const [isFocused, setIsFocused] = useState(false);
  const labelAnimation = new Animated.Value(value ? 1 : 0);

  const handleFocus = () => {
    setIsFocused(true);
    Animated.timing(labelAnimation, {
      toValue: 1,
      duration: 200,
      useNativeDriver: false,
    }).start();
  };

  const handleBlur = () => {
    setIsFocused(false);
    if (!value) {
      Animated.timing(labelAnimation, {
        toValue: 0,
        duration: 200,
        useNativeDriver: false,
      }).start();
    }
  };

  return (
    <View style={[styles.container, style]}>
      {label && (
        <Animated.Text
          style={[
            styles.label,
            {
              transform: [
                {
                  translateY: labelAnimation.interpolate({
                    inputRange: [0, 1],
                    outputRange: [20, 0],
                  }),
                },
              ],
              fontSize: labelAnimation.interpolate({
                inputRange: [0, 1],
                outputRange: [16, 12],
              }),
            },
          ]}
        >
          {label}
        </Animated.Text>
      )}
      <View
        style={[
          styles.inputContainer,
          isFocused && styles.inputContainerFocused,
          error && styles.inputContainerError,
        ]}
      >
        {leftIcon && (
          <View style={styles.iconContainer}>
            {/* Icon component */}
          </View>
        )}
        <TextInput
          style={styles.input}
          value={value}
          onChangeText={onChangeText}
          placeholder={!label ? placeholder : undefined}
          placeholderTextColor={colors.text.tertiary}
          secureTextEntry={secureTextEntry}
          keyboardType={keyboardType}
          autoCapitalize={autoCapitalize}
          onFocus={handleFocus}
          onBlur={handleBlur}
        />
        {rightIcon && (
          <TouchableOpacity
            style={styles.iconContainer}
            onPress={onRightIconPress}
          >
            {/* Icon component */}
          </TouchableOpacity>
        )}
      </View>
      {error && <Text style={styles.errorText}>{error}</Text>}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.md,
  },
  label: {
    ...typography.bodySmall,
    color: colors.text.secondary,
    marginBottom: spacing.xs,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: colors.surface,
    borderRadius: 12,
    borderWidth: 1,
    borderColor: colors.border,
    paddingHorizontal: spacing.md,
    height: 56,
  },
  inputContainerFocused: {
    borderColor: colors.accent.main,
    borderWidth: 2,
    shadowColor: colors.accent.main,
    shadowOffset: { width: 0, height: 0 },
    shadowOpacity: 0.2,
    shadowRadius: 8,
    elevation: 4,
  },
  inputContainerError: {
    borderColor: colors.error,
  },
  input: {
    flex: 1,
    ...typography.body,
    color: colors.text.primary,
  },
  iconContainer: {
    padding: spacing.xs,
  },
  errorText: {
    ...typography.caption,
    color: colors.error,
    marginTop: spacing.xs,
  },
});

export default Input;
```

---

## 🚀 6. NAVIGATION SETUP

### 6.1 App Navigator

**Dosya**: `src/navigation/AppNavigator.tsx`

```typescript
import React, { useEffect } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { useSelector, useDispatch } from 'react-redux';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { restoreSession } from '../store/slices/authSlice';

// Auth Screens
import LoginScreen from '../screens/Auth/LoginScreen';
import RegisterScreen from '../screens/Auth/RegisterScreen';
import ForgotPasswordScreen from '../screens/Auth/ForgotPasswordScreen';

// Main Screens
import MainNavigator from './MainNavigator';

const Stack = createStackNavigator();

const AppNavigator = () => {
  const { isAuthenticated } = useSelector((state: RootState) => state.auth);
  const dispatch = useDispatch();

  useEffect(() => {
    // Restore session on app start
    const restoreUserSession = async () => {
      try {
        const token = await AsyncStorage.getItem('token');
        const clientId = await AsyncStorage.getItem('clientId');
        const userStr = await AsyncStorage.getItem('user');

        if (token && clientId && userStr) {
          const user = JSON.parse(userStr);
          dispatch(restoreSession({ user, token, clientId: parseInt(clientId) }));
        }
      } catch (error) {
        console.error('Failed to restore session:', error);
      }
    };

    restoreUserSession();
  }, []);

  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        {isAuthenticated ? (
          <Stack.Screen name="Main" component={MainNavigator} />
        ) : (
          <>
            <Stack.Screen name="Login" component={LoginScreen} />
            <Stack.Screen name="Register" component={RegisterScreen} />
            <Stack.Screen name="ForgotPassword" component={ForgotPasswordScreen} />
          </>
        )}
      </Stack.Navigator>
    </NavigationContainer>
  );
};

export default AppNavigator;
```

### 6.2 Main Navigator (Bottom Tabs)

**Dosya**: `src/navigation/MainNavigator.tsx`

```typescript
import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { createStackNavigator } from '@react-navigation/stack';
import { useSelector } from 'react-redux';

// Screens
import AdminDashboardScreen from '../screens/Dashboard/AdminDashboardScreen';
import WorkOrderListScreen from '../screens/WorkOrders/WorkOrderListScreen';
import CustomerListScreen from '../screens/Customers/CustomerListScreen';
import PartsListScreen from '../screens/Parts/PartsListScreen';
import ProfileScreen from '../screens/Profile/ProfileScreen';

// Detail Screens
import WorkOrderDetailScreen from '../screens/WorkOrders/WorkOrderDetailScreen';
import CustomerDetailScreen from '../screens/Customers/CustomerDetailScreen';

const Tab = createBottomTabNavigator();
const Stack = createStackNavigator();

const WorkOrderStack = () => (
  <Stack.Navigator>
    <Stack.Screen
      name="WorkOrderList"
      component={WorkOrderListScreen}
      options={{ headerShown: false }}
    />
    <Stack.Screen
      name="WorkOrderDetail"
      component={WorkOrderDetailScreen}
      options={{ title: 'İş Emri Detayı' }}
    />
  </Stack.Navigator>
);

const MainNavigator = () => {
  const { user } = useSelector((state: RootState) => state.auth);

  return (
    <Tab.Navigator
      screenOptions={{
        tabBarActiveTintColor: colors.accent.main,
        tabBarInactiveTintColor: colors.text.secondary,
        headerShown: false,
        tabBarStyle: {
          backgroundColor: colors.surface,
          borderTopColor: colors.border,
          paddingBottom: 8,
          paddingTop: 8,
          height: 60,
        },
      }}
    >
      <Tab.Screen
        name="Dashboard"
        component={AdminDashboardScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="home" size={size} color={color} />
          ),
        }}
      />
      <Tab.Screen
        name="WorkOrders"
        component={WorkOrderStack}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="clipboard" size={size} color={color} />
          ),
          tabBarBadge: 5, // Active work orders count
        }}
      />
      <Tab.Screen
        name="Customers"
        component={CustomerListScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="users" size={size} color={color} />
          ),
        }}
      />
      <Tab.Screen
        name="Parts"
        component={PartsListScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="box" size={size} color={color} />
          ),
        }}
      />
      <Tab.Screen
        name="Profile"
        component={ProfileScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="user" size={size} color={color} />
          ),
        }}
      />
    </Tab.Navigator>
  );
};

export default MainNavigator;
```

---

## 📋 7. IMPLEMENTATION CHECKLIST

### Phase 1: Setup & Authentication ✅
- [ ] Expo projesi oluştur
- [ ] Paketleri yükle (package.json)
- [ ] API client kurulumu
- [ ] Redux store kurulumu
- [ ] Navigation kurulumu
- [ ] Theme sistemi (colors, typography, spacing)
- [ ] Login Screen
- [ ] Register Screen
- [ ] Forgot Password Screen
- [ ] Reset Password Screen
- [ ] Auth Redux slice
- [ ] Token storage & restore

### Phase 2: Dashboard ✅
- [ ] Dashboard API service
- [ ] Admin Dashboard Screen
- [ ] Employee Dashboard Screen
- [ ] Customer Dashboard Screen
- [ ] Stat Cards component
- [ ] Charts (Line, Pie, Bar)
- [ ] Activity Timeline component
- [ ] Pull-to-refresh

### Phase 3: Work Orders ✅
- [ ] Work Order API service
- [ ] Work Order List Screen
- [ ] Work Order Detail Screen (tabs)
- [ ] Create Work Order Screen (multi-step)
- [ ] Update Status modal
- [ ] Work Order Card component
- [ ] Swipe actions
- [ ] Search & filters

### Phase 4: Customers ✅
- [ ] Customer API service
- [ ] Customer List Screen
- [ ] Customer Detail Screen (tabs)
- [ ] Create/Edit Customer Screen
- [ ] Customer Card component

### Phase 5: Vehicles ✅
- [ ] Vehicle API service
- [ ] Vehicle List Screen
- [ ] Vehicle Detail Screen
- [ ] Create/Edit Vehicle Screen

### Phase 6: Parts ✅
- [ ] Parts API service
- [ ] Parts List Screen
- [ ] Part Detail Screen
- [ ] Barcode Scanner Screen
- [ ] Barcode scanner integration

### Phase 7: Appointments ✅
- [ ] Appointment API service
- [ ] Calendar View Screen
- [ ] Appointment List Screen
- [ ] Create Appointment Screen

### Phase 8: Invoices ✅
- [ ] Invoice API service
- [ ] Invoice List Screen
- [ ] Invoice Detail Screen
- [ ] PDF viewer

### Phase 9: AI Features ✅
- [ ] AI Chat Screen
- [ ] Photo Analysis Screen
- [ ] AI API service

### Phase 10: Profile & Settings ✅
- [ ] Profile Screen
- [ ] Settings Screen
- [ ] Dark Mode toggle
- [ ] Language selection

---

## 🎯 ÖNEMLİ TALİMATLAR

### Cursor'da Kullanım:

1. **Bu dosyayı Cursor'a aç**
2. **Claude Sonnet 4.5 modelini seç**
3. **Her modül için sırayla şunu söyle:**
   ```
   "Phase 1: Setup & Authentication modülünü oluştur. 
   Yukarıdaki prompt'a göre tüm dosyaları oluştur ve 
   backend API'leri ile entegre et."
   ```

4. **Her modülü tamamladıktan sonra test et**
5. **Sorunları düzelt ve devam et**

### Kod Standartları:

- **TypeScript**: Tüm dosyalar TypeScript olmalı
- **Functional Components**: Class component kullanma
- **Hooks**: useState, useEffect, useSelector, useDispatch
- **Error Handling**: Try-catch blokları, error states
- **Loading States**: Her API çağrısında loading göster
- **Empty States**: Boş durumlar için component'ler
- **Accessibility**: AccessibilityLabel, testID ekle
- **Performance**: useMemo, useCallback kullan
- **Code Organization**: Her modül kendi klasöründe

### Tasarım Kuralları:

- **Renk Paleti**: %55 Mavi, %20 Turuncu (kesinlikle uygula)
- **Glassmorphism**: Kartlarda şeffaf arka plan, blur efekti
- **Gradient**: Butonlar ve kartlarda gradient kullan
- **Animations**: Smooth, 60fps animasyonlar
- **Spacing**: Tutarlı spacing (4, 8, 16, 24, 32px)
- **Typography**: Tutarlı font sizes ve weights
- **Dark Mode**: Tam dark mode desteği

---

## 🚀 BAŞLANGIÇ KOMUTLARI

```bash
# 1. Expo projesi oluştur
npx create-expo-app magic-car-repair-mobile --template

# 2. Paketleri yükle
cd magic-car-repair-mobile
npm install

# 3. Gerekli paketleri ekle
npx expo install @react-navigation/native @react-navigation/stack @react-navigation/bottom-tabs
npx expo install react-native-gesture-handler react-native-reanimated react-native-screens
npx expo install @react-native-async-storage/async-storage
npx expo install react-native-paper react-native-vector-icons
npx expo install expo-linear-gradient
npx expo install axios
npm install @reduxjs/toolkit react-redux
npm install react-native-chart-kit react-native-svg
npm install react-hook-form @hookform/resolvers zod

# 4. iOS için pod install
cd ios && pod install && cd ..

# 5. Projeyi başlat
npx expo start
```

---

## ✅ SONUÇ

Bu prompt'u Cursor'da Claude Sonnet 4.5 ile kullanarak:

1. **Adım adım** her modülü oluştur
2. **Backend API'leri** ile tam entegrasyon yap
3. **Modern, kullanıcı dostu** tasarım uygula
4. **Tüm özellikleri** implement et (pull-to-refresh, dark mode, vb.)
5. **Test et** ve **düzelt**

**Başarılar! 🚀**
