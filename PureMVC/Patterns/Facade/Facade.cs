//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;
using PureMVC.Interfaces;
using PureMVC.Core;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// A base Singleton <c>IFacade</c> implementation.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         In PureMVC, the <c>Facade</c> class assumes these 
    ///         responsibilities:
    ///     </para>
    ///     <list type="bullet">
    ///         <item>Initializing the <c>Model</c>, <c>View</c> and <c>Controller</c> Singletons.</item>
    ///         <item>Providing all the methods defined by the <c>IModel, IView, and IController</c> interfaces.</item>
    ///         <item>Providing the ability to override the specific <c>Model</c>, <c>View</c> and <c>Controller</c> Singletons created.</item>
    ///         <item>Providing a single point of contact to the application for registering <c>Commands</c> and notifying <c>Observers</c></item>
    ///     </list>
    /// </remarks>
    /// <seealso cref="PureMVC.Core.Model"/>
    /// <seealso cref="PureMVC.Core.View"/>
    /// <seealso cref="PureMVC.Core.Controller"/>
    public class Facade : IFacade
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This <c>IFacade</c> implementation is a Singleton, 
        ///         so you should not call the constructor 
        ///         directly, but instead call the static Factory method, 
        ///         passing the unique key for this instance 
        ///         <c>Facade.getInstance(() => new Facade())</c>
        ///     </para>
        /// </remarks>
        /// <exception cref="System.Exception">Thrown if instance for this Singleton key has already been constructed</exception>
        public Facade()
        {
            if (instance != null) throw new Exception(Singleton_MSG);
            instance = this;
            InitializeFacade();
        }

        /// <summary>
        /// Initialize the Singleton <c>Facade</c> instance.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called automatically by the constructor. Override in your
        ///         subclass to do any subclass specific initializations. Be
        ///         sure to call <c>super.initializeFacade()</c>, though.
        ///     </para>
        /// </remarks>
        protected virtual void InitializeFacade()
        {
            InitializeModel();
            InitializeController();
            InitializeView();
        }

        /// <summary>
        /// Facade Singleton Factory method
        /// </summary>
        /// <param name="facadeFunc">the <c>FuncDelegate</c> of the <c>IFacade</c></param>
        /// <returns>the Singleton instance of the Facade</returns>
        public static IFacade GetInstance(Func<IFacade> facadeFunc)
        {
            if (instance == null)
            {
                instance = facadeFunc();
            }
            return instance;
        }

        /// <summary>
        /// Initialize the <c>Controller</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called by the <c>initializeFacade</c> method.
        ///         Override this method in your subclass of <c>Facade</c> 
        ///         if one or both of the following are true:
        ///         <list type="bullet">
        ///             <item>You wish to initialize a different <c>IController</c>.</item>
        ///             <item>You have <c>Commands</c> to register with the <c>Controller</c> at startup.</item>
        ///         </list>
        ///     </para>
        ///     <para>
        ///         If you don't want to initialize a different <c>IController</c>, 
        ///         call <c>super.initializeController()</c> at the beginning of your
        ///         method, then register <c>Command</c>s.
        ///     </para>
        /// </remarks>
        protected virtual void InitializeController()
        {
            controller = Controller.GetInstance(() => new Controller());
        }

        /// <summary>
        /// Initialize the <c>Model</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called by the <c>initializeFacade</c> method.
        ///         Override this method in your subclass of <c>Facade</c> 
        ///         if one or both of the following are true:
        ///         <list type="bullet">
        ///             <item>You wish to initialize a different <c>IModel</c>.</item>
        ///             <item>You have <c>Proxy</c>s to register with the <c>Model</c> that do not
        ///                 retrieve a reference to the Facade at construction time.
        ///             </item>
        ///         </list>
        ///     </para>
        ///     <para>
        ///         If you don't want to initialize a different <c>IModel</c>, 
        ///         call <c>super.initializeModel()</c> at the beginning of your
        ///         method, then register <c>Proxy</c>s.
        ///     </para>
        ///     <para>
        ///         Note: This method is <i>rarely</i> overridden; in practice you are more
        ///         likely to use a <c>Command</c> to create and register <c>Proxy</c>s
        ///         with the <c>Model</c>, since <c>Proxy</c>s with mutable data will likely
        ///         need to send <c>INotification</c>s and thus will likely want to fetch a reference to 
        ///         the <c>Facade</c> during their construction. 
        ///     </para>
        /// </remarks>
        protected virtual void InitializeModel()
        {
            model = Model.GetInstance(() => new Model());
        }

        /// <summary>
        /// Initialize the <c>View</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Called by the <c>initializeFacade</c> method.
        ///         Override this method in your subclass of <c>Facade</c> 
        ///         if one or both of the following are true:
        ///         <list type="bullet">
        ///             <item>You wish to initialize a different <c>IView</c>.</item>
        ///             <item>You have <c>Observers</c> to register with the <c>View</c></item>
        ///         </list>
        ///     </para>
        /// </remarks>
        protected virtual void InitializeView()
        {
            view = View.GetInstance(() => new View());
        }

        /// <summary>
        /// Register an <c>ICommand</c> with the <c>Controller</c> by Notification name.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c> to associate the <c>ICommand</c> with</param>
        /// <param name="commandFunc">a reference to the Class of the <c>ICommand</c></param>
        public virtual void RegisterCommand(string notificationName, Func<ICommand> commandFunc)
        {
            controller.RegisterCommand(notificationName, commandFunc);
        }

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
        public virtual void RemoveCommand(string notificationName)
        {
            controller.RemoveCommand(notificationName);
        }

        /// <summary>
        /// Check if a Command is registered for a given Notification 
        /// </summary>
        /// <param name="notificationName"></param>
        /// <returns>whether a Command is currently registered for the given <c>notificationName</c>.</returns>
        public virtual bool HasCommand(string notificationName)
        {
            return controller.HasCommand(notificationName);
        }

        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxy">the <c>IProxy</c> instance to be registered with the <c>Model</c>.</param>
        public virtual void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy(proxy);
        }

        /// <summary>
        /// Retrieve an <c>IProxy</c> from the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxyName">the name of the proxy to be retrieved.</param>
        /// <returns>the <c>IProxy</c> instance previously registered with the given <c>proxyName</c>.</returns>
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy(proxyName);
        }

        /// <summary>
        /// Remove an <c>IProxy</c> from the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxyName">the <c>IProxy</c> to remove from the <c>Model</c>.</param>
        /// <returns>the <c>IProxy</c> that was removed from the <c>Model</c></returns>
        public virtual IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy(proxyName);
        }

        /// <summary>
        /// Check if a Proxy is registered
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
        public virtual bool HasProxy(string proxyName)
        {
            return model.HasProxy(proxyName);
        }

        /// <summary>
        /// Register a <c>IMediator</c> with the <c>View</c>.
        /// </summary>
        /// <param name="mediator">a reference to the <c>IMediator</c></param>
        public virtual void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator(mediator);
        }

        /// <summary>
        /// Retrieve an <c>IMediator</c> from the <c>View</c>.
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns>the <c>IMediator</c> previously registered with the given <c>mediatorName</c>.</returns>
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator(mediatorName);
        }

        /// <summary>
        /// Remove an <c>IMediator</c> from the <c>View</c>.
        /// </summary>
        /// <param name="mediatorName">name of the <c>IMediator</c> to be removed.</param>
        /// <returns>the <c>IMediator</c> that was removed from the <c>View</c></returns>
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator(mediatorName);
        }

        /// <summary>
        /// Check if a Mediator is registered or not
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns>whether a Mediator is registered with the given <c>mediatorName</c>.</returns>
        public virtual bool HasMediator(string mediatorName)
        {
            return view.HasMediator(mediatorName);
        }

        /// <summary>
        /// Create and send an <c>INotification</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Keeps us from having to construct new notification 
        ///         instances in our implementation code.
        ///     </para>
        /// </remarks>
        /// <param name="notificationName">the name of the notiification to send</param>
        /// <param name="body">the body of the notification (optional)</param>
        /// <param name="type">type the type of the notification (optional)</param>
        public virtual void SendNotification(string notificationName, object body = null, string type = null)
        {
            NotifyObservers(new Notification(notificationName, body, type));
        }

        /// <summary>
        /// Notify <c>Observer</c>s.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This method is left public mostly for backward 
        ///         compatibility, and to allow you to send custom 
        ///         notification classes using the facade.
        ///     </para>
        ///     <para>
        ///         Usually you should just call sendNotification
        ///         and pass the parameters, never having to 
        ///         construct the notification yourself.
        ///     </para>
        /// </remarks>
        /// <param name="notification">the <c>INotification</c> to have the <c>View</c> notify <c>Observers</c> of.</param>
        public virtual void NotifyObservers(INotification notification)
        {
            view.NotifyObservers(notification);
        }

        /// <summary>References to Controller</summary>
        protected IController controller;
        /// <summary>Reference to Model</summary>
        protected IModel model;
        /// <summary>References to View</summary>
        protected IView view;

        /// <summary>Singleton instance</summary>
        protected static IFacade instance;

        /// <summary>Message Constants</summary>
        protected const string Singleton_MSG = "Facade Singleton already constructed!";
    }
}
