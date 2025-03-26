using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class RoutineManager
{
    public void Create(Routine routine, List<int> exerciseIds)
    {
        if (exerciseIds.Count > 0)
        {
            var rCrud = new RoutineCrudFactory();
            var reCrud = new RoutineExerciseCrudFactory();

            // Crear la rutina
            rCrud.Create(routine);

            // Recuperar el último RoutineId generado
            var lastRoutine = rCrud.RetrieveLastRoutine();

            // Registrar los ejercicios asociados
            foreach (var exerciseId in exerciseIds)
            {
                var routineExercise = new Routine_Exercise
                {
                    RoutineId = lastRoutine.Id,
                    ExerciseId = exerciseId
                };
                reCrud.Create(routineExercise);
            }
        }
        else
        {
            throw new Exception("Debe de elegir al menos un ejercicio");
        }
    }

    public List<Routine> RetrieveByUser(int clientId)
    {
        var rCrud = new RoutineCrudFactory();
        var routines = rCrud.RetrieveRoutineByUser(clientId);
        return routines;
    }

    public Routine RetrieveById(int id)
    {
        var rCrud = new RoutineCrudFactory();
        var eCrud = new ExerciseCrudFactory();

        var routine = rCrud.RetrieveById(id);
        if (routine != null)
        {
            routine.Exercise = eCrud.RetrieveByRoutineId<Exercise>(routine.Id);
            return routine;
        }

        throw new Exception("Rutina no encontrada");
    }
}