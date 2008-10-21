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
			ConsoleUI testRunner = new ConsoleUI();
			testRunner.Execute(new string[] { "PureMVC.Silverlight.20.Tests, Version=3.0.0.0" });
			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
		}
	}
}
