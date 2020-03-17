//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A utility class used by SimpleCommandTest.
    /// </summary>
    /// <seealso cref="SimpleCommandTest"/>
    /// <seealso cref="SimpleCommandTestCommand"/>
    public class SimpleCommandTestVO
    {
        public SimpleCommandTestVO(int input)
        {
            Input = input;
        }

        public int Input { get; set; }

        public int Result { get; set; }
    }
}
