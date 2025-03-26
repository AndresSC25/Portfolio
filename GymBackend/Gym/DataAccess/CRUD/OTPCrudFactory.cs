using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class OTPCrudFactory : CrudFactory
{
    public OTPCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        var otp = baseDto as OTP;
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "CRE_OTP_PR";
        sqlOperation.AddIntParam("P_OTP", otp.OtpData);
        sqlOperation.AddIntParam("P_UserId", otp.UserId);

        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public void UpdateNew(int userId, string email, string phone, int otpData)
    {
        // Crear el instructivo para que el DAO pueda realizar un update en la base de datos
        var sqlOperation = new SqlOperation();

        // Set del nombre del procedimiento
        sqlOperation.ProcedureName = "UPD_OTP_PR";
        sqlOperation.AddIntParam("User_Id", userId);
        sqlOperation.AddStringParam("Email", email);
        sqlOperation.AddStringParam("Phone", phone);
        sqlOperation.AddIntParam("OTP", otpData);
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        throw new NotImplementedException();
    }

    private OTP BuildOtp(Dictionary<string, object> row)
    {
        var otpToReturn = new OTP
        {
            Id = (int)row["id"],
            OtpData = (int)row["otp"],
            UserId = (int)row["user_id"],
            ExpiredDate = (DateTime)row["expire_data"],
            WasUsed = (string)row["was_used"],
            Created = (DateTime)row["created"]
        };
        return otpToReturn;
    }
}