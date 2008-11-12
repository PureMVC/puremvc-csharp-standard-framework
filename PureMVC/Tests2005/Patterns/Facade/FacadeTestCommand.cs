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
		override public void Execute(INotification note)
		{
			FacadeTestVO vo = (FacadeTestVO) note.Body;
			
			// Fabricate a result
			vo.result = 2 * vo.input;
		}
    }
}
