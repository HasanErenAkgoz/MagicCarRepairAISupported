/**
 * Configuration Example
 * Copy this file to config.js and update with your values
 * Or use environment variables
 */

export const config = {
  api: {
    baseUrl: process.env.API_BASE_URL || 'http://localhost:5000',
    token: process.env.API_TOKEN || '',
  },
  logging: {
    level: process.env.LOG_LEVEL || 'info',
  },
};
