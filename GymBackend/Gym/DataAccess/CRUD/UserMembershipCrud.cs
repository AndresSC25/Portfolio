using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class UserMembershipCrud : CrudFactory
{
    public UserMembershipCrud()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var userMembership = baseDto as UserMembership;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_USER_MEMBERSHIP_PR"
        };

        sqlOperation.AddIntParam("P_UserId", userMembership.UserId);
        sqlOperation.AddIntParam("P_MembershipId", userMembership.MembershipId);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var userMembership = baseDto as UserMembership;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_USER_MEMBERSHIP_PR"
        };

        sqlOperation.AddIntParam("P_Id", userMembership.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstUserMemberships = new List<T>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_ALL_USER_MEMBERSHIP_PR"
        };

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userMembership = BuildUserMembership(row);
                lstUserMemberships.Add((T)Convert.ChangeType(userMembership, typeof(T)));
            }

        return lstUserMemberships;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_USER_MEMBERSHIP_BY_ID_PR"
        };

        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retUserMembership = (T)Convert.ChangeType(BuildUserMembership(row), typeof(T));
            return retUserMembership;
        }

        return default;
    }

    public List<UserMembership> RetrieveByUserId(int userId)
    {
        var lstUserMemberships = new List<UserMembership>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_USER_MEMBERSHIP_BY_USERID_PR"
        };
        sqlOperation.AddIntParam("P_UserId", userId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userMembership = BuildUserMembership(row);
                lstUserMemberships.Add(userMembership);
            }

        return lstUserMemberships;
    }

    public List<UserMembership> RetrieveByUserIdStatusChange(int userId)
    {
        var lstUserMemberships = new List<UserMembership>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_USER_MEMBERSHIP_BY_USERID_STATUS_V_PR"
        };
        sqlOperation.AddIntParam("P_UserId", userId);
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userMembership = BuildUserMembership(row);
                lstUserMemberships.Add(userMembership);
            }

        return lstUserMemberships;
    }

    public override void Update(BaseDTO baseDto)
    {
        var userMembership = baseDto as UserMembership;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_USER_MEMBERSHIP_PR"
        };

        sqlOperation.AddIntParam("P_Id", userMembership.Id);
        sqlOperation.AddIntParam("P_UserId", userMembership.UserId);
        sqlOperation.AddIntParam("P_MembershipId", userMembership.MembershipId);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private UserMembership BuildUserMembership(Dictionary<string, object> row)
    {
        var userMembershipToReturn = new UserMembership
        {
            Id = (int)row["id"],
            UserId = (int)row["user_id"],
            MembershipId = (int)row["membership_id"],
            Created = (DateTime)row["created"]
        };
        return userMembershipToReturn;
    }

    #endregion
}