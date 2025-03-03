using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawazun.Framework.Mongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTawazunMongoDb(this IServiceCollection services, string mongoConnectionString, string database)
        {
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(mongoConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var mongoClient = c.GetRequiredService<IMongoClient>();

                return mongoClient.GetDatabase(database);
            });

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
