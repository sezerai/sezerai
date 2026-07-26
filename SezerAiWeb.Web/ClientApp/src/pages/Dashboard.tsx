import { LeftPanel } from '../components/LeftPanel';
import { RightPanel } from '../components/RightPanel';
import { MessagingSection } from '../components/MessagingSection';
import { ChatbotSection } from '../components/ChatbotSection';

export function Dashboard() {
  return (
    <div className="min-h-screen bg-surface p-6">
      <div className="max-w-[1600px] mx-auto">
        {/* Header */}
        <div className="mb-6">
          <h1 className="font-display-md text-display-md text-on-surface mb-2">
            SEZER AI OS
          </h1>
          <p className="font-body-lg text-body-lg text-on-surface-variant">
            Master Control Center - Shaping the Future
          </p>
        </div>

        {/* Main Grid */}
        <div className="grid grid-cols-12 gap-6">
          {/* Left Panel: Platforms */}
          <LeftPanel />

          {/* Center Area: Messaging + Chatbot */}
          <div className="col-span-7 flex items-start justify-center gap-6 relative">
            {/* Left: Messaging Section */}
            <MessagingSection />

            {/* Gold Vertical Divider */}
            <div
              className="w-1 h-full rounded-full"
              style={{
                background:
                  'linear-gradient(180deg, rgba(212, 175, 55, 0.2) 0%, rgba(212, 175, 55, 0.8) 50%, rgba(212, 175, 55, 0.2) 100%)'
              }}
            ></div>

            {/* Right: AI Chatbot Section */}
            <ChatbotSection />
          </div>

          {/* Right Panel: Metrics, AI Agents, Traffic */}
          <RightPanel />
        </div>
      </div>
    </div>
  );
}
