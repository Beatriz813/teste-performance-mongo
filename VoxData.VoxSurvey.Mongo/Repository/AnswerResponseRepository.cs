using MongoDB.Driver;
using VoxData.VoxSurvey.Mongo.Models;

namespace VoxData.VoxSurvey.Mongo.Repository
{
    public class AnswerResponseRepository
    {
        private IMongoCollection<AnswerResponse> _AnswerResponse;

        public AnswerResponseRepository(IMongoDatabase database)
        {
            _AnswerResponse = database.GetCollection<AnswerResponse>("AnswerResponse");
        }

        public async Task Update(AnswerResponse response)
        {
            var filter = Builders<AnswerResponse>.Filter.Eq(a => a.Request.SKe, response.Request.SKe);
            var update = Builders<AnswerResponse>.Update.Set(a => a.Response, response.Response);
            UpdateOptions opts = new UpdateOptions()
            {
                IsUpsert = true
            };
            await _AnswerResponse.UpdateOneAsync(filter, update, opts);
        }

        public async Task InsertRequest(AnswerResponse response)
        {
            await _AnswerResponse.InsertOneAsync(response);
        }

        public async Task<List<GroupModel>> GetGroup(DateTime pdtStart, DateTime pdtEnd, long lIDEvaluation, string psGroupBy)
        {
            IAggregateFluent<AnswerResponse> pipeline = _AnswerResponse
                        .Aggregate()
                        .Match(
                            Builders<AnswerResponse>.Filter.And(
                                    Builders<AnswerResponse>.Filter.Eq(x => x.Request.Ev, lIDEvaluation),
                                    Builders<AnswerResponse>.Filter.Gte(x => x.Request.Dt, pdtStart),
                                    Builders<AnswerResponse>.Filter.Lte(x => x.Response.Dta, pdtEnd)
                                )
                            );

            switch (psGroupBy.ToUpper())
            {
                case "D":
                    return pipeline
                        .AppendStage<GroupModel>("{$group: { _id: { $dateToString: { format: '%Y-%m-%d', date: '$Response.Dta' } }, Cnt: { $sum: 1 }}}")
                        .ToList();

                case "M":
                    return pipeline
                        .AppendStage<GroupModel>("{$group: { _id: { $dateToString: { format: '%Y-%m', date: '$Dt' } }, Cnt: { $sum: 1 }}}")
                        .ToList();

                case "Y":
                    return pipeline
                        .AppendStage<GroupModel>("{$group: { _id: { $dateToString: { format: '%Y', date: '$Dt' } }, Cnt: { $sum: 1 }}}")
                        .ToList();

                default:
                    return pipeline
                        .AppendStage<GroupModel>("{$group: { _id: { $dateToString: { format: '%H', date: '$Dt' } }, Cnt: { $sum: 1 }}}")
                        .ToList();
            }
        }
    }
}
