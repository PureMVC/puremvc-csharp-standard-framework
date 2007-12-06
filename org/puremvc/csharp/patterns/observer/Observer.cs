using System;
using System.Reflection;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.patterns.observer
{
    /**
	 * A base <code>IObserver</code> implementation.
	 * 
	 * <P> 
	 * An <code>Observer</code> is an object that encapsulates information
	 * about an interested object with a method that should 
	 * be called when a particular <code>INotification</code> is broadcast. </P>
	 * 
	 * <P>
	 * In PureMVC, the <code>Observer</code> class assumes these responsibilities:
	 * <UL>
	 * <LI>Encapsulate the notification (callback) method of the interested object.</LI>
	 * <LI>Encapsulate the notification context (this) of the interested object.</LI>
	 * <LI>Provide methods for setting the notification method and context.</LI>
	 * <LI>Provide a method for notifying the interested object.</LI>
	 * </UL>
	 * 
	 * @see org.puremvc.core.view.View View
	 * @see org.puremvc.patterns.observer.Notification Notification
	 */
    public class Observer : IObserver
    {
        private String notify;
		private Object context;
	
		/**
		 * Constructor. 
		 * 
		 * <P>
		 * The notification method on the interested object should take 
		 * one parameter of type <code>INotification</code></P>
		 * 
		 * @param notifyMethod the notification method of the interested object
		 * @param notifyContext the notification context of the interested object
		 */
        public Observer(String notifyMethod, Object notifyContext) 
		{
			setNotifyMethod( notifyMethod );
			setNotifyContext( notifyContext );
		}
		
		/**
		 * Set the notification method.
		 * 
		 * <P>
		 * The notification method should take one parameter of type <code>INotification</code>.</P>
		 * 
		 * @param notifyMethod the notification (callback) method of the interested object.
		 */
        public void setNotifyMethod(String notifyMethod)
		{
			notify = notifyMethod;
		}
		
		/**
		 * Set the notification context.
		 * 
		 * @param notifyContext the notification context (this) of the interested object.
		 */
		public void setNotifyContext(Object notifyContext)
		{
			context = notifyContext;
		}
		
		/**
		 * Get the notification method.
		 * 
		 * @return the notification (callback) method of the interested object.
		 */
        private String getNotifyMethod()
		{
			return notify;
		}
		
		/**
		 * Get the notification context.
		 * 
		 * @return the notification context (<code>this</code>) of the interested object.
		 */
		private Object getNotifyContext()
		{
			return context;
		}
		
		/**
		 * Notify the interested object.
		 * 
		 * @param notification the <code>INotification</code> to pass to the interested object's notification method.
		 */
		public void notifyObserver( INotification notification )
		{
            Type t = this.getNotifyContext().GetType();
            BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            MethodInfo mi = t.GetMethod(this.getNotifyMethod(), f);
            mi.Invoke(this.getNotifyContext(), new Object[] { notification });
		}
	
		/**
		 * Compare an object to the notification context. 
		 * 
		 * @param obj the object to compare
		 * @return boolean indicating if the object and the notification context are the same
		 */
		 public Boolean compareNotifyContext(Object obj)
		 {
		 	return this.getNotifyContext().Equals(obj);
		 }
    }
}
