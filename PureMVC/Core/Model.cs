//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;
using System.Collections.Concurrent;
using PureMVC.Interfaces;

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
    public class Model: IModel
    {
        /// <summary>
        /// Constructs and initializes a new model
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This <c>IModel</c> implementation is a Singleton, 
        ///         so you should not call the constructor 
        ///         directly, but instead call the static Singleton 
        ///         Factory method <c>Model.getInstance(() => new Model())</c>
        ///     </para>
        /// </remarks>
        /// <exception cref="System.Exception">Thrown if instance for this Singleton key has already been constructed</exception>
        public Model()
        {
            if (instance != null) throw new Exception(Singleton_MSG);
            instance = this;
            proxyMap = new ConcurrentDictionary<string, IProxy>();
            InitializeModel();
        }

        /// <summary>
        /// Initialize the Singleton <c>Model</c> instance.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called automatically by the constructor, this 
        ///         is your opportunity to initialize the Singleton 
        ///         instance in your subclass without overriding the 
        ///         constructor
        ///     </para>
        /// </remarks>
        protected virtual void InitializeModel()
        {
        }

        /// <summary>
        /// <c>Model</c> Singleton Factory method. 
        /// </summary>
        /// <param name="modelFunc">the <c>FuncDelegate</c> of the <c>IModel</c></param>
        /// <returns>the instance for this Singleton key </returns>
        public static IModel GetInstance(Func<IModel> modelFunc)
        {
            if (instance == null) {
                instance = modelFunc();
            }
            return instance;
        }

        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c>.
        /// </summary>
        /// <param name="proxy">proxy an <c>IProxy</c> to be held by the <c>Model</c>.</param>
        public virtual void RegisterProxy(IProxy proxy)
        {
            proxyMap[proxy.ProxyName] = proxy;
            proxy.OnRegister();
        }

        /// <summary>
        /// Retrieve an <c>IProxy</c> from the <c>Model</c>.
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns>the <c>IProxy</c> instance previously registered with the given <c>proxyName</c>.</returns>
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return proxyMap.TryGetValue(proxyName, out IProxy proxy) ? proxy : null;
        }

        /// <summary>
        /// Remove an <c>IProxy</c> from the <c>Model</c>.
        /// </summary>
        /// <param name="proxyName">proxyName name of the <c>IProxy</c> instance to be removed.</param>
        /// <returns>the <c>IProxy</c> that was removed from the <c>Model</c></returns>
        public virtual IProxy RemoveProxy(string proxyName)
        {
            if (proxyMap.TryRemove(proxyName, out IProxy proxy))
            {
                proxy.OnRemove();
            }
            return proxy;
        }

        /// <summary>
        /// Check if a Proxy is registered
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
        public virtual bool HasProxy(string proxyName)
        {
            return proxyMap.ContainsKey(proxyName);
        }

        /// <summary>Mapping of proxyNames to IProxy instances</summary>
        protected readonly ConcurrentDictionary<string, IProxy> proxyMap;

        /// <summary>Singleton instance</summary>
        protected static IModel instance;

        /// <summary>Message Constants</summary>
        protected const string Singleton_MSG = "Model Singleton already constructed!";
    }
}
