//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Mediator
{
    /// <summary>
    /// Test the PureMVC Mediator class.
    /// </summary>
    /// <seealso cref="IMediator"/>
    /// <seealso cref="Mediator"/>
    [TestClass]
    public class MediatorTest
    {
        /// <summary>
        /// Tests getting the name using Mediator class accessor method. 
        /// </summary>
        [TestMethod]
        public void TestNameAccessor()
        {
            // Create a new Mediator and use accessors to set the medi ator name 
            var mediator = new Mediator("TestMediator");

            // test assertions
            Assert.IsTrue(mediator.MediatorName == "TestMediator", "Expecting mediator.MediatorName == 'TestMediator'");
        }

        /// <summary>
        /// Tests getting the name using Mediator class accessor method. 
        /// </summary>
        [TestMethod]
        public void TestViewAccessor()
        {
            // Create a view object
            var view = new object();

            // Create a new Mediator and use accessors to set the mediator name 
            var mediator = new Mediator("TestMediator", view);

            // test assertions
            Assert.IsNotNull(mediator.ViewComponent, "Expecting mediator.ViewComponent not null");
        }
    }
}
