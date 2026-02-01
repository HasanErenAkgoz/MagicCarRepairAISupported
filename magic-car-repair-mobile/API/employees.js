import { apiGet } from './http';

/**
 * Get all employees
 * @param {object} filters - Optional filters (employmentStatus, position, pageNumber, pageSize)
 */
export async function getAllEmployees(filters = {}) {
  const params = new URLSearchParams();
  if (filters.employmentStatus) params.append('employmentStatus', filters.employmentStatus);
  if (filters.position) params.append('position', filters.position);
  if (filters.pageNumber) params.append('pageNumber', filters.pageNumber);
  if (filters.pageSize) params.append('pageSize', filters.pageSize);
  
  const queryString = params.toString();
  const url = `/employees${queryString ? `?${queryString}` : ''}`;
  
  return await apiGet(url);
}
