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
  	 * @see org.puremvc.csharp.core.view.ViewTest ViewTest
  	 */
    public class ViewTestMediator : Mediator, IMediator
    {
        /**
		 * The Mediator name
		 */
		public new static string NAME = "ViewTestMediator";
		
		/**
		 * Constructor
		 */
		public ViewTestMediator(string threadName, object view) 
            : base(NAME + threadName, view)
        { }

		override public IList<string> ListNotificationInterests()
		{
			// be sure that the mediator has some Observers created
			// in order to test removeMediator
			return new List<string>(new string[]{"ABC", "DEF", "GHI"});
		}
    }
}
