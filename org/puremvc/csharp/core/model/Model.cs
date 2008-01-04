using System;
using System.Collections;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.core.model
{
    /**
	 * A Singleton <code>IModel</code> implementation.
	 * 
	 * <P>
	 * In PureMVC, the <code>Model</code> class provides
	 * access to model objects (Proxies) by named lookup. 
	 * 
	 * <P>
	 * The <code>Model</code> assumes these responsibilities:</P>
	 * 
	 * <UL>
	 * <LI>Maintain a cache of <code>IProxy</code> instances.</LI>
	 * <LI>Provide methods for registering, retrieving, and removing 
	 * <code>IProxy</code> instances.</LI>
	 * </UL>
	 * 
	 * <P>
	 * Your application must register <code>IProxy</code> instances 
	 * with the <code>Model</code>. Typically, you use an 
	 * <code>ICommand</code> to create and register <code>IProxy</code> 
	 * instances once the <code>Facade</code> has initialized the Core 
	 * actors.</p>
	 *
	 * @see org.puremvc.patterns.proxy.Proxy Proxy
	 * @see org.puremvc.interfaces.IProxy IProxy
	 */
    public class Model : IModel
    {
        /**
		 * Constructor. 
		 * 
		 * <P>
		 * This <code>IModel</code> implementation is a Singleton, 
		 * so you should not call the constructor 
		 * directly, but instead call the static Singleton 
		 * Factory method <code>Model.getInstance()</code>
		 * 
		 * @throws Error Error if Singleton instance has already been constructed
		 * 
		 */
		protected Model()
		{
			proxyMap = new Hashtable();	
			initializeModel();	
		}
		
        /**
         * Explicit static constructor to tell C# compiler
         * not to mark type as beforefieldinit
         */
        static Model()
        { }

		/**
		 * Initialize the Singleton <code>Model</code> instance.
		 * 
		 * <P>
		 * Called automatically by the constructor, this
		 * is your opportunity to initialize the Singleton
		 * instance in your subclass without overriding the
		 * constructor.</P>
		 * 
		 * @return void
		 */
        protected virtual void initializeModel()
		{ }
				
		/**
		 * <code>Model</code> Singleton Factory method.
		 * 
		 * @return the Singleton instance
		 */
		public static IModel getInstance() 
		{
			return instance;
		}

		/**
		 * Register an <code>IProxy</code> with the <code>Model</code>.
		 * 
		 * @param proxy an <code>IProxy</code> to be held by the <code>Model</code>.
		 */
		public void registerProxy(IProxy proxy)
		{
            removeProxy(proxy.getProxyName());
            proxyMap[proxy.getProxyName()] = proxy;
		}

		/**
		 * Retrieve an <code>IProxy</code> from the <code>Model</code>.
		 * 
		 * @param proxyName
		 * @return the <code>IProxy</code> instance previously registered with the given <code>proxyName</code>.
		 */
		public IProxy retrieveProxy(String proxyName) 
		{
			return (IProxy)proxyMap[proxyName];
		}

		/**
		 * Remove an <code>IProxy</code> from the <code>Model</code>.
		 * 
		 * @param proxyName name of the <code>IProxy</code> instance to be removed.
		 */
		public void removeProxy(String proxyName)
		{
            if (proxyMap.Contains(proxyName))
            {
                proxyMap.Remove(proxyName);
            }
		}

		// Mapping of proxyNames to IProxy instances
		protected IDictionary proxyMap;

		// Singleton instance
		protected static IModel instance = new Model();
    }
}
