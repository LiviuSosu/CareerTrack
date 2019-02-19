using System;

namespace CareerTrack.Common
{
    public interface ILogger
    {
        void LogInformation(string methodName, string input, string token);
        void LogException(Exception exception, string actionName, string input, string token);
    }
}
