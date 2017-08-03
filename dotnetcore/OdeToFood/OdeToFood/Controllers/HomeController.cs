using Microsoft.AspNetCore.Mvc;
 using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController:Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }
        //IActionRsult is a formal way to encapsulate the decision of the controller.
        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            return View(model);
        } 
        public string Details(int id)
        {
            return id.ToString();
        }
    }
} 
