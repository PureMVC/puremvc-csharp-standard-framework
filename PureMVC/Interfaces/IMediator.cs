/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;
using System.Collections.Generic;

#endregion

namespace PureMVC.Interfaces
{
    /// <summary>
    /// The interface definition for a PureMVC Mediator.
    /// </summary>
    /// <remarks>
    ///     <para>In PureMVC, <c>IMediator</c> implementors assume these responsibilities:</para>
    ///     <list type="bullet">
    ///         <item>Implement a common method which returns a list of all <c>INotification</c>s the <c>IMediator</c> has interest in.</item>
    ///         <item>Implement a common notification (callback) method</item>
    ///     </list>
    ///     <para>Additionally, <c>IMediator</c>s typically:</para>
    ///     <list type="bullet">
    ///         <item>Act as an intermediary between one or more view components such as text boxes or list controls, maintaining references and coordinating their behavior.</item>
    ///         <item>In Flash-based apps, this is often the place where event listeners are added to view components, and their handlers implemented</item>
    ///         <item>Respond to and generate <c>INotifications</c>, interacting with of the rest of the PureMVC app</item>
    ///     </list>
    ///     <para>When an <c>IMediator</c> is registered with the <c>IView</c>, the <c>IView</c> will call the <c>IMediator</c>'s <c>listNotificationInterests</c> method. The <c>IMediator</c> will return an <c>IList</c> of <c>INotification</c> names which it wishes to be notified about</para>
    ///     <para>The <c>IView</c> will then create an <c>Observer</c> object encapsulating that <c>IMediator</c>'s (<c>handleNotification</c>) method and register it as an Observer for each <c>INotification</c> name returned by <c>listNotificationInterests</c></para>
    ///     <para>A concrete IMediator implementor usually looks something like this:</para>
    ///     <example>
    ///         <code>
	///	using PureMVC.Patterns.~~;
	///	using PureMVC.Core.View.~~;
    /// 
    ///	using com.me.myapp.model.~~;
    ///	using com.me.myapp.view.~~;
    ///	using com.me.myapp.controller.~~;
    /// 		
    /// using System.Windows.Forms; 
    /// using System.Data;
    /// 
    /// public class MyMediator : Mediator, IMediator {
    /// 
    /// 		public MyMediator( viewComponent:object ) {
    /// 			base( viewComponent );
    ///             combo.DataSourceChanged += new EventHandler(onChange);
    /// 		}
    /// 		
    /// 		public IList listNotificationInterests() {
    /// 				return new string[] {
    ///                      MyFacade.SET_SELECTION, 
    ///                      MyFacade.SET_DATAPROVIDER };
    /// 		}
    /// 
    /// 		public void handleNotification( notification:INotification ) {
    /// 				switch ( notification.getName() ) {
    /// 					case MyFacade.SET_SELECTION:
    ///                         combo.SelectedItem = notification.getBody();
    /// 						break;
    ///                     // set the data source of the combo box
    /// 					case MyFacade.SET_DATASOURCE:
    /// 						combo.DataSource = notification.getBody();
    /// 						break;
    /// 				}
    /// 		}
    /// 
    /// 		// Invoked when the combo box dispatches a change event, we send a
    ///      // notification with the
    /// 		protected void onChange(object sender, EventArgs e) {
    /// 			sendNotification( MyFacade.MYCOMBO_CHANGED, sender );
    /// 		}
    /// 
    /// 		// A private getter for accessing the view object by class
    ///      private ComboBox combo {
    ///          get { return view as ComboBox; }
    ///      }
    /// 
    /// }
    ///         </code>
    ///     </example>
    /// </remarks>
	/// <see cref="PureMVC.Interfaces.INotification"/>
    public interface IMediator
	{
		/// <summary>
        /// Tthe <c>IMediator</c> instance name
        /// </summary>
		string MediatorName { get; }
		
        /// <summary>
        /// The <c>IMediator</c>'s view component
        /// </summary>
		object ViewComponent { get; set; }

		/// <summary>
        /// List <c>INotification interests</c>
        /// </summary>
        /// <returns>An <c>IList</c> of the <c>INotification</c> names this <c>IMediator</c> has an interest in</returns>
        IList<string> ListNotificationInterests();
		
        /// <summary>
        /// Handle an <c>INotification</c>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to be handled</param>
		void HandleNotification(INotification notification);
		
		/// <summary>
		/// Called by the View when the Mediator is registered
		/// </summary>
		void OnRegister();

		/// <summary>
		/// Called by the View when the Mediator is removed
		/// </summary>
		void OnRemove();
	}
}
