using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShadowRuler;

namespace ShadowTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I'm Shadow Tester look at me!");

            var threadFactory = new ShadowThreadBase();
            ShadowThreads.SetFactory(threadFactory);

            ShadowThreads.CreateThread("worker");
            ShadowThreads.CreateThread("houler");

            Console.WriteLine("Threads created!");
            ShadowThreads.RunOnThread("worker", () =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("The work is done!");
            });
            ShadowThreads.RunOnThread("worker", () =>
            {
                Console.WriteLine("The work 2 is done!");
            });
            ShadowThreads.RunOnThread("worker", () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("The work 3 is done!");
            });
            ShadowThreads.RunOnThread("houler", () =>
            {
                while(true)
                {
                    Console.WriteLine("Praise the Ori!");
                    Thread.Sleep(1500);
                }
            });
            Console.WriteLine("Tasks distributed");

            
            // waiting for user input
            Console.ReadLine();
            ShadowThreads.StopAllThreads();
        }
    }
}
