/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;

namespace org.puremvc.csharp.core.view
{
    /**
  	 * A Mediator class used by ViewTest.
  	 * 
  	 * @see org.puremvc.csharp.core.view.ViewTest ViewTest
  	 */
    public class ViewTestMediator3 : Mediator, IMediator
    {
        /**
		 * The Mediator name
		 */
        public new static String NAME = "ViewTestMediator3";

        /**
         * Constructor
         */
        public ViewTestMediator3(Object view)
            : base(NAME, view)
        { }

        override public IList listNotificationInterests()
        {
            // be sure that the mediator has some Observers created
            // in order to test removeMediator
            return new ArrayList(new string[] { ViewTest.NOTE3 });
        }

        override public void handleNotification(INotification notification)
		{
			viewTest.lastNotification = notification.getName();
		}

        public ViewTest viewTest
		{
            get { return viewComponent as ViewTest; }
		}
    }
}
