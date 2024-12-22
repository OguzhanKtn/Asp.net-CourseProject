namespace Shared.Events
{
    public record OrderCreatedEvent(List<CourseEventData> Courses,string UserName,string Email);
    
    public record CourseEventData(string Title,string Price, string Picture);
    
}
