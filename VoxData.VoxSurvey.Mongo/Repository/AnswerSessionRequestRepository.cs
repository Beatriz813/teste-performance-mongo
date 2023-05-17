using MongoDB.Driver;
using VoxData.VoxSurvey.Mongo.Models;

namespace VoxData.VoxSurvey.Mongo.Repository
{
    public class AnswerSessionRequestRepository
    {
        private IMongoCollection<AnswerSessionRequest> _colAnswerSessionRequest;

        public AnswerSessionRequestRepository(IMongoDatabase database)
        {
            _colAnswerSessionRequest = database.GetCollection<AnswerSessionRequest>("AnswerSessionRequest");
        }

        public long CountSessionByIDEvaluation(DateTime startDate, DateTime endDate, long lIDEvaluation) => _colAnswerSessionRequest?
            .CountDocuments(Builders<AnswerSessionRequest>.Filter.And(
                        Builders<AnswerSessionRequest>.Filter.Eq(z => z.IDEvaluation, lIDEvaluation),
                        Builders<AnswerSessionRequest>.Filter.Gte(y => y.CreatedDate, startDate),
                        Builders<AnswerSessionRequest>.Filter.Lte(y => y.CreatedDate, endDate)
                )) ?? 0;

        public List<GroupModel> GetAnswerSessionRequestGrouped(DateTime pdtStart, DateTime pdtEnd, long lIDEvaluation, string psGroupBy)
        {
            IAggregateFluent<AnswerSessionRequest> pipeline = _colAnswerSessionRequest
                        .Aggregate()
                        .Match(
                            Builders<AnswerSessionRequest>.Filter.And(
                                    Builders<AnswerSessionRequest>.Filter.Eq(z => z.IDEvaluation, lIDEvaluation),
                                    Builders<AnswerSessionRequest>.Filter.Gte("Dt", pdtStart),
                                    Builders<AnswerSessionRequest>.Filter.Lte("Dt", pdtEnd)
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
