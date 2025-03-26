using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class RoutineProgressionCrudFactory : CrudFactory
{
    public RoutineProgressionCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var routineProgression = baseDto as Routine_Progression;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_ROUTINE_PROGRESS_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("@P_RoutineId", routineProgression.RoutineId);
        sqlOperation.AddIntParam("@P_ExerciseId", routineProgression.ExerciseId);
        sqlOperation.AddIntParam("@P_Sets", routineProgression.Sets);
        sqlOperation.AddIntParam("P_Weight", routineProgression.Weight);
        sqlOperation.AddIntParam("P_Reps", routineProgression.Reps);
        sqlOperation.AddIntParam("P_Duration", routineProgression.Duration);
        sqlOperation.AddStringParam("@P_Comments", routineProgression.Comments);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public List<Routine_Progression> RetrieveByRoutineId(int routineId)
    {
        var lstRoutineProgression = new List<Routine_Progression>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ROUTINE_PROGRESSION_BY_ROUTINE_PR" };

        //Agregamos los parametros
        sqlOperation.AddIntParam("@P_RoutineId", routineId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var routineProgression = BuildRoutineProgression(row);
                lstRoutineProgression.Add(routineProgression);
            }

        return lstRoutineProgression;
    }

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    private Routine_Progression BuildRoutineProgression(Dictionary<string, object> row)
    {
        var routineProgressionToReturn = new Routine_Progression
        {
            Id = (int)row["id"],
            RoutineId = (int)row["routine_id"],
            ExerciseId = (int)row["exercise_id"],
            ExerciseName = (string)row["exercise_name"],
            Sets = (int)row["sets"],
            Weight = (int)row["weight"],
            Reps = (int)row["reps"],
            Duration = (int)row["duration"],
            Comments = (string)row["comments"],
            Created = (DateTime)row["created"]
        };

        return routineProgressionToReturn;
    }
}