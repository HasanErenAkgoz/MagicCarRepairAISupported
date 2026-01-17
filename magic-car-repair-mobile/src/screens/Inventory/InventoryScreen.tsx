
import React from 'react';
import { View, Text, StyleSheet, ScrollView, TextInput, TouchableOpacity, Image } from 'react-native';

const InventoryScreen = () => {
  const parts = [
    {
      name: 'Alternator Belt',
      code: 'AB-9920',
      price: 12.50,
      status: 'In Stock',
      quantity: 45,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuC87ncYpOi3HoTcQhGkGqJ4jrRXqclr1Nr3Q8-u-uR5jmjkHdFjkTELRxXCyCLflfJLlP3vbaH8KFdGrPGY3t1Vy6WZp1eNT1cOU5zy47hdUCoTSMVY13wxS7ZuEZQfGwz0xJkiZD46-PEvP-1pqWj3Yeeqg_kvueDEgy_6nXeL9zvOYBV_1BgG2KnTCZDmKPkg8HYIevoQ306KbMN7gTuvA1ygVJINMIdv1LCQQFdad0zkqDwXR8iYMBKRC8H0n9DoJLzqHMVvau8r',
    },
    {
      name: 'Hydraulic Pump',
      code: 'HP-X200',
      price: 450.00,
      status: 'Out of Stock',
      quantity: 0,
      image: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAus36rfw0j8reU3Q3cgRzfewOdZjLZVGIhvul_hQRZsq00sBwK0n-0JIrocuzGXxtMnmaRZNm-iAdU6IkE2oDfUlidEIf3lqVepenXCVkswrlvPSg1bCl5Lf7GfzREbCmbr1FqIWw0To0fGwDTk_Pox-9-I16JnWN-DC5JErJ5KjYCFjiiOn9wGFfqjhfQGpvzdj2dp7Mb0xTMPGvrGaU1SLA7qO_NYdOz-xatbl3WzapfJFEE5ILCgTW1F74luuK2sFIKXeGX6q88',
    },
  ];

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Parts Inventory</Text>
      </View>
      <View style={styles.searchContainer}>
        <TextInput
          style={styles.searchInput}
          placeholder="Search code, name..."
          placeholderTextColor="#94a3b8"
        />
      </View>
      <ScrollView contentContainerStyle={styles.listContainer}>
        {parts.map((part, index) => (
          <View key={index} style={styles.card}>
            <Image source={{ uri: part.image }} style={styles.partImage} />
            <View style={styles.partInfo}>
              <View>
                <Text style={styles.partName}>{part.name}</Text>
                <Text style={styles.partCode}>{part.code}</Text>
              </View>
              <View style={styles.partFooter}>
                <Text style={[styles.status, styles[`status${part.status.replace(/\s+/g, '')}`]]}>{part.status}</Text>
                <Text style={styles.quantity}>Qty: {part.quantity}</Text>
              </View>
            </View>
            <Text style={styles.price}>${part.price.toFixed(2)}</Text>
          </View>
        ))}
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  header: {
    paddingTop: 60,
    paddingHorizontal: 20,
    paddingBottom: 10,
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
  searchContainer: {
    paddingHorizontal: 20,
    paddingVertical: 10,
  },
  searchInput: {
    backgroundColor: '#1e293b',
    borderRadius: 12,
    paddingHorizontal: 16,
    paddingVertical: 14,
    color: 'white',
    fontSize: 16,
  },
  listContainer: {
    paddingHorizontal: 20,
    paddingBottom: 20,
  },
  card: {
    flexDirection: 'row',
    backgroundColor: '#1e293b',
    borderRadius: 16,
    padding: 12,
    marginBottom: 16,
    alignItems: 'center',
  },
  partImage: {
    width: 80,
    height: 80,
    borderRadius: 12,
  },
  partInfo: {
    flex: 1,
    marginLeft: 12,
    justifyContent: 'space-between',
    height: 80,
  },
  partName: {
    color: 'white',
    fontWeight: 'bold',
  },
  partCode: {
    color: '#94a3b8',
    fontSize: 12,
    marginTop: 4,
  },
  partFooter: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  status: {
    fontSize: 12,
    fontWeight: '600',
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 12,
    overflow: 'hidden',
  },
  statusInStock: {
    backgroundColor: 'rgba(16, 185, 129, 0.2)',
    color: '#10B981',
  },
  statusOutofStock: {
    backgroundColor: 'rgba(239, 68, 68, 0.2)',
    color: '#EF4444',
  },
  quantity: {
    color: '#94a3b8',
    fontSize: 12,
  },
  price: {
    color: '#3B82F6',
    fontSize: 16,
    fontWeight: 'bold',
    marginLeft: 12,
  },
});

export default InventoryScreen;
