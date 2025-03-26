namespace DTOs;

public class UserGroupClass : BaseDTO
{
    public int GroupClassId { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public DateTime Created { get; set; }
}