/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

using NUnitLite;
using NUnit.Framework;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Mediator class.
	 * 
	 * @see org.puremvc.csharp.interfaces.IMediator IMediator
	 * @see org.puremvc.csharp.patterns.mediator.Mediator Mediator
	 */
    [TestFixture]
    public class MediatorTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public MediatorTest(string methodName) 
            : base(methodName)
        { }

        /**
         * Create the TestSuite.
         */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(MediatorTest));

                ts.AddTest(new MediatorTest("TestNameAccessor"));
                ts.AddTest(new MediatorTest("TestViewAccessor"));

                return ts;
            }
        }

        /**
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
  		public void TestNameAccessor()
        {
			// Create a new Mediator and use accessors to set the mediator name 
   			IMediator mediator = new Mediator("TestMediator");
   			
   			// test assertions
            Assert.True(mediator.MediatorName == "TestMediator", "Expecting mediator.MediatorName == 'TestMediator'");
   		}

        /**
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
  		public void TestViewAccessor()
        {
			// Create a view object
			object view = new object();
			
			// Create a new Proxy and use accessors to set the proxy name 
            IMediator mediator = new Mediator("TestMediator", view);
			   			
   			// test assertions
   			Assert.NotNull(mediator.ViewComponent, "Expecting mediator.ViewComponent not null");
   		}
    }
}
