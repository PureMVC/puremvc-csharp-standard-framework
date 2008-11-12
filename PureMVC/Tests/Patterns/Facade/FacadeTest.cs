/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Core;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Facade class.
	 *
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestVO FacadeTestVO
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestCommand FacadeTestCommand
	 */
    [TestClass]
    public class FacadeTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public FacadeTest() 
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
  		 * Tests the Facade Singleton Factory Method 
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void GetInstance()
        {
   			// Test Factory Method
			IFacade facade = Facade.Instance;
   			
   			// test assertions
            Assert.IsNotNull(facade, "Expecting instance not null");
   			Assert.IsTrue(facade is IFacade, "Expecting instance implements IFacade");
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
		[TestMethod]
		[Description("Facade Tests")]
		public void RegisterCommandAndNotifyObservers()
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
   			Assert.IsTrue(vo.result == 64, "Expecting vo.result == 64");
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
		[TestMethod]
		[Description("Facade Tests")]
		public void RegisterAndRemoveCommandAndSendNotification()
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
   			Assert.IsTrue(vo.result != 64, "Expecting vo.result != 64");
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
		[TestMethod]
		[Description("Facade Tests")]
		public void RegisterAndRetrieveProxy()
        {
   			// register a proxy and retrieve it.
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("colors", new List<string>(new string[] { "red", "green", "blue" })));
			IProxy proxy = facade.RetrieveProxy("colors");
			
			// test assertions
   			Assert.IsTrue(proxy is IProxy, "Expecting proxy is IProxy");

			// retrieve data from proxy
			List<string> data = (List<string>) proxy.Data;
			
			// test assertions
   			Assert.IsNotNull(data, "Expecting data not null");
			Assert.IsTrue(data is List<string>, "Expecting data is ArrayList");
   			Assert.IsTrue(data.Count == 3, "Expecting data.Count == 3");
   			Assert.IsTrue(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.IsTrue(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.IsTrue(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}

        /**
  		 * Tests the removing Model proxys via the Facade.
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void RegisterAndRemoveProxy()
        {
   			// register a proxy, remove it, then try to retrieve it
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("sizes", new List<int>(new int[] { 7, 13, 21 })));

			IProxy removedProxy = facade.RemoveProxy("sizes");

            Assert.IsTrue(removedProxy.ProxyName == "sizes", "Expecting removedProxy.ProxyName == 'sizes'");

			IProxy proxy = facade.RetrieveProxy("sizes");
			
			// test assertions
   			Assert.IsNull(proxy, "Expecting proxy is null");
   		}

  		/**
  		 * Tests registering, retrieving and removing Mediators via the Facade.
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void RegisterRetrieveAndRemoveMediator()
        {  			
   			// register a mediator, remove it, then try to retrieve it
			IFacade facade = Facade.Instance;
			facade.RegisterMediator(new Mediator(Mediator.NAME, new object()));
			
			// retrieve the mediator
   			Assert.IsNotNull(facade.RetrieveMediator(Mediator.NAME), "Expecting mediator is not null");

			// remove the mediator
			IMediator removedMediator = facade.RemoveMediator(Mediator.NAME);

			// assert that we have removed the appropriate mediator
   			Assert.IsTrue(removedMediator.MediatorName == Mediator.NAME, "Expecting removedMediator.MediatorName == Mediator.NAME");
				
			// assert that the mediator is no longer retrievable
   			Assert.IsTrue(facade.RetrieveMediator( Mediator.NAME ) == null, "Expecting facade.retrieveMediator(Mediator.NAME) == null )");		  			
   		}

	
  		/**
  		 * Tests the hasProxy Method
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void HasProxy()
		{
   			// register a Proxy
			IFacade facade = Facade.Instance;
			facade.RegisterProxy(new Proxy("hasProxyTest", new List<int>(new int[] { 1, 2, 3 })));
			
   			// assert that the model.hasProxy method returns true
   			// for that proxy name
   			Assert.IsTrue(facade.HasProxy("hasProxyTest") == true, "Expecting facade.hasProxy('hasProxyTest') == true");
   		}

  		/**
  		 * Tests the hasMediator Method
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void HasMediator()
		{
   			// register a Mediator
			IFacade facade = Facade.Instance;
			facade.RegisterMediator( new Mediator( "facadeHasMediatorTest", new object() ) );
			
   			// assert that the facade.hasMediator method returns true
   			// for that mediator name
   			Assert.IsTrue(facade.HasMediator("facadeHasMediatorTest") == true, "Expecting facade.hasMediator('facadeHasMediatorTest') == true");
   						
   			facade.RemoveMediator( "facadeHasMediatorTest" );
   			
   			// assert that the facade.hasMediator method returns false
   			// for that mediator name
   			Assert.IsTrue(facade.HasMediator("facadeHasMediatorTest") == false, "Expecting facade.hasMediator('facadeHasMediatorTest') == false");
   		}

  		/**
  		 * Test hasCommand method.
  		 */
		[TestMethod]
		[Description("Facade Tests")]
		public void HasCommand()
		{
   			// register the ControllerTestCommand to handle 'hasCommandTest' notes
   			IFacade facade = Facade.Instance;
   			facade.RegisterCommand("facadeHasCommandTest", typeof(FacadeTestCommand));
   			
   			// test that hasCommand returns true for hasCommandTest notifications 
   			Assert.IsTrue(facade.HasCommand("facadeHasCommandTest") == true, "Expecting facade.hasCommand('facadeHasCommandTest') == true");
   			
   			// Remove the Command from the Controller
   			facade.RemoveCommand("facadeHasCommandTest");
			
   			// test that hasCommand returns false for hasCommandTest notifications 
   			Assert.IsTrue(facade.HasCommand("facadeHasCommandTest") == false, "Expecting facade.hasCommand('facadeHasCommandTest') == false");
   			
   		}
	}
}
