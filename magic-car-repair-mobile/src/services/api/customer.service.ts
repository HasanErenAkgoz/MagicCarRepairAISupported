
import apiClient from './client';

const getCustomers = async () => {
  try {
    const response = await apiClient.get('/Customers');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export default {
  getCustomers,
};
