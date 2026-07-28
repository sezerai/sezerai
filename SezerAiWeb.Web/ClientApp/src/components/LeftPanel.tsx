import { useDashboardStore } from '../store/useDashboardStore';
import { PlatformCard } from './PlatformCard';

export function LeftPanel() {
  const platforms = useDashboardStore(state => state.platforms);

  return (
    <div className="flex flex-col gap-3 sm:gap-4 lg:gap-6 max-h-[400px] sm:max-h-[600px] lg:max-h-none overflow-y-auto custom-scroll pr-2">
      <h2 className="font-headline-sm sm:font-headline-md text-headline-sm sm:text-headline-md text-on-surface opacity-80 pl-2">
        Platformlar
      </h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-1 gap-3 sm:gap-4">
        {platforms.map((platform) => (
          <PlatformCard key={platform.id} platform={platform} />
        ))}
      </div>
    </div>
  );
}
