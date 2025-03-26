using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class DetailCrudFactory : CrudFactory
{
    public DetailCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var detail = baseDto as Detail;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "CRE_DETAIL_PR"
        };

        sqlOperation.AddIntParam("P_InvoiceId", detail.InvoiceId);
        sqlOperation.AddIntParam("P_UserMembershipId", detail.UserMembershipId);
        sqlOperation.AddIntParam("P_PersonalTrainingId", detail.PersonalTrainingId);
        sqlOperation.AddDoubleParam("P_Price", detail.Price);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var detail = baseDto as Detail;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_DETAIL_PR"
        };

        sqlOperation.AddIntParam("P_Id", detail.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }


    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstDetails = new List<T>();
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_ALL_DETAILS_PR"
        };

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var detail = BuildDetail(row);
                lstDetails.Add((T)Convert.ChangeType(detail, typeof(T)));
            }

        return lstDetails;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RET_DETAIL_BY_ID_PR"
        };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retDetail = (T)Convert.ChangeType(BuildDetail(row), typeof(T));
            return retDetail;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var detail = baseDto as Detail;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "UPD_DETAIL_PR"
        };

        sqlOperation.AddIntParam("P_Id", detail.Id);
        sqlOperation.AddIntParam("P_InvoiceId", detail.InvoiceId);
        sqlOperation.AddIntParam("P_UserMembershipId", detail.UserMembershipId);
        sqlOperation.AddIntParam("P_PersonalTrainingId", detail.PersonalTrainingId);
        sqlOperation.AddDoubleParam("P_Price", detail.Price);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Helper Methods

    private Detail BuildDetail(Dictionary<string, object> row)
    {
        var detailToReturn = new Detail
        {
            Id = (int)row["id"],
            InvoiceId = (int)row["invoice_id"],
            UserMembershipId = (int)row["user_membership_id"],
            PersonalTrainingId = (int)row["personal_training_id"],
            Price = (double)(decimal)row["price"],
            Created = (DateTime)row["created"]
        };
        return detailToReturn;
    }

    #endregion
}