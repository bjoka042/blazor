using System;
using System.Collections.Generic;
using System.Text;
using AnotherLibrary.Providers;
using Microsoft.Extensions.DependencyInjection;
using Web.Core;

namespace AnotherLibrary
{
    public class AnotherLibraryFeature : IFeature
    {
        public string Name => "AnotherLibrary";

        public void Register(IServiceCollection services)
        {
            services.AddTransient<ITextProvider, TextProvider>();
            //services.AddControllers();
            //services.AddControllersWithViews();
        }
    }
}
