
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using System;

namespace RuslanSh.SerilogElasticTest.ConsoleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var consoleLogger = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
				{
					AutoRegisterTemplate = true,
					FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
					EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
									   EmitEventFailureHandling.WriteToFailureSink |
									   EmitEventFailureHandling.RaiseCallback,
					FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null)
				})
				.CreateLogger();
		}
	}
}
