
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const WorkOrderListScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Work Orders</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="bell" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search Job ID, Plate, or Customer"
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.filters}>
        <TouchableOpacity style={[styles.filterButton, styles.activeFilter]}>
          <Text style={styles.activeFilterText}>All</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Ongoing</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Ready</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Waiting</Text>
        </TouchableOpacity>
      </ScrollView>

      <ScrollView style={styles.scrollView}>
        <WorkOrderCard
          id="#WO-2942"
          status="Ongoing"
          vehicle="2018 Ford F-150"
          plate="KJA-8842"
          customer="Michael Scott"
          dueDate="Due Today"
          avatar="MS"
          imageUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuA-8Jm3WsJ69EhPTRVf6DPvzhpAOu-ViK5azl2mmBXnXOoxFWKwLD8QIChXzfv-WfdGFN54uJdkV2DjEqZApWsDWx0-g_ti5W5BMrMf-waqyKmov0OOlAshR46RsN_R_lKcLI3hD_idYJ6k8v60YNyo22iClec5w2Z8j-dTUTuAUal-7QFcweEOSu3uh4hxWfVeMVTtYNAHZy610U0jsnVu_1keDwrmZoQwinE7OeTkF_vEXd7SiJpen8_q3-Q5VzleoNhbWzUQ5H87"
        />
        <WorkOrderCard
          id="#WO-2943"
          status="Ready"
          vehicle="2020 Toyota Camry"
          plate="LMN-9921"
          customer="Pam Beesly"
          dueDate="Done 2h ago"
          avatar="PB"
          imageUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuCQYVAjN7jhcEA1lvWaZnWBXiH9_tdbI8ErjOiKSXRtpBg0JbSU42p1tpLf-bdxMJO3YPRNJj5yJnhc5wcJ4ixT5Ww8EyAFJiS8bEsX_BhAfQWvYEVsxoMtOQR621q6S7LFO8H6ekRBlBB4O6yqc-lIsQG9Iz0oQqtny-h4eHX--jg18NRs0OhFQONBMM85wjthkX6d_3QG-MoFBtOHR6N5D-NdtvxzHWWr6lL5nTKoZ9oSRra4hkwV1VGHvrZbRBRkrYPqQnNhMtgO"
        />
      </ScrollView>
      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={28} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const WorkOrderCard = ({ id, status, vehicle, plate, customer, dueDate, avatar, imageUrl }) => {
  const statusStyles = {
    Ongoing: {
      backgroundColor: 'rgba(59, 130, 246, 0.1)',
      color: '#3b82f6',
    },
    Ready: {
      backgroundColor: 'rgba(16, 185, 129, 0.1)',
      color: '#10b981',
    },
  };

  return (
    <View style={styles.card}>
      <View style={styles.cardHeader}>
        <Text style={styles.workOrderId}>{id}</Text>
        <Text style={[styles.status, statusStyles[status]]}>{status}</Text>
      </View>
      <View style={styles.cardBody}>
        <View style={styles.vehicleInfo}>
          <Text style={styles.vehicleName}>{vehicle}</Text>
          <Text style={styles.plate}>{plate}</Text>
        </View>
        <Image source={{ uri: imageUrl }} style={styles.vehicleImage} />
      </View>
      <View style={styles.cardFooter}>
        <View style={styles.customerInfo}>
          <View style={styles.avatar}>
            <Text>{avatar}</Text>
          </View>
          <Text style={styles.customerName}>{customer}</Text>
        </View>
        <Text style={styles.dueDate}>{dueDate}</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101722',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  searchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#1e293b',
    borderRadius: 12,
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
    backgroundColor: '#1e293b',
    marginRight: 8,
  },
  activeFilter: {
    backgroundColor: '#3c83f6',
  },
  filterText: {
    color: '#cbd5e1',
  },
  activeFilterText: {
    color: 'white',
    fontWeight: 'bold',
  },
  scrollView: {
    padding: 16,
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
    marginBottom: 12,
  },
  workOrderId: {
    color: '#3c83f6',
    fontWeight: 'bold',
  },
  status: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    fontSize: 12,
  },
  cardBody: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 12,
  },
  vehicleInfo: {
    flex: 1,
  },
  vehicleName: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  plate: {
    color: '#94a3b8',
    marginTop: 4,
  },
  vehicleImage: {
    width: 64,
    height: 64,
    borderRadius: 8,
  },
  cardFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderTopWidth: 1,
    borderTopColor: 'rgba(255, 255, 255, 0.1)',
    paddingTop: 12,
  },
  customerInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatar: {
    width: 24,
    height: 24,
    borderRadius: 12,
    backgroundColor: '#334155',
    alignItems: 'center',
    justifyContent: 'center',
    marginRight: 8,
  },
  customerName: {
    color: 'white',
  },
  dueDate: {
    color: '#94a3b8',
    fontSize: 12,
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    width: 56,
    height: 56,
    borderRadius: 28,
    backgroundColor: '#3c83f6',
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default WorkOrderListScreen;
