/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;

namespace org.puremvc.csharp.core
{
   	/**
  	 * A Mediator class used by ViewTest.
  	 * 
  	 * @see org.puremvc.as3.core.view.ViewTest ViewTest
  	 */
	public class ViewTestMediator4 : Mediator, IMediator 
	{
		/**
		 * The Mediator name
		 */
		public new static String NAME = "ViewTestMediator4";
				
		/**
		 * Constructor
		 */
		public ViewTestMediator4(Object view)
			: base(NAME, view)
		{
		}

        public ViewTest viewTest
		{
            get { return viewComponent as ViewTest; }
		}
				
		public override void onRegister()
		{
			viewTest.onRegisterCalled = true;
		}
				
		public override  void onRemove()
		{
			viewTest.onRemoveCalled = true;
		}
				
				
	}
}