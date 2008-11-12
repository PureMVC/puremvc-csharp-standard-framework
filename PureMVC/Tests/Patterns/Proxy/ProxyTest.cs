/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Proxy class.
	 * 
	 * @see org.puremvc.interfaces.IProxy IProxy
	 * @see org.puremvc.patterns.proxy.Proxy Proxy
	 */
    [TestClass]
    public class ProxyTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public ProxyTest() 
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
  		 * Tests getting the name using Proxy class accessor method. Setting can only be done in constructor.
  		 */
		[TestMethod]
		[Description("Proxy Tests")]
		public void NameAccessor()
        {
			// Create a new Proxy and use accessors to set the proxy name 
   			IProxy proxy = new Proxy("TestProxy");
   			
   			// test assertions
   			Assert.IsTrue(proxy.ProxyName == "TestProxy", "Expecting proxy.ProxyName == 'TestProxy'");
   		}

  		/**
  		 * Tests setting and getting the data using Proxy class accessor methods.
  		 */
		[TestMethod]
		[Description("Proxy Tests")]
		public void TestDataAccessors()
        {
			// Create a new Proxy and use accessors to set the data
   			IProxy proxy = new Proxy("colors");
			proxy.Data = new List<string>(new string[] { "red", "green", "blue" });
			List<string> data = (List<string>) proxy.Data;
   			
   			// test assertions
   			Assert.IsTrue(data.Count == 3, "Expecting data.Count == 3");
   			Assert.IsTrue(data[0].ToString() == "red", "Expecting data[0] == 'red'");
   			Assert.IsTrue(data[1].ToString() == "green", "Expecting data[1] == 'green'");
   			Assert.IsTrue(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}

  		/**
  		 * Tests setting the name and body using the Notification class Constructor.
  		 */
		[TestMethod]
		[Description("Proxy Tests")]
		public void TestConstructor()
        {
			// Create a new Proxy using the Constructor to set the name and data
			IProxy proxy = new Proxy("colors", new List<string>(new string[] { "red", "green", "blue" }));
			List<string> data = (List<string>) proxy.Data;
   			
   			// test assertions
   			Assert.IsNotNull(proxy, "Expecting proxy not null");
   			Assert.IsTrue(proxy.ProxyName == "colors", "Expecting proxy.ProxyName == 'colors'");
            Assert.IsTrue(data.Count == 3, "Expecting data.Count == 3");
            Assert.IsTrue(data[0].ToString() == "red", "Expecting data[0] == 'red'");
            Assert.IsTrue(data[1].ToString() == "green", "Expecting data[1] == 'green'");
            Assert.IsTrue(data[2].ToString() == "blue", "Expecting data[2] == 'blue'");
   		}
    }
}
