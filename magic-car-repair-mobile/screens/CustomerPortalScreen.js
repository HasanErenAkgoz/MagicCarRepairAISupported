
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const CustomerPortalScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <View style={styles.userInfo}>
          <Image
            source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCctS_zQCQM4z9vPQLvfCIE10vG-Eaz16S6ilVVEV9ksSBJe0Tdc-M_A4VBoSzUi_yHgmGHPtWhQJyevdeOgBMFx_e-b7eCFF_TzhfhAHO0m-OHS9gfENJIExSi6r-F987gsp3f-adCC9rpCTvNPy7TfGu7wgkRo83-SGvNwdxHnvHvruxh3mNOTC7OSYnS3OmqW0nmigPf7RExMKKJmsOCPC11jHaNZNxg7Al8YHCv_FjN9AD_KgVK89DIb1A_887FqCUTMF_CqLOX' }}
            style={styles.avatar}
          />
          <View>
            <Text style={styles.welcomeText}>Welcome back,</Text>
            <Text style={styles.userName}>Sarah</Text>
          </View>
        </View>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="bell" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <ScrollView contentContainerStyle={styles.content}>
        <View style={styles.mainCard}>
          <Text style={styles.mainCardTitle}>Your Vehicle's Status</Text>
          <View style={styles.vehicleInfo}>
            <Feather name="truck" size={20} color="white" />
            <Text style={styles.vehicleName}>2021 Tesla Model 3</Text>
          </View>
          <View style={styles.progressBar}>
            <View style={styles.progress} />
          </View>
          <Text style={styles.progressText}>Repair Completion: 65%</Text>
        </View>

        <View style={styles.grid}>
          <GridButton icon="tool" text="Active Orders" />
          <GridButton icon="calendar" text="Appointments" />
          <GridButton icon="file-text" text="Quotes" />
          <GridButton icon="clock" text="History" />
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const GridButton = ({ icon, text }) => (
  <TouchableOpacity style={styles.gridButton}>
    <Feather name={icon} size={24} color="#3b82f6" />
    <Text style={styles.gridButtonText}>{text}</Text>
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
  mainCard: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 16,
    marginBottom: 24,
  },
  mainCardTitle: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  vehicleInfo: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 16,
  },
  vehicleName: {
    color: 'white',
    marginLeft: 8,
  },
  progressBar: {
    height: 10,
    backgroundColor: '#334155',
    borderRadius: 5,
    marginTop: 16,
  },
  progress: {
    width: '65%',
    height: '100%',
    backgroundColor: '#3b82f6',
    borderRadius: 5,
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
});

export default CustomerPortalScreen;
