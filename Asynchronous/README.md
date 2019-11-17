When we run synchronous code we block the main Thread from doing anything else than just what's it's doing currently. This makes your software and user experience slower than it needs to be.

a library called TPL, Task Parallel Library that lives on top of the Thread model and makes it really easy to schedule and manage work.

It's part of the `System.Threading` and `System.Threading.Tasks` namespaces. It does a lot for us like:
- __Partitioning__ of the work
- __Scheduling__ of threads on the ThreadPool
- __Cancellation__ support
- __State__ management

__Task__, a task represent an asynchronous operation, like fetching content from a file or doing a calculation that takes time. There are some interesting properties on a Task that allows us to communicate to a UI, for example, how the asynchronous work is doing, like:
- __Status__, this can tell us if it's currently working on something, is done, errored out or it was canceled
- __IsCanceled__, if canceled this would be set to true
- __IsFaulted__, if something went wrong, like an exception, this would be set to true
- __IsCompleted__, once it has finished its operation it would be set to true

__Async/Await__. The `await` keyword means that we wait for the asynchronous operation to end and by the end of the operation we are given the result

__Blocking/Non-blocking__. When we use Tasks we are not blocking and other Threads can carry out work. There are exceptions though when we use the method Wait() on a Task. Then we are forcing the code to run synchronously.

# Authoring methods

__Return type__, `Task<int>`. This tells us that it will be a Task that once resolved will return something of type `int`.

`Task.FromResult()`, This creates a Task given a value. We give it the calculation to perform

__Async/Await__, we use the `async` keyword inside of the method to wait for the result to arrive back to us. This needs to be followed by the `async` keyword to ensure the compiler is happy.

`Task.FromResult` is used when the answer is immediately known so it's status is `RanToCompletion` and the answer is available right away so you can argue that `await` is unnecessary, it's already available on the `Task.Result` property. Another way to do the above is `Task.Run()`

# Control flow

`Task.WaitAll()`, this one takes a list of Tasks in. What you are essentially saying is that all tasks need to finish before we can carry on, it's blocking. You can see that by it returning void A typical use-case is to wait for all Web Requests to finish cause we want to return a result that consists of us stitching all their data together

`Task.WaitAny()`, we give it a list of Tasks here as well but the meaning is different. We say that as long as any of the Task has finished we are good. This usually a race for data towards an endpoint or search for a file/file content on a disk. We don't care who finished first, as long as we get a response. This is also blocking and waiting for one of the Tasks to finish

`Task.WhenAll()`, this gives you a `Task` back that you can interact with. When all of the tasks have finished it will resolve.

`Task.WhenAny()`, this gives you a `Task` back that you can interact with. When one of the Tasks has finished then it will resolve.

# Using Async APIs

# Blocking code

`WaitAll` and `WaitAny` blocks, the rule of thumb here seems to be that they return void and use the word Wait.... Sometimes you want it to wait though, so learn to be intentional with block/non-block

`task.Result`, this also blocks and waits for the result to be available

`Wait()`, this method on a Task will block and cause you to wait here until the code has finished, for example `Task.Delay(2000).Wait()`
