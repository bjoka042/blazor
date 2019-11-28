using AnotherLibrary.Providers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnotherLibrary
{
    public class TestComponentBase : ComponentBase
    {
        [Inject] private ITextProvider TextProvider { get; set; }

        public string GetSomeText()
        {
            return TextProvider.GetText();
        }
    }
}
