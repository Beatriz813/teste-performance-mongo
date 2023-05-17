using MongoDB.Bson;
using MongoDB.Driver;
using VoxData.VoxSurvey.Mongo.Models;

namespace VoxData.VoxSurvey.Mongo.Repository
{
    public class AnswerSessionResponseRepository
    {
        private IMongoCollection<AnswerSessionResponse> _colAnswerSessionResponse;
        public AnswerSessionResponseRepository(IMongoDatabase database)
        {
            _colAnswerSessionResponse = database.GetCollection<AnswerSessionResponse>("AnswerSessionResponse");
        }
        public long CountAnswerByIDEvaluation(DateTime startDate, DateTime endDate, long lIDEvaluation) => _colAnswerSessionResponse
                .Aggregate()
                .Lookup("AnswerSessionRequest", "SKe", "SKe", "answer")
                .Unwind("answer")
                .ReplaceRoot<AnswerSessionResponseLookedUp>(new BsonDocument("$mergeObjects",
                new BsonArray
                {
                    "$answer",
                    new BsonDocument
                    {
                        { "Ev", "$Ev" },
                        { "SDt", "$Dt" },
                        { "Ans", "$Ans"}
                    }
                }))
                .Match(
                    Builders<AnswerSessionResponseLookedUp>.Filter.And(
                        Builders<AnswerSessionResponseLookedUp>.Filter.Eq(z => z.IDEvaluation, lIDEvaluation),
                        Builders<AnswerSessionResponseLookedUp>.Filter.Gte(y => y.CreatedDate, startDate),
                        Builders<AnswerSessionResponseLookedUp>.Filter.Lte(y => y.CreatedDate, endDate)
                ))
                .Count()
                .FirstOrDefault()
                ?.Count ?? 0;

        public double GetResponseTimeAVG(DateTime startDate, DateTime endDate, long lIDEvaluation) => _colAnswerSessionResponse.Aggregate()
                .Lookup("AnswerSessionRequest", "SKe", "SKe", "answer")
                .Unwind("answer")
                .ReplaceRoot<AnswerSessionResponseLookedUp>(new BsonDocument("$mergeObjects",
                new BsonArray
                {
                    "$answer",
                    new BsonDocument
                    {
                        { "Ev", "$Ev" },
                        { "SDt", "$Dt" },
                        { "Ans", "$Ans"},
                        {
                            "Ttr",
                            new BsonDocument
                            {
                                {
                                    "$dateDiff",
                                    new BsonDocument
                                    {
                                        { "startDate", "$Dt" },
                                        { "endDate", "$Dt" },
                                        { "unit", "second" }
                                    }
                                }
                            }
                        }
                    }
                }))
                .AppendStage<AnswerSessionResponseLookedUp>("{$addFields: {Ttr: {$dateDiff:{ startDate: '$Dt', endDate: '$SDt', unit: 'second' }}}}")
                .Match(
                    Builders<AnswerSessionResponseLookedUp>.Filter.And(
                        Builders<AnswerSessionResponseLookedUp>.Filter.Eq(z => z.IDEvaluation, lIDEvaluation),
                        Builders<AnswerSessionResponseLookedUp>.Filter.Gte(y => y.CreatedDate, startDate),
                        Builders<AnswerSessionResponseLookedUp>.Filter.Lte(y => y.CreatedDate, endDate)
                ))
                .AppendStage<AnswerSessionResponseAVGResponseTime>("{$group: {_id: null, avg: { $avg: '$Ttr'} }}")
                .FirstOrDefault()
                ?.ResponseTimeAVG ?? default;

        public List<GroupModel> GetAnswerSessionResponseGrouped(DateTime pdtStart, DateTime pdtEnd, long lIDEvaluation, string psGroupBy)
        {
            IAggregateFluent<AnswerSessionResponse> pipeline = _colAnswerSessionResponse
                        .Aggregate()
                        .Lookup("AnswerSessionRequest", "SKe", "SKe", "request")
                        .Unwind("request")
                        .ReplaceRoot<AnswerSessionResponse>(new BsonDocument("$mergeObjects", new BsonArray { "$request", new BsonDocument { { "Ev", "$Ev" } } }))
                        .Match(
                            Builders<AnswerSessionResponse>.Filter.And(

                                    Builders<AnswerSessionResponse>.Filter.Eq("Ev", lIDEvaluation),
                                    Builders<AnswerSessionResponse>.Filter.Gte(x => x.CreatedDate, pdtStart),
                                    Builders<AnswerSessionResponse>.Filter.Lte(x => x.CreatedDate, pdtEnd)
                                )
                            );

            switch (psGroupBy.ToUpper())
            {
                case "D":
                    return pipeline
                        .AppendStage<GroupModel>("{$group: { _id: { $dateToString: { format: '%Y-%m-%d', date: '$Dt' } }, Cnt: { $sum: 1 }}}")
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
