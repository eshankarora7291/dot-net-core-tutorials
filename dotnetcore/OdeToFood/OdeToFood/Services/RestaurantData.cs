using OdeToFood.Entities;
using System.Collections.Generic;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {           
            _restaurants = new List<Restaurant>
            {
                new Restaurant {Id=1,Name="Sheraton"},
                new Restaurant {Id=2,Name="The Taj"},
                new Restaurant {Id=3,Name="Seven Seasons"}

            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants; 
        }
        List<Restaurant> _restaurants;
        //this list collection is not thread safe
    }
}
