using System;

namespace RuslanSh.SerilogElasticTest.ConsoleClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var logger = new BenchmarkLogger();
			var n = 100000;
			//Benchmark(n, logger.LogToFile, "Log to File");
			//Benchmark(n, logger.LogToElastic, "Log to Elastic");
			Benchmark(n, logger.LogToRabbit, "Log to Rabbit");
			Console.ReadKey();
		}

		private static void Benchmark(int n, Action action, string actionName)
		{
			var startDate = DateTime.Now;
			for (int i = 0; i < n; i++)
			{
				action.Invoke();
			}
			var delta = DateTime.Now - startDate;
			Console.WriteLine($"{actionName} {n}: {delta}");
		}
	}
}
															  