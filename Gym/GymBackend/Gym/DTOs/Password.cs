namespace DTOs;

public class Password : BaseDTO
{
    public int UserId { get; set; }
    public string PasswordContent { get; set; }
    public DateTime Created { get; set; }

    public string? PasswordToChange { get; set; }
}