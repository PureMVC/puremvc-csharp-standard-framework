using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.observer
{
    /**
  	 * A Notification class used by NotificationTest.
  	 * 
  	 * @see org.puremvc.csharp.patterns.observer.NotificationTest NotificationTest
  	 */
    public class NotificationTestNote : Notification, INotification
    {
        /**
		 * The name of this Notification.
		 */
		public const String NAME = "NotificationTestNote";
		
		/**
		 * Constructor.
		 * 
		 * <P>
		 * The INotification interface dictates the constructor should take
		 * name and body parameters. You could use PureMVC without
		 * creating any custom note classes, always creating a new
		 * Notification by passing in its name and body, like this:
		 * <listing>
		 * var note:INotification = new Notification('UpdateNote', bodyObject);
		 * </listing>
		 * or:
		 * <listing>
		 * facade.notifyObservers( new Notification('UpdateNote', bodyObject) );
		 * </listing>
		 * </P>
		 * 
		 * <P>
		 * There are a number of reasons for creating custom notification classes, 
		 * however.
		 * <UL>
		 * <LI>It helps to expose the Notifications your application is using 
		 * at a class level so that they can be documented.</LI>
		 * <LI>Generic Notification creation can be error prone if a type-safe 
		 * convention for naming isn't used, which means you need to store note name
		 * constants somewhere that your whole application can see them.</LI>
		 * <LI>The idiom for creating a custom <code>INotification</code> can be 
		 * made simpler by adding a Factory method, as this note does.</LI> </P>
		 * 
		 * <P>
		 * The idiom to create the custom INotification and notify Observers is then:
		 * <listing>
		 * var note:INotification = UpdateNote.create(bodyObject);
		 * facade.notifyObservers( note );
		 * </listing>
		 * or more simply:
		 * <listing>
		 * facade.notifyObservers( UpdateNote.create(bodyObject) );
		 * </listing>
		 * and to register a Command to be executed when the Controller is notified:
		 * <listing>
		 * facade.registerCommand( UpdateNote.NAME, com.me.myapp.commands.UpdateCommand );
		 * </listing>
		 * </P>
		 * 
		 * @param noteName Ignored and forced to NAME.
		 * @param body the body of the Notification to be constructed.
		 * @param type the type of Notification
		 */
        public NotificationTestNote(String noteName)
            : base(noteName)
		{ }

        public NotificationTestNote(String noteName, Object body)
            : base(noteName, body)
		{ }

		public NotificationTestNote(String noteName, Object body, String type)
            : base(noteName, body, type)
		{ }
		
		/**
		 * Factory method.
		 * 
		 * <P> 
		 * This method creates new instances of the NotificationTestNote class,
		 * automatically setting the note name so you don't have to. Use
		 * this as an alternative to the constructor.</P>
		 * 
		 * @param noteName the name of the Notification to be constructed.
		 * @param body the body of the Notification to be constructed.
		 */
		public static INotification create(Object body)
		{
			return new NotificationTestNote(NAME, body);
		}

        public static INotification create(Object body, String type)
        {
            return new NotificationTestNote(NAME, body, type);
        }
    }
}
