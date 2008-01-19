/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;
using System.Collections;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.proxy;

namespace org.puremvc.csharp.core.model
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
        public ModelTest(String methodName)
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

                ts.AddTest(new ModelTest("testGetInstance"));
                ts.AddTest(new ModelTest("testRegisterAndRetrieveProxy"));
                ts.AddTest(new ModelTest("testRegisterAndRemoveProxy"));

                return ts;
            }
        }

        /**
  		 * Tests the Model Singleton Factory Method 
  		 */
  		public void testGetInstance()
        {
   			// Test Factory Method
   			IModel model = Model.getInstance();
   			
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
  		public void testRegisterAndRetrieveProxy()
        {
   			// register a proxy and retrieve it.
   			IModel model = Model.getInstance();
			model.registerProxy(new Proxy("colors", new ArrayList(new string[]{"red", "green", "blue"})));
			IProxy proxy = model.retrieveProxy("colors") as Proxy;
            ArrayList data = proxy.getData() as ArrayList;
			
			// test assertions
            Assert.NotNull(data, "Expecting data not null");
            Assert.True(data is ArrayList, "Expecting data type is ArrayList");
   			Assert.True(data.Count == 3, "Expecting data.length == 3");
   			Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}
  		
  		/**
  		 * Tests the proxy removal method.
  		 */
  		public void testRegisterAndRemoveProxy()
        {
   			// register a proxy, remove it, then try to retrieve it
   			IModel model = Model.getInstance();
			model.registerProxy(new Proxy("sizes", new ArrayList(new int[]{7, 13, 21})));
			
            IProxy removedProxy = model.removeProxy("sizes");

            Assert.True(removedProxy.getProxyName() == "sizes", "Expecting removedProxy.getProxyName() == 'sizes'");

			IProxy proxy = model.retrieveProxy("sizes");
			
			// test assertions
   			Assert.Null(proxy, "Expecting proxy is null");
   		}
    }
}
