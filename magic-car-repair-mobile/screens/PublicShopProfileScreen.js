
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const PublicShopProfileScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Shop Profile</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="share-2" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.content}>
        <View style={styles.profileHeader}>
          <Image
            source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuDePZ9SHjruFbGmYUZ4E1u7PNDLs5j5MrVBlueZLogsSRSziRgKqBptenSXacjZk1UMymuX7b2NIpmIrowUhXTmdnDSLtyLam17Ji4AxAx_CmmfkgAybCOA3FB0PIJsy5QKCoGY8iZrF-xS4CHi1ECfeDBEc2ISJaudQf3gTd1t3vTDlasB-vcPgUDZeJhQ2x74KU8mcOFfkLuhetkKob_k3A_8p2y_PLXgRrOa0eJWD-GYN6s2scWsYwCJMmPE2slgQNkmdW_Ppqq8Cp' }}
            style={styles.logo}
          />
          <Text style={styles.shopName}>Precision Auto Care</Text>
          <Text style={styles.shopLocation}>San Francisco, CA • <Text style={styles.openStatus}>Open Now</Text></Text>
          <View style={styles.rating}>
            <Feather name="star" size={16} color="#facc15" />
            <Text style={styles.ratingText}>4.8 (124 Reviews)</Text>
          </View>
        </View>

        <View style={styles.actions}>
          <ActionButton icon="phone" text="Call" />
          <ActionButton icon="map-pin" text="Directions" />
          <ActionButton icon="globe" text="Website" />
        </View>

        <Text style={styles.sectionTitle}>Popular Services</Text>
        <ServiceItem name="Synthetic Oil Change" price="$49" icon="droplet" />
        <ServiceItem name="Brake Service" price="$120" icon="tool" />
        <ServiceItem name="Battery Replacement" price="$80" icon="battery-charging" />

        <Text style={styles.sectionTitle}>About Us</Text>
        <Text style={styles.aboutText}>
          Precision Auto Care has been serving the community for over 20 years. Our certified mechanics use state-of-the-art diagnostic equipment to get you back on the road safely and quickly.
        </Text>
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="calendar" size={24} color="white" style={{marginRight: 8}}/>
        <Text style={styles.fabText}>Book Appointment</Text>
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const ActionButton = ({ icon, text }) => (
  <TouchableOpacity style={styles.actionButton}>
    <Feather name={icon} size={24} color="#3c83f6" />
    <Text style={styles.actionText}>{text}</Text>
  </TouchableOpacity>
);

const ServiceItem = ({ name, price, icon }) => (
  <View style={styles.serviceItem}>
    <Feather name={icon} size={24} color="#3c83f6" />
    <View style={styles.serviceDetails}>
      <Text style={styles.serviceName}>{name}</Text>
    </View>
    <Text style={styles.servicePrice}>{price}</Text>
  </View>
);

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
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(255,255,255,0.1)',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  content: {
    padding: 16,
  },
  profileHeader: {
    alignItems: 'center',
  },
  logo: {
    width: 128,
    height: 128,
    borderRadius: 64,
    borderWidth: 4,
    borderColor: 'rgba(255,255,255,0.1)',
  },
  shopName: {
    color: 'white',
    fontSize: 24,
    fontWeight: 'bold',
    marginTop: 16,
  },
  shopLocation: {
    color: '#94a3b8',
  },
  openStatus: {
    color: '#22c55e',
  },
  rating: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 8,
  },
  ratingText: {
    color: '#facc15',
    marginLeft: 4,
  },
  actions: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    marginVertical: 24,
  },
  actionButton: {
    alignItems: 'center',
  },
  actionText: {
    color: 'white',
    marginTop: 8,
  },
  sectionTitle: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  serviceItem: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 12,
  },
  serviceDetails: {
    flex: 1,
    marginLeft: 16,
  },
  serviceName: {
    color: 'white',
    fontWeight: '500',
  },
  servicePrice: {
    color: '#3c83f6',
    fontWeight: 'bold',
  },
  aboutText: {
    color: '#cbd5e1',
    lineHeight: 22,
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    left: 24,
    right: 24,
    backgroundColor: '#3c83f6',
    borderRadius: 12,
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
  },
  fabText: {
    color: 'white',
    fontWeight: 'bold',
  },
});

export default PublicShopProfileScreen;
