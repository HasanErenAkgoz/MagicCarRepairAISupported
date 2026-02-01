
import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import { createDrawerNavigator } from '@react-navigation/drawer';

import LoginScreen from '../screens/LoginScreen';
import RegisterScreen from '../screens/RegisterScreen';
import ServiceScheduleScreen from '../screens/ServiceScheduleScreen';
import MessagesScreen from '../screens/MessagesScreen';
import WorkOrdersEmptyScreen from '../screens/WorkOrdersEmptyScreen';
import DataSyncScreen from '../screens/DataSyncScreen';
import AnalyticsScreen from '../screens/AnalyticsScreen';
import VehicleProfileScreen from '../screens/VehicleProfileScreen';
import NewWorkOrderScreen from '../screens/NewWorkOrderScreen';
import NewWorkOrderWizardScreen from '../screens/NewWorkOrderWizardScreen';
import StaffManagementScreen from '../screens/StaffManagementScreen';
import AdminDashboardScreen from '../screens/AdminDashboardScreen';
import WorkOrderListScreen from '../screens/WorkOrderListScreen';
import WorkOrderDetailScreen from '../screens/WorkOrderDetailScreen';
import WorkOrderManagementScreen from '../screens/WorkOrderManagementScreen';
import BillingScreen from '../screens/BillingScreen';
import CustomerDashboardScreen from '../screens/CustomerDashboardScreen';
import DashboardLoadingScreen from '../screens/DashboardLoadingScreen';
import InventoryScreen from '../screens/InventoryScreen';
import PublicShopProfileScreen from '../screens/PublicShopProfileScreen';
import LoginScreen2 from '../screens/LoginScreen2';
import CustomerListScreen from '../screens/CustomerListScreen';
import ConnectionIssueScreen from '../screens/ConnectionIssueScreen';
import GlobalSearchScreen from '../screens/GlobalSearchScreen';
import CustomerDirectoryScreen from '../screens/CustomerDirectoryScreen';
import AdminDashboardOverviewScreen from '../screens/AdminDashboardOverviewScreen';
import ServiceCalendarScreen from '../screens/ServiceCalendarScreen';
import MessagesSupportScreen from '../screens/MessagesSupportScreen';
import AIChatScreen from '../screens/AIChatScreen';
import BillingInvoicesScreen from '../screens/BillingInvoicesScreen';
import CustomerPortalScreen from '../screens/CustomerPortalScreen';
import UserProfileScreen from '../screens/UserProfileScreen';
import PersonalInformationScreen from '../screens/PersonalInformationScreen';
import RevenueAnalyticsScreen from '../screens/RevenueAnalyticsScreen';
import AddCustomerScreen from '../screens/AddCustomerScreen';
import AddVehicleScreen from '../screens/AddVehicleScreen';
import DrawerContent from '../components/DrawerContent';

const Stack = createStackNavigator();
const Drawer = createDrawerNavigator();

// Drawer Navigator for Admin screens
const AdminDrawerNavigator = () => {
  return (
    <Drawer.Navigator
      drawerContent={(props) => <DrawerContent {...props} />}
      screenOptions={{
        headerShown: false,
        drawerStyle: {
          backgroundColor: '#0f172a',
          width: 280,
        },
        drawerActiveTintColor: '#3B82F6',
        drawerInactiveTintColor: 'rgba(255, 255, 255, 0.6)',
        overlayColor: 'rgba(0, 0, 0, 0.5)',
      }}
    >
      <Drawer.Screen name="AdminDashboard" component={AdminDashboardScreen} />
      <Drawer.Screen name="WorkOrderManagement" component={WorkOrderManagementScreen} />
      <Drawer.Screen name="WorkOrderList" component={WorkOrderListScreen} />
      <Drawer.Screen name="CustomerList" component={CustomerListScreen} />
      <Drawer.Screen name="Inventory" component={InventoryScreen} />
      <Drawer.Screen name="BillingInvoices" component={BillingInvoicesScreen} />
      <Drawer.Screen name="ServiceCalendar" component={ServiceCalendarScreen} />
      <Drawer.Screen name="RevenueAnalytics" component={RevenueAnalyticsScreen} />
      <Drawer.Screen name="UserProfile" component={UserProfileScreen} />
    </Drawer.Navigator>
  );
};

const AppNavigator = () => {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Login" component={LoginScreen} />
      <Stack.Screen name="Register" component={RegisterScreen} />
      <Stack.Screen name="ServiceSchedule" component={ServiceScheduleScreen} />
      <Stack.Screen name="Messages" component={MessagesScreen} />
      <Stack.Screen name="WorkOrdersEmpty" component={WorkOrdersEmptyScreen} />
      <Stack.Screen name="DataSync" component={DataSyncScreen} />
      <Stack.Screen name="Analytics" component={AnalyticsScreen} />
      <Stack.Screen name="VehicleProfile" component={VehicleProfileScreen} />
      <Stack.Screen name="NewWorkOrder" component={NewWorkOrderScreen} />
      <Stack.Screen name="NewWorkOrderWizard" component={NewWorkOrderWizardScreen} />
      <Stack.Screen name="StaffManagement" component={StaffManagementScreen} />
      <Stack.Screen name="AdminDrawer" component={AdminDrawerNavigator} />
      <Stack.Screen name="AdminDashboard" component={AdminDashboardScreen} />
      <Stack.Screen name="WorkOrderList" component={WorkOrderListScreen} />
      <Stack.Screen name="WorkOrderDetail" component={WorkOrderDetailScreen} />
      <Stack.Screen name="WorkOrderManagement" component={WorkOrderManagementScreen} />
      <Stack.Screen name="Billing" component={BillingScreen} />
      <Stack.Screen name="CustomerDashboard" component={CustomerDashboardScreen} />
      <Stack.Screen name="DashboardLoading" component={DashboardLoadingScreen} />
      <Stack.Screen name="Inventory" component={InventoryScreen} />
      <Stack.Screen name="PublicShopProfile" component={PublicShopProfileScreen} />
      <Stack.Screen name="Login2" component={LoginScreen2} />
      <Stack.Screen name="CustomerList" component={CustomerListScreen} />
      <Stack.Screen name="ConnectionIssue" component={ConnectionIssueScreen} />
      <Stack.Screen name="GlobalSearch" component={GlobalSearchScreen} />
      <Stack.Screen name="CustomerDirectory" component={CustomerDirectoryScreen} />
      <Stack.Screen name="AdminDashboardOverview" component={AdminDashboardOverviewScreen} />
      <Stack.Screen name="ServiceCalendar" component={ServiceCalendarScreen} />
      <Stack.Screen name="MessagesSupport" component={MessagesSupportScreen} />
      <Stack.Screen name="AIChat" component={AIChatScreen} />
      <Stack.Screen name="BillingInvoices" component={BillingInvoicesScreen} />
      <Stack.Screen name="CustomerPortal" component={CustomerPortalScreen} />
      <Stack.Screen name="UserProfile" component={UserProfileScreen} />
      <Stack.Screen name="PersonalInformation" component={PersonalInformationScreen} />
      <Stack.Screen name="RevenueAnalytics" component={RevenueAnalyticsScreen} />
      <Stack.Screen 
        name="AddCustomer" 
        component={AddCustomerScreen}
        options={{
          presentation: 'modal',
          headerShown: false,
        }}
      />
      <Stack.Screen 
        name="AddVehicle" 
        component={AddVehicleScreen}
        options={{
          presentation: 'modal',
          headerShown: false,
        }}
      />
    </Stack.Navigator>
  );
};

export default AppNavigator;
