﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketFlow.Services.Tickets.Core.Data;
using TicketFlow.Shared.Queries;

namespace TicketFlow.Services.Tickets.Core.Queries.GetTicketDetails;

public class GetTicketDetailsQueryHandler(TicketsDbContext dbContext, ILogger<GetTicketDetailsQueryHandler> logger) : IQueryHandler<GetTicketDetailsQuery, TicketDetailsDto>
{
    public async Task<TicketDetailsDto> HandleAsync(GetTicketDetailsQuery query, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = (await dbContext.Tickets
                .Include(x => x.AssignedAgent)
                .SingleOrDefaultAsync(x => x.Id == query.TicketId, cancellationToken))!;

            return new TicketDetailsDto(
                result.Id.ToString(),
                result.Email,
                result.Status.ToString(),
                result.CreatedAt,
                result.Severity,
                result.AssignedAgent?.UserId,
                result.Type?.ToString(),
                result.Resolution);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}