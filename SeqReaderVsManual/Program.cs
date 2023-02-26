using BenchmarkDotNet.Running;

namespace SeqReaderVsManual
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}