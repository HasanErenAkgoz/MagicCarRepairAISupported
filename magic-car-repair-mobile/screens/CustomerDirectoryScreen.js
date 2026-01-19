
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const CustomerDirectoryScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Customers</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="settings" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search by name, company..."
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView contentContainerStyle={styles.content}>
        <CustomerCard
          name="Sarah Jenkins"
          company="CEO at Apex Solutions"
          orders="12"
          spend="$4,500"
          isVip
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuC-FyWXa4Ft2eItDzYMqJStrMLo5KUlb4w9KnSNtKePe7SHoQgZGJuFvYPjlXchcKEWkvSqsUpYHC2_IkyR4KS8Tv-omuHXUpLBTRTWEm782x3TiixicXqWMFgrYy2f39gpghkg3mcduinQU3H7xBktFz0_qtrB_S-s7DK57ji6BlJodlvbiqHXlMnmj-tywMbx9WRRf8MbfD8NpM--R5BMy0gxhxbTL22CRG3jdDyGAe3g5vHSTxuwD8LKA4ywJThRciXqDut-Azg5"
        />
        <CustomerCard
          name="David Kim"
          company="Product Manager"
          orders="5"
          spend="$1,250"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuCYL3-lF7aGVdMTXvmVZfJkAIxSwOAh1jGE5Cs-lrzh9xzh-oe09_Ueq0hbI4bbCQy6rEPU7Os4cC__gHOeRtceWOAHWSmTaOkYbCejjLUWPWxilQS99aR0753usnAELtO8XpvRu0HE8AEUtCpk40rHOjNShoTO6cg0OJAI5lNgqNwpA_608QSGa1bE6zj83jgBgctPOPzWl7aa0SpeVs5A0qEqu4qlxmEhRAFLRwtv_d2jozJAOSTICATZziOX5_xittB5fQvJlJ1M"
        />
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={32} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const CustomerCard = ({ name, company, orders, spend, isVip, avatarUrl }) => (
  <View style={styles.card}>
    <View style={styles.cardHeader}>
      <Image source={{ uri: avatarUrl }} style={styles.avatar} />
      <View style={{ flex: 1 }}>
        <Text style={styles.customerName}>{name}</Text>
        <Text style={styles.companyName}>{company}</Text>
      </View>
      {isVip && <Text style={styles.vipBadge}>VIP</Text>}
    </View>
    <View style={styles.cardFooter}>
      <View style={styles.footerStat}>
        <Text style={styles.statLabel}>Total Orders</Text>
        <Text style={styles.statValue}>{orders}</Text>
      </View>
      <View style={styles.footerStat}>
        <Text style={styles.statLabel}>Total Spend</Text>
        <Text style={styles.statValue}>{spend}</Text>
      </View>
    </View>
  </View>
);

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
    marginBottom: 16,
  },
  searchIcon: {
    marginRight: 8,
  },
  searchInput: {
    flex: 1,
    height: 48,
    color: 'white',
  },
  content: {
    paddingHorizontal: 16,
  },
  card: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
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
    marginRight: 16,
  },
  customerName: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  companyName: {
    color: '#94a3b8',
  },
  vipBadge: {
    backgroundColor: '#F59E0B',
    color: 'white',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    fontSize: 10,
    fontWeight: 'bold',
  },
  cardFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 16,
    paddingTop: 16,
    borderTopWidth: 1,
    borderTopColor: 'rgba(255, 255, 255, 0.1)',
  },
  footerStat: {
    alignItems: 'center',
  },
  statLabel: {
    color: '#94a3b8',
    fontSize: 12,
  },
  statValue: {
    color: 'white',
    fontSize: 16,
    fontWeight: '600',
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

export default CustomerDirectoryScreen;
