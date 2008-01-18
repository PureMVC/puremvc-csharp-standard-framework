using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.command
{
    /// <summary>
    /// A base <c>ICommand</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>Your subclass should override the <c>execute</c> method where your business logic will handle the <c>INotification</c></para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.controller.Controller"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    /// <see cref="org.puremvc.csharp.patterns.command.MacroCommand"/>
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        /// <summary>
        /// Fulfill the use-case initiated by the given <c>INotification</c>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to handle</param>
        /// <remarks>
        ///     <para>In the Command Pattern, an application use-case typically begins with some user action, which results in an <c>INotification</c> being broadcast, which is handled by business logic in the <c>execute</c> method of an <c>ICommand</c></para>
        /// </remarks>
		public virtual void execute( INotification notification )
		{ }
    }
}
