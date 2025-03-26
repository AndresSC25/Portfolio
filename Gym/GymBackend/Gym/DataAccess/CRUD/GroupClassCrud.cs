using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class GroupClassCrudFactory : CrudFactory
{
    public GroupClassCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var groupClass = baseDto as GroupClass;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_GROUP_CLASS_PR"
        };

        sqlOperation.AddIntParam("P_EmployeeId", groupClass.EmployeeId);
        sqlOperation.AddStringParam("P_ClassName", groupClass.ClassName);
        sqlOperation.AddIntParam("P_MaxCapacity", groupClass.MaxCapacity);
        sqlOperation.AddIntParam("P_CurrentRegistered", groupClass.CurrentRegistered);
        sqlOperation.AddDateTimeParam("P_ClassDate", groupClass.ClassDate);
        sqlOperation.AddTimeParam("P_StartTime", groupClass.StartTime);
        sqlOperation.AddTimeParam("P_EndTime", groupClass.EndTime);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var groupClass = baseDto as GroupClass;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_GROUP_CLASS_PR"
        };

        sqlOperation.AddIntParam("P_Id", groupClass.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstGroupClasses = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_GROUP_CLASSES_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var groupClass = BuildGroupClass(row);
                lstGroupClasses.Add((T)Convert.ChangeType(groupClass, typeof(T)));
            }

        return lstGroupClasses;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_GROUP_CLASS_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retGroupClass = (T)Convert.ChangeType(BuildGroupClass(row), typeof(T));
            return retGroupClass;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var groupClass = baseDto as GroupClass;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_GROUP_CLASS_PR"
        };

        sqlOperation.AddIntParam("P_Id", groupClass.Id);
        sqlOperation.AddIntParam("P_EmployeeId", groupClass.EmployeeId);
        sqlOperation.AddStringParam("P_ClassName", groupClass.ClassName);
        sqlOperation.AddIntParam("P_MaxCapacity", groupClass.MaxCapacity);
        sqlOperation.AddIntParam("P_CurrentRegistered", groupClass.CurrentRegistered);
        sqlOperation.AddDateTimeParam("P_ClassDate", groupClass.ClassDate);
        sqlOperation.AddTimeParam("P_StartTime", groupClass.StartTime);
        sqlOperation.AddTimeParam("P_EndTime", groupClass.EndTime);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public List<GroupClass> RetrieveAllWithName()
    {
        var lstGroupClasses = new List<GroupClass>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_GROUP_CLASSES_W_NAME_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var groupClass = BuildGroupClassWithName(row);
                lstGroupClasses.Add(groupClass);
            }

        return lstGroupClasses;
    }

    private GroupClass BuildGroupClassWithName(Dictionary<string, object> row)
    {
        var groupClassToReturn = new GroupClass

        {
            Id = (int)row["id"],
            EmployeeId = (int)row["employee_id"],
            ClassName = (string)row["class_name"],
            MaxCapacity = (int)row["max_capacity"],
            CurrentRegistered = (int)row["current_registered"],
            ClassDate = (DateTime)row["class_time"],
            StartTime = TimeOnly.FromTimeSpan((TimeSpan)row["start_time"]),
            EndTime = TimeOnly.FromTimeSpan((TimeSpan)row["end_time"]),
            EmployeeName = (string)row["full_name"]
        };
        return groupClassToReturn;
    }

    public List<GroupClass> RetrieveAvailableWithName()
    {
        var lstGroupClasses = new List<GroupClass>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_AVA_GROUP_CLASSES_W_NAME_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var groupClass = BuildGroupClassWithName(row);
                lstGroupClasses.Add(groupClass);
            }

        return lstGroupClasses;
    }

    #region Funciones extras

    public List<GroupClass> RetrieveByUserId(int id)
    {
        List<GroupClass> lstGroupClasses = [];
        var sqlOperation = new SqlOperation { ProcedureName = "RET_GROUP_CLASSES_BYUSERID_PR" };
        sqlOperation.AddIntParam("P_Id", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var groupClass = BuildGroupClass(row);
                lstGroupClasses.Add(groupClass);
            }

        return lstGroupClasses;
    }

    private GroupClass BuildGroupClass(Dictionary<string, object> row)
    {
        var groupClassToReturn = new GroupClass

        {
            Id = (int)row["id"],
            EmployeeId = (int)row["employee_id"],
            ClassName = (string)row["class_name"],
            MaxCapacity = (int)row["max_capacity"],
            CurrentRegistered = (int)row["current_registered"],
            ClassDate = (DateTime)row["class_time"],
            StartTime = TimeOnly.FromTimeSpan((TimeSpan)row["start_time"]),
            EndTime = TimeOnly.FromTimeSpan((TimeSpan)row["end_time"])
        };
        return groupClassToReturn;
    }

    #endregion
}