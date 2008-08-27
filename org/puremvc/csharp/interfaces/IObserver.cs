/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Reflection;

using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Observer
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, <c>IObserver</c> implementors assume these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Encapsulate the notification (callback) method of the interested object</item>
    ///         <item>Encapsulate the notification context (<c>this</c>) of the interested object</item>
    ///         <item>Provide methods for setting the interested object' notification method and context</item>
    ///         <item>Provide a method for notifying the interested object</item>
    ///     </list>
    ///     <para>PureMVC does not rely upon underlying event models</para>
    ///     <para>The Observer Pattern as implemented within PureMVC exists to support event driven communication between the application and the actors of the MVC triad</para>
    ///     <para>An Observer is an object that encapsulates information about an interested object with a notification method that should be called when an <c>INotification</c> is broadcast. The Observer then acts as a proxy for notifying the interested object</para>
    ///     <para>Observers can receive <c>Notification</c>s by having their <c>notifyObserver</c> method invoked, passing in an object implementing the <c>INotification</c> interface, such as a subclass of <c>Notification</c></para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.interfaces.IView"/>
    /// <see cref="org.puremvc.csharp.interfaces.INotification"/>
    public interface IObserver
    {
        /// <summary>
        /// Set the notification method
        /// </summary>
        /// <remarks>The notification method should take one parameter of type <c>INotification</c></remarks>
        /// <param name="notifyMethod">The notification (callback) method of the interested object</param>
        void setNotifyMethod(String notifyMethod);

        /// <summary>
        /// Set the notification context
        /// </summary>
        /// <param name="notifyContext">The notification context (this) of the interested object</param>
		void setNotifyContext(Object notifyContext);

        /// <summary>
        /// Notify the interested object
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to pass to the interested object's notification method</param>
        void notifyObserver(INotification notification);
		
        /// <summary>
        /// Compare the given object to the notificaiton context object
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Indicates if the notification context and the object are the same.</returns>
		Boolean compareNotifyContext(Object obj);
    }
}
