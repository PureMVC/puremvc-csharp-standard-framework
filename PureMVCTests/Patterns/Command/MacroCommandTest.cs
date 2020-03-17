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
    /// <seealso cref="MacroCommandTestVO"/>
    /// <seealso cref="MacroCommandTestCommand"/>
    [TestClass]
    public class MacroCommandTest
    {
        /// <summary>
        /// Tests operation of a<c>MacroCommand</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     This test creates a new <c>Notification</c>, adding a
        ///     <c>MacroCommandTestVO</c> as the body. 
        ///     It then creates a <c>MacroCommandTestCommand</c> and invokes
        ///     its <c>execute</c> method, passing in the
        ///     <c> Notification</c>.
        /// </para>
  		/// <para>
        ///     The<c> MacroCommandTestCommand</c> has defined an
        ///     <c> initializeMacroCommand</c> method, which is 
        ///     called automatically by its constructor.In this method
        ///     the <c>MacroCommandTestCommand</c> adds 2 SubCommands
        ///     to itself, <c>MacroCommandTestSub1Command</c> and
  		///     <c>MacroCommandTestSub2Command</c>.
        /// </para>
  		/// <para>
        ///     The<c> MacroCommandTestVO</c> has 2 result properties,
        ///     one is set by <c>MacroCommandTestSub1Command</c> by
        ///     multiplying the input property by 2, and the other is set
        ///     by <c>MacroCommandTestSub2Command</c> by multiplying
        ///     the input property by itself. 
        /// </para>
  		/// <para>
        ///     Success is determined by evaluating the 2 result properties
        ///     on the <c>MacroCommandTestVO</c> that was passed to
        ///     the <c>MacroCommandTestCommand</c> on the Notification
        ///     body.
        /// </para>
        /// </remarks>
       [TestMethod]
        public void TestMacroCommandExecute()
        {
            // Create the VO
            var vo = new MacroCommandTestVO(5);

            // Create the Notification (notification)
            var note = new Notification("MacroCommandTest", vo);

            // Create the SimpleCommand  			
            var command = new MacroCommandTestCommand();

            // Execute the SimpleCommand
            command.Execute(note);

            // test assertions
            Assert.IsTrue(vo.Result1 == 10, "Expecting vo.Result1 == 10");
            Assert.IsTrue(vo.Result2 == 25, "Expecting vo.Result2 == 25");
        }
    }
}
