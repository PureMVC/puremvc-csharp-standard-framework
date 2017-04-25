//
//  PureMVC C# Standard
//
//  Copyright(c) 2017 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// A base <c>ICommand</c> implementation that executes other <c>ICommand</c>s.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A <c>MacroCommand</c> maintains an list of
    ///         <c>ICommand</c> Class references called <i>SubCommands</i>.
    ///     </para>
    ///     <para>
    ///         When <c>execute</c> is called, the <c>MacroCommand</c> 
    ///         instantiates and calls <c>execute</c> on each of its <i>SubCommands</i> turn.
    ///         Each <i>SubCommand</i> will be passed a reference to the original
    ///         <c>INotification</c> that was passed to the <c>MacroCommand</c>'s 
    ///         <c>execute</c> method.
    ///     </para>
    ///     <para>
    ///         Unlike <c>SimpleCommand</c>, your subclass
    ///         should not override <c>execute</c>, but instead, should 
    ///         override the <c>initializeMacroCommand</c> method, 
    ///         calling <c>addSubCommand</c> once for each <i>SubCommand</i>
    ///         to be executed.
    ///     </para>
    /// </remarks>
    /// <seealso cref="PureMVC.Core.Controller"/>
    /// <seealso cref="PureMVC.Patterns.Observer.Notification"/>
    /// <seealso cref="PureMVC.Patterns.Command.SimpleCommand"/>
    public class MacroCommand : Notifier, ICommand, INotifier
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You should not need to define a constructor, 
        ///         instead, override the <c>initializeMacroCommand</c>
        ///         method.
        ///     </para>
        ///     <para>
        ///         If your subclass does define a constructor, be 
        ///         sure to call <c>super()</c>.
        ///     </para>
        /// </remarks>
        public MacroCommand()
        {
            subcommands = new List<Func<ICommand>>();
            InitializeMacroCommand();
        }

        /// <summary>
        /// Initialize the <c>MacroCommand</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         In your subclass, override this method to 
        ///         initialize the <c>MacroCommand</c>'s <i>SubCommand</i>  
        ///         list with <c>ICommand</c> class references like
        ///         this:
        ///     </para>
        ///     <example>
        ///         <code>
        ///             override void InitializeMacroCommand() 
        ///             {
        ///                 AddSubCommand(() => new com.me.myapp.controller.FirstCommand());
        ///                 AddSubCommand(() => new com.me.myapp.controller.SecondCommand());
        ///                 AddSubCommand(() => new com.me.myapp.controller.ThirdCommand());
        ///             }
        ///         </code>
        ///     </example>
        ///     <para>
        ///         Note that <i>SubCommand</i>s may be any <c>ICommand</c> implementor,
        ///         <c>MacroCommand</c>s or <c>SimpleCommands</c> are both acceptable.
        ///     </para>
        /// </remarks>
        protected virtual void InitializeMacroCommand()
        {
        }

        /// <summary>
        /// Add a <c>SubCommand</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The <i>SubCommands</i> will be called in First In/First Out (FIFO)
        ///         order.
        ///     </para>
        /// </remarks>
        /// <param name="commandFunc">a reference to the <c>FuncDelegate</c> of the <c>ICommand</c>.</param>
        protected void AddSubCommand(Func<ICommand> commandFunc)
        {
            subcommands.Add(commandFunc);
        }

        /// <summary>
        /// Execute this <c>MacroCommand</c>'s <i>SubCommands</i>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The <i>SubCommands</i> will be called in First In/First Out (FIFO)
        ///         order.
        ///     </para>
        /// </remarks>
        /// <param name="notification">the <c>INotification</c> object to be passsed to each <i>SubCommand</i>.</param>
        public virtual void Execute(INotification notification)
        {
            while(subcommands.Count > 0)
            {
                Func<ICommand> commandFunc = subcommands[0];
                ICommand commandInstance = commandFunc();
                commandInstance.Execute(notification);
                subcommands.RemoveAt(0);
            }
        }

        /// <summary>List of subcommands</summary>
        public IList<Func<ICommand>> subcommands;
    }
}
