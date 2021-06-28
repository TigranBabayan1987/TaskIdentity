using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config => {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            })
              .AddCookie("Cookie")
              .AddOpenIdConnect("oidc", config => {
                  config.Authority = "https://localhost:44325/";
                  config.ClientId = "client_id_mvc";
                  config.ClientSecret = "client_secret_mvc";
                  config.SaveTokens = true;
                  config.ResponseType = "code";
                  config.SignedOutCallbackPath = "/Home/Index";

                    // two trips to load claims in to the cookie
                    // but the id token is smaller !
                    config.GetClaimsFromUserInfoEndpoint = true;

                    // configure scope
                    config.Scope.Clear();
                  config.Scope.Add("openid");
                  config.Scope.Add("rc.scope");

              });

            services.AddHttpClient();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
