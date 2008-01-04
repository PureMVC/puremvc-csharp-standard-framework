using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core.view
{
    /**
	 * Test the PureMVC View class.
	 */
    [TestFixture]
    public class ViewTest : TestCase
    {
        public String lastNotification;	
  		
 		public const String NOTE1 = "Notification1";
		public const String NOTE2 = "Notification2";
		public const String NOTE3 = "Notification3";

        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ViewTest(String methodName)
            : base(methodName)
        { }

        /**
		 * Create the TestSuite.
		 */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(ViewTest));

                ts.AddTest(new ViewTest("testGetInstance"));
                ts.AddTest(new ViewTest("testRegisterAndNotifyObserver"));
                ts.AddTest(new ViewTest("testRegisterAndRetrieveMediator"));
                ts.AddTest(new ViewTest("testRegisterAndRemoveMediator"));
                ts.AddTest(new ViewTest("testSuccessiveRegisterAndRemoveMediator"));
                ts.AddTest(new ViewTest("testRemoveMediatorAndSubsequentNotify"));

                return ts;
            }
        }

        /**
  		 * Tests the View Singleton Factory Method 
  		 */
  		public void testGetInstance()
        {
   			// Test Factory Method
   			IView view = View.getInstance();
   			
   			// test assertions
            Assert.NotNull(view, "Expecting instance not null");
   			Assert.True(view is IView, "Expecting instance implements IView");
   			
   		}

  		/**
  		 * Tests registration and notification of Observers.
  		 * 
  		 * <P>
  		 * An Observer is created to callback the viewTestMethod of
  		 * this ViewTest instance. This Observer is registered with
  		 * the View to be notified of 'ViewTestEvent' events. Such
  		 * an event is created, and a value set on its payload. Then
  		 * the View is told to notify interested observers of this
  		 * Event. 
  		 * 
  		 * <P>
  		 * The View calls the Observer's notifyObserver method
  		 * which calls the viewTestMethod on this instance
  		 * of the ViewTest class. The viewTestMethod method will set 
  		 * an instance variable to the value passed in on the Event
  		 * payload. We evaluate the instance variable to be sure
  		 * it is the same as that passed out as the payload of the 
  		 * original 'ViewTestEvent'.
  		 * 
 		 */
  		public void testRegisterAndNotifyObserver()
        {
  			// Get the Singleton View instance
  			IView view = View.getInstance();
  			
   			// Create observer, passing in notification method and context
   			IObserver observer = new Observer("viewTestMethod", this);
   			
   			// Register Observer's interest in a particulat Notification with the View 
   			view.registerObserver(ViewTestNote.NAME, observer);
  			
   			// Create a ViewTestNote, setting 
   			// a body value, and tell the View to notify 
   			// Observers. Since the Observer is this class 
   			// and the notification method is viewTestMethod,
   			// successful notification will result in our local 
   			// viewTestVar being set to the value we pass in 
   			// on the note body.
   			INotification note = ViewTestNote.create(10);
			view.notifyObservers(note);

			// test assertions  			
   			Assert.True(viewTestVar == 10, "Expecting viewTestVar = 10");
   		}
   		
  		/**
  		 * A test variable that proves the viewTestMethod was
  		 * invoked by the View.
  		 */
  		private int viewTestVar;

  		/**
  		 * A utility method to test the notification of Observers by the view
  		 */
  		public void viewTestMethod(INotification note)
  		{
  			// set the local viewTestVar to the number on the event payload
  			viewTestVar = (int)note.getBody();
  		}

		/**
		 * Tests registering and retrieving a mediator with
		 * the View.
		 */
		public void testRegisterAndRetrieveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.getInstance();

			// Create and register the test mediator
			IMediator viewTestMediator = new ViewTestMediator(this);
			view.registerMediator(viewTestMediator);

			// Retrieve the component
			IMediator mediator = view.retrieveMediator(ViewTestMediator.NAME) as IMediator;
			
			// test assertions  			
   			Assert.True(mediator is ViewTestMediator, "Expecting comp is ViewTestMediator");
   			
   			cleanup();
		}
 		
		/**
		 * Tests registering and removing a mediator 
		 */
		public void testRegisterAndRemoveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.getInstance();

			// Create and register the test mediator, 
			// but not so we have a reference to it
			view.registerMediator(new ViewTestMediator(this));
			
			// Remove the component
			view.removeMediator(ViewTestMediator.NAME);
			
			// test assertions  			
   			Assert.Null(view.retrieveMediator(ViewTestMediator.NAME), "Expecting view.retrieveMediator(ViewTestMediator.NAME) == null");			  			
		}

		/**
		 * Tests successive regster and remove of same mediator.
		 */
		public void testSuccessiveRegisterAndRemoveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.getInstance();

			// Create and register the test mediator, 
			// but not so we have a reference to it
			view.registerMediator(new ViewTestMediator(this));
			
			// test that we can retrieve it
            Assert.True(view.retrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.retrieveMediator( ViewTestMediator.NAME ) is ViewTestMediator"); 

			// Remove the Mediator
			view.removeMediator(ViewTestMediator.NAME);

			// test that retrieving it now returns null			
   			Assert.Null(view.retrieveMediator(ViewTestMediator.NAME), "Expecting view.retrieveMediator( ViewTestMediator.NAME ) == null");

			// test that removing the mediator again once its gone doesn't cause crash 	
            try
            {
                view.removeMediator(ViewTestMediator.NAME);
            }
            catch
            {
                Assert.Fail("Expecting view.removeMediator( ViewTestMediator.NAME ) doesn't crash", null);
            }

			// Create and register another instance of the test mediator, 
			view.registerMediator(new ViewTestMediator(this));
			
   			Assert.True(view.retrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.retrieveMediator( ViewTestMediator.NAME ) is ViewTestMediator"); 

			// Remove the Mediator
			view.removeMediator(ViewTestMediator.NAME);
			
			// test that retrieving it now returns null			
   			Assert.Null(view.retrieveMediator(ViewTestMediator.NAME), "Expecting view.retrieveMediator( ViewTestMediator.NAME ) == null");

			cleanup();						  			
		}
		
        /**
		 * Tests registering a Mediator for 3 different notifications, removing the
		 * Mediator from the View, and seeing that neither notification causes the
		 * Mediator to be notified. Added for the fix deployed in version 1.7
		 */
		public void testRemoveMediatorAndSubsequentNotify()
        {			
  			// Get the Singleton View instance
  			IView view = View.getInstance();
			
			// Create and register the test mediator to be removed.
			view.registerMediator(new ViewTestMediator2(this));
			
			// Create and register the Mediator to remain
			view.registerMediator(new ViewTestMediator(this));
			
			// test that notifications work
   			view.notifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.notifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");

            view.notifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");
		   			
			// Remove the Mediator
			view.removeMediator(ViewTestMediator2.NAME);

			// test that retrieving it now returns null			
   			Assert.Null(view.retrieveMediator(ViewTestMediator2.NAME), "Expecting view.retrieveMediator(ViewTestMediator2.NAME) == null");

			// test that notifications no longer work
			// (ViewTestMediator2 is the one that sets lastNotification
			// on this component, and ViewTestMediator)
			lastNotification = null;

            view.notifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.notifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");

            view.notifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification != NOTE3, "Expecting lastNotification != NOTE3");

			cleanup();						  			
		}

		private void cleanup()
		{
			 View.getInstance().removeMediator(ViewTestMediator.NAME);
		}
    }
}
