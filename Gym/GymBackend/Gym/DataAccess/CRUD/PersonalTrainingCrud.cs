using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class PersonalTrainingCrudFactory : CrudFactory
{
    public PersonalTrainingCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        // Conversión del DTO base a PersonalTraining
        var personalTraining = baseDto as PersonalTraining;

        // Crear el instructivo para que el DAO pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_PERSONAL_TRAINING_PR"
        };

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_ClientId", personalTraining.ClientId);
        sqlOperation.AddIntParam("P_EmployeeId", personalTraining.EmployeeId);
        sqlOperation.AddStringParam("P_IsCancelled", personalTraining.IsCancelled);
        sqlOperation.AddStringParam("P_IsPaid", personalTraining.IsPaid);
        sqlOperation.AddTimeParam("P_TimeOfEntry", personalTraining.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", personalTraining.TimeOfExit);
        sqlOperation.AddDateTimeParam("P_ProgrammedDate", personalTraining.ProgrammedDate);
        sqlOperation.AddDoubleParam("P_HourlyRate", personalTraining.HourlyRate);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        // Conversión del DTO base a PersonalTraining
        var personalTraining = baseDto as PersonalTraining;

        // Crear el instructivo para que el DAO pueda realizar un delete en la base de datos
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_PERSONAL_TRAINING_PR"
        };

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_Id", personalTraining.Id);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstPersonalTrainings = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_PERSONAL_TRAININGS_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var personalTraining = BuildPersonalTraining(row);
                lstPersonalTrainings.Add((T)Convert.ChangeType(personalTraining, typeof(T)));
            }

        return lstPersonalTrainings;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_PERSONAL_TRAINING_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);
        // Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retPersonalTraining = (T)Convert.ChangeType(BuildPersonalTraining(row), typeof(T));
            return retPersonalTraining;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        // Conversión del DTO base a PersonalTraining
        var personalTraining = baseDto as PersonalTraining;

        // Crear el instructivo para que el DAO pueda realizar un update en la base de datos
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_PERSONAL_TRAINING_PR"
        };

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_Id", personalTraining.Id);
        sqlOperation.AddIntParam("P_ClientId", personalTraining.ClientId);
        sqlOperation.AddIntParam("P_EmployeeId", personalTraining.EmployeeId);
        sqlOperation.AddStringParam("P_IsCancelled", personalTraining.IsCancelled);
        sqlOperation.AddStringParam("P_IsPaid", personalTraining.IsPaid);
        sqlOperation.AddTimeParam("P_TimeOfEntry", personalTraining.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", personalTraining.TimeOfExit);
        sqlOperation.AddDateTimeParam("P_ProgrammedDate", personalTraining.ProgrammedDate);
        sqlOperation.AddDoubleParam("P_HourlyRate", personalTraining.HourlyRate);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public void Cancel(PersonalTraining personalTraining)
    {
        // Crear el instructivo para que el DAO pueda realizar un update en la base de datos
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_CANCEL_PERSONAL_TRAINING"
        };

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_Id", personalTraining.Id);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public List<PersonalTraining> RetrieveByClientId(int id)
    {
        var lstPT = new List<PersonalTraining>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_PT_BY_CLIENTID_PR" };
        sqlOperation.AddIntParam("P_Id", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var personalTraining = BuildPTWithNames(row);
                lstPT.Add(personalTraining);
            }

        return lstPT;
    }

    public List<PersonalTraining> RetrieveByEmployeeId(int id)
    {
        var lstPT = new List<PersonalTraining>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_PT_BY_EMPLOYEEID_PR" };
        sqlOperation.AddIntParam("P_Id", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var personalTraining = BuildPTWithNames(row);
                lstPT.Add(personalTraining);
            }

        return lstPT;
    }

    #region Funciones extras

    private PersonalTraining BuildPersonalTraining(Dictionary<string, object> row)
    {
        var personalTrainingToReturn = new PersonalTraining
        {
            Id = (int)row["id"],
            ClientId = (int)row["client_id"],
            EmployeeId = (int)row["employee_id"],
            IsCancelled = (string)row["is_cancelled"],
            IsPaid = (string)row["is_paid"],
            TimeOfEntry = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_entry"]),
            TimeOfExit = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_exit"]),
            ProgrammedDate = (DateTime)row["programed_date"],
            HourlyRate = (double)(decimal)row["hourly_rate"]
        };
        return personalTrainingToReturn;
    }

    private PersonalTraining BuildPTWithNames(Dictionary<string, object> row)
    {
        var personalTrainingToReturn = new PersonalTraining
        {
            Id = (int)row["id"],
            ClientId = (int)row["client_id"],
            ClientName = (string)row["client_full_name"],
            EmployeeId = (int)row["employee_id"],
            EmployeeName = (string)row["employee_full_name"],
            IsCancelled = (string)row["is_cancelled"],
            IsPaid = (string)row["is_paid"],
            TimeOfEntry = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_entry"]),
            TimeOfExit = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_exit"]),
            ProgrammedDate = (DateTime)row["programed_date"],
            HourlyRate = (double)(decimal)row["hourly_rate"]
        };
        return personalTrainingToReturn;
    }

    #endregion
}