using NUnit.Framework.Internal;
using Serilog.Core;
using Serilog.Events;

namespace tiver_cockatiel.Logging
{
    public class TestDetailsEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "TestName", TestExecutionContext.CurrentContext.CurrentTest.Name));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                "TestId", TestExecutionContext.CurrentContext.CurrentTest.Id));
        }
    }    
}