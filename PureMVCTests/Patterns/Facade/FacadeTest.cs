//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// Test the PureMVC Facade class.
    /// </summary>
    /// <seealso cref="FacadeTestVO"/>
    /// <seealso cref="FacadeTestCommand"/>
    [TestClass]
    public class FacadeTest
    {
        /// <summary>
        /// Tests the Facade Multiton Factory Method 
        /// </summary>
        [TestMethod]
        public void TestGetInstance()
        {
            // Test Factory Method
            var facade = Facade.GetInstance(() => new Facade());

            // test assertions
            Assert.IsTrue(facade != null, "Expecting instance not null");
            Assert.IsTrue(facade is IFacade, "Expecting instance implements IFacade");
        }

        /// <summary>
        /// Tests Command registration and execution via the Facade.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test gets a Multiton Facade instance 
        ///         and registers the FacadeTestCommand class 
        ///         to handle 'FacadeTest' Notifcations.
        ///     </para>
        ///     <para>
        ///         It then sends a notification using the Facade. 
        ///         Success is determined by evaluating 
        ///         a property on an object placed in the body of
        ///         the Notification, which will be modified by the Command.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterCommandAndSendNotification()
        {
            // Create the Facade, register the FacadeTestCommand to 
            // handle 'FacadeTest' notifications
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterCommand("FacadeTestNote", () => new FacadeTestCommand());

            // Send notification. The Command associated with the event
            // (FacadeTestCommand) will be invoked, and will multiply 
            // the vo.input value by 2 and set the result on vo.result
            var vo = new FacadeTestVO(32);
            facade.SendNotification("FacadeTestNote", vo);

            // test assertions
            Assert.IsTrue(vo.result == 64, "Expecting vo.result == 64");
        }

        /// <summary>
        /// Tests Command removal via the Facade.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This test gets a Multiton Facade instance 
        ///         and registers the FacadeTestCommand class 
        ///         to handle 'FacadeTest' Notifcations. Then it removes the command.
        ///     </para>
        ///     <para>
        ///         It then sends a Notification using the Facade. 
        ///         Success is determined by evaluating 
        ///         a property on an object placed in the body of
        ///         the Notification, which will NOT be modified by the Command.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterAndRemoveCommandAndSendNotification()
        {
            // Create the Facade, register the FacadeTestCommand to 
            // handle 'FacadeTest' events
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterCommand("FacadeTestNote", () => new FacadeTestCommand());
            facade.RemoveCommand("FacadeTestNote");

            // Send notification. The Command associated with the event
            // (FacadeTestCommand) will NOT be invoked, and will NOT multiply 
            // the vo.input value by 2 
            var vo = new FacadeTestVO(32);
            facade.SendNotification("FacadeTestNote", vo);

            // test assertions 
            Assert.IsTrue(vo.result != 64, "Expecting vo.result != 64");
        }

        /// <summary>
        /// Tests the regsitering and retrieving Model proxies via the Facade.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tests <c>registerProxy</c> and <c>retrieveProxy</c> in the same test.
        ///         These methods cannot currently be tested separately
        ///         in any meaningful way other than to show that the
        ///         methods do not throw exception when called.
        ///     </para>
        /// </remarks>
        [TestMethod]
        public void TestRegisterAndRetrieveProxy()
        {
            // register a proxy and retrieve it.
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterProxy(new Proxy.Proxy("colors", new string[3] { "red", "green", "blue" }));
            var proxy = facade.RetrieveProxy("colors");

            // test assertions
            Assert.IsTrue(proxy is IProxy, "Expecting proxy is IProxy");

            // retrieve data from proxy
            var data = (string[])proxy.Data;

            // test assertions
            Assert.IsNotNull(data, "Expecting data not null");
            Assert.IsTrue(data.Length == 3, "Expecting data.Length == 3");
            Assert.IsTrue(data[0] == "red", "Expected data[0] == 'red'");
            Assert.IsTrue(data[1] == "green", "Expecting data[1] == 'green'");
            Assert.IsTrue(data[2] == "blue", "Expecting data[2] == 'blue'");
        }

        /// <summary>
        /// Tests the removing Proxies via the Facade.
        /// </summary>
        [TestMethod]
        public void TestRegisterAndRemoveProxy()
        {
            // register a proxy, remove it, then try to retrieve it
            var facade = Facade.GetInstance(() => new Facade());
            IProxy proxy = new Proxy.Proxy("sizes", new string[3] { "7", "13", "21" });
            facade.RegisterProxy(proxy);

            // remove the proxy
            var removedProxy = facade.RemoveProxy("sizes");

            // assert that we removed the appropriate proxy
            Assert.IsTrue(removedProxy.ProxyName == "sizes", "Expecting removedProxy.ProxyName == 'sizes'");

            // make sure we can no longer retrieve the proxy from the model
            proxy = facade.RemoveProxy("sizes");

            // test assertions
            Assert.IsNull(proxy, "Expecting proxy is null");
        }

        /// <summary>
        /// Tests registering, retrieving and removing Mediators via the Facade.
        /// </summary>
        [TestMethod]
        public void TestRegisterRetrieveAndRemoveMediator()
        {
            // register a mediator, remove it, then try to retrieve it
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterMediator(new Mediator.Mediator(Mediator.Mediator.NAME, new object()));

            // retrieve the mediator
            Assert.IsNotNull(facade.RetrieveMediator(Mediator.Mediator.NAME));

            // remove the mediator
            var removedMediator = facade.RemoveMediator(Mediator.Mediator.NAME);

            // assert that we have removed the appropriate mediator
            Assert.IsTrue(removedMediator.MediatorName == Mediator.Mediator.NAME, "Expecting removedMediator.MediatorName == Mediator.NAME");
        }

        /// <summary>
        /// Tests the hasProxy Method
        /// </summary>
        [TestMethod]
        public void TestHasProxy()
        {
            // register a Proxy
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterProxy(new Proxy.Proxy("hasProxyTest", new int[] { 1, 2, 3 }));

            // assert that the model.hasProxy method returns true
            // for that proxy name
            Assert.IsTrue(facade.HasProxy("hasProxyTest"), "Expecting facade.hasProxy('hasProxyTest') == true");
        }

        /// <summary>
        /// Tests the hasMediator Method
        /// </summary>
        [TestMethod]
        public void TestHasMediator()
        {
            // register a Mediator
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterMediator(new Mediator.Mediator("facadeHasMediatorTest", new object()));

            // assert that the facade.hasMediator method returns true
            // for that mediator name
            Assert.IsTrue(facade.HasMediator("facadeHasMediatorTest") == true, "Expecting facade.HasMediator('facadeHasMediatorTest') == true");

            facade.RemoveMediator("facadeHasMediatorTest");

            // assert that the facade.hasMediator method returns false
            // for that mediator name
            Assert.IsFalse(facade.HasMediator("facadeHasMediatoTest"), "Expecting facade.HasMediator('facadeHasMediatorTest') == false");
        }

        /// <summary>
        /// Test hasCommand method.
        /// </summary>
        [TestMethod]
        public void TestHasCommand()
        {
            // register the ControllerTestCommand to handle 'hasCommandTest' notes
            var facade = Facade.GetInstance(() => new Facade());
            facade.RegisterCommand("facadeHasCommandTest", () => new FacadeTestCommand());

            // test that hasCommand returns true for hasCommandTest notifications 
            Assert.IsTrue(facade.HasCommand("facadeHasCommandTest"), "Expecting facade.HasCommand('facadeHasCommandTest') == true");

            // Remove the Command from the Controller
            facade.RemoveCommand("facadeHasCommandTest");

            // test that hasCommand returns false for hasCommandTest notifications 
            Assert.IsFalse(facade.HasCommand("facadeHasCommandTest"), "facade.HasCommand('facadeHasCommandTest')");
        }
    }
}
