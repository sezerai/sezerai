import { useDashboardStore } from '../store/useDashboardStore';
import { PlatformCard } from './PlatformCard';

export function LeftPanel() {
  const platforms = useDashboardStore(state => state.platforms);

  return (
    <div className="col-span-2 flex flex-col gap-6 overflow-y-auto custom-scroll pr-2">
      <h2 className="font-headline-md text-headline-md text-on-surface opacity-80 pl-2">
        Platformlar
      </h2>
      <div className="grid grid-cols-1 gap-4">
        {platforms.map((platform) => (
          <PlatformCard key={platform.id} platform={platform} />
        ))}
      </div>
    </div>
  );
}
