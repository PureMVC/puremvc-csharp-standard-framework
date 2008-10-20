/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using NUnitLite;
using NUnit.Framework;

using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Notification class.
	 * 
	 * @see org.puremvc.patterns.observer.Notification Notification
	 */
    [TestFixture]
    public class NotificationTest : TestCase
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public NotificationTest(string methodName) 
            : base(methodName)
        { }

        /**
         * Create the TestSuite.
         */
        public static ITest Suite
        {
            get
            {
                TestSuite ts = new TestSuite(typeof(NotificationTest));

                ts.AddTest(new NotificationTest("testNameAccessors"));
                ts.AddTest(new NotificationTest("testBodyAccessors"));
                ts.AddTest(new NotificationTest("testConstructor"));
				ts.AddTest(new NotificationTest("testToString"));

                return ts;
            }
        }

        /**
  		 * Tests setting and getting the name using Notification class accessor methods.
  		 */
  		public void testNameAccessors()
        {
			// Create a new Notification and use accessors to set the note name 
   			INotification note = new Notification("TestNote");
   			
   			// test assertions
			Assert.True(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
   		}

        /**
  		 * Tests setting and getting the body using Notification class accessor methods.
  		 */
  		public void testBodyAccessors()
        {
			// Create a new Notification and use accessors to set the body
   			INotification note = new Notification(null);
   			note.Body = 5;
   			
   			// test assertions
			Assert.True((int) note.Body == 5, "Expecting (int) note.Body == 5");
   		}

        /**
  		 * Tests setting the name and body using the Notification class Constructor.
  		 */
  		public void testConstructor()
        {
			// Create a new Notification using the Constructor to set the note name and body
   			INotification note = new Notification("TestNote", 5, "TestNoteType");
   			
   			// test assertions
			Assert.True(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
			Assert.True((int) note.Body == 5, "Expecting (int) note.Body == 5");
   			Assert.True(note.Type == "TestNoteType", "Expecting note.Type == 'TestNoteType'");
   		}
   		
  		/**
  		 * Tests the toString method of the notification
  		 */
  		public void testToString() {

			// Create a new Notification and use accessors to set the note name 
   			INotification note = new Notification("TestNote", "1,3,5", "TestType");
   			string ts = "Notification Name: TestNote\nBody:1,3,5\nType:TestType";
   			
   			// test assertions
			Assert.True(note.ToString() == ts, "Expecting note.testToString() == '" + ts + "'");
   		}
    }
}
