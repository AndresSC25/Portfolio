using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class DiscountCrudFactory : CrudFactory
{
    public DiscountCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var discount = baseDto as Discount;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_DISCOUNT_PR";

        sqlOperation.AddStringParam("P_Type", discount.Type);
        sqlOperation.AddStringParam("P_Coupon", discount.Coupon);
        sqlOperation.AddIntParam("P_Percentage", discount.Percentage);
        sqlOperation.AddDateTimeParam("P_ValidFrom", discount.ValidFrom);
        sqlOperation.AddDateTimeParam("P_ValidTo", discount.ValidTo);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var discount = baseDto as Discount;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "DEL_DISCOUNT_PR";

        sqlOperation.AddIntParam("P_Id", discount.Id);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstDiscounts = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_DISCOUNTS_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var discount = BuildDiscount(row);
                lstDiscounts.Add((T)Convert.ChangeType(discount, typeof(T)));
            }

        return lstDiscounts;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_DISCOUNT_BYID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retDiscount = (T)Convert.ChangeType(BuildDiscount(row), typeof(T));
            return retDiscount;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        var discount = baseDto as Discount;

        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "UPD_DISCOUNT_PR";

        sqlOperation.AddIntParam("P_Id", discount.Id);
        sqlOperation.AddStringParam("P_Type", discount.Type);
        sqlOperation.AddStringParam("P_Coupon", discount.Coupon);
        sqlOperation.AddIntParam("P_Percentage", discount.Percentage);
        sqlOperation.AddDateTimeParam("P_ValidFrom", discount.ValidFrom);
        sqlOperation.AddDateTimeParam("P_ValidTo", discount.ValidTo);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Discount BuildDiscount(Dictionary<string, object> row)
    {
        var discountToReturn = new Discount
        {
            Id = (int)row["id"],
            Type = (string)row["type"],
            Coupon = (string)row["coupon"],
            Percentage = (int)row["percentage"],
            ValidFrom = (DateTime)row["valid_from"],
            ValidTo = (DateTime)row["valid_to"],
            Created = (DateTime)row["created"]
        };
        return discountToReturn;
    }

    #endregion
}