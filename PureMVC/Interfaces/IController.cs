/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

#endregion

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Controller
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, an <c>IController</c> implementor follows the 'Command and Controller' strategy, and assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Remembering which <c>ICommand</c>s are intended to handle which <c>INotifications</c></item>
    ///         <item>Registering itself as an <c>IObserver</c> with the <c>View</c> for each <c>INotification</c> that it has an <c>ICommand</c> mapping for</item>
    ///         <item>Creating a new instance of the proper <c>ICommand</c> to handle a given <c>INotification</c> when notified by the <c>View</c></item>
    ///         <item>Calling the <c>ICommand</c>'s <c>execute</c> method, passing in the <c>INotification</c></item>
    ///     </list>
    /// </remarks>
	/// <see cref="PureMVC.Interfaces.INotification"/>
	/// <see cref="PureMVC.Interfaces.ICommand"/>
    public interface IController
    {
        /// <summary>
        /// Register a particular <c>ICommand</c> class as the handler for a particular <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c></param>
        /// <param name="commandType">The <c>Type</c> of the <c>ICommand</c></param>
        void RegisterCommand(string notificationName, Type commandType);

        /// <summary>
        /// Execute the <c>ICommand</c> previously registered as the handler for <c>INotification</c>s with the given notification name
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to execute the associated <c>ICommand</c> for</param>
		void ExecuteCommand(INotification notification);

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping.
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
		void RemoveCommand(string notificationName);

		/// <summary>
		/// Check if a Command is registered for a given Notification.
		/// </summary>
		/// <param name="notificationName">The name of the <c>INotification</c> to check the <c>ICommand</c> mapping for</param>
		/// <returns>whether a Command is currently registered for the given <c>notificationName</c>.</returns>
		bool HasCommand(string notificationName);
	}
}
