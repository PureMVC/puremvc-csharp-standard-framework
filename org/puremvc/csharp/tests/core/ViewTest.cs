/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.core
{
    /**
	 * Test the PureMVC View class.
	 */
    [TestFixture]
    public class ViewTest : TestCase
    {
        public String lastNotification;	
  		public Boolean onRegisterCalled = false;
  		public Boolean onRemoveCalled = false;
  		public Int32 counter = 0;
  		
 		public const String NOTE1 = "Notification1";
		public const String NOTE2 = "Notification2";
		public const String NOTE3 = "Notification3";
		public const String NOTE4 = "Notification4";
		public const String NOTE5 = "Notification5";
		public const String NOTE6 = "Notification6";

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
				ts.AddTest(new ViewTest("testHasMediator"));
				ts.AddTest(new ViewTest("testRegisterAndRemoveMediator"));
				ts.AddTest(new ViewTest("testOnRegisterAndOnRemove"));
				ts.AddTest(new ViewTest("testSuccessiveRegisterAndRemoveMediator"));
				ts.AddTest(new ViewTest("testRemoveMediatorAndSubsequentNotify"));
				ts.AddTest(new ViewTest("testRemoveOneOfTwoMediatorsAndSubsequentNotify"));
				ts.AddTest(new ViewTest("testMediatorReregistration"));
				ts.AddTest(new ViewTest("testModifyObserverListDuringNotification"));

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
  		 * Tests the hasMediator Method
  		 */
  		public void testHasMediator() {
  			
   			// register a Mediator
   			IView view = View.getInstance();
			
			// Create and register the test mediator
			Mediator mediator = new Mediator("hasMediatorTest", this);
			view.registerMediator(mediator);
			
   			// assert that the view.hasMediator method returns true
   			// for that mediator name
   			Assert.True(view.hasMediator("hasMediatorTest") == true, "Expecting view.hasMediator('hasMediatorTest') == true");

			view.removeMediator("hasMediatorTest");
			
   			// assert that the view.hasMediator method returns false
   			// for that mediator name
   			Assert.True(view.hasMediator("hasMediatorTest") == false, "Expecting view.hasMediator('hasMediatorTest') == false");
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
			view.registerMediator(new Mediator("testing", this));
			
			// Remove the component
            IMediator removedMediator = view.removeMediator("testing");
			
			// test assertions  		
            Assert.True(removedMediator.getMediatorName() == "testing", "Expecting removedMediator.getMediatorName() == 'testing'");
            Assert.Null(view.retrieveMediator("testing"), "Expecting view.retrieveMediator('testing') == null");

			cleanup();
		}
		
		/**
		 * Tests that the View callse the onRegister and onRemove methods
		 */
		public void testOnRegisterAndOnRemove() {
			
  			// Get the Singleton View instance
  			IView view = View.getInstance();

			// Create and register the test mediator
			IMediator mediator = new ViewTestMediator4(this);
			view.registerMediator(mediator);

			// assert that onRegsiter was called, and the mediator responded by setting our boolean
   			Assert.True(onRegisterCalled, "Expecting onRegisterCalled == true");
				
			// Remove the component
			view.removeMediator(ViewTestMediator4.NAME);
			
			// assert that the mediator is no longer retrievable
   			Assert.True(onRemoveCalled, "Expecting onRemoveCalled == true");
   						
			cleanup();
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

			cleanup();						  			
		}

        /**
		 * Tests registering one of two registered Mediators and seeing
		 * that the remaining one still responds.
		 * Added for the fix deployed in version 1.7.1
		 */
		public void testRemoveOneOfTwoMediatorsAndSubsequentNotify()
        {
  			// Get the Singleton View instance
  			IView view = View.getInstance();
			
			// Create and register that responds to notifications 1 and 2
			view.registerMediator(new ViewTestMediator2(this));
			
			// Create and register that responds to notification 3
			view.registerMediator(new ViewTestMediator3(this));
			
			// test that all notifications work
            view.notifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.notifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");

            view.notifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");
		   			
			// Remove the Mediator that responds to 1 and 2
			view.removeMediator(ViewTestMediator2.NAME);

			// test that retrieving it now returns null				
            Assert.Null(view.retrieveMediator(ViewTestMediator2.NAME), "Expecting view.retrieveMediator(ViewTestMediator2.NAME) == null");

			// test that notifications no longer work
			// for notifications 1 and 2, but still work for 3
            lastNotification = null;

            view.notifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.notifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");

            view.notifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");

			cleanup();						  			
		}
		
		/**
		 * Tests registering the same mediator twice. 
		 * A subsequent notification should only illicit
		 * one response. Also, since reregistration
		 * was causing 2 observers to be created, ensure
		 * that after removal of the mediator there will
		 * be no further response.
		 * 
		 * Added for the fix deployed in version 2.0.4
		 */
		public void testMediatorReregistration() {
			
  			// Get the Singleton View instance
  			IView view = View.getInstance();
			
			// Create and register that responds to notification 5
			view.registerMediator(new ViewTestMediator5( this ) );
			
			// try to register another instance of that mediator (uses the same NAME constant).
			view.registerMediator( new ViewTestMediator5( this ) );
			
			// test that the counter is only incremented once (mediator 5's response) 
			counter = 0;
   			view.notifyObservers( new Notification(NOTE5) );
			Assert.True(counter == 1, "Expecting counter == 1");

			// Remove the Mediator 
			view.removeMediator( ViewTestMediator5.NAME );

			// test that retrieving it now returns null			
   			Assert.True(view.retrieveMediator( ViewTestMediator5.NAME ) == null, "Expecting view.retrieveMediator( ViewTestMediator5.NAME ) == null");

			// test that the counter is no longer incremented  
			counter = 0;
   			view.notifyObservers( new Notification(NOTE5) );
   			Assert.True(counter == 0, "Expecting counter == 0");
		}
		
		
		/**
		 * Tests the ability for the observer list to 
		 * be modified during the process of notification,
		 * and all observers be properly notified. This
		 * happens most often when multiple Mediators
		 * respond to the same notification by removing
		 * themselves.  
		 * 
		 * Added for the fix deployed in version 2.0.4
		 */
		public void testModifyObserverListDuringNotification() {
			
  			// Get the Singleton View instance
  			IView view = View.getInstance();
			
			// Create and register several mediator instances that respond to notification 6 
			// by removing themselves, which will cause the observer list for that notification 
			// to change. versions prior to Standard Version 2.0.4 will see every other mediator
			// fails to be notified.  
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/1", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/2", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/3", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/4", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/5", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/6", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/7", this ) );
			view.registerMediator( new ViewTestMediator6(  "ViewTestMediator6/8", this ) );

			// clear the counter
			counter = 0;
			// send the notification. each of the above mediators will respond by removing
			// themselves and incrementing the counter by 1. This should leave us with a
			// count of 8, since 8 mediators will respond.
			view.notifyObservers( new Notification( NOTE6 ) );
			// verify the count is correct
   			Assert.True(counter == 8, "Expecting counter == 8");
	
			// clear the counter
			counter=0;
			view.notifyObservers( new Notification( NOTE6 ) );
			// verify the count is 0
   			Assert.True(counter == 0, "Expecting counter == 0");

		}

		private void cleanup()
		{
            View.getInstance().removeMediator(ViewTestMediator.NAME);
            View.getInstance().removeMediator(ViewTestMediator2.NAME);
            View.getInstance().removeMediator(ViewTestMediator3.NAME);
		}
    }
}
