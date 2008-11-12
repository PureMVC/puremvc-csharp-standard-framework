/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;

namespace PureMVC.Tests.Patterns
{
    /**
  	 * A utility class used by SimpleCommandTest.
  	 * 
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTest SimpleCommandTest
  	 * @see org.puremvc.csharp.patterns.command.SimpleCommandTestCommand SimpleCommandTestCommand
  	 */
    public class SimpleCommandTestVO
    {
        /**
		 * Constructor.
		 * 
		 * @param input the number to be fed to the SimpleCommandTestCommand
		 */
		public SimpleCommandTestVO (int input)
        {
			this.input = input;
		}

		public int input;
        public int result;
    }
}
