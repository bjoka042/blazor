using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnotherLibrary.Models.Test;
using AnotherLibrary.Providers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnotherLibrary.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ITextProvider _textProvider;

        public TestController(ITextProvider textProvider)
        {
            _textProvider = textProvider;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //return View(new TextModel());
            var model = new TextModel() { Text = _textProvider.GetText() };
            return View(model);
        }
    }
}
