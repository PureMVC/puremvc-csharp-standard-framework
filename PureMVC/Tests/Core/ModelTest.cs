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
using PureMVC.Patterns;
using PureMVC.Core;

namespace PureMVC.Tests.Core
{
    /**
	 * Test the PureMVC Model class.
	 */
    [TestFixture]
    public class ModelTest : TestCase
    {
         /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ModelTest(string methodName)
            : base(methodName)
        { }

        /**
		 * Create the TestSuite.
		 */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(ModelTest));

                ts.AddTest(new ModelTest("TestGetInstance"));
                ts.AddTest(new ModelTest("TestRegisterAndRetrieveProxy"));
                ts.AddTest(new ModelTest("TestRegisterAndRemoveProxy"));
				ts.AddTest(new ModelTest("TestHasProxy"));
				ts.AddTest(new ModelTest("TestOnRegisterAndOnRemove"));

				return ts;
            }
        }

        /**
  		 * Tests the Model Singleton Factory Method 
  		 */
  		public void TestGetInstance()
        {
   			// Test Factory Method
   			IModel model = Model.Instance;
   			
   			// test assertions
            Assert.NotNull(model, "Expecting instance not null");
            Assert.True(model is IModel, "Expecting instance implements IModel");
   		}

  		/**
  		 * Tests the proxy registration and retrieval methods.
  		 * 
  		 * <P>
  		 * Tests <code>registerProxy</code> and <code>retrieveProxy</code> in the same test.
  		 * These methods cannot currently be tested separately
  		 * in any meaningful way other than to show that the
  		 * methods do not throw exception when called. </P>
  		 */
  		public void TestRegisterAndRetrieveProxy()
        {
   			// register a proxy and retrieve it.
   			IModel model = Model.Instance;
			model.RegisterProxy(new Proxy("colors", new List<string>(new string[] { "red", "green", "blue" })));
			IProxy proxy = model.RetrieveProxy("colors");
			List<string> data = (List<string>) proxy.Data;
			
			// test assertions
            Assert.NotNull(data, "Expecting data not null");
			Assert.True(data is List<string>, "Expecting data type is ArrayList");
   			Assert.True(data.Count == 3, "Expecting data.length == 3");
   			Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}
  		
  		/**
  		 * Tests the proxy removal method.
  		 */
  		public void TestRegisterAndRemoveProxy()
        {
   			// register a proxy, remove it, then try to retrieve it
   			IModel model = Model.Instance;
			model.RegisterProxy(new Proxy("sizes", new List<int>(new int[] { 7, 13, 21 })));
			
            IProxy removedProxy = model.RemoveProxy("sizes");

            Assert.True(removedProxy.ProxyName == "sizes", "Expecting removedProxy.ProxyName == 'sizes'");

			IProxy proxy = model.RetrieveProxy("sizes");
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}
  		
  		/**
  		 * Tests the hasProxy Method
  		 */
  		public void TestHasProxy() {
  			
   			// register a proxy
   			IModel model = Model.Instance;
			IProxy proxy = new Proxy("aces", new List<string>(new string[] { "clubs", "spades", "hearts", "diamonds" }));
			model.RegisterProxy(proxy);
			
   			// assert that the model.hasProxy method returns true
   			// for that proxy name
   			Assert.True(model.HasProxy("aces") == true, "Expecting model.hasProxy('aces') == true");
			
			// remove the proxy
			model.RemoveProxy("aces");
			
   			// assert that the model.hasProxy method returns false
   			// for that proxy name
   			Assert.True(model.HasProxy("aces") == false, "Expecting model.hasProxy('aces') == false");
   		}
  		
		/**
		 * Tests that the Model calls the onRegister and onRemove methods
		 */
		public void TestOnRegisterAndOnRemove() {
			
  			// Get the Singleton View instance
  			IModel model = Model.Instance;

			// Create and register the test mediator
			IProxy proxy = new ModelTestProxy();
			model.RegisterProxy(proxy);

			// assert that onRegsiter was called, and the proxy responded by setting its data accordingly
			Assert.True(proxy.Data.ToString() == ModelTestProxy.ON_REGISTER_CALLED, "Expecting proxy.Data.ToString() == ModelTestProxy.ON_REGISTER_CALLED");
			
			// Remove the component
			model.RemoveProxy(ModelTestProxy.NAME);
			
			// assert that onRemove was called, and the proxy responded by setting its data accordingly
   			Assert.True(proxy.Data.ToString() == ModelTestProxy.ON_REMOVE_CALLED, "Expecting proxy.Data.ToString() == ModelTestProxy.ON_REMOVE_CALLED");
		}
    }
}
