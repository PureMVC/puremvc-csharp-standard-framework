using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.command;

namespace org.puremvc.csharp.patterns.command
{
    /**
	 * A SimpleCommand subclass used by MacroCommandTestCommand.
	 *
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTest MacroCommandTest
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestCommand MacroCommandTestCommand
  	 * @see org.puremvc.csharp.patterns.command.MacroCommandTestVO MacroCommandTestVO
	 */
    public class MacroCommandTestSub1Command : SimpleCommand
    {
        /**
		 * Constructor.
		 */
        public MacroCommandTestSub1Command()
            : base()
		{ }
		
		/**
		 * Fabricate a result by multiplying the input by 2
		 * 
		 * @param event the <code>IEvent</code> carrying the <code>MacroCommandTestVO</code>
		 */
		public override void execute(INotification note)
		{
			MacroCommandTestVO vo = note.getBody() as MacroCommandTestVO;
			
			// Fabricate a result
			vo.result1 = 2 * vo.input;
		}
    }
}
