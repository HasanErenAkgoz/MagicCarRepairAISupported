
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const StaffManagementScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Staff Management</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="user" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search by name or ID..."
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.filters}>
        <TouchableOpacity style={[styles.filterButton, styles.activeFilter]}>
          <Text style={styles.activeFilterText}>All</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Technicians</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Advisors</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Management</Text>
        </TouchableOpacity>
      </ScrollView>

      <ScrollView style={styles.scrollView}>
        <StaffCard
          name="Alex Johnson"
          id="#M-8821"
          role="Lead Mechanic"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuDRRTTQlQdMtt9cX_hLosaOC2HZNqqskoqRh7j7RGfmWtZWpSxO3SQDi8T0VkPJYYw700X2-7Ev4ANz1Z0V1G2nEIjv-njZIEMWptPi927KMEBM5Gq9bYrHO2JDbzoUIQ_nAinWxnQGFQ5-Mm_q0HFnOawedFnmgk8z2cYYjlf1Y4E7idp6_MbNnB1v9MAVseOkoLn48vy5ZeUKqpxih5MRgrczN2PzYAV_1uAxm9HcNq_hIYABTNDN7CSQLZDSkmTCfqqsHiHWnGTk"
        />
        <StaffCard
          name="Sarah Miller"
          id="#A-2290"
          role="Service Advisor"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuDYoOokdPEaikm_Kryq8ywd_OlSDOwni2fcgX-9z1c9HiHtIaBOc7MMUp41yHxAeEyJC109LsIRT4rX7xHH4HfW1VuHp2G6HttN4VNPzmJgyQ29tvbC50M3rPgfDR5Kb5D_-BOSJwqtKbHS3bAdbDnBRx7hEehC4ISmVl_3v5NEITC5fMlA0BLS1vq3pS7r0ey8Ue6ntrJPf1ykJuBuz1j-57jIHWNFKpkgCvMoAaoYrqedp3uWZwwXgsy_-Js9i9x1qNCPkTD0y2Iv"
        />
        <StaffCard
          name="Mike Chen"
          id="#T-9932"
          role="Technician"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuC5-w8askXYlvmS4jR4-63uapLkNWs43t2XdVhpidNRrDdYZo9yR6G1eAOYNq9FwjfvAl87FlB0rK7aeKRipyVVluuVxnft_x7kW3kxNB_mFIdrplmyNl5EH6NWzhO0JQv1wXl2sh2UklOr0gBRBIAySAtNYBAsJ8pMujx877xyoO7TXOyHv5ExqhBdriCNanmiBukmF3nHqywmG7FcwrovS3mS4Z_kQs0bdYbjN4VqPNlfQ4_oybyDJWzUC4S8uqUu19KdRKgEIbIS"
        />
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={28} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const StaffCard = ({ name, id, role, avatarUrl }) => (
  <View style={styles.card}>
    <Image source={{ uri: avatarUrl }} style={styles.avatar} />
    <View style={styles.cardDetails}>
      <Text style={styles.name}>{name}</Text>
      <Text style={styles.staffId}>{id}</Text>
      <Text style={styles.role}>{role}</Text>
    </View>
    <View style={styles.actionButtons}>
      <TouchableOpacity style={styles.actionButton}>
        <Feather name="phone" size={20} color="#3c83f6" />
      </TouchableOpacity>
      <TouchableOpacity style={styles.actionButton}>
        <Feather name="message-square" size={20} color="#3c83f6" />
      </TouchableOpacity>
    </View>
  </View>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101722',
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
    borderRadius: 12,
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  avatar: {
    width: 56,
    height: 56,
    borderRadius: 28,
  },
  cardDetails: {
    flex: 1,
    marginLeft: 16,
  },
  name: {
    fontSize: 16,
    fontWeight: 'bold',
    color: 'white',
  },
  staffId: {
    fontSize: 12,
    color: '#94a3b8',
  },
  role: {
    fontSize: 12,
    color: '#3c83f6',
    backgroundColor: 'rgba(60, 131, 246, 0.1)',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    marginTop: 4,
    alignSelf: 'flex-start',
  },
  actionButtons: {
    flexDirection: 'row',
  },
  actionButton: {
    padding: 8,
    marginLeft: 8,
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

export default StaffManagementScreen;
