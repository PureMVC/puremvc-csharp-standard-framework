/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using org.puremvc.csharp.interfaces;

namespace org.puremvc.csharp.patterns.observer
{
    /// <summary>
    /// A base <c>INotification</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>PureMVC does not rely upon underlying event models</para>
    ///     <para>The Observer Pattern as implemented within PureMVC exists to support event-driven communication between the application and the actors of the MVC triad</para>
    ///     <para>Notifications are not meant to be a replacement for Events. Generally, <c>IMediator</c> implementors place event handlers on their view components, which they then handle in the usual way. This may lead to the broadcast of <c>Notification</c>s to trigger <c>ICommand</c>s or to communicate with other <c>IMediators</c>. <c>IProxy</c> and <c>ICommand</c> instances communicate with each other and <c>IMediator</c>s by broadcasting <c>INotification</c>s</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.patterns.observer.Observer"/>
    public class Notification : INotification
    {
        /// <summary>
        /// Constructs a new notification with the specified name, default body and type
        /// </summary>
        /// <param name="name">The name of the <c>Notification</c> instance</param>
        public Notification(String name)
            : this(name, null, null)
		{ }

        /// <summary>
        /// Constructs a new notification with the specified name and body, with the default type
        /// </summary>
        /// <param name="name">The name of the <c>Notification</c> instance</param>
        /// <param name="body">The <c>Notification</c>s body</param>
        public Notification(String name, Object body)
            : this(name, body, null)
		{ }

        /// <summary>
        /// Constructs a new notification with the specified name, body and type
        /// </summary>
        /// <param name="name">The name of the <c>Notification</c> instance</param>
        /// <param name="body">The <c>Notification</c>s body</param>
        /// <param name="type">The type of the <c>Notification</c></param>
        public Notification(String name, Object body, String type)
		{
			this.name = name;
			this.body = body;
			this.type = type;
		}
		
        /// <summary>
        /// Get thename of the <c>Notification</c> instance
        /// </summary>
        /// <returns>The name of the <c>Notification</c> instance</returns>
		public String getName()
		{
			return name;
		}
		
        /// <summary>
        /// Set the body of the <c>Notification</c> instance
        /// </summary>
        /// <param name="body">The body of the <c>Notification</c></param>
		public void setBody(Object body)
		{
			this.body = body;
		}
		
        /// <summary>
        /// Get the body of the <c>Notification</c> instance
        /// </summary>
        /// <returns>The body object</returns>
		public Object getBody()
		{
			return body;
		}

		/// <summary>
		/// Set the type of the <c>Notification</c> instance
		/// </summary>
		/// <param name="type">The type of the <c>Notification</c> instance</param>
		public void setType(String type)
        {
			this.type = type;
		}
		
        /// <summary>
        /// Get the type of the <c>Notification</c> instance
        /// </summary>
        /// <returns>The type</returns>
		public String getType()
        {
			return type;
		}

        /// <summary>
        /// Get the string representation of the <c>Notification instance</c>
        /// </summary>
        /// <returns>The string representation of the <c>Notification</c> instance</returns>
		public String toString()
		{
			String msg = "Notification Name: "+ getName();
			msg += "\nBody:"+ (( body == null )? "null" : body.ToString());
			msg += "\nType:"+ (( type == null )? "null" : type);
			return msg;
		}
		
        /// <summary>
        /// The name of the notification instance 
        /// </summary>
		private String name;

        /// <summary>
        /// The type of the notification instance
        /// </summary>
		private String type;

        /// <summary>
        /// The body of the notification instance
        /// </summary>
		private Object body;
    }
}
