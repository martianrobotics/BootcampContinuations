using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampContinuations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            //Scenario0();
            //Scenario1();
            //Scenario2();
            //Scenario3();
            Scenario4();
            Console.WriteLine("Press any key to exit....");
            Console.ReadKey();
        }

        private static void Scenario4()
        {
            //((Step1|Step2)|Step3)
            Console.WriteLine("Scenario 4");
            Task step1Task = Task.Run(() => Step1());
            Task step2Task = Task.Run(() => Step2());
            Task step3Task = Task.Factory.ContinueWhenAny(new Task[] { step1Task, step2Task },
                (previousTasks) => Step3());
            step3Task.Wait();
        }

        private static void Scenario3()
        {
            //((Step1|Step2)&Step3)
            Console.WriteLine("Scenario 3");
            Task step1Task = Task.Run(() => Step1());
            Task step2Task = Task.Run(() => Step2());
            Task step3Task = Task.Factory.ContinueWhenAll(new Task[] { step1Task, step2Task },
                (previousTasks) => Step3());
            step3Task.Wait();
        }

        private static void Scenario2()
        {
            // (Step1|Step2) & (Step1 & Step3)
            Console.WriteLine("Scenario 2");
            Task step1Task = Task.Run(() => Step1());
            Task step2Task = Task.Run(() => Step2());
            Task step3Task = step1Task.ContinueWith((previousTask) => Step3());
            Task.WaitAll(step2Task, step3Task);
        }

        private static void Scenario1()
        {
            // Step1|Step2|Step3
            Console.WriteLine("Scenario 1");
            Parallel.Invoke(Step1, Step2, Step3);
        }

        private static void Scenario0()
        {
            // no pre-requisites required
            Console.WriteLine("Scenario 0");
            Console.WriteLine("----------");
            Step1();Step2();Step3();
        }

        static void Step1()
        {
            Console.WriteLine("Step 1");
        }
        static void Step2()
        {
            Console.WriteLine("Step 2");
        }
        static void Step3()
        {
            Console.WriteLine("Step 3");
        }
    }
}
