using System;
using System.IO;
using System.Windows.Controls;

namespace org.puremvc.csharp.tests
{
    public class TextBlockWriter : TextWriter
    {
        private readonly TextBlock textBlox;

        public TextBlockWriter(TextBlock textBlox)
        {
            this.textBlox = textBlox;
        }

        public override void Write(char value)
        {
            textBlox.Text += value;
        }

        public override void Write(string value)
        {
            textBlox.Text += value;
        }

        public override void WriteLine(string value)
        {
            textBlox.Text += value + Environment.NewLine;
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }
    }
}
