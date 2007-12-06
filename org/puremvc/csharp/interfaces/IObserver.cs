using System;
using System.Reflection;

using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.interfaces
{
    /**
	 * The interface definition for a PureMVC Observer.
	 *
	 * <P>
	 * In PureMVC, <code>IObserver</code> implementors assume these responsibilities:
	 * <UL>
	 * <LI>Encapsulate the notification (callback) method of the interested object.</LI>
	 * <LI>Encapsulate the notification context (this) of the interested object.</LI>
	 * <LI>Provide methods for setting the interested object' notification method and context.</LI>
	 * <LI>Provide a method for notifying the interested object.</LI>
	 * </UL>
	 * 
	 * <P>
	 * PureMVC does not rely upon underlying event
	 * models such as the one provided with Flash,
	 * and ActionScript 3 does not have an inherent
	 * event model.</P>
	 * 
	 * <P>
	 * The Observer Pattern as implemented within
	 * PureMVC exists to support event driven communication
	 * between the application and the actors of the
	 * MVC triad.</P>
	 * 
	 * <P> 
	 * An Observer is an object that encapsulates information
	 * about an interested object with a notification method that
	 * should be called when an </code>INotification</code> is broadcast. The Observer then
	 * acts as a proxy for notifying the interested object.
	 * 
	 * <P>
	 * Observers can receive <code>Notification</code>s by having their
	 * <code>notifyObserver</code> method invoked, passing
	 * in an object implementing the <code>INotification</code> interface, such
	 * as a subclass of <code>Notification</code>.</P>
	 * 
	 * @see org.puremvc.interfaces.IView IView
	 * @see org.puremvc.interfaces.INotification INotification
	 */
    public interface IObserver
    {
        /**
		 * Set the notification method.
		 * 
		 * <P>
		 * The notification method should take one parameter of type <code>INotification</code></P>
		 * 
		 * @param notifyMethod the notification (callback) method of the interested object
		 */
        void setNotifyMethod(String notifyMethod);
		
		/**
		 * Set the notification context.
		 * 
		 * @param notifyContext the notification context (this) of the interested object
		 */
		void setNotifyContext(Object notifyContext);
		
		/**
		 * Notify the interested object.
		 * 
		 * @param notification the <code>INotification</code> to pass to the interested object's notification method
		 */
		void notifyObserver(INotification notification);
		
		/**
		 * Compare the given object to the notificaiton context object.
		 * 
		 * @param obj the object to compare.
		 * @return boolean indicating if the notification context and the object are the same.
		 */
		Boolean compareNotifyContext(Object obj);
    }
}
