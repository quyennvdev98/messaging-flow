using TicketFlow.Shared.Commands;

namespace TicketFlow.Services.Inquiries.Core.Commands.SubmitInquirySynchronously;

public sealed record CreateTicketSynchronously(
    Guid Id,
    string Name,
    string Email,
    string Title,
    string Description,
    string TranslatedDescription,
    string Category,
    string LanguageCode,
    DateTimeOffset CreatedAt) : ICommand;