using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class MeetingsCrudFactory : CrudFactory
{
    public MeetingsCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var meeting = baseDto as Meetings;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_MEETINGS_PR"
        };

        sqlOperation.AddIntParam("P_ClientId", meeting.ClientId);
        sqlOperation.AddIntParam("P_EmployeeId", meeting.EmployeeId);
        sqlOperation.AddTimeParam("P_TimeOfEntry", meeting.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", meeting.TimeOfExit);
        sqlOperation.AddDateTimeParam("P_ProgrammedDate", meeting.ProgrammedDate);
        sqlOperation.AddStringParam("P_IsCancelled", meeting.IsCancelled);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var meeting = baseDto as Meetings;
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_MEETINGS_PR"
        };

        sqlOperation.AddIntParam("P_Id", meeting.Id); // Asegúrate de que Meetings tenga un campo Id

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstMeetings = new List<T>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_ALL_MEETINGS_PR"
        };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var meeting = BuildMeeting(row);
                lstMeetings.Add((T)Convert.ChangeType(meeting, typeof(T)));
            }

        return lstMeetings;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_MEETING_BY_ID_PR"
        };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retMeeting = (T)Convert.ChangeType(BuildMeeting(row), typeof(T));
            return retMeeting;
        }

        return default;
    }

    public List<Meetings> RetrieveByUserId(int employeeId)
    {
        var lstGroupMeetings = new List<Meetings>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_MEETING_BYUSERID_PR" };
        sqlOperation.AddIntParam("P_Id", employeeId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var meeting = BuildMeeting(row);
                lstGroupMeetings.Add(meeting);
            }

        return lstGroupMeetings;
    }

    public override void Update(BaseDTO baseDto)
    {
        var meeting = baseDto as Meetings;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_MEETINGS_PR"
        };

        sqlOperation.AddIntParam("P_Id", meeting.Id); // Asegúrate de que Meetings tenga un campo Id
        sqlOperation.AddIntParam("P_ClientId", meeting.ClientId);
        sqlOperation.AddIntParam("P_EmployeeId", meeting.EmployeeId);
        sqlOperation.AddTimeParam("P_TimeOfEntry", meeting.TimeOfEntry);
        sqlOperation.AddTimeParam("P_TimeOfExit", meeting.TimeOfExit);
        sqlOperation.AddDateTimeParam("P_ProgrammedDate", meeting.ProgrammedDate);
        sqlOperation.AddStringParam("P_IsCancelled", meeting.IsCancelled);
        sqlOperation.AddDateTimeParam("P_Created", meeting.Created);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Meetings BuildMeeting(Dictionary<string, object> row)
    {
        var meetingToReturn = new Meetings
        {
            Id = (int)row["id"], // Asegúrate de que Meetings tenga un campo Id
            ClientId = (int)row["client_id"],
            EmployeeId = (int)row["employee_id"],
            TimeOfEntry = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_entry"]),
            TimeOfExit = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_exit"]),
            ProgrammedDate = (DateTime)row["programmed_date"],
            IsCancelled = (string)row["is_cancelled"],
            Created = (DateTime)row["created"]
        };
        return meetingToReturn;
    }

    private Meetings BuildMeetingWithName(Dictionary<string, object> row)
    {
        var meetingToReturn = new Meetings
        {
            Id = (int)row["id"], // Asegúrate de que Meetings tenga un campo Id
            ClientId = (int)row["client_id"],
            ClientName = (string)row["client_name"],
            EmployeeId = (int)row["employee_id"],
            EmployeeName = (string)row["employee_name"],
            TimeOfEntry = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_entry"]),
            TimeOfExit = TimeOnly.FromTimeSpan((TimeSpan)row["time_of_exit"]),
            ProgrammedDate = (DateTime)row["programmed_date"],
            IsCancelled = (string)row["is_cancelled"],
            Created = (DateTime)row["created"]
        };
        return meetingToReturn;
    }

    public List<Meetings> RetrieveAllWithName()
    {
        var lstMeetings = new List<Meetings>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_ALL_MEETINGS_W_NAMES_PR"
        };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var meeting = BuildMeetingWithName(row);
                lstMeetings.Add(meeting);
            }

        return lstMeetings;
    }

    public void CancelMeeting(Meetings meeting)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_CANCEL_MEETING_PR"
        };

        sqlOperation.AddIntParam("P_Id", meeting.Id); // Asegúrate de que Meetings tenga un campo Id

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #endregion
}