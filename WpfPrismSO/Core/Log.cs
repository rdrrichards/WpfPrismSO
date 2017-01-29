using NLog;

namespace WpfPrismSO.Core
{
    public class Log
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        public static void Debug(string msg) { _log.Debug(msg); }
        public static void Error(string msg) { _log.Error(msg); }
        public static void Info(string msg) { _log.Info(msg); }
        public static void Warn(string msg) { _log.Warn(msg); }
    }
}