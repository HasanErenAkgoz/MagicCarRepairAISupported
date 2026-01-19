
import React from 'react';
import { View, StyleSheet, SafeAreaView, ActivityIndicator } from 'react-native';

const DashboardLoadingScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <View style={styles.shimmerAvatar} />
        <View style={styles.shimmerHeader} />
      </View>
      <View style={styles.grid}>
        <View style={styles.shimmerCard} />
        <View style={styles.shimmerCard} />
      </View>
      <View style={styles.shimmerLargeCard} />
      <View style={styles.shimmerListItem} />
      <View style={styles.shimmerListItem} />
      <View style={styles.shimmerListItem} />
      <ActivityIndicator size="large" color="#3c83f6" style={styles.activityIndicator} />
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101722',
    padding: 16,
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 24,
  },
  shimmerAvatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    backgroundColor: '#1e293b',
  },
  shimmerHeader: {
    height: 20,
    width: '40%',
    backgroundColor: '#1e293b',
    borderRadius: 4,
    marginLeft: 12,
  },
  grid: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 24,
  },
  shimmerCard: {
    width: '48%',
    height: 100,
    backgroundColor: '#1e293b',
    borderRadius: 16,
  },
  shimmerLargeCard: {
    height: 200,
    backgroundColor: '#1e293b',
    borderRadius: 16,
    marginBottom: 24,
  },
  shimmerListItem: {
    height: 60,
    backgroundColor: '#1e293b',
    borderRadius: 12,
    marginBottom: 16,
  },
  activityIndicator: {
    marginTop: 24,
  },
});

export default DashboardLoadingScreen;
