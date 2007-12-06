using System;
using System.Collections;

using org.puremvc.csharp.core.view;
using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core.controller
{
    /// <summary>
    /// A Singleton <c>IController</c> implementation.
    /// </summary>
    /// <remarks>
    /// <para>In PureMVC, the <c>Controller</c> class follows the 'Command and Controller' strategy, and assumes these responsibilities:</para>
    /// <list type="bullet">
    /// <item>Remembering which <c>ICommand</c>s are intended to handle which <c>INotifications</c>.</item>
    /// <item>Registering itself as an <c>IObserver</c> with the <c>View</c> for each <c>INotification</c> that it has an <c>ICommand</c> mapping for.</item>
    /// <item>Creating a new instance of the proper <c>ICommand</c> to handle a given <c>INotification</c> when notified by the <c>View</c>.</item>
	/// <item>Calling the <c>ICommand</c>'s <code>execute</code> method, passing in the <c>INotification</c>.</item>
    /// </list>
    /// <para>Your application must register <c>ICommands</c> with the <c>Controller</c>.</para>
    /// <para>The simplest way is to subclass <c>Facade</c>, and use its <c>initializeController</c> method to add your registrations.</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.view.View"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Observer"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    /// <see cref="org.puremvc.csharp.patterns.command.SimpleCommand"/>
    /// <see cref="org.puremvc.csharp.patterns.command.MacroCommand"/>
    public class Controller : IController
    {
        /**
		 * Constructor. 
		 * 
		 * <P>
		 * This <code>IController</code> implementation is a Singleton, 
		 * so you should not call the constructor 
		 * directly, but instead call the static Singleton 
		 * Factory method <code>Controller.getInstance()</code>
		 * 
		 */
		private Controller( )
		{
            commandMap = new Hashtable();	
			initializeController();	
		}
		
		/**
		 * Initialize the Singleton <code>Controller</code> instance.
		 * 
		 * <P>Called automatically by the constructor.</P> 
		 * 
		 * <P>Note that if you are using a subclass of <code>View</code>
		 * in your application, you should <i>also</i> subclass <code>Controller</code>
		 * and override the <code>initializeController</code> method in the 
		 * following way:</P>
		 * 
		 * <listing>
		 *		// ensure that the Controller is talking to my IView implementation
		 *		override public function initializeController(  ) : void 
		 *		{
		 *			view = MyView.getInstance();
		 *		}
		 * </listing>
		 * 
		 * @return void
		 */
		protected void initializeController()
		{
			view = View.getInstance();
		}
	
		/**
		 * <code>Controller</code> Singleton Factory method.
		 * 
		 * @return the Singleton instance of <code>Controller</code>
		 */
		public static IController getInstance()
		{
			return Nested.instance;
		}

        /**
		 * Nested class for thread safe Singleton.
		 */
        private class Nested
        {
            /* Explicit static constructor to tell C# compiler 
             * not to mark type as beforefieldinit. */
            static Nested()
            { }

            internal static readonly IController instance = new Controller();
        }

		/**
		 * If an <code>ICommand</code> has previously been registered 
		 * to handle a the given <code>INotification</code>, then it is executed.
		 * 
		 * @param note an <code>INotification</code>
		 */
		public void executeCommand(INotification note)
		{
			Type commandType = (Type)commandMap[note.getName()];
            if (commandType == null) return;

            Object commandInstance = Activator.CreateInstance(commandType);
            if (commandInstance is ICommand)
            {
                ((ICommand)commandInstance).execute(note);
            }
		}

        /**
         * Register a particular <code>ICommand</code> class as the handler 
         * for a particular <code>INotification</code>.
         * 
         * <P>
         * If an <code>ICommand</code> has already been registered to 
         * handle <code>INotification</code>s with this name, it is no longer
         * used, the new <code>ICommand</code> is used instead.</P>
         * 
         * @param notificationName the name of the <code>INotification</code>
         * @param commandType the <code>Type</code> of the <code>ICommand</code>
         */
        public void registerCommand(String notificationName, Type commandType)
		{
            removeCommand(notificationName);
            commandMap[notificationName] = commandType;
            view.registerObserver(notificationName, new Observer("executeCommand", this));
		}
		
		/**
		 * Remove a previously registered <code>ICommand</code> to <code>INotification</code> mapping.
		 * 
		 * @param notificationName the name of the <code>INotification</code> to remove the <code>ICommand</code> mapping for
		 */
		public void removeCommand(String notificationName)
		{
            if (commandMap.Contains(notificationName))
            {
                commandMap.Remove(notificationName);
            }
		}
		
		// Local reference to View 
		protected IView view;
		
		// Mapping of Notification names to Command Class references
        protected IDictionary commandMap;
    }
}
