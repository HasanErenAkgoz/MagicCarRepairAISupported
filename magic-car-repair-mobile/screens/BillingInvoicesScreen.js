
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView } from 'react-native';
import { Feather } from '@expo/vector-icons';
import * as Progress from 'react-native-progress';

const BillingInvoicesScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Invoices</Text>
        <TouchableOpacity style={styles.addButton}>
          <Feather name="plus" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.summary}>
        <Text style={styles.summaryLabel}>Total Outstanding</Text>
        <Text style={styles.summaryAmount}>$12,450.00</Text>
      </View>

      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.filters}>
        <TouchableOpacity style={[styles.filterButton, styles.activeFilter]}>
          <Text style={styles.activeFilterText}>All</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Pending</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Paid</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Overdue</Text>
        </TouchableOpacity>
      </ScrollView>

      <ScrollView contentContainerStyle={styles.content}>
        <InvoiceCard
          company="Acme Corp."
          invoice="#INV-004"
          status="Pending"
          dueDate="Due Oct 24"
          amount="$4,500.00"
          paidStatus="50% Paid"
          progress={0.5}
        />
        <InvoiceCard
          company="Globex Inc."
          invoice="#INV-003"
          status="Paid"
          dueDate="Due Oct 10"
          amount="$2,100.00"
          paidStatus="Completed"
          progress={1}
        />
      </ScrollView>
    </SafeAreaView>
  );
};

const InvoiceCard = ({ company, invoice, status, dueDate, amount, paidStatus, progress }) => {
    const getStatusStyle = () => {
        switch(status) {
            case 'Pending': return { backgroundColor: 'rgba(249, 115, 22, 0.1)', color: '#F97316' };
            case 'Paid': return { backgroundColor: 'rgba(34, 197, 94, 0.1)', color: '#22C55E' };
            case 'Overdue': return { backgroundColor: 'rgba(239, 68, 68, 0.1)', color: '#EF4444' };
            default: return {};
        }
    }
    return (
        <View style={styles.card}>
            <View style={styles.cardHeader}>
                <View>
                    <Text style={styles.companyName}>{company}</Text>
                    <Text style={styles.invoiceNumber}>{invoice}</Text>
                </View>
                <View style={[styles.statusBadge, getStatusStyle()]}>
                    <Text style={[styles.statusText, {color: getStatusStyle().color}]}>{status}</Text>
                </View>
            </View>
            <View style={styles.cardBody}>
                <View>
                    <Text style={styles.amountLabel}>Amount</Text>
                    <Text style={styles.amountValue}>{amount}</Text>
                </View>
                <Text style={styles.paidStatus}>{paidStatus}</Text>
            </View>
            <Progress.Bar
              progress={progress}
              width={null}
              color={progress === 1 ? '#22C55E' : '#2563eb'}
              unfilledColor="rgba(255, 255, 255, 0.1)"
              borderWidth={0}
              height={8}
            />
        </View>
    );
}

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: 'bold',
    color: 'white',
  },
  addButton: {
    backgroundColor: '#2563eb',
    width: 40,
    height: 40,
    borderRadius: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  summary: {
    paddingHorizontal: 16,
    marginBottom: 16,
  },
  summaryLabel: {
    color: '#94a3b8',
    textTransform: 'uppercase',
  },
  summaryAmount: {
    color: '#2563eb',
    fontSize: 32,
    fontWeight: 'bold',
  },
  filters: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    marginBottom: 16,
  },
  filterButton: {
    paddingVertical: 8,
    paddingHorizontal: 16,
    borderRadius: 20,
    backgroundColor: '#1e293b',
    marginRight: 8,
  },
  activeFilter: {
    backgroundColor: '#2563eb',
  },
  filterText: {
    color: '#cbd5e1',
  },
  activeFilterText: {
    color: 'white',
    fontWeight: 'bold',
  },
  content: {
    paddingHorizontal: 16,
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
    alignItems: 'flex-start',
    marginBottom: 16,
  },
  companyName: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  invoiceNumber: {
    color: '#94a3b8',
    fontSize: 12,
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
    justifyContent: 'space-between',
    alignItems: 'flex-end',
    marginBottom: 8,
  },
  amountLabel: {
    color: '#94a3b8',
    fontSize: 12,
  },
  amountValue: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  paidStatus: {
    color: '#94a3b8',
    fontSize: 12,
  },
});

export default BillingInvoicesScreen;
