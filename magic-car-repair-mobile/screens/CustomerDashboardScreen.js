
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, ImageBackground, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';
import * as Progress from 'react-native-progress';

const CustomerDashboardScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <View style={styles.userInfo}>
          <Image
            source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBoA0W_BLiIj16ogfjroXgZQysm8CF0lmxn9QIx2rUmihCbaKqik3XRz_2IrHOirHTU1MarHOXU9gBq0moN0FEYLWABCSlvNEv1QBL0zLndmQRohwV9L-DUF9CK2wun3XNH137e-5sfoLWXq-TM-rSxMtFet9VQhdh6OxgjMr59maSj-Ly-JRwYTrWGAyvdeJv9v7VTS00ouCCsWt5gALHX56wtMLsOBavS3cteAo6Sa10-WkfkXNdcEuyFcVCT5qWXTmBGFXMtGLF6' }}
            style={styles.avatar}
          />
          <View>
            <Text style={styles.welcomeText}>Welcome back</Text>
            <Text style={styles.userName}>Good Morning, Sarah</Text>
          </View>
        </View>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="bell" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.content}>
        <ImageBackground
          source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAynBC1UOOnJJ3NRWGdcP9eriwEu7m_PzjClWKiyu6yapJkUkZ3ijv_Lw1uO8LLSxXikjDnUytupmuvOc0WeY-Vio3jOlj5C39s6xcSLlFRntFZDMMAhTlVGgAb16ORICdQ81AgTNtlp3TWVEPperLhXLCZX1dkGdTzrQk17tKfze5rRmbhuiO-7qSaWc_0OLTcSAEaOP-embYtwTfQdpf7XxTHfDqF8aDSb_-1PXBCTDe68P82l9gx8zz9yYA4EbFmuG_wlu107Qbi' }}
          style={styles.vehicleCard}
        >
          <View style={styles.vehicleOverlay}>
            <Text style={styles.vehicleName}>2021 Tesla Model 3</Text>
            <View style={styles.statusContainer}>
              <View style={styles.statusDot} />
              <Text style={styles.statusText}>Service in Progress</Text>
            </View>
            <Progress.Bar
              progress={0.65}
              width={null}
              color="#3b82f6"
              unfilledColor="rgba(255, 255, 255, 0.2)"
              borderWidth={0}
              style={styles.progressBar}
            />
            <Text style={styles.progressText}>Estimated completion: Tomorrow by 5:00 PM</Text>
          </View>
        </ImageBackground>

        <View style={styles.grid}>
          <GridButton icon="tool" text="Active Orders" notification="1" />
          <GridButton icon="calendar" text="Appointments" />
          <GridButton icon="file-text" text="Quotes" notification="2" />
          <GridButton icon="clock" text="History" />
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const GridButton = ({ icon, text, notification }) => (
  <TouchableOpacity style={styles.gridButton}>
    <Feather name={icon} size={24} color="#3b82f6" />
    <Text style={styles.gridButtonText}>{text}</Text>
    {notification && (
      <View style={styles.notificationBadge}>
        <Text style={styles.notificationText}>{notification}</Text>
      </View>
    )}
  </TouchableOpacity>
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
  userInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    marginRight: 12,
  },
  welcomeText: {
    color: '#cbd5e1',
    fontSize: 12,
  },
  userName: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
  iconButton: {
    padding: 8,
  },
  content: {
    padding: 16,
  },
  vehicleCard: {
    height: 200,
    borderRadius: 16,
    overflow: 'hidden',
    justifyContent: 'flex-end',
    padding: 16,
  },
  vehicleOverlay: {
    backgroundColor: 'rgba(0,0,0,0.5)',
  },
  vehicleName: {
    color: 'white',
    fontSize: 24,
    fontWeight: 'bold',
  },
  statusContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 8,
  },
  statusDot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#22c55e',
    marginRight: 8,
  },
  statusText: {
    color: 'white',
  },
  progressBar: {
    marginTop: 16,
  },
  progressText: {
    color: '#cbd5e1',
    fontSize: 12,
    marginTop: 8,
  },
  grid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
    marginTop: 24,
  },
  gridButton: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    width: '48%',
    padding: 16,
    marginBottom: 16,
    alignItems: 'center',
  },
  gridButtonText: {
    color: 'white',
    marginTop: 8,
    fontWeight: '500',
  },
  notificationBadge: {
    position: 'absolute',
    top: 12,
    right: 12,
    backgroundColor: '#3b82f6',
    borderRadius: 10,
    width: 20,
    height: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  notificationText: {
    color: 'white',
    fontSize: 12,
    fontWeight: 'bold',
  },
});

export default CustomerDashboardScreen;
