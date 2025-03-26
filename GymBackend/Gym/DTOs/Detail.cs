namespace DTOs;

public class Detail : BaseDTO
{
    public int InvoiceId { get; set; }
    public int? UserMembershipId { get; set; }
    public int? PersonalTrainingId { get; set; }
    public double Price { get; set; }

    public DateTime Created { get; set; }
}