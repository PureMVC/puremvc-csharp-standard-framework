using System;

namespace org.puremvc.csharp.interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC View
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, <c>IView</c> implementors assume these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Maintain a cache of <c>IMediator</c> instances</item>
    ///         <item>Provide methods for registering, retrieving, and removing <c>IMediators</c></item>
    ///         <item>Managing the observer lists for each <c>INotification</c> in the application</item>
    ///         <item>Providing a method for attaching <c>IObservers</c> to an <c>INotification</c>'s observer list</item>
    ///         <item>Providing a method for broadcasting an <c>INotification</c></item>
    ///         <item>Notifying the <c>IObservers</c> of a given <c>INotification</c> when it broadcast</item>
    ///     </list>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.interfaces.IMediator"/>
    /// <see cref="org.puremvc.csharp.interfaces.IObserver"/>
    /// <see cref="org.puremvc.csharp.interfaces.INotification"/>
    public interface IView
    {
        /// <summary>
        /// Register an <c>IObserver</c> to be notified of <c>INotifications</c> with a given name
        /// </summary>
        /// <param name="noteName">The name of the <c>INotifications</c> to notify this <c>IObserver</c> of</param>
        /// <param name="observer">The <c>IObserver</c> to register</param>
		void registerObserver ( String noteName, IObserver observer);

        /// <summary>
        /// Notify the <c>IObservers</c> for a particular <c>INotification</c>
        /// </summary>
        /// <param name="note">The <c>INotification</c> to notify <c>IObservers</c> of</param>
        /// <remarks>
        ///     <para>All previously attached <c>IObservers</c> for this <c>INotification</c>'s list are notified and are passed a reference to the <c>INotification</c> in the order in which they were registered</para>
        /// </remarks>
		void notifyObservers( INotification note );
		
        /// <summary>
        /// Register an <c>IMediator</c> instance with the <c>View</c>
        /// </summary>
        /// <param name="mediator">A a reference to the <c>IMediator</c> instance</param>
        /// <remarks>
        ///     <para>Registers the <c>IMediator</c> so that it can be retrieved by name, and further interrogates the <c>IMediator</c> for its <c>INotification</c> interests</para>
        ///     <para>If the <c>IMediator</c> returns any <c>INotification</c> names to be notified about, an <c>Observer</c> is created encapsulating  the <c>IMediator</c> instance's <c>handleNotification</c> method and registering it as an <c>Observer</c> for all <c>INotifications</c> the <c>IMediator</c> is interested in</para>
        /// </remarks>
		void registerMediator( IMediator mediator );

        /// <summary>
        /// Retrieve an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to retrieve</param>
        /// <returns>The <c>IMediator</c> instance previously registered with the given <c>mediatorName</c></returns>
		IMediator retrieveMediator( String mediatorName );

        /// <summary>
        /// Remove an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
		void removeMediator( String mediatorName );
    }
}
