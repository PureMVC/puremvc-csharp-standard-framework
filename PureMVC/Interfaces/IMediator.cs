//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Mediator.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, <c>IMediator</c> implementors assume these responsibilities:
    ///         <list type="bullet">
    ///             <item>Implement a common method which returns a list of all <c>INotification</c>s
    ///             the <c>IMediator</c> has interest in.</item>
    ///             <item>Implement a notification callback method.</item>
    ///             <item>Implement methods that are called when the IMediator is registered or removed from the View</item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         Additionally, <c>IMediator</c>s typically:
    ///         <list type="bullet">
    ///             <item>Act as an intermediary between one or more view components such as text boxes or 
    ///             list controls, maintaining references and coordinating their behavior.</item>
    ///             <item>In Flash-based apps, this is often the place where event listeners are
    ///             added to view components, and their handlers implemented.</item>
    ///             <item>Respond to and generate <c>INotifications</c>, interacting with of
    ///             the rest of the PureMVC app.</item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         When an <c>IMediator</c> is registered with the <c>IView</c>, 
    ///         the <c>IView</c> will call the <c>IMediator</c>'s 
    ///         <c>listNotificationInterests</c> method. The <c>IMediator</c> will 
    ///         return an <c>Array</c> of <c>INotification</c> names which 
    ///         it wishes to be notified about.
    ///     </para>
    ///     <para>
    ///         The <c>IView</c> will then create an <c>Observer</c> object 
    ///         encapsulating that <c>IMediator</c>'s (<c>handleNotification</c>) method
    ///         and register it as an Observer for each <c>INotification</c> name returned by 
    ///         <c>listNotificationInterests</c>.
    ///     </para>
    /// </remarks>
    /// <seealso cref="INotification"/>
    public interface IMediator: INotifier
    {
        /// <summary>
        /// Get or Set the <c>IMediator</c> instance name
        /// </summary>
        string MediatorName { get; }

        /// <summary>
        /// Get or Set the <c>IMediator</c>'s view component.
        /// </summary>
        object ViewComponent { get; set; }

        /// <summary>
        /// List <c>INotification</c> interests.
        /// </summary>
        /// <returns> an <c>Array</c> of the <c>INotification</c> names this <c>IMediator</c> has an interest in.</returns>
        string[] ListNotificationInterests();

        /// <summary>
        /// Handle an <c>INotification</c>.
        /// </summary>
        /// <param name="notification">notification the <c>INotification</c> to be handled</param>
        void HandleNotification(INotification notification);

        /// <summary>
        /// Called by the View when the Mediator is registered
        /// </summary>
        void OnRegister();

        /// <summary>
        /// Called by the View when the Mediator is removed
        /// </summary>
        void OnRemove();
    }
}
