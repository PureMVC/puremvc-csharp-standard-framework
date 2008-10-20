/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

using PureMVC.Interfaces;
using PureMVC.Patterns;

#endregion

namespace PureMVC.Patterns
{
    /// <summary>
    /// A base <c>ICommand</c> implementation
    /// </summary>
    /// <remarks>
    ///     <para>Your subclass should override the <c>execute</c> method where your business logic will handle the <c>INotification</c></para>
    /// </remarks>
	/// <see cref="PureMVC.Core.Controller"/>
	/// <see cref="PureMVC.Patterns.Notification"/>
	/// <see cref="PureMVC.Patterns.MacroCommand"/>
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
		#region Public Methods

		#region ICommand Members

		/// <summary>
        /// Fulfill the use-case initiated by the given <c>INotification</c>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to handle</param>
        /// <remarks>
        ///     <para>In the Command Pattern, an application use-case typically begins with some user action, which results in an <c>INotification</c> being broadcast, which is handled by business logic in the <c>execute</c> method of an <c>ICommand</c></para>
        /// </remarks>
		public virtual void Execute(INotification notification)
		{
		}

		#endregion

		#endregion
	}
}
