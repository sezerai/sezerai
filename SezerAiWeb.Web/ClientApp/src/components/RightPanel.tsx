import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Legend } from 'recharts';
import { mockSystemMetrics, mockAIAgents, mockTrafficData } from '../lib/mockData';

export function RightPanel() {
  return (
    <div className="flex flex-col gap-3 sm:gap-4 lg:gap-6 max-h-[600px] sm:max-h-[800px] lg:max-h-none overflow-y-auto custom-scroll pl-0 sm:pl-2 w-full">
      {/* System Status */}
      <div className="glass rounded-panel-radius p-3 sm:p-4 md:p-6 mac-shadow">
        <h3 className="font-title-sm sm:font-title-md text-title-sm sm:text-title-md text-on-surface mb-2 sm:mb-3 md:mb-4 truncate">Sistem Durumu</h3>
        <div className="grid grid-cols-2 gap-2 sm:gap-3 lg:gap-4">
          {mockSystemMetrics.map((metric, idx) => (
            <div key={idx} className="glass-dark rounded-lg p-2 sm:p-3 min-h-[72px] sm:min-h-[80px] flex flex-col justify-center touch-manipulation cursor-pointer hover:bg-primary/5 active:scale-[0.98] transition-all">
              <div className="flex items-center gap-1 sm:gap-2 mb-1">
                <span className="material-symbols-outlined text-primary text-sm sm:text-base flex-shrink-0">{metric.icon}</span>
                <span className="font-label-xs sm:font-label-sm text-label-xs sm:text-label-sm text-on-surface-variant truncate">{metric.label}</span>
              </div>
              <div className="font-title-md sm:font-title-lg text-title-md sm:text-title-lg text-on-surface truncate">{metric.value}</div>
              {metric.change !== undefined && (
                <div className={`font-label-xs text-label-xs ${metric.change >= 0 ? 'text-green-500' : 'text-red-500'}`}>
                  {metric.change >= 0 ? '↑' : '↓'} {Math.abs(metric.change)}%
                </div>
              )}
            </div>
          ))}
        </div>
      </div>

      {/* AI Agents */}
      <div className="glass rounded-panel-radius p-3 sm:p-4 md:p-6 mac-shadow">
        <h3 className="font-title-sm sm:font-title-md text-title-sm sm:text-title-md text-on-surface mb-2 sm:mb-3 md:mb-4 truncate">AI Ajanlar</h3>
        <div className="space-y-2 sm:space-y-3">
          {mockAIAgents.map((agent) => (
            <div key={agent.id} className="glass-dark rounded-lg p-2 sm:p-3 min-h-[60px] touch-manipulation cursor-pointer hover:bg-primary/5 active:scale-[0.98] transition-all">
              <div className="flex items-center justify-between mb-1 sm:mb-2">
                <span className="font-label-md sm:font-title-sm text-label-md sm:text-title-sm text-on-surface truncate pr-2">{agent.name}</span>
                <span className={`w-2 h-2 min-w-[8px] rounded-full flex-shrink-0 ${
                  agent.status === 'active' ? 'bg-green-500 animate-pulse' :
                  agent.status === 'idle' ? 'bg-yellow-500' : 'bg-red-500'
                }`}></span>
              </div>
              <div className="flex justify-between text-label-xs font-label-xs text-on-surface-variant">
                <span className="truncate">Görevler: {agent.tasksCompleted}</span>
                <span className="flex-shrink-0 ml-2">Doğruluk: {agent.accuracy}%</span>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Traffic Chart */}
      <div className="glass rounded-panel-radius p-3 sm:p-4 md:p-6 mac-shadow">
        <h3 className="font-title-sm sm:font-title-md text-title-sm sm:text-title-md text-on-surface mb-2 sm:mb-3 md:mb-4 truncate">Trafik Analizi</h3>
        <ResponsiveContainer width="100%" height={140} className="sm:h-[160px]">
          <LineChart data={mockTrafficData}>
            <CartesianGrid strokeDasharray="3 3" stroke="rgba(0,0,0,0.1)" />
            <XAxis dataKey="time" tick={{ fontSize: 8 }} />
            <YAxis tick={{ fontSize: 8 }} />
            <Tooltip contentStyle={{ fontSize: 10 }} />
            <Legend wrapperStyle={{ fontSize: 8 }} />
            <Line type="monotone" dataKey="visits" stroke="#6750A4" strokeWidth={2} dot={false} />
            <Line type="monotone" dataKey="pageViews" stroke="#d4af37" strokeWidth={2} dot={false} />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}
