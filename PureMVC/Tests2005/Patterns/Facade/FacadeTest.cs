/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using NUnitLite;
using NUnit.Framework;

using PureMVC.Interfaces;
using PureMVC.Core;
using PureMVC.Patterns;

namespace PureMVC.Tests.Patterns
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
        public FacadeTest(string methodName) 
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

				ts.AddTest(new FacadeTest("TestGetInstance"));
				ts.AddTest(new FacadeTest("TestRegisterCommandAndSendNotification"));
				ts.AddTest(new FacadeTest("TestRegisterAndRemoveCommandAndSendNotification"));
				ts.AddTest(new FacadeTest("TestRegisterAndRetrieveProxy"));
				ts.AddTest(new FacadeTest("TestRegisterAndRemoveProxy"));
				ts.AddTest(new FacadeTest("TestRegisterRetrieveAndRemoveMediator"));
				ts.AddTest(new FacadeTest("TestHasProxy"));
				ts.AddTest(new FacadeTest("TestHasMediator"));
				ts.AddTest(new FacadeTest("TestHasCommand"));

                return ts;
            }
        }

        /**
  		 * Tests the Facade Singleton Factory Method 
  		 */
  		public void TestGetInstance()
        {
   			// Test Factory Method
			IFacade facade = Facade.Instance;
   			
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
  		public void TestRegisterCommandAndNotifyObservers()
        {
   			// Create the Facade, register the FacadeTestCommand to 
   			// handle 'FacadeTest' events
			IFacade facade = Facade.Instance;
   			facade.RegisterCommand("FacadeTestNote", typeof(FacadeTestCommand));

			// Send notification. The Command associated with the event
			// (FacadeTestCommand) will be invoked, and will multiply 
			// the vo.input value by 2 and set the result on vo.result
			FacadeTestVO vo = new FacadeTestVO(32);
            facade.SendNotification("FacadeTestNote", vo);
   			
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
  		public void TestRegisterAndRemoveCommandAndSendNotification()
        {
   			// Create the Facade, register the FacadeTestCommand to 
   			// handle 'FacadeTest' events
			IFacade facade = Facade.Instance;
   			facade.RegisterCommand("FacadeTestNote", typeof(FacadeTestCommand));
   			facade.RemoveCommand("FacadeTestNote");

			// Send notification. The Command associated with the event
			// (FacadeTestCommand) will NOT be invoked, and will NOT multiply 
			// the vo.input value by 2 
            FacadeTestVO vo = new FacadeTestVO(32);
   			facade.SendNotification("FacadeTestNote", vo);
   			
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
  		public void TestRegisterAndRetrieveProxy()
        {
   			// register a proxy and retrieve it.
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("colors", new List<string>(new string[] { "red", "green", "blue" })));
			IProxy proxy = facade.RetrieveProxy("colors");
			
			// test assertions
   			Assert.True(proxy is IProxy, "Expecting proxy is IProxy");

			// retrieve data from proxy
			List<string> data = (List<string>) proxy.Data;
			
			// test assertions
   			Assert.NotNull(data, "Expecting data not null");
			Assert.True(data is List<string>, "Expecting data is ArrayList");
   			Assert.True(data.Count == 3, "Expecting data.Count == 3");
   			Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}

        /**
  		 * Tests the removing Model proxys via the Facade.
  		 */
  		public void TestRegisterAndRemoveProxy()
        {
   			// register a proxy, remove it, then try to retrieve it
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("sizes", new List<int>(new int[] { 7, 13, 21 })));

			IProxy removedProxy = facade.RemoveProxy("sizes");

            Assert.True(removedProxy.ProxyName == "sizes", "Expecting removedProxy.ProxyName == 'sizes'");

			IProxy proxy = facade.RetrieveProxy("sizes");
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}

  		/**
  		 * Tests registering, retrieving and removing Mediators via the Facade.
  		 */
  		public void TestRegisterRetrieveAndRemoveMediator()
        {  			
   			// register a mediator, remove it, then try to retrieve it
			IFacade facade = Facade.Instance;
			facade.RegisterMediator(new Mediator(Mediator.NAME, new object()));
			
			// retrieve the mediator
   			Assert.NotNull(facade.RetrieveMediator(Mediator.NAME), "Expecting mediator is not null");

			// remove the mediator
			IMediator removedMediator = facade.RemoveMediator(Mediator.NAME);

			// assert that we have removed the appropriate mediator
   			Assert.True(removedMediator.MediatorName == Mediator.NAME, "Expecting removedMediator.MediatorName == Mediator.NAME");
				
			// assert that the mediator is no longer retrievable
   			Assert.True(facade.RetrieveMediator( Mediator.NAME ) == null, "Expecting facade.retrieveMediator(Mediator.NAME) == null )");		  			
   		}

	
  		/**
  		 * Tests the hasProxy Method
  		 */
  		public void TestHasProxy()
		{
   			// register a Proxy
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("hasProxyTest", new List<int>(new int[] { 1, 2, 3 })));
			
   			// assert that the model.hasProxy method returns true
   			// for that proxy name
   			Assert.True(facade.HasProxy("hasProxyTest") == true, "Expecting facade.hasProxy('hasProxyTest') == true");
   		}

  		/**
  		 * Tests the hasMediator Method
  		 */
  		public void TestHasMediator()
		{
   			// register a Mediator
			IFacade facade = Facade.Instance;
			facade.RegisterMediator( new Mediator( "facadeHasMediatorTest", new object() ) );
			
   			// assert that the facade.hasMediator method returns true
   			// for that mediator name
   			Assert.True(facade.HasMediator("facadeHasMediatorTest") == true, "Expecting facade.hasMediator('facadeHasMediatorTest') == true");
   						
   			facade.RemoveMediator( "facadeHasMediatorTest" );
   			
   			// assert that the facade.hasMediator method returns false
   			// for that mediator name
   			Assert.True(facade.HasMediator("facadeHasMediatorTest") == false, "Expecting facade.hasMediator('facadeHasMediatorTest') == false");
   		}

  		/**
  		 * Test hasCommand method.
  		 */
  		public void TestHasCommand()
		{
   			// register the ControllerTestCommand to handle 'hasCommandTest' notes
   			IFacade facade = Facade.Instance;
   			facade.RegisterCommand("facadeHasCommandTest", typeof(FacadeTestCommand));
   			
   			// test that hasCommand returns true for hasCommandTest notifications 
   			Assert.True(facade.HasCommand("facadeHasCommandTest") == true, "Expecting facade.hasCommand('facadeHasCommandTest') == true");
   			
   			// Remove the Command from the Controller
   			facade.RemoveCommand("facadeHasCommandTest");
			
   			// test that hasCommand returns false for hasCommandTest notifications 
   			Assert.True(facade.HasCommand("facadeHasCommandTest") == false, "Expecting facade.hasCommand('facadeHasCommandTest') == false");
   			
   		}
	}
}
