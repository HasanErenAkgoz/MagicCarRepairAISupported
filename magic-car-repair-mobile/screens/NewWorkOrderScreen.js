
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView } from 'react-native';
import { Feather } from '@expo/vector-icons';

const NewWorkOrderScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="x" size={24} color="white" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>New Work Order</Text>
        <TouchableOpacity>
          <Text style={styles.draftButton}>Draft</Text>
        </TouchableOpacity>
      </View>

      <View style={styles.stepper}>
        <Text style={styles.stepText}>Step 1 of 3</Text>
        <View style={styles.stepIndicatorContainer}>
          <View style={[styles.stepIndicator, styles.activeStep]} />
          <View style={styles.stepIndicator} />
          <View style={styles.stepIndicator} />
        </View>
      </View>

      <ScrollView style={styles.scrollView}>
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Vehicle Information</Text>
          <TextInput style={styles.input} placeholder="VIN" placeholderTextColor="#94a3b8" />
          <View style={styles.row}>
            <TextInput style={[styles.input, styles.halfInput]} placeholder="Year" placeholderTextColor="#94a3b8" />
            <TextInput style={[styles.input, styles.halfInput]} placeholder="Make" placeholderTextColor="#94a3b8" />
          </View>
          <TextInput style={styles.input} placeholder="Model" placeholderTextColor="#94a3b8" />
        </View>

        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Service Details</Text>
          <TextInput style={styles.input} placeholder="Service Type" placeholderTextColor="#94a3b8" />
          <TextInput style={styles.input} placeholder="Preferred Drop-off" placeholderTextColor="#94a3b8" />
          <TextInput
            style={[styles.input, styles.textArea]}
            placeholder="Issue Description"
            placeholderTextColor="#94a3b8"
            multiline
          />
        </View>
      </ScrollView>

      <View style={styles.footer}>
        <TouchableOpacity style={styles.continueButton}>
          <Text style={styles.continueButtonText}>Continue to Customer</Text>
          <Feather name="arrow-right" size={20} color="white" />
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
  draftButton: {
    color: '#3c83f6',
    fontWeight: '600',
  },
  stepper: {
    padding: 16,
  },
  stepText: {
    color: '#3c83f6',
    fontSize: 12,
    marginBottom: 8,
  },
  stepIndicatorContainer: {
    flexDirection: 'row',
  },
  stepIndicator: {
    flex: 1,
    height: 4,
    backgroundColor: '#334155',
    borderRadius: 2,
    marginHorizontal: 2,
  },
  activeStep: {
    backgroundColor: '#3c83f6',
  },
  scrollView: {
    paddingHorizontal: 16,
  },
  section: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    padding: 16,
    marginBottom: 16,
  },
  sectionTitle: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  input: {
    backgroundColor: '#101722',
    borderRadius: 8,
    padding: 12,
    color: 'white',
    marginBottom: 12,
    borderWidth: 1,
    borderColor: '#334155',
  },
  row: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  halfInput: {
    width: '48%',
  },
  textArea: {
    height: 100,
    textAlignVertical: 'top',
  },
  footer: {
    padding: 16,
    borderTopWidth: 1,
    borderTopColor: 'rgba(255, 255, 255, 0.1)',
  },
  continueButton: {
    backgroundColor: '#3c83f6',
    borderRadius: 12,
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
  },
  continueButtonText: {
    color: 'white',
    fontWeight: 'bold',
    marginRight: 8,
  },
});

export default NewWorkOrderScreen;
