using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading;

namespace Samples.Entities.SampleActionEntities
{
    public class LoggedSampleAction
    {
        protected readonly ILogger<LoggedSampleAction> _sampleActionLogger;

        protected LoggedSampleAction()
        {
            _sampleActionLogger = LogExecute(Thread.CurrentThread.Name);  
        }

        public ILogger<LoggedSampleAction> LogExecute(string logTypeName)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .WriteTo.File($"log_{logTypeName}.txt", rollingInterval: RollingInterval.Day);
                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });
            ILogger<LoggedSampleAction> sampleActionLogger = loggerFactory.CreateLogger<LoggedSampleAction>();
            return sampleActionLogger;
        }

        public void WriteToLogInformation(string message)
        {
            this._sampleActionLogger.LogInformation(message);
        }
        public void WriteToLogError(string message)
        {
            this._sampleActionLogger.LogError(message);
        }
    }
}
