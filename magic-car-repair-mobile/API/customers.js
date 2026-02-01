import { apiGet, apiPost } from './http';

/**
 * Get all customers with optional search
 * @param {string} searchTerm - Search by name, email, phone, or identity number
 * @param {number} pageNumber - Page number for pagination
 * @param {number} pageSize - Page size for pagination
 */
export async function getAllCustomers(searchTerm = '', pageNumber = null, pageSize = null) {
  const params = new URLSearchParams();
  if (searchTerm) params.append('searchTerm', searchTerm);
  if (pageNumber) params.append('pageNumber', pageNumber);
  if (pageSize) params.append('pageSize', pageSize);
  
  const queryString = params.toString();
  const url = `/customers${queryString ? `?${queryString}` : ''}`;
  
  return await apiGet(url);
}

/**
 * Get customer by ID
 * @param {number} id - Customer ID
 */
export async function getCustomerById(id) {
  return await apiGet(`/customers/${id}`);
}

/**
 * Create a new customer
 * @param {object} customerData - Customer data
 */
export async function createCustomer(customerData) {
  return await apiPost('/customers', customerData);
}
