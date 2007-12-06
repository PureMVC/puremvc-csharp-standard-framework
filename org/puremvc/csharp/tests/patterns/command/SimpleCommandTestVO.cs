using System;

namespace org.puremvc.csharp.patterns.command
{
    /**
  	 * A utility class used by SimpleCommandTest.
  	 * 
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTest SimpleCommandTest
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestCommand SimpleCommandTestCommand
  	 */
    public class SimpleCommandTestVO
    {
        /**
		 * Constructor.
		 * 
		 * @param input the number to be fed to the SimpleCommandTestCommand
		 */
		public SimpleCommandTestVO (int input)
        {
			this.input = input;
		}

		public int input;
        public int result;
    }
}
