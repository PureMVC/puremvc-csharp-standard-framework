using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core.view
{
    /**
	 * A Singleton <code>IView</code> implementation.
	 * 
	 * <P>
	 * In PureMVC, the <code>View</code> class assumes these responsibilities:
	 * <UL>
	 * <LI>Maintain a cache of <code>IMediator</code> instances.</LI>
	 * <LI>Provide methods for registering, retrieving, and removing <code>IMediators</code>.</LI>
	 * <LI>Managing the observer lists for each <code>INotification</code> in the application.</LI>
	 * <LI>Providing a method for attaching <code>IObservers</code> to an <code>INotification</code>'s observer list.</LI>
	 * <LI>Providing a method for broadcasting an <code>INotification</code>.</LI>
	 * <LI>Notifying the <code>IObservers</code> of a given <code>INotification</code> when it broadcast.</LI>
	 * </UL>
	 * 
	 * @see org.puremvc.patterns.mediator.Mediator Mediator
	 * @see org.puremvc.patterns.observer.Observer Observer
	 * @see org.puremvc.patterns.observer.Notification Notification
	 */
    public class View : IView
    {
        /**
		 * Constructor. 
		 * 
		 * <P>
		 * This <code>IView</code> implementation is a Singleton, 
		 * so you should not call the constructor 
		 * directly, but instead call the static Singleton 
		 * Factory method <code>View.getInstance()</code>
		 * 
		 * @throws Error Error if Singleton instance has already been constructed
		 * 
		 */
		private View()
		{
            mediatorMap = new Hashtable();
            observerMap = new Hashtable();
            initializeView();
		}
		
		/**
		 * Initialize the Singleton View instance.
		 * 
		 * <P>
		 * Called automatically by the constructor, this
		 * is your opportunity to initialize the Singleton
		 * instance in your subclass without overriding the
		 * constructor.</P>
		 * 
		 * @return void
		 */
		protected void initializeView(  )
		{ }
	
		/**
		 * View Singleton Factory method.
		 * 
		 * @return the Singleton instance of <code>View</code>
		 */
		public static IView getInstance() 
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

            internal static readonly IView instance = new View();
        }

		/**
		 * Register an <code>IObserver</code> to be notified
		 * of <code>INotifications</code> with a given name.
		 * 
		 * @param notificationName the name of the <code>INotifications</code> to notify this <code>IObserver</code> of
		 * @param observer the <code>IObserver</code> to register
		 */
		public void registerObserver ( String notificationName, IObserver observer )
		{
			if(!observerMap.Contains(notificationName)) 
            {
                observerMap[notificationName] = new ArrayList();
			}
            ((IList)observerMap[notificationName]).Add(observer);
		}


		/**
		 * Notify the <code>IObservers</code> for a particular <code>INotification</code>.
		 * 
		 * <P>
		 * All previously attached <code>IObservers</code> for this <code>INotification</code>'s
		 * list are notified and are passed a reference to the <code>INotification</code> in 
		 * the order in which they were registered.</P>
		 * 
		 * @param notification the <code>INotification</code> to notify <code>IObservers</code> of.
		 */
		public void notifyObservers( INotification notification )
		{
            if(observerMap.Contains(notification.getName())) 
            {
                IList observers = (IList)observerMap[notification.getName()];
                for (int i = 0; i < observers.Count; i++ )
                {
                    IObserver observer = (IObserver)observers[i];
                    observer.notifyObserver(notification);
                }
            }
		}
						
		/**
		 * Register an <code>IMediator</code> instance with the <code>View</code>.
		 * 
		 * <P>
		 * Registers the <code>IMediator</code> so that it can be retrieved by name,
		 * and further interrogates the <code>IMediator</code> for its 
		 * <code>INotification</code> interests.</P>
		 * <P>
		 * If the <code>IMediator</code> returns any <code>INotification</code> 
		 * names to be notified about, an <code>Observer</code> is created encapsulating 
		 * the <code>IMediator</code> instance's <code>handleNotification</code> method 
		 * and registering it as an <code>Observer</code> for all <code>INotifications</code> the 
		 * <code>IMediator</code> is interested in.</p>
		 * 
		 * @param mediatorName the name to associate with this <code>IMediator</code> instance
		 * @param mediator a reference to the <code>IMediator</code> instance
		 */
		public void registerMediator( IMediator mediator )
		{
            // Register the Mediator for retrieval by name
            mediatorMap[mediator.getMediatorName()] = mediator;
			
            // Get Notification interests, if any.
            IList interests = mediator.listNotificationInterests();
            if ( interests.Count == 0) return;
			
            // Create Observer
            IObserver observer = new Observer("handleNotification", mediator);
			
            // Register Mediator as Observer for its list of Notification interests
            for ( int i = 0;  i < interests.Count; i++ ) 
            {
                registerObserver( interests[i].ToString(),  observer );
            }
		}

		/**
		 * Retrieve an <code>IMediator</code> from the <code>View</code>.
		 * 
		 * @param mediatorName the name of the <code>IMediator</code> instance to retrieve.
		 * @return the <code>IMediator</code> instance previously registered with the given <code>mediatorName</code>.
		 */
		public IMediator retrieveMediator( String mediatorName )
		{
			return (IMediator)mediatorMap[mediatorName];
		}

		/**
		 * Remove an <code>IMediator</code> from the <code>View</code>.
		 * 
		 * @param mediatorName name of the <code>IMediator</code> instance to be removed.
		 */
		public void removeMediator( String mediatorName )
		{
            // Remove all Observers with a reference to this Mediator			
            // also, when an notification's observer list length falls to 
            // zero, remove it.
            ArrayList keysToRemove = new ArrayList();
            foreach (String notificationName in observerMap.Keys) {
                IList observers = (IList)observerMap[ notificationName ];
                for ( int i = 0;  i < observers.Count; i++ ) 
                {
                    IObserver observer = (IObserver)observers[i];
                    if (observer.compareNotifyContext(retrieveMediator(mediatorName)) == true)
                    {
                        observers.Remove(observer);
                        if (observers.Count == 0) 
                        {
                            // We can't alter the HashTable during the loop
                            // so add the key to a list of keys to be removed
                            // at the end of the loop
                            keysToRemove.Add(notificationName);							
                            break;
                        }
						
                    }
                }
            }

            for (int i = 0; i < keysToRemove.Count; i++)
            {
                observerMap.Remove(keysToRemove[i].ToString());
            }

            // Remove the reference to the Mediator itself
            mediatorMap.Remove(mediatorName);
		}
						
		// Mapping of Mediator names to Mediator instances
		protected IDictionary mediatorMap;

		// Mapping of Notification names to Observer lists
		protected IDictionary observerMap;
    }
}
