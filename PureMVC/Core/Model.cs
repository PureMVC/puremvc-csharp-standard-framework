/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;

#endregion

namespace PureMVC.Core
{
    /// <summary>
    /// A Singleton <c>IModel</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, the <c>Model</c> class provides access to model objects (Proxies) by named lookup</para>
    ///     <para>The <c>Model</c> assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Maintain a cache of <c>IProxy</c> instances</item>
    ///         <item>Provide methods for registering, retrieving, and removing <c>IProxy</c> instances</item>
    ///     </list>
    ///     <para>
    ///         Your application must register <c>IProxy</c> instances
    ///         with the <c>Model</c>. Typically, you use an 
    ///         <c>ICommand</c> to create and register <c>IProxy</c> 
    ///         instances once the <c>Facade</c> has initialized the Core actors
    ///     </para>
    /// </remarks>
	/// <seealso cref="PureMVC.Patterns.Proxy"/>
	/// <seealso cref="PureMVC.Interfaces.IProxy" />
    public class Model : IModel
    {
		#region Constructors

		/// <summary>
		/// Constructs and initializes a new model
		/// </summary>
		/// <remarks>
		///     <para>This <c>IModel</c> implementation is a Singleton, so you should not call the constructor directly, but instead call the static Singleton Factory method <c>Model.getInstance()</c></para>
		/// </remarks>
		protected Model()
		{
			m_proxyMap = new Dictionary<string, IProxy>();
			InitializeModel();
		}

		#endregion

		#region Public Methods

		#region IModel Members

		/// <summary>
		/// Register an <c>IProxy</c> with the <c>Model</c>
		/// </summary>
		/// <param name="proxy">An <c>IProxy</c> to be held by the <c>Model</c></param>
		public void RegisterProxy(IProxy proxy)
		{
			m_proxyMap[proxy.ProxyName] = proxy;
			proxy.OnRegister();
		}

		/// <summary>
		/// Retrieve an <c>IProxy</c> from the <c>Model</c>
		/// </summary>
		/// <param name="proxyName">The name of the <c>IProxy</c> to retrieve</param>
		/// <returns>The <c>IProxy</c> instance previously registered with the given <c>proxyName</c></returns>
		public IProxy RetrieveProxy(string proxyName)
		{
			if (!m_proxyMap.ContainsKey(proxyName)) return null;
			return m_proxyMap[proxyName];
		}

		/// <summary>
		/// Check if a Proxy is registered
		/// </summary>
		/// <param name="proxyName"></param>
		/// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
		public bool HasProxy(string proxyName)
		{
			return m_proxyMap.ContainsKey(proxyName);
		}

		/// <summary>
		/// Remove an <c>IProxy</c> from the <c>Model</c>
		/// </summary>
		/// <param name="proxyName">The name of the <c>IProxy</c> instance to be removed</param>
		public IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;

			if (m_proxyMap.ContainsKey(proxyName))
			{
				proxy = RetrieveProxy(proxyName);
				m_proxyMap.Remove(proxyName);
				proxy.OnRemove();
			}

			return proxy;
		}

		#endregion

		#endregion

		#region Accessors

		/// <summary>
		/// <c>Model</c> Singleton Factory method
		/// </summary>
		public static IModel Instance
		{
			get { return m_instance; }
		}

		#endregion

		#region Protected & Internal Methods

		/// <summary>
		/// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
		/// </summary>
		static Model()
		{
			m_instance = new Model();
		}

		/// <summary>
		/// Initialize the Singleton <c>Model</c> instance.
		/// </summary>
		/// <remarks>
		///     <para>Called automatically by the constructor, this is your opportunity to initialize the Singleton instance in your subclass without overriding the constructor</para>
		/// </remarks>
		protected virtual void InitializeModel()
		{
		}

		#endregion

		#region Members

		/// <summary>
		/// Mapping of proxyNames to <c>IProxy</c> instances
		/// </summary>
		protected IDictionary<string, IProxy> m_proxyMap;

		/// <summary>
		/// Singleton instance
		/// </summary>
		protected static IModel m_instance;

		#endregion
    }
}
