/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Patterns
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
		public override void Execute(INotification note)
		{
			MacroCommandTestVO vo = (MacroCommandTestVO) note.Body;
			
			// Fabricate a result
			vo.result1 = 2 * vo.input;
		}
    }
}
