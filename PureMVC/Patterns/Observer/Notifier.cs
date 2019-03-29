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
    /// A Base <c>INotifier</c> implementation.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <c>MacroCommand, Command, Mediator</c> and <c>Proxy</c> 
    ///         all have a need to send <c>Notifications</c>.
    ///     </para>
    ///     <para>
    ///         The <c>INotifier</c> interface provides a common method called
    ///         <c>sendNotification</c> that relieves implementation code of 
    ///         the necessity to actually construct <c>Notifications</c>.
    ///     </para>
    ///     <para>
    ///         The <c>Notifier</c> class, which all of the above mentioned classes
    ///         extend, provides an initialized reference to the <c>Facade</c>
    ///         Singleton, which is required for the convienience method
    ///         for sending <c>Notifications</c>, but also eases implementation as these
    ///         classes have frequent <c>Facade</c> interactions and usually require
    ///         access to the facade anyway.
    ///     </para>
    /// </remarks>
    /// <seealso cref="PureMVC.Patterns.Proxy.Proxy"/>
    /// <seealso cref="PureMVC.Patterns.Facade.Facade"/>
    /// <seealso cref="PureMVC.Patterns.Mediator.Mediator"/>
    /// <seealso cref="PureMVC.Patterns.Command.MacroCommand"/>
    /// <seealso cref="PureMVC.Patterns.Command.SimpleCommand"/>
    public class Notifier : INotifier
    {
        /// <summary>
        /// Create and send an <c>INotification</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Keeps us from having to construct new INotification 
        ///         instances in our implementation code.
        ///     </para>
        /// </remarks>
        /// <param name="notificationName">the name of the notiification to send</param>
        /// <param name="body">the body of the notification (optional)</param>
        /// <param name="type">the type of the notification (optional)</param>
        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.SendNotification(notificationName, body, type);
        }

        /// <summary>Return the Singleton Facade instance</summary>
        protected IFacade Facade
        {
            get
            {
                return Patterns.Facade.Facade.GetInstance(() => new Facade.Facade());
            }
        }
    }
}
