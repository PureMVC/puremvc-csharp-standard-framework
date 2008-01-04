using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.mediator;

namespace org.puremvc.csharp.core.view
{
    /**
  	 * A Mediator class used by ViewTest.
  	 * 
  	 * @see org.puremvc.csharp.core.view.ViewTest ViewTest
  	 */
    public class ViewTestMediator : Mediator, IMediator
    {
        /**
		 * The Mediator name
		 */
		public new static String NAME = "ViewTestMediator";
		
		/**
		 * Constructor
		 */
		public ViewTestMediator(Object view) 
            : base(NAME, view)
        { }

		override public IList listNotificationInterests()
		{
			// be sure that the mediator has some Observers created
			// in order to test removeMediator
			return new ArrayList(new string[]{"ABC", "DEF", "GHI", ViewTest.NOTE1, ViewTest.NOTE2, ViewTest.NOTE3});
		}
    }
}
