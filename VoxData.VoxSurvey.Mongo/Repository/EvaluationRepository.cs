using MongoDB.Driver;
using VoxData.VoxSurvey.Mongo.Models;

namespace VoxData.VoxSurvey.Mongo.Repository
{
    public class EvaluationRepository
    {
        public IMongoCollection<Evaluation> _collection { get; set; }
        public EvaluationRepository(IMongoDatabase database) 
        { 
            _collection = database.GetCollection<Evaluation>("Evaluation");
        }

        public void Insert(Evaluation evaluation)
        {
            _collection.InsertOne(evaluation);
        }

        public void Update(Evaluation evaluation)
        {
            FilterDefinition<Evaluation> filter = Builders<Evaluation>.Filter.Eq(e => e._id, evaluation._id);
            _collection.ReplaceOne(filter, evaluation);
        }

        public void UpdateStatus(string IdEvaluation, bool active)
        {
            FilterDefinition<Evaluation> filter = Builders<Evaluation>.Filter.Eq(e => e._id, IdEvaluation);
            var updateDefinition = Builders<Evaluation>.Update.Set(e => e.Active, active);
            _collection.UpdateOne(filter, updateDefinition);
        }

        public void Delete(Evaluation evaluation)
        {
            FilterDefinition<Evaluation> filter = Builders<Evaluation>.Filter.Eq(e => e._id, evaluation._id);
            _collection.DeleteOne(filter);
        }

        public IEnumerable<Evaluation> GetEvaluationByIDCompanyLevel(long idCompanyLevel)
        {
            FilterDefinition<Evaluation> filter = Builders<Evaluation>.Filter.Eq(e => e.IdCompanyLevel, idCompanyLevel);
            return _collection.Find(filter).ToList();
        }

        public Evaluation GetEvaluationById(string id)
        {
            FilterDefinition<Evaluation> filter = Builders<Evaluation>.Filter.Eq(e => e._id, id);
            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
