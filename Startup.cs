using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using AspNetMaker2019.Models;
using AspNetMaker2019.Controllers;
using static AspNetMaker2019.Models.SampleProject;

// Project
namespace AspNetMaker2019
{
	public class Startup
	{
		private readonly ILogger<Startup> _logger;
		public Startup(ILogger<Startup> logger, IConfiguration configuration)
		{
			_logger = logger;
			Configuration = configuration;
		}
		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// Memory cache
			services.AddMemoryCache();

			// Add framework services
			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

			// Add HttpContext accessor
			services.AddHttpContextAccessor();

			// Adds a default in-memory implementation of IDistributedCache.
			services.AddDistributedMemoryCache();

			// Session
			services.AddSession(options => {
				options.Cookie.Name = ".SampleProject.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(Config.SessionTimeout);
			});

			// JWT
			var tokenValidationParameters = new TokenValidationParameters
			{

				// Token signature will be verified using a private key.
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),

				// Token will only be valid if contains below domain (e.g http://localhost) for "iss" claim.
				ValidateIssuer = true,
				ValidIssuer = Configuration["Jwt:Issuer"],

				// Token will only be valid if contains below domain (e.g http://localhost) for "aud" claim.
				ValidateAudience = true,
				ValidAudience = Configuration["Jwt:Audience"],

				// Token will only be valid if not expired yet, with 5 minutes clock skew.
				ValidateLifetime = true
			};

			// Authentication
			services.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = "default";
			})
			.AddPolicyScheme("default", "Authorization Bearer or Cookies", options => {
				options.ForwardDefaultSelector = context =>
				{
					if (IsApi() && !IsLoggedIn())
						return JwtBearerDefaults.AuthenticationScheme;
					return CookieAuthenticationDefaults.AuthenticationScheme;
				};
			})
			.AddCookie(options => {
				options.ExpireTimeSpan = TimeSpan.FromMinutes(Config.SessionTimeout);
			})
			.AddJwtBearer(options => {
				options.TokenValidationParameters = tokenValidationParameters;
			});

			// HTTP context accessor
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			// Configure supported cultures and localization options
			services.Configure<RequestLocalizationOptions>(options =>
			{

				// State what the default culture for your application is. This will be used if no specific culture
				// can be determined for a given request.

				options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

				// You must explicitly state which cultures your application supports.
				// These are the cultures the app supports for formatting numbers, dates, etc.

				options.SupportedCultures = new[]
				{
					new CultureInfo("en-US")
				};
			});

			// CORS
			services.AddCors(options =>
			{

				// CORS policy
				options.AddPolicy("CorsPolicy", builder => {
					builder.WithOrigins(GetOrigins(Configuration["Cors:AccessControlAllowOrigin"]))
						.AllowAnyHeader().AllowCredentials();
				});

				// API CORS policy
				options.AddPolicy("ApiCorsPolicy", builder => {
					builder.WithOrigins(GetOrigins(Configuration["Cors:ApiAccessControlAllowOrigin"]))
						.AllowAnyHeader().AllowCredentials();
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor)
		{
			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				if (Config.Debug) {
					app.UseDeveloperExceptionPage();
				} else {
					app.UseExceptionHandler("/Home/Error");
				}
			}
			app.UseStaticFiles(FileOptions);
			SampleProject.Configure(httpContextAccessor, env, Configuration, antiforgery);
			app.UseSession(); // IMPORTANT: MUST be before UseMvc()
			app.UseAuthentication();
			app.UseCors("CorsPolicy");
			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
