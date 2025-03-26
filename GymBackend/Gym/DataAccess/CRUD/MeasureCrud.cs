using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class MeasureCrud : CrudFactory
{
    public MeasureCrud()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var measure = baseDto as Measures;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_MEASURES_PR"
        };

        sqlOperation.AddIntParam("P_ClientId", measure.ClientId);
        sqlOperation.AddDoubleParam("P_Weight", measure.Weight);
        sqlOperation.AddDoubleParam("P_Height", measure.Height);
        sqlOperation.AddDoubleParam("P_AverageOfFat", measure.AverageOfFat);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var measure = baseDto as Measures;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_MEASURES_PR"
        };

        sqlOperation.AddIntParam("P_Id", measure.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstMeasures = new List<T>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_ALL_MEASURES_PR"
        };

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var measure = BuildMeasure(row);
                lstMeasures.Add((T)Convert.ChangeType(measure, typeof(T)));
            }

        return lstMeasures;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_MEASURES_BY_ID_PR"
        };

        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retMeasure = (T)Convert.ChangeType(BuildMeasure(row), typeof(T));
            return retMeasure;
        }

        return default;
    }

    public List<Measures> RetrieveByUserId(int id)
    {
        var lstMeasures = new List<Measures>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_MEASURES_BY_USERID_PR"
        };
        sqlOperation.AddIntParam("P_Id", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var measure = BuildMeasure(row);
                lstMeasures.Add(measure);
            }

        return lstMeasures;
    }

    public override void Update(BaseDTO baseDto)
    {
        var measure = baseDto as Measures;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_MEASURES_PR"
        };

        sqlOperation.AddIntParam("P_Id", measure.Id);
        sqlOperation.AddIntParam("P_ClientId", measure.ClientId);
        sqlOperation.AddDoubleParam("P_Weight", measure.Weight);
        sqlOperation.AddDoubleParam("P_Height", measure.Height);
        sqlOperation.AddDoubleParam("P_AverageOfFat", measure.AverageOfFat);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Measures BuildMeasure(Dictionary<string, object> row)
    {
        var measureToReturn = new Measures
        {
            Id = (int)row["id"],
            ClientId = (int)row["client_id"],
            Weight = Convert.ToDouble(row["weight"]),
            Height = Convert.ToDouble(row["height"]),
            AverageOfFat = Convert.ToDouble(row["average_of_fat"]),
            Created = (DateTime)row["created"]
        };
        return measureToReturn;
    }

    #endregion
}