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
    /// <seealso cref="ViewTest"/>
    public class ViewTestMediator6 : Mediator
    {
        // The Mediator base name
        public new const string NAME = "ViewTestMediator6";

        // Constructor
        public ViewTestMediator6(string name, object view): base(name, view)
        {
            
        }
    
        public override string[] ListNotificationInterests()
        {
            return new [] { ViewTest.NOTE6 };
        }
        public override void HandleNotification(INotification notification)
        {
            Facade.RemoveMediator(MediatorName);
        }

        public override void OnRemove()
        {
            ViewTest.counter++;
        }

        public ViewTest ViewTest => (ViewTest) ViewComponent;
    }
}