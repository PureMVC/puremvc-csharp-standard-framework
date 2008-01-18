using System;
using System.IO;
using System.Diagnostics;

using NUnitLite.Runner;

namespace org.puremvc.csharp.tests
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
