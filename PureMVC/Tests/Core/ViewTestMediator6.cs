/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;

namespace org.puremvc.csharp.core
{
   	/**
  	 * A Mediator class used by ViewTest.
  	 * 
  	 * @see org.puremvc.as3.core.view.ViewTest ViewTest
  	 */
	public class ViewTestMediator6 : Mediator, IMediator 
	{
		/**
		 * The Mediator base name
		 */
		public new static String NAME = "ViewTestMediator6";
				
		/**
		 * Constructor
		 */
		public ViewTestMediator6(String name, Object view)
			: base(name, view)
		{
		}

		public override IList<String> listNotificationInterests()
		{
			return new List<String>(new string[] { ViewTest.NOTE6 });
		}

		public override void handleNotification(INotification note)
		{
			facade.removeMediator(getMediatorName());
		}
		
		public override void onRemove()
		{
			viewTest.counter++;
		}

		public ViewTest viewTest
		{
			get { return (ViewTest) viewComponent; }
		}
	}
}