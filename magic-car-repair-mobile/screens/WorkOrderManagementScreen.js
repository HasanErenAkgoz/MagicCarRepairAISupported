
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const WorkOrderManagementScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Work Orders</Text>
        <View style={{ width: 40 }}/>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search by plate, ID, or customer..."
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.filters}>
        <TouchableOpacity style={[styles.filterButton, styles.activeFilter]}>
          <Text style={styles.activeFilterText}>All</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Active</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Pending Parts</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Completed</Text>
        </TouchableOpacity>
      </ScrollView>

      <ScrollView contentContainerStyle={styles.content}>
        <WorkOrderCard
          id="#WO-8821"
          status="In Progress"
          customer="John Doe"
          vehicle="Toyota Camry • 2019"
          plate="ABC-1234"
          estimate="$450.00"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuDLL_c1bbt6_zqpY4k3g-ZuOhmsG6omARKuh8VIeoLEs-1veyAIIEOSAousaPdMF-FilV5_0iFIrUw4vpTDSmKXqi4Xxgv_igCHUybewLnxVskDtk0DyB4AHFj_ltfj-oLdGQ7Lbrb1ax0H2oa1ac6R8-tYBi85LD4FaF35JdLCS42SML5RoV0BbrLu9KGEcAWYvtL63BUV_M0Uu3QwKsQjyO6RAKiAr4N2mQ2iS4oQhuKnfSAvGJINcZsG5POOa3S3fwVPxAlBMnw9"
        />
        <WorkOrderCard
          id="#WO-8822"
          status="Pending Parts"
          customer="Jane Smith"
          vehicle="Honda Civic • 2021"
          plate="XYZ-9876"
          estimate="$120.00"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBCnDmobIDRCcw9YgLeeWt2NtyrcE1vBE7PTeSoTqPeAlw3rkpepSQEPGV3JLnHN4gNRtdnL-Jz1oMFz31pBDf8ThhNoVIjdyRZ2LnI-D3pFd2YrL4NepRaZc7Rylqf1m4cBXSL49FAgEhHIsFURHJZeDso8XZFxQ3d78K-GIfaaAuSXbHR92fbDcFSHvp8Vs2NyK1w_hbQk6hTLUtDieFe18FrTSWcSi3CtpbSii_gtRqMfXk5rUE3NmcwP7RfQ8uZnbRXc4DVh2lc"
        />
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={32} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const WorkOrderCard = ({ id, status, customer, vehicle, plate, estimate, avatarUrl }) => {
    const getStatusStyle = () => {
        switch(status) {
            case 'In Progress': return { backgroundColor: 'rgba(59, 130, 246, 0.2)', color: '#3B82F6', borderColor: 'rgba(59, 130, 246, 0.2)' };
            case 'Pending Parts': return { backgroundColor: 'rgba(245, 158, 11, 0.2)', color: '#F59E0B', borderColor: 'rgba(245, 158, 11, 0.2)' };
            default: return {};
        }
    }
    return (
        <TouchableOpacity style={styles.card}>
            <View style={styles.cardHeader}>
                <Text style={styles.cardId}>{id}</Text>
                <View style={[styles.statusBadge, getStatusStyle()]}>
                    <Text style={[styles.statusText, {color: getStatusStyle().color}]}>{status}</Text>
                </View>
            </View>
            <View style={styles.cardBody}>
                <Image source={{ uri: avatarUrl }} style={styles.avatar} />
                <View>
                    <Text style={styles.customerName}>{customer}</Text>
                    <Text style={styles.vehicleInfo}>{vehicle}</Text>
                    <Text style={styles.plate}>{plate}</Text>
                </View>
            </View>
            <View style={styles.cardFooter}>
                <Text style={styles.estimateLabel}>Total Estimate</Text>
                <Text style={styles.estimateValue}>{estimate}</Text>
            </View>
        </TouchableOpacity>
    );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: 16,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  searchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
    borderRadius: 8,
    marginHorizontal: 16,
    paddingHorizontal: 12,
  },
  searchIcon: {
    marginRight: 8,
  },
  searchInput: {
    flex: 1,
    height: 48,
    color: 'white',
  },
  filters: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    marginTop: 16,
  },
  filterButton: {
    paddingVertical: 8,
    paddingHorizontal: 16,
    borderRadius: 20,
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
    marginRight: 8,
  },
  activeFilter: {
    backgroundColor: '#3B82F6',
  },
  filterText: {
    color: '#cbd5e1',
  },
  activeFilterText: {
    color: 'white',
    fontWeight: 'bold',
  },
  content: {
    padding: 16,
  },
  card: {
    backgroundColor: 'rgba(255, 255, 255, 0.05)',
    borderRadius: 12,
    padding: 16,
    marginBottom: 16,
    borderWidth: 1,
    borderColor: 'rgba(255, 255, 255, 0.1)',
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  cardId: {
    color: 'white',
    fontWeight: '600',
  },
  statusBadge: {
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 12,
  },
  statusText: {
    fontSize: 12,
    fontWeight: '500',
  },
  cardBody: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 16,
  },
  avatar: {
    width: 48,
    height: 48,
    borderRadius: 24,
    marginRight: 12,
  },
  customerName: {
    color: 'white',
    fontWeight: '500',
  },
  vehicleInfo: {
    color: 'rgba(255, 255, 255, 0.5)',
    fontSize: 12,
  },
  plate: {
    backgroundColor: 'rgba(255, 255, 255, 0.1)',
    color: 'rgba(255, 255, 255, 0.7)',
    paddingHorizontal: 6,
    paddingVertical: 2,
    borderRadius: 4,
    alignSelf: 'flex-start',
    marginTop: 4,
  },
  cardFooter: {
    borderTopWidth: 1,
    borderColor: 'rgba(255, 255, 255, 0.1)',
    marginTop: 16,
    paddingTop: 16,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  estimateLabel: {
    color: 'rgba(255, 255, 255, 0.4)',
    fontSize: 12,
  },
  estimateValue: {
    color: '#3B82F6',
    fontSize: 18,
    fontWeight: 'bold',
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    width: 56,
    height: 56,
    borderRadius: 28,
    backgroundColor: '#3B82F6',
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default WorkOrderManagementScreen;
