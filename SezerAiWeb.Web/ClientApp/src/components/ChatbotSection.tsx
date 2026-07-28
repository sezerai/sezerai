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
    <div className="flex-1 glass rounded-panel-radius p-4 sm:p-6 mac-shadow flex flex-col h-full max-h-[500px] sm:max-h-[600px]">
      <div className="flex items-center gap-2 mb-2 sm:mb-3">
        <div className="w-2 h-2 rounded-full bg-primary animate-pulse"></div>
        <span className="font-title-sm text-title-sm text-on-surface">SEZER AI Asistan</span>
      </div>

      {/* Chat Messages */}
      <div className="space-y-2 sm:space-y-3 mb-2 sm:mb-3 flex-1 overflow-y-auto custom-scroll min-h-0">
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
      <div className="flex flex-wrap gap-2 mb-2 sm:mb-3">
        <button
          type="button"
          className="glass-dark px-2.5 sm:px-3 py-1.5 sm:py-2 rounded-full font-label-sm sm:font-label-md text-label-sm sm:text-label-md hover:bg-primary/10 active:scale-95 transition-all min-h-[36px]"
          onClick={() => setInput('Son 24 saatteki metrikleri göster')}
        >
          📊 Metrikler
        </button>
        <button
          type="button"
          className="glass-dark px-2.5 sm:px-3 py-1.5 sm:py-2 rounded-full font-label-sm sm:font-label-md text-label-sm sm:text-label-md hover:bg-primary/10 active:scale-95 transition-all min-h-[36px]"
          onClick={() => setInput('Sistem sağlığını kontrol et')}
        >
          ❤️ Sağlık
        </button>
        <button
          type="button"
          className="glass-dark px-2.5 sm:px-3 py-1.5 sm:py-2 rounded-full font-label-sm sm:font-label-md text-label-sm sm:text-label-md hover:bg-primary/10 active:scale-95 transition-all min-h-[36px]"
          onClick={() => setInput('AI ajanlarının durumu nedir?')}
        >
          🤖 AI Ajanlar
        </button>
      </div>

      {/* Input */}
      <div className="flex gap-2">
        <input
          type="text"
          className="flex-1 glass-dark border-none rounded-full py-2 sm:py-2.5 px-3 sm:px-4 focus:ring-2 focus:ring-primary/20 text-body-sm sm:text-body-md font-body-sm sm:font-body-md min-h-[44px]"
          placeholder="Bir şey sorun..."
          value={input}
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === 'Enter' && handleSend()}
        />
        <button
          type="button"
          className="px-3 sm:px-4 py-2 rounded-full bg-primary text-white hover:opacity-90 active:scale-95 transition-all min-w-[44px] min-h-[44px] flex items-center justify-center"
          onClick={handleSend}
        >
          <span className="material-symbols-outlined text-lg sm:text-xl">send</span>
        </button>
      </div>
    </div>
  );
}
