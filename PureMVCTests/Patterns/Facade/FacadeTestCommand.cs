//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// A SimpleCommand subclass used by FacadeTest.
    /// </summary>
    /// <seealso cref="FacadeTest"/>
    /// <seealso cref="FacadeTestVO"/>
    public class FacadeTestCommand: SimpleCommand
    {
        /// <summary>
        /// Fabricate a result by multiplying the input by 2
        /// </summary>
        /// <param name="notification">note the Notification carrying the FacadeTestVO</param>
        public override void Execute(INotification notification)
        {
            var vo = (FacadeTestVO) notification.Body;

            // Fabricate a result
            vo.result = 2 * vo.input;
        }

    }
}
