using System;

namespace org.puremvc.csharp.patterns.command
{
    /**
  	 * A utility class used by MacroCommandTest.
  	 * 
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTest MacroCommandTest
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestCommand MacroCommandTestCommand
   	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestSub1Command MacroCommandTestSub1Command
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestSub2Command MacroCommandTestSub2Command
 	 */
    public class MacroCommandTestVO
    {
        /**
		 * Constructor.
		 * 
		 * @param input the number to be fed to the MacroCommandTestCommand
		 */
		public MacroCommandTestVO(int input)
        {
			this.input = input;
		}

		public int input;
        public int result1;
        public int result2;
    }
}
