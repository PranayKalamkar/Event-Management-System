using Event_Management_App.Extension;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Event_Management_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.AddControllers(options =>
            {
                options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
            });

            builder.Services.AddAppSetting();

            builder.Services.AddSession();

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(1);
            //});

            //builder.Services.AddMvc();

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(5);
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseSession();

            //app.UseMvcWithDefaultRoute();


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}");
        //pattern: "{controller=AddEvent}/{action=AddEvent}/{id?}");
        pattern: "{controller=User}/{action=Login}/{id?}");
            //pattern: "{controller=User}/{action=SignUp}/{id?}");
            //pattern: "{controller=AdminDashboard}/{action=Dashboard}/{id?}");
            //pattern: "{controller=UserPage}/{action=UserPage}/{id?}");


            app.Run();
        }
    }
}
