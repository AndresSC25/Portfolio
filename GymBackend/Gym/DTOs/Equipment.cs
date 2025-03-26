namespace DTOs;

public class Equipment : BaseDTO
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
}