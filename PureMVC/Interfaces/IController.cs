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
    /// The interface definition for a PureMVC Controller.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, an <c>IController</c> implementor 
    ///         follows the 'Command and Controller' strategy, and 
    ///         assumes these responsibilities:
    ///         <list type="bullet">
    ///             <item>Remembering which <c>ICommand</c>s 
    ///             are intended to handle which <c>INotifications</c>.
    ///             </item>
    ///             <item>Registering itself as an <c>IObserver</c> with
    ///             the <c>View</c> for each <c>INotification</c> 
    ///             that it has an <c>ICommand</c> mapping for.
    ///             </item>
    ///             <item>Creating a new instance of the proper <c>ICommand</c> 
    ///             to handle a given <c>INotification</c> when notified by the <c>View</c>.
    ///             </item>
    ///             <item>Calling the <c>ICommand</c>'s <c>execute</c> 
    ///             method, passing in the <c>INotification</c>.
    ///             </item>
    ///         </list>
    ///     </para>
    /// </remarks>
    /// <seealso cref="INotification"/>
    /// <seealso cref="ICommand"/>
    public interface IController
    {
        /// <summary>
        /// Register a particular <c>ICommand</c> class as the handler 
        ///  for a particular <c>INotification</c>.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c></param>
        /// <param name="commandClassRef">the FuncDelegate of the <c>ICommand</c></param>
        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

        /// <summary>
        /// Execute the <c>ICommand</c> previously registered as the
        /// handler for <c>INotification</c>s with the given notification name.
        /// </summary>
        /// <param name="notification">the <c>INotification</c> to execute the associated <c>ICommand</c> for</param>
        void ExecuteCommand(INotification notification);

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
        void RemoveCommand(string notificationName);

        /// <summary>
        /// Check if a Command is registered for a given Notification 
        /// </summary>
        /// <param name="notificationName">whether a Command is currently registered for the given <c>notificationName</c>.</param>
        /// <returns></returns>
        bool HasCommand(string notificationName);
    }
}
