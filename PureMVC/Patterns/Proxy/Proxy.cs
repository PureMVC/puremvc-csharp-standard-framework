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
    /// A base <c>IProxy</c> implementation
    /// </summary>
    /// <remarks>
    /// 	<para>In PureMVC, <c>Proxy</c> classes are used to manage parts of the application's data model</para>
    /// 	<para>A <c>Proxy</c> might simply manage a reference to a local data object, in which case interacting with it might involve setting and getting of its data in synchronous fashion</para>
    /// 	<para><c>Proxy</c> classes are also used to encapsulate the application's interaction with remote services to save or retrieve data, in which case, we adopt an asyncronous idiom; setting data (or calling a method) on the <c>Proxy</c> and listening for a <c>Notification</c> to be sent when the <c>Proxy</c> has retrieved the data from the service</para>
    /// </remarks>
	/// <see cref="PureMVC.Core.Model"/>
    public class Proxy : Notifier, IProxy, INotifier
    {
		#region Constants

		/// <summary>
        /// The default proxy name
        /// </summary>
        public static string NAME = "Proxy";

		#endregion

		#region Constructors

		/// <summary>
        /// Constructs a new proxy with the default name and no data
        /// </summary>
        public Proxy() 
            : this(NAME, null)
        {
		}

        /// <summary>
        /// Constructs a new proxy with the specified name and no data
        /// </summary>
        /// <param name="proxyName">The name of the proxy</param>
        public Proxy(string proxyName)
            : this(proxyName, null)
        {
		}

        /// <summary>
        /// Constructs a new proxy with the specified name and data
        /// </summary>
        /// <param name="proxyName">The name of the proxy</param>
        /// <param name="data">The data to be managed</param>
		public Proxy(string proxyName, object data)
		{

			m_proxyName = (proxyName != null) ? proxyName : NAME;
			if (data != null) m_data = data;
		}

		#endregion

		#region Public Methods

		#region IProxy Members

		/// <summary>
		/// Called by the Model when the Proxy is registered
		/// </summary>
		public virtual void OnRegister()
		{
		}

		/// <summary>
		/// Called by the Model when the Proxy is removed
		/// </summary>
		public virtual void OnRemove()
		{
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// Get the proxy name
		/// </summary>
		/// <returns></returns>
		public virtual string ProxyName
		{
			get { return m_proxyName; }
		}

		/// <summary>
		/// Set the data object
		/// </summary>
		public virtual object Data
		{
			get { return m_data; }
			set { m_data = value; }
		}

		#endregion

		#region Members

		/// <summary>
        /// The name of the proxy
        /// </summary>
		protected string m_proxyName;
		
		/// <summary>
		/// The data object to be managed
		/// </summary>
		protected object m_data;

		#endregion
	}
}
