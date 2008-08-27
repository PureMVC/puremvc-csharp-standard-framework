/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;
using org.puremvc.csharp.patterns.facade;

namespace org.puremvc.csharp.patterns.observer
{
    /// <summary>
    /// A Base <c>INotifier</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para><c>MacroCommand, Command, Mediator</c> and <c>Proxy</c> all have a need to send <c>Notifications</c></para>
    ///     <para>The <c>INotifier</c> interface provides a common method called <c>sendNotification</c> that relieves implementation code of the necessity to actually construct <c>Notifications</c></para>
    ///     <para>The <c>Notifier</c> class, which all of the above mentioned classes extend, provides an initialized reference to the <c>Facade</c> Singleton, which is required for the convienience method for sending <c>Notifications</c>, but also eases implementation as these classes have frequent <c>Facade</c> interactions and usually require access to the facade anyway</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.patterns.facade.Facade"/>
    /// <see cref="org.puremvc.csharp.patterns.mediator.Mediator"/>
    /// <see cref="org.puremvc.csharp.patterns.proxy.Proxy"/>
    /// <see cref="org.puremvc.csharp.patterns.command.SimpleCommand"/>
    /// <see cref="org.puremvc.csharp.patterns.command.MacroCommand"/>
    public class Notifier : INotifier
    {
        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notiification to send</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName) 
		{
            facade.sendNotification(notificationName);
		}

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName, Object body)
		{
            facade.sendNotification(notificationName, body);
		}

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <param name="type">The type of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName, Object body, String type)
		{
            facade.sendNotification(notificationName, body, type);
		}
		
        /// <summary>
        /// Local reference to the Facade Singleton
        /// </summary>
		protected IFacade facade = Facade.getInstance();
    }
}
