/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

#endregion

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Model
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, <c>IModel</c> implementors provide access to <c>IProxy</c> objects by named lookup</para>
    ///     <para>An <c>IModel</c> assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Maintain a cache of <c>IProxy</c> instances</item>
    ///         <item>Provide methods for registering, retrieving, and removing <c>IProxy</c> instances</item>
    ///     </list>
    /// </remarks>
    public interface IModel
    {
        /// <summary>
        /// Register an <c>IProxy</c> instance with the <c>Model</c>
        /// </summary>
        /// <param name="proxy">A reference to the proxy object to be held by the <c>Model</c></param>
		void RegisterProxy(IProxy proxy);

        /// <summary>
        /// Retrieve an <c>IProxy</c> instance from the Model
        /// </summary>
        /// <param name="proxyName">The name of the proxy to retrieve</param>
        /// <returns>The <c>IProxy</c> instance previously registered with the given <c>proxyName</c></returns>
		IProxy RetrieveProxy(string proxyName);

        /// <summary>
        /// Remove an <c>IProxy</c> instance from the Model
        /// </summary>
        /// <param name="proxyName">The name of the <c>IProxy</c> instance to be removed</param>
        IProxy RemoveProxy(string proxyName);

		/// <summary>
		/// Check if a Proxy is registered
		/// </summary>
		/// <param name="proxyName">The name of the proxy to check for</param>
		/// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
		bool HasProxy(string proxyName);
    }
}
