// Zinedean Saaiman
// ST10348753
// PROG6212 - POE: PART 2
// Group: 2

// References:  
// https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
// https://getbootstrap.com/docs/5.1/getting-started/introduction/
// https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/dotnet/standard/class-libraries
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0

namespace ST10348753_PROG6212POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize the builder for configuring services and middleware
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the application
            builder.Services.AddControllersWithViews(); // Enables MVC pattern support (Controllers and Views)

            // Configure logging
            builder.Logging.ClearProviders(); // Removes default logging providers
            builder.Logging.AddConsole(); // Adds console logging for easier debugging

            // Build the WebApplication instance
            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                // Exception handling for production
                app.UseExceptionHandler("/Home/Error");

                // Enforce HTTPS with HSTS (HTTP Strict Transport Security)
                app.UseHsts(); // Default value is 30 days; modify if needed
            }

            // Middleware to redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Enable serving of static files (e.g., CSS, JS, images) from the "wwwroot" folder
            app.UseStaticFiles();

            // Add routing to the middleware pipeline
            app.UseRouting();

            // Add authorization middleware
            // (Authorization is currently not implemented but can be added in the future)
            app.UseAuthorization();

            // Configure the default routing for the application
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Start the application
            app.Run();
        }
    }
}
