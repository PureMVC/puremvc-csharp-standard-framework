using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;
using org.puremvc.csharp.patterns.command;

namespace org.puremvc.csharp.patterns.command
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
        public SimpleCommandTest(String methodName)
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

                ts.AddTest(new SimpleCommandTest("testSimpleCommandExecute"));

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
  		public void testSimpleCommandExecute()
        {
  			// Create the VO
  			SimpleCommandTestVO vo = new SimpleCommandTestVO(5);
  			
  			// Create the Notification (note)
  			INotification note = new Notification("SimpleCommandTestNote", vo);

			// Create the SimpleCommand  			
			ICommand command = new SimpleCommandTestCommand();
   			
   			// Execute the SimpleCommand
   			command.execute(note);
   			
   			// test assertions
            Assert.True(vo.result == 10, "Expecting vo.result == 10");
   		}
    }
}
