using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GolfCourseManager.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using GolfCourseManager.ViewModels;
using Microsoft.AspNet.Mvc;

namespace GolfCourseManager
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public static IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<GCMContext>();

			services.AddMvc(config =>
			{
			})
			.AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

			services.AddIdentity<Member, IdentityRole>(config =>
			{
				config.User.RequireUniqueEmail = true;
				config.Password.RequiredLength = 8;
				config.Password.RequireDigit = true;
				config.Cookies.ApplicationCookie.LoginPath = "/UserManagement/Login";
			})
			.AddEntityFrameworkStores<GCMContext>();

			// Add application services.
			services.AddScoped<GCMRepository>();
			services.AddTransient<GCMContextSeedData>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, GCMContextSeedData seedData)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				// For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
				try
				{
					using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
						.CreateScope())
					{
						serviceScope.ServiceProvider.GetService<GCMContext>()
							 .Database.Migrate();
					}
				}
				catch { }
			}

			app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

			app.UseStaticFiles();

			app.UseIdentity();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			Mapper.Initialize(config => {
				config.CreateMap<Member, MemberViewModel>().ReverseMap();
				config.CreateMap<TeeTime, ReserveViewModel>().ReverseMap();
			});

			await seedData.EnsureSeedData();
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
