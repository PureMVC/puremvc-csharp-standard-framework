/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Mediator class.
	 * 
	 * @see org.puremvc.csharp.interfaces.IMediator IMediator
	 * @see org.puremvc.csharp.patterns.mediator.Mediator Mediator
	 */
    [TestClass]
    public class MediatorTest : BaseTest
    {
        /**
  		 * Constructor.
  		 */
        public MediatorTest() 
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
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
		[TestMethod]
		[Description("Mediator Tests")]
		public void NameAccessor()
        {
			// Create a new Mediator and use accessors to set the mediator name 
   			IMediator mediator = new Mediator("TestMediator");
   			
   			// test assertions
            Assert.IsTrue(mediator.MediatorName == "TestMediator", "Expecting mediator.MediatorName == 'TestMediator'");
   		}

        /**
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
		[TestMethod]
		[Description("Mediator Tests")]
		public void ViewAccessor()
        {
			// Create a view object
			object view = new object();
			
			// Create a new Proxy and use accessors to set the proxy name 
            IMediator mediator = new Mediator("TestMediator", view);
			   			
   			// test assertions
   			Assert.IsNotNull(mediator.ViewComponent, "Expecting mediator.ViewComponent not null");
   		}
    }
}
