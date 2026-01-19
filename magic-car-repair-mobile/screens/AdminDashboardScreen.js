
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const AdminDashboardScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <View style={styles.userInfo}>
          <Image
            source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAxma9OqJvtHMbZDTRc0rPKuqVudyZU8HFBx46FZY0Ro8KZ08NZyKqWtpgLYRMqce6cH8s4fmKpkaBOH2SFQCV0toz0l8bIcrTcl1-oUuXwzIT-nTW4HSYYHK7xNObLpEcfNt7cHtBDr287DNQLYftTBYmxbTEoFhFAM8ugn5t0NCO0UZpjJy4HJa8GvZ4BbUOCWB7WeQHa4vq6Y-YEm8imBJ31y4C388t0E3wjrgU35jg7JGGNzyi-Wr5VbvIivHKAgwzLwNlKqJiP' }}
            style={styles.avatar}
          />
          <View>
            <Text style={styles.welcomeText}>Welcome back,</Text>
            <Text style={styles.userName}>Admin User</Text>
          </View>
        </View>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="bell" size={24} color="white" />
          <View style={styles.notificationDot} />
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.scrollViewContent}>
        <View style={styles.statsGrid}>
          <StatCard icon="tool" label="Total Jobs" value="124" change="+12%" />
          <StatCard icon="activity" label="Active" value="18" />
          <StatCard icon="dollar-sign" label="Revenue" value="$14.2k" change="+5%" isPrimary />
          <StatCard icon="clock" label="Pending" value="4" />
        </View>

        <View style={styles.chartCard}>
          <Text style={styles.chartTitle}>Weekly Revenue</Text>
          <Text style={styles.chartSubtitle}>Last 7 days performance</Text>
          {/* Simplified Chart Representation */}
        </View>

        <View style={styles.chartCard}>
          <Text style={styles.chartTitle}>Job Status</Text>
           {/* Simplified Chart Representation */}
        </View>
      </ScrollView>
      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={24} color="white" />
        <Text style={styles.fabText}>New Order</Text>
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const StatCard = ({ icon, label, value, change, isPrimary }) => (
  <View style={[styles.statCard, isPrimary && styles.primaryStatCard]}>
    <Feather name={icon} size={24} color={isPrimary ? 'white' : '#3caff6'} />
    <Text style={[styles.statLabel, isPrimary && styles.lightText]}>{label}</Text>
    <Text style={[styles.statValue, isPrimary && styles.lightText]}>{value}</Text>
    {change && <Text style={[styles.statChange, isPrimary && styles.lightText]}>{change}</Text>}
  </View>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101b22',
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
    width: 40,
    height: 40,
    borderRadius: 20,
    marginRight: 12,
  },
  welcomeText: {
    color: '#90a7cb',
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
  notificationDot: {
    position: 'absolute',
    top: 8,
    right: 8,
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#3caff6',
  },
  scrollViewContent: {
    padding: 16,
  },
  statsGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
  },
  statCard: {
    backgroundColor: '#16232d',
    borderRadius: 16,
    padding: 16,
    width: '48%',
    marginBottom: 16,
  },
  primaryStatCard: {
    backgroundColor: '#3caff6',
  },
  statLabel: {
    color: '#90a7cb',
    marginTop: 8,
  },
  statValue: {
    color: 'white',
    fontSize: 24,
    fontWeight: 'bold',
  },
  statChange: {
    color: '#3caff6',
    fontSize: 12,
  },
  lightText: {
    color: 'white',
  },
  chartCard: {
    backgroundColor: '#16232d',
    borderRadius: 16,
    padding: 16,
    marginBottom: 16,
  },
  chartTitle: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  chartSubtitle: {
    color: '#90a7cb',
    marginBottom: 16,
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    backgroundColor: '#3caff6',
    borderRadius: 28,
    paddingVertical: 12,
    paddingHorizontal: 16,
    flexDirection: 'row',
    alignItems: 'center',
    shadowColor: '#3caff6',
    shadowRadius: 10,
    shadowOpacity: 0.5,
  },
  fabText: {
    color: 'white',
    fontWeight: 'bold',
    marginLeft: 8,
  },
});

export default AdminDashboardScreen;
