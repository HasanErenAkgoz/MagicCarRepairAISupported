
import React from 'react';
import { View, Text, StyleSheet, ScrollView, TouchableOpacity, Image, Dimensions } from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import { useNavigation } from '@react-navigation/native';
import { LineChart, ProgressChart } from 'react-native-chart-kit';

const DashboardScreen = () => {
  const navigation = useNavigation();
  return (
    <View style={styles.container}>
      <LinearGradient colors={['#0f172a', '#1e3a8a']} style={styles.background} />
      <ScrollView showsVerticalScrollIndicator={false}>
        <View style={styles.header}>
          <View style={styles.headerLeft}>
            <Image
              source={{ uri: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBo-cSRo0W4CS10xYmippWvnMbbpw4g27rLHQDaCysObf6r9pm7YgXTVy-FtvoafsX9bdWioIT5HieaPiuCbu-CKvqE30KYAcRAE8OrrAI5YFr0mkS-1q3NouKAfNNnzFVyxfyvFHRDarSBFL1ikkHwlzUvmnUV4w3Q2VN9VNNcpBc_d6DmJJQSJR2IpHYL0XXtut-ZvbDVI5SYzpCFPTZF_8p2y_PLXgRrOa0eJWD-GYN6s2scWsYwCJMmPE2slgQNkmdW_Ppqq8Cp' }}
              style={styles.avatar}
            />
            <View>
              <Text style={styles.welcomeText}>Welcome back</Text>
              <Text style={styles.userName}>Good Morning, Alex</Text>
            </View>
          </View>
          <TouchableOpacity style={styles.notificationButton}>
            <Text style={styles.notificationIcon}>🔔</Text>
            <View style={styles.notificationBadge} />
          </TouchableOpacity>
        </View>

        <View style={styles.statusContainer}>
          <View style={styles.statusBadge}>
            <View style={styles.statusIndicator} />
            <Text style={styles.statusText}>Shop Floor Operational</Text>
          </View>
        </View>

        <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.cardsContainer}>
          <TouchableOpacity
            style={styles.card}
            onPress={() => navigation.navigate('Customers')}
          >
            <Text style={styles.cardTitle}>Total</Text>
            <Text style={styles.cardValue}>142</Text>
            <Text style={styles.cardSubtitle}>Work Orders</Text>
          </TouchableOpacity>
          <View style={[styles.card, styles.activeCard]}>
            <Text style={styles.cardTitle}>Active</Text>
            <Text style={styles.cardValue}>18</Text>
            <Text style={styles.cardSubtitle}>In Progress</Text>
          </View>
          <View style={styles.card}>
            <Text style={styles.cardTitle}>Revenue</Text>
            <Text style={styles.cardValue}>$2.4k</Text>
            <Text style={styles.cardSubtitle}>Today's Revenue</Text>
          </View>
          <View style={styles.card}>
            <Text style={styles.cardTitle}>Pending</Text>
            <Text style={styles.cardValue}>4</Text>
            <Text style={styles.cardSubtitle}>Pending Approval</Text>
          </View>
        </ScrollView>

        <View style={styles.chartContainer}>
          <Text style={styles.chartTitle}>Revenue Analytics</Text>
          <LineChart
            data={{
              labels: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
              datasets: [
                {
                  data: [
                    Math.random() * 100,
                    Math.random() * 100,
                    Math.random() * 100,
                    Math.random() * 100,
                    Math.random() * 100,
                    Math.random() * 100,
                    Math.random() * 100,
                  ]
                }
              ]
            }}
            width={Dimensions.get("window").width - 40}
            height={220}
            yAxisLabel="$"
            yAxisSuffix="k"
            chartConfig={{
              backgroundColor: "#1e293b",
              backgroundGradientFrom: "#1e293b",
              backgroundGradientTo: "#1e293b",
              decimalPlaces: 2,
              color: (opacity = 1) => `rgba(59, 130, 246, ${opacity})`,
              labelColor: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
              style: {
                borderRadius: 16
              },
              propsForDots: {
                r: "6",
                strokeWidth: "2",
                stroke: "#3B82F6"
              }
            }}
            bezier
            style={{
              marginVertical: 8,
              borderRadius: 16
            }}
          />
        </View>

        <View style={styles.chartContainer}>
          <Text style={styles.chartTitle}>Fleet Status</Text>
          <ProgressChart
            data={{
              labels: ["Repairing", "Completed", "Waiting"], // optional
              data: [0.4, 0.6, 0.8]
            }}
            width={Dimensions.get("window").width - 40}
            height={220}
            strokeWidth={16}
            radius={32}
            chartConfig={{
              backgroundColor: "#1e293b",
              backgroundGradientFrom: "#1e293b",
              backgroundGradientTo: "#1e293b",
              color: (opacity = 1) => `rgba(59, 130, 246, ${opacity})`,
            }}
            hideLegend={false}
          />
        </View>
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  background: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    height: '100%',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingTop: 60,
    paddingHorizontal: 20,
    paddingBottom: 20,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  avatar: {
    width: 48,
    height: 48,
    borderRadius: 24,
    marginRight: 16,
  },
  welcomeText: {
    color: 'rgba(255, 255, 255, 0.6)',
    fontSize: 12,
    textTransform: 'uppercase',
  },
  userName: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  notificationButton: {
    position: 'relative',
  },
  notificationIcon: {
    fontSize: 24,
  },
  notificationBadge: {
    position: 'absolute',
    top: 2,
    right: 2,
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#3B82F6',
  },
  statusContainer: {
    paddingHorizontal: 20,
    marginBottom: 20,
  },
  statusBadge: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(16, 185, 129, 0.1)',
    borderRadius: 20,
    paddingHorizontal: 12,
    paddingVertical: 6,
    alignSelf: 'flex-start',
  },
  statusIndicator: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: '#10B981',
    marginRight: 8,
  },
  statusText: {
    color: '#10B981',
    fontSize: 12,
    fontWeight: '600',
    textTransform: 'uppercase',
  },
  cardsContainer: {
    paddingLeft: 20,
    marginBottom: 20,
  },
  card: {
    backgroundColor: 'rgba(255, 255, 255, 0.05)',
    borderRadius: 16,
    padding: 20,
    marginRight: 16,
    width: 160,
    justifyContent: 'space-between',
  },
  activeCard: {
    backgroundColor: 'rgba(59, 130, 246, 0.15)',
    borderColor: 'rgba(59, 130, 246, 0.3)',
    borderWidth: 1,
  },
  cardTitle: {
    color: 'rgba(255, 255, 255, 0.7)',
    fontSize: 12,
  },
  cardValue: {
    color: 'white',
    fontSize: 32,
    fontWeight: 'bold',
    marginTop: 8,
  },
  cardSubtitle: {
    color: 'rgba(255, 255, 255, 0.5)',
    fontSize: 12,
    marginTop: 4,
  },
});

export default DashboardScreen;
