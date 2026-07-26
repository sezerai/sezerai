import { useState } from 'react';
import { useDashboardStore } from '../store/useDashboardStore';

export function ChatbotSection() {
  const [input, setInput] = useState('');
  const { chatMessages, addChatMessage } = useDashboardStore();

  const handleSend = () => {
    if (!input.trim()) return;

    // Add user message
    addChatMessage({
      id: Date.now().toString(),
      role: 'user',
      content: input,
      timestamp: new Date()
    });

    // Mock AI response
    setTimeout(() => {
      addChatMessage({
        id: (Date.now() + 1).toString(),
        role: 'assistant',
        content: 'Anladım, sizin için bu bilgiyi işliyorum... (Mock yanıt)',
        timestamp: new Date()
      });
    }, 1000);

    setInput('');
  };

  return (
    <div className="flex-1 glass rounded-panel-radius p-6 mac-shadow flex flex-col max-h-[600px]">
      <div className="flex items-center gap-2 mb-6">
        <div className="w-2 h-2 rounded-full bg-primary animate-pulse"></div>
        <span className="font-title-sm text-title-sm text-on-surface">SEZER AI Asistan</span>
      </div>

      {/* Chat Messages */}
      <div className="space-y-4 mb-6 flex-1 overflow-y-auto custom-scroll">
        {chatMessages.map((msg) => (
          <div key={msg.id} className="text-on-surface">
            <p className="font-body-md text-body-md opacity-90">
              {msg.role === 'assistant' ? '🤖 ' : '👤 '}
              {msg.content}
            </p>
          </div>
        ))}
      </div>

      {/* Quick Actions */}
      <div className="flex flex-wrap gap-2 mb-4">
        <button
          className="glass-dark px-3 py-2 rounded-full font-label-md text-label-md hover:bg-primary/10 transition-colors"
          onClick={() => setInput('Son 24 saatteki metrikleri göster')}
        >
          📊 Metrikler
        </button>
        <button
          className="glass-dark px-3 py-2 rounded-full font-label-md text-label-md hover:bg-primary/10 transition-colors"
          onClick={() => setInput('Sistem sağlığını kontrol et')}
        >
          ❤️ Sağlık
        </button>
        <button
          className="glass-dark px-3 py-2 rounded-full font-label-md text-label-md hover:bg-primary/10 transition-colors"
          onClick={() => setInput('AI ajanlarının durumu nedir?')}
        >
          🤖 AI Ajanlar
        </button>
      </div>

      {/* Input */}
      <div className="flex gap-2">
        <input
          type="text"
          className="flex-1 glass-dark border-none rounded-full py-2 px-4 focus:ring-2 focus:ring-primary/20 text-body-md font-body-md"
          placeholder="Bir şey sorun..."
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && handleSend()}
        />
        <button
          className="px-4 py-2 rounded-full bg-primary text-white hover:opacity-90 transition-opacity"
          onClick={handleSend}
        >
          <span className="material-symbols-outlined">send</span>
        </button>
      </div>
    </div>
  );
}
