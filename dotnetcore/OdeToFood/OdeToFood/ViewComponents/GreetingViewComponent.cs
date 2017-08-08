using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private IGreeter _greeting;

        public GreetingViewComponent(IGreeter greeting)
        {
            _greeting = greeting;
        }
        public IViewComponentResult Invoke()
        {
            var model = _greeting.GetGreeting();
            return View("Default",model);
        }
    }
}
