//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Command.
    /// </summary>
    /// <seealso cref="INotification"/>
    public interface ICommand: INotifier
    {
        /// <summary>
        /// Execute the <c>ICommand</c>'s logic to handle a given <c>INotification</c>.
        /// </summary>
        /// <param name="Notification">an <c>INotification</c> to handle.</param>
        void Execute(INotification Notification);
    }
}
