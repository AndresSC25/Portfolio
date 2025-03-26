namespace DTOs;

public class Exercise : BaseDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int EquipmentId { get; set; }
    public string? EquipmentName { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public int Weight { get; set; }
    public int Duration { get; set; }
}