namespace DTOs;

public class UserMembership : BaseDTO
{
    public int UserId { get; set; }
    public int MembershipId { get; set; }
    public DateTime Created { get; set; }
}