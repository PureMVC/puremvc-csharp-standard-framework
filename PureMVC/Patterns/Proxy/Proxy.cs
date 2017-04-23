//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Proxy
{
    /// <summary>
    /// A base <c>IProxy</c> implementation. 
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, <c>Proxy</c> classes are used to manage parts of the 
    ///         application's data model.
    ///     </para>
    ///     <para>
    ///          A <c>Proxy</c> might simply manage a reference to a local data object, 
    ///          in which case interacting with it might involve setting and 
    ///          getting of its data in synchronous fashion.
    ///     </para>
    ///     <para>
    ///         <c>Proxy</c> classes are also used to encapsulate the application's 
    ///         interaction with remote services to save or retrieve data, in which case,
    ///         we adopt an asyncronous idiom; setting data (or calling a method) on the 
    ///         <c>Proxy</c> and listening for a <c>Notification</c> to be sent 
    ///         when the <c>Proxy</c> has retrieved the data from the service.
    ///     </para>
    /// </remarks>
    /// <seealso cref="PureMVC.Core.Model"/>
    public class Proxy: Notifier, IProxy, INotifier
    {
        /// <summary> Name of the proxy</summary>
        public static string NAME = "Proxy";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="proxyName"></param>
        /// <param name="data"></param>
        public Proxy(string proxyName, object data=null)
        {
            ProxyName = proxyName ?? Proxy.NAME;
            if (data != null) Data = data;
        }

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

        /// <summary>the proxy name</summary>
        public string ProxyName { get; protected set; }

        /// <summary>the data object</summary>
        public object Data { get; set; }
    }
}
