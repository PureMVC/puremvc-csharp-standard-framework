/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.core;
using org.puremvc.csharp.patterns.mediator;
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
				ts.AddTest(new FacadeTest("testRegisterCommandAndSendNotification"));
				ts.AddTest(new FacadeTest("testRegisterAndRemoveCommandAndSendNotification"));
				ts.AddTest(new FacadeTest("testRegisterAndRetrieveProxy"));
				ts.AddTest(new FacadeTest("testRegisterAndRemoveProxy"));
				ts.AddTest(new FacadeTest("testRegisterRetrieveAndRemoveMediator"));
				ts.AddTest(new FacadeTest("testHasProxy"));
				ts.AddTest(new FacadeTest("testHasMediator"));
				ts.AddTest(new FacadeTest("testHasCommand"));

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

			// Send notification. The Command associated with the event
			// (FacadeTestCommand) will be invoked, and will multiply 
			// the vo.input value by 2 and set the result on vo.result
			FacadeTestVO vo = new FacadeTestVO(32);
            facade.sendNotification("FacadeTestNote", vo);
   			
   			// test assertions 
   			Assert.True(vo.result == 64, "Expecting vo.result == 64");
   		}

  		/**
  		 * Tests Command removal via the Facade.
  		 * 
  		 * <P>
  		 * This test gets the Singleton Facade instance 
  		 * and registers the FacadeTestCommand class 
  		 * to handle 'FacadeTest' Notifcations. Then it removes the command.<P>
  		 * 
  		 * <P>
  		 * It then sends a Notification using the Facade. 
  		 * Success is determined by evaluating 
  		 * a property on an object placed in the body of
  		 * the Notification, which will NOT be modified by the Command.</P>
  		 * 
  		 */
  		public void testRegisterAndRemoveCommandAndSendNotification()
        {
   			// Create the Facade, register the FacadeTestCommand to 
   			// handle 'FacadeTest' events
   			IFacade facade = Facade.getInstance();
   			facade.registerCommand("FacadeTestNote", typeof(FacadeTestCommand));
   			facade.removeCommand("FacadeTestNote");

			// Send notification. The Command associated with the event
			// (FacadeTestCommand) will NOT be invoked, and will NOT multiply 
			// the vo.input value by 2 
            FacadeTestVO vo = new FacadeTestVO(32);
   			facade.sendNotification("FacadeTestNote", vo);
   			
   			// test assertions 
   			Assert.True(vo.result != 64, "Expecting vo.result != 64");
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

			IProxy removedProxy = facade.removeProxy("sizes");

            Assert.True(removedProxy.getProxyName() == "sizes", "Expecting removedProxy.getProxyName() == 'sizes'");

			IProxy proxy = facade.retrieveProxy("sizes");
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}

  		/**
  		 * Tests registering, retrieving and removing Mediators via the Facade.
  		 */
  		public void testRegisterRetrieveAndRemoveMediator()
        {  			
   			// register a mediator, remove it, then try to retrieve it
   			IFacade facade = Facade.getInstance();
			facade.registerMediator(new Mediator(Mediator.NAME, new Object()));
			
			// retrieve the mediator
   			Assert.NotNull(facade.retrieveMediator(Mediator.NAME), "Expecting mediator is not null");

			// remove the mediator
			IMediator removedMediator = facade.removeMediator(Mediator.NAME);

			// assert that we have removed the appropriate mediator
   			Assert.True(removedMediator.getMediatorName() == Mediator.NAME, "Expecting removedMediator.getMediatorName() == Mediator.NAME");
				
			// assert that the mediator is no longer retrievable
   			Assert.True(facade.retrieveMediator( Mediator.NAME ) == null, "Expecting facade.retrieveMediator(Mediator.NAME) == null )");		  			
   		}

	
  		/**
  		 * Tests the hasProxy Method
  		 */
  		public void testHasProxy()
		{
   			// register a Proxy
			IFacade facade = Facade.getInstance();
			facade.registerProxy( new Proxy( "hasProxyTest", new ArrayList(new int[]{1,2,3})));
			
   			// assert that the model.hasProxy method returns true
   			// for that proxy name
   			Assert.True(facade.hasProxy("hasProxyTest") == true, "Expecting facade.hasProxy('hasProxyTest') == true");
   		}

  		/**
  		 * Tests the hasMediator Method
  		 */
  		public void testHasMediator()
		{
   			// register a Mediator
			IFacade facade = Facade.getInstance();
			facade.registerMediator( new Mediator( "facadeHasMediatorTest", new Object() ) );
			
   			// assert that the facade.hasMediator method returns true
   			// for that mediator name
   			Assert.True(facade.hasMediator("facadeHasMediatorTest") == true, "Expecting facade.hasMediator('facadeHasMediatorTest') == true");
   						
   			facade.removeMediator( "facadeHasMediatorTest" );
   			
   			// assert that the facade.hasMediator method returns false
   			// for that mediator name
   			Assert.True(facade.hasMediator("facadeHasMediatorTest") == false, "Expecting facade.hasMediator('facadeHasMediatorTest') == false");
   		}

  		/**
  		 * Test hasCommand method.
  		 */
  		public void testHasCommand()
		{
   			// register the ControllerTestCommand to handle 'hasCommandTest' notes
   			IFacade facade = Facade.getInstance();
   			facade.registerCommand("facadeHasCommandTest", typeof(FacadeTestCommand));
   			
   			// test that hasCommand returns true for hasCommandTest notifications 
   			Assert.True(facade.hasCommand("facadeHasCommandTest") == true, "Expecting facade.hasCommand('facadeHasCommandTest') == true");
   			
   			// Remove the Command from the Controller
   			facade.removeCommand("facadeHasCommandTest");
			
   			// test that hasCommand returns false for hasCommandTest notifications 
   			Assert.True(facade.hasCommand("facadeHasCommandTest") == false, "Expecting facade.hasCommand('facadeHasCommandTest') == false");
   			
   		}
	}
}
