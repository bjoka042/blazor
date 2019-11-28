using Plugin1.Providers;
using Microsoft.AspNetCore.Components;

namespace Plugin1.Components
{
    public class Plugin1ComponentBase : ComponentBase
    {
        [Inject] private ITextProvider TextProvider { get; set; }

        public string GetSomeText()
        {
            return TextProvider.GetText();
        }
    }
}
