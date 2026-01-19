
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const MessagesScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Messages</Text>
        <TouchableOpacity style={styles.editButton}>
          <Feather name="edit" size={24} color="#3c83f6" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search client or vehicle..."
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView style={styles.scrollView}>
        <MessageItem
          name="Sarah Jenkins"
          vehicle="2020 Honda Civic"
          message="Is the oil change finished yet?"
          time="2m ago"
          unreadCount={2}
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuAV1Ttwby8RmzkIMl8_ruNmqusGng1cBjPZ1zB7ju1a-FKc-e6oMuIWOi0AadmmrFG-r2Mwyqey1CKAXmoeGRDrEQ48Yvy65_gW1ydfn-k4mymZ7izAq-JP87YUg243SnoGlM1Fv20OWcl5oAU5o5NbgQMOKV5dJ6uug-DJGsQ54ZKc-RCCVLqHs7w-W-lVIISugnuES6tSkjvTbj9UuQLdrW7TC1nictCptFYcwWA-pcPFnnNRy1kfJPDrYhQ5LfU7jmhMTfH1NzWm"
        />
        <MessageItem
          name="Mike Ross"
          vehicle="2015 BMW X5"
          message="Parts ordered. We will update you..."
          time="1h ago"
          isRead
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuCjk-loeH8dT91nf9c9sO9RIFK_cmwHuW6gf0n9EQGtpNrQJQ4PD1PMf-f5vUmpbJ70wRQ-o6Meb__lQOp26PYZdgUg_IK-oCh_49G3vczjbNpcXMLE6CmEhVxjwcFM80d-pPAX9WjT22bDJrwxDmtZ5-ILoOFTXFDn7crtKEgwCnmDi07b1yDmW44ef7fjnhRLPDCsJjLSydeUi6Y-6tknVzjnYQuVrdFQZRDODPad_LhZRDzqxhXEH3wB843WYx3eBQIoPDLoHjUN"
        />
        <MessageItem
          name="AutoZone Parts"
          vehicle="Commercial Account"
          message="Invoice #4492 attached."
          time="Yesterday"
          hasAttachment
        />
        <MessageItem
          name="David Chen"
          vehicle="2018 Ford F-150"
          message="Can I drop it off around 5pm?"
          time="Yesterday"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBk4jMAnUe5S_9QNLxclXkuKZwwJDswvYM7nAotrZyb-7VD6N4CUqvYuDoPaPfubaLyyYYcfKv1yi7CZdGwa7vySnnQFSTK304Vw_9glM91D0wPmDGQV0HcREaijW7iHtlGOvxR3CFbe5CwXGYoamcVu3ilaABPjj92yUGgYKeQ3SJq59BX_b8wj2mQXq2MFwzWwoANn6CAQuDbM_KjCcKhhsN7hFV20ADupQ3wFHsF14sQjzY4vTfQNWyWKTcSH8PZAkFDMWFPdDcD"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

const MessageItem = ({ name, vehicle, message, time, unreadCount, isRead, hasAttachment, avatarUrl }) => (
  <TouchableOpacity style={styles.messageItem}>
    {avatarUrl ? (
      <Image source={{ uri: avatarUrl }} style={styles.avatar} />
    ) : (
      <View style={styles.avatarInitials}>
        <Text style={styles.initialsText}>{name.substring(0, 2).toUpperCase()}</Text>
      </View>
    )}
    <View style={styles.messageContent}>
      <View style={styles.messageHeader}>
        <Text style={styles.name}>{name}</Text>
        <Text style={styles.time}>{time}</Text>
      </View>
      <Text style={styles.vehicle}>{vehicle}</Text>
      <View style={styles.messageFooter}>
        {hasAttachment && <Feather name="paperclip" size={14} color="#94a3b8" />}
        <Text style={styles.messageText} numberOfLines={1}>{message}</Text>
        {unreadCount > 0 && (
          <View style={styles.unreadBadge}>
            <Text style={styles.unreadText}>{unreadCount}</Text>
          </View>
        )}
        {isRead && <Feather name="check-circle" size={16} color="#3c83f6" />}
      </View>
    </View>
  </TouchableOpacity>
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
  },
  headerTitle: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
  editButton: {
    padding: 8,
  },
  searchContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#1e293b',
    borderRadius: 12,
    marginHorizontal: 16,
    paddingHorizontal: 12,
  },
  searchIcon: {
    marginRight: 8,
  },
  searchInput: {
    flex: 1,
    height: 48,
    color: 'white',
  },
  scrollView: {
    padding: 16,
  },
  messageItem: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  avatar: {
    width: 56,
    height: 56,
    borderRadius: 28,
  },
  avatarInitials: {
    width: 56,
    height: 56,
    borderRadius: 28,
    backgroundColor: '#1e293b',
    alignItems: 'center',
    justifyContent: 'center',
  },
  initialsText: {
    color: 'white',
    fontWeight: 'bold',
  },
  messageContent: {
    flex: 1,
    marginLeft: 16,
  },
  messageHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  name: {
    fontSize: 16,
    fontWeight: '600',
    color: 'white',
  },
  time: {
    fontSize: 12,
    color: '#94a3b8',
  },
  vehicle: {
    fontSize: 12,
    color: '#94a3b8',
    marginBottom: 4,
  },
  messageFooter: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  messageText: {
    flex: 1,
    color: '#cbd5e1',
    marginLeft: 4,
  },
  unreadBadge: {
    backgroundColor: '#3c83f6',
    borderRadius: 10,
    paddingHorizontal: 6,
    paddingVertical: 2,
    marginLeft: 'auto',
  },
  unreadText: {
    color: 'white',
    fontSize: 10,
    fontWeight: 'bold',
  },
});

export default MessagesScreen;
