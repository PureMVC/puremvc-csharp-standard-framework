//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// Test the PureMVC SimpleCommand class.
    /// </summary>
    /// <seealso cref="SimpleCommandTestVO"/>
    /// <seealso cref="SimpleCommandTestCommand"/>
    [TestClass]
    public class SimpleCommandTest
    {
        /// <summary>
        /// Tests the <c>execute</c> method of a <c>SimpleCommand</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test creates a new <c>Notification</c>, adding a 
        ///         <c>SimpleCommandTestVO</c> as the body. 
        ///         It then creates a <c>SimpleCommandTestCommand</c> and invokes
        ///         its <c>execute</c> method, passing in the note.
        ///     </para>
        ///     <para>
        ///         Success is determined by evaluating a property on the 
        ///         object that was passed on the Notification body, which will
        ///         be modified by the SimpleCommand
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestExecute()
        {
            // Create the VO
            var vo = new SimpleCommandTestVO(5);

            // Create the Notification (notification)
            var note = new Notification("SimpleCommandTestNote", vo);

            // Create the SimpleCommand  
            var command = new SimpleCommandTestCommand();

            // Execute the SimpleCommand
            command.Execute(note);

            // test assertions
            Assert.IsTrue(vo.Result == 10, "Expecting vo.Result == 10");
        }
    }
}
