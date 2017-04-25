//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A base <c>ICommand</c> implementation.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Your subclass should override the <c>execute</c> 
    ///         method where your business logic will handle the <c>INotification</c>. 
    ///     </para>
    /// </remarks>
    /// <seealso cref="PureMVC.Core.Controller"/>
    /// <seealso cref="PureMVC.Patterns.Observer.Notification"/>
    /// <seealso cref="PureMVC.Patterns.Command.MacroCommand"/>
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        /// <summary>
        /// Fulfill the use-case initiated by the given <c>INotification</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         In the Command Pattern, an application use-case typically
        ///         begins with some user action, which results in an <c>INotification</c> being broadcast, which 
        ///         is handled by business logic in the <c>execute</c> method of an
        ///         <c>ICommand</c>.
        ///     </para>
        /// </remarks>
        /// <param name="notification">the <c>INotification</c> to handle.</param>
        public virtual void Execute(INotification notification)
        {
        }
    }
}
