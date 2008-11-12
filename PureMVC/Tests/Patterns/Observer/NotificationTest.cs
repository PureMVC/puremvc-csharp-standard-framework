/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/
using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Tests.Util;

namespace PureMVC.Tests.Patterns
{
    /**
	 * Test the PureMVC Notification class.
	 * 
	 * @see org.puremvc.patterns.observer.Notification Notification
	 */
    [TestClass]
    public class NotificationTest : BaseTest
    {
        /**
  		 * Constructor.
  		 * 
  		 * @param methodName the name of the test method an instance to run
  		 */
        public NotificationTest() 
        { }

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

        /**
  		 * Tests setting and getting the name using Notification class accessor methods.
  		 */
		[TestMethod]
		[Description("Notification Tests")]
		public void NameAccessors()
        {
			// Create a new Notification and use accessors to set the note name 
   			INotification note = new Notification("TestNote");
   			
   			// test assertions
			Assert.IsTrue(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
   		}

        /**
  		 * Tests setting and getting the body using Notification class accessor methods.
  		 */
		[TestMethod]
		[Description("Notification Tests")]
		public void BodyAccessors()
        {
			// Create a new Notification and use accessors to set the body
   			INotification note = new Notification(null);
   			note.Body = 5;
   			
   			// test assertions
			Assert.IsTrue((int) note.Body == 5, "Expecting (int) note.Body == 5");
   		}

        /**
  		 * Tests setting the name and body using the Notification class Constructor.
  		 */
		[TestMethod]
		[Description("Notification Tests")]
		public void TestConstructor()
        {
			// Create a new Notification using the Constructor to set the note name and body
   			INotification note = new Notification("TestNote", 5, "TestNoteType");
   			
   			// test assertions
			Assert.IsTrue(note.Name == "TestNote", "Expecting note.Name == 'TestNote'");
			Assert.IsTrue((int) note.Body == 5, "Expecting (int) note.Body == 5");
   			Assert.IsTrue(note.Type == "TestNoteType", "Expecting note.Type == 'TestNoteType'");
   		}
   		
  		/**
  		 * Tests the toString method of the notification
  		 */
		[TestMethod]
		[Description("Notification Tests")]
		public void TestToString()
		{

			// Create a new Notification and use accessors to set the note name 
   			INotification note = new Notification("TestNote", "1,3,5", "TestType");
   			string ts = "Notification Name: TestNote\nBody:1,3,5\nType:TestType";
   			
   			// test assertions
			Assert.IsTrue(note.ToString() == ts, "Expecting note.testToString() == '" + ts + "'");
   		}
    }
}
