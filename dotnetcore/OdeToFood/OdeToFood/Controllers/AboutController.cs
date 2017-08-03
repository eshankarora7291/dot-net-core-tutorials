using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    //ATTRIBUTE BASED ROUTING
    [Route("[controller]/[action]")]
    public class AboutController
    {
        //another way to define routes is to use attributes directly on the controller
       // [Route("[action]")]
        public string Phone()
        {
            return "+91 - 85577 - 56789";
        }
        //[Route("address")]
        public string Address()
        {
            return "Delhi meri jaan";
        }
    }
}
