using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class RoutineProgressionManager
{
    public void Create(Routine_Progression routineProgression)
    {
        var rpCrud = new RoutineProgressionCrudFactory();
        rpCrud.Create(routineProgression);
    }

    public List<Routine_Progression> RetrieveByRoutineId(int routineId)
    {
        var rpCrud = new RoutineProgressionCrudFactory();
        var routinesProgression = rpCrud.RetrieveByRoutineId(routineId);
        return routinesProgression;
    }
}