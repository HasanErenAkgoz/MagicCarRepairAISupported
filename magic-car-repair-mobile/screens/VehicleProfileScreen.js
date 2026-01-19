
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, ImageBackground } from 'react-native';
import { Feather } from '@expo/vector-icons';

const VehicleProfileScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Vehicle Details</Text>
        <TouchableOpacity>
          <Text style={styles.editButton}>Edit</Text>
        </TouchableOpacity>
      </View>

      <ScrollView>
        <ImageBackground
          source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAwhFJ3-GnWaBvuEhZhCxSYMT9JAGxzFX_2YrbrDtGX-IQUZ9aZeTEtNRPMWP2fQXWj_aXWqreXx4VTJR0vP7-ALmeRpCnttT-2W6EDg7dGIhlHbGtAh2FV1tLQphtW8Fv_VE6C3WcaKtQMIFq2zzLWcPoVl7yOqDcUkVIBYtirQHMwruPVPJk-IIrTZz6XNRM0QTjdIJ1lFvDSuvnAQh7RE3MoQU_TjiGJVXYtp--wC9hfqvZnwQDvsbP8FSouH1N0dxvhwZRPEHM9' }}
          style={styles.vehicleImage}
        >
          <View style={styles.imageOverlay}>
            <Text style={styles.licensePlate}>ABC-1234</Text>
            <Text style={styles.vehicleModel}>2023 Tesla Model 3</Text>
          </View>
        </ImageBackground>

        <View style={styles.quickActions}>
          <QuickActionButton icon="calendar" text="Book Service" />
          <QuickActionButton icon="alert-triangle" text="Report Issue" />
          <QuickActionButton icon="file-text" text="View Docs" />
          <QuickActionButton icon="phone" text="Contact Driver" />
        </View>

        <Text style={styles.sectionTitle}>Specifications</Text>
        <View style={styles.specsGrid}>
          <SpecCard label="VIN" value="5YJ3E1EA8JF..." />
          <SpecCard label="Odometer" value="42,300 km" />
          <SpecCard label="Engine" value="Dual Motor" />
          <SpecCard label="Year" value="2023" />
        </View>

        <Text style={styles.sectionTitle}>History</Text>
        <View style={styles.timeline}>
          <TimelineItem date="Today" service="Tire Rotation" status="Scheduled" isFirst />
          <TimelineItem date="Last Month" service="Brake Inspection" status="Completed" />
          <TimelineItem date="3 Months Ago" service="Annual Maintenance" status="Completed" />
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const QuickActionButton = ({ icon, text }) => (
  <TouchableOpacity style={styles.actionButton}>
    <Feather name={icon} size={24} color="#3c83f6" />
    <Text style={styles.actionText}>{text}</Text>
  </TouchableOpacity>
);

const SpecCard = ({ label, value }) => (
  <View style={styles.specCard}>
    <Text style={styles.specLabel}>{label}</Text>
    <Text style={styles.specValue}>{value}</Text>
  </View>
);

const TimelineItem = ({ date, service, status, isFirst }) => (
  <View style={styles.timelineItem}>
    <View style={styles.timelineMarkerContainer}>
      <View style={[styles.timelineMarker, isFirst && styles.firstMarker]} />
    </View>
    <View style={styles.timelineContent}>
      <Text style={styles.timelineDate}>{date}</Text>
      <View style={styles.timelineCard}>
        <View>
          <Text style={styles.timelineService}>{service}</Text>
          <Text style={styles.timelineStatus}>{status}</Text>
        </View>
        <Feather name={status === 'Completed' ? 'check-circle' : 'chevron-right'} size={20} color={status === 'Completed' ? '#22c55e' : '#94a3b8'} />
      </View>
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
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(255, 255, 255, 0.1)',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  editButton: {
    color: '#3c83f6',
    fontWeight: 'bold',
  },
  vehicleImage: {
    height: 200,
    justifyContent: 'flex-end',
    padding: 16,
  },
  imageOverlay: {
    backgroundColor: 'rgba(0,0,0,0.4)',
  },
  licensePlate: {
    color: 'white',
    backgroundColor: '#3c83f6',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    alignSelf: 'flex-start',
    fontWeight: 'bold',
  },
  vehicleModel: {
    color: 'white',
    fontSize: 24,
    fontWeight: 'bold',
    marginTop: 8,
  },
  quickActions: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    padding: 16,
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
    paddingHorizontal: 16,
    marginBottom: 16,
  },
  specsGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
  },
  specCard: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    padding: 16,
    width: '48%',
    marginBottom: 16,
  },
  specLabel: {
    color: '#94a3b8',
    fontSize: 12,
  },
  specValue: {
    color: 'white',
    fontSize: 16,
    fontWeight: '600',
  },
  timeline: {
    paddingHorizontal: 16,
  },
  timelineItem: {
    flexDirection: 'row',
  },
  timelineMarkerContainer: {
    alignItems: 'center',
    width: 20,
  },
  timelineMarker: {
    width: 12,
    height: 12,
    borderRadius: 6,
    backgroundColor: '#3c83f6',
    position: 'absolute',
    top: 4,
    left: -5,
  },
  firstMarker: {
    backgroundColor: '#3c83f6',
  },
  timelineContent: {
    flex: 1,
    paddingLeft: 16,
    borderLeftWidth: 1,
    borderLeftColor: 'rgba(255,255,255,0.1)',
  },
  timelineDate: {
    color: '#94a3b8',
    marginBottom: 8,
  },
  timelineCard: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    padding: 16,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16,
  },
  timelineService: {
    color: 'white',
    fontWeight: 'bold',
  },
  timelineStatus: {
    color: '#94a3b8',
  },
});

export default VehicleProfileScreen;
