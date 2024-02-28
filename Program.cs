using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models;
using System;
using System.Security.Claims;

namespace SocialMediaPlatform
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			//builder.Services.AddIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			//	.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
				options.SignIn.RequireConfirmedAccount =
				false).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddRazorPages();

			builder.Services.AddControllersWithViews();

			builder.Services.Configure<IdentityOptions>(options =>
			{

				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
				options.Lockout.MaxFailedAccessAttempts = 50;
				options.Lockout.AllowedForNewUsers = false;

				//options.User.AllowedUserNameCharacters = "abc";
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;


			});


			builder.Services.AddTransient<DataAccessLayer<Profile>, ProfileList>();
			builder.Services.AddTransient<DataAccessLayer<Post>, PostList>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
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

			app.UseAuthentication();
			app.UseAuthorization();


			//pattern: "{controller=Media}/{action=MyPage}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Media}/{action=MyPage}/{id?}");



			app.MapRazorPages();
			app.Run();
		}
	}
}



///TODO:
/*
There should be a way to go back to the last profile you were just on. 
Landing Page



*/
