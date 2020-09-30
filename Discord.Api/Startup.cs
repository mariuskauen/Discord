using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using Discord.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Discord.Core.Helpers;
//using Discord.Hubs;
using Microsoft.Extensions.Options;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Discord.Api.Components;
using Discord.Api.Data.Repositories;
using Discord.Api.Components.User;
using Discord.Api.Components.Auth;
using Discord.Api.Components.Mediator;

namespace Discord.Api
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
            services.Configure<MongoSettings>(
                Configuration.GetSection(nameof(MongoSettings)));

            services.AddSingleton<IMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoSettings>>().Value);

            services.AddSignalR();
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddScoped<AuthService>();
            services.AddScoped<QueryRepository>();
            services.AddScoped<CommandRepository>();
            services.AddScoped<UserService>();
            //services.AddScoped<RelationService>();
            //services.AddScoped<ServerService>();
            //services.AddScoped<LoadService>();
            services.AddScoped<MapConfig>();
            //services.AddScoped<ChannelService>();
            //services.AddScoped<ConversationService>();
            services.AddScoped<MediatorService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/discordhub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseCors(builder =>
    builder.WithOrigins("https://localhost:44318/", "https://10.0.0.185:45460")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<DiscordHub>("/discordhub");
            });
        }
    }
}
