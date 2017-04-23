//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Observer.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, the <c>Observer</c> class assumes these responsibilities:
    ///         <list type="bullet">
    ///             <item>Encapsulate the notification (callback) method of the interested object.</item>
    ///             <item>Encapsulate the notification context (this) of the interested object.</item>
    ///             <item>Provide methods for setting the notification method and context.</item>
    ///             <item>Provide a method for notifying the interested object.</item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         PureMVC does not rely upon underlying event models such 
    ///         as the one provided with Flash, and ActionScript 3 does 
    ///         not have an inherent event model.
    ///     </para>
    ///     <para>
    ///         The Observer Pattern as implemented within PureMVC exists 
    ///         to support event-driven communication between the 
    ///         application and the actors of the MVC triad.
    ///     </para>
    ///     <para>
    ///         An Observer is an object that encapsulates information
    ///         about an interested object with a notification method that
    ///         should be called when an <c>INotification</c> is broadcast. The Observer then
    ///         acts as a proxy for notifying the interested object.
    ///     </para>
    ///     <para>
    ///         Observers can receive <c>Notification</c>s by having their
    ///         <c>notifyObserver</c> method invoked, passing
    ///         in an object implementing the <c>INotification</c> interface, such
    ///         as a subclass of <c>Notification</c>.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IView"/>
    /// <seealso cref="INotification"/>
    public interface IObserver
    {
        /// <summary>
        /// Set the notification method (callback) method of the interested object
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The notification method should take one parameter of type <c>INotification</c>
        ///     </para>
        /// </remarks>
        Action<INotification> NotifyMethod { set; }

        /// <summary>
        /// Set the notification context (this) of the interested object
        /// </summary>
        object NotifyContext { set; }

        /// <summary>
        /// Notify the interested object.
        /// </summary>
        /// <param name="notification">the <c>INotification</c> to pass to the interested object's notification method</param>
        void NotifyObserver(INotification notification);

        /// <summary>
        /// Compare the given object to the notificaiton context object.
        /// </summary>
        /// <param name="obj">the object to compare.</param>
        /// <returns>indicating if the notification context and the object are the same.</returns>
        bool CompareNotifyContext(object obj);
    }
}
