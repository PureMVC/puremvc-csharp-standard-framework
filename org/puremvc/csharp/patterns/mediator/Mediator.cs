/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.facade;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.mediator
{
    /// <summary>
    /// A base <c>IMediator</c> implementation
    /// </summary>
    /// <see cref="org.puremvc.csharp.core.view.View"/>
    public class Mediator : Notifier, IMediator, INotifier
    {
        /// <summary>
        /// The name of the <c>Mediator</c>
        /// </summary>
        /// <remarks>
        ///     <para>Typically, a <c>Mediator</c> will be written to serve one specific control or group controls and so, will not have a need to be dynamically named</para>
        /// </remarks>
		public const String NAME = "Mediator";
		
        /// <summary>
        /// Constructs a new mediator with the default name and no view component
        /// </summary>
        public Mediator()
            : this(NAME, null)
        { }

        /// <summary>
        /// Constructs a new mediator with the specified name and no view component
        /// </summary>
        /// <param name="mediatorName">The name of the mediator</param>
        public Mediator(String mediatorName)
            : this(mediatorName, null)
        { }

        /// <summary>
        /// Constructs a new mediator with the specified name and view component
        /// </summary>
        /// <param name="mediatorName">The name of the mediator</param>
        /// <param name="viewComponent">The view component to be mediated</param>
		public Mediator(String mediatorName, Object viewComponent)
		{
			this.mediatorName = (mediatorName != null) ? mediatorName : NAME;
			this.viewComponent = viewComponent;
		}

        /// <summary>
        /// Get the name of the <c>Mediator</c>
        /// </summary>
        /// <returns>The Mediator name</returns>
        /// <remarks><para>You should override this in your subclass</para></remarks>
		public virtual String getMediatorName()
		{
            return mediatorName;
		}

		/// <summary>
		/// Set the <code>IMediator</code>'s view component.
		/// </summary>
        /// <param name="viewComponent">The view component</param>
		public void setViewComponent(Object viewComponent)
		{
			this.viewComponent = viewComponent;
		}

        /// <summary>
        /// Get the <c>Mediator</c>s view component
        /// </summary>
        /// <returns>The view component</returns>
        /// <remarks>
        ///     <para>Additionally, an implicit getter will usually be defined in the subclass that casts the view object to a type, like this:</para>
        ///     <example>
        ///         <code>
        ///             private System.Windows.Form.ComboBox comboBox {
        ///                 get { return viewComponent as ComboBox; }
        ///             }
        ///         </code>
        ///     </example>
        /// </remarks>
        public virtual Object getViewComponent()
		{	
			return viewComponent;
		}

        /// <summary>
        /// List the <c>INotification</c> names this <c>Mediator</c> is interested in being notified of
        /// </summary>
        /// <returns>The list of <c>INotification</c> names </returns>
        public virtual IList listNotificationInterests()
		{
            return new ArrayList();
		}

        /// <summary>
        /// Handle <c>INotification</c>s
        /// </summary>
        /// <param name="notification">The <c>INotification</c> instance to handle</param>
        /// <remarks>
        ///     <para>
        ///        Typically this will be handled in a switch statement, with one 'case' entry per <c>INotification</c> the <c>Mediator</c> is interested in. 
        ///     </para>
        /// </remarks>
        public virtual void handleNotification(INotification notification)
        { }
		
		/// <summary>
		/// Called by the View when the Mediator is registered
		/// </summary>
		public virtual void onRegister()
		{ }

		/// <summary>
		/// Called by the View when the Mediator is removed
		/// </summary>
		public virtual void onRemove()
		{ }

        /// <summary>
        /// The mediator name
        /// </summary>
        protected String mediatorName;

        /// <summary>
        /// The view component being mediated
        /// </summary>
        protected Object viewComponent;
    }
}
