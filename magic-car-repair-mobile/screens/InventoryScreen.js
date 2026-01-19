
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const InventoryScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Inventory</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="settings" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search part # or name..."
          placeholderTextColor="#94a3b8"
        />
        <TouchableOpacity style={styles.scanButton}>
          <Feather name="camera" size={20} color="#3c83f6" />
        </TouchableOpacity>
      </View>

      <ScrollView horizontal showsHorizontalScrollIndicator={false} style={styles.filters}>
        <TouchableOpacity style={[styles.filterButton, styles.activeFilter]}>
          <Text style={styles.activeFilterText}>All</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Engine</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Brakes</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.filterButton}>
          <Text style={styles.filterText}>Electrical</Text>
        </TouchableOpacity>
      </ScrollView>

      <ScrollView contentContainerStyle={styles.content}>
        <PartCard
          name="Oil Filter - Bosch 3323"
          code="3323-BF"
          price="$12.99"
          stock="45"
          imageUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuDRbR6Vwn74utC7rYzyx1s7vaa6mt4Zyx2j65c9xno1fV8L42xDg-Qo0D2gW3y3GTijB8D8qlZpsfpehmZQ9nAwZes3Ut4WvHHrS_dE6r7rQtTowUvTnZ50NYBYkyQge04higLJ_WhuSq4cq-FbfpcfQ_dSnqyNbKAREXc7uaQCpB4IHHXW5vAhCmW8TD0mWv2e1v70oNmBm6oyjI49tunI1W6kfUtlIbO2Ik4oOGtdhucu7pX-qG3_WhAcpOlqgCA9fmjClz-j6yU"
        />
        <PartCard
          name="Brake Pad Set - Ceramic"
          code="BP-CER-001"
          price="$55.00"
          stock="12"
          imageUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBhjjgmDEijiKGh3pndEXpR63zJ4AfHAJJV31NdyPCJEPvFhghy78HUZJuSfz_oagjCCo5exdMRxtQ3TZqAG10mlTiCkMkWFHjc7hUl4-AxV5ogKbgHmxcnd8giZ37E0HL2719BkCf8TSsslreNBcZzx40ipciijRVCx5fW8KM1JyiloaHIf6S0qvRcXlenAQZtKKAKNC88r-FFbPsdu8USA0Hl8jgy4Qcs6eV_4XlA4EG5qPyncwCGcAYZUNsnrk0YklHwkENvsXjK"
        />
        <PartCard
          name="Spark Plug - Iridium"
          code="SP-IR-99"
          price="$8.50"
          stock="8"
          stockStatus="Low"
          imageUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBfuvmqIdkI4j-rq6TpSB9YI8feMOLkvxHH6JFxcDIm4VAaprb9yNOx1u2Oq6vcDfidRDybq1No8ftLH9BXT-XU36D_fO2Rl4rtJXewHhRrjUCbo-4qwJSsY9M-Bqae8QnPecWEhdcrUUVVCZnE_K9YWK2JPm-FvK7-RcQj56ylGGbt8E5lR85NfEOPZdoOZhJU20Xm1i7wH5AKQLtTEtErHWkfHS8VduKYzgV96-Iy5F9Ir9_T42qiBNrsU4fjUkny1RhOocGnbLYf"
        />
      </ScrollView>

      <TouchableOpacity style={styles.fab}>
        <Feather name="plus" size={28} color="white" />
      </TouchableOpacity>
    </SafeAreaView>
  );
};

const PartCard = ({ name, code, price, stock, stockStatus, imageUrl }) => {
    const getStockStyle = () => {
        if (stockStatus === 'Low') return { backgroundColor: 'rgba(249, 115, 22, 0.1)', color: '#F97316' };
        return { backgroundColor: 'rgba(34, 197, 94, 0.1)', color: '#22C55E' };
    }

    return (
        <View style={styles.card}>
            <Image source={{ uri: imageUrl }} style={styles.partImage} />
            <View style={styles.cardContent}>
                <View style={styles.cardHeader}>
                    <Text style={styles.partName}>{name}</Text>
                    <Text style={styles.price}>{price}</Text>
                </View>
                <Text style={styles.partCode}>{code}</Text>
                <View style={styles.cardFooter}>
                    <View style={[styles.stockBadge, getStockStyle()]}>
                        <Text style={[styles.stockText, {color: getStockStyle().color}]}>Stock: {stock}</Text>
                    </View>
                </View>
            </View>
        </View>
    );
}


const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#101723',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
    padding: 8,
  },
  searchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(15, 23, 42, 0.4)',
    borderRadius: 12,
    marginHorizontal: 16,
    paddingHorizontal: 12,
    borderWidth: 1,
    borderColor: 'rgba(255, 255, 255, 0.05)',
  },
  searchIcon: {
    marginRight: 8,
  },
  searchInput: {
    flex: 1,
    height: 48,
    color: 'white',
  },
  scanButton: {
    padding: 8,
  },
  filters: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    marginTop: 16,
  },
  filterButton: {
    paddingVertical: 8,
    paddingHorizontal: 16,
    borderRadius: 20,
    backgroundColor: 'rgba(30, 41, 59, 0.6)',
    marginRight: 8,
    borderWidth: 1,
    borderColor: 'rgba(255, 255, 255, 0.08)',
  },
  activeFilter: {
    backgroundColor: '#3c83f6',
  },
  filterText: {
    color: '#cbd5e1',
  },
  activeFilterText: {
    color: 'white',
    fontWeight: 'bold',
  },
  content: {
    padding: 16,
  },
  card: {
    backgroundColor: 'rgba(30, 41, 59, 0.6)',
    borderRadius: 12,
    padding: 12,
    flexDirection: 'row',
    marginBottom: 16,
    borderWidth: 1,
    borderColor: 'rgba(255, 255, 255, 0.08)',
  },
  partImage: {
    width: 84,
    height: 84,
    borderRadius: 8,
  },
  cardContent: {
    flex: 1,
    marginLeft: 12,
  },
  cardHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  partName: {
    color: 'white',
    fontWeight: '600',
    flex: 1,
  },
  price: {
    color: 'white',
    fontWeight: 'bold',
  },
  partCode: {
    color: '#94a3b8',
    fontSize: 12,
    fontFamily: 'monospace',
  },
  cardFooter: {
    marginTop: 'auto',
  },
  stockBadge: {
    paddingHorizontal: 8,
    paddingVertical: 4,
    borderRadius: 6,
    alignSelf: 'flex-start',
  },
  stockText: {
    fontSize: 12,
    fontWeight: '500',
  },
  fab: {
    position: 'absolute',
    bottom: 32,
    right: 24,
    width: 56,
    height: 56,
    borderRadius: 28,
    backgroundColor: '#3c83f6',
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default InventoryScreen;
