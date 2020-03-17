//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A SimpleCommand subclass used by SimpleCommandTest.
    /// </summary>
    /// <seealso cref="SimpleCommandTest"/>
    /// <seealso cref="SimpleCommandTestVO"/>
    public class SimpleCommandTestCommand: SimpleCommand
    {
        /// <summary>
        /// Fabricate a result by multiplying the input by 2
        /// </summary>
        /// <param name="note">event the <c>INotification</c> carrying the <c>SimpleCommandTestVO</c></param>
        public override void Execute(INotification note)
        {
            var vo = (SimpleCommandTestVO)note.Body;

            // Fabricate a result
            vo.Result = 2 * vo.Input;
        }
    }
}
