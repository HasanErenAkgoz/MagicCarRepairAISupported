
import apiClient from './client';

const getStats = async () => {
  try {
    const response = await apiClient.get('/Dashboard/stats');
    return response.data;
  } catch (error) {
    throw error;
  }
};

const getIncomeExpenseChart = async () => {
  try {
    const response = await apiClient.get('/Dashboard/income-expense-chart');
    return response.data;
  } catch (error) {
    throw error;
  }
};

const getWorkOrderStatusChart = async () => {
  try {
    const response = await apiClient.get('/Dashboard/workorder-status-chart');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export default {
  getStats,
  getIncomeExpenseChart,
  getWorkOrderStatusChart,
};
