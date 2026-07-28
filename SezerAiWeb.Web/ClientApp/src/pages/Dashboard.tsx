import { LeftPanel } from '../components/LeftPanel';
import { RightPanel } from '../components/RightPanel';
import { MessagingSection } from '../components/MessagingSection';
import { ChatbotSection } from '../components/ChatbotSection';

export function Dashboard() {
  return (
    <div className="min-h-screen bg-surface p-3 sm:p-6">
      <div className="max-w-[1600px] mx-auto">
        {/* Header */}
        <div className="mb-4 sm:mb-6">
          <h1 className="font-display-sm sm:font-display-md text-display-sm sm:text-display-md text-on-surface mb-1 sm:mb-2">
            SEZER AI OS
          </h1>
          <p className="font-body-md sm:font-body-lg text-body-md sm:text-body-lg text-on-surface-variant">
            Master Control Center - Shaping the Future
          </p>
        </div>

        {/* Main Grid - Mobile: Stack vertically, Desktop: 3 columns */}
        <div className="grid grid-cols-1 lg:grid-cols-12 gap-4 sm:gap-6">
          {/* Left Panel: Platforms */}
          <div className="lg:col-span-2">
            <LeftPanel />
          </div>

          {/* Center Area: Messaging + Chatbot */}
          <div className="lg:col-span-7 flex flex-col sm:flex-row items-start justify-center gap-4 sm:gap-6 relative">
            {/* Left: Messaging Section */}
            <MessagingSection />

            {/* Gold Divider - Vertical on desktop, Horizontal on mobile */}
            <div
              className="hidden sm:block w-1 h-full rounded-full"
              style={{
                background:
                  'linear-gradient(180deg, rgba(212, 175, 55, 0.2) 0%, rgba(212, 175, 55, 0.8) 50%, rgba(212, 175, 55, 0.2) 100%)'
              }}
            ></div>
            <div
              className="sm:hidden h-1 w-full rounded-full my-2"
              style={{
                background:
                  'linear-gradient(90deg, rgba(212, 175, 55, 0.2) 0%, rgba(212, 175, 55, 0.8) 50%, rgba(212, 175, 55, 0.2) 100%)'
              }}
            ></div>

            {/* Right: AI Chatbot Section */}
            <ChatbotSection />
          </div>

          {/* Right Panel: Metrics, AI Agents, Traffic */}
          <div className="lg:col-span-3">
            <RightPanel />
          </div>
        </div>
      </div>
    </div>
  );
}
