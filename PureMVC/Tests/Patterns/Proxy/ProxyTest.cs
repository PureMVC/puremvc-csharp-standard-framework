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

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Proxy class.
	 * 
	 * @see org.puremvc.interfaces.IProxy IProxy
	 * @see org.puremvc.patterns.proxy.Proxy Proxy
	 */
    [TestFixture]
    public class ProxyTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public ProxyTest(string methodName) 
            : base(methodName)
        { }

        /**
         * Create the TestSuite.
         */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(ProxyTest));

                ts.AddTest(new ProxyTest("testNameAccessor"));
                ts.AddTest(new ProxyTest("testDataAccessors"));
                ts.AddTest(new ProxyTest("testConstructor"));

                return ts;
            }
        }

        /**
  		 * Tests getting the name using Proxy class accessor method. Setting can only be done in constructor.
  		 */
  		public void testNameAccessor()
        {
			// Create a new Proxy and use accessors to set the proxy name 
   			IProxy proxy = new Proxy("TestProxy");
   			
   			// test assertions
   			Assert.True(proxy.ProxyName == "TestProxy", "Expecting proxy.ProxyName == 'TestProxy'");
   		}

  		/**
  		 * Tests setting and getting the data using Proxy class accessor methods.
  		 */
  		public void testDataAccessors()
        {
			// Create a new Proxy and use accessors to set the data
   			IProxy proxy = new Proxy("colors");
			proxy.Data = new List<string>(new string[] { "red", "green", "blue" });
			List<string> data = (List<string>) proxy.Data;
   			
   			// test assertions
   			Assert.True(data.Count == 3, "Expecting data.Count == 3");
   			Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
   			Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
   			Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}

  		/**
  		 * Tests setting the name and body using the Notification class Constructor.
  		 */
  		public void testConstructor()
        {
			// Create a new Proxy using the Constructor to set the name and data
			IProxy proxy = new Proxy("colors", new List<string>(new string[] { "red", "green", "blue" }));
			List<string> data = (List<string>) proxy.Data;
   			
   			// test assertions
   			Assert.NotNull(proxy, "Expecting proxy not null");
   			Assert.True(proxy.ProxyName == "colors", "Expecting proxy.ProxyName == 'colors'");
            Assert.True(data.Count == 3, "Expecting data.Count == 3");
            Assert.True(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.True(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.True(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}
    }
}
