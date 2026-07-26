import type { Platform } from '../types';
import { cn } from '../lib/utils';

interface PlatformCardProps {
  platform: Platform;
  onClick?: () => void;
}

export function PlatformCard({ platform, onClick }: PlatformCardProps) {
  const statusColor = platform.status === 'online' ? 'bg-green-500' :
                      platform.status === 'offline' ? 'bg-red-500' :
                      'bg-yellow-500';

  return (
    <div
      className={cn(
        "glass p-4 rounded-panel-radius hover:scale-[1.02]",
        "transition-transform cursor-pointer group flex items-center gap-4"
      )}
      onClick={onClick}
    >
      <div className="w-16 h-16 bg-white rounded-2xl flex items-center justify-center shadow-sm border border-white/40 overflow-hidden"
           style={platform.iconBg ? { backgroundColor: platform.iconBg } : undefined}>
        {platform.iconUrl ? (
          <img
            alt={platform.name}
            className={cn(
              "w-full h-full",
              platform.slug === 'ai-hospital' || platform.slug === 'geliyoo' ? 'object-contain p-2' : 'object-cover'
            )}
            src={platform.iconUrl}
          />
        ) : platform.icon ? (
          <span className="material-symbols-outlined text-white text-3xl"
                style={{ fontVariationSettings: "'FILL' 1, 'wght' 400, 'GRAD' 0, 'opsz' 24" }}>
            {platform.icon}
          </span>
        ) : null}
      </div>
      <div className="flex-1">
        <div className="flex items-center gap-2">
          <span className="font-title-sm text-title-sm text-on-surface">{platform.name}</span>
          <span className={cn("w-2 h-2 rounded-full", statusColor)}></span>
        </div>
        <p className="font-label-xs text-label-xs text-on-surface-variant">{platform.description}</p>
      </div>
    </div>
  );
}
