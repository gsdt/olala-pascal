using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace FastColoredTextBoxNS
{
    public class SyntaxDescriptor: IDisposable
    {
        private Dictionary<string, Style> type = new Dictionary<string, Style>();
        public Dictionary<string, Style> Type
        {
            get { return type; }
            set { type = value; }
        }

        public System.Drawing.Font font { get; set; }

        public void Dispose()
        {
            type.Clear();
            font.Dispose();
        }
    }
}
