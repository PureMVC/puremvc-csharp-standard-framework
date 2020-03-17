//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using PureMVC.Patterns.Mediator;

namespace PureMVC.Core
{
    /// <summary>
    /// Test the PureMVC View class.
    /// </summary>
    [TestClass]
    public class ViewTest
    {
        public string lastNotification;
        public bool onRegisterCalled;
        public bool onRemoveCalled;
        public int counter;
        public const string NOTE1 = "Notification1";
        public const string NOTE2 = "Notification2";
        public const string NOTE3 = "Notification3";
        public const string NOTE4 = "Notification4";
        public const string NOTE5 = "Notification5";
        public const string NOTE6 = "Notification6";

        /// <summary>
        /// Tests the View Singleton Factory Method 
        /// </summary>
        [TestMethod]
        public void TestGetInstance()
        {
            // Test Factory Method
            var view = View.GetInstance(() => new View());

            // test assertions
            Assert.IsNotNull(view, "Expecting instance not null");
            Assert.IsTrue(view != null, "Expecting instance implements IView");
        }

        /// <summary>
        /// Tests registration and notification of Observers.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         An Observer is created to callback the viewTestMethod of
        ///         this ViewTest instance. This Observer is registered with
        ///         the View to be notified of 'ViewTestEvent' events. Such
        ///         an event is created, and a value set on its payload. Then
        ///         the View is told to notify interested observers of this
        ///         Event. 
        ///     </para>
        ///     <para>
        ///         The View calls the Observer's notifyObserver method
        ///         which calls the viewTestMethod on this instance
        ///         of the ViewTest class. The viewTestMethod method will set 
        ///         an instance variable to the value passed in on the Event
        ///         payload. We evaluate the instance variable to be sure
        ///         it is the same as that passed out as the payload of the 
        ///         original 'ViewTestEvent'.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterAndNotifyObserver()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create observer, passing in notification method and context
            var observer = new Observer(ViewTestMethod, this);

            // Register Observer's interest in a particulat Notification with the View 
            view.RegisterObserver(ViewTestNote.NAME, observer);

            // Create a ViewTestNote, setting 
            // a body value, and tell the View to notify 
            // Observers. Since the Observer is this class 
            // and the notification method is viewTestMethod,
            // successful notification will result in our local 
            // viewTestVar being set to the value we pass in 
            // on the note body.
            var note = ViewTestNote.Create(10);
            view.NotifyObservers(note);

            // test assertions  
            Assert.IsTrue(viewTestVar == 10, "Expecting viewTestVar = 10");
        }

        // A test variable that proves the viewTestMethod was
  		// invoked by the View.
        private int viewTestVar;

        // A utility method to test the notification of Observers by the view
        private void ViewTestMethod(INotification note)
        {
            // set the local viewTestVar to the number on the event payload
            viewTestVar = (int)note.Body;
        }

        /// <summary>
        /// Tests registering and retrieving a mediator with
        /// the View.
        /// </summary>
        [TestMethod]
        public void TestRegisterAndRetrieveMediator()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator
            var viewTestMediator = new ViewTestMediator(this);
            view.RegisterMediator(viewTestMediator);

            // Retrieve the component
            var mediator = view.RetrieveMediator(ViewTestMediator.NAME);

            //  assertions  
            Assert.IsTrue(mediator is ViewTestMediator, "Expecting comp is ViewTestMediator");
        }

        /// <summary>
        /// Tests the hasMediator Method
        /// </summary>
        [TestMethod]
        public void TestHasMediator()
        {
            // // register a Mediator
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator
            IMediator mediator = new Mediator("HasMediatorTest", this);
            view.RegisterMediator(mediator);

            // assert that the view.hasMediator method returns true
            // for that mediator name
            Assert.IsTrue(view.HasMediator("HasMediatorTest") == true, "Expecting view.HasMediator('HasMediatorTest') == true");

            view.RemoveMediator("HasMediatorTest");

            // assert that the view.hasMediator method returns false
            // for that mediator name
            Assert.IsTrue(view.HasMediator("HasMediatorTest") == false, "Expecting view.HasMediator('HasMediatorTest') == false");
        }

        /// <summary>
        /// Tests registering and removing a mediator 
        /// </summary>
        [TestMethod]
        public void TestRegisterAndRemoveMediator()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator
            var mediator = new Mediator("testing", this);
            view.RegisterMediator(mediator);

            // Remove the component
            var removedMediator = view.RemoveMediator("testing");

            // assert that we have removed the appropriate mediator
            Assert.IsTrue(removedMediator.MediatorName == "testing", "Expecting removedMediator.MediatorName == 'testing'");

            // assert that the mediator is no longer retrievable
            Assert.IsNull(view.RetrieveMediator("testing"), "Expecting view.RetrieveMediator('testing') == null");
        }

        /// <summary>
        /// Tests that the View callse the onRegister and onRemove methods
        /// </summary>
        [TestMethod]
        public void TestOnRegisterAndOnRemove()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator
            var mediator = new ViewTestMediator4(this);
            view.RegisterMediator(mediator);

            // assert that onRegsiter was called, and the mediator responded by setting our boolean
            Assert.IsTrue(onRegisterCalled, "Expecting onRegisterCalled == true");

            //Remove the component
            view.RemoveMediator(ViewTestMediator4.NAME);

            // assert that the mediator is no longer retrievable
            Assert.IsTrue(onRemoveCalled, "Expecting onRemoveCalled == true");
        }

        /// <summary>
        /// Tests successive regster and remove of same mediator.
        /// </summary>
        [TestMethod]
        public void TestSuccessiveRegisterAndRemoveMediator()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator, 
            // but not so we have a reference to it
            view.RegisterMediator(new ViewTestMediator(this));

            // test that we can retrieve it
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator");

            // Remove the Mediator
            view.RemoveMediator(ViewTestMediator.NAME);

            // test that retrieving it now returns null
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator.NAME) == null, "Expecting view.RetrieveMediator(ViewTestMediator.NAME) == null");

            // test that removing the mediator again once its gone doesn't cause crash 
            Assert.IsTrue(view.RemoveMediator(ViewTestMediator.NAME) == null, "Expecting view.RemoveMediator(ViewTestMediator).NAME doesn't crash");

            // Create and register another instance of the test mediator, 
            view.RegisterMediator(new ViewTestMediator(this));

            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator, "Expecting view.RetrieveMediator(ViewTestMediator.NAME) is ViewTestMediator");

            // Remove the Mediator
            view.RemoveMediator(ViewTestMediator.NAME);

            // test that retrieving it now returns null	
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator.NAME) == null, "Expecting view.RetrieveMediator(ViewTestMediator.NAME) == null");
        }

        /// <summary>
        /// Tests registering a Mediator for 2 different notifications, removing the
        /// Mediator from the View, and seeing that neither notification causes the
        /// Mediator to be notified.
        /// </summary>
        [TestMethod]
        public void TestRemoveMediatorAndSubsequentNotify()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register the test mediator to be removed.
            view.RegisterMediator(new ViewTestMediator2(this));

            // test that notifications work
            view.NotifyObservers(new Notification(NOTE1));
            Assert.IsTrue(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.IsTrue(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");

            // Remove the Mediator
            view.RemoveMediator(ViewTestMediator2.NAME);

            // test that retrieving it now returns null	
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator2.NAME) == null, "Expecting view.RetrieveMediator(ViewTestMediator2.NAME) == null");

            // test that notifications no longer work
            // (ViewTestMediator2 is the one that sets lastNotification
            // on this component, and ViewTestMediator)
            lastNotification = null;

            view.NotifyObservers(new Notification(NOTE1));
            Assert.IsTrue(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.IsTrue(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");
        }

        /// <summary>
        /// Tests registering one of two registered Mediators and seeing
        /// that the remaining one still responds.
        /// </summary>
        [TestMethod]
        public void TestRemoveOneOfTwoMediatorsAndSubsequentNotify()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register that responds to notifications 1 and 2
            view.RegisterMediator(new ViewTestMediator2(this));

            // Create and register that responds to notification 3
            view.RegisterMediator(new ViewTestMediator3(this));

            // test that all notifications work
            view.NotifyObservers(new Notification(NOTE1));
            Assert.IsTrue(lastNotification == NOTE1, "Expecting lastNotification == NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.IsTrue(lastNotification == NOTE2, "Expecting lastNotification == NOTE2");

            view.NotifyObservers(new Notification(NOTE3));
            Assert.IsTrue(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");

            // Remove the Mediator that responds to 1 and 2
            view.RemoveMediator(ViewTestMediator2.NAME);

            // test that retrieving it now returns null	
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator2.NAME) == null, "Expecting view.RetrieveMediator(ViewTestMediator2.NAME) == null");

            // test that notifications no longer work
            // for notifications 1 and 2, but still work for 3
            lastNotification = null;

            view.NotifyObservers(new Notification(NOTE1));
            Assert.IsTrue(lastNotification != NOTE1, "Expecting lastNotification != NOTE1");

            view.NotifyObservers(new Notification(NOTE2));
            Assert.IsTrue(lastNotification != NOTE2, "Expecting lastNotification != NOTE2");

            view.NotifyObservers(new Notification(NOTE3));
            Assert.IsTrue(lastNotification == NOTE3, "Expecting lastNotification == NOTE3");
        }

        /// <summary>
        /// Tests registering the same mediator twice. 
        /// A subsequent notification should only illicit
        /// one response. Also, since reregistration
        /// was causing 2 observers to be created, ensure
        /// that after removal of the mediator there will
        /// be no further response.
        /// </summary>
        [TestMethod]
        public void TestMediatorRegistration()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register that responds to notification 5
            view.RegisterMediator(new ViewTestMediator5(this));

            // try to register another instance of that mediator (uses the same NAME constant).
            view.RegisterMediator(new ViewTestMediator5(this));

            // test that the counter is only incremented once (mediator 5's response) 
            counter = 0;
            view.NotifyObservers(new Notification(NOTE5));
            Assert.AreEqual(1, counter, "Expecting counter == 1");

            // Remove the Mediator 
            view.RemoveMediator(ViewTestMediator5.NAME);

            // test that retrieving it now returns null
            Assert.IsTrue(view.RetrieveMediator(ViewTestMediator5.NAME) == null, "Expecting view.RetrieveMediator(ViewTestMediator5.NAME) == null");

            // test that the counter is no longer incremented  
            counter = 0;
            view.NotifyObservers(new Notification(NOTE5));
            Assert.AreEqual(0, counter, "Expecting counter == 0");
        }

        /// <summary>
        /// Tests the ability for the observer list to 
        /// be modified during the process of notification,
        /// and all observers be properly notified. This
        /// happens most often when multiple Mediators
        /// respond to the same notification by removing
        /// themselves.
        /// </summary>
        [TestMethod]
        public void TestModifyObserverListDuringNotification()
        {
            // Get the Singleton View instance
            var view = View.GetInstance(() => new View());

            // Create and register several mediator instances that respond to notification 6 
            // by removing themselves, which will cause the observer list for that notification 
            // to change.
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/1", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/2", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/3", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/4", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/5", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/6", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/7", this));
            view.RegisterMediator(new ViewTestMediator6(ViewTestMediator6.NAME + "/8", this));

            // clear the counter
            counter = 0;
            // send the notification. each of the above mediators will respond by removing
            // themselves and incrementing the counter by 1. This should leave us with a
            // count of 8, since 8 mediators will respond.

            view.NotifyObservers(new Notification(NOTE6));
            // verify the count is correct
            Assert.AreEqual(8, counter, "Expecting counter == 8");

            // clear the counter
            counter = 0;
            view.NotifyObservers(new Notification(NOTE6));
            // verify the count is 0
            Assert.AreEqual(0, counter, "Expecting counter ==0");
        }
    }
}
