//
//  PureMVC C# Standard
//
//  Copyright(c) 2020 Saad Shams <saad.shams@puremvc.org>
//  Your reuse is governed by the Creative Commons Attribution 3.0 License
//

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    /// <summary>
    /// Test the PureMVC Notification class.
    /// </summary>
    /// <seealso cref="Notification"/>
    [TestClass]
    public class NotificationTest
    {
        /// <summary>
        /// Tests setting and getting the name using Notification class accessor methods.
        /// </summary>
        [TestMethod]
        public void TestNameAccessors()
        {
            // // Create a new Notification and use accessors to set the note name 
            var note = new Notification("TestNote");

            // test assertions
            Assert.IsTrue(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
        }

        /// <summary>
        /// Tests setting and getting the body using Notification class accessor methods.
        /// </summary>
        [TestMethod]
        public void TestBodyAccessors()
        {
            // Create a new Notification and use accessors to set the body
            var note = new Notification(null);
            note.Body = 5;

            // test assertions
            Assert.IsTrue((int)note.Body == 5, "Expecting (int) note.Body == 5");
        }

        /// <summary>
        /// Tests setting the name and body using the Notification class Constructor.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            // Create a new Notification using the Constructor to set the notification name and body
            var note = new Notification("TestNote", 5, "TestNoteType");

            // test assertions
            Assert.IsTrue(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
            Assert.IsTrue((int)note.Body == 5, "Expecting (int) note.Body == 5");
            Assert.IsTrue(note.Type == "TestNoteType", "Expecting note.Type == 'TestNoteType'");
        }

        /// <summary>
        /// Tests the toString method of the notification
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // Create a new Notification and use accessors to set the notification name 
            var note = new Notification("TestNote", "1,3,5", "TestType");
            const string ts = "Notification Name: TestNote\nBody:1,3,5\nType:TestType";

            // test assertions
            Assert.IsTrue(note.ToString() == ts, "Expecting note.TestToString() == '" + ts + "'");
        }
    }
}
