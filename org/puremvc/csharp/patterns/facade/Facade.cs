/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using org.puremvc.csharp.core;
using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.facade
{
    /// <summary>
    /// A base Singleton <c>IFacade</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, the <c>Facade</c> class assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Initializing the <c>Model</c>, <c>View</c> and <c>Controller</c> Singletons</item>
    ///         <item>Providing all the methods defined by the <c>IModel, IView, &amp; IController</c> interfaces</item>
    ///         <item>Providing the ability to override the specific <c>Model</c>, <c>View</c> and <c>Controller</c> Singletons created</item>
    ///         <item>Providing a single point of contact to the application for registering <c>Commands</c> and notifying <c>Observers</c></item>
    ///     </list>
    ///     <example>
    ///         <code>
    ///	using org.puremvc.csharp.patterns.facade;
    /// 
    ///	using com.me.myapp.model;
    ///	using com.me.myapp.view;
    ///	using com.me.myapp.controller;
    /// 
    ///	public class MyFacade : Facade
    ///	{
    ///		// Notification constants. The Facade is the ideal
    ///		// location for these constants, since any part
    ///		// of the application participating in PureMVC 
    ///		// Observer Notification will know the Facade.
    ///		public static const string GO_COMMAND = "go";
    /// 
    ///     // we aren't allowed to initialize new instances from outside this class
    ///     protected MyFacade() {}
    /// 
    ///     // we must specify the type of instance
    ///     static MyFacade()
    ///     {
    ///         instance = new MyFacade();
    ///     }
    /// 
    ///		// Override Singleton Factory method 
    ///		public new static MyFacade getInstance() {
    ///			return instance as MyFacade;
    ///		}
    /// 		
    ///		// optional initialization hook for Facade
    ///		public override void initializeFacade() {
    ///			base.initializeFacade();
    ///			// do any special subclass initialization here
    ///		}
    ///	
    ///		// optional initialization hook for Controller
    ///		public override void initializeController() {
    ///			// call base to use the PureMVC Controller Singleton. 
    ///			base.initializeController();
    /// 
    ///			// Otherwise, if you're implmenting your own
    ///			// IController, then instead do:
    ///			// if ( controller != null ) return;
    ///			// controller = MyAppController.getInstance();
    /// 		
    ///			// do any special subclass initialization here
    ///			// such as registering Commands
    ///			registerCommand( GO_COMMAND, com.me.myapp.controller.GoCommand )
    ///		}
    ///	
    ///		// optional initialization hook for Model
    ///		public override void initializeModel() {
    ///			// call base to use the PureMVC Model Singleton. 
    ///			base.initializeModel();
    /// 
    ///			// Otherwise, if you're implmenting your own
    ///			// IModel, then instead do:
    ///			// if ( model != null ) return;
    ///			// model = MyAppModel.getInstance();
    /// 		
    ///			// do any special subclass initialization here
    ///			// such as creating and registering Model proxys
    ///			// that don't require a facade reference at
    ///			// construction time, such as fixed type lists
    ///			// that never need to send Notifications.
    ///			regsiterProxy( new USStateNamesProxy() );
    /// 			
    ///			// CAREFUL: Can't reference Facade instance in constructor 
    ///			// of new Proxys from here, since this step is part of
    ///			// Facade construction!  Usually, Proxys needing to send 
    ///			// notifications are registered elsewhere in the app 
    ///			// for this reason.
    ///		}
    ///	
    ///		// optional initialization hook for View
    ///		public override void initializeView() {
    ///			// call base to use the PureMVC View Singleton. 
    ///			base.initializeView();
    /// 
    ///			// Otherwise, if you're implmenting your own
    ///			// IView, then instead do:
    ///			// if ( view != null ) return;
    ///			// view = MyAppView.getInstance();
    /// 		
    ///			// do any special subclass initialization here
    ///			// such as creating and registering Mediators
    ///			// that do not need a Facade reference at construction
    ///			// time.
    ///			registerMediator( new LoginMediator() ); 
    /// 
    ///			// CAREFUL: Can't reference Facade instance in constructor 
    ///			// of new Mediators from here, since this is a step
    ///			// in Facade construction! Usually, all Mediators need 
    ///			// receive notifications, and are registered elsewhere in 
    ///			// the app for this reason.
    ///		}
    ///	}
    ///         </code>
    ///     </example>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.model.Model"/>
    /// <see cref="org.puremvc.csharp.core.view.View"/>
    /// <see cref="org.puremvc.csharp.core.controller.Controller"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    /// <see cref="org.puremvc.csharp.patterns.mediator.Mediator"/>
    /// <see cref="org.puremvc.csharp.patterns.proxy.Proxy"/>
    /// <see cref="org.puremvc.csharp.patterns.command.SimpleCommand"/>
    /// <see cref="org.puremvc.csharp.patterns.command.MacroCommand"/>
    public class Facade : IFacade
    {
        /// <summary>
        /// Constructor that initializes the Facade
        /// </summary>
        /// <remarks>
        ///     <para>This <c>IFacade</c> implementation is a Singleton, so you should not call the constructor directly, but instead call the static Singleton Factory method <c>Facade.getInstance()</c></para>
        /// </remarks>
        protected Facade() 
        {
			initializeFacade();	
		}

        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        ///</summary>
        static Facade()
        {
            instance = new Facade();
        }

        /// <summary>
        /// Initialize the Singleton <c>Facade</c> instance
        /// </summary>
        /// <remarks>
        /// <para>Called automatically by the constructor. Override in your subclass to do any subclass specific initializations. Be sure to call <c>base.initializeFacade()</c>, though</para>
        /// </remarks>
        protected virtual void initializeFacade()
        {
			initializeModel();
			initializeController();
			initializeView();
		}

        /// <summary>
        /// Facade Singleton Factory method
        /// </summary>
        /// <returns>The Singleton instance of the Facade</returns>
		public static IFacade getInstance()
        {
            return instance;
		}

        /// <summary>
        /// Initialize the <c>Controller</c>
        /// </summary>
        /// <remarks>
        ///     <para>Called by the <c>initializeFacade</c> method. Override this method in your subclass of <c>Facade</c> if one or both of the following are true:</para>
        ///     <list type="bullet">
        ///         <item>You wish to initialize a different <c>IController</c></item>
        ///         <item>You have <c>Commands</c> to register with the <c>Controller</c> at startup</item>
        ///     </list>
        ///     <para>If you don't want to initialize a different <c>IController</c>, call <c>base.initializeController()</c> at the beginning of your method, then register <c>Command</c>s</para>
        /// </remarks>
		protected virtual void initializeController()
        {
			if (controller != null) return;
			controller = Controller.getInstance();
		}

        /// <summary>
        /// Initialize the <c>Model</c>
        /// </summary>
        /// <remarks>
        ///     <para>Called by the <c>initializeFacade</c> method. Override this method in your subclass of <c>Facade</c> if one or both of the following are true:</para>
        ///     <list type="bullet">
        ///         <item>You wish to initialize a different <c>IModel</c></item>
        ///         <item>You have <c>Proxy</c>s to register with the Model that do not retrieve a reference to the Facade at construction time</item>
        ///     </list>
        ///     <para>If you don't want to initialize a different <c>IModel</c>, call <c>base.initializeModel()</c> at the beginning of your method, then register <c>Proxy</c>s</para>
        ///     <para>Note: This method is <i>rarely</i> overridden; in practice you are more likely to use a <c>Command</c> to create and register <c>Proxy</c>s with the <c>Model</c>, since <c>Proxy</c>s with mutable data will likely need to send <c>INotification</c>s and thus will likely want to fetch a reference to the <c>Facade</c> during their construction</para>
        /// </remarks>
        protected virtual void initializeModel()
        {
			if (model != null) return;
			model = Model.getInstance();
		}
		
        /// <summary>
        /// Initialize the <c>View</c>
        /// </summary>
        /// <remarks>
        ///     <para>Called by the <c>initializeFacade</c> method. Override this method in your subclass of <c>Facade</c> if one or both of the following are true:</para>
        ///     <list type="bullet">
        ///         <item>You wish to initialize a different <c>IView</c></item>
        ///         <item>You have <c>Observers</c> to register with the <c>View</c></item>
        ///     </list>
        ///     <para>If you don't want to initialize a different <c>IView</c>, call <c>base.initializeView()</c> at the beginning of your method, then register <c>IMediator</c> instances</para>
        ///     <para>Note: This method is <i>rarely</i> overridden; in practice you are more likely to use a <c>Command</c> to create and register <c>Mediator</c>s with the <c>View</c>, since <c>IMediator</c> instances will need to send <c>INotification</c>s and thus will likely want to fetch a reference to the <c>Facade</c> during their construction</para>
        /// </remarks>
        protected virtual void initializeView()
        {
			if (view != null) return;
			view = View.getInstance();
		}

        /// <summary>
        /// Register an <c>ICommand</c> with the <c>Controller</c>
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c> to associate the <c>ICommand</c> with.</param>
        /// <param name="commandType">A reference to the <c>Type</c> of the <c>ICommand</c></param>
        public void registerCommand(String notificationName, Type commandType) 
        {
			controller.registerCommand(notificationName, commandType);
		}

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.
        /// </summary>
        /// <param name="notificationName">TRemove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.</param>
        public void removeCommand(String notificationName)
        {
			controller.removeCommand(notificationName);
		}

		/// <summary>
		/// Check if a Command is registered for a given Notification 
		/// </summary>
		/// <param name="notificationName">The name of the <c>INotification</c> to check for.</param>
		/// <returns>whether a Command is currently registered for the given <c>notificationName</c>.</returns>
		public Boolean hasCommand(String notificationName)
		{
			return controller.hasCommand(notificationName);
		}

        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c> by name
        /// </summary>
        /// <param name="proxy">The <c>IProxy</c> to be registered with the <c>Model</c></param>
        public void registerProxy(IProxy proxy)
        {
			model.registerProxy (proxy);	
		}

        /// <summary>
        /// Retrieve a <c>IProxy</c> from the <c>Model</c> by name
        /// </summary>
        /// <param name="proxyName">The name of the <c>IProxy</c> instance to be retrieved</param>
        /// <returns>The <c>IProxy</c> previously regisetered by <c>proxyName</c> with the <c>Model</c></returns>
        public IProxy retrieveProxy(String proxyName)
        {
			return model.retrieveProxy (proxyName);	
		}

        /// <summary>
        /// Remove an <c>IProxy</c> instance from the <c>Model</c> by name
        /// </summary>
        /// <param name="proxyName">The <c>IProxy</c> to remove from the <c>Model</c></param>
        public IProxy removeProxy(String proxyName) 
        {
            IProxy proxy = null;
            if (model != null) proxy = model.removeProxy(proxyName);
            return proxy;
		}

		/// <summary>
		/// Check if a Proxy is registered
		/// </summary>
		/// <param name="proxyName">The name of the <c>IProxy</c> instance to check for</param>
		/// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
		public Boolean hasProxy(String proxyName)
		{
			return model.hasProxy(proxyName);
		}

        /// <summary>
        /// Register an <c>IMediator</c> instance with the <c>View</c>
        /// </summary>
        /// <param name="mediator">A reference to the <c>IMediator</c> instance</param>
        public void registerMediator(IMediator mediator)
        {
			if (view != null) view.registerMediator(mediator);
		}

        /// <summary>
        /// Retrieve an <c>IMediator</c> instance from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to retrieve</param>
        /// <returns>The <c>IMediator</c> previously registered with the given <c>mediatorName</c></returns>
        public IMediator retrieveMediator(String mediatorName)
        {
			return view.retrieveMediator( mediatorName ) as IMediator;
		}

        /// <summary>
        /// Remove a <c>IMediator</c> instance from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
        public IMediator removeMediator(String mediatorName)
        {
            IMediator mediator = null;
            if (view != null) mediator = view.removeMediator(mediatorName);
            return mediator;
        }

		/// <summary>
		/// Check if a Mediator is registered or not
		/// </summary>
		/// <param name="mediatorName">The name of the <c>IMediator</c> instance to check for</param>
		/// <returns>whether a Mediator is registered with the given <code>mediatorName</code>.</returns>
		public Boolean hasMediator(String mediatorName)
		{
			return view.hasMediator(mediatorName);
		}

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notiification to send</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName)
        {
            notifyObservers(new Notification(notificationName));
        }

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName, Object body)
        {
            notifyObservers(new Notification(notificationName, body));
        }

        /// <summary>
        /// Send an <c>INotification</c>
        /// </summary>
        /// <param name="notificationName">The name of the notification to send</param>
        /// <param name="body">The body of the notification</param>
        /// <param name="type">The type of the notification</param>
        /// <remarks>Keeps us from having to construct new notification instances in our implementation code</remarks>
        public void sendNotification(String notificationName, Object body, String type)
        {
            notifyObservers(new Notification(notificationName, body, type));
        }

        /// <summary>
        /// Notify <c>Observer</c>s of an <c>INotification</c>
        /// </summary>
        /// <remarks>This method is left public mostly for backward compatibility, and to allow you to send custom notification classes using the facade.</remarks>
        /// <remarks>Usually you should just call sendNotification and pass the parameters, never having to construct the notification yourself.</remarks>
        /// <param name="notification">The <c>INotification</c> to have the <c>View</c> notify observers of</param>
        public void notifyObservers(INotification notification)
        {
			if ( view != null ) view.notifyObservers( notification );
		}

        /// <summary>
        /// Private reference to the Controller
        /// </summary>
		protected IController controller;

        /// <summary>
        /// Private reference to the Model
        /// </summary>
        protected IModel model;

        /// <summary>
        /// Private reference to the View
        /// </summary>
        protected IView view;

        /// <summary>
        /// The Singleton Facade Instance
        /// </summary>
        protected static IFacade instance;
    }
}
