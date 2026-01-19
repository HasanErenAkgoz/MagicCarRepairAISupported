
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const GlobalSearchScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Global Search</Text>
        <TouchableOpacity>
          <Text style={styles.filterText}>Filter</Text>
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#3c83f6" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          value="Brake pads"
        />
        <TouchableOpacity>
          <Feather name="x-circle" size={20} color="#94a3b8" />
        </TouchableOpacity>
      </View>

      <ScrollView>
        <Text style={styles.sectionHeader}>Customers (2)</Text>
        <SearchResult
            type="customer"
            title="Marcus Brakeman"
            subtitle="Last Visit: Oct 12, 2023"
            avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBgFWc4QRuonc1ZOZvznrlrKBRkZU7ZPGEkfNaYn0PFfFujJ9KgrgDt3jJGWNx6w6WjiZL4FjPP3k08ekpu5RBd_xd9XV-M6rKxnRC27IklXPRTucVlWlkxM0QMLKTd15qv4B-n5oJI2OpBfjLjxu84jL0gUNo5MOaVd13Y8x5R8yFBYMX3x4H1yn_qbLc2X2sDZjhZqC95VqJzcknvZIH-aJn6IrxyndLvZZ7tibWcZAWuhVS4mie9ZJNiWr0sBNHekdes6tBOfghr"
        />
        <SearchResult
            type="customer"
            title="Jane Doe"
            subtitle="Purchased ceramic brake pads"
            initials="JD"
        />

        <Text style={styles.sectionHeader}>Vehicles (1)</Text>
        <SearchResult
            type="vehicle"
            title="2019 Tesla Model 3"
            subtitle="VIN: 5YJ3E1EA5KF123456"
            notes="Needs front brake pads"
        />

        <Text style={styles.sectionHeader}>Parts (3)</Text>
        <SearchResult
            type="part"
            title="Ceramic Brake Pads (Front)"
            subtitle="SKU: BRK-4402-C"
            price="$89.99"
            stock="12 in stock"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

const SearchResult = ({ type, title, subtitle, notes, price, stock, avatarUrl, initials }) => (
    <TouchableOpacity style={styles.resultCard}>
        {avatarUrl && <Image source={{ uri: avatarUrl }} style={styles.avatar} />}
        {initials && <View style={styles.initialsContainer}><Text style={styles.initialsText}>{initials}</Text></View>}
        {!avatarUrl && !initials && (
            <View style={styles.iconContainer}>
                <Feather name={type === 'vehicle' ? 'truck' : 'settings'} size={24} color="#3c83f6" />
            </View>
        )}
        <View style={styles.resultContent}>
            <Text style={styles.resultTitle}>{title}</Text>
            <Text style={styles.resultSubtitle}>{subtitle}</Text>
            {notes && <Text style={styles.notes}>{notes}</Text>}
        </View>
        {price && <Text style={styles.price}>{price}</Text>}
    </TouchableOpacity>
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
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  filterText: {
    color: '#3c83f6',
    fontWeight: '600',
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
    fontSize: 16,
  },
  sectionHeader: {
    color: '#3c83f6',
    fontWeight: 'bold',
    textTransform: 'uppercase',
    padding: 16,
  },
  resultCard: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#1e293b',
    borderRadius: 12,
    padding: 16,
    marginHorizontal: 16,
    marginBottom: 8,
  },
  avatar: {
    width: 48,
    height: 48,
    borderRadius: 24,
  },
  initialsContainer: {
    width: 48,
    height: 48,
    borderRadius: 24,
    backgroundColor: 'rgba(60, 131, 246, 0.2)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  initialsText: {
    color: '#3c83f6',
    fontWeight: 'bold',
  },
  iconContainer: {
    width: 48,
    height: 48,
    borderRadius: 12,
    backgroundColor: 'rgba(60, 131, 246, 0.1)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  resultContent: {
    flex: 1,
    marginLeft: 16,
  },
  resultTitle: {
    color: 'white',
    fontWeight: '600',
  },
  resultSubtitle: {
    color: '#94a3b8',
    fontSize: 12,
  },
  notes: {
    color: '#3c83f6',
    fontSize: 12,
    marginTop: 4,
  },
  price: {
    color: '#3c83f6',
    fontWeight: 'bold',
  },
});

export default GlobalSearchScreen;
