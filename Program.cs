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
// https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/signalr/real-time-web-apps?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/dependency-injection?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
// https://learn.microsoft.com/en-us/dotnet/core/testing/testing-with-moq
// https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0
// https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-6.0




namespace ST10348753_PROG6212POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=HomePage}/{id?}");

            app.Run();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...------------------------------------------------------//
