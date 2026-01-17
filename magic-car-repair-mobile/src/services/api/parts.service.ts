
import apiClient from './client';

const getParts = async () => {
  try {
    const response = await apiClient.get('/Parts');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export default {
  getParts,
};
