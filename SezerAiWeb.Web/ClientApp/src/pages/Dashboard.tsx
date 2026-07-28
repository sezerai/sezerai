import { LeftPanel } from '../components/LeftPanel';
import { RightPanel } from '../components/RightPanel';
import { MessagingSection } from '../components/MessagingSection';
import { ChatbotSection } from '../components/ChatbotSection';

export function Dashboard() {
  return (
    <div className="min-h-screen bg-surface p-3 sm:p-4 md:p-6 overflow-x-hidden">
      <div className="max-w-[1600px] mx-auto">
        {/* Header */}
        <div className="mb-3 sm:mb-4 md:mb-6">
          <h1 className="font-display-sm sm:font-display-md text-display-sm sm:text-display-md text-on-surface mb-1 sm:mb-2 leading-tight">
            SEZER AI OS
          </h1>
          <p className="font-body-md sm:font-body-lg text-body-md sm:text-body-lg text-on-surface-variant leading-snug">
            Master Control Center - Shaping the Future
          </p>
        </div>

        {/* Main Grid - Mobile: Stack vertically, Desktop: 3 columns */}
        <div className="grid grid-cols-1 lg:grid-cols-12 gap-3 sm:gap-4 md:gap-6">
          {/* Left Panel: Platforms */}
          <div className="lg:col-span-2 w-full">
            <LeftPanel />
          </div>

          {/* Center Area: Messaging + Chatbot */}
          <div className="lg:col-span-7 flex flex-col sm:flex-row items-stretch justify-center gap-3 sm:gap-4 md:gap-6 relative w-full min-h-[600px] h-[calc(100vh-200px)]">
            {/* Left: Messaging Section */}
            <MessagingSection />

            {/* Gold Divider - Vertical on desktop, Horizontal on mobile */}
            <div
              className="hidden sm:block w-1 self-stretch rounded-full flex-shrink-0"
              style={{
                background:
                  'linear-gradient(180deg, rgba(212, 175, 55, 0.2) 0%, rgba(212, 175, 55, 0.8) 50%, rgba(212, 175, 55, 0.2) 100%)'
              }}
            ></div>
            <div
              className="sm:hidden h-1 w-full rounded-full my-1"
              style={{
                background:
                  'linear-gradient(90deg, rgba(212, 175, 55, 0.2) 0%, rgba(212, 175, 55, 0.8) 50%, rgba(212, 175, 55, 0.2) 100%)'
              }}
            ></div>

            {/* Right: AI Chatbot Section */}
            <ChatbotSection />
          </div>

          {/* Right Panel: Metrics, AI Agents, Traffic */}
          <div className="lg:col-span-3 w-full">
            <RightPanel />
          </div>
        </div>
      </div>
    </div>
  );
}
