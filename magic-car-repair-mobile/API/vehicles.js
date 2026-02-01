import { apiGet, apiPost } from './http';

/**
 * Get all vehicles with optional filters
 * @param {number} customerId - Filter by customer ID
 * @param {string} searchTerm - Search by license plate, brand, or model
 * @param {number} pageNumber - Page number for pagination
 * @param {number} pageSize - Page size for pagination
 */
export async function getAllVehicles(customerId = null, searchTerm = '', pageNumber = null, pageSize = null) {
  const params = new URLSearchParams();
  if (customerId) params.append('customerId', customerId);
  if (searchTerm) params.append('searchTerm', searchTerm);
  if (pageNumber) params.append('pageNumber', pageNumber);
  if (pageSize) params.append('pageSize', pageSize);
  
  const queryString = params.toString();
  const url = `/vehicles${queryString ? `?${queryString}` : ''}`;
  
  return await apiGet(url);
}

/**
 * Get vehicles by customer ID
 * @param {number} customerId - Customer ID
 */
export async function getVehiclesByCustomer(customerId) {
  return await apiGet(`/vehicles/customer/${customerId}`);
}

/**
 * Get vehicle by ID
 * @param {number} id - Vehicle ID
 */
export async function getVehicleById(id) {
  return await apiGet(`/vehicles/${id}`);
}

/**
 * Create a new vehicle
 * @param {object} vehicleData - Vehicle data
 */
export async function createVehicle(vehicleData) {
  return await apiPost('/vehicles', vehicleData);
}
