using System;
using System.Collections;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.core.model;
using org.puremvc.csharp.patterns.observer;
using org.puremvc.csharp.patterns.proxy;

namespace org.puremvc.csharp.patterns.facade
{
    /**
	 * Test the PureMVC Facade class.
	 *
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestVO FacadeTestVO
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestCommand FacadeTestCommand
	 */
    [TestFixture]
    public class FacadeTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public FacadeTest(String methodName) 
            : base(methodName)
        { }

        /**
         * Create the TestSuite.
         */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(FacadeTest));

                ts.AddTest(new FacadeTest("testGetInstance"));
                ts.AddTest(new FacadeTest("testRegisterCommandAndNotifyObservers"));
                ts.AddTest(new FacadeTest("testRegisterAndRetreiveProxy"));
                ts.AddTest(new FacadeTest("testRegisterAndRemoveProxy"));

                return ts;
            }
        }

        /**
  		 * Tests the Facade Singleton Factory Method 
  		 */
  		public void testGetInstance()
        {
   			// Test Factory Method
   			IFacade facade = Facade.getInstance();
   			
   			// test assertions
            Assert.NotNull(facade, "Expecting instance not null");
   			Assert.True(facade is IFacade, "Expecting instance implements IFacade");
   		}

        /**
  		 * Tests Command registration and execution via the Facade.
  		 * 
  		 * <P>
  		 * This test gets the Singleton Facade instance 
  		 * and registers the FacadeTestCommand class 
  		 * to handle 'FacadeTest' Notifcations.<P>
  		 * 
  		 * <P>
  		 * It then constructs such a Notification and notifies Observers
  		 * via the Facade. Success is determined by evaluating 
  		 * a property on an object placed in the body of
  		 * the Notification, which will be modified by the Command.</P>
  		 * 
  		 */
  		public void testRegisterCommandAndNotifyObservers()
        {
   			// Create the Facade, register the FacadeTestCommand to 
   			// handle 'FacadeTest' events
   			IFacade facade = Facade.getInstance();
   			facade.registerCommand("FacadeTestNote", typeof(FacadeTestCommand));
   			
   			// Create a 'FacadeTest' event
   			FacadeTestVO vo = new FacadeTestVO(32);
   			INotification note = new Notification("FacadeTestNote", vo);

			// Notify Observers. The Command associated with the event
			// (FacadeTestCommand) will be invoked, and will multiply 
			// the vo.input value by 2 and set the result on vo.result
   			facade.notifyObservers(note);
   			
   			// test assertions 
   			Assert.True(vo.result == 64, "Expecting vo.result == 64");
   		}

        /**
  		 * Tests the regsitering and retrieving Model proxys via the Facade.
  		 * 
  		 * <P>
  		 * Tests <code>registerModelProxy</code> and <code>retrieveModelProxy</code> in the same test.
  		 * These methods cannot currently be tested separately
  		 * in any meaningful way other than to show that the
  		 * methods do not throw exception when called. </P>
  		 */
  		public void testRegisterAndRetrieveProxy()
        {
   			// register a proxy and retrieve it.
   			IFacade facade = Facade.getInstance();
			facade.registerProxy(new Proxy("colors", new ArrayList(new string[]{"red", "green", "blue"})));
			IProxy proxy = facade.retrieveProxy("colors");
			
			// test assertions
   			Assert.True(proxy is IProxy, "Expecting proxy is IProxy");

			// retrieve data from proxy
			ArrayList data = proxy.getData() as ArrayList;
			
			// test assertions
   			Assert.NotNull(data, "Expecting data not null");
   			Assert.True(data is ArrayList, "Expecting data is ArrayList");
   			Assert.True(data.Count == 3, "Expecting data.Count == 3");
   			Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}

        /**
  		 * Tests the removing Model proxys via the Facade.
  		 */
  		public void testRegisterAndRemoveProxy()
        {
   			// register a proxy, remove it, then try to retrieve it
   			IFacade facade = Facade.getInstance();
			facade.registerProxy(new Proxy("sizes", new ArrayList(new int[]{7, 13, 21})));
			facade.removeProxy("sizes");
			IProxy proxy = facade.retrieveProxy("sizes");
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}
    }
}
