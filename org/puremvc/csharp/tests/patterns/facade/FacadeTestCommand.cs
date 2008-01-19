/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.command;

namespace org.puremvc.csharp.patterns.facade
{
    /**
	 * A SimpleCommand subclass used by FacadeTest.
	 *
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTest FacadeTest
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestVO FacadeTestVO
	 */
    public class FacadeTestCommand : SimpleCommand
    {
        /**
		 * Constructor.
		 */
		public FacadeTestCommand()
            : base()
		{ }
		
		/**
		 * Fabricate a result by multiplying the input by 2
		 * 
		 * @param note the Notification carrying the FacadeTestVO
		 */
		override public void execute(INotification note)
		{
			FacadeTestVO vo = note.getBody() as FacadeTestVO;
			
			// Fabricate a result
			vo.result = 2 * vo.input;
		}
    }
}
