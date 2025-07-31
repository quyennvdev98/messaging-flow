using TicketFlow.Shared.Exceptions;

namespace TicketFlow.Services.Inquiries.Core.Data.Models;

public sealed class Inquiry
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public InquiryStatus Status { get; private set; }
    public InquiryCategory Category { get; private set; }
    public Guid? TicketId { get; private set; }

    private Inquiry()
    {
    }

    public Inquiry(string name, string email, string title, string description, InquiryCategory category)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Title = title;
        Description = description;
        Category = category;
        Status = InquiryStatus.New;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void SetRelatedTicketId(Guid ticketId)
    {
        if (TicketId != null && TicketId != ticketId)
        {
            throw new TicketFlowException("Inquiry is already correlated with another ticket!");
        }
        TicketId = ticketId;
    }
}  