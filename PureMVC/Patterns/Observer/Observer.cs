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
			NotifyMethod = notifyMethod;
			NotifyContext = notifyContext;
		}

		#endregion

		#region Public Methods

		#region IObserver Members

		/// <summary>
		/// Notify the interested object
		/// </summary>
		/// <param name="notification">The <c>INotification</c> to pass to the interested object's notification method</param>
		public void NotifyObserver(INotification notification)
		{
			Type t = this.NotifyContext.GetType();
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
			MethodInfo mi = t.GetMethod(this.NotifyMethod, f);
			mi.Invoke(this.NotifyContext, new object[] { notification });
		}

		/// <summary>
		/// Compare an object to the notification context
		/// </summary>
		/// <param name="obj">The object to compare</param>
		/// <returns>Indicating if the object and the notification context are the same</returns>
		public bool CompareNotifyContext(object obj)
		{
			return this.NotifyContext.Equals(obj);
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// The notification (callback) method of the interested object
		/// </summary>
		/// <remarks>The notification method should take one parameter of type <c>INotification</c></remarks>
		public string NotifyMethod
		{
			private get { return m_notify; }
			set { m_notify = value; }
		}

		/// <summary>
		/// The notification context (this) of the interested object
		/// </summary>
		public object NotifyContext
		{
			private get { return m_context; }
			set { m_context = value; }
		}

		#endregion

		#region Members

		private string m_notify;

		private object m_context;

		#endregion
	}
}
