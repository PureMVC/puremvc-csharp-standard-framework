using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.facade;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.mediator
{
    /**
	 * A base <code>IMediator</code> implementation. 
	 * 
	 * @see org.puremvc.core.view.View View
	 */
    public class Mediator : IMediator
    {
        /**
		 * The name of the <code>Mediator</code>. 
		 * 
		 * <P>
		 * Typically, a <code>Mediator</code> will be written to serve
		 * one specific control or group controls and so,
		 * will not have a need to be dynamically named.</P>
		 */
		public const String NAME = "Mediator";
		
		/**
		 * Constructor.
		 */
        public Mediator()
        { }

		public Mediator( Object viewComponent ) 
        {
			this.viewComponent = viewComponent;	
		}

		/**
		 * Get the name of the <code>Mediator</code>.
		 * <P>
		 * Override in subclass!</P>
		 */		
		public virtual String getMediatorName()
		{	
			return Mediator.NAME;
		}

		/**
		 * Get the <code>Mediator</code>'s view component.
		 * 
		 * <P>
		 * Additionally, an implicit getter will usually
		 * be defined in the subclass that casts the view 
		 * object to a type, like this:</P>
		 * 
		 * <listing>
		 *		private function get comboBox : mx.controls.ComboBox 
		 *		{
		 *			return viewComponent as mx.controls.ComboBox;
		 *		}
		 * </listing>
		 */
        public virtual Object getViewComponent()
		{	
			return viewComponent;
		}

		/**
		 * List the <code>INotification</code> names this
		 * <code>Mediator</code> is interested in being notified of.
		 * 
		 * @return IList the list of <code>INotification</code> names 
		 */
        public virtual IList listNotificationInterests()
		{
            return new ArrayList();
		}

		/**
		 * Handle <code>INotification</code>s.
		 * 
		 * <P>
		 * Typically this will be handled in a switch statement,
		 * with one 'case' entry per <code>INotification</code>
		 * the <code>Mediator</code> is interested in.
		 */
        public virtual void handleNotification(INotification notification)
        { }
		
		// The view component
		protected Object viewComponent;
    }
}
