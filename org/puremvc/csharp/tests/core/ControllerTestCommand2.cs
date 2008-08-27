/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.command;

namespace org.puremvc.csharp.core
{
    /**
	 * A SimpleCommand subclass used by ControllerTest.
	 *
  	 * @see org.puremvc.csharp.core.controller.ControllerTest ControllerTest
  	 * @see org.puremvc.csharp.core.controller.ControllerTestVO ControllerTestVO
	 */
    public class ControllerTestCommand2 : SimpleCommand
    {
        /**
		 * Constructor.
		 */
        public ControllerTestCommand2()
            : base()
        { }

        /**
		 * Fabricate a result by multiplying the input by 2
		 * 
		 * @param note the note carrying the ControllerTestVO
		 */
		override public void execute( INotification note )
		{
			
			ControllerTestVO vo = note.getBody() as ControllerTestVO;

			// Fabricate a result
			vo.result = vo.result + (2 * vo.input);

		}
    }
}
