//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace PureMVC.Core
{
    /// <summary>
    /// A Mediator class used by ViewTest.
    /// </summary>
    public class ViewTestMediator5 : Mediator
    {
        // The Mediator name
        public new const string NAME = "ViewTestMediator5";

        // Constructor
        public ViewTestMediator5(object view): base(NAME, view)
        {
        }
    
        public override string[] ListNotificationInterests()
        {
            return new string[1] { ViewTest.NOTE5 };
        }
        public override void HandleNotification(INotification notification)
        {
            ViewTest.counter++;
        }

        public ViewTest ViewTest => (ViewTest) ViewComponent;
    }
}