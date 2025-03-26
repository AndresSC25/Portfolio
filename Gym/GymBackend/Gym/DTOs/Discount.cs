namespace DTOs;

public class Discount : BaseDTO
{
    public string Type { get; set; }
    public string Coupon { get; set; }
    public int Percentage { get; set; }
    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public DateTime Created { get; set; }
}