using GameApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameApi.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<ButtonClickRequest> _collection;

        public MongoDBService(IOptions<MongoDBSettings> settings)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<ButtonClickRequest>(settings.Value.CollectionName);

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

        public async Task SaveButtonClick(ButtonClickRequest clickRequest)
        {
            // Adiciona metadados automáticos
            clickRequest.Metadata["receivedAt"] = DateTime.UtcNow.ToString("o");
            clickRequest.Metadata["source"] = "UnityGame";

            await _collection.InsertOneAsync(clickRequest);
        }
    }
}
