using System;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;

namespace org.puremvc.csharp.patterns.mediator
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
        public MediatorTest(String methodName) 
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

                ts.AddTest(new MediatorTest("testNameAccessor"));
                ts.AddTest(new MediatorTest("testViewAccessor"));

                return ts;
            }
        }

        /**
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
  		public void testNameAccessor()
        {
			// Create a new Mediator and use accessors to set the mediator name 
   			IMediator mediator = new Mediator();
   			
   			// test assertions
   			Assert.True(mediator.getMediatorName() == Mediator.NAME, "Expecting mediator.getMediatorName() == Mediator.NAME");
   		}

        /**
  		 * Tests getting the name using Mediator class accessor method. 
  		 */
  		public void testViewAccessor()
        {
			// Create a view object
			Object view = new Object();
			
			// Create a new Proxy and use accessors to set the proxy name 
   			IMediator mediator = new Mediator(view);
			   			
   			// test assertions
   			Assert.NotNull(mediator.getViewComponent(), "Expecting mediator.getViewComponent() not null");
   		}
    }
}
