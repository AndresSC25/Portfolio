namespace DTOs;

public class Notification : BaseDTO
{
    public int UserId { get; set; }
    public string NotificationContent { get; set; }
    public string WasRead { get; set; }
    public DateTime Created { get; set; }
}