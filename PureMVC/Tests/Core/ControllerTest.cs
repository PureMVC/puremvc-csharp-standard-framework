/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Threading;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Core;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Core
{
	/**
	 * Test the PureMVC Controller class.
	 */
	[TestClass]
	public class ControllerTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public ControllerTest()
        { }

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

        /**
  		 * Tests the Controller Singleton Factory Method 
  		 */
		[TestMethod]
		[Description("Controller Tests")]
		public void GetInstance()
        {
   			// Test Factory Method
   			IController controller = Controller.Instance;
   			
   			// test assertions
            Assert.IsNotNull(controller, "Expecting instance not null");
            Assert.IsTrue(controller is IController, "Expecting instance implements IController");
   		}

        /**
  		 * Tests Command registration and execution.
  		 * 
  		 * <P>
  		 * This test gets the Singleton Controller instance 
  		 * and registers the ControllerTestCommand class 
  		 * to handle 'ControllerTest' Notifications.<P>
  		 * 
  		 * <P>
  		 * It then constructs such a Notification and tells the 
  		 * Controller to execute the associated Command.
  		 * Success is determined by evaluating a property
  		 * on an object passed to the Command, which will
  		 * be modified when the Command executes.</P>
  		 * 
  		 */
		[TestMethod]
		[Description("Controller Tests")]
		public void RegisterAndExecuteCommand() 
        {
   			// Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
   			IController controller = Controller.Instance;
			string name = "ControllerTest" + Thread.CurrentThread.Name;
   			controller.RegisterCommand(name, typeof(ControllerTestCommand));
   			
   			// Create a 'ControllerTest' note
            ControllerTestVO vo = new ControllerTestVO(12);
   			INotification note = new Notification(name, vo);

			// Tell the controller to execute the Command associated with the note
			// the ControllerTestCommand invoked will multiply the vo.input value
			// by 2 and set the result on vo.result
   			controller.ExecuteCommand(note);
   			
   			// test assertions 
            Assert.IsTrue(vo.result == 24, "Expecting vo.result == 24");
   		}

        /**
  		 * Tests Command registration and removal.
  		 * 
  		 * <P>
  		 * Tests that once a Command is registered and verified
  		 * working, it can be removed from the Controller.</P>
  		 */
		[TestMethod]
		[Description("Controller Tests")]
		public void RegisterAndRemoveCommand()
        {
  			
   			// Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
   			IController controller = Controller.Instance;
			string name = "ControllerRemoveTest" + Thread.CurrentThread.Name;
			controller.RegisterCommand(name, typeof(ControllerTestCommand));
   			
   			// Create a 'ControllerTest' note
            ControllerTestVO vo = new ControllerTestVO(12);
   			INotification note = new Notification(name, vo);

			// Tell the controller to execute the Command associated with the note
			// the ControllerTestCommand invoked will multiply the vo.input value
			// by 2 and set the result on vo.result
   			controller.ExecuteCommand(note);
   			
   			// test assertions 
   			Assert.IsTrue(vo.result == 24, "Expecting vo.result == 24");
   			
   			// Reset result
   			vo.result = 0;
   			
   			// Remove the Command from the Controller
   			controller.RemoveCommand(name);
			
			// Tell the controller to execute the Command associated with the
			// note. This time, it should not be registered, and our vo result
			// will not change   			
   			controller.ExecuteCommand(note);
   			
   			// test assertions 
            Assert.IsTrue(vo.result == 0, "Expecting vo.result == 0");
   			
   		}
  		
  		/**
  		 * Test hasCommand method.
  		 */
		[TestMethod]
		[Description("Controller Tests")]
		public void HasCommand()
		{
   			// register the ControllerTestCommand to handle 'hasCommandTest' notes
   			IController controller = Controller.Instance;
			string name = "HasCommandTest" + Thread.CurrentThread.Name;
			controller.RegisterCommand(name, typeof(ControllerTestCommand));
   			
   			// test that hasCommand returns true for hasCommandTest notifications 
			Assert.IsTrue(controller.HasCommand(name) == true, "Expecting controller.HasCommand(name) == true");
   			
   			// Remove the Command from the Controller
			controller.RemoveCommand(name);
			
   			// test that hasCommand returns false for hasCommandTest notifications 
			Assert.IsTrue(controller.HasCommand(name) == false, "Expecting controller.HasCommand(name) == false");
   		}
   		
 		/**
  		 * Tests Removing and Reregistering a Command
  		 * 
  		 * <P>
  		 * Tests that when a Command is re-registered that it isn't fired twice.
  		 * This involves, minimally, registration with the controller but
  		 * notification via the View, rather than direct execution of
  		 * the Controller's executeCommand method as is done above in 
  		 * testRegisterAndRemove. The bug under test was fixed in AS3 Standard 
  		 * Version 2.0.2. If you run the unit tests with 2.0.1 this
  		 * test will fail.</P>
  		 */
		[TestMethod]
		[Description("Controller Tests")]
		public void ReregisterAndExecuteCommand()
		{
  			 
   			// Fetch the controller, register the ControllerTestCommand2 to handle 'ControllerTest2' notes
   			IController controller = Controller.Instance;
			string name = "ControllerTest2" + Thread.CurrentThread.Name;
			controller.RegisterCommand(name, typeof(ControllerTestCommand2));
   			
   			// Remove the Command from the Controller
			controller.RemoveCommand(name);
			
   			// Re-register the Command with the Controller
			controller.RegisterCommand(name, typeof(ControllerTestCommand2));

   			// Create a 'ControllerTest2' note
			ControllerTestVO vo = new ControllerTestVO(12);
			Notification note = new Notification(name, vo);

			// retrieve a reference to the View.
   			IView view = View.Instance;
   			
			// send the Notification
   			view.NotifyObservers(note);
   			
   			// test assertions 
			// if the command is executed once the value will be 24
			Assert.IsTrue(vo.result == 24, "Expecting vo.result == 24");

   			// Prove that accumulation works in the VO by sending the notification again
   			view.NotifyObservers(note);
   			
			// if the command is executed twice the value will be 48
			Assert.IsTrue(vo.result == 48, "Expecting vo.result == 48");
   		}

		/// <summary>
		/// Test all of the function above using many threads at once.
		/// </summary>
		[TestMethod]
		[Description("Controller Tests")]
		public void MultiThreadedOperations()
		{
			count = 20;
			IList<Thread> threads = new List<Thread>();

			for (int i = 0; i < count; i++) {
				Thread t = new Thread(new ThreadStart(MultiThreadedTestFunction));
				t.Name = "ControllerTest" + i;
				threads.Add(t);
			}

			foreach (Thread t in threads)
			{
				t.Start();
			}

			while (true)
			{
				if (count <= 0) break;
				Thread.Sleep(100);
			}
		}

		private int count = 0;

		private int threadIterationCount = 10000;

		private void MultiThreadedTestFunction()
		{
			for (int i = 0; i < threadIterationCount; i++)
			{
				// All we need to do is test the registration and removal of commands.
				RegisterAndExecuteCommand();
				RegisterAndRemoveCommand();
				HasCommand();
				ReregisterAndExecuteCommand();
			}

			count--;
		}
	}
}
