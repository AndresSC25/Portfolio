using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class ExerciseCrudFactory : CrudFactory
{
    public ExerciseCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var exercise = baseDto as Exercise;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_EXE_PR";

        //Agregamos los parametros
        sqlOperation.AddStringParam("P_Name", exercise.Name);
        sqlOperation.AddStringParam("P_Type", exercise.Type);
        sqlOperation.AddIntParam("P_EquipmentId", exercise.EquipmentId);
        sqlOperation.AddIntParam("P_Sets", exercise.Sets);
        sqlOperation.AddIntParam("P_Reps", exercise.Reps);
        sqlOperation.AddIntParam("P_Weight", exercise.Weight);
        sqlOperation.AddIntParam("P_Duration", exercise.Duration);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Update(BaseDTO baseDto)
    {
        var exercise = baseDto as Exercise;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_EXE_PR"
        };

        sqlOperation.AddIntParam("P_Id", exercise.Id);
        sqlOperation.AddStringParam("P_Name", exercise.Name);
        sqlOperation.AddStringParam("P_Type", exercise.Type);
        sqlOperation.AddIntParam("P_EquipmentId", exercise.EquipmentId);
        sqlOperation.AddIntParam("P_Sets", exercise.Sets);
        sqlOperation.AddIntParam("P_Reps", exercise.Reps);
        sqlOperation.AddIntParam("P_Weight", exercise.Weight);
        sqlOperation.AddIntParam("P_Duration", exercise.Duration);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var exercise = baseDto as Exercise;
        var sqlOperation = new SqlOperation { ProcedureName = "DEL_EXE_PR" };
        sqlOperation.AddIntParam("P_Id", exercise.Id);
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstExercises = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_EXE_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var exercise = BuildExercise(row);
                lstExercises.Add((T)Convert.ChangeType(exercise, typeof(T)));
            }

        return lstExercises;
    }

    public List<T> RetrieveByRoutineId<T>(int routineId)
    {
        var lstExercises = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_EXE_BY_ROUTINE_PR" };

        //Agregamos los parametros
        sqlOperation.AddIntParam("@P_RoutineId", routineId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var exercise = BuildExercise(row);
                lstExercises.Add((T)Convert.ChangeType(exercise, typeof(T)));
            }

        return lstExercises;
    }

    private Exercise BuildExercise(Dictionary<string, object> row)
    {
        var exerciseToReturn = new Exercise
        {
            Id = (int)row["id"],
            EquipmentId = (int)row["equipment_id"],
            EquipmentName = (string)row["equipmentName"],
            Type = (string)row["type"],
            Name = (string)row["name"],
            Sets = (int)row["sets"],
            Weight = (int)row["weight"],
            Reps = (int)row["reps"],
            Duration = (int)row["duration"]
        };

        return exerciseToReturn;
    }
}