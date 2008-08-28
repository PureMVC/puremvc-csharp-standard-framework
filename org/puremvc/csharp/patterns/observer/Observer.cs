/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Reflection;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.patterns.observer
{
    /// <summary>
    /// A base <c>IObserver</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>An <c>Observer</c> is an object that encapsulates information about an interested object with a method that should be called when a particular <c>INotification</c> is broadcast</para>
    ///     <para>In PureMVC, the <c>Observer</c> class assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Encapsulate the notification (callback) method of the interested object</item>
    ///         <item>Encapsulate the notification context (this) of the interested object</item>
    ///         <item>Provide methods for setting the notification method and context</item>
    ///         <item>Provide a method for notifying the interested object</item>
    ///     </list>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.View"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    public class Observer : IObserver
    {
        private String notify;
		private Object context;
	
        /// <summary>
        /// Constructs a new observer with the specified notification method and context
        /// </summary>
        /// <param name="notifyMethod">The notification method of the interested object</param>
        /// <param name="notifyContext">The notification context of the interested object</param>
        /// <remarks>
        ///     <para>The notification method on the interested object should take on parameter of type <c>INotification</c></para>
        /// </remarks>
        public Observer(String notifyMethod, Object notifyContext) 
		{
			setNotifyMethod(notifyMethod);
			setNotifyContext(notifyContext);
		}

        /// <summary>
        /// Set the notification method
        /// </summary>
        /// <remarks>The notification method should take one parameter of type <c>INotification</c></remarks>
        /// <param name="notifyMethod">The notification (callback) method of the interested object</param>
        public void setNotifyMethod(String notifyMethod)
		{
			notify = notifyMethod;
		}

        /// <summary>
        /// Set the notification context
        /// </summary>
        /// <param name="notifyContext">The notification context (this) of the interested object</param>
		public void setNotifyContext(Object notifyContext)
		{
			context = notifyContext;
		}

        /// <summary>
        /// Gets the notification method.
        /// </summary>
        /// <returns>The notification (callback) method of the interested object</returns>
        private String getNotifyMethod()
		{
			return notify;
		}
		
        /// <summary>
        /// Get the notification context
        /// </summary>
        /// <returns>The notification context (<c>this</c>) of the interested object</returns>
		private Object getNotifyContext()
		{
			return context;
		}

        /// <summary>
        /// Notify the interested object
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to pass to the interested object's notification method</param>
		public void notifyObserver(INotification notification)
		{
            Type t = this.getNotifyContext().GetType();
            BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            MethodInfo mi = t.GetMethod(this.getNotifyMethod(), f);
            mi.Invoke(this.getNotifyContext(), new Object[] { notification });
		}
	
        /// <summary>
        /// Compare an object to the notification context
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Indicating if the object and the notification context are the same</returns>
		 public Boolean compareNotifyContext(Object obj)
		 {
		 	return this.getNotifyContext().Equals(obj);
		 }
     }
}
