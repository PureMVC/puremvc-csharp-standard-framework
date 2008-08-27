/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.command
{
    /// <summary>
    /// A base <c>ICommand</c> implementation that executes other <c>ICommand</c>s
    /// </summary>
    /// <remarks>
    ///     <para>A <c>MacroCommand</c> maintains an list of <c>ICommand</c> Class references called <i>SubCommands</i></para>
    ///     <para>When <c>execute</c> is called, the <c>MacroCommand</c> instantiates and calls <c>execute</c> on each of its <i>SubCommands</i> turn. Each <i>SubCommand</i> will be passed a reference to the original <c>INotification</c> that was passed to the <c>MacroCommand</c>'s <c>execute</c> method</para>
    ///     <para>Unlike <c>SimpleCommand</c>, your subclass should not override <c>execute</c>, but instead, should override the <c>initializeMacroCommand</c> method, calling <c>addSubCommand</c> once for each <i>SubCommand</i> to be executed</para>
    /// </remarks>
    /// <see cref="org.puremvc.csharp.core.controller.Controller"/>
    /// <see cref="org.puremvc.csharp.patterns.observer.Notification"/>
    /// <see cref="org.puremvc.csharp.patterns.command.SimpleCommand"/>
    public class MacroCommand : Notifier, ICommand, INotifier
    {
        private IList subCommands;
		
        /// <summary>
        /// Constructs a new macro command
        /// </summary>
        /// <remarks>
        ///     <para>You should not need to define a constructor, instead, override the <c>initializeMacroCommand</c> method</para>
        ///     <para>If your subclass does define a constructor, be sure to call <c>super()</c></para>
        /// </remarks>
		public MacroCommand()
		{
			subCommands = new ArrayList();
			initializeMacroCommand();			
		}

        /// <summary>
        /// Initialize the <c>MacroCommand</c>
        /// </summary>
        /// <remarks>
        ///     <para>In your subclass, override this method to initialize the <c>MacroCommand</c>'s <i>SubCommand</i> list with <c>ICommand</c> class references like this:</para>
        ///     <example>
        ///         <code>
        ///             // Initialize MyMacroCommand
        ///             protected override initializeMacroCommand( )
        ///             {
        ///                 addSubCommand( com.me.myapp.controller.FirstCommand );
        ///                 addSubCommand( com.me.myapp.controller.SecondCommand );
        ///                 addSubCommand( com.me.myapp.controller.ThirdCommand );
        ///             }
        ///         </code>
        ///     </example>
        ///     <para>Note that <i>SubCommand</i>s may be any <c>ICommand</c> implementor, <c>MacroCommand</c>s or <c>SimpleCommands</c> are both acceptable</para>
        /// </remarks>
		protected virtual void initializeMacroCommand()
		{ }

        /// <summary>
        /// Add a <i>SubCommand</i>
        /// </summary>
        /// <param name="commandType">A a reference to the <c>Type</c> of the <c>ICommand</c></param>
        /// <remarks>
        ///     <para>The <i>SubCommands</i> will be called in First In/First Out (FIFO) order</para>
        /// </remarks>
        protected void addSubCommand( Type commandType )
		{
            subCommands.Add(commandType);
		}
		
        /// <summary>
        /// Execute this <c>MacroCommand</c>'s <i>SubCommands</i>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> object to be passsed to each <i>SubCommand</i></param>
        /// <remarks>
        ///     <para>The <i>SubCommands</i> will be called in First In/First Out (FIFO) order</para>
        /// </remarks>
		public void execute(INotification notification)
		{
			while (subCommands.Count > 0) 
            {
                Type commandType = (Type)subCommands[0];
                Object commandInstance = Activator.CreateInstance(commandType);
                if (commandInstance is ICommand)
                {
                    ((ICommand)commandInstance).execute(notification);
                }
                subCommands.RemoveAt(0);
			}
		}
    }
}
