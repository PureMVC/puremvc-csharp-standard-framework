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
	public class ViewTestMediator6 : Mediator, IMediator 
	{
		/**
		 * The Mediator base name
		 */
		public new static string NAME = "ViewTestMediator6";
				
		/**
		 * Constructor
		 */
		public ViewTestMediator6(string name, object view)
			: base(name, view)
		{
		}

		public override IList<string> ListNotificationInterests()
		{
			return new List<string>(new string[] { ViewTest.NOTE6 });
		}

		public override void HandleNotification(INotification note)
		{
			m_facade.RemoveMediator(MediatorName);
		}
		
		public override void OnRemove()
		{
			viewTest.counter++;
		}

		public ViewTest viewTest
		{
			get { return (ViewTest) m_viewComponent; }
		}
	}
}