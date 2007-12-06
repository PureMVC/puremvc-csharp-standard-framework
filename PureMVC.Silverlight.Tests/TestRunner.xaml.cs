using System;
using System.Windows.Controls;

using NUnitLite.Runner;

namespace org.puremvc.csharp.tests
{
    public partial class TestRunner : Canvas
    {
        public void TestRunner_Loaded(object o, EventArgs e)
        {
            // Required to initialize variables
            InitializeComponent();

            try 
            {
                TextUI testRunner = new TextUI(new TextBlockWriter(resultsTextBlock));
                testRunner.Execute(new string[] {"PureMVC.Silverlight.Tests, Version=0.1.0.0"});
            }
            catch (Exception e1) {
                resultsTextBlock.Text += e1.Message;
            }
        }
    }
}
