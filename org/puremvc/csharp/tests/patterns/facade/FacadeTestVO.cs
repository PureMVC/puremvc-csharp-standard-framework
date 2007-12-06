using System;

namespace org.puremvc.csharp.patterns.facade
{
    /**
  	 * A utility class used by FacadeTest.
  	 * 
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTest FacadeTest
  	 * @see org.puremvc.csharp.patterns.facade.FacadeTestCommand FacadeTestCommand
  	 */
    public class FacadeTestVO
    {
        /**
		 * Constructor.
		 * 
		 * @param input the number to be fed to the FacadeTestCommand
		 */
		public FacadeTestVO(int input)
        {
			this.input = input;
		}

		public int input;
        public int result;
    }
}
