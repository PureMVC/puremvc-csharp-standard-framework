using System;

using NUnitLite.Runner;

namespace org.puremvc.csharp.tests
{
    public class TestRunner
    {
        static void Main(string[] args)
        {
            ConsoleUI testRunner = new ConsoleUI();
            testRunner.Execute(new string[] { "PureMVC.DotNET.Tests, Version=0.1.0.0" });
            
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}
