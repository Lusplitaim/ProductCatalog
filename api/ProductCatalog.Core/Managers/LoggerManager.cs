using NLog;

namespace ProductCatalog.Core.Managers
{
    internal class LoggerManager : ILoggerManager
    {
        private static ILogger m_Logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            m_Logger.Debug(message);
        }

        public void LogError(string message)
        {
            m_Logger.Error(message);
        }

        public void LogError(Exception ex, string? message)
        {
            m_Logger.Error(ex, message);
        }

        public void LogInfo(string message)
        {
            m_Logger.Info(message);
        }

        public void LogWarn(string message)
        {
            m_Logger.Warn(message);
        }
    }
}
