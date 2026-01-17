
import React from 'react';
import { View, Text, StyleSheet, ScrollView, TextInput, TouchableOpacity, Image } from 'react-native';

const WorkOrdersScreen = () => {
  const orders = [
    {
      id: '#WO-8821',
      status: 'In Progress',
      customer: 'John Doe',
      vehicle: 'Toyota Camry • 2019',
      plate: 'ABC-1234',
      avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDLL_c1bbt6_zqpY4k3g-ZuOhmsG6omARKuh8VIeoLEs-1veyAIIEOSAousaPdMF-FilV5_0iFIrUw4vpTDSmKXqi4Xxgv_igCHUybewLnxVskDtk0DyB4AHFj_ltfj-oLdGQ7Lbrb1ax0H2oa1ac6R8-tYBi85LD4FaF35JdLCS42SML5RoV0BbrLu9KGEcAWYvtL63BUV_M0Uu3QwKsQjyO6RAKiAr4N2mQ2iS4oQhuKnfSAvGJINcZsG5POOa3S3fwVPxAlBMnw9',
      estimate: 450.00,
    },
    {
      id: '#WO-8822',
      status: 'Pending Parts',
      customer: 'Jane Smith',
      vehicle: 'Honda Civic • 2021',
      plate: 'XYZ-9876',
      avatar: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBCnDmobIDRCcw9YgLeeWt2NtyrcE1vBE7PTeSoTqPeAlw3rkpepSQEPGV3JLnHN4gNRtdnL-Jz1oMFz31pBDf8ThhNoVIjdyRZ2LnI-D3pFd2YrL4NepRaZc7Rylqf1m4cBXSL49FAgEhHIsFURHJZeDso8XZFxQ3d78K-GIfaaAuSXbHR92fbDcFSHvp8Vs2NyK1w_hbQk6hTLUtDieFe18FrTSWcSi3CtpbSii_gtRqMfXk5rUE3NmcwP7RfQ8uZnbRXc4DVh2lc',
      estimate: 120.00,
    },
  ];

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Work Orders</Text>
      </View>
      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder="Search by plate, ID, or customer..."
          placeholderTextColor="#94a3b8"
        />
      </View>
      <ScrollView contentContainerStyle={styles.listContainer}>
        {orders.map((order, index) => (
          <View key={index} style={styles.card}>
            <View style={styles.cardHeader}>
              <Text style={styles.orderId}>{order.id}</Text>
              <Text style={[styles.status, styles[`status${order.status.replace(/\s+/g, '')}`]]}>{order.status}</Text>
            </View>
            <View style={styles.cardBody}>
              <Image source={{ uri: order.avatar }} style={styles.avatar} />
              <View style={styles.customerInfo}>
                <Text style={styles.customerName}>{order.customer}</Text>
                <Text style={styles.vehicleInfo}>{order.vehicle}</Text>
                <Text style={styles.plate}>{order.plate}</Text>
              </View>
            </View>
            <View style={styles.cardFooter}>
              <Text style={styles.estimateLabel}>Total Estimate</Text>
              <Text style={styles.estimateValue}>${order.estimate.toFixed(2)}</Text>
            </View>
          </View>
        ))}
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  header: {
    paddingTop: 60,
    paddingHorizontal: 20,
    paddingBottom: 10,
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
    textAlign: 'center',
  },
  searchContainer: {
    paddingHorizontal: 20,
    paddingVertical: 10,
  },
  searchInput: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    paddingHorizontal: 16,
    paddingVertical: 14,
    color: 'white',
    fontSize: 16,
  },
  listContainer: {
    paddingHorizontal: 20,
    paddingBottom: 20,
  },
  card: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 16,
    marginBottom: 16,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16,
  },
  orderId: {
    color: 'white',
    fontWeight: '600',
  },
  status: {
    fontSize: 12,
    fontWeight: '600',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 12,
    overflow: 'hidden',
  },
  statusInProgress: {
    backgroundColor: 'rgba(59, 130, 246, 0.2)',
    color: '#3B82F6',
  },
  statusPendingParts: {
    backgroundColor: 'rgba(245, 158, 11, 0.2)',
    color: '#F59E0B',
  },
  cardBody: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  avatar: {
    width: 48,
    height: 48,
    borderRadius: 24,
  },
  customerInfo: {
    marginLeft: 16,
  },
  customerName: {
    color: 'white',
    fontWeight: 'bold',
  },
  vehicleInfo: {
    color: '#94a3b8',
    marginTop: 2,
  },
  plate: {
    color: 'white',
    backgroundColor: '#334155',
    alignSelf: 'flex-start',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    marginTop: 4,
    overflow: 'hidden',
  },
  cardFooter: {
    borderTopWidth: 1,
    borderTopColor: '#334155',
    paddingTop: 16,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  estimateLabel: {
    color: '#94a3b8',
    fontSize: 12,
  },
  estimateValue: {
    color: '#3B82F6',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default WorkOrdersScreen;
