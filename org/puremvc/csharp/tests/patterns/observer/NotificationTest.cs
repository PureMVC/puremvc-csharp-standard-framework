/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using NUnitLite;
using NUnit.Framework;

using org.puremvc.csharp.interfaces;
using org.puremvc.csharp.patterns.observer;

namespace org.puremvc.csharp.patterns.observer
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
        public NotificationTest(String methodName) 
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
   			Assert.True(note.getName() == "TestNote", "Expecting note.getName() == 'TestNote'");
   		}

        /**
  		 * Tests setting and getting the body using Notification class accessor methods.
  		 */
  		public void testBodyAccessors()
        {
			// Create a new Notification and use accessors to set the body
   			INotification note = new Notification(null);
   			note.setBody(5);
   			
   			// test assertions
            Assert.True((int)note.getBody() == 5, "Expecting (int)note.getBody() == 5");
   		}

        /**
  		 * Tests setting the name and body using the Notification class Constructor.
  		 */
  		public void testConstructor()
        {
			// Create a new Notification using the Constructor to set the note name and body
   			INotification note = new Notification("TestNote", 5, "TestNoteType");
   			
   			// test assertions
   			Assert.True(note.getName() == "TestNote", "Expecting note.getName() == 'TestNote'");
   			Assert.True((int)note.getBody() == 5, "Expecting (int)note.getBody() == 5");
   			Assert.True(note.getType() == "TestNoteType", "Expecting note.getType() == 'TestNoteType'");
   		}
   		
  		/**
  		 * Tests the toString method of the notification
  		 */
  		public void testToString() {

			// Create a new Notification and use accessors to set the note name 
   			INotification note = new Notification("TestNote", "1,3,5", "TestType");
   			String ts = "Notification Name: TestNote\nBody:1,3,5\nType:TestType";
   			
   			// test assertions
			Assert.True(note.toString() == ts, "Expecting note.testToString() == '" + ts + "'");
   		}
    }
}
