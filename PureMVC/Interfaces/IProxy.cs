//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Proxy.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, <c>IProxy</c> implementors assume these responsibilities:
    ///         <list type="bullet">
    ///             <item>Implement a common method which returns the name of the Proxy.</item>
    ///             <item>Provide methods for setting and getting the data object.</item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         Additionally, <c>IProxy</c>s typically:
    ///         <list type="bullet">
    ///             <item>Maintain references to one or more pieces of model data.</item>
    ///             <item>Provide methods for manipulating that data.</item>
    ///             <item>Generate <c>INotifications</c> when their model data changes.</item>
    ///             <item>Expose their name as a <c>public static const</c> called <c>NAME</c>, if they are not instantiated multiple times.</item>
    ///             <item>Encapsulate interaction with local or remote services used to fetch and persist model data.</item>
    ///         </list>
    ///     </para>
    /// </remarks>
    public interface IProxy: INotifier
    {
        /// <summary>
        /// Get the Proxy name
        /// </summary>
        string ProxyName { get; }

        /// <summary>
        /// Get or Set the data object
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Called by the Model when the Proxy is registered
        /// </summary>
        void OnRegister();

        /// <summary>
        /// Called by the Model when the Proxy is removed
        /// </summary>
        void OnRemove();
    }
}
