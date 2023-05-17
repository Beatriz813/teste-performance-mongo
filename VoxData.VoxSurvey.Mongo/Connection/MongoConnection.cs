using MongoDB.Driver;

namespace VoxData.VoxSurvey.Mongo.Connection
{
    public class MongoConnection
    {
        public MongoSettings _settings { get; set; }
        public MongoClient _client { get; set; }
        public MongoConnection(MongoSettings settings)
        {
            _settings = settings;
            MongoClientSettings mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
            _client = new MongoClient(mongoSettings);
        }

        public IMongoDatabase GetConnection()
        {
            return _client.GetDatabase(_settings.DbName);
        }

        public IMongoDatabase GetConnection(string DBName)
        {
            return _client.GetDatabase(DBName);
        }

    }
}
