using System;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;
using org.puremvc.csharp.patterns.facade;

namespace org.puremvc.csharp.patterns.observer
{
    /**
	 * A Base <code>INotifier</code> implementation.
	 * 
	 * <P>
	 * <code>MacroCommand, Command, Mediator</code> and <code>Proxy</code> 
	 * all have a need to send <code>Notifications</code>. <P>
	 * <P>
	 * The <code>INotifier</code> interface provides a common method called
	 * <code>sendNotification</code> that relieves implementation code of 
	 * the necessity to actually construct <code>Notifications</code>.</P>
	 * 
	 * <P>
	 * The <code>Notifier</code> class, which all of the above mentioned classes
	 * extend, provides an initialized reference to the <code>Facade</code>
	 * Singleton, which is required for the convienience method
	 * for sending <code>Notifications</code>, but also eases implementation as these
	 * classes have frequent <code>Facade</code> interactions and usually require
	 * access to the facade anyway.</P>
	 * 
	 * @see org.puremvc.patterns.facade.Facade Facade
	 * @see org.puremvc.patterns.mediator.Mediator Mediator
	 * @see org.puremvc.patterns.proxy.Proxy Proxy
	 * @see org.puremvc.patterns.command.SimpleCommand SimpleCommand
	 * @see org.puremvc.patterns.command.MacroCommand MacroCommand
	 */
    public class Notifier : INotifier
    {
        /**
		 * Send an <code>INotification</code>s.
		 * 
		 * <P>
		 * Keeps us from having to construct new notification 
		 * instances in our implementation code.
		 * @param notificationName the name of the notiification to send
		 * @param body the body of the notification (optional)
		 * @param type the type of the notification (optional)
		 */ 
		public void sendNotification( String notificationName ) 
		{
			facade.notifyObservers( new Notification( notificationName ) );
		}

        public void sendNotification( String notificationName, Object body )
		{
			facade.notifyObservers( new Notification( notificationName, body ) );
		}

        public void sendNotification( String notificationName, Object body, String type )
		{
			facade.notifyObservers( new Notification( notificationName, body, type ) );
		}
		
		// Local reference to the Facade Singleton
		protected IFacade facade = Facade.getInstance();
    }
}
