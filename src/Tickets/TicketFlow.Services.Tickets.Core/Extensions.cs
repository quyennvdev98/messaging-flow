using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketFlow.Services.SystemMetrics.Generator;
using TicketFlow.Services.Tickets.Core.Data;
using TicketFlow.Services.Tickets.Core.Data.Repositories;
using TicketFlow.Services.Tickets.Core.Initializers;
using TicketFlow.Services.Tickets.Core.Messaging;
using TicketFlow.Services.Tickets.Core.Messaging.Publishing.Conventions;
using TicketFlow.Shared.AnomalyGeneration;
using TicketFlow.Shared.App;
using TicketFlow.Shared.Commands;
using TicketFlow.Shared.Data;
using TicketFlow.Shared.Exceptions;
using TicketFlow.Shared.Messaging;
using TicketFlow.Shared.Messaging.Deduplication;
using TicketFlow.Shared.Messaging.Outbox;
using TicketFlow.Shared.Messaging.RabbitMQ;
using TicketFlow.Shared.Messaging.Resiliency;
using TicketFlow.Shared.Observability;
using TicketFlow.Shared.Queries;
using TicketFlow.Shared.Serialization;

namespace TicketFlow.Services.Tickets.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddExceptions()
            .AddApp(configuration)
            .AddSerialization()
            .AddAppInitializers()
            .AddCommands()
            .AddQueries()
            .AddLogging()
            .AddMessaging(configuration, x => x
                .UseRabbitMq()
                .UseMessagePublisherConvention<TicketsMessagePublisherConventionProvider>()
                .UseDeduplication()
                .UseOutbox()
                .UseAnomalies()
                .UseResiliency())
            .AddPostgres<TicketsDbContext>(configuration)
            .AddSystemMetrics(configuration)
            .AddObservability(configuration);
        
        services.AddHostedService<TicketsConsumerService>();
        services.AddHostedService<TicketsTopologyInitializer>();
        services.AddTransient<ITicketsRepository, TicketsRepository>();
        
        return services;
    }
}