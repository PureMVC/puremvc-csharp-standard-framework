/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite;
using NUnit.Framework;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Core;

namespace PureMVC.Tests.Core
{
    /**
	 * Test the PureMVC View class.
	 */
    [TestFixture]
    public class ViewTest : TestCase
    {
        public string lastNotification;	
  		public bool onRegisterCalled = false;
  		public bool onRemoveCalled = false;
  		public Int32 counter = 0;
  		
 		public const string NOTE1 = "Notification1";
		public const string NOTE2 = "Notification2";
		public const string NOTE3 = "Notification3";
		public const string NOTE4 = "Notification4";
		public const string NOTE5 = "Notification5";
		public const string NOTE6 = "Notification6";

        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ViewTest(string methodName)
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

				ts.AddTest(new ViewTest("TestGetInstance"));
				ts.AddTest(new ViewTest("TestRegisterAndNotifyObserver"));
				ts.AddTest(new ViewTest("TestRegisterAndRetrieveMediator"));
				ts.AddTest(new ViewTest("TestHasMediator"));
				ts.AddTest(new ViewTest("TestRegisterAndRemoveMediator"));
				ts.AddTest(new ViewTest("TestOnRegisterAndOnRemove"));
				ts.AddTest(new ViewTest("TestSuccessiveRegisterAndRemoveMediator"));
				ts.AddTest(new ViewTest("TestRemoveMediatorAndSubsequentNotify"));
				ts.AddTest(new ViewTest("TestRemoveOneOfTwoMediatorsAndSubsequentNotify"));
				ts.AddTest(new ViewTest("TestMediatorReregistration"));
				ts.AddTest(new ViewTest("TestModifyObserverListDuringNotification"));

                return ts;
            }
        }

        /**
  		 * Tests the View Singleton Factory Method 
  		 */
  		public void TestGetInstance()
        {
   			// Test Factory Method
   			IView view = View.Instance;
   			
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
  		public void TestRegisterAndNotifyObserver()
        {
  			// Get the Singleton View instance
  			IView view = View.Instance;
  			
   			// Create observer, passing in notification method and context
   			IObserver observer = new Observer("viewTestMethod", this);
   			
   			// Register Observer's interest in a particulat Notification with the View 
   			view.RegisterObserver(ViewTestNote.NAME, observer);
  			
   			// Create a ViewTestNote, setting 
   			// a body value, and tell the View to notify 
   			// Observers. Since the Observer is this class 
   			// and the notification method is viewTestMethod,
   			// successful notification will result in our local 
   			// viewTestVar being set to the value we pass in 
   			// on the note body.
   			INotification note = ViewTestNote.Create(10);
			view.NotifyObservers(note);

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
  		public void ViewTestMethod(INotification note)
  		{
  			// set the local viewTestVar to the number on the event payload
			viewTestVar = (int) note.Body;
  		}

		/**
		 * Tests registering and retrieving a mediator with
		 * the View.
		 */
		public void TestRegisterAndRetrieveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.Instance;

			// Create and register the test mediator
			IMediator viewTestMediator = new ViewTestMediator(this);
			view.RegisterMediator(viewTestMediator);

			// Retrieve the component
			IMediator mediator = view.RetrieveMediator(ViewTestMediator.NAME);
			
			// test assertions  			
   			Assert.True(mediator is ViewTestMediator, "Expecting comp is ViewTestMediator");
   			
   			Cleanup();
		}
 		
  		/**
  		 * Tests the hasMediator Method
  		 */
  		public void TestHasMediator() {
  			
   			// register a Mediator
   			IView view = View.Instance;
			
			// Create and register the test mediator
			Mediator mediator = new Mediator("hasMediatorTest", this);
			view.RegisterMediator(mediator);
			
   			// assert that the view.hasMediator method returns true
   			// for that mediator name
   			Assert.True(view.HasMediator("hasMediatorTest") == true, "Expecting view.hasMediator('hasMediatorTest') == true");

			view.RemoveMediator("hasMediatorTest");
			
   			// assert that the view.hasMediator method returns false
   			// for that mediator name
   			Assert.True(view.HasMediator("hasMediatorTest") == false, "Expecting view.hasMediator('hasMediatorTest') == false");
   		}

		/**
		 * Tests registering and removing a mediator 
		 */
		public void TestRegisterAndRemoveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.Instance;

			// Create and register the test mediator, 
			// but not so we have a reference to it
			view.RegisterMediator(new Mediator("Testing", this));
			
			// Remove the component
            IMediator removedMediator = view.RemoveMediator("Testing");
			
			// test assertions  		
            Assert.True(removedMediator.MediatorName == "Testing", "Expecting removedMediator.MediatorName == 'testing'");
            Assert.Null(view.RetrieveMediator("Testing"), "Expecting view.retrieveMediator('testing') == null");

			Cleanup();
		}
		
		/**
		 * Tests that the View callse the onRegister and onRemove methods
		 */
		public void TestOnRegisterAndOnRemove() {
			
  			// Get the Singleton View instance
  			IView view = View.Instance;

			// Create and register the test mediator
			IMediator mediator = new ViewTestMediator4(this);
			view.RegisterMediator(mediator);

			// assert that onRegsiter was called, and the mediator responded by setting our boolean
   			Assert.True(onRegisterCalled, "Expecting onRegisterCalled == true");
				
			// Remove the component
			view.RemoveMediator(ViewTestMediator4.NAME);
			
			// assert that the mediator is no longer retrievable
   			Assert.True(onRemoveCalled, "Expecting onRemoveCalled == true");
   						
			Cleanup();
		}

		/**
		 * Tests successive regster and remove of same mediator.
		 */
		public void TestSuccessiveRegisterAndRemoveMediator()
        {
  			// Get the Singleton View instance
  			IView view = View.Instance;

			// Create and register the test mediator, 
			// but not so we have a reference to it
			view.RegisterMediator(new ViewTestMediator(this));
			
			// test that we can retrieve it
            Assert.True(view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.retrieveMediator( ViewTestMediator.NAME ) is ViewTestMediator"); 

			// Remove the Mediator
			view.RemoveMediator(ViewTestMediator.NAME);

			// test that retrieving it now returns null			
   			Assert.Null(view.RetrieveMediator(ViewTestMediator.NAME), "Expecting view.retrieveMediator( ViewTestMediator.NAME ) == null");

			// test that removing the mediator again once its gone doesn't cause crash 	
            try
            {
                view.RemoveMediator(ViewTestMediator.NAME);
            }
            catch
            {
                Assert.Fail("Expecting view.removeMediator( ViewTestMediator.NAME ) doesn't crash", null);
            }

			// Create and register another instance of the test mediator, 
			view.RegisterMediator(new ViewTestMediator(this));
			
   			Assert.True(view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.retrieveMediator( ViewTestMediator.NAME ) is ViewTestMediator"); 

			// Remove the Mediator
			view.RemoveMediator(ViewTestMediator.NAME);
			
			// test that retrieving it now returns null			
   			Assert.Null(view.RetrieveMediator(ViewTestMediator.NAME), "Expecting view.retrieveMediator( ViewTestMediator.NAME ) == null");

			Cleanup();						  			
		}
		
        /**
		 * Tests registering a Mediator for 3 different notifications, removing the
		 * Mediator from the View, and seeing that neither notification causes the
		 * Mediator to be notified. Added for the fix deployed in version 1.7
		 */
		public void TestRemoveMediatorAndSubsequentNotify()
        {			
  			// Get the Singleton View instance
  			IView view = View.Instance;
			
			// Create and register the test mediator to be removed.
			view.RegisterMediator(new ViewTestMediator2(this));
			
			// Create and register the Mediator to remain
			view.RegisterMediator(new ViewTestMediator(this));
			
			// test that notifications work
   			view.NotifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");
		   			
			// Remove the Mediator
			view.RemoveMediator(ViewTestMediator2.NAME);

			// test that retrieving it now returns null			
   			Assert.Null(view.RetrieveMediator(ViewTestMediator2.NAME), "Expecting view.retrieveMediator(ViewTestMediator2.NAME) == null");

			// test that notifications no longer work
			// (ViewTestMediator2 is the one that sets lastNotification
			// on this component, and ViewTestMediator)
			lastNotification = null;

            view.NotifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");

			Cleanup();						  			
		}

        /**
		 * Tests registering one of two registered Mediators and seeing
		 * that the remaining one still responds.
		 * Added for the fix deployed in version 1.7.1
		 */
		public void TestRemoveOneOfTwoMediatorsAndSubsequentNotify()
        {
  			// Get the Singleton View instance
  			IView view = View.Instance;
			
			// Create and register that responds to notifications 1 and 2
			view.RegisterMediator(new ViewTestMediator2(this));
			
			// Create and register that responds to notification 3
			view.RegisterMediator(new ViewTestMediator3(this));
			
			// test that all notifications work
            view.NotifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");

            view.NotifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");
		   			
			// Remove the Mediator that responds to 1 and 2
			view.RemoveMediator(ViewTestMediator2.NAME);

			// test that retrieving it now returns null				
            Assert.Null(view.RetrieveMediator(ViewTestMediator2.NAME), "Expecting view.retrieveMediator(ViewTestMediator2.NAME) == null");

			// test that notifications no longer work
			// for notifications 1 and 2, but still work for 3
            lastNotification = null;

            view.NotifyObservers(new Notification(NOTE1));
            Assert.True(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.True(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");

            view.NotifyObservers(new Notification(NOTE3));
            Assert.True(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");

			Cleanup();						  			
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
		public void TestMediatorReregistration() {
			
  			// Get the Singleton View instance
  			IView view = View.Instance;
			
			// Create and register that responds to notification 5
			view.RegisterMediator(new ViewTestMediator5( this ) );
			
			// try to register another instance of that mediator (uses the same NAME constant).
			view.RegisterMediator( new ViewTestMediator5( this ) );
			
			// test that the counter is only incremented once (mediator 5's response) 
			counter = 0;
   			view.NotifyObservers( new Notification(NOTE5) );
			Assert.True(counter == 1, "Expecting counter == 1");

			// Remove the Mediator 
			view.RemoveMediator( ViewTestMediator5.NAME );

			// test that retrieving it now returns null			
   			Assert.True(view.RetrieveMediator( ViewTestMediator5.NAME ) == null, "Expecting view.retrieveMediator( ViewTestMediator5.NAME ) == null");

			// test that the counter is no longer incremented  
			counter = 0;
   			view.NotifyObservers( new Notification(NOTE5) );
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
		public void TestModifyObserverListDuringNotification() {
			
  			// Get the Singleton View instance
  			IView view = View.Instance;
			
			// Create and register several mediator instances that respond to notification 6 
			// by removing themselves, which will cause the observer list for that notification 
			// to change. versions prior to Standard Version 2.0.4 will see every other mediator
			// fails to be notified.  
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/1", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/2", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/3", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/4", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/5", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/6", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/7", this ) );
			view.RegisterMediator( new ViewTestMediator6(  "ViewTestMediator6/8", this ) );

			// clear the counter
			counter = 0;
			// send the notification. each of the above mediators will respond by removing
			// themselves and incrementing the counter by 1. This should leave us with a
			// count of 8, since 8 mediators will respond.
			view.NotifyObservers( new Notification( NOTE6 ) );
			// verify the count is correct
   			Assert.True(counter == 8, "Expecting counter == 8");
	
			// clear the counter
			counter=0;
			view.NotifyObservers( new Notification( NOTE6 ) );
			// verify the count is 0
   			Assert.True(counter == 0, "Expecting counter == 0");

		}

		private void Cleanup()
		{
            View.Instance.RemoveMediator(ViewTestMediator.NAME);
            View.Instance.RemoveMediator(ViewTestMediator2.NAME);
            View.Instance.RemoveMediator(ViewTestMediator3.NAME);
		}
    }
}
