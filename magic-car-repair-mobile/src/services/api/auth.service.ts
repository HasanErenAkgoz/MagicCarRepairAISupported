
import apiClient from './client';
import AsyncStorage from '@react-native-async-storage/async-storage';

const login = async (email, password) => {
  try {
    const response = await apiClient.post('/Auth/login', { email, password });
    const { token } = response.data;
    await AsyncStorage.setItem('token', token);
    return response.data;
  } catch (error) {
    throw error;
  }
};

export default {
  login,
};
