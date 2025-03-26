using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class MembershipCrudFactory : CrudFactory
{
    public MembershipCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var membership = baseDto as Membership;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_MEMBERSHIP_PR";
        sqlOperation.AddStringParam("P_Type", membership.Type);
        sqlOperation.AddIntParam("P_AmountClassesAllowed", membership.AmountClassesAllowed);
        sqlOperation.AddDoubleParam("P_MonthlyCost", membership.MonthlyCost);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var membership = baseDto as Membership;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "DEL_MEMBERSHIP_PR";
        sqlOperation.AddIntParam("P_Id", membership.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstMemberships = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_MEMBERSHIPS_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var membership = BuildMembership(row);
                lstMemberships.Add((T)Convert.ChangeType(membership, typeof(T)));
            }

        return lstMemberships;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_MEMBERSHIP_BYID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retMembership = (T)Convert.ChangeType(BuildMembership(row), typeof(T));
            return retMembership;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var membership = baseDto as Membership;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "UPD_MEMBERSHIP_PR";
        sqlOperation.AddIntParam("P_Id", membership.Id);
        sqlOperation.AddStringParam("P_Type", membership.Type);
        sqlOperation.AddIntParam("P_AmountClassesAllowed", membership.AmountClassesAllowed);
        sqlOperation.AddDoubleParam("P_MonthlyCost", membership.MonthlyCost);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Membership BuildMembership(Dictionary<string, object> row)
    {
        var membershipToReturn = new Membership
        {
            Id = (int)row["id"],
            Type = (string)row["type"],
            AmountClassesAllowed = (int)row["amount_classes_allowed"],
            MonthlyCost = Convert.ToDouble(row["monthly_cost"]),
            Created = (DateTime)row["created"]
        };
        return membershipToReturn;
    }

    #endregion
}