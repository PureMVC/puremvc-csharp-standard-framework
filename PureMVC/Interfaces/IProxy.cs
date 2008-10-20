/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Proxy
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, <c>IProxy</c> implementors assume these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Implement a common method which returns the name of the Proxy</item>
    ///     </list>
    ///     <para>Additionally, <c>IProxy</c>s typically:</para>
    ///     <list type="bullet">
    ///         <item>Maintain references to one or more pieces of model data</item>
    ///         <item>Provide methods for manipulating that data</item>
    ///         <item>Generate <c>INotifications</c> when their model data changes</item>
    ///         <item>Expose their name as a <c>public static const</c> called <c>NAME</c></item>
    ///         <item>Encapsulate interaction with local or remote services used to fetch and persist model data</item>
    ///     </list>
    /// </remarks>
    public interface IProxy
    {
		/// <summary>
        /// The Proxy instance name
        /// </summary>
		string ProxyName { get; }

        /// <summary>
        /// The data of the proxy
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
