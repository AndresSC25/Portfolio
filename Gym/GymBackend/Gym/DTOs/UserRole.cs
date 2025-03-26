namespace DTOs;

public class UserRole : BaseDTO
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? DaysOfWeek { get; set; }

    public TimeOnly? TimeOfEntry { get; set; }

    public TimeOnly? TimeOfExit { get; set; }
}