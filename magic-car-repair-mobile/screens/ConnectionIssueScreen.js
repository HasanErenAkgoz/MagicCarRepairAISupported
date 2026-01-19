
import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet, SafeAreaView } from 'react-native';
import { Feather } from '@expo/vector-icons';

const ConnectionIssueScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="x" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Status</Text>
        <View style={{ width: 40 }} />
      </View>

      <View style={styles.container}>
        <View style={styles.illustrationContainer}>
          <View style={styles.outerCircle}>
            <Feather name="tool" size={96} color="#3c83f6" />
          </View>
          <View style={styles.wifiIconContainer}>
            <Feather name="wifi-off" size={48} color="#3c83f6" />
          </View>
        </View>

        <Text style={styles.title}>Connection Issue</Text>
        <Text style={styles.subtitle}>
          Could not connect to the server. Please check your internet connection and try again.
        </Text>

        <TouchableOpacity style={styles.primaryButton}>
          <Text style={styles.primaryButtonText}>Try Again</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.secondaryButton}>
          <Text style={styles.secondaryButtonText}>Check Internet Settings</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

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
    fontSize: 16,
    fontWeight: '600',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 24,
  },
  illustrationContainer: {
    alignItems: 'center',
    marginBottom: 32,
  },
  outerCircle: {
    width: 192,
    height: 192,
    borderRadius: 96,
    backgroundColor: 'rgba(60, 131, 246, 0.1)',
    alignItems: 'center',
    justifyContent: 'center',
  },
  wifiIconContainer: {
    position: 'absolute',
    top: -8,
    right: -8,
    backgroundColor: '#101722',
    borderRadius: 30,
    padding: 12,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
    textAlign: 'center',
    marginBottom: 12,
  },
  subtitle: {
    fontSize: 16,
    color: '#94a3b8',
    textAlign: 'center',
    maxWidth: 320,
    marginBottom: 32,
  },
  primaryButton: {
    width: '100%',
    backgroundColor: '#3c83f6',
    borderRadius: 12,
    paddingVertical: 16,
    alignItems: 'center',
    marginBottom: 16,
  },
  primaryButtonText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: 'white',
  },
  secondaryButton: {
    width: '100%',
    borderColor: '#3c83f6',
    borderWidth: 1,
    borderRadius: 12,
    paddingVertical: 16,
    alignItems: 'center',
  },
  secondaryButtonText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#3c83f6',
  },
});

export default ConnectionIssueScreen;
