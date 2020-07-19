using Serilog;
using Serilog.Events;
using System;

namespace CareerTrack.Common
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new Configuration().LoggingFilePath)
                .CreateLogger();
        }

        public void LogInformation(string methodName, string input, string token)
        {
            Log.Logger.Information(string.Format("Logging on {0} with input {1} having the token {2}", methodName, input, token), LogEventLevel.Information);
        }

        public void LogException(Exception exception, string actionName, string input, string token)
        {
            Log.Logger.Error(string.Format("Occured exception {0} on {1} having the input: {2} and the token {3}", exception.Message, actionName, input, token), LogEventLevel.Error);
        }
    }
}
