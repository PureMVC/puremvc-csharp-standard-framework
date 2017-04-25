//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Interfaces
{
    /// <summary>
    /// A base <c>INotification</c> implementation.
    /// </summary>
    /// <remarks>
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
    ///         Notifications are not meant to be a replacement for Events
    ///         in Flex/Flash/Apollo. Generally, <c>IMediator</c> implementors
    ///         place event listeners on their view components, which they
    ///         then handle in the usual way. This may lead to the broadcast of <c>Notification</c>s to 
    ///         trigger <c>ICommand</c>s or to communicate with other <c>IMediators</c>. 
    ///         <c>IProxy</c> and <c>ICommand</c>
    ///         instances communicate with each other and <c>IMediator</c>s
    ///         by broadcasting <c>INotification</c>s.
    ///     </para>
    ///     <para>
    ///         A key difference between Flash <c>Event</c>s and PureMVC
    ///         <c>Notification</c>s is that <c>Event</c>s follow the 
    ///         'Chain of Responsibility' pattern, 'bubbling' up the display hierarchy 
    ///         until some parent component handles the <c>Event</c>, while
    ///         PureMVC <c>Notification</c>s follow a 'Publish/Subscribe'
    ///         pattern. PureMVC classes need not be related to each other in a 
    ///         parent/child relationship in order to communicate with one another
    ///         using <c>Notification</c>s.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IView"/>
    /// <seealso cref="IObserver"/>
    public interface INotification
    {
        /// <summary>
        /// Get the name of the <c>INotification</c> instance. 
        /// No setter, should be set by constructor only
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get or Set the body of the <c>INotification</c> instance
        /// </summary>
        object Body { get; set; }

        /// <summary>
        /// Get or Set the type of the <c>INotification</c> instance
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Get the string representation of the <c>INotification</c> instance
        /// </summary>
        /// <returns>String representation</returns>
        string ToString();
    }
}
