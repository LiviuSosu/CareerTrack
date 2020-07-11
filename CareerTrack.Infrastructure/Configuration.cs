using CareerTrack.Common;

namespace CareerTrack.Infrastructure
{
    public class Configuration : IConfiguration
    {
        private readonly string displayUserErrorMessage;
        public string DisplayUserErrorMessage { get => displayUserErrorMessage; }

        public Configuration()
        {
            displayUserErrorMessage = "Internal server error";// it can also be read from a configuration file
        }
    }
}
