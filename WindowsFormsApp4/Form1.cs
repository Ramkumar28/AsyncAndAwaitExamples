using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cancellationTask = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Program Begin");
            // CallDoSomethingAsTask();
            //TaskWhenAllExample();
            // await CallDoSomethingAsAsyncAndAwait();

            await TaskWiThCancellationExample(cancellationTask);
            Debug.WriteLine("Program End");
        }
        private static void CallDoSomethingAsTask()
        {
            Debug.WriteLine("I gonna call the method as Task");
            var result = Task.Factory.StartNew<int>(() => DoSomethingInAsync());
            Debug.WriteLine("Task Started");
            result.Wait();
            Debug.WriteLine("Task output result" + result.Result);
        }

        private static void TaskWhenAllExample()
        {
            Debug.WriteLine("I gonna call the method as Task.WaitAll()");
            Task[] tasks = new Task[3];
            for (var i = 0; i < 3; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => DoSomething());
            }
            Task.WaitAll(tasks);
        }

        private static Task TaskWiThCancellationExample(CancellationTokenSource cancellationTokenSource)
        {
            Debug.WriteLine("I am inside the TaskWithCancellationExample");
            return Task.Factory.StartNew(() =>
            {
                for (int o = 0; o < 100000; o++)
                {
                    if (!cancellationTokenSource.IsCancellationRequested)
                    {
                        Debug.WriteLine(o);
                    }
                }
            });
        }


        private static async Task CallDoSomethingAsAsyncAndAwait()
        {
            Debug.WriteLine("I gonna call the method as async");
            Task[] taskArray = new Task[4];
            var result = Task.Factory.StartNew<int>(() => DoSomethingInAsync());
            Debug.WriteLine("Task Started");
            var resultOfAwait = await result;
            Debug.WriteLine("Task output result" + resultOfAwait);
        }
        private static int DoSomethingInAsync()
        {
            Debug.WriteLine("I am inside the Dosomething Async Method - Gonna Sleep");
            Thread.Sleep(5000);
            Debug.WriteLine("Gonna End the method");
            return 123;
        }

        private static void DoSomething()
        {
            Debug.WriteLine("I am inside the Dosomething Method - Gonna Sleep");
            Thread.Sleep(5000);
            Debug.WriteLine("Gonna End the Dosomething Method");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancellationTask.Cancel();
        }
    }
}
