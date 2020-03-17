//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// A utility class used by FacadeTest.
    /// </summary>
    /// <seealso cref="FacadeTest"/>
    /// <seealso cref="FacadeTestCommand"/>
    public class FacadeTestVO
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">input the number to be fed to the FacadeTestCommand</param>
        public FacadeTestVO(int input)
        {
            this.input = input;
        }

        public int input;

        public int result;
    }
}
