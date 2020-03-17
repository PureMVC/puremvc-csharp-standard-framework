//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Core
{
    /// <summary>
    /// A Notification class used by ViewTest.
    /// </summary>
    /// <seealso cref="ViewTest"/>
    public class ViewTestNote : Notification
    {
        public const string NAME = "ViewTestNote";

        public ViewTestNote(object body) : base(NAME, body)
        {
        }

        public static INotification Create(object body)
        {
            return new ViewTestNote(body);
        }
    }
}
