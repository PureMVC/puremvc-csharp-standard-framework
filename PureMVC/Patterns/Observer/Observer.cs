/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Reflection;

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Patterns
{
    /// <summary>
    /// A base <c>IObserver</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>An <c>Observer</c> is an object that encapsulates information about an interested object with a method that should be called when a particular <c>INotification</c> is broadcast</para>
    ///     <para>In PureMVC, the <c>Observer</c> class assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Encapsulate the notification (callback) method of the interested object</item>
    ///         <item>Encapsulate the notification context (this) of the interested object</item>
    ///         <item>Provide methods for setting the notification method and context</item>
    ///         <item>Provide a method for notifying the interested object</item>
    ///     </list>
    /// </remarks>
	/// <see cref="PureMVC.Core.View"/>
	/// <see cref="PureMVC.Patterns.Notification"/>
	public class Observer : IObserver
	{
		#region Constructors

		/// <summary>
		/// Constructs a new observer with the specified notification method and context
		/// </summary>
		/// <param name="notifyMethod">The notification method of the interested object</param>
		/// <param name="notifyContext">The notification context of the interested object</param>
		/// <remarks>
		///     <para>The notification method on the interested object should take on parameter of type <c>INotification</c></para>
		/// </remarks>
		public Observer(string notifyMethod, object notifyContext)
		{
			m_notifyMethod = notifyMethod;
			m_notifyContext = notifyContext;
		}

		#endregion

		#region Public Methods

		#region IObserver Members

		/// <summary>
		/// Notify the interested object
		/// </summary>
		/// <remarks>This method is thread safe</remarks>
		/// <param name="notification">The <c>INotification</c> to pass to the interested object's notification method</param>
		public virtual void NotifyObserver(INotification notification)
		{
			object context;
			string method;

			// Retrieve the current state of the object, then notify outside of our thread safe block
			lock (m_syncRoot)
			{
				context = NotifyContext;
				method = NotifyMethod;
			}

			Type t = context.GetType();
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
			MethodInfo mi = t.GetMethod(method, f);
			mi.Invoke(context, new object[] { notification });
		}

		/// <summary>
		/// Compare an object to the notification context
		/// </summary>
		/// <remarks>This method is thread safe</remarks>
		/// <param name="obj">The object to compare</param>
		/// <returns>Indicating if the object and the notification context are the same</returns>
		public virtual bool CompareNotifyContext(object obj)
		{
			lock (m_syncRoot)
			{
				// Compare on the current state
				return NotifyContext.Equals(obj);
			}
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// The notification (callback) method of the interested object
		/// </summary>
		/// <remarks>The notification method should take one parameter of type <c>INotification</c></remarks>
		/// <remarks>This accessor is thread safe</remarks>
		public virtual string NotifyMethod
		{
			private get
			{
				// Setting and getting of reference types is atomic, no need to lock here
				return m_notifyMethod;
			}
			set
			{
				// Setting and getting of reference types is atomic, no need to lock here
				m_notifyMethod = value;
			}
		}

		/// <summary>
		/// The notification context (this) of the interested object
		/// </summary>
		/// <remarks>This accessor is thread safe</remarks>
		public virtual object NotifyContext
		{
			private get
			{
				// Setting and getting of reference types is atomic, no need to lock here
				return m_notifyContext;
			}
			set
			{
				// Setting and getting of reference types is atomic, no need to lock here
				m_notifyContext = value;
			}
		}

		#endregion

		#region Members

		/// <summary>
		/// Holds the notify method name.
		/// </summary>
		private string m_notifyMethod;

		/// <summary>
		/// Holds the notify context.
		/// </summary>
		private object m_notifyContext;

		/// <summary>
		/// Used for locking
		/// </summary>
		protected readonly object m_syncRoot = new object();

		#endregion
	}
}
