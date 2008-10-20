/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Core
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
		public new static string NAME = "ViewTestMediator4";
				
		/**
		 * Constructor
		 */
		public ViewTestMediator4(object view)
			: base(NAME, view)
		{
		}

        public ViewTest viewTest
		{
			get { return (ViewTest) m_viewComponent; }
		}
				
		public override void OnRegister()
		{
			viewTest.onRegisterCalled = true;
		}
				
		public override  void OnRemove()
		{
			viewTest.onRemoveCalled = true;
		}
				
				
	}
}