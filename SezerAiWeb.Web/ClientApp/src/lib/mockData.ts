import type { Platform, SystemMetric, AIAgent, TrafficData, ChatMessage } from '../types';

export const mockPlatforms: Platform[] = [
  {
    id: '1',
    name: 'AI Hospital',
    slug: 'ai-hospital',
    description: 'Sağlık Platformu',
    iconUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBP1B0BM7nW6IVIrK3X24p1JHlrLSc3AwP9nBJANakbZfdKhYctReaJV9FVbuNMTDCDCKqyn6JA8cvydBpfebTFJX4N6qoKfkJpfCO_53Nkxs2aJZ0wioiQUqtPAaOyQ_0vHorvwRlV1f6QXPnV_wA7X2F7OTPCFnvO6Rct2bSIlaZ9UKDu7z_XRKNh9nbq1ee4Szzel_L4zqQv5J5OSclk5D9Tu4xNJAmdz0TiG3woQsha-F6S0CxBDcyHXhfLq_8Uk1Wb-ggZQA',
    status: 'online'
  },
  {
    id: '2',
    name: 'AiBazaar',
    slug: 'aibazaar',
    description: 'E-Ticaret Platformu',
    iconUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuC_dKfSmLsO87PwpVSq9g6FXVNeiQCuytSr8VNCpXAKxu45PibznfZdVoyxSrsfsHhYbSP7wELGk_bB2N5nNqm1xUOy5CgIz-JmG2lrRkBUbMiKsCDA4Lm82WGuMNKeS_UK6X39Mxr7ffue78CZDznfUivV-BD_w99T6P_snh4_d9j8vOk5dZ-Zk1TLURsriX3kBKFeI1Z_ttdonC5RDZAdnyb6vkE2eEMwgR_lMyFLGnYc4rf3kKHlfsyuzmyGCqdVXv0Sv8hZRw',
    status: 'online'
  },
  {
    id: '3',
    name: 'Geliyoo',
    slug: 'geliyoo',
    description: 'Hizmet Platformu',
    iconUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuBJBuZBFxLMU_iN8NdbwT9-eNSAMRoUSoTlFuzfqAbHxINiVD8xx0ovTb6zKWooW8bWApgHsYQrgfZ8CfbyhEVKUCQY2xLYnuTi5xkgHVaM0ORHjFD_DXIHugaLPj5K11YF2S7Iyhs_D2QoNyIVeSiyXdyQGI4yXFUdjTOQMlzmjKJPZwcJXXj2eGitXtaGhpNDNiv-lijQUiskPgtdmtKicH1p--O-KGFCHRXwnasz1-shRsNeK4C3OyVsvUh0KHgtCc98AcxrJQ',
    status: 'online'
  },
  {
    id: '4',
    name: 'TR-AI',
    slug: 'tr-ai',
    description: 'Al Teknoloji',
    iconBg: '#001452',
    icon: 'neurology',
    status: 'online'
  },
  {
    id: '5',
    name: 'Perde',
    slug: 'perde',
    description: 'Perde Platformu',
    iconBg: '#554300',
    icon: 'curtains',
    status: 'online'
  }
];

export const mockSystemMetrics: SystemMetric[] = [
  {
    label: 'Toplam Ziyaret',
    value: '1.2M',
    change: 12.5,
    icon: 'visibility'
  },
  {
    label: 'Aktif Kullanıcı',
    value: '45.8K',
    change: 8.3,
    icon: 'people'
  },
  {
    label: 'AI İşlem',
    value: '892K',
    change: 23.1,
    icon: 'psychology'
  },
  {
    label: 'Sistem Sağlığı',
    value: '99.9%',
    change: 0.1,
    icon: 'health_and_safety'
  }
];

export const mockAIAgents: AIAgent[] = [
  {
    id: '1',
    name: 'Robot Hemşire',
    status: 'active',
    tasksCompleted: 1247,
    accuracy: 98.5
  },
  {
    id: '2',
    name: 'E-Ticaret Asistan',
    status: 'active',
    tasksCompleted: 3421,
    accuracy: 96.2
  },
  {
    id: '3',
    name: 'Hizmet Planlayıcı',
    status: 'idle',
    tasksCompleted: 892,
    accuracy: 94.8
  },
  {
    id: '4',
    name: 'Veri Analiz AI',
    status: 'active',
    tasksCompleted: 5678,
    accuracy: 99.1
  }
];

export const mockTrafficData: TrafficData[] = [
  { time: '00:00', visits: 1200, pageViews: 3400 },
  { time: '04:00', visits: 800, pageViews: 2100 },
  { time: '08:00', visits: 2400, pageViews: 6200 },
  { time: '12:00', visits: 4200, pageViews: 11200 },
  { time: '16:00', visits: 3800, pageViews: 9800 },
  { time: '20:00', visits: 3200, pageViews: 8400 },
  { time: '23:59', visits: 1800, pageViews: 4800 }
];

export const mockChatMessages: ChatMessage[] = [
  {
    id: '1',
    role: 'assistant',
    content: 'Merhaba Sezer, bugün size nasıl yardımcı olabilirim?',
    timestamp: new Date(Date.now() - 1000 * 60 * 5)
  },
  {
    id: '2',
    role: 'user',
    content: 'AI Hospital platformundaki son metrikleri göster',
    timestamp: new Date(Date.now() - 1000 * 60 * 4)
  },
  {
    id: '3',
    role: 'assistant',
    content: 'AI Hospital platformunda son 24 saatte 12,345 hasta kaydı işlendi. Robot Hemşire 98.5% doğruluk oranıyla çalışıyor.',
    timestamp: new Date(Date.now() - 1000 * 60 * 3)
  }
];

export const messageTemplates = [
  { icon: '🎉', title: 'Tebrik' },
  { icon: '⏰', title: 'Hatırlatma' },
  { icon: '🎁', title: 'Promosyon' },
  { icon: '💡', title: 'Bilgi' },
  { icon: '⚡', title: 'Uyarı' },
  { icon: '✅', title: 'Başarı' },
  { icon: '🔗', title: 'Link' },
  { icon: '📅', title: 'Randevu' },
  { icon: '💰', title: 'Kampanya' },
  { icon: '💬', title: 'Destek' }
];
