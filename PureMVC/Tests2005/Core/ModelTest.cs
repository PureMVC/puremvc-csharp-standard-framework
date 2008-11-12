/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;
using System.Threading;

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
				ts.AddTest(new ModelTest("TestMultiThreadedOperations"));

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
			string name = "colors" + Thread.CurrentThread.Name;
			model.RegisterProxy(new Proxy(name, new List<string>(new string[] { "red", "green", "blue" })));
			IProxy proxy = model.RetrieveProxy(name);
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
			string name = "sizes" + Thread.CurrentThread.Name;
			model.RegisterProxy(new Proxy(name, new List<int>(new int[] { 7, 13, 21 })));

			IProxy removedProxy = model.RemoveProxy(name);

			Assert.True(removedProxy.ProxyName == name, "Expecting removedProxy.ProxyName == name");

			IProxy proxy = model.RetrieveProxy(name);
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}
  		
  		/**
  		 * Tests the hasProxy Method
  		 */
  		public void TestHasProxy() {
  			
   			// register a proxy
   			IModel model = Model.Instance;
			string name = "aces" + Thread.CurrentThread.Name;
			IProxy proxy = new Proxy(name, new List<string>(new string[] { "clubs", "spades", "hearts", "diamonds" }));
			model.RegisterProxy(proxy);
			
   			// assert that the model.hasProxy method returns true
   			// for that proxy name
			Assert.True(model.HasProxy(name) == true, "Expecting model.hasProxy(name) == true");
			
			// remove the proxy
			model.RemoveProxy(name);
			
   			// assert that the model.hasProxy method returns false
   			// for that proxy name
			Assert.True(model.HasProxy(name) == false, "Expecting model.hasProxy(name) == false");
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

		/// <summary>
		/// Test all of the function above using many threads at once.
		/// </summary>
		public void TestMultiThreadedOperations()
		{
			count = 20;
			IList<Thread> threads = new List<Thread>();

			for (int i = 0; i < count; i++)
			{
				Thread t = new Thread(new ThreadStart(MultiThreadedTestFunction));
				t.Name = "ControllerTest" + i;
				threads.Add(t);
			}

			foreach (Thread t in threads)
			{
				t.Start();
			}

			while (true)
			{
				if (count <= 0) break;
				Thread.Sleep(100);
			}
		}

		private int count = 0;

		private int threadIterationCount = 10000;

		private void MultiThreadedTestFunction()
		{
			for (int i = 0; i < threadIterationCount; i++)
			{
				// All we need to do is test the registration and removal of proxies.
				TestRegisterAndRetrieveProxy();
				TestRegisterAndRemoveProxy();
				TestHasProxy();
			}

			count--;
		}
	}
}
