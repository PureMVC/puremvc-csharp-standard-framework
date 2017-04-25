//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
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
    /// <seealso cref="PureMVC.Patterns.Observer.Observer"/>
    public class Notification: INotification
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name of the <c>Notification</c> instance. (required)</param>
        /// <param name="body">the <c>Notification</c> body. (optional)</param>
        /// <param name="type">the type of the <c>Notification</c> (optional)</param>
        public Notification(string name, object body=null, string type=null)
        {
            Name = name;
            Body = body;
            Type = type;
        }

        /// <summary>
        /// Get the string representation of the <c>Notification</c> instance.
        /// </summary>
        /// <returns>the string representation of the <c>Notification</c> instance.</returns>
        public override string ToString()
        {
            string msg = "Notification Name: " + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type);
            return msg;
        }

        /// <summary>the name of the notification instance</summary>
        public string Name { get; }

        /// <summary>the body of the notification instance</summary>
        public object Body { get; set; }

        /// <summary>the type of the notification instance</summary>
        public string Type { get; set; }
    }
}
