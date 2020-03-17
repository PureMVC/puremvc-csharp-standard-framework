//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Core
{
    /// <summary>
    /// Test the PureMVC Controller class.
    /// </summary>
    /// <seealso cref="ControllerTestVO"/>
    /// <seealso cref="ControllerTestCommand"/>
    [TestClass]
    public class ControllerTest
    {
        /// <summary>
        /// Tests the Controller Singleton Factory Method 
        /// </summary>
        [TestMethod]
        public void TestGetInstance()
        {
            // Test Factory Method
            var controller = Controller.GetInstance(() => new Controller());

            // test assertions
            Assert.IsNotNull(controller, "Expecting instance not null");
            Assert.IsTrue(controller != null, "Expecting instance implements IController");
        }

        /// <summary>
        /// Tests Command registration and execution.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test gets a Singleton Controller instance 
        ///         and registers the ControllerTestCommand class
        ///         to handle 'ControllerTest' Notifications.
        ///     </para>
        ///     <para>
        ///          It then constructs such a Notification and tells the 
        ///          Controller to execute the associated Command.
        ///          Success is determined by evaluating a property
        ///          on an object passed to the Command, which will
        ///          be modified when the Command executes.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterAndExecuteCommand()
        {
            // Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
            var controller = Controller.GetInstance(() => new Controller());
            controller.RegisterCommand("ControllerTest", () => new ControllerTestCommand() );

            // Create a 'ControllerTest' notification
            var vo = new ControllerTestVO(12);
            var note = new Notification("ControllerTest", vo);

            // Tell the controller to execute the Command associated with the notification
            // the ControllerTestCommand invoked will multiply the vo.input value
            // by 2 and set the result on vo.result
            controller.ExecuteCommand(note);

            // test assertions 
            Assert.IsTrue(vo.result == 24, "Expecting vo.result == 24");
        }

        /// <summary>
        /// Tests Command registration and removal.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tests that once a Command is registered and verified
        ///         working, it can be removed from the Controller.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterAndRemoveCommand()
        {
            // Create the controller, register the ControllerTestCommand to handle 'ControllerTest' notes
            var controller = Controller.GetInstance(() => new Controller());
            controller.RegisterCommand("ControllerRemoveTest", () => new ControllerTestCommand());

            // Create a 'ControllerTest' note
            var vo = new ControllerTestVO(12);
            INotification note = new Notification("ControllerRemoveTest", vo);

            // Tell the controller to execute the Command associated with the note
            // the ControllerTestCommand invoked will multiply the vo.input value
            // by 2 and set the result on vo.result

            controller.ExecuteCommand(note);

            // test assertions
            Assert.AreEqual(24, vo.result, "Expecting vo.result == 24");

            // Reset result
            vo.result = 0;

            // Remove the Command from the Controller
            controller.RemoveCommand("ControllerRemoveTest");

            // Tell the controller to execute the Command associated with the
            // note. This time, it should not be registered, and our vo result
            // will not change 
            controller.ExecuteCommand(note);

            // test assertions
            Assert.IsTrue(vo.result == 0, "Expecting vo.result == 0");
        }

        /// <summary>
        /// Test hasCommand method.
        /// </summary>
        [TestMethod]
        public void TestHasCommand()
        {
            // register the ControllerTestCommand to handle 'hasCommandTest' notes
            var controller = Controller.GetInstance(() => new Controller());
            controller.RegisterCommand("HasCommandTest", () => new ControllerTestCommand());

            // test that hasCommand returns true for hasCommandTest notifications 
            Assert.IsTrue(controller.HasCommand("HasCommandTest") == true, "Expecting controller.HasCommand('HasCommandTest') == true");

            // Remove the Command from the Controller
            controller.RemoveCommand("HasCommandTest");

            // test that hasCommand returns false for hasCommandTest notifications 
            Assert.IsTrue(controller.HasCommand("HasCommandTest") == false, "Expecting controller.HasCommand('HasCommandTest') == false");
        }

        /// <summary>
        /// Tests Removing and Reregistering a Command
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tests that when a Command is re-registered that it isn't fired twice.
        ///         This involves, minimally, registration with the controller but
        ///         notification via the View, rather than direct execution of
        ///         the Controller's executeCommand method as is done above in 
        ///         testRegisterAndRemove.The bug under test was fixed in AS3 Standard
        ///         Version 2.0.2.If you run the unit tests with 2.0.1 this
        ///         test will fail.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestReRegisterAndExecuteCommand()
        {
            // Fetch the controller, register the ControllerTestCommand2 to handle 'ControllerTest2' notes
            var controller = Controller.GetInstance(() => new Controller());
            controller.RegisterCommand("ControllerTest2", () => new ControllerTestCommand2());

            // Remove the Command from the Controller
            controller.RemoveCommand("ContrllerTest2");

            // Re-register the Command with the Controller
            controller.RegisterCommand("ControllerTest2", () => new ControllerTestCommand2());

            // Create a 'ControllerTest2' note
            var vo = new ControllerTestVO(12);
            var note = new Notification("ControllerTest2", vo);

            // retrieve a reference to the View from the same core.
            var view = View.GetInstance(() => new View());

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
    }
}
