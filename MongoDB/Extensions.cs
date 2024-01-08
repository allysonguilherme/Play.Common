using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Play.Common.Settings;

namespace Play.Common.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, string collectionName){
            var provider = services.BuildServiceProvider();

            var configuration = provider.GetService<IConfiguration>();
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var mongoDBSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

            var mongoClient = new MongoClient($"mongodb://{mongoDBSettings.Host}:{mongoDBSettings.Port}");
            var mongoDatabase = mongoClient.GetDatabase(serviceSettings.Name);

            services.AddSingleton(mongoDatabase);

            return services;
        }

        public static IServiceCollection AddMongoCollection<T>(this IServiceCollection services, string collectionName){
            var provider = services.BuildServiceProvider();

            var database = provider.GetService<IMongoDatabase>();

            var collection = database.GetCollection<T>(collectionName);

            services.AddSingleton(collection);

            return services;
        }
    }
}