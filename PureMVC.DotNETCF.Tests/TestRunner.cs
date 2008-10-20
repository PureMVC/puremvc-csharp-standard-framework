/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.IO;
using System.Diagnostics;

using NUnitLite.Runner;

namespace PureMVC.Tests
{
    public class TestRunner
    {
        static void Main(string[] args)
        {
            const string resultsPath = "\\PureMVC.DotNETCF.Tests.Results.txt";

            using (TextWriter writer = new StreamWriter(new FileStream(resultsPath, FileMode.Create)))
            {
                TextUI testRunner = new TextUI(writer);
                testRunner.Execute(new string[] { "PureMVC.DotNETCF.Tests, Version=0.1.0.0" });
            }
        }
    }
}
