using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Common
{
    public static class Configuration
    {
        public static string SecretKey = "MySuperSecureKey";
        public static string Issuer = "CareerTrack";
        public static string Audience = "CareerTrack";
        public static int JwtLifeTime = 24;

        public static string DisplayUserErrorMessage = "Internal server error";
        public static string LoggingFilePath = @"C:\Users\lsosu\Work\Proiecte\Personale\CareerTrack\CareerTrack\CareerTrack.WebApi\Logs\logs.log";
    }
}
