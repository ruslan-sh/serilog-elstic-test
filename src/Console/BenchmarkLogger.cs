using System;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace RuslanSh.SerilogElasticTest.ConsoleClient
{
	public class BenchmarkLogger
	{
		private readonly Logger _elasticLogger = new LoggerConfiguration()
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
		private readonly Logger _fileLogger = new LoggerConfiguration()
			.WriteTo.RollingFile("log-{Date}.log")
			.CreateLogger();

		private readonly Logger _rabbitLogger = new LoggerConfiguration()
			.WriteTo.RabbitMQ(
				new RabbitMQConfiguration
				{
					Hostname = "localhost",
					Username = "Transcrypt",
					Password = "123456",
					Exchange = "Logs",
					ExchangeType = "direct",
					DeliveryMode = RabbitMQDeliveryMode.Durable,
					RouteKey = "Logs",
					Port = 5672
				},
				new JsonFormatter())
			.CreateLogger();

		public string LogMesage = "Log {system} {index}";

		private int logToFileCount = 0;
		public void LogToFile() => _fileLogger.Information(LogMesage, "file", ++logToFileCount);

		private int logToElasticCount = 0;
		public void LogToElastic() => _elasticLogger.Information(LogMesage, "elastic", ++logToElasticCount);

		private int logToRabbitCount = 0;
		public void LogToRabbit() => _rabbitLogger.Information(LogMesage, "rabbit", ++logToRabbitCount);
	}
}