//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A utility class used by MacroCommandTest.
    /// </summary>
    /// <seealso cref="MacroCommandTest"/>
    /// <seealso cref="MacroCommandTestCommand"/>
    /// <seealso cref="MacroCommandTestSub1Command"/>
    /// <seealso cref="MacroCommandTestSub2Command"/>
    public class MacroCommandTestVO
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="input">input the number to be fed to the MacroCommandTestCommand</param>
        public MacroCommandTestVO(int input)
        {
            Input = input;
        }

        public int Input { get; set; }

        public int Result1 { get; set; }

        public int Result2 { get; set; }
    }
}
