using EventBus.Abstractions;
using EventBus.Realizations;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Shared.Auth;
using System.Reflection;
using System.Text;

namespace Shared.Extentions
{
    public static class CommonServiceCollectionExtentions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    var jwtConfig = configuration.GetSection("Audience");
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = jwtConfig["Iss"],
                        ValidateAudience = true,
                        ValidAudience = jwtConfig["Aud"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,

                    };
                });
            services.AddAuthorization();
            services.Configure<Audience>(configuration.GetSection("Audience"));

        }
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            var openApi = configuration.GetRequiredSection("OpenApi");
            services.AddSwaggerGen(options =>
            {
                var document = openApi.GetRequiredSection("Document");
                var version  = document.GetValue<string>("Version") ?? "v1";
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = document.GetValue<string>("Title"),
                    Version = version,
                    Description = document.GetValue<string>("Description")

                });
                
            });
        }
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var eventBusSection = configuration.GetSection("EventBus");
            if (!eventBusSection.Exists())
            {
                return;
            }
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = eventBusSection.GetValue<string>("SubscriptionClientName"),
                    DispatchConsumersAsync = true
                };
                if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
                {
                    factory.UserName = eventBusSection["UserName"];
                }

                if (!string.IsNullOrEmpty(eventBusSection["Password"]))
                {
                    factory.Password = eventBusSection["Password"];
                }

                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = eventBusSection.GetValue<string>("SubscriptionClientName");
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionManager>();
                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            });
            services.AddSingleton<IEventBusSubscriptionManager, InMemoryEventBusSubscriptionManager>();
           

        }

    }
}
