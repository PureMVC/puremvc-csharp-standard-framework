/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core
{
    /// <summary>
    /// A Singleton <c>IView</c> implementation.
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, the <c>View</c> class assumes these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Maintain a cache of <c>IMediator</c> instances</item>
    ///         <item>Provide methods for registering, retrieving, and removing <c>IMediators</c></item>
    ///         <item>Managing the observer lists for each <c>INotification</c> in the application</item>
    ///         <item>Providing a method for attaching <c>IObservers</c> to an <c>INotification</c>'s observer list</item>
    ///         <item>Providing a method for broadcasting an <c>INotification</c></item>
    ///         <item>Notifying the <c>IObservers</c> of a given <c>INotification</c> when it broadcast</item>
    ///     </list>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.patterns.mediator.Mediator"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Observer"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    public class View : IView
    {
        /// <summary>
        /// Constructs and initializes a new view
        /// </summary>
        /// <remarks>
        /// <para>This <c>IView</c> implementation is a Singleton, so you should not call the constructor directly, but instead call the static Singleton Factory method <c>View.getInstance()</c></para>
        /// </remarks>
		protected View()
		{
			mediatorMap = new Dictionary<String, IMediator>();
			observerMap = new Dictionary<String, IList<IObserver>>();
            initializeView();
		}
		
        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        /// </summary>
        static View()
        {
            instance = new View();
        }

        /// <summary>
        /// Initialize the Singleton View instance
        /// </summary>
        /// <remarks>
        /// <para>Called automatically by the constructor, this is your opportunity to initialize the Singleton instance in your subclass without overriding the constructor</para>
        /// </remarks>
        protected virtual void initializeView()
		{ }

        /// <summary>
        /// View Singleton Factory method
        /// </summary>
        /// <returns>The Singleton instance of <c>View</c></returns>
        public static IView getInstance() 
		{
			return instance;
		}

        /// <summary>
        /// Register an <c>IObserver</c> to be notified of <c>INotifications</c> with a given name
        /// </summary>
        /// <param name="notificationName">The name of the <c>INotifications</c> to notify this <c>IObserver</c> of</param>
        /// <param name="observer">The <c>IObserver</c> to register</param>
		public void registerObserver(String notificationName, IObserver observer)
		{
			if (!observerMap.ContainsKey(notificationName))
			{
				observerMap[notificationName] = new List<IObserver>();
			}

			observerMap[notificationName].Add(observer);
		}

        /// <summary>
        /// Notify the <c>IObservers</c> for a particular <c>INotification</c>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to notify <c>IObservers</c> of</param>
        /// <remarks>
        /// <para>All previously attached <c>IObservers</c> for this <c>INotification</c>'s list are notified and are passed a reference to the <c>INotification</c> in the order in which they were registered</para>
        /// </remarks>
		public void notifyObservers(INotification notification)
		{
			if (observerMap.ContainsKey(notification.getName()))
			{
				// Get a reference to the observers list for this notification name
				IList<IObserver> observers_ref = observerMap[notification.getName()];
				// Copy observers from reference array to working array, 
				// since the reference array may change during the notification loop
				IList<IObserver> observers = new List<IObserver>(observers_ref);

				// Notify Observers from the working array				
				for (int i = 0; i < observers.Count; i++)
				{
					IObserver observer = observers[i];
					observer.notifyObserver(notification);
				}
			}
		}

		/// <summary>
		/// Remove the observer for a given notifyContext from an observer list for a given Notification name.
		/// </summary>
		/// <param name="notificationName">which observer list to remove from</param>
		/// <param name="notifyContext">remove the observer with this object as its notifyContext</param>
		public void removeObserver(String notificationName, Object notifyContext)
		{
			// the observer list for the notification under inspection
			if (observerMap.ContainsKey(notificationName))
			{
				IList<IObserver> observers = observerMap[notificationName];

				// find the observer for the notifyContext
				for (int i = 0; i < observers.Count; i++)
				{
					if (observers[i].compareNotifyContext(notifyContext))
					{
						// there can only be one Observer for a given notifyContext 
						// in any given Observer list, so remove it and break
						observers.RemoveAt(i);
						break;
					}
				}

				// Also, when a Notification's Observer list length falls to 
				// zero, delete the notification key from the observer map
				if (observers.Count == 0)
				{
					observerMap.Remove(notificationName);
				}
			}
		}

        /// <summary>
        /// Register an <c>IMediator</c> instance with the <c>View</c>
        /// </summary>
        /// <param name="mediator">A reference to the <c>IMediator</c> instance</param>
        /// <remarks>
        ///     <para>Registers the <c>IMediator</c> so that it can be retrieved by name, and further interrogates the <c>IMediator</c> for its <c>INotification</c> interests</para>
        ///     <para>If the <c>IMediator</c> returns any <c>INotification</c> names to be notified about, an <c>Observer</c> is created encapsulating the <c>IMediator</c> instance's <c>handleNotification</c> method and registering it as an <c>Observer</c> for all <c>INotifications</c> the <c>IMediator</c> is interested in</para>
        /// </remarks>
		public void registerMediator(IMediator mediator)
		{
			// do not allow re-registration (you must to removeMediator fist)
			if (mediatorMap.ContainsKey(mediator.getMediatorName())) return;

			// Register the Mediator for retrieval by name
			mediatorMap[mediator.getMediatorName()] = mediator;

			// Get Notification interests, if any.
			IList<String> interests = mediator.listNotificationInterests();

			// Register Mediator as an observer for each of its notification interests
			if (interests.Count > 0)
			{
				// Create Observer
				IObserver observer = new Observer("handleNotification", mediator);

				// Register Mediator as Observer for its list of Notification interests
				for (int i = 0; i < interests.Count; i++)
				{
					registerObserver(interests[i].ToString(), observer);
				}
			}

			// alert the mediator that it has been registered
			mediator.onRegister();
		}

        /// <summary>
        /// Retrieve an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to retrieve</param>
        /// <returns>The <c>IMediator</c> instance previously registered with the given <c>mediatorName</c></returns>
        public IMediator retrieveMediator(String mediatorName)
		{
			if (!mediatorMap.ContainsKey(mediatorName)) return null;
			return mediatorMap[mediatorName];
		}

        /// <summary>
        /// Remove an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
		public IMediator removeMediator(String mediatorName)
		{
			// Retrieve the named mediator
			if (!mediatorMap.ContainsKey(mediatorName)) return null;
			IMediator mediator = (IMediator) mediatorMap[mediatorName];

			// for every notification this mediator is interested in...
			IList<String> interests = mediator.listNotificationInterests();

			for (int i = 0; i < interests.Count; i++)
			{
				// remove the observer linking the mediator 
				// to the notification interest
				removeObserver(interests[i], mediator);
			}

			// remove the mediator from the map		
			mediatorMap.Remove(mediatorName);

			// alert the mediator that it has been removed
			mediator.onRemove();
			return mediator;
		}
		
		/// <summary>
		/// Check if a Mediator is registered or not
		/// </summary>
		/// <param name="mediatorName"></param>
		/// <returns>whether a Mediator is registered with the given <code>mediatorName</code>.</returns>
		public Boolean hasMediator(String mediatorName)
		{
			return mediatorMap.ContainsKey(mediatorName);
		}
						
        /// <summary>
        /// Mapping of Mediator names to Mediator instances
        /// </summary>
		protected IDictionary<String, IMediator> mediatorMap;

        /// <summary>
        /// Mapping of Notification names to Observer lists
        /// </summary>
		protected IDictionary<String, IList<IObserver>> observerMap;
		
        /// <summary>
        /// Singleton instance
        /// </summary>
		protected static IView instance;
    }
}
