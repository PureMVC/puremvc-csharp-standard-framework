//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Core;
using PureMVC.Patterns.Mediator;

namespace PureMVC.Core
{
    /// <summary>
    /// A Mediator class used by ViewTest.
    /// </summary>
    /// <seealso cref="ViewTest"/>
    public class ViewTestMediator4 : Mediator
    {
        // The Mediator name
        public new const string NAME = "ViewTestMediator4";

        // Constructor
        public ViewTestMediator4(object view): base(NAME, view)
        {
            
        }

        public override void OnRegister()
        {
            ViewTest.onRegisterCalled = true;
        }

        public override void OnRemove()
        {
            ViewTest.onRemoveCalled = true;
        }

        public ViewTest ViewTest => (ViewTest) ViewComponent;
    }
}