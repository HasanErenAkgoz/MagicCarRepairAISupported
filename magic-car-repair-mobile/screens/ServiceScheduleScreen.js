
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView } from 'react-native';
import { Feather } from '@expo/vector-icons';

const ServiceScheduleScreen = () => {
  const calendarDays = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
  const dates = Array.from({ length: 31 }, (_, i) => i + 1);

  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="arrow-left" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Schedule</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="settings" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <ScrollView>
        <View style={styles.calendarContainer}>
          <View style={styles.monthSelector}>
            <TouchableOpacity>
              <Feather name="chevron-left" size={24} color="white" />
            </TouchableOpacity>
            <Text style={styles.monthText}>October 2023</Text>
            <TouchableOpacity>
              <Feather name="chevron-right" size={24} color="white" />
            </TouchableOpacity>
          </View>

          <View style={styles.calendarGrid}>
            {calendarDays.map((day, index) => (
              <Text key={index} style={styles.dayHeader}>{day}</Text>
            ))}
            {dates.map((date) => (
              <TouchableOpacity key={date} style={[styles.dateButton, date === 24 && styles.selectedDate]}>
                <Text style={date === 24 ? styles.selectedDateText : styles.dateText}>{date}</Text>
              </TouchableOpacity>
            ))}
          </View>
        </View>

        <View style={styles.appointmentsContainer}>
          <Text style={styles.appointmentsTitle}>Today, Oct 24</Text>

          <AppointmentCard
            time="09:00"
            period="AM"
            customer="John Doe"
            service="Oil Change"
            vehicle="2018 Ford F-150"
            color="#3b82f6"
          />
          <AppointmentCard
            time="11:30"
            period="AM"
            customer="Sarah Smith"
            service="Tire Rotation"
            vehicle="2021 Tesla Model 3"
            color="#10b981"
          />
          <AppointmentCard
            time="02:00"
            period="PM"
            customer="Mike Ross"
            service="Brake Service"
            vehicle="2015 BMW 328i"
            color="#64748b"
          />
        </View>
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={28} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const AppointmentCard = ({ time, period, customer, service, vehicle, color }) => (
  <View style={[styles.card, { borderLeftColor: color, borderLeftWidth: 4 }]}>
    <View style={styles.timeContainer}>
      <Text style={styles.timeText}>{time}</Text>
      <Text style={[styles.periodText, { color }]}>{period}</Text>
    </View>
    <View style={styles.cardDetails}>
      <View style={styles.cardHeader}>
        <Text style={styles.customerName}>{customer}</Text>
        <Text style={[styles.serviceTag, { backgroundColor: `${color}20`, color }]}>{service}</Text>
      </View>
      <View style={styles.vehicleInfo}>
        <Feather name="truck" size={16} color="#94a3b8" />
        <Text style={styles.vehicleText}>{vehicle}</Text>
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
  calendarContainer: {
    padding: 16,
  },
  monthSelector: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16,
  },
  monthText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  calendarGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-between',
  },
  dayHeader: {
    width: '14%',
    textAlign: 'center',
    color: '#94a3b8',
    marginBottom: 8,
  },
  dateButton: {
    width: '14%',
    alignItems: 'center',
    justifyContent: 'center',
    height: 40,
  },
  dateText: {
    color: 'white',
  },
  selectedDate: {
    backgroundColor: '#3b82f6',
    borderRadius: 20,
  },
  selectedDateText: {
    color: 'white',
    fontWeight: 'bold',
  },
  appointmentsContainer: {
    padding: 16,
  },
  appointmentsTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
    marginBottom: 16,
  },
  card: {
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 16,
    flexDirection: 'row',
    marginBottom: 16,
  },
  timeContainer: {
    alignItems: 'center',
    marginRight: 16,
  },
  timeText: {
    fontSize: 20,
    fontWeight: 'bold',
    color: 'white',
  },
  periodText: {
    fontSize: 12,
  },
  cardDetails: {
    flex: 1,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-start',
    marginBottom: 8,
  },
  customerName: {
    fontSize: 16,
    fontWeight: '600',
    color: 'white',
  },
  serviceTag: {
    fontSize: 12,
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 8,
  },
  vehicleInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  vehicleText: {
    color: '#94a3b8',
    marginLeft: 8,
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    width: 56,
    height: 56,
    borderRadius: 28,
    backgroundColor: '#3b82f6',
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#3b82f6',
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.5,
    shadowRadius: 8,
    elevation: 5,
  },
});

export default ServiceScheduleScreen;
