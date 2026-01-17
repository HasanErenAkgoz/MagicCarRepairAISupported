
import apiClient from './client';

const getWorkOrders = async () => {
  try {
    const response = await apiClient.get('/WorkOrders');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export default {
  getWorkOrders,
};
