using System;
using System.Threading.Tasks;

namespace task_demo
{
    class Program1
    {
        static async Task DoSomething()
        {
            await Task.Delay(2000);
        }

        static async Task<int> Sum(int a, int b)
        {
            var result = await Task.FromResult(a + b);
            return result;
        }

        // Another way to do:
        // static async Task<int> Sum2(int a, int b) 
        // {
        //     var result = await Task.Run(() => {
        //         // do some time-consuming work
        //         return a + b;
        //     });
        //     return result;
        // }

        static void Main(string[] args)
        {
            var start = DateTime.Now;

            var taskSum = Sum(2, 2);
            var taskDelay = DoSomething();

            // Time taken! 00:00:02.0285359
            // Even though the calculation from calling Sum() took a few milliseconds,
            // we don't get any response until 2 seconds later, when DoSomething() has finished.
            // Task.WaitAll(taskSum, taskDelay);

            // Time taken! 00:00:00.0115538
            // Task.WhenAll(taskSum, taskDelay);

            // if we want the code to wait at a specific point, using WaitAny is a good idea
            // but if you want to start up a lot of asynchronous work then use When....

            // var end = DateTime.Now;

            // Console.WriteLine("Time taken! {0}", end - start);

            var twoTasks = Task.WhenAll(taskSum, taskDelay);
            if(twoTasks.IsCompleted) 
            {
                var end = DateTime.Now;
                Console.WriteLine("Result {0}", taskSum.Result);
                Console.WriteLine("Time taken! {0}", end - start);
            }
            Console.WriteLine("Time taken!");
        }
    }
}
