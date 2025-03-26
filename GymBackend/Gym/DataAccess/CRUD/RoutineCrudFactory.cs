using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class RoutineCrudFactory : CrudFactory
{
    public RoutineCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var routine = baseDto as Routine;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_ROUTINE_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("P_ClientId", routine.ClientId);
        sqlOperation.AddStringParam("P_Name", routine.Name);

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

    public Routine RetrieveLastRoutine()
    {
        var sqlOperation = new SqlOperation();

        sqlOperation.ProcedureName = "RET_LAST_ROUTINE_PR";
        var listaResultados = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (listaResultados.Count > 0)
        {
            var row = listaResultados[0];
            var readRoutine = BuildRoutine(row);
            return readRoutine;
        }

        return default;
    }

    public List<Routine> RetrieveRoutineByUser(int clientId)
    {
        try
        {
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "RET_ROUTINE_BY_USER_PR";

            //Agregamos los parametros
            sqlOperation.AddIntParam("P_ClientId", clientId);
            var listaResultados = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            var routines = new List<Routine>();

            foreach (var row in listaResultados)
            {
                var readRoutine = BuildRoutine(row);
                routines.Add(readRoutine);
            }

            return routines;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Routine RetrieveById(int id)
    {
        try
        {
            var sqlOperation = new SqlOperation();

            sqlOperation.ProcedureName = "RET_ROUTINE_BY_ID_PR";

            //Agregamos los parametros
            sqlOperation.AddIntParam("P_Id", id);
            var listaResultados = _sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (listaResultados.Count > 0)
            {
                var row = listaResultados[0];
                var readRoutine = BuildRoutine(row);
                return readRoutine;
            }

            return default;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    private Routine BuildRoutine(Dictionary<string, object> row)
    {
        var routineToReturn = new Routine
        {
            Id = (int)row["id"],
            ClientId = (int)row["client_id"],
            Created = (DateTime)row["created"],
            Name = (string)row["name"]
        };

        return routineToReturn;
    }
}