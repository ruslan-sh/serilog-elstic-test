using System;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace RuslanSh.SerilogElasticTest.ConsoleClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var benchmarkSummary = BenchmarkRunner.Run<BenchmarkLogger>();
			//var logger = new BenchmarkLogger();
			//logger.LogToFile();
			//logger.LogToElastic();
			//logger.LogToRabbit();
		}
	}
}
															  