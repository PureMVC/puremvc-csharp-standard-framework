/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite;
using NUnit.Framework;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC SimpleCommand class.
	 *
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestVO SimpleCommandTestVO
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestCommand SimpleCommandTestCommand
	 */
    [TestFixture]
    public class SimpleCommandTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public SimpleCommandTest(string methodName)
            : base(methodName)
        { }

        /**
		 * Create the TestSuite.
		 */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(SimpleCommandTest));

                ts.AddTest(new SimpleCommandTest("TestSimpleCommandExecute"));

                return ts;
            }
        }

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
  		public void TestSimpleCommandExecute()
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
            Assert.True(vo.result == 10, "Expecting vo.result == 10");
   		}
    }
}
