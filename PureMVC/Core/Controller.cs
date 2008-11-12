/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Core
{
    /// <summary>
    /// A Singleton <c>IController</c> implementation.
    /// </summary>
    /// <remarks>
    /// 	<para>In PureMVC, the <c>Controller</c> class follows the 'Command and Controller' strategy, and assumes these responsibilities:</para>
    /// 	<list type="bullet">
    /// 		<item>Remembering which <c>ICommand</c>s are intended to handle which <c>INotifications</c>.</item>
    /// 		<item>Registering itself as an <c>IObserver</c> with the <c>View</c> for each <c>INotification</c> that it has an <c>ICommand</c> mapping for.</item>
    /// 		<item>Creating a new instance of the proper <c>ICommand</c> to handle a given <c>INotification</c> when notified by the <c>View</c>.</item>
    /// 		<item>Calling the <c>ICommand</c>'s <c>execute</c> method, passing in the <c>INotification</c>.</item>
    /// 	</list>
    /// 	<para>Your application must register <c>ICommands</c> with the <c>Controller</c>.</para>
    /// 	<para>The simplest way is to subclass <c>Facade</c>, and use its <c>initializeController</c> method to add your registrations.</para>
    /// </remarks>
	/// <see cref="PureMVC.Core.View"/>
	/// <see cref="PureMVC.Patterns.Observer"/>
	/// <see cref="PureMVC.Patterns.Notification"/>
	/// <see cref="PureMVC.Patterns.SimpleCommand"/>
	/// <see cref="PureMVC.Patterns.MacroCommand"/>
    public class Controller : IController
	{
		#region Constructors

		/// <summary>
        /// Constructs and initializes a new controller
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This <c>IController</c> implementation is a Singleton, 
        ///         so you should not call the constructor 
        ///         directly, but instead call the static Singleton
        ///         Factory method <c>Controller.getInstance()</c>
        ///     </para>
        /// </remarks>
		protected Controller()
		{
			m_commandMap = new Dictionary<string, Type>();	
			InitializeController();
		}

		#endregion

		#region Public Methods

		#region IController Members

		/// <summary>
		/// If an <c>ICommand</c> has previously been registered
		/// to handle a the given <c>INotification</c>, then it is executed.
		/// </summary>
		/// <param name="note">An <c>INotification</c></param>
		/// <remarks>This method is thread safe and needs to be thread safe in all implementations.</remarks>
		public virtual void ExecuteCommand(INotification note)
		{
			Type commandType = null;

			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(note.Name)) return;
				commandType = m_commandMap[note.Name];
			}

			object commandInstance = Activator.CreateInstance(commandType);

			if (commandInstance is ICommand)
			{
				((ICommand) commandInstance).Execute(note);
			}
		}

		/// <summary>
		/// Register a particular <c>ICommand</c> class as the handler
		/// for a particular <c>INotification</c>.
		/// </summary>
		/// <param name="notificationName">The name of the <c>INotification</c></param>
		/// <param name="commandType">The <c>Type</c> of the <c>ICommand</c></param>
		/// <remarks>
		///     <para>
		///         If an <c>ICommand</c> has already been registered to 
		///         handle <c>INotification</c>s with this name, it is no longer
		///         used, the new <c>ICommand</c> is used instead.
		///     </para>
		/// </remarks> 
		/// <remarks>This method is thread safe and needs to be thread safe in all implementations.</remarks>
		public virtual void RegisterCommand(string notificationName, Type commandType)
		{
			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(notificationName))
				{
					// This call needs to be monitored carefully. Have to make sure that RegisterObserver
					// doesn't call back into the controller, or a dead lock could happen.
					m_view.RegisterObserver(notificationName, new Observer("executeCommand", this));
				}

				m_commandMap[notificationName] = commandType;
			}
		}

		/// <summary>
		/// Check if a Command is registered for a given Notification 
		/// </summary>
		/// <param name="notificationName"></param>
		/// <returns>whether a Command is currently registered for the given <c>notificationName</c>.</returns>
		/// <remarks>This method is thread safe and needs to be thread safe in all implementations.</remarks>
		public virtual bool HasCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				return m_commandMap.ContainsKey(notificationName);
			}
		}

		/// <summary>
		/// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping.
		/// </summary>
		/// <param name="notificationName">The name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
		/// <remarks>This method is thread safe and needs to be thread safe in all implementations.</remarks>
		public virtual void RemoveCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				if (m_commandMap.ContainsKey(notificationName))
				{
					// remove the observer

					// This call needs to be monitored carefully. Have to make sure that RemoveObserver
					// doesn't call back into the controller, or a dead lock could happen.
					m_view.RemoveObserver(notificationName, this);
					m_commandMap.Remove(notificationName);
				}
			}
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// Singleton Factory method.  This method is thread safe.
		/// </summary>
		public static IController Instance
		{
			get
			{
				if (m_instance == null)
				{
					lock (m_staticSyncRoot)
					{
						if (m_instance == null) m_instance = new Controller();
					}
				}

				return m_instance;
			}
		}

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// Explicit static constructor to tell C# compiler
		/// not to mark type as beforefieldinit
		/// </summary>
		static Controller()
		{
		}

		/// <summary>
		/// Initialize the Singleton <c>Controller</c> instance
		/// </summary>
		/// <remarks>
		///     <para>Called automatically by the constructor</para>
		///     
		///     <para>
		///         Note that if you are using a subclass of <c>View</c>
		///         in your application, you should also subclass <c>Controller</c>
		///         and override the <c>initializeController</c> method in the following way:
		///     </para>
		/// 
		///     <c>
		///         // ensure that the Controller is talking to my IView implementation
		///         public override void initializeController()
		///         {
		///             view = MyView.Instance;
		///         }
		///     </c>
		/// </remarks>
		protected virtual void InitializeController()
		{
			m_view = View.Instance;
		}

		#endregion

		#region Members

		/// <summary>
        /// Local reference to View
        /// </summary>
		protected IView m_view;
		
        /// <summary>
        /// Mapping of Notification names to Command Class references
        /// </summary>
        protected IDictionary<string, Type> m_commandMap;

        /// <summary>
        /// Singleton instance, can be sublcassed though....
        /// </summary>
		protected static volatile IController m_instance;

		/// <summary>
		/// Used for locking
		/// </summary>
		protected readonly object m_syncRoot = new object();

		/// <summary>
		/// Used for locking the instance calls
		/// </summary>
		protected static readonly object m_staticSyncRoot = new object();

		#endregion
	}
}
