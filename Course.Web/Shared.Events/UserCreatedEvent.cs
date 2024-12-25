namespace Shared.Events
{
    public record UserCreatedEvent(string FirstName,string LastName,string Email,string DiscountCode);
}
