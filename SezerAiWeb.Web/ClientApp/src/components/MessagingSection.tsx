import { useState } from 'react';
import { messageTemplates } from '../lib/mockData';

export function MessagingSection() {
  const [phoneNumber, setPhoneNumber] = useState('');
  const [message, setMessage] = useState('');
  const [whatsapp, setWhatsapp] = useState(false);
  const [telegram, setTelegram] = useState(false);

  // Phone number validation and formatting
  const formatPhoneNumber = (phone: string): string => {
    // Remove all non-numeric characters except +
    let cleaned = phone.replace(/[^\d+]/g, '');

    // If starts with 0, replace with +90
    if (cleaned.startsWith('0')) {
      cleaned = '+90' + cleaned.substring(1);
    }

    // If doesn't start with +, add +90
    if (!cleaned.startsWith('+')) {
      cleaned = '+90' + cleaned;
    }

    return cleaned;
  };

  const handleSend = () => {
    // Validation
    if (!phoneNumber.trim()) {
      alert('⚡ Lütfen telefon numarası girin!');
      return;
    }

    if (!message.trim()) {
      alert('⚡ Lütfen mesaj yazın!');
      return;
    }

    if (!whatsapp && !telegram) {
      alert('⚡ Lütfen en az bir platform seçin (WhatsApp veya Telegram)!');
      return;
    }

    const formattedPhone = formatPhoneNumber(phoneNumber);
    const encodedMessage = encodeURIComponent(message.trim());

    // Open WhatsApp
    if (whatsapp) {
      const whatsappUrl = `https://wa.me/${formattedPhone.replace(/\+/g, '')}?text=${encodedMessage}`;
      window.open(whatsappUrl, '_blank');
    }

    // Open Telegram
    if (telegram) {
      const telegramUrl = `https://t.me/${formattedPhone}?text=${encodedMessage}`;
      window.open(telegramUrl, '_blank');
    }

    // Clear form after sending
    setTimeout(() => {
      setMessage('');
      setPhoneNumber('');
    }, 500);
  };

  return (
    <div className="flex-1 glass rounded-panel-radius p-3 sm:p-4 mac-shadow flex flex-col h-full max-h-[calc(100vh-180px)] sm:max-h-[calc(100vh-160px)] w-full touch-manipulation overflow-y-auto custom-scroll pb-20">
      <div className="flex items-center gap-2 mb-1.5 sm:mb-2">
        <div className="w-2 h-2 rounded-full bg-primary animate-pulse flex-shrink-0"></div>
        <span className="font-title-sm text-title-sm text-on-surface truncate">Toplu Mesaj Gönder</span>
      </div>

      {/* Phone Input */}
      <div className="mb-1.5">
        <label className="text-label-xs font-label-xs text-on-surface-variant mb-0.5 block">
          Telefon Numarası
        </label>
        <input
          type="tel"
          inputMode="tel"
          autoComplete="tel"
          className="w-full glass-dark border-none rounded-full py-1.5 sm:py-2 px-3 sm:px-4 focus:ring-2 focus:ring-primary/20 text-body-sm sm:text-body-md font-body-sm sm:font-body-md min-h-[44px] touch-manipulation"
          placeholder="+90 5XX XXX XX XX"
          maxLength={17}
          value={phoneNumber}
          onChange={(e) => setPhoneNumber(e.target.value)}
        />
      </div>

      {/* Platform Selection */}
      <div className="mb-1.5 flex items-center gap-3 sm:gap-4 flex-wrap">
        <label className="flex items-center gap-2 cursor-pointer min-h-[44px] touch-manipulation">
          <input
            type="checkbox"
            className="w-5 h-5 min-w-[20px] rounded border-2 border-primary text-primary focus:ring-primary focus:ring-2 focus:ring-offset-2 cursor-pointer"
            checked={whatsapp}
            onChange={(e) => setWhatsapp(e.target.checked)}
          />
          <svg className="w-6 h-6 min-w-[24px] text-green-500 flex-shrink-0" fill="currentColor" viewBox="0 0 24 24">
            <path d="M17.472 14.382c-.297-.149-1.758-.867-2.03-.967-.273-.099-.471-.148-.67.15-.197.297-.767.966-.94 1.164-.173.199-.347.223-.644.075-.297-.15-1.255-.463-2.39-1.475-.883-.788-1.48-1.761-1.653-2.059-.173-.297-.018-.458.13-.606.134-.133.298-.347.446-.52.149-.174.198-.298.298-.497.099-.198.05-.371-.025-.52-.075-.149-.669-1.612-.916-2.207-.242-.579-.487-.5-.669-.51-.173-.008-.371-.01-.57-.01-.198 0-.52.074-.792.372-.272.297-1.04 1.016-1.04 2.479 0 1.462 1.065 2.875 1.213 3.074.149.198 2.096 3.2 5.077 4.487.709.306 1.262.489 1.694.625.712.227 1.36.195 1.871.118.571-.085 1.758-.719 2.006-1.413.248-.694.248-1.289.173-1.413-.074-.124-.272-.198-.57-.347m-5.421 7.403h-.004a9.87 9.87 0 01-5.031-1.378l-.361-.214-3.741.982.998-3.648-.235-.374a9.86 9.86 0 01-1.51-5.26c.001-5.45 4.436-9.884 9.888-9.884 2.64 0 5.122 1.03 6.988 2.898a9.825 9.825 0 012.893 6.994c-.003 5.45-4.437 9.884-9.885 9.884m8.413-18.297A11.815 11.815 0 0012.05 0C5.495 0 .16 5.335.157 11.892c0 2.096.547 4.142 1.588 5.945L.057 24l6.305-1.654a11.882 11.882 0 005.683 1.448h.005c6.554 0 11.89-5.335 11.893-11.893a11.821 11.821 0 00-3.48-8.413z"/>
          </svg>
          <span className="text-body-md font-body-md select-none">WhatsApp</span>
        </label>

        <label className="flex items-center gap-2 cursor-pointer min-h-[44px] touch-manipulation">
          <input
            type="checkbox"
            className="w-5 h-5 min-w-[20px] rounded border-2 border-primary text-primary focus:ring-primary focus:ring-2 focus:ring-offset-2 cursor-pointer"
            checked={telegram}
            onChange={(e) => setTelegram(e.target.checked)}
          />
          <svg className="w-6 h-6 min-w-[24px] text-blue-500 flex-shrink-0" fill="currentColor" viewBox="0 0 24 24">
            <path d="M11.944 0A12 12 0 0 0 0 12a12 12 0 0 0 12 12 12 12 0 0 0 12-12A12 12 0 0 0 12 0a12 12 0 0 0-.056 0zm4.962 7.224c.1-.002.321.023.465.14a.506.506 0 0 1 .171.325c.016.093.036.306.02.472-.18 1.898-.962 6.502-1.36 8.627-.168.9-.499 1.201-.82 1.23-.696.065-1.225-.46-1.9-.902-1.056-.693-1.653-1.124-2.678-1.8-1.185-.78-.417-1.21.258-1.91.177-.184 3.247-2.977 3.307-3.23.007-.032.014-.15-.056-.212s-.174-.041-.249-.024c-.106.024-1.793 1.14-5.061 3.345-.48.33-.913.49-1.302.48-.428-.008-1.252-.241-1.865-.44-.752-.245-1.349-.374-1.297-.789.027-.216.325-.437.893-.663 3.498-1.524 5.83-2.529 6.998-3.014 3.332-1.386 4.025-1.627 4.476-1.635z"/>
          </svg>
          <span className="text-body-md font-body-md select-none">Telegram</span>
        </label>
      </div>

      {/* Message Icons */}
      <div className="mb-1.5 flex flex-wrap gap-1 sm:gap-1.5">
        {messageTemplates.map((template, idx) => (
          <button
            key={idx}
            type="button"
            className="w-9 h-9 sm:w-8 sm:h-8 min-w-[36px] min-h-[36px] glass-dark rounded-lg flex items-center justify-center hover:bg-primary/20 active:scale-95 transition-all text-sm sm:text-base touch-manipulation"
            title={template.title}
            onClick={() => setMessage(message + ' ' + template.icon)}
          >
            {template.icon}
          </button>
        ))}
      </div>

      {/* Message TextArea */}
      <div className="relative mb-1.5 sm:mb-2 flex-1 min-h-0">
        <textarea
          className="w-full h-full glass-dark border-none rounded-2xl py-2 sm:py-2.5 px-3 sm:px-4 focus:ring-2 focus:ring-primary/20 text-body-sm sm:text-body-md font-body-sm sm:font-body-md resize-none custom-scroll touch-manipulation"
          placeholder="Mesajınızı yazın..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          rows={3}
        ></textarea>
      </div>

      {/* Send Button */}
      <button
        type="button"
        className="w-full py-2 sm:py-2.5 rounded-full font-label-md sm:font-title-sm text-label-md sm:text-title-sm transition-all shadow-lg hover:shadow-xl active:scale-[0.98] min-h-[44px] touch-manipulation"
        style={{
          background: 'linear-gradient(135deg, #c0c0c0 0%, #e8e8e8 100%)',
          border: '2px solid #d4af37',
          color: '#2d3133'
        }}
        onClick={handleSend}
      >
        <span className="flex items-center justify-center gap-2">
          <span className="material-symbols-outlined text-lg sm:text-xl">send</span>
          <span className="select-none">Gönder</span>
        </span>
      </button>
    </div>
  );
}
