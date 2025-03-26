namespace DTOs;

public class OTP : BaseDTO
{
    public int OtpData { get; set; }
    public int UserId { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string WasUsed { get; set; }
    public DateTime Created { get; set; }
}