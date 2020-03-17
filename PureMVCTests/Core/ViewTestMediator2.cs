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
    /// <seealso cref="ViewTestMediator2"/>
    public class ViewTestMediator2 : Mediator
    {
        // The Mediator name
        public new const string NAME = "ViewTestMediator2";

        //  Constructor
        public ViewTestMediator2(object viewComponent) : base(NAME, viewComponent)
        {
        }

        // be sure that the mediator has some Observers created
        // in order to test removeMediator
        public override string[] ListNotificationInterests()
        {
            return new [] { ViewTest.NOTE1, ViewTest.NOTE2 };
        }

        public override void HandleNotification(INotification notification)
        {
            ViewTest.lastNotification = notification.Name;
        }

        public ViewTest ViewTest => (ViewTest)ViewComponent;
    }
}
