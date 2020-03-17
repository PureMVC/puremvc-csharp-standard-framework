//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Proxy
{
    /// <summary>
    /// Test the PureMVC Proxy class.
    /// </summary>
    /// <seealso cref="IProxy"/>
    /// <seealso cref="Proxy"/>
    [TestClass]
    public class ProxyTest
    {
        /// <summary>
        /// Tests getting the name using Proxy class accessor method. Setting can only be done in constructor.
        /// </summary>
        [TestMethod]
        public void TestNameAccessor()
        {
            // Create a new Proxy and use accessors to set the proxy name 
            var proxy = new Proxy("TestProxy");

            // test assertions
            Assert.IsTrue(proxy.ProxyName == "TestProxy", "Expecting proxy.ProxyName == 'TestProxy'");
        }

        /// <summary>
        /// Tests setting and getting the data using Proxy class accessor methods.
        /// </summary>
        [TestMethod]
        public void TestDataAccessor()
        {
            // Create a new Proxy and use accessors to set the data
            var proxy = new Proxy("colors");
            proxy.Data = new string[3] { "red", "green", "blue" };

            // test assertions
            var data = (string[])proxy.Data;
            Assert.IsTrue(data.Length == 3, "Expecting data.length == 3");
            Assert.IsTrue(data[0] == "red", "Expecting data[0] == 'red'");
            Assert.IsTrue(data[1] == "green", "Expecting data[1] == 'green'");
            Assert.IsTrue(data[2] == "blue", "Expecting data[2] == 'blue'");
        }

        /// <summary>
        /// Tests setting the name and body using the Notification class Constructor.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            // Create a new Proxy using the Constructor to set the name and data
            var proxy = new Proxy("colors", new string[3] { "red", "green", "blue" });

            // test assertions
            var data = (string[])proxy.Data;
            Assert.IsNotNull(proxy, "Expecting proxy not null");
            Assert.IsTrue(proxy.ProxyName == "colors", "Expecting proxy.ProxyName == 'colors'");
            Assert.IsTrue(data.Length == 3, "Expecting data.Count == 3");
            Assert.IsTrue(data[0] == "red", "Expecting data[0] == 'red'");
            Assert.IsTrue(data[1] == "green", "Expecting data[1] == 'green'");
            Assert.IsTrue(data[2] == "blue", "Expecting data[2] == 'blue'");
        }
    }
}
