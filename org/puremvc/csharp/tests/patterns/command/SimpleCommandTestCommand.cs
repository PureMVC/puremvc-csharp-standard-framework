using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.command;

namespace org.puremvc.csharp.patterns.command
{
    /**
	 * A SimpleCommand subclass used by SimpleCommandTest.
	 *
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTest SimpleCommandTest
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestVO SimpleCommandTestVO
	 */
    public class SimpleCommandTestCommand : SimpleCommand
    {
        /**
		 * Constructor.
		 */
        public SimpleCommandTestCommand()
            : base()
        { }

        /**
		 * Fabricate a result by multiplying the input by 2
		 * 
		 * @param event the <code>INotification</code> carrying the <code>SimpleCommandTestVO</code>
		 */
		public override void execute(INotification note)
		{
			SimpleCommandTestVO vo = note.getBody() as SimpleCommandTestVO;
			
			// Fabricate a result
			vo.result = 2 * vo.input;
		}
    }
}
