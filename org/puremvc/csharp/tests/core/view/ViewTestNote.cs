using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core.view
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
		public const String NAME = "ViewTestNote";
		
		/**
		 * Constructor.
		 * 
		 * @param name Ignored and forced to NAME.
		 * @param body the body of the Notification to be constructed.
		 */
		public ViewTestNote( String name, Object body )
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
		 * @param name the name of the Notification to be constructed.
		 * @param body the body of the Notification to be constructed.
		 */
		public static INotification create(Object body) 		
		{
			return new ViewTestNote(NAME, body);
		}
    }
}
