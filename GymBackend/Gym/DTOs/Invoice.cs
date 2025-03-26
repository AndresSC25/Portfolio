namespace DTOs;

public class Invoice : BaseDTO
{
    public int UserId { get; set; }

    public string? UserName { get; set; }
    public int DiscountId { get; set; }
    public double Amount { get; set; }
    public double AmountAfterDiscount { get; set; }

    public string PaymentMethod { get; set; }

    public string IsConfirmed { get; set; }

    public DateTime Created { get; set; }

    public int? MembershipID { get; set; }
}