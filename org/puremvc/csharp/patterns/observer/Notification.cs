using System;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.patterns.observer
{
    /**
	 * A base <code>INotification</code> implementation.
	 * 
	 * <P>
	 * PureMVC does not rely upon underlying event models such 
	 * as the one provided with Flash, and ActionScript 3 does 
	 * not have an inherent event model.</P>
	 * 
	 * <P>
	 * The Observer Pattern as implemented within PureMVC exists 
	 * to support event-driven communication between the 
	 * application and the actors of the MVC triad.</P>
	 * 
	 * <P>
	 * Notifications are not meant to be a replacement for Events
	 * in Flex/Flash/Apollo. Generally, <code>IMediator</code> implementors
	 * place event listeners on their view components, which they
	 * then handle in the usual way. This may lead to the broadcast of <code>Notification</code>s to 
	 * trigger <code>ICommand</code>s or to communicate with other <code>IMediators</code>. <code>IProxy</code> and <code>ICommand</code>
	 * instances communicate with each other and <code>IMediator</code>s 
	 * by broadcasting <code>INotification</code>s.</P>
	 * 
	 * <P>
	 * A key difference between Flash <code>Event</code>s and PureMVC 
	 * <code>Notification</code>s is that <code>Event</code>s follow the 
	 * 'Chain of Responsibility' pattern, 'bubbling' up the display hierarchy 
	 * until some parent component handles the <code>Event</code>, while
	 * PureMVC <code>Notification</code>s follow a 'Publish/Subscribe'
	 * pattern. PureMVC classes need not be related to each other in a 
	 * parent/child relationship in order to communicate with one another
	 * using <code>Notification</code>s.
	 * 
	 * @see org.puremvc.patterns.observer.Observer Observer
	 * 
	 */
    public class Notification : INotification
    {
        /**
		 * Constructor. 
		 * 
		 * @param name name of the <code>Notification</code> instance. (required)
		 * @param body the <code>Notification</code> body. (optional)
		 * @param type the type of the <code>Notification</code> (optional)
		 */
		public Notification(String name) : this(name, null, null)
		{ }

        public Notification(String name, Object body) : this(name, body, null)
		{ }

        public Notification(String name, Object body, String type)
		{
			this.name = name;
			this.body = body;
			this.type = type;
		}
		
		/**
		 * Get the name of the <code>Notification</code> instance.
		 * 
		 * @return the name of the <code>Notification</code> instance.
		 */
		public String getName()
		{
			return name;
		}
		
		/**
		 * Set the body of the <code>Notification</code> instance.
		 */
		public void setBody( Object body )
		{
			this.body = body;
		}
		
		/**
		 * Get the body of the <code>Notification</code> instance.
		 * 
		 * @return the body object. 
		 */
		public Object getBody()
		{
			return body;
		}
		
		/**
		 * Set the type of the <code>Notification</code> instance.
		 */
		public void setType( String type )
		{
			this.type = type;
		}
		
		/**
		 * Get the type of the <code>Notification</code> instance.
		 * 
		 * @return the type  
		 */
		public String getType()
		{
			return type;
		}

		/**
		 * Get the string representation of the <code>Notification</code> instance.
		 * 
		 * @return the string representation of the <code>Notification</code> instance.
		 */
		public String toString()
		{
			String msg = "Notification Name: "+ getName();
			msg += "\nBody:"+ (( body == null )? "null" : body.ToString());
			msg += "\nType:"+ (( type == null )? "null" : type);
			return msg;
		}
		
		// the name of the notification instance
		private String name;
		// the type of the notification instance
		private String type;
		// the body of the notification instance
		private Object body;
    }
}
