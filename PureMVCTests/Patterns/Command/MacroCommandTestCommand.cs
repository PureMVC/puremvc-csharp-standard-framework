//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A MacroCommand subclass used by MacroCommandTest.
    /// </summary>
    /// <seealso cref="MacroCommandTest"/>
    /// <seealso cref="MacroCommandTestSub1Command"/>
    /// <seealso cref="MacroCommandTestSub2Command"/>
    /// <seealso cref="MacroCommandTestVO"/>
    public class MacroCommandTestCommand: MacroCommand
    {
        /// <summary>
        /// Initialize the MacroCommandTestCommand by adding
        /// its 2 SubCommands.
        /// </summary>
        protected override void InitializeMacroCommand()
        {
            AddSubCommand(() => new MacroCommandTestSub1Command());
            AddSubCommand(() => new MacroCommandTestSub2Command());
        }
    }
}
