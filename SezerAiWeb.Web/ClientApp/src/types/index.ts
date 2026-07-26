export interface Platform {
  id: string;
  name: string;
  slug: string;
  description: string;
  iconUrl?: string;
  iconBg?: string;
  icon?: string;
  status: 'online' | 'offline' | 'maintenance';
}

export interface Message {
  id: string;
  phoneNumber: string;
  content: string;
  platforms: ('whatsapp' | 'telegram')[];
  sentAt: Date;
  status: 'pending' | 'sent' | 'failed';
}

export interface ChatMessage {
  id: string;
  role: 'user' | 'assistant';
  content: string;
  timestamp: Date;
}

export interface SystemMetric {
  label: string;
  value: string | number;
  change?: number;
  icon: string;
}

export interface AIAgent {
  id: string;
  name: string;
  status: 'active' | 'idle' | 'offline';
  tasksCompleted: number;
  accuracy: number;
}

export interface TrafficData {
  time: string;
  visits: number;
  pageViews: number;
}

export interface Notification {
  id: string;
  title: string;
  message: string;
  type: 'info' | 'success' | 'warning' | 'error';
  timestamp: Date;
}
