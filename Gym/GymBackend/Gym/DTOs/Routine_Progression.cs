namespace DTOs;

public class Routine_Progression : BaseDTO
{
    public int RoutineId { get; set; }
    public int ExerciseId { get; set; }
    public string? ExerciseName { get; set; }
    public int Sets { get; set; }
    public int Weight { get; set; }
    public int Reps { get; set; }
    public int Duration { get; set; }
    public string? Comments { get; set; }
    public DateTime Created { get; set; }
}