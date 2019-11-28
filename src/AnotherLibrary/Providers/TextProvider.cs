using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherLibrary.Providers
{
    public class TextProvider : ITextProvider
    {
        public string GetText()
        {
            return "Hello from TextProvider";
        }
    }
}
