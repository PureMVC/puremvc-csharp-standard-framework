using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.observer
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
    [TestFixture]
    public class ObserverTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ObserverTest(String methodName) 
            : base(methodName)
        { }

        /**
         * Create the TestSuite.
         */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(ObserverTest));

                ts.AddTest(new ObserverTest("testObserverAccessors"));
                ts.AddTest(new ObserverTest("testObserverConstructor"));
                ts.AddTest(new ObserverTest("testCompareNotifyContext"));

                return ts;
            }
        }

        /**
  		 * Tests observer class when initialized by accessor methods.
  		 * 
  		 */
  		public void testObserverAccessors()
        {
   			// Create observer with null args, then
   			// use accessors to set notification method and context
    		IObserver observer = new Observer(null, null);
    		observer.setNotifyContext(this);
   			observer.setNotifyMethod("observerTestMethod");
  			
   			// create a test event, setting a payload value and notify 
   			// the observer with it. since the observer is this class 
   			// and the notification method is observerTestMethod,
   			// successful notification will result in our local 
   			// observerTestVar being set to the value we pass in 
   			// on the note body.
   			INotification note = new Notification("ObserverTestNote", 10);
			observer.notifyObserver(note);

			// test assertions  			
   			Assert.True(observerTestVar == 10, "Expecting observerTestVar = 10");
   		}

  		/**
  		 * Tests observer class when initialized by constructor.
  		 * 
 		 */
  		public void testObserverConstructor()
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
			observer.notifyObserver(note);

			// test assertions  			
   			Assert.True(observerTestVar == 5, "Expecting observerTestVar = 5");
   		}

  		/**
  		 * Tests the compareNotifyContext method of the Observer class
  		 * 
 		 */
  		public void testCompareNotifyContext()
        {
  			
   			// Create observer passing in notification method and context
   			IObserver observer = new Observer("observerTestMethod", this);
  			
  			Object negTestObj = new Object();
  			
			// test assertions  			
   			Assert.True(observer.compareNotifyContext(negTestObj) == false, "Expecting observer.compareNotifyContext(negTestObj) == false");
            Assert.True(observer.compareNotifyContext(this) == true, "Expecting observer.compareNotifyContext(this) == true");
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
  			observerTestVar = (int)note.getBody();
  		}
    }
}
