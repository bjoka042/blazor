#pragma checksum "D:\Repos\blazor\src\Components\Pages\Dynamic1.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dda7a5a43d439c6065e88a3346f7d3e16cf0b589"
// <auto-generated/>
#pragma warning disable 1591
namespace Components.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "D:\Repos\blazor\src\Components\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
    [Microsoft.AspNetCore.Components.RouteAttribute("/dynamic1")]
    public class Dynamic1 : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Dynamic1</h3>\r\n\r\nTime: ");
            __builder.AddContent(1, 
#line 5 "D:\Repos\blazor\src\Components\Pages\Dynamic1.razor"
       Time

#line default
#line hidden
            );
        }
        #pragma warning restore 1998
#line 7 "D:\Repos\blazor\src\Components\Pages\Dynamic1.razor"
       
    private static string Time { get; set; } = DateTime.Now.ToString();

#line default
#line hidden
    }
}
#pragma warning restore 1591