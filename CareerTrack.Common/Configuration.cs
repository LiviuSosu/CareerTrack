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
    }
}
