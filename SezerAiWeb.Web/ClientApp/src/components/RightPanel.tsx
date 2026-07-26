import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, Legend } from 'recharts';
import { mockSystemMetrics, mockAIAgents, mockTrafficData } from '../lib/mockData';

export function RightPanel() {
  return (
    <div className="col-span-3 flex flex-col gap-6 overflow-y-auto custom-scroll pl-2">
      {/* System Status */}
      <div className="glass rounded-panel-radius p-6 mac-shadow">
        <h3 className="font-title-md text-title-md text-on-surface mb-4">Sistem Durumu</h3>
        <div className="grid grid-cols-2 gap-4">
          {mockSystemMetrics.map((metric, idx) => (
            <div key={idx} className="glass-dark rounded-lg p-3">
              <div className="flex items-center gap-2 mb-1">
                <span className="material-symbols-outlined text-primary text-sm">{metric.icon}</span>
                <span className="font-label-sm text-label-sm text-on-surface-variant">{metric.label}</span>
              </div>
              <div className="font-title-lg text-title-lg text-on-surface">{metric.value}</div>
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
      <div className="glass rounded-panel-radius p-6 mac-shadow">
        <h3 className="font-title-md text-title-md text-on-surface mb-4">AI Ajanlar</h3>
        <div className="space-y-3">
          {mockAIAgents.map((agent) => (
            <div key={agent.id} className="glass-dark rounded-lg p-3">
              <div className="flex items-center justify-between mb-2">
                <span className="font-title-sm text-title-sm text-on-surface">{agent.name}</span>
                <span className={`w-2 h-2 rounded-full ${
                  agent.status === 'active' ? 'bg-green-500 animate-pulse' :
                  agent.status === 'idle' ? 'bg-yellow-500' : 'bg-red-500'
                }`}></span>
              </div>
              <div className="flex justify-between text-label-xs font-label-xs text-on-surface-variant">
                <span>Görevler: {agent.tasksCompleted}</span>
                <span>Doğruluk: {agent.accuracy}%</span>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* Traffic Chart */}
      <div className="glass rounded-panel-radius p-6 mac-shadow">
        <h3 className="font-title-md text-title-md text-on-surface mb-4">Trafik Analizi</h3>
        <ResponsiveContainer width="100%" height={200}>
          <LineChart data={mockTrafficData}>
            <CartesianGrid strokeDasharray="3 3" stroke="rgba(0,0,0,0.1)" />
            <XAxis dataKey="time" tick={{ fontSize: 10 }} />
            <YAxis tick={{ fontSize: 10 }} />
            <Tooltip />
            <Legend wrapperStyle={{ fontSize: 10 }} />
            <Line type="monotone" dataKey="visits" stroke="#6750A4" strokeWidth={2} />
            <Line type="monotone" dataKey="pageViews" stroke="#d4af37" strokeWidth={2} />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}
