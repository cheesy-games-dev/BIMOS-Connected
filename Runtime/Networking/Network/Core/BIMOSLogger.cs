using System;
using UnityEngine;

namespace KadenZombie8.BIMOS.Networking
{
    public static class BIMOSLogger
    {
        public const string Key = "[BIMOS]";
        public static Action<string> OnLog {
            get;set;
        } = Debug.Log;
        public static Action<string> OnWarning {
            get; set;
        } = Debug.LogWarning;
        public static Action<string> OnError {
            get; set;
        } = Debug.LogError;
        public static Action<Exception> OnException {
            get; set;
        } = Debug.LogException;
        public static void InitLogger(Action<string> onLog, Action<string> onWarning, Action<string> onError, Action<Exception> onException) {
            OnLog = onLog;
            OnWarning = onWarning;
            OnError = onError;
            OnException = onException;
        }
        public static void Log(string message) {
            OnLog?.Invoke($"{Key}: {message}");
        }
        public static void LogWarn(string message) {
            OnWarning?.Invoke($"{Key}: {message}");
        }
        public static void LogError(string message) {
            OnError?.Invoke($"{Key}: {message}");
        }
        public static void LogException(Exception message) {
            OnException?.Invoke(message);
        }
    }
}
