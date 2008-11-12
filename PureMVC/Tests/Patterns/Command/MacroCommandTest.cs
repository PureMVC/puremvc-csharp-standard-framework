/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC SimpleCommand class.
	 *
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestVO MacroCommandTestVO
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestCommand MacroCommandTestCommand
	 */
    [TestClass]
    public class MacroCommandTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
  	    public MacroCommandTest() 
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
  		 * Tests operation of a <code>MacroCommand</code>.
  		 * 
  		 * <P>
  		 * This test creates a new <code>Notification</code>, adding a 
  		 * <code>MacroCommandTestVO</code> as the body. 
  		 * It then creates a <code>MacroCommandTestCommand</code> and invokes
  		 * its <code>execute</code> method, passing in the 
  		 * <code>Notification</code>.<P>
  		 * 
  		 * <P>
  		 * The <code>MacroCommandTestCommand</code> has defined an
  		 * <code>initializeMacroCommand</code> method, which is 
  		 * called automatically by its constructor. In this method
  		 * the <code>MacroCommandTestCommand</code> adds 2 SubCommands
  		 * to itself, <code>MacroCommandTestSub1Command</code> and
  		 * <code>MacroCommandTestSub2Command</code>.
  		 * 
  		 * <P>
  		 * The <code>MacroCommandTestVO</code> has 2 result properties,
  		 * one is set by <code>MacroCommandTestSub1Command</code> by
  		 * multiplying the input property by 2, and the other is set
  		 * by <code>MacroCommandTestSub2Command</code> by multiplying
  		 * the input property by itself. 
  		 * 
  		 * <P>
  		 * Success is determined by evaluating the 2 result properties
  		 * on the <code>MacroCommandTestVO</code> that was passed to 
  		 * the <code>MacroCommandTestCommand</code> on the Notification 
  		 * body.</P>
  		 * 
  		 */
		[TestMethod]
		[Description("Command Tests")]
		public void MacroCommandExecute()
        {
  			// Create the VO
  			MacroCommandTestVO vo = new MacroCommandTestVO(5);
  			
  			// Create the Notification (note)
  			INotification note = new Notification("MacroCommandTest", vo);

			// Create the SimpleCommand  			
			ICommand command = new MacroCommandTestCommand();
   			
   			// Execute the SimpleCommand
   			command.Execute(note);
   			
   			// test assertions
   			Assert.IsTrue(vo.result1 == 10, "Expecting vo.result1 == 10");
            Assert.IsTrue(vo.result2 == 25, "Expecting vo.result2 == 25");
   		}
    }
}
