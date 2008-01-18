using System;

namespace org.puremvc.csharp.interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Command
    /// </summary>
    /// <see cref="org.puremvc.csharp.interfaces.INotification"/>
    public interface ICommand
    {
        /// <summary>
        /// Execute the <c>ICommand</c>'s logic to handle a given <c>INotification</c>
        /// </summary>
        /// <param name="notification">An <c>INotification</c> to handle</param>
		void execute( INotification notification );
    }
}
