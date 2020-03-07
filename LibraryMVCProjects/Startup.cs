using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryMVCProjects
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
			services.AddAuthorization(options => { options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin")); });

			services.AddMvc();
			services.AddRazorPages().AddRazorPagesOptions(op =>
			{
				op.Conventions.AllowAnonymousToFolder("/AuthorMVC");
				op.Conventions.AuthorizePage("/AuthorMVC/Detail", "RequireAdministratorRole");
			});
			services.AddDbContextPool<LibraryDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connStr")));
			services.AddScoped<IAuthor, AuthorDataSql>();
			services.AddScoped<IBook, BookDataSql>();
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				
			}

			
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllerRoute(
					name: "author",
					pattern: "{controller=Home}/{action=Index}/{Id?}");
				endpoints.MapControllerRoute(
					name: "book",
					pattern: "{controller=Home}/{action=Index}/{Id?}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{Id?}");
			});
		}
	}
}
