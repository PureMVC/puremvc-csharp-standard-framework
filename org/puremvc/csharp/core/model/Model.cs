/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.core.model
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
    /// <seealso cref="org.puremvc.csharp.patterns.proxy.Proxy"/>
    /// <seealso cref="org.puremvc.csharp.interfaces.IProxy" />
    public class Model : IModel
    {
        /// <summary>
        /// Constructs and initializes a new model
        /// </summary>
        /// <remarks>
        ///     <para>This <c>IModel</c> implementation is a Singleton, so you should not call the constructor directly, but instead call the static Singleton Factory method <c>Model.getInstance()</c></para>
        /// </remarks>
		protected Model()
		{
			proxyMap = new Hashtable();	
			initializeModel();	
		}
		
        /// <summary>
        /// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        /// </summary>
        static Model()
        {
            instance = new Model();
        }

        /// <summary>
        /// Initialize the Singleton <c>Model</c> instance.
        /// </summary>
        /// <remarks>
        ///     <para>Called automatically by the constructor, this is your opportunity to initialize the Singleton instance in your subclass without overriding the constructor</para>
        /// </remarks>
        protected virtual void initializeModel()
		{ }
		
		/// <summary>
        /// <c>Model</c> Singleton Factory method
		/// </summary>
        /// <returns>The Singleton instance</returns>
		public static IModel getInstance()
        {
			return instance;
		}

        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c>
        /// </summary>
        /// <param name="proxy">An <c>IProxy</c> to be held by the <c>Model</c></param>
		public void registerProxy(IProxy proxy)
        {
            proxyMap[proxy.getProxyName()] = proxy;
		}

        /// <summary>
        /// Retrieve an <c>IProxy</c> from the <c>Model</c>
        /// </summary>
        /// <param name="proxyName">The name of the <c>IProxy</c> to retrieve</param>
        /// <returns>The <c>IProxy</c> instance previously registered with the given <c>proxyName</c></returns>
		public IProxy retrieveProxy(String proxyName)
        {
			return (IProxy)proxyMap[proxyName];
		}

        /// <summary>
        /// Remove an <c>IProxy</c> from the <c>Model</c>
        /// </summary>
        /// <param name="proxyName">The name of the <c>IProxy</c> instance to be removed</param>
		public IProxy removeProxy(String proxyName)
        {
            IProxy proxy = null;

            if (proxyMap.Contains(proxyName))
            {
                proxy = retrieveProxy(proxyName);
                proxyMap.Remove(proxyName);
            }

            return proxy;
		}

        /// <summary>
        /// Mapping of proxyNames to <c>IProxy</c> instances
        /// </summary>
		protected IDictionary proxyMap;

        /// <summary>
        /// Singleton instance
        /// </summary>
		protected static IModel instance;
    }
}
