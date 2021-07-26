using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleApp
{
    public class App
    {
        private readonly ILogger<App> _log;
        private readonly AppOption _appOption;
        private readonly IDataService _dataService;
        public App(ILogger<App> log, IOptions<AppOption> appOption, IDataService dataService)
        {
            _log = log;
            _appOption = appOption.Value;
            this._dataService = dataService;
        }

        public void Run()
        {
            _log.LogInformation($"{_appOption.ToString()}");
            _log.LogInformation("App Run Start");
            _dataService.Run();
            _log.LogInformation("App Run End");
        }
    }
}
