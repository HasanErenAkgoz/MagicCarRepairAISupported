
import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, SafeAreaView, ScrollView, Image } from 'react-native';
import { Feather } from '@expo/vector-icons';

const MessagesSupportScreen = () => {
  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Messages</Text>
        <TouchableOpacity style={styles.iconButton}>
          <Feather name="edit" size={24} color="white" />
        </TouchableOpacity>
      </View>

      <View style={styles.searchContainer}>
        <Feather name="search" size={20} color="#94a3b8" style={styles.searchIcon} />
        <TextInput
          style={styles.searchInput}
          placeholder="Search conversations..."
          placeholderTextColor="#94a3b8"
        />
      </View>

      <ScrollView contentContainerStyle={styles.content}>
        <MessageThread
          name="Sarah from Support"
          lastMessage="Your ticket #492 has been resolved."
          time="10:42 AM"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuBng7lmLkHIdaW59-Zryd7vVxTYAeYHDZJ7NCO-cVxqhDjHZEVCL_PcR_Q3P0SC1CAkLWAPKMiY8IFTmLzG1O-LdxnmeMKXrL8ciJhhEQAheZJ7YgoNRxulJRLo_HzZiIATZgEQuKGCPkdqbLwibXCLUNDlj_M0SJRDQ0h_e7gufwCImo6G9FJVDtNqRhtDcuWE5Qxq_0sKG_Wf-kaVwYuWm4LXEnUgm87IrWZJ7-2aT9PqUgAsgniH_KSu1xcofRr9QzjBMAVpn5cs"
          unreadCount={2}
          isOnline
        />
        <MessageThread
          name="John Doe"
          lastMessage="Thanks for the update!"
          time="Yesterday"
          avatarUrl="https://lh3.googleusercontent.com/aida-public/AB6AXuAg81DY5PUVySRdVmvBBcUlOiQfD3VvvnT2EDuj2TNKbWH0ThKCEQg4iNZBkknSkBf4fmKnlwQ6TYe1LtQ7yfykfKl9zP3IaQIJ1OnVUe0dRYqiWRb0UZVSkbTZN33jTMUhI4ILPWCecLrb-ouFMhzrlooqig9ftfwoFRyTBiRkfDzggxsndEnQky6dX9wldCaETkrNqAm6hamVZrm95HzkGB2qTkEP3CIhi-yEN1b63bpChkIxmvC28LPAtKzcz5iol22G6mZv4sJs"
        />
      </ScrollView>
    </SafeAreaView>
  );
};

const MessageThread = ({ name, lastMessage, time, avatarUrl, unreadCount, isOnline }) => (
  <TouchableOpacity style={styles.thread}>
    <View>
      <Image source={{ uri: avatarUrl }} style={styles.avatar} />
      {isOnline && <View style={styles.onlineIndicator} />}
    </View>
    <View style={styles.threadContent}>
      <View style={styles.threadHeader}>
        <Text style={styles.threadName}>{name}</Text>
        <Text style={styles.threadTime}>{time}</Text>
      </View>
      <View style={styles.threadMessage}>
        <Text style={styles.lastMessage} numberOfLines={1}>{lastMessage}</Text>
        {unreadCount > 0 && (
          <View style={styles.unreadBadge}>
            <Text style={styles.unreadCount}>{unreadCount}</Text>
          </View>
        )}
      </View>
    </View>
  </TouchableOpacity>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#0f172a',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 16,
  },
  headerTitle: {
    fontSize: 28,
    fontWeight: 'bold',
    color: 'white',
  },
  iconButton: {
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
  content: {
    padding: 16,
  },
  thread: {
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  avatar: {
    width: 56,
    height: 56,
    borderRadius: 28,
  },
  onlineIndicator: {
    position: 'absolute',
    bottom: 0,
    right: 0,
    width: 14,
    height: 14,
    borderRadius: 7,
    backgroundColor: '#22c55e',
    borderWidth: 2,
    borderColor: '#0f172a',
  },
  threadContent: {
    flex: 1,
    marginLeft: 16,
  },
  threadHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  threadName: {
    color: 'white',
    fontWeight: '600',
  },
  threadTime: {
    color: '#94a3b8',
    fontSize: 12,
  },
  threadMessage: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 4,
  },
  lastMessage: {
    color: '#94a3b8',
    flex: 1,
  },
  unreadBadge: {
    backgroundColor: '#3B82F6',
    borderRadius: 10,
    width: 20,
    height: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  unreadCount: {
    color: 'white',
    fontSize: 12,
    fontWeight: 'bold',
  },
});

export default MessagesSupportScreen;
