using System;

namespace org.puremvc.csharp.interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Notifier
    /// </summary>
    /// <remarks>
    ///     <para><c>MacroCommand, Command, Mediator</c> and <c>Proxy</c> all have a need to send <c>Notifications</c></para>
    ///     <para>The <c>INotifier</c> interface provides a common method called <c>sendNotification</c> that relieves implementation code of the necessity to actually construct <c>Notifications</c></para>
    ///     <para>The <c>Notifier</c> class, which all of the above mentioned classes extend, also provides an initialized reference to the <c>Facade</c> Singleton, which is required for the convienience method for sending <c>Notifications</c>, but also eases implementation as these classes have frequent <c>Facade</c> interactions and usually require access to the facade anyway</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.interfaces.IFacade"/>
    /// <see cref="org.puremvc.csharp.interfaces.INotification"/>
    public interface INotifier
    {
        /// <summary>
        /// Send a <c>INotification</c>
        /// </summary>
        /// <remarks>
        ///     <para>Convenience method to prevent having to construct new notification instances in our implementation code</para>
        /// </remarks>
        /// <param name="notificationName">The name of the notification to send</param>
		void sendNotification( String notificationName );
        /// <summary>
        /// Send a <c>INotification</c>
        /// </summary>
        /// <remarks>
        ///     <para>Convenience method to prevent having to construct new notification instances in our implementation code</para>
        /// </remarks>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
		void sendNotification( String notificationName, Object body );
        /// <summary>
        /// Send a <c>INotification</c>
        /// </summary>
        /// <remarks>
        ///     <para>Convenience method to prevent having to construct new notification instances in our implementation code</para>
        /// </remarks>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <param name="type">The type of the notification</param>
		void sendNotification( String notificationName, Object body, String type );
    }
}
