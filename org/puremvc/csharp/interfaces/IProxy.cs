using System;

namespace org.puremvc.csharp.interfaces
{
    /**
	 * The interface definition for a PureMVC Proxy.
	 *
	 * <P>
	 * In PureMVC, <code>IProxy</code> implementors assume these responsibilities:</P>
	 * <UL>
	 * <LI>Implement a common method which returns the name of the Proxy.</LI>
	 * </UL>
	 * <P>
	 * Additionally, <code>IProxy</code>s typically:</P>
	 * <UL>
	 * <LI>Maintain references to one or more pieces of model data.</LI>
	 * <LI>Provide methods for manipulating that data.</LI>
	 * <LI>Generate <code>INotifications</code> when their model data changes.</LI>
	 * <LI>Expose their name as a <code>public static const</code> called <code>NAME</code>.</LI>
	 * <LI>Encapsulate interaction with local or remote services used to fetch and persist model data.</LI>
	 * </UL>
	 */
    public interface IProxy
    {
        /**
		 * Get the Proxy name
		 * 
		 * @return the Proxy instance name
		 */
        String getProxyName();

        /**
		 * Set the data
		 * 
         * @param data the data of the proxy
		 * @return void
		 */
        void setData(Object data);

        /**
		 * Get the data
		 * 
		 * @return the data object
		 */
        Object getData();
    }
}
