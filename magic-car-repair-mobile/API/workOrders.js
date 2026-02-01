import { apiPost, apiGet, apiPut } from './http';

/**
 * Get all work orders with optional filters
 * @param {object} filters - Optional filters (status, customerId, vehicleId, employeeId, startDate, endDate)
 */
export async function getAllWorkOrders(filters = {}) {
  const queryParams = new URLSearchParams();
  if (filters.status) queryParams.append('status', filters.status);
  if (filters.customerId) queryParams.append('customerId', filters.customerId);
  if (filters.vehicleId) queryParams.append('vehicleId', filters.vehicleId);
  if (filters.employeeId) queryParams.append('employeeId', filters.employeeId);
  if (filters.startDate) queryParams.append('startDate', filters.startDate);
  if (filters.endDate) queryParams.append('endDate', filters.endDate);
  
  const queryString = queryParams.toString();
  const url = `/workorders${queryString ? `?${queryString}` : ''}`;
  return await apiGet(url);
}

/**
 * Get active work orders
 */
export async function getActiveWorkOrders() {
  return await apiGet('/workorders/active');
}

/**
 * Get work order by ID
 * @param {number} id - Work order ID
 */
export async function getWorkOrderById(id) {
  return await apiGet(`/workorders/${id}`);
}

/**
 * Get work order timeline
 * @param {number} id - Work order ID
 */
export async function getWorkOrderTimeline(id) {
  return await apiGet(`/workorders/${id}/timeline`);
}

/**
 * Create a new work order
 * @param {object} workOrderData - Work order data
 */
export async function createWorkOrder(workOrderData) {
  return await apiPost('/workorders', workOrderData);
}

/**
 * Update work order
 * @param {number} id - Work order ID
 * @param {object} workOrderData - Work order data
 */
export async function updateWorkOrder(id, workOrderData) {
  return await apiPut(`/workorders/${id}`, workOrderData);
}

/**
 * Update work order status
 * @param {number} id - Work order ID
 * @param {string} status - New status
 * @param {string} notes - Optional notes
 */
export async function updateWorkOrderStatus(id, status, notes = '') {
  return await apiPut(`/workorders/${id}/status`, { status, notes });
}

/**
 * Complete work order
 * @param {number} id - Work order ID
 * @param {string} completionNotes - Completion notes
 * @param {string} actualCompletionDate - Actual completion date (ISO string)
 */
export async function completeWorkOrder(id, completionNotes = '', actualCompletionDate = null) {
  return await apiPost(`/workorders/${id}/complete`, {
    completionNotes,
    actualCompletionDate: actualCompletionDate || new Date().toISOString(),
  });
}

/**
 * Deliver work order
 * @param {number} id - Work order ID
 * @param {string} deliveryNotes - Delivery notes
 * @param {string} deliveryDate - Delivery date (ISO string)
 */
export async function deliverWorkOrder(id, deliveryNotes = '', deliveryDate = null) {
  return await apiPost(`/workorders/${id}/deliver`, {
    deliveryNotes,
    deliveryDate: deliveryDate || new Date().toISOString(),
  });
}

/**
 * Add photo to work order
 * @param {number} workOrderId - Work order ID
 * @param {object} photoData - Photo data (filePath, uploadedFileId, description, photoType, etc.)
 */
export async function addWorkOrderPhoto(workOrderId, photoData) {
  return await apiPost(`/workorders/${workOrderId}/photos`, photoData);
}
