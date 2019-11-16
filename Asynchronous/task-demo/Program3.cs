using System;
using System.Threading.Tasks;

namespace task_demo
{
    class Program3
    {
        static string ReadFileSync1()
        {
            Task.Delay(2000).Wait();
            return "content1";
        }

        static string ReadFileSync2()
        {
            Task.Delay(2000).Wait();
            return "content2";
        }

        static string ReadFileSync3()
        {
            Task.Delay(2000).Wait();
            return "content3";
        }

        static void Main(string[] args)
        {
            var start = DateTime.Now;
            var c1 = ReadFileSync1();
            var c2 = ReadFileSync2();
            var c3 = ReadFileSync3();
            var end = DateTime.Now;

            // Time taken 00:00:06.0258269
            Console.WriteLine("Time taken {0}", end - start);
        }
    }
}
