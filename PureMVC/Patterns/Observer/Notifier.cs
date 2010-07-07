/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using


using System;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Patterns
{
    /// <summary>
    /// A Base <c>INotifier</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para><c>MacroCommand, Command, Mediator</c> and <c>Proxy</c> all have a need to send <c>Notifications</c></para>
    ///     <para>The <c>INotifier</c> interface provides a common method called <c>sendNotification</c> that relieves implementation code of the necessity to actually construct <c>Notifications</c></para>
    ///     <para>The <c>Notifier</c> class, which all of the above mentioned classes extend, provides an initialized reference to the <c>Facade</c> Singleton, which is required for the convienience method for sending <c>Notifications</c>, but also eases implementation as these classes have frequent <c>Facade</c> interactions and usually require access to the facade anyway</para>
    /// </remarks>
	/// <see cref="PureMVC.Patterns.Facade"/>
	/// <see cref="PureMVC.Patterns.Mediator"/>
	/// <see cref="PureMVC.Patterns.Proxy"/>
	/// <see cref="PureMVC.Patterns.SimpleCommand"/>
	/// <see cref="PureMVC.Patterns.MacroCommand"/>
    public class Notifier : INotifier
    {
		#region Public Methods

		#region INotifier Members

		/// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notiification to send</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
		/// <remarks>This method is thread safe</remarks>
		public virtual void SendNotification(string notificationName) 
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
			m_facade.SendNotification(notificationName);
		}

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
		/// <remarks>This method is thread safe</remarks>
		public virtual void SendNotification(string notificationName, object body)
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
			m_facade.SendNotification(notificationName, body);
		}

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <param name="type">The type of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
		/// <remarks>This method is thread safe</remarks>
		public virtual void SendNotification(string notificationName, object body, string type)
		{
			// The Facade SendNotification is thread safe, therefore this method is thread safe.
            m_facade.SendNotification(notificationName, body, type);
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// Local reference to the Facade Singleton
		/// </summary>
		protected IFacade Facade
		{
			get { return m_facade; }
		}

		#endregion

		#region Members

		/// <summary>
        /// Local reference to the Facade Singleton
        /// </summary>
		private IFacade m_facade = PureMVC.Patterns.Facade.Instance;

		#endregion
	}
}
