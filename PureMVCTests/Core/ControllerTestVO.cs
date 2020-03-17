//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Core
{
    /// <summary>
    /// A utility class used by ControllerTest.
    /// </summary>
    /// <seealso cref="ControllerTest"/>
    /// <seealso cref="ControllerTestCommand"/>
    public class ControllerTestVO
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">the number to be fed to the ControllerTestCommand</param>
        public ControllerTestVO(int input)
        {
            this.input = input;
        }

        public int input;

        public int result;
    }
}
