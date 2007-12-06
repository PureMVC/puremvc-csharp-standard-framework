using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.command
{
    /**
	 * A base <code>ICommand</code> implementation.
	 * 
	 * <P>
	 * Your subclass should override the <code>execute</code> 
	 * method where your business logic will handle the <code>INotification</code>. </P>
	 * 
	 * @see org.puremvc.core.controller.Controller Controller
	 * @see org.puremvc.patterns.observer.Notification Notification
	 * @see org.puremvc.patterns.command.MacroCommand MacroCommand
	 */
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        /**
		 * Fulfill the use-case initiated by the given <code>INotification</code>.
		 * 
		 * <P>
		 * In the Command Pattern, an application use-case typically
		 * begins with some user action, which results in an <code>INotification</code> being broadcast, which 
		 * is handled by business logic in the <code>execute</code> method of an
		 * <code>ICommand</code>.</P>
		 * 
		 * @param notification the <code>INotification</code> to handle.
		 */
		public virtual void execute( INotification notification )
		{ }
    }
}
