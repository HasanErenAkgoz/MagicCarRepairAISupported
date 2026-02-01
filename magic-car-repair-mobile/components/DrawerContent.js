import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image, ScrollView } from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Feather } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import { getMyProfile } from '../API/user';

const DrawerContent = ({ navigation }) => {
  const [userProfile, setUserProfile] = useState(null);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const profile = await getMyProfile();
        setUserProfile(profile);
      } catch (error) {
        console.warn('Failed to fetch user profile in drawer:', error);
      }
    };
    fetchProfile();
  }, []);
  const menuItems = [
    { icon: 'home', label: 'Dashboard', screen: 'AdminDashboard' },
    { icon: 'file-text', label: 'İş Emirleri', screen: 'WorkOrderManagement' },
    { icon: 'users', label: 'Müşteriler', screen: 'CustomerList' },
    { icon: 'package', label: 'Stok Takibi', screen: 'Inventory' },
    { icon: 'dollar-sign', label: 'Faturalar', screen: 'BillingInvoices' },
    { icon: 'calendar', label: 'Takvim', screen: 'ServiceCalendar' },
    { icon: 'bar-chart-2', label: 'Analitik', screen: 'RevenueAnalytics' },
    { icon: 'settings', label: 'Ayarlar', screen: 'UserProfile' },
  ];

  const userName = userProfile?.fullName || userProfile?.firstName || 'User';
  const userAvatar = userProfile?.avatar || 'https://lh3.googleusercontent.com/aida-public/AB6AXuBo-cSRo0W4CS10xYmippWvnMbbpw4g27rLHQDaCysObf6r9pm7YgXTVy-FtvoafsX9bdWioIT5HieaPiuCbu-CKvqE30KYAcRAE8OrrAI5YFr0mkS-1q3NouKAfNNnzFVyxfyvFHRDarSBFL1ikkHwlzUvmnUV4w3Q2VN9VNNcpBc_d6DmJJQSJR2IpHYL0XXtut-ZvbDVI5SYzpCFPTZF_8p2y_PLXgRrOa0eJWD-GYN6s2scWsYwCJMmPE2slgQNkmdW_Ppqq8Cp';

  return (
    <SafeAreaView style={styles.container} edges={['top', 'bottom']}>
      <ScrollView style={styles.scrollView} showsVerticalScrollIndicator={false}>
        {/* User Profile Section */}
        <View style={styles.profileSection}>
          <Image source={{ uri: userAvatar }} style={styles.avatar} />
          <Text style={styles.userName}>{userName}</Text>
          <Text style={styles.userRole}>Admin</Text>
        </View>

        {/* Menu Items */}
        <View style={styles.menuSection}>
          {menuItems.map((item, index) => (
            <TouchableOpacity
              key={index}
              style={styles.menuItem}
              onPress={() => {
                navigation.navigate(item.screen);
                navigation.closeDrawer();
              }}
              activeOpacity={0.7}
            >
              <View style={styles.menuItemLeft}>
                <View style={styles.menuIconContainer}>
                  <Feather name={item.icon} size={20} color="rgba(255, 255, 255, 0.8)" />
                </View>
                <Text style={styles.menuItemLabel}>{item.label}</Text>
              </View>
              <Feather name="chevron-right" size={18} color="rgba(255, 255, 255, 0.3)" />
            </TouchableOpacity>
          ))}
        </View>

        {/* Footer */}
        <View style={styles.footer}>
          <TouchableOpacity
            style={styles.logoutButton}
            onPress={() => {
              // Handle logout
              navigation.navigate('Login');
            }}
            activeOpacity={0.7}
          >
            <Feather name="log-out" size={18} color="#EF4444" />
            <Text style={styles.logoutText}>Çıkış Yap</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  scrollView: {
    flex: 1,
  },
  profileSection: {
    paddingHorizontal: 24,
    paddingVertical: 32,
    alignItems: 'center',
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(255, 255, 255, 0.08)',
  },
  avatar: {
    width: 80,
    height: 80,
    borderRadius: 40,
    borderWidth: 3,
    borderColor: 'rgba(59, 130, 246, 0.3)',
    marginBottom: 16,
  },
  userName: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 4,
  },
  userRole: {
    color: 'rgba(255, 255, 255, 0.5)',
    fontSize: 14,
    fontWeight: '500',
  },
  menuSection: {
    paddingVertical: 16,
  },
  menuItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 24,
    paddingVertical: 16,
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(255, 255, 255, 0.05)',
  },
  menuItemLeft: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
  },
  menuIconContainer: {
    width: 40,
    height: 40,
    borderRadius: 12,
    backgroundColor: 'rgba(255, 255, 255, 0.05)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  menuItemLabel: {
    color: 'rgba(255, 255, 255, 0.8)',
    fontSize: 16,
    fontWeight: '500',
  },
  footer: {
    paddingHorizontal: 24,
    paddingVertical: 24,
    borderTopWidth: 1,
    borderTopColor: 'rgba(255, 255, 255, 0.08)',
    marginTop: 'auto',
  },
  logoutButton: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
    paddingVertical: 12,
  },
  logoutText: {
    color: '#EF4444',
    fontSize: 16,
    fontWeight: '600',
  },
});

export default DrawerContent;
