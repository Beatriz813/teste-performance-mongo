using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoxData.VoxSurvey.Mongo.Connection;
using VoxData.VoxSurvey.Mongo.Models;
using VoxData.VoxSurvey.Mongo.Repository;

namespace VoxData.VoxSurvey.Mongo.Extension
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddConnectionMongo(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoDB").Get<MongoSettings>();
            var connection = new MongoConnection(settings);

            return services.AddSingleton(settings)
                .AddSingleton(connection)
                .AddSingleton(connection.GetConnection());
        }

        public static IServiceCollection AddRepositoryEvaluation(this IServiceCollection services) => services.AddSingleton<EvaluationRepository>();
        public static IServiceCollection AddRepositoryAnswerSessionRequest(this IServiceCollection services) => services.AddSingleton<AnswerSessionRequestRepository>();
        public static IServiceCollection AddRepositoryAnswerSessionResponse(this IServiceCollection services) => services.AddSingleton<AnswerSessionResponseRepository>();
        public static IServiceCollection AddRepositoryAnswerResponse(this IServiceCollection services) => services.AddSingleton<AnswerResponseRepository>();

    }
}
