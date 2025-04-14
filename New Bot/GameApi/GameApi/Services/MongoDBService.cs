using GameApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameApi.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Button> _collection;

        public MongoDBService(IOptions<MongoDBSettings> settings)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Button>(settings.Value.CollectionName);

            VerifyConnection(client);
        }

        private void VerifyConnection(MongoClient client)
        {
            try
            {
                client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("✅ Conexão com MongoDB verificada!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Falha na conexão: {ex.Message}");
                throw;
            }
        }

        public async Task SaveButtonClick(Button click)
        {
            await _collection.InsertOneAsync(click);
        }
    }
}
