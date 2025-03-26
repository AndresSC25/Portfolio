namespace DTOs;

public class Membership : BaseDTO
{
    public string Type { get; set; }
    public int AmountClassesAllowed { get; set; }
    public double MonthlyCost { get; set; }
    public DateTime Created { get; set; }
}