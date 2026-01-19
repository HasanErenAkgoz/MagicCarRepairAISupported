
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView } from 'react-native';
import { Feather } from '@expo/vector-icons';

const AnalyticsScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Analytics</Text>
        <TouchableOpacity style={styles.dateRangeButton}>
          <Text style={styles.dateRangeText}>This Month</Text>
          <Feather name="chevron-down" size={20} color="#3c83f6" />
        </TouchableOpacity>
      </View>

      <ScrollView>
        <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.horizontalScroll}>
          <RevenueChart />
          <ServiceMixChart />
        </ScrollView>

        <View style={styles.statsRow}>
          <StatCard icon="dollar-sign" label="Avg Ticket" value="$428" change="+3.2%" />
          <StatCard icon="truck" label="Serviced" value="148" change="+5.0%" />
          <StatCard icon="tool" label="Parts Wait" value="12" change="+2.0%" isNegative />
        </View>

        <Text style={styles.sectionTitle}>Report Categories</Text>
        <View style={styles.categoriesGrid}>
          <CategoryCard icon="trending-up" title="Revenue" subtitle="Income & Profit" />
          <CategoryCard icon="credit-card" title="Expenses" subtitle="Costs & Overhead" />
          <CategoryCard icon="file-text" title="Invoices" subtitle="Paid & Pending" />
          <CategoryCard icon="users" title="Tech Efficiency" subtitle="Hours & Output" />
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const RevenueChart = () => (
  <View style={styles.chartCard}>
    <Text style={styles.chartTitle}>Revenue vs Costs</Text>
    <Text style={styles.chartValue}>$42,500</Text>
    {/* Simplified bar chart representation */}
    <View style={styles.barChart}>
      <View style={[styles.bar, { height: '60%' }]} />
      <View style={[styles.bar, { height: '45%' }]} />
      <View style={[styles.bar, { height: '75%' }]} />
      <View style={[styles.bar, { height: '100%' }]} />
    </View>
  </View>
);

const ServiceMixChart = () => (
  <View style={styles.chartCard}>
    <Text style={styles.chartTitle}>Service Distribution</Text>
    <Text style={styles.chartValue}>148 Jobs</Text>
    {/* Simplified pie chart representation */}
    <View style={styles.pieChart}>
      <View style={styles.pieSlice1} />
      <View style={styles.pieSlice2} />
    </View>
  </View>
);

const StatCard = ({ icon, label, value, change, isNegative }) => (
  <View style={styles.statCard}>
    <Feather name={icon} size={24} color="#3c83f6" />
    <Text style={styles.statLabel}>{label}</Text>
    <Text style={styles.statValue}>{value}</Text>
    <Text style={[styles.statChange, isNegative && styles.negativeChange]}>{change}</Text>
  </View>
);

const CategoryCard = ({ icon, title, subtitle }) => (
  <TouchableOpacity style={styles.categoryCard}>
    <Feather name={icon} size={24} color="#3c83f6" />
    <Text style={styles.categoryTitle}>{title}</Text>
    <Text style={styles.categorySubtitle}>{subtitle}</Text>
  </TouchableOpacity>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101722',
  },
  header: {
    padding: 16,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: 'bold',
    color: 'white',
  },
  dateRangeButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#1e293b',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 20,
  },
  dateRangeText: {
    color: '#3c83f6',
    marginRight: 4,
  },
  horizontalScroll: {
    paddingLeft: 16,
    marginBottom: 16,
  },
  chartCard: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 16,
    width: 300,
    marginRight: 16,
  },
  chartTitle: {
    color: '#94a3b8',
  },
  chartValue: {
    color: 'white',
    fontSize: 24,
    fontWeight: 'bold',
    marginTop: 4,
  },
  barChart: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    height: 100,
    marginTop: 16,
  },
  bar: {
    width: 20,
    backgroundColor: '#3c83f6',
    borderRadius: 4,
  },
  pieChart: {
    width: 100,
    height: 100,
    borderRadius: 50,
    marginTop: 16,
    backgroundColor: '#3c83f6',
    alignSelf: 'center',
  },
  pieSlice1: {
    position: 'absolute',
    width: '50%',
    height: '100%',
    backgroundColor: '#60a5fa',
    borderTopLeftRadius: 50,
    borderBottomLeftRadius: 50,
  },
  pieSlice2: {
    position: 'absolute',
    width: '50%',
    height: '50%',
    backgroundColor: '#93c5fd',
    left: '50%',
  },
  statsRow: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingHorizontal: 16,
    marginBottom: 24,
  },
  statCard: {
    alignItems: 'center',
  },
  statLabel: {
    color: '#94a3b8',
    fontSize: 12,
    marginTop: 4,
  },
  statValue: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  statChange: {
    color: '#10b981',
    fontSize: 12,
  },
  negativeChange: {
    color: '#ef4444',
  },
  sectionTitle: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
    paddingHorizontal: 16,
    marginBottom: 16,
  },
  categoriesGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
  },
  categoryCard: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 16,
    width: '48%',
    marginBottom: 16,
  },
  categoryTitle: {
    color: 'white',
    fontWeight: 'bold',
    marginTop: 8,
  },
  categorySubtitle: {
    color: '#94a3b8',
    fontSize: 12,
  },
});

export default AnalyticsScreen;
