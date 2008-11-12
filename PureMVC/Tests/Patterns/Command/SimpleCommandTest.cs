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
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestVO SimpleCommandTestVO
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestCommand SimpleCommandTestCommand
	 */
	[TestClass]
	public class SimpleCommandTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public SimpleCommandTest()
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
  		 * Tests the <code>execute</code> method of a <code>SimpleCommand</code>.
  		 * 
  		 * <P>
  		 * This test creates a new <code>Notification</code>, adding a 
  		 * <code>SimpleCommandTestVO</code> as the body. 
  		 * It then creates a <code>SimpleCommandTestCommand</code> and invokes
  		 * its <code>execute</code> method, passing in the note.</P>
  		 * 
  		 * <P>
  		 * Success is determined by evaluating a property on the 
  		 * object that was passed on the Notification body, which will
  		 * be modified by the SimpleCommand</P>.
  		 * 
  		 */
		[TestMethod]
		[Description("Command Tests")]
		public void SimpleCommandExecute()
        {
  			// Create the VO
  			SimpleCommandTestVO vo = new SimpleCommandTestVO(5);
  			
  			// Create the Notification (note)
  			INotification note = new Notification("SimpleCommandTestNote", vo);

			// Create the SimpleCommand  			
			ICommand command = new SimpleCommandTestCommand();
   			
   			// Execute the SimpleCommand
   			command.Execute(note);
   			
   			// test assertions
            Assert.IsTrue(vo.result == 10, "Expecting vo.result == 10");
   		}
    }
}
