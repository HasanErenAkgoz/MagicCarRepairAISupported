import { apiGet } from './user';

/**
 * Dashboard genel istatistiklerini getirir
 * @param {Object} options - Query parametreleri
 * @param {Date} options.startDate - Başlangıç tarihi
 * @param {Date} options.endDate - Bitiş tarihi
 * @returns {Promise<Object>} Dashboard stats response
 */
export async function getDashboardStats(options = {}) {
  const params = new URLSearchParams();
  
  if (options.startDate) {
    params.append('startDate', options.startDate.toISOString());
  }
  if (options.endDate) {
    params.append('endDate', options.endDate.toISOString());
  }

  const queryString = params.toString();
  const path = `/dashboard/stats${queryString ? `?${queryString}` : ''}`;
  
  const res = await apiGet(path);
  
  // Response formatı: GetDashboardStatsResponse (PascalCase from backend)
  const data = res?.data || res || {};
  return {
    totalWorkOrders: Number(data?.totalWorkOrders || data?.TotalWorkOrders || 0),
    activeWorkOrders: Number(data?.activeWorkOrders || data?.ActiveWorkOrders || 0),
    completedWorkOrders: Number(data?.completedWorkOrders || data?.CompletedWorkOrders || 0),
    pendingWorkOrders: Number(data?.pendingWorkOrders || data?.PendingWorkOrders || 0),
    totalIncome: Number(data?.totalIncome || data?.TotalIncome || 0),
    totalExpense: Number(data?.totalExpense || data?.TotalExpense || 0),
    netProfit: Number(data?.netProfit || data?.NetProfit || 0),
    pendingInvoiceAmount: Number(data?.pendingInvoiceAmount || data?.PendingInvoiceAmount || 0),
    overdueInvoiceAmount: Number(data?.overdueInvoiceAmount || data?.OverdueInvoiceAmount || 0),
    totalParts: Number(data?.totalParts || data?.TotalParts || 0),
    lowStockParts: Number(data?.lowStockParts || data?.LowStockParts || 0),
    outOfStockParts: Number(data?.outOfStockParts || data?.OutOfStockParts || 0),
    activeStockAlerts: Number(data?.activeStockAlerts || data?.ActiveStockAlerts || 0),
    totalCustomers: Number(data?.totalCustomers || data?.TotalCustomers || 0),
    totalVehicles: Number(data?.totalVehicles || data?.TotalVehicles || 0),
    newCustomersThisMonth: Number(data?.newCustomersThisMonth || data?.NewCustomersThisMonth || 0),
    openQuoteRequests: Number(data?.openQuoteRequests || data?.OpenQuoteRequests || 0),
    pendingQuoteResponses: Number(data?.pendingQuoteResponses || data?.PendingQuoteResponses || 0),
    unreadNotifications: Number(data?.unreadNotifications || data?.UnreadNotifications || 0),
    startDate: data?.startDate || data?.StartDate,
    endDate: data?.endDate || data?.EndDate,
  };
}

/**
 * Bugünün gelir bilgilerini getirir
 * @returns {Promise<Object>} Today revenue response
 */
export async function getTodayRevenue() {
  const res = await apiGet('/dashboard/today-revenue');
  
  // Response formatı: GetTodayRevenueResponse (PascalCase from backend)
  const data = res?.data || res || {};
  return {
    todayRevenue: Number(data?.todayRevenue || data?.TodayRevenue || 0),
    yesterdayRevenue: Number(data?.yesterdayRevenue || data?.YesterdayRevenue || 0),
    changePercent: Number(data?.changePercent || data?.ChangePercent || 0),
  };
}

/**
 * Haftalık revenue chart verilerini getirir (son 7 gün)
 * @returns {Promise<Array>} Weekly revenue chart data
 */
export async function getWeeklyRevenueChart() {
  const endDate = new Date();
  const startDate = new Date();
  startDate.setDate(startDate.getDate() - 6); // Son 7 gün (bugün dahil)

  const params = new URLSearchParams();
  params.append('startDate', startDate.toISOString());
  params.append('endDate', endDate.toISOString());
  params.append('groupBy', '1'); // Day = 1

  const res = await apiGet(`/dashboard/income-expense-chart?${params.toString()}`);
  
  // Response formatı: List<GetIncomeExpenseChartResponse> (PascalCase from backend)
  const data = res?.data || res || [];
  
  // Son 7 günün revenue değerlerini döndür (Income değerleri)
  return data.map(item => ({
    period: item?.period || item?.Period || '',
    periodStart: item?.periodStart ? new Date(item.periodStart) : (item?.PeriodStart ? new Date(item.PeriodStart) : null),
    periodEnd: item?.periodEnd ? new Date(item.periodEnd) : (item?.PeriodEnd ? new Date(item.PeriodEnd) : null),
    income: Number(item?.income || item?.Income || 0),
    expense: Number(item?.expense || item?.Expense || 0),
    profit: Number(item?.profit || item?.Profit || 0),
  }));
}

/**
 * Önceki haftanın revenue verilerini getirir (karşılaştırma için)
 * @returns {Promise<number>} Previous week total revenue
 */
export async function getPreviousWeekRevenue() {
  const endDate = new Date();
  endDate.setDate(endDate.getDate() - 7); // 7 gün önce
  const startDate = new Date(endDate);
  startDate.setDate(startDate.getDate() - 6); // Önceki haftanın başlangıcı

  const params = new URLSearchParams();
  params.append('startDate', startDate.toISOString());
  params.append('endDate', endDate.toISOString());
  params.append('groupBy', '1'); // Day = 1

  const res = await apiGet(`/dashboard/income-expense-chart?${params.toString()}`);
  
  const data = res?.data || res || [];
  const total = data.reduce((sum, item) => sum + Number(item?.income || item?.Income || 0), 0);
  
  return total;
}

/**
 * Fleet status bilgilerini getirir
 * @returns {Promise<Object>} Fleet status response
 */
export async function getFleetStatus() {
  const res = await apiGet('/dashboard/fleet-status');
  
  // Response formatı: GetFleetStatusResponse (PascalCase from backend)
  const data = res?.data || res || {};
  return {
    repairing: Number(data?.repairing || data?.Repairing || 0),
    completed: Number(data?.completed || data?.Completed || 0),
    waiting: Number(data?.waiting || data?.Waiting || 0),
    efficiency: Number(data?.efficiency || data?.Efficiency || 0),
  };
}

/**
 * Income-Expense chart verilerini getirir
 * @param {Object} options - Query parametreleri
 * @param {Date} options.startDate - Başlangıç tarihi
 * @param {Date} options.endDate - Bitiş tarihi
 * @param {number} options.groupBy - Gruplama tipi (1: Day, 2: Month, 3: Year)
 * @returns {Promise<Array>} Income-Expense chart data
 */
export async function getIncomeExpenseChart(options = {}) {
  const params = new URLSearchParams();
  
  if (options.startDate) {
    params.append('startDate', options.startDate.toISOString());
  }
  if (options.endDate) {
    params.append('endDate', options.endDate.toISOString());
  }
  if (options.groupBy) {
    params.append('groupBy', String(options.groupBy));
  }

  const queryString = params.toString();
  const path = `/dashboard/income-expense-chart${queryString ? `?${queryString}` : ''}`;
  
  const res = await apiGet(path);
  
  // Response formatı: List<GetIncomeExpenseChartResponse> (PascalCase from backend)
  const data = res?.data || res || [];
  
  return data.map(item => ({
    period: item?.period || item?.Period || '',
    periodStart: item?.periodStart ? new Date(item.periodStart) : (item?.PeriodStart ? new Date(item.PeriodStart) : null),
    periodEnd: item?.periodEnd ? new Date(item.periodEnd) : (item?.PeriodEnd ? new Date(item.PeriodEnd) : null),
    income: Number(item?.income || item?.Income || 0),
    expense: Number(item?.expense || item?.Expense || 0),
    profit: Number(item?.profit || item?.Profit || 0),
  }));
}

/**
 * Work order status chart verilerini getirir
 * @returns {Promise<Array>} Work order status chart data
 */
export async function getWorkOrderStatusChart() {
  const res = await apiGet('/dashboard/workorder-status-chart');
  
  // Response formatı: List<GetWorkOrderStatusChartResponse>
  const data = res?.data || res || [];
  
  return data.map(item => ({
    statusName: item?.statusName || '',
    count: item?.count || 0,
    percentage: item?.percentage || 0,
  }));
}
