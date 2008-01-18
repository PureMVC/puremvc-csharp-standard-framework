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
    /// 	<para>In PureMVC, the <c>Controller</c> class follows the 'Command and Controller' strategy, and assumes these responsibilities:</para>
    /// 	<list type="bullet">
    /// 		<item>Remembering which <c>ICommand</c>s are intended to handle which <c>INotifications</c>.</item>
    /// 		<item>Registering itself as an <c>IObserver</c> with the <c>View</c> for each <c>INotification</c> that it has an <c>ICommand</c> mapping for.</item>
    /// 		<item>Creating a new instance of the proper <c>ICommand</c> to handle a given <c>INotification</c> when notified by the <c>View</c>.</item>
    /// 		<item>Calling the <c>ICommand</c>'s <c>execute</c> method, passing in the <c>INotification</c>.</item>
    /// 	</list>
    /// 	<para>Your application must register <c>ICommands</c> with the <c>Controller</c>.</para>
    /// 	<para>The simplest way is to subclass <c>Facade</c>, and use its <c>initializeController</c> method to add your registrations.</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.view.View"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Observer"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    /// <see cref="org.puremvc.csharp.patterns.command.SimpleCommand"/>
    /// <see cref="org.puremvc.csharp.patterns.command.MacroCommand"/>
    public class Controller : IController
    {
        /// <summary>
        /// Constructs and initializes a new controller
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This <c>IController</c> implementation is a Singleton, 
        ///         so you should not call the constructor 
        ///         directly, but instead call the static Singleton
        ///         Factory method <c>Controller.getInstance()</c>
        ///     </para>
        /// </remarks>
		protected Controller()
		{
            commandMap = new Hashtable();	
			initializeController();	
		}
		
        /// <summary>
        /// Explicit static constructor to tell C# compiler
        /// not to mark type as beforefieldinit
        /// </summary>
        static Controller()
        { }

        /// <summary>
        /// Initialize the Singleton <c>Controller</c> instance
        /// </summary>
        /// <remarks>
        ///     <para>Called automatically by the constructor</para>
        ///     
        ///     <para>
        ///         Note that if you are using a subclass of <c>View</c>
        ///         in your application, you should also subclass <c>Controller</c>
        ///         and override the <c>initializeController</c> method in the following way:
        ///     </para>
        /// 
        ///     <c>
        ///         // ensure that the Controller is talking to my IView implementation
        ///         public override void initializeController()
        ///         {
        ///             view = MyView.getInstance();
        ///         }
        ///     </c>
        /// </remarks>
        protected virtual void initializeController()
		{
			view = View.getInstance();
		}
	
        /// <summary>
        /// Singleton Factory method
        /// </summary>
        /// <returns>The Singleton instance of <c>Controller</c></returns>
		public static IController getInstance()
		{
			return instance;
		}

        /// <summary>
        /// If an <c>ICommand</c> has previously been registered
        /// to handle a the given <c>INotification</c>, then it is executed.
        /// </summary>
        /// <param name="note">An <c>INotification</c></param>
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

        /// <summary>
        /// Register a particular <c>ICommand</c> class as the handler
        /// for a particular <c>INotification</c>.
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c></param>
        /// <param name="commandType">The <c>Type</c> of the <c>ICommand</c></param>
        /// <remarks>
        ///     <para>
        ///         If an <c>ICommand</c> has already been registered to 
        ///         handle <c>INotification</c>s with this name, it is no longer
        ///         used, the new <c>ICommand</c> is used instead.
        ///     </para>
        /// </remarks> 
        public void registerCommand(String notificationName, Type commandType)
		{
            if (!commandMap.Contains(notificationName))
            {
                view.registerObserver(notificationName, new Observer("executeCommand", this));
            }
            commandMap[notificationName] = commandType;
		}

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping.
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
		public void removeCommand(String notificationName)
		{
            if (commandMap.Contains(notificationName))
            {
                commandMap.Remove(notificationName);
            }
		}
		
        /// <summary>
        /// Local reference to View
        /// </summary>
		protected IView view;
		
        /// <summary>
        /// Mapping of Notification names to Command Class references
        /// </summary>
        protected IDictionary commandMap;

        /// <summary>
        /// Singleton instance
        /// </summary>
		protected static IController instance = new Controller();
    }
}
