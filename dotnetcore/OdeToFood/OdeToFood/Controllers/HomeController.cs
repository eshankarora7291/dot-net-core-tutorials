using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetGreeting();
            return View(model);
        } 
        public IActionResult Details(int id)
        {
            var model = GetModel(id);
            if (model == null)
            { //return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Entities.Restaurant GetModel(int id)
        {
            return _restaurantData.Get(id);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if(model==null)
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);
            if(ModelState.IsValid)
            {
                restaurant.Cuisine = model.Cuisine;
                restaurant.Name = model.Name;
                _restaurantData.Commit();
                return RedirectToAction("Details",new { id =restaurant.Id});

            }
            return View(restaurant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;
                newRestaurant = _restaurantData.Add(newRestaurant);
                // return View("Details", newRestaurant);
                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }
            else
            {
                return View();
            }

        }

    }
} 
