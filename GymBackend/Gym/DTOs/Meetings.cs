namespace DTOs;

public class Meetings : BaseDTO
{
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
    public TimeOnly TimeOfEntry { get; set; }
    public TimeOnly TimeOfExit { get; set; }
    public DateTime ProgrammedDate { get; set; }
    public string IsCancelled { get; set; }
    public DateTime Created { get; set; }
    public string ClientName { get; set; }
    public string EmployeeName { get; set; }
}