/*
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved.
 Your reuse is governed by the Creative Commons Attribution 3.0 United States License
*/
using System;

namespace org.puremvc.csharp.core.controller
{
    /**
  	 * A utility class used by ControllerTest.
  	 * 
  	 * @see org.puremvc.csharp.core.controller.ControllerTest ControllerTest
  	 * @see org.puremvc.csharp.core.controller.ControllerTestCommand ControllerTestCommand
  	 */
    public class ControllerTestVO
    {
        /**
		 * Constructor.
		 * 
		 * @param input the number to be fed to the ControllerTestCommand
		 */
		public ControllerTestVO (int input)
        {
			this.input = input;
		}

		public int input;
		public int result;
    }
}
