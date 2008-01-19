/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;

namespace org.puremvc.csharp.interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Facade
    /// </summary>
    /// <remarks>
    ///     <para>The Facade Pattern suggests providing a single class to act as a certal point of communication for subsystems</para>
    ///     <para>In PureMVC, the Facade acts as an interface between the core MVC actors (Model, View, Controller) and the rest of your application</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.interfaces.IModel"/>
    /// <see cref="org.puremvc.csharp.interfaces.IView"/>
    /// <see cref="org.puremvc.csharp.interfaces.IController"/>
    /// <see cref="org.puremvc.csharp.interfaces.ICommand"/>
    /// <see cref="org.puremvc.csharp.interfaces.INotification"/>
    public interface IFacade : INotifier
    {
        /// <summary>
        /// Register an <c>IProxy</c> with the <c>Model</c> by name
        /// </summary>
        /// <param name="proxy">The <c>IProxy</c> to be registered with the <c>Model</c></param>
		void registerProxy(IProxy proxy);

        /// <summary>
        /// Retrieve a <c>IProxy</c> from the <c>Model</c> by name
        /// </summary>
        /// <param name="proxyName">The name of the <c>IProxy</c> instance to be retrieved</param>
        /// <returns>The <c>IProxy</c> previously regisetered by <c>proxyName</c> with the <c>Model</c></returns>
		IProxy retrieveProxy(String proxyName);
		
        /// <summary>
        /// Remove an <c>IProxy</c> instance from the <c>Model</c> by name
        /// </summary>
        /// <param name="proxyName">The <c>IProxy</c> to remove from the <c>Model</c></param>
        IProxy removeProxy(String proxyName);

        /// <summary>
        /// Register an <c>ICommand</c> with the <c>Controller</c>
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotification</c> to associate the <c>ICommand</c> with.</param>
        /// <param name="commandType">A reference to the <c>Type</c> of the <c>ICommand</c></param>
        void registerCommand(String notificationName, Type commandType);

        /// <summary>
        /// Remove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.
        /// </summary>
        /// <param name="notificationName">TRemove a previously registered <c>ICommand</c> to <c>INotification</c> mapping from the Controller.</param>
		void removeCommand(String notificationName);

        /// <summary>
        /// Register an <c>IMediator</c> instance with the <c>View</c>
        /// </summary>
        /// <param name="mediator">A reference to the <c>IMediator</c> instance</param>
		void registerMediator(IMediator mediator);

        /// <summary>
        /// Retrieve an <c>IMediator</c> instance from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to retrieve</param>
        /// <returns>The <c>IMediator</c> previously registered with the given <c>mediatorName</c></returns>
		IMediator retrieveMediator(String mediatorName);

        /// <summary>
        /// Remove a <c>IMediator</c> instance from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
        IMediator removeMediator(String mediatorName);

        /// <summary>
        /// Startup the application
        /// </summary>
        /// <param name="app">A reference to the application instance</param>
        void startup(Object app);
    }
}
