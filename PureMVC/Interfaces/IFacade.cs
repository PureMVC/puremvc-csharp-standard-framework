//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Facade.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The Facade Pattern suggests providing a single
    ///         class to act as a central point of communication 
    ///         for a subsystem.
    ///     </para>
    ///     <para>
    ///         In PureMVC, the Facade acts as an interface between
    ///         the core MVC actors (Model, View, Controller) and
    ///         the rest of your application.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IModel"/>
    /// <seealso cref="IView"/>
    /// <seealso cref="IController"/>
    /// <seealso cref="ICommand"/>
    /// <seealso cref="INotification"/>
    public interface IFacade: INotifier
    {
        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxy">the <c>IProxy</c> to be registered with the <c>Model</c>.</param>
        void RegisterProxy(IProxy proxy);

        /// <summary>
        /// Retrieve a <c>IProxy</c> from the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxyName">the name of the <c>IProxy</c> instance to be retrieved.</param>
        /// <returns>the <c>IProxy</c> previously regisetered by <c>proxyName</c> with the <c>Model</c>.</returns>
        IProxy RetrieveProxy(string proxyName);

        /// <summary>
        /// Remove an <c>IProxy</c> instance from the <c>Model</c> by name.
        /// </summary>
        /// <param name="proxyName">the <c>IProxy</c> to remove from the <c>Model</c>.</param>
        /// <returns>the <c>IProxy</c> that was removed from the <c>Model</c></returns>
        IProxy RemoveProxy(string proxyName);

        /// <summary>
        /// Check if a Proxy is registered
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns>whether a Proxy is currently registered with the given <c>proxyName</c>.</returns>
        bool HasProxy(string proxyName);

        /// <summary>
        /// Register an <c>ICommand</c> with the <c>Controller</c>.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c> to associate the <c>ICommand</c> with.</param>
        /// <param name="commandClassRef">a reference to the <c>FuncDelegate</c> of the <c>ICommand</c></param>
        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.
        /// </summary>
        /// <param name="notificationName">the name of the <c>INotification</c> to remove the <c>ICommand</c> mapping for</param>
        void RemoveCommand(string notificationName);

        /// <summary>
        /// Check if a Command is registered for a given Notification 
        /// </summary>
        /// <param name="notificationName"></param>
        /// <returns>whether a Command is currently registered for the given <c>notificationName</c>.</returns>
        bool HasCommand(string notificationName);

        /// <summary>
        /// Register an <c>IMediator</c> instance with the <c>View</c>.
        /// </summary>
        /// <param name="mediator">a reference to the <c>IMediator</c> instance</param>
        void RegisterMediator(IMediator mediator);

        /// <summary>
        /// Retrieve an <c>IMediator</c> instance from the <c>View</c>.
        /// </summary>
        /// <param name="mediatorName">the name of the <c>IMediator</c> instance to retrievve</param>
        /// <returns>the <c>IMediator</c> previously registered with the given <c>mediatorName</c>.</returns>
        IMediator RetrieveMediator(string mediatorName);

        /// <summary>
        /// Remove a <c>IMediator</c> instance from the <c>View</c>.
        /// </summary>
        /// <param name="mediatorName">name of the <c>IMediator</c> instance to be removed</param>
        /// <returns>the <c>IMediator</c> instance previously registered with the given <c>mediatorName</c>.</returns>
        IMediator RemoveMediator(string mediatorName);

        /// <summary>
        /// Check if a Mediator is registered or not
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns>whether a Mediator is registered with the given <c>mediatorName</c>.</returns>
        bool HasMediator(string mediatorName);

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
        void NotifyObservers(INotification notification);
    }
}
