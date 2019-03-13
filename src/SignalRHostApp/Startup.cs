using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using MultiAuthority.AccessTokenValidation;
using SignalRHostApp.Hubs;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SignalRHostApp
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserIdProvider, SignalRUserProvider>();
            services.AddSingleton<ConnectionMapping<string>>();
            services.AddOptions();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    corsBuilder => corsBuilder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            var signalServiceBuilder = services.AddSignalR().AddAzureSignalR();
           

            services.AddHostedService<Worker>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            List<SchemeRecord> schemeRecords = new List<SchemeRecord>()
            {  new SchemeRecord()
                {
                    Name = "One",
                    JwtBearerOptions = options =>
                    {
                        options.Authority = "https://p7identityserver4.azurewebsites.net";
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Query["access_token"];

                                // If the request is for our hub...
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/ChatHub")))
                                {
                                    // Read the token out of the query string
                                    context.Token = accessToken;
                                }
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                return Task.CompletedTask;
                            }
                        };
                    }

                },
            };

            services.AddAuthentication("Bearer")
                .AddMultiAuthorityAuthentication(schemeRecords);

            services.AddHttpContextAccessor(); services.AddHttpContextAccessor();
           
            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            /*
            app.Use(async (context, next) =>
            {
                var x = context.Request.Path.Value;
                if (x.StartsWith("/chatHub/", StringComparison.CurrentCultureIgnoreCase))
                {
                    StringValues value;
                    if (context.Request.Query.TryGetValue("access_token", out value))
                    {
                        context.Request.Headers.Add("Authorization", new string[] { "bearer " + value });
                        if (context.Request.Query.TryGetValue("x-authScheme", out value))
                        {
                            context.Request.Headers.Add("x-authScheme", new string[] { value });
                        }
                    }


                }
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });
            */
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseFileServer();
            app.UseCookiePolicy();


            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<ClockHub>("/hubs/clock");
                routes.MapHub<ChatHub>("/chatHub");
            });
          
            app.UseMvc();
        }
    }
}
