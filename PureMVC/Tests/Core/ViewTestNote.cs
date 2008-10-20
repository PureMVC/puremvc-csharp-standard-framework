/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Core
{
    /**
  	 * A Notification class used by ViewTest.
  	 * 
  	 * @see org.puremvc.csharp.core.view.ViewTest ViewTest
  	 */
    public class ViewTestNote : Notification, INotification
    {
        /**
		 * The name of this Notification.
		 */
		public const string NAME = "ViewTestNote";
		
		/**
		 * Constructor.
		 * 
		 * @param name Ignored and forced to NAME.
		 * @param body the body of the Notification to be constructed.
		 */
		public ViewTestNote(object body)
            : base(NAME, body)
		{ }
		
		/**
		 * Factory method.
		 * 
		 * <P> 
		 * This method creates new instances of the ViewTestNote class,
		 * automatically setting the note name so you don't have to. Use
		 * this as an alternative to the constructor.</P>
		 * 
		 * @param body the body of the Notification to be constructed.
		 */
		public static INotification create(object body) 		
		{
			return new ViewTestNote(body);
		}
    }
}
