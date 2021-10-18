using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleApp
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _log;
        private readonly IConfiguration _config;
        public DataService(ILogger<DataService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            // Connect to the database
            var connectionString = _config.GetValue<string>("ConnectionStrings:DefaultConnection");

            _log.LogInformation("Connection String {cs}", connectionString);
        }
    }
}
