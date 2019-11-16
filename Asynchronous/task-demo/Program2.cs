using System;
using System.Threading.Tasks;

namespace task_demo
{
    class Program2
    {
        static async Task<string> ReadFile1()
        {
            await Task.Delay(3000);
            return "file1";
        }

        static async Task<string> ReadFile2()
        {
            await Task.Delay(4000);
            return "file2";
        }

        static async Task<string> ReadFile3()
        {
            await Task.Delay(2000);
            return "file3";
        }

        static void Main(string[] args)
        {
            var task1 = ReadFile1();
            var task2 = ReadFile2();
            var task3 = ReadFile3();

            var start = DateTime.Now;
            Task.WaitAny(task1, task2, task3);

            Console.WriteLine("Task1, completed: {0}", task1.IsCompleted);

            Console.WriteLine("Task2, completed: {0}", task2.IsCompleted);

            Console.WriteLine("Task3, completed: {0}", task3.IsCompleted);
            Console.WriteLine("Task3, completed: {0}", task3.Result);

            var end = DateTime.Now;
            Console.WriteLine("Time taken! {0}", end - start);
        }
    }
}
