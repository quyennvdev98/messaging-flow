using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Services.SystemMetrics.Generator;
using TicketFlow.Services.Translations.Core.Messaging;
using TicketFlow.Services.Translations.Core.Translations;
using TicketFlow.Shared.AnomalyGeneration;
using TicketFlow.Shared.App;
using TicketFlow.Shared.Commands;
using TicketFlow.Shared.Messaging;
using TicketFlow.Shared.Messaging.RabbitMQ;
using TicketFlow.Shared.Messaging.Resiliency;
using TicketFlow.Shared.Observability;
using TicketFlow.Shared.Queries;
using TicketFlow.Shared.Serialization;

namespace TicketFlow.Services.Translations.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSerialization()
            .AddApp(configuration)
            .AddCommands()
            .AddQueries()
            .AddLogging()
            .AddMessaging(configuration, x => x.UseRabbitMq().UseAnomalies().UseResiliency())
            .AddTranslations(configuration)
            .AddSystemMetrics(configuration)
            .AddObservability(configuration);
        
        services.AddHostedService<TranslationsConsumerService>();
        services.AddHostedService<TranslationTopologyInitializer>();
        return services;
    }
}