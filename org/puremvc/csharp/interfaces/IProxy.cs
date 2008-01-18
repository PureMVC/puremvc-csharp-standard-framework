using System;

namespace org.puremvc.csharp.interfaces
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
        /// Get the Proxy name
        /// </summary>
        /// <returns>The Proxy instance name</returns>
        String getProxyName();

        /// <summary>
        /// Set the data
        /// </summary>
        /// <param name="data">The data of the proxy</param>
        void setData(Object data);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>The data object</returns>
        Object getData();
    }
}
