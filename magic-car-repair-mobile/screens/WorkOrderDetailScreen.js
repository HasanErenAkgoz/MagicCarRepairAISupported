
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, ImageBackground } from 'react-native';
import { Feather } from '@expo/vector-icons';

const WorkOrderDetailScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Work Order #4921</Text>
        <TouchableOpacity>
          <Text style={styles.editButton}>Edit</Text>
        </TouchableOpacity>
      </View>

      <ScrollView>
        <ImageBackground
          source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCABQrQ_RJmHqexW9hZccoq5sIS2joYltYzIZXD97my2FctPD6XUovE77tScMHwmvVuSyKkFolRPNoRvbJ46Qf6snPMTKeQgw9rXpxWQKHJJEeRj1RJ93BclXPyBhtV02SW0RYtDJdftcGsaSz0D8r4IaNqtR1zvBb95V01-1WH30q5TwusOC8Nb-FMClwiqQTlypV14vlURlmalQgcGWriE3-SI0Z2SWnxdI8drFELsAVFvVQyvAoII_jrPcAGRwB7gLahWXDw-p3A' }}
          style={styles.vehicleImage}
          resizeMode="cover"
        >
          <View style={styles.imageOverlay} />
        </ImageBackground>

        <View style={styles.detailsContainer}>
          <Text style={styles.vehicleModel}>2018 Ford F-150</Text>
          <Text style={styles.licensePlate}>CA • 7NUM832</Text>

          <View style={styles.tabs}>
            <TouchableOpacity style={[styles.tab, styles.activeTab]}>
              <Text style={styles.activeTabText}>Summary</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.tab}>
              <Text style={styles.tabText}>Parts</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.tab}>
              <Text style={styles.tabText}>Photos</Text>
            </TouchableOpacity>
          </View>

          <DetailSection title="Customer Details">
            <CustomerCard name="Alex Morgan" since="2021" />
          </DetailSection>

          <DetailSection title="Vehicle Specifications">
            <SpecRow icon="hash" label="VIN" value="1FTEW1CP5KFA8921" />
            <SpecRow icon="activity" label="Odometer" value="84,392 mi" />
            <SpecRow icon="settings" label="Engine" value="3.5L V6 EcoBoost" />
          </DetailSection>

          <DetailSection title="Technician Notes">
            <Text style={styles.notes}>
              Customer reports a rattling noise from the front suspension. Initial inspection suggests worn sway bar links. Brakes look good, approx 60% life remaining.
            </Text>
          </DetailSection>
        </View>
      </ScrollView>
      <ActionButtons />
    </SafeAreaView>
  );
};

const DetailSection = ({ title, children }) => (
  <View style={styles.section}>
    <Text style={styles.sectionTitle}>{title}</Text>
    {children}
  </View>
);

const CustomerCard = ({ name, since }) => (
  <View style={styles.customerCard}>
    <View style={styles.avatar}>
      <Text style={styles.avatarText}>AM</Text>
    </View>
    <View>
      <Text style={styles.customerName}>{name}</Text>
      <Text style={styles.customerSince}>Customer since {since}</Text>
    </View>
  </View>
);

const SpecRow = ({ icon, label, value }) => (
  <View style={styles.specRow}>
    <Feather name={icon} size={20} color="#90a7cb" />
    <Text style={styles.specLabel}>{label}</Text>
    <Text style={styles.specValue}>{value}</Text>
  </View>
);

const ActionButtons = () => (
  <View style={styles.actionsContainer}>
    <TouchableOpacity style={styles.secondaryButton}>
      <Feather name="plus" size={20} color="#3c83f6" />
      <Text style={styles.secondaryButtonText}>Add Part</Text>
    </TouchableOpacity>
    <TouchableOpacity style={styles.primaryButton}>
      <Feather name="check-circle" size={20} color="white" />
      <Text style={styles.primaryButtonText}>Complete Job</Text>
    </TouchableOpacity>
  </View>
);

const styles = StyleSheet.create({
  safeArea: { flex: 1, backgroundColor: '#101723' },
  header: { flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', padding: 16, borderBottomWidth: 1, borderBottomColor: 'rgba(255, 255, 255, 0.1)' },
  headerTitle: { fontSize: 18, fontWeight: 'bold', color: 'white' },
  iconButton: { padding: 8 },
  editButton: { color: '#3c83f6', fontWeight: 'bold' },
  vehicleImage: { height: 220, justifyContent: 'flex-end' },
  imageOverlay: { flex: 1, backgroundColor: 'rgba(16, 23, 35, 0.4)' },
  detailsContainer: { padding: 16 },
  vehicleModel: { fontSize: 28, fontWeight: 'bold', color: 'white' },
  licensePlate: { fontSize: 32, fontWeight: 'bold', color: 'white', marginBottom: 16 },
  tabs: { flexDirection: 'row', borderBottomWidth: 1, borderBottomColor: '#314668', marginBottom: 16 },
  tab: { flex: 1, alignItems: 'center', paddingVertical: 12 },
  activeTab: { borderBottomWidth: 3, borderBottomColor: '#3c83f6' },
  tabText: { color: '#90a7cb' },
  activeTabText: { color: '#3c83f6', fontWeight: 'bold' },
  section: { marginBottom: 24 },
  sectionTitle: { color: 'white', fontSize: 16, fontWeight: 'bold', marginBottom: 12 },
  customerCard: { flexDirection: 'row', alignItems: 'center', backgroundColor: '#1c2635', borderRadius: 12, padding: 12 },
  avatar: { width: 48, height: 48, borderRadius: 24, backgroundColor: 'rgba(60, 131, 246, 0.2)', alignItems: 'center', justifyContent: 'center', marginRight: 12 },
  avatarText: { color: '#3c83f6', fontWeight: 'bold', fontSize: 18 },
  customerName: { color: 'white', fontSize: 18, fontWeight: 'bold' },
  customerSince: { color: '#90a7cb' },
  specRow: { flexDirection: 'row', alignItems: 'center', borderBottomWidth: 1, borderBottomColor: 'rgba(255, 255, 255, 0.1)', paddingVertical: 12 },
  specLabel: { color: '#90a7cb', marginLeft: 12, flex: 1 },
  specValue: { color: 'white', fontFamily: 'monospace' },
  notes: { color: '#cbd5e1', lineHeight: 20 },
  actionsContainer: { flexDirection: 'row', padding: 16, borderTopWidth: 1, borderTopColor: 'rgba(255, 255, 255, 0.1)', backgroundColor: '#101723' },
  secondaryButton: { flex: 1, flexDirection: 'row', alignItems: 'center', justifyContent: 'center', padding: 16, borderRadius: 12, borderWidth: 1, borderColor: '#3c83f6', marginRight: 8 },
  secondaryButtonText: { color: '#3c83f6', fontWeight: 'bold', marginLeft: 8 },
  primaryButton: { flex: 2, flexDirection: 'row', alignItems: 'center', justifyContent: 'center', padding: 16, borderRadius: 12, backgroundColor: '#3c83f6' },
  primaryButtonText: { color: 'white', fontWeight: 'bold', marginLeft: 8 },
});

export default WorkOrderDetailScreen;
