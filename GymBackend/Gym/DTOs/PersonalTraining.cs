namespace DTOs;

public class PersonalTraining : BaseDTO
{
    public int ClientId { get; set; }
    public string ClientName { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string IsCancelled { get; set; }
    public string IsPaid { get; set; }
    public TimeOnly TimeOfEntry { get; set; }
    public TimeOnly TimeOfExit { get; set; }
    public DateTime ProgrammedDate { get; set; }
    public double HourlyRate { get; set; }
}