import { create } from 'zustand';
import type { Platform, Notification, ChatMessage } from '../types';
import { mockPlatforms, mockChatMessages } from '../lib/mockData';

interface DashboardState {
  platforms: Platform[];
  notifications: Notification[];
  chatMessages: ChatMessage[];
  isMessagingPanelOpen: boolean;
  selectedPlatforms: string[];

  // Actions
  setPlatforms: (platforms: Platform[]) => void;
  addNotification: (notification: Notification) => void;
  addChatMessage: (message: ChatMessage) => void;
  toggleMessagingPanel: () => void;
  togglePlatformSelection: (platformId: string) => void;
}

export const useDashboardStore = create<DashboardState>((set) => ({
  platforms: mockPlatforms,
  notifications: [],
  chatMessages: mockChatMessages,
  isMessagingPanelOpen: false,
  selectedPlatforms: [],

  setPlatforms: (platforms) => set({ platforms }),

  addNotification: (notification) =>
    set((state) => ({
      notifications: [notification, ...state.notifications].slice(0, 50)
    })),

  addChatMessage: (message) =>
    set((state) => ({
      chatMessages: [...state.chatMessages, message]
    })),

  toggleMessagingPanel: () =>
    set((state) => ({ isMessagingPanelOpen: !state.isMessagingPanelOpen })),

  togglePlatformSelection: (platformId) =>
    set((state) => ({
      selectedPlatforms: state.selectedPlatforms.includes(platformId)
        ? state.selectedPlatforms.filter(id => id !== platformId)
        : [...state.selectedPlatforms, platformId]
    })),
}));
