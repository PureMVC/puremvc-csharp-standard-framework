/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite.Runner;

namespace org.puremvc.csharp.tests
{
    public class TestRunner
    {
        static void Main(string[] args)
        {
            ConsoleUI testRunner = new ConsoleUI();
            testRunner.Execute(new string[] { "PureMVC.DotNET.35.Tests, Version=0.1.0.0" });
            
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}
