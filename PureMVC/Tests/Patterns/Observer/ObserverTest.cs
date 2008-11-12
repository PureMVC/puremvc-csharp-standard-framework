/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
  	 * Tests PureMVC Observer class.
  	 * 
  	 * <P>
  	 * Since the Observer encapsulates the interested object's
  	 * callback information, there are no getters, only setters. 
  	 * It is, in effect write-only memory.</P>
  	 * 
  	 * <P>
  	 * Therefore, the only way to test it is to set the 
  	 * notification method and context and call the notifyObserver
  	 * method.</P>
  	 * 
  	 */
    [TestClass]
    public class ObserverTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public ObserverTest() 
        { }

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

        /**
  		 * Tests observer class when initialized by accessor methods.
  		 * 
  		 */
		[TestMethod]
		[Description("Observer Tests")]
		public void ObserverAccessors()
        {
   			// Create observer with null args, then
   			// use accessors to set notification method and context
			IObserver observer = new Observer(null, null);
    		observer.NotifyContext = this;
   			observer.NotifyMethod = "observerTestMethod";
  			
   			// create a test event, setting a payload value and notify 
   			// the observer with it. since the observer is this class 
   			// and the notification method is observerTestMethod,
   			// successful notification will result in our local 
   			// observerTestVar being set to the value we pass in 
   			// on the note body.
   			INotification note = new Notification("ObserverTestNote", 10);
			observer.NotifyObserver(note);

			// test assertions  			
   			Assert.IsTrue(observerTestVar == 10, "Expecting observerTestVar = 10");
   		}

  		/**
  		 * Tests observer class when initialized by constructor.
  		 * 
 		 */
		[TestMethod]
		[Description("Observer Tests")]
		public void ObserverConstructor()
        {
   			// Create observer passing in notification method and context
			IObserver observer = new Observer("observerTestMethod", this);
  			
   			// create a test note, setting a body value and notify 
   			// the observer with it. since the observer is this class 
   			// and the notification method is observerTestMethod,
   			// successful notification will result in our local 
   			// observerTestVar being set to the value we pass in 
   			// on the note body.
   			INotification note = new Notification("ObserverTestNote", 5);
			observer.NotifyObserver(note);

			// test assertions  			
   			Assert.IsTrue(observerTestVar == 5, "Expecting observerTestVar = 5");
   		}

  		/**
  		 * Tests the compareNotifyContext method of the Observer class
  		 * 
 		 */
		[TestMethod]
		[Description("Observer Tests")]
		public void CompareNotifyContext()
        {
  			
   			// Create observer passing in notification method and context
			IObserver observer = new Observer("observerTestMethod", this);
  			
  			object negTestObj = new object();
  			
			// test assertions  			
   			Assert.IsTrue(observer.CompareNotifyContext(negTestObj) == false, "Expecting observer.compareNotifyContext(negTestObj) == false");
            Assert.IsTrue(observer.CompareNotifyContext(this) == true, "Expecting observer.compareNotifyContext(this) == true");
   		}

  		/**
  		 * A test variable that proves the notify method was
  		 * executed with 'this' as its exectution context
  		 */
  		private int observerTestVar;

  		/**
  		 * A function that is used as the observer notification
  		 * method. It multiplies the input number by the 
  		 * observerTestVar value
  		 */
  		public void observerTestMethod(INotification note)
  		{
			observerTestVar = (int) note.Body;
  		}
    }
}
