namespace DTOs;

public class Schedule : BaseDTO
{
    public int EmployeeId { get; set; }
    public string DaysOfWeek { get; set; }
    public TimeOnly TimeOfEntry { get; set; }
    public TimeOnly TimeOfExit { get; set; }
    public DateTime Created { get; set; }
}