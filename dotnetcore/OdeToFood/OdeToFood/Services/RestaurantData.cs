using OdeToFood.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newrestaurant);

        void Commit();
    }
    public class SQLRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SQLRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            Commit();
            return newRestaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r=> r.Id==id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
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
        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r=>r.Id==id);
            // firstordefault -if it doesnt find a value that matches the predicate it returns a default value that is a null refrence

        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);
            return newRestaurant;
            
        }

        public void Commit()
        {
            //.......no opr

        }

        static List<Restaurant> _restaurants;
        //this list collection is not thread safe
    }
}
