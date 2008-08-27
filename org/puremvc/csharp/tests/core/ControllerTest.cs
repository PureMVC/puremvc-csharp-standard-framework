/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core
{
    /**
	 * Test the PureMVC Controller class.
	 *
  	 * @see org.puremvc.csharp.core.controller.ControllerTestVO ControllerTestVO
  	 * @see org.puremvc.csharp.core.controller.ControllerTestCommand ControllerTestCommand
	 */
    [TestFixture]
    public class ControllerTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ControllerTest(String methodName)
            : base(methodName)
        { }

        /**
		 * Create the TestSuite.
		 */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(ControllerTest));

                ts.AddTest(new ControllerTest("testGetInstance"));
                ts.AddTest(new ControllerTest("testRegisterAndExecuteCommand"));
                ts.AddTest(new ControllerTest("testRegisterAndRemoveCommand"));
				ts.AddTest(new ControllerTest("testHasCommand"));
				ts.AddTest(new ControllerTest("testReregisterAndExecuteCommand"));

                return ts;
            }
        }

        /**
  		 * Tests the Controller Singleton Factory Method 
  		 */
  		public void testGetInstance()
        {
   			// Test Factory Method
   			IController controller = Controller.getInstance();
   			
   			// test assertions
            Assert.NotNull(controller, "Expecting instance not null");
            Assert.True(controller is IController, "Expecting instance implements IController");
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
  		public void testRegisterAndExecuteCommand() 
        {
   			// Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
   			IController controller = Controller.getInstance();
   			controller.registerCommand("ControllerTest", typeof(ControllerTestCommand));
   			
   			// Create a 'ControllerTest' note
            ControllerTestVO vo = new ControllerTestVO(12);
   			INotification note = new Notification("ControllerTest", vo);

			// Tell the controller to execute the Command associated with the note
			// the ControllerTestCommand invoked will multiply the vo.input value
			// by 2 and set the result on vo.result
   			controller.executeCommand(note);
   			
   			// test assertions 
            Assert.True(vo.result == 24, "Expecting vo.result == 24");
   		}

        /**
  		 * Tests Command registration and removal.
  		 * 
  		 * <P>
  		 * Tests that once a Command is registered and verified
  		 * working, it can be removed from the Controller.</P>
  		 */
  		public void testRegisterAndRemoveCommand()
        {
  			
   			// Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
   			IController controller = Controller.getInstance();
   			controller.registerCommand("ControllerRemoveTest", typeof(ControllerTestCommand));
   			
   			// Create a 'ControllerTest' note
            ControllerTestVO vo = new ControllerTestVO(12);
   			INotification note = new Notification("ControllerRemoveTest", vo);

			// Tell the controller to execute the Command associated with the note
			// the ControllerTestCommand invoked will multiply the vo.input value
			// by 2 and set the result on vo.result
   			controller.executeCommand(note);
   			
   			// test assertions 
   			Assert.True(vo.result == 24, "Expecting vo.result == 24");
   			
   			// Reset result
   			vo.result = 0;
   			
   			// Remove the Command from the Controller
   			controller.removeCommand("ControllerRemoveTest");
			
			// Tell the controller to execute the Command associated with the
			// note. This time, it should not be registered, and our vo result
			// will not change   			
   			controller.executeCommand(note);
   			
   			// test assertions 
            Assert.True(vo.result == 0, "Expecting vo.result == 0");
   			
   		}
  		
  		/**
  		 * Test hasCommand method.
  		 */
  		public void testHasCommand() {
   			// register the ControllerTestCommand to handle 'hasCommandTest' notes
   			IController controller = Controller.getInstance();
   			controller.registerCommand("hasCommandTest", typeof(ControllerTestCommand));
   			
   			// test that hasCommand returns true for hasCommandTest notifications 
   			Assert.True(controller.hasCommand("hasCommandTest") == true, "Expecting controller.hasCommand('hasCommandTest') == true");
   			
   			// Remove the Command from the Controller
   			controller.removeCommand("hasCommandTest");
			
   			// test that hasCommand returns false for hasCommandTest notifications 
			Assert.True(controller.hasCommand("hasCommandTest") == false, "Expecting controller.hasCommand('hasCommandTest') == false");
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
  		public void testReregisterAndExecuteCommand() {
  			 
   			// Fetch the controller, register the ControllerTestCommand2 to handle 'ControllerTest2' notes
   			IController controller = Controller.getInstance();
   			controller.registerCommand("ControllerTest2", typeof(ControllerTestCommand2));
   			
   			// Remove the Command from the Controller
   			controller.removeCommand("ControllerTest2");
			
   			// Re-register the Command with the Controller
   			controller.registerCommand("ControllerTest2", typeof(ControllerTestCommand2));

   			// Create a 'ControllerTest2' note
			ControllerTestVO vo = new ControllerTestVO(12);
   			Notification note = new Notification( "ControllerTest2", vo );

			// retrieve a reference to the View.
   			IView view = View.getInstance();
   			
			// send the Notification
   			view.notifyObservers(note);
   			
   			// test assertions 
			// if the command is executed once the value will be 24
			Assert.True(vo.result == 24, "Expecting vo.result == 24");

   			// Prove that accumulation works in the VO by sending the notification again
   			view.notifyObservers(note);
   			
			// if the command is executed twice the value will be 48
			Assert.True(vo.result == 48, "Expecting vo.result == 48");
   			
   		}
    }
}
