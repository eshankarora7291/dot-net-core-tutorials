using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
//using Microsoft.AspNetCore.StaticFiles;

namespace OdeToFood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //to configure components in my application.
            //dependency injection 
            services.AddMvc();
            services.AddSingleton(Configuration);   
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            //anytime u need  Irestaurantdata use inmemoryrestaurantdata
            //scoped means one instance of this service for each HTTP request.
        }

        // This method gets called by the runtime.
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
            //IServiceCollection )
        {//This is where i will build my http pipeline,this defines how my application will respond to incoming requests.

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Oops!!")
                });
            }
            //app.UseFileServer();
            //app.UseDefaultFiles();    
             app.UseStaticFiles();
            // app.UseMiddleware<StaticFileMiddleware>(new StaticFileOptions());
            app.UseMvc(ConfigureRoutes);
            // app.UseMvcWithDefaultRoute();
            //this middleware is going to look at an incoming http request and will try to map
            //that request to a method on a C# class and what the mvc framework will do is instantiate
            //a class,invoke a method and that method will tellthe mvc framework what to do next.
            app.Run(ctx => ctx.Response.WriteAsync("not found"));
        }
        //CONVENTION BASED ROUTING
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index
            // ? means optional
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            // if u dont find a controller ,the default controller should be home,default action,index.

        }
    }
}
