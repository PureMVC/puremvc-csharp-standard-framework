//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    /// <summary>
    /// Tests PureMVC Observer class.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Since the Observer encapsulates the interested object's
    ///         callback information, there are no getters, only setters. 
    ///         It is, in effect write-only memory.
    ///     </para>
    ///     <para>
    ///         Therefore, the only way to test it is to set the 
    ///         notification method and context and call the notifyObserver
    ///         method.
    ///     </para>
    /// </remarks>
    [TestClass]
    public class ObserverTest
    {
        /// <summary>
        /// Tests observer class when initialized by accessor methods.
        /// </summary>
        [TestMethod]
        public void TestObserverAccessor()
        {
            // Create observer with null args, then
            // use accessors to set notification method and context
            var observer = new Observer(null, null);
            observer.NotifyContext = this;
            observer.NotifyMethod = ObserverTestMethod;

            // create a test event, setting a payload value and notify 
            // the observer with it. since the observer is this class 
            // and the notification method is observerTestMethod,
            // successful notification will result in our local 
            // observerTestVar being set to the value we pass in 
            // on the note body.
            var note = new Notification("ObserverTestNote", 10);
            observer.NotifyObserver(note);

            // test assertions  
            Assert.IsTrue(ObserverTestVar == 10, "Expecting ObserverTestVar == 10");
        }

        /// <summary>
        /// Tests observer class when initialized by constructor.
        /// </summary>
        [TestMethod]
        public void TestObserverConstructor()
        {
            // Create observer passing in notification method and context
            var observer = new Observer(ObserverTestMethod, this);

            // create a test notification, setting a body value and notify 
            // the observer with it. since the observer is this class 
            // and the notification method is observerTestMethod,
            // successful notification will result in our local 
            // observerTestVar being set to the value we pass in 
            // on the notification body.
            var note = new Notification("ObserverTestNote", 5);
            observer.NotifyObserver(note);

            // test assertions  			
            Assert.IsTrue(ObserverTestVar == 5, "Expecting observerTestVar = 5");
        }

        /// <summary>
        /// Tests the compareNotifyContext method of the Observer class
        /// </summary>
        [TestMethod]
        public void TestCompareNotifyContext()
        {
            // Create observer passing in notification method and context
            var observer = new Observer(ObserverTestMethod, this);

            var negTestObj = new object();

            // test assertions  			
            Assert.IsTrue(observer.CompareNotifyContext(negTestObj) == false, "Expecting observer.compareNotifyContext(negTestObj) == false");
            Assert.IsTrue(observer.CompareNotifyContext(this), "Expecting observer.compareNotifyContext(this) == true");
            Assert.IsTrue(observer.CompareNotifyContext(new WeakReference<object>(null)) == false, "Expecting garbage collected value (null) == false");
        }

        /// <summary>
  		/// A test variable that proves the notify method was
        /// executed with 'this' as its exectution context
        /// </summary>
        private int ObserverTestVar;

        /// <summary>
        /// A function that is used as the observer notification
        /// method. It multiplies the input number by the 
        /// observerTestVar value
        /// </summary>
        /// <param name="note">notification</param>
        private void ObserverTestMethod(INotification note)
        {
            ObserverTestVar = (int)note.Body;
        }
    }
}
