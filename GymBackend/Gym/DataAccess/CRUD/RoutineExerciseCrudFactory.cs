using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class RoutineExerciseCrudFactory : CrudFactory
{
    public RoutineExerciseCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var routineExercise = baseDto as Routine_Exercise;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_ROUTINE_EXERCISE_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("P_RoutineId", routineExercise.RoutineId);
        sqlOperation.AddIntParam("P_ExerciseId", routineExercise.ExerciseId);

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

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }
}