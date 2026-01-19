
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView } from 'react-native';
import { Feather } from '@expo/vector-icons';
import * as Progress from 'react-native-progress';

const DataSyncScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.container}>
        <View style={styles.bottomSheet}>
          <View style={styles.handle} />
          <Text style={styles.title}>Data Synchronization</Text>
          <Text style={styles.subtitle}>System data is being synchronized</Text>

          <View style={styles.progressContainer}>
            <Progress.Circle
              size={160}
              progress={0.7}
              thickness={8}
              color="#3c83f6"
              unfilledColor="#334155"
              borderWidth={0}
            />
            <View style={styles.progressTextContainer}>
              <Text style={styles.progressPercentage}>70%</Text>
              <Text style={styles.progressStatus}>Active</Text>
            </View>
          </View>

          <Text style={styles.sectionHeader}>Updated Items</Text>

          <SyncItem icon="users" title="Customers" status="Completed" />
          <SyncItem icon="package" title="Stock Information" status="In Progress" progress={0.45} />
          <SyncItem icon="file-text" title="Service Records" status="Pending" />

          <TouchableOpacity style={styles.actionButton}>
            <Text style={styles.actionButtonText}>Continue in Background</Text>
          </TouchableOpacity>
        </View>
      </View>
    </SafeAreaView>
  );
};

const SyncItem = ({ icon, title, status, progress }) => (
  <View style={styles.syncItem}>
    <View style={styles.itemIconContainer}>
      <Feather name={icon} size={24} color="#3c83f6" />
    </View>
    <View style={styles.itemDetails}>
      <Text style={styles.itemTitle}>{title}</Text>
      <Text style={styles.itemStatus}>{status === 'In Progress' ? 'Updating...' : 'Synchronized'}</Text>
    </View>
    {status === 'Completed' && <Feather name="check-circle" size={24} color="#22c55e" />}
    {status === 'In Progress' && (
      <View style={styles.itemProgress}>
        <Progress.Bar
          progress={progress}
          width={60}
          color="#3c83f6"
          unfilledColor="#334155"
          borderWidth={0}
        />
        <Text style={styles.itemProgressText}>{Math.round(progress * 100)}%</Text>
      </View>
    )}
    {status === 'Pending' && <Feather name="clock" size={24} color="#94a3b8" />}
  </View>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: 'rgba(0,0,0,0.5)',
    justifyContent: 'flex-end',
  },
  container: {
    flex: 1,
    justifyContent: 'flex-end',
  },
  bottomSheet: {
    backgroundColor: '#101722',
    borderTopLeftRadius: 20,
    borderTopRightRadius: 20,
    padding: 16,
    paddingBottom: 32,
  },
  handle: {
    width: 48,
    height: 6,
    borderRadius: 3,
    backgroundColor: '#334155',
    alignSelf: 'center',
    marginBottom: 16,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
    textAlign: 'center',
  },
  subtitle: {
    fontSize: 14,
    color: '#94a3b8',
    textAlign: 'center',
    marginBottom: 24,
  },
  progressContainer: {
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 24,
  },
  progressTextContainer: {
    position: 'absolute',
    alignItems: 'center',
  },
  progressPercentage: {
    fontSize: 32,
    fontWeight: 'bold',
    color: 'white',
  },
  progressStatus: {
    fontSize: 12,
    fontWeight: '500',
    color: '#3c83f6',
    textTransform: 'uppercase',
  },
  sectionHeader: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
    marginBottom: 16,
  },
  syncItem: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  itemIconContainer: {
    width: 48,
    height: 48,
    borderRadius: 12,
    backgroundColor: 'rgba(60, 131, 246, 0.1)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  itemDetails: {
    flex: 1,
    marginLeft: 16,
  },
  itemTitle: {
    fontSize: 16,
    fontWeight: '500',
    color: 'white',
  },
  itemStatus: {
    fontSize: 12,
    color: '#94a3b8',
  },
  itemProgress: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  itemProgressText: {
    fontSize: 12,
    color: 'white',
    marginLeft: 8,
  },
  actionButton: {
    backgroundColor: '#3c83f6',
    borderRadius: 12,
    paddingVertical: 16,
    alignItems: 'center',
    marginTop: 24,
  },
  actionButtonText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: 'white',
  },
});

export default DataSyncScreen;
