using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class UserGroupClassCrudFactory : CrudFactory
{
    public UserGroupClassCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var userGroupClass = baseDto as UserGroupClass;
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_USER_GROUP_CLASS_PR";

        sqlOperation.AddIntParam("P_GroupClassId", userGroupClass.GroupClassId);
        sqlOperation.AddIntParam("P_ClientId", userGroupClass.ClientId);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var userGroupClass = baseDto as UserGroupClass;
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "DEL_USER_GROUP_CLASS_PR";

        sqlOperation.AddIntParam("P_Id", userGroupClass.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }


    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstUserGroupClasses = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_USER_GROUP_CLASSES_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userGroupClass = BuildUserGroupClass(row);
                lstUserGroupClasses.Add((T)Convert.ChangeType(userGroupClass, typeof(T)));
            }


        return lstUserGroupClasses;
    }

    public List<UserGroupClass> RetrieveByGroupClassId(int groupClassId)
    {
        var lstUserGroupClasses = new List<UserGroupClass>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_USER_GROUP_CLASS_BY_GROUP_CLASS_ID_PR" };
        sqlOperation.AddIntParam("P_GP_Id", groupClassId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userGroupClass = BuildUserGroupClass(row);
                lstUserGroupClasses.Add(userGroupClass);
            }

        return lstUserGroupClasses;
    }

    public List<UserGroupClass> RetrieveByGroupClassWithName(int groupClassId)
    {
        var lstUserGroupClasses = new List<UserGroupClass>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_USER_GROUP_CLASS_WITH_NAME_BY_GROUP_CLASS_ID_PR" };
        sqlOperation.AddIntParam("P_GP_Id", groupClassId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userGroupClass = BuildUserGroupClassWithName(row);
                lstUserGroupClasses.Add(userGroupClass);
            }

        return lstUserGroupClasses;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_USER_GROUP_CLASS_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retUserGroupClass = (T)Convert.ChangeType(BuildUserGroupClass(row), typeof(T));
            return retUserGroupClass;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var userGroupClass = baseDto as UserGroupClass;
        var sqlOperation = new SqlOperation();

        sqlOperation.ProcedureName = "UPD_USER_GROUP_CLASS_PR";

        sqlOperation.AddIntParam("P_Id", userGroupClass.Id);
        sqlOperation.AddIntParam("P_GroupClassId", userGroupClass.GroupClassId);
        sqlOperation.AddIntParam("P_ClientId", userGroupClass.ClientId);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private UserGroupClass BuildUserGroupClass(Dictionary<string, object> row)
    {
        var userGroupClassToReturn = new UserGroupClass
        {
            Id = (int)row["id"],
            GroupClassId = (int)row["group_class_id"],
            ClientId = (int)row["client_id"],
            Created = (DateTime)row["created"]
        };
        return userGroupClassToReturn;
    }

    private UserGroupClass BuildUserGroupClassWithName(Dictionary<string, object> row)
    {
        var userGroupClassToReturn = new UserGroupClass
        {
            Id = (int)row["id"],
            GroupClassId = (int)row["group_class_id"],
            ClientId = (int)row["client_id"],
            Created = (DateTime)row["created"],
            ClientName = row.ContainsKey("client_name") ? row["client_name"].ToString() : null
        };
        return userGroupClassToReturn;
    }

    #endregion
}