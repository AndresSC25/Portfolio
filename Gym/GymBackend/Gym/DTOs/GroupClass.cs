namespace DTOs;

public class GroupClass : BaseDTO
{
    public int EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string ClassName { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentRegistered { get; set; }
    public DateTime ClassDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}