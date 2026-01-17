
import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, ScrollView, TextInput, TouchableOpacity, Image } from 'react-native';
import customerService from '../../services/api/customer.service';

const CustomerListScreen = () => {
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const data = await customerService.getCustomers();
        setCustomers(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchCustomers();
  }, []);

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Customers</Text>
      </View>
      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder="Search by name, company..."
          placeholderTextColor="#94a3b8"
        />
      </View>
      <ScrollView contentContainerStyle={styles.listContainer}>
        {customers.map((customer, index) => (
          <View key={index} style={styles.card}>
            <View style={styles.cardHeader}>
              <Image source={{ uri: customer.avatar }} style={styles.avatar} />
              <View style={styles.cardHeaderText}>
                <Text style={styles.customerName}>{customer.firstName} {customer.lastName}</Text>
                <Text style={styles.customerTitle}>{customer.email}</Text>
              </View>
            </View>
            <View style={styles.cardBody}>
              <View style={styles.stat}>
                <Text style={styles.statLabel}>Total Orders</Text>
                <Text style={styles.statValue}>{customer.totalOrders || 0}</Text>
              </View>
              <View style={styles.stat}>
                <Text style={styles.statLabel}>Total Spend</Text>
                <Text style={styles.statValue}>${(customer.totalSpend || 0).toLocaleString()}</Text>
              </View>
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
    alignItems: 'center',
  },
  avatar: {
    width: 64,
    height: 64,
    borderRadius: 32,
  },
  cardHeaderText: {
    marginLeft: 16,
    flex: 1,
  },
  customerName: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  customerTitle: {
    fontSize: 14,
    color: '#94a3b8',
    marginTop: 4,
  },
  cardBody: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 16,
    borderTopWidth: 1,
    borderTopColor: '#334155',
    paddingTop: 16,
  },
  stat: {
    alignItems: 'center',
  },
  statLabel: {
    fontSize: 12,
    color: '#94a3b8',
    textTransform: 'uppercase',
  },
  statValue: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
    marginTop: 4,
  },
});

export default CustomerListScreen;
