using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Api.Data.Repositories
{
    public class CommandRepository
    {
        private readonly IMongoDatabase database;
        public CommandRepository(IMongoSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task Post<T>(T model, string query)
        {
            string[] queries = query.Split(':');

            var collection = database.GetCollection<T>(queries[0]);

            await collection.InsertOneAsync(model);
        }
    }
}
