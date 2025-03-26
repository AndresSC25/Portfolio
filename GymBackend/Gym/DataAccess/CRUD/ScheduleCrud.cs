using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class ScheduleCrudFactory : CrudFactory
{
    public ScheduleCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var schedule = baseDto as Schedule;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_SCHEDULE_PR";

        sqlOperation.AddIntParam("P_EmployeeId", schedule.EmployeeId);
        sqlOperation.AddStringParam("P_DaysOfWeek", schedule.DaysOfWeek);
        sqlOperation.AddTimeParam("P_TimeOfEntry", schedule.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", schedule.TimeOfExit);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var schedule = baseDto as Schedule;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "DEL_SCHEDULE_PR";

        sqlOperation.AddIntParam("P_Id", schedule.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public void DeleteByUserId(int idUser)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "DEL_BYIDUSER_SCHEDULE_PR";

        sqlOperation.AddIntParam("P_IdUser", idUser);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstSchedules = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_SCHEDULES_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var schedule = BuildSchedule(row);
                lstSchedules.Add((T)Convert.ChangeType(schedule, typeof(T)));
            }

        return lstSchedules;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_SCHEDULE_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retSchedule = (T)Convert.ChangeType(BuildSchedule(row), typeof(T));
            return retSchedule;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var schedule = baseDto as Schedule;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "UPD_SCHEDULE_PR";

        sqlOperation.AddIntParam("P_Id", schedule.Id);
        sqlOperation.AddIntParam("P_EmployeeId", schedule.EmployeeId);
        sqlOperation.AddStringParam("P_DaysOfWeek", schedule.DaysOfWeek);
        sqlOperation.AddTimeParam("P_TimeOfEntry", schedule.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", schedule.TimeOfExit);
        sqlOperation.AddDateTimeParam("P_Created", schedule.Created);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public Schedule RetrieveScheduleByUserID(int id)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "RET_SCHEDULE_BYUSERID_PR";
        sqlOperation.AddIntParam("P_EmployeeId", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retSchedule = BuildSchedule(row);
            return retSchedule;
        }

        return default;
    }

    #region Funciones extras

    private Schedule BuildSchedule(Dictionary<string, object> row)
    {
        var scheduleToReturn = new Schedule
        {
            Id = (int)row["id"],
            EmployeeId = (int)row["employee_id"],
            DaysOfWeek = (string)row["days_of_week"],
            TimeOfEntry = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_entry"]),
            TimeOfExit = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_exit"]),
            Created = (DateTime)row["created"]
        };
        return scheduleToReturn;
    }

    #endregion
}