using System;

namespace org.puremvc.csharp.interfaces
{
    /**
	 * The interface definition for a PureMVC Command.
	 *
	 * @see org.puremvc.interfaces INotification
	 */
    public interface ICommand
    {
        /**
		 * Execute the <code>ICommand</code>'s logic to handle a given <code>INotification</code>.
		 * 
		 * @param note an <code>INotification</code> to handle.
		 */
		void execute( INotification notification );
    }
}
