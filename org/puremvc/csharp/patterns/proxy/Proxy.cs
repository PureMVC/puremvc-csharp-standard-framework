using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.facade;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.proxy
{
    /**
	 * A base <code>IProxy</code> implementation. 
	 * 
	 * <P>
	 * In PureMVC, <code>Proxy</code> classes are used to manage parts of the 
	 * application's data model. </P>
	 * 
	 * <P>
	 * A <code>Proxy</code> might simply manage a reference to a local data object, 
	 * in which case interacting with it might involve setting and 
	 * getting of its data in synchronous fashion.</P>
	 * 
	 * <P>
	 * <code>Proxy</code> classes are also used to encapsulate the application's 
	 * interaction with remote services to save or retrieve data, in which case, 
	 * we adopt an asyncronous idiom; setting data (or calling a method) on the 
	 * <code>Proxy</code> and listening for a <code>Notification</code> to be sent 
	 * when the <code>Proxy</code> has retrieved the data from the service. </P>
	 * 
	 * @see org.puremvc.{port}.core.model.Model Model
	 */
    public class Proxy : Notifier, IProxy, INotifier
    {
        public static String NAME = "Proxy";
		
		/**
		 * Constructor
		 */
        public Proxy() 
            : this(NAME, null)
        { }

        public Proxy(String proxyName)
            : this(proxyName, null)
        { }

		public Proxy( String proxyName , Object data ) 
		{
			
			this.proxyName = (proxyName != null)? proxyName : NAME; 
			if (data != null) setData(data);
		}

		/**
		 * Get the proxy name
		 */
		public String getProxyName()
		{
			return proxyName;
		}		
		
		/**
		 * Set the data object
		 */
		public void setData( Object data )
		{
			this.data = data;
		}
		
		/**
		 * Get the data object
		 */
		public Object getData()
		{
			return data;
		}		
		
		// the proxy name
		protected String proxyName;
		
		// the data object
		protected Object data;
    }
}
