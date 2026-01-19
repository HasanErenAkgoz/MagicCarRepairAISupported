
import React, { useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView } from 'react-native';
import { Feather } from '@expo/vector-icons';

const ServiceCalendarScreen = () => {
  const [selectedDate, setSelectedDate] = useState(24);
  const days = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
  const dates = Array.from({ length: 31 }, (_, i) => i + 1);

  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}><Feather name="menu" size={24} color="white" /></TouchableOpacity>
        <Text style={styles.headerTitle}>Schedule</Text>
        <TouchableOpacity style={styles.iconButton}><Feather name="plus" size={24} color="#3c83f6" /></TouchableOpacity>
      </View>

      <View style={styles.calendarContainer}>
        <View style={styles.monthSelector}>
            <TouchableOpacity><Feather name="chevron-left" size={20} color="#94a3b8" /></TouchableOpacity>
            <Text style={styles.monthText}>October 2023</Text>
            <TouchableOpacity><Feather name="chevron-right" size={20} color="#94a3b8" /></TouchableOpacity>
        </View>
        <View style={styles.daysHeader}>
            {days.map(day => <Text key={day} style={styles.dayText}>{day}</Text>)}
        </View>
        <View style={styles.datesGrid}>
            {dates.map(date => (
                <TouchableOpacity
                    key={date}
                    style={[styles.date, selectedDate === date && styles.selectedDate]}
                    onPress={() => setSelectedDate(date)}
                >
                    <Text style={selectedDate === date ? styles.selectedDateText : styles.dateText}>{date}</Text>
                </TouchableOpacity>
            ))}
        </View>
      </View>

      <ScrollView style={styles.appointmentsList}>
        <Text style={styles.listHeader}>Wednesday, Oct 24</Text>
        <AppointmentCard
            time="09:00"
            period="AM"
            title="Oil Change & Tire Rotation"
            vehicle="2018 Ford F-150"
            customer="John Doe"
            status="In Service"
            icon="tool"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

const AppointmentCard = ({ time, period, title, vehicle, customer, status, icon }) => (
    <View style={styles.card}>
        <View style={styles.timeInfo}>
            <Text style={styles.timeText}>{time}</Text>
            <Text style={styles.periodText}>{period}</Text>
        </View>
        <View style={styles.cardContent}>
            <View style={styles.cardIcon}>
                <Feather name={icon} size={20} color="#3c83f6" />
            </View>
            <View>
                <Text style={styles.cardTitle}>{title}</Text>
                <Text style={styles.cardSubtitle}>{vehicle} • {customer}</Text>
            </View>
            <Text style={styles.status}>{status}</Text>
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
    justifyContent: 'space-between',
    alignItems: 'center',
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
  calendarContainer: {
    padding: 16,
    backgroundColor: '#101722',
    borderBottomWidth: 1,
    borderBottomColor: 'rgba(255,255,255,0.1)'
  },
  monthSelector: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 16,
  },
  monthText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
  daysHeader: {
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
  dayText: {
    color: '#94a3b8',
    fontSize: 12,
  },
  datesGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-around',
    marginTop: 8,
  },
  date: {
    width: '14%',
    alignItems: 'center',
    padding: 8,
  },
  dateText: {
    color: 'white',
  },
  selectedDate: {
    backgroundColor: '#3c83f6',
    borderRadius: 20,
  },
  selectedDateText: {
    color: 'white',
    fontWeight: 'bold',
  },
  appointmentsList: {
    flex: 1,
  },
  listHeader: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
    padding: 16,
  },
  card: {
    flexDirection: 'row',
    backgroundColor: '#1e293b',
    borderRadius: 12,
    marginHorizontal: 16,
    marginBottom: 12,
    padding: 16,
  },
  timeInfo: {
    alignItems: 'center',
    marginRight: 16,
  },
  timeText: {
    color: 'white',
    fontWeight: 'bold',
    fontSize: 16,
  },
  periodText: {
    color: '#94a3b8',
    fontSize: 12,
  },
  cardContent: {
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
  },
  cardIcon: {
      backgroundColor: 'rgba(60, 131, 246, 0.1)',
      padding: 8,
      borderRadius: 8,
      marginRight: 12,
  },
  cardTitle: {
      color: 'white',
      fontWeight: '600',
  },
  cardSubtitle: {
      color: '#94a3b8',
      fontSize: 12,
  },
  status: {
    backgroundColor: 'rgba(60, 131, 246, 0.1)',
    color: '#3c83f6',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    fontSize: 12,
    marginLeft: 'auto',
  },
});

export default ServiceCalendarScreen;
