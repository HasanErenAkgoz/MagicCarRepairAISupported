import { apiPost, apiGet } from './http';

/**
 * AI Chat - Stitch benzeri AI asistan ile konuşma
 */
export async function sendChatMessage(message, conversationHistory = []) {
  try {
    const response = await apiPost('/AI/chat', {
      message,
      conversationHistory,
    });
    return response;
  } catch (error) {
    console.error('[AI API] Chat error:', error);
    throw error;
  }
}

/**
 * AI destekli randevu optimizasyonu
 */
export async function optimizeAppointments(date, duration = 60) {
  try {
    const response = await apiGet(
      `/AI/optimize-appointments?date=${date}&duration=${duration}`
    );
    return response;
  } catch (error) {
    console.error('[AI API] Optimize appointments error:', error);
    throw error;
  }
}

/**
 * AI destekli stok tahmini
 */
export async function forecastStock(partId, days = 30) {
  try {
    const response = await apiGet(
      `/AI/forecast-stock?partId=${partId}&days=${days}`
    );
    return response;
  } catch (error) {
    console.error('[AI API] Forecast stock error:', error);
    throw error;
  }
}

/**
 * AI destekli müşteri analizi
 */
export async function analyzeCustomers(customerId = null) {
  try {
    const url = customerId
      ? `/AI/analyze-customers?customerId=${customerId}`
      : '/AI/analyze-customers';
    const response = await apiGet(url);
    return response;
  } catch (error) {
    console.error('[AI API] Analyze customers error:', error);
    throw error;
  }
}

/**
 * AI destekli parça önerisi
 */
export async function suggestParts(workOrderId = null, vehicleId = null) {
  try {
    const params = new URLSearchParams();
    if (workOrderId) params.append('workOrderId', workOrderId);
    if (vehicleId) params.append('vehicleId', vehicleId);
    
    const url = `/AI/suggest-parts${params.toString() ? '?' + params.toString() : ''}`;
    const response = await apiGet(url);
    return response;
  } catch (error) {
    console.error('[AI API] Suggest parts error:', error);
    throw error;
  }
}
