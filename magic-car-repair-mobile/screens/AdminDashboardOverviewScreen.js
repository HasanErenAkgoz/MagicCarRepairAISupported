
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';

const AdminDashboardOverviewScreen = () => {
  return (
    <LinearGradient colors={['#0f172a', '#1e3a8a']} style={styles.container}>
      <SafeAreaView style={styles.safeArea}>
        <View style={styles.header}>
          <View style={styles.userInfo}>
            <Image
              source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBo-cSRo0W4CS10xYmippWvnMbbpw4g27rLHQDaCysObf6r9pm7YgXTVy-FtvoafsX9bdWioIT5HieaPiuCbu-CKvqE30KYAcRAE8OrrAI5YFr0mkS-1q3NouKAfNNnzFVyxfyvFHRDarSBFL1ikkHwlzUvmnUV4w3Q2VN9VNNcpBc_d6DmJJQSJR2IpHYL0XXtut-ZvbDVI5SYzpCFPTZF_8p2y_PLXgRrOa0eJWD-GYN6s2scWsYwCJMmPE2slgQNkmdW_Ppqq8Cp' }}
              style={styles.avatar}
            />
            <View>
              <Text style={styles.welcomeText}>Welcome back</Text>
              <Text style={styles.userName}>Good Morning, Alex</Text>
            </View>
          </View>
          <TouchableOpacity style={styles.iconButton}>
            <Feather name="bell" size={24} color="white" />
          </TouchableOpacity>
        </View>

        <ScrollView showsVerticalScrollIndicator={false}>
          <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.statsScroller}>
            <StatCard icon="file-text" label="Total" value="142" subtitle="Work Orders" />
            <StatCard icon="activity" label="Active" value="18" subtitle="In Progress" isPrimary />
            <StatCard icon="dollar-sign" label="Revenue" value="$2.4k" subtitle="Today's Revenue" />
            <StatCard icon="clock" label="Pending" value="4" subtitle="Approval" />
          </ScrollView>

          <View style={styles.chartContainer}>
            <Text style={styles.sectionTitle}>Revenue Analytics</Text>
            {/* Chart placeholder */}
          </View>

          <View style={styles.chartContainer}>
            <Text style={styles.sectionTitle}>Fleet Status</Text>
            {/* Chart placeholder */}
          </View>
        </ScrollView>

        <TouchableOpacity style={styles.fab}>
          <Feather name="plus" size={24} color="white" />
          <Text style={styles.fabText}>New Order</Text>
        </TouchableOpacity>
      </SafeAreaView>
    </LinearGradient>
  );
};

const StatCard = ({ icon, label, value, subtitle, isPrimary }) => (
    <View style={[styles.statCard, isPrimary && styles.primaryCard]}>
        <Feather name={icon} size={24} color={isPrimary ? 'white' : '#3B82F6'} />
        <Text style={[styles.statValue, isPrimary && {color: 'white'}]}>{value}</Text>
        <Text style={[styles.statLabel, isPrimary && {color: 'rgba(255,255,255,0.7)'}]}>{subtitle}</Text>
    </View>
);

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  safeArea: {
    flex: 1,
    paddingTop: 24,
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  userInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatar: {
    width: 48,
    height: 48,
    borderRadius: 24,
    marginRight: 12,
  },
  welcomeText: {
    color: 'rgba(255,255,255,0.6)',
    fontSize: 12,
  },
  userName: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
  iconButton: {
    padding: 8,
  },
  statsScroller: {
    paddingLeft: 16,
  },
  statCard: {
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
    borderRadius: 16,
    padding: 16,
    width: 160,
    marginRight: 16,
  },
  primaryCard: {
    backgroundColor: '#3B82F6',
  },
  statValue: {
    color: 'white',
    fontSize: 28,
    fontWeight: 'bold',
    marginTop: 8,
  },
  statLabel: {
    color: 'rgba(255,255,255,0.5)',
    fontSize: 12,
  },
  chartContainer: {
    backgroundColor: 'rgba(255, 255, 255, 0.05)',
    borderRadius: 16,
    padding: 16,
    margin: 16,
  },
  sectionTitle: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#2563EB',
    borderRadius: 28,
    paddingVertical: 12,
    paddingHorizontal: 16,
  },
  fabText: {
    color: 'white',
    fontWeight: 'bold',
    marginLeft: 8,
  },
});

export default AdminDashboardOverviewScreen;
