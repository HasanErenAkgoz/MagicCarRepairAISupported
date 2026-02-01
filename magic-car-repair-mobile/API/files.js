import { API_BASE_URL } from './config';
import { getToken } from './storage';

/**
 * Upload a file to the server
 * @param {string} fileUri - Local file URI
 * @param {string} containerName - Container name (e.g., 'workorders', 'vehicles')
 * @param {string} fileName - Optional file name
 */
export async function uploadFile(fileUri, containerName = 'workorders', fileName = null) {
  const token = await getToken();
  const url = `${API_BASE_URL}/files/upload`;

  // Create FormData
  const formData = new FormData();
  
  // Get file name from URI if not provided
  if (!fileName) {
    const uriParts = fileUri.split('/');
    fileName = uriParts[uriParts.length - 1];
  }

  // Get file extension and determine MIME type
  const fileExtension = fileName.split('.').pop()?.toLowerCase() || 'jpg';
  const mimeTypes = {
    jpg: 'image/jpeg',
    jpeg: 'image/jpeg',
    png: 'image/png',
    gif: 'image/gif',
    webp: 'image/webp',
  };
  const fileType = mimeTypes[fileExtension] || 'image/jpeg';

  // Append file to FormData
  // React Native FormData format
  formData.append('File', {
    uri: fileUri,
    type: fileType,
    name: fileName,
  });

  formData.append('ContainerName', containerName);

  try {
    const headers = {
      'X-Client-Id': '1',
      ...(token ? { 'Authorization': `Bearer ${token}` } : {}),
    };

    const response = await fetch(url, {
      method: 'POST',
      headers,
      body: formData,
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || 'File upload failed');
    }

    const result = await response.json();
    
    // Handle different response formats
    // Backend returns: { Path: "file/path/here" }
    // Also handle wrapped responses
    if (result.Path) {
      return result.Path;
    }
    if (result.path) {
      return result.path;
    }
    if (result.Data && result.Data.Path) {
      return result.Data.Path;
    }
    // If result is a string directly
    if (typeof result === 'string') {
      return result;
    }
    
    throw new Error('File upload response format is invalid');
  } catch (error) {
    console.error('File upload error:', error);
    throw error;
  }
}
