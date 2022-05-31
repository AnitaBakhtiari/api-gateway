using APIGateway;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");


const string authenticationProviderKey = "Key";

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = false;
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = bool.Parse(builder.Configuration["Authentication:ValidateAudience"])
        };
        options.SaveToken = true;
    });

builder.Services.AddOcelot().AddPollyWithInternalServerErrorHandling().AddEureka();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app
    .UseAuthentication()
    .UseOcelot()
    .Wait();


app.Run();