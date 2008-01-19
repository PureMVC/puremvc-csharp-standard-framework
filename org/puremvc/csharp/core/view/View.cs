/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core.view
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
            mediatorMap = new Hashtable();
            observerMap = new Hashtable();
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
		public void registerObserver (String notificationName, IObserver observer)
		{
			if(!observerMap.Contains(notificationName)) 
            {
                observerMap[notificationName] = new ArrayList();
			}
            ((IList)observerMap[notificationName]).Add(observer);
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
            if(observerMap.Contains(notification.getName())) 
            {
                IList observers = (IList)observerMap[notification.getName()];
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserver observer = (IObserver)observers[i];
                    observer.notifyObserver(notification);
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
        public void registerMediator( IMediator mediator )
		{
            // Register the Mediator for retrieval by name
            mediatorMap[mediator.getMediatorName()] = mediator;
			
            // Get Notification interests, if any.
            IList interests = mediator.listNotificationInterests();
            if (interests.Count == 0) return;
			
            // Create Observer
            IObserver observer = new Observer("handleNotification", mediator);
			
            // Register Mediator as Observer for its list of Notification interests
            for (int i = 0;  i < interests.Count; i++) 
            {
                registerObserver(interests[i].ToString(),  observer);
            }
		}

        /// <summary>
        /// Retrieve an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to retrieve</param>
        /// <returns>The <c>IMediator</c> instance previously registered with the given <c>mediatorName</c></returns>
        public IMediator retrieveMediator(String mediatorName)
		{
			return (IMediator)mediatorMap[mediatorName];
		}

        /// <summary>
        /// Remove an <c>IMediator</c> from the <c>View</c>
        /// </summary>
        /// <param name="mediatorName">The name of the <c>IMediator</c> instance to be removed</param>
		public IMediator removeMediator(String mediatorName)
		{
            // Go through the observer list for each notification 
            // in the observer map and remove all Observers with a 
            // reference to the Mediator being removed.
            IList keysToRemove = new ArrayList();
            foreach (String notificationName in observerMap.Keys) {
                // the observer list for the notification under inspection
                IList observers = (IList)observerMap[notificationName];
                // First, collect the indices of the observers to be removed 
				IList observersToRemove = new ArrayList();
                for (int i = 0; i < observers.Count; i++)
                {
                    IObserver observer = (IObserver)observers[i];
                    if (observer.compareNotifyContext(retrieveMediator(mediatorName)) == true)
                    {
                        observersToRemove.Add(i);
					}
				}
                // now the removalTargets array has an ascending 
                // list of indices to be removed from the observers array
                // so pop them off the array, effectively going from 
                // highest index value to lowest, and splice each
                // from the observers array. since we're going backwards,
                // the collapsing of the array elements to fill the spliced
                // out element's space does not affect the position of the
                // lower numbered indices we've yet to remove
                int observerIndex = observersToRemove.Count;
                while (observerIndex-- > 0)
                {
                    observers.RemoveAt((int)observersToRemove[observerIndex]);
                }
                // Also, when an notification's observer list length falls to 
                // zero, delete the notification key from the observer map
                if (observers.Count == 0)
                {
                    // We can't alter the HashTable during the loop
                    // so add the key to a list of keys to be removed
                    // at the end of the loop
                    keysToRemove.Add(notificationName);
                }
            }

            int keyIndex = keysToRemove.Count;
            while (keyIndex-- > 0)
            {
                observerMap.Remove(keysToRemove[keyIndex].ToString());
            }

            // Remove the reference to the Mediator itself
            IMediator mediator = retrieveMediator(mediatorName);
            mediatorMap.Remove(mediatorName);
            return mediator;
		}
						
        /// <summary>
        /// Mapping of Mediator names to Mediator instances
        /// </summary>
		protected IDictionary mediatorMap;

        /// <summary>
        /// Mapping of Notification names to Observer lists
        /// </summary>
		protected IDictionary observerMap;
		
        /// <summary>
        /// Singleton instance
        /// </summary>
		protected static IView instance;
    }
}
