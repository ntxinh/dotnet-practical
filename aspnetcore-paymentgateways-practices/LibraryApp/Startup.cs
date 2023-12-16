using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Data.Models;
using LibraryApp.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBraintreeService, BraintreeService>();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(new Guid().ToString()));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            var scope = app.ApplicationServices.CreateScope();
            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            SeedDatabase(context);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Books}/{action=Index}/{id?}");
            });
        }

        private static void SeedDatabase(AppDbContext context)
        {
            context.Database.EnsureCreated();

            var _books = new List<Book>()
            {
                new Book()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Title="Managing Oneself",
                    Description="We live in an age of unprecedented opportunity: with ambition, drive, and talent, you can rise to the top of your chosen profession, regardless of where you started out...",
                    Author= "Peter Ducker",
                    Thumbnail = "https://devyscope.com/linkedin-learning/payment-gateways/book-covers/managing-oneself.png",
                    Price = "19.90"
                },
                new Book()
                {
                    Id= new Guid("117366b8-3541-4ac5-8732-860d698e26a2"),
                    Title="Evolutionary Psychology",
                    Description="Evolutionary Psychology: The New Science of the Mind, 5th edition provides students with the conceptual tools of evolutionary psychology, and applies them to empirical research...",
                    Author= "David Buss",
                    Thumbnail = "https://devyscope.com/linkedin-learning/payment-gateways/book-covers/evoluationary-psychology.png",
                    Price = "29.90"
                },
                new Book()
                {
                    Id= new Guid("66ff5116-bcaa-4061-85b2-6f58fbb6db25"),
                    Title="How to Win Friends & Influence People",
                    Description="Millions of people around the world have improved their lives based on the teachings of Dale Carnegie. In How to Win Friends and Influence People, he offers practical advice...",
                    Author= "Dale Carnegie",
                    Thumbnail = "https://devyscope.com/linkedin-learning/payment-gateways/book-covers/win-friends-influence-people.png",
                    Price = "32.49"
                },
                new Book()
                {
                    Id =  new Guid("cd5089dd-9754-4ed2-b44c-488f533243ef"),
                    Title = "The Selfish Gene",
                    Description = "Professor Dawkins articulates a gene’s eye view of evolution. A view giving center stage to these persistent units of information, and in which organisms can be seen as...",
                    Author = "Richard Dawkins",
                    Thumbnail = "https://devyscope.com/linkedin-learning/payment-gateways/book-covers/the-selfish-gene.png",
                    Price = "17.89"
                },
                new Book()
                {
                    Id =  new Guid("d81e0829-55fa-4c37-b62f-f578c692af78"),
                    Title = "The Lessons of History",
                    Description = "Will and Ariel Durant have succeeded in distilling for the reader the accumulated store of knowledge and experience from their five decades of work on the eleven monumental...",
                    Author = "Will & Ariel Durant",
                    Thumbnail = "https://devyscope.com/linkedin-learning/payment-gateways/book-covers/the-lessons-of-history.png",
                    Price = "32.00"
                }
            };

            context.Books.AddRange(_books);
            context.SaveChanges();
        }
    }
}
