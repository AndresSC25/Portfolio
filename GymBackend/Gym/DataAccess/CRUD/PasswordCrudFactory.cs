using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class PasswordCrudFactory : CrudFactory
{
    public PasswordCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        // Conversión del DTO base a Password
        var password = baseDto as Password;

        // Crear el instructivo para que el DAO pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        // Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_PASSWORD_PR";

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_UserId", password.UserId);
        sqlOperation.AddStringParam("P_PasswordContent", password.PasswordContent);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        // Conversión del DTO base a Password
        var password = baseDto as Password;

        // Crear el instructivo para que el DAO pueda realizar un delete en la base de datos
        var sqlOperation = new SqlOperation();

        // Set del nombre del procedimiento
        sqlOperation.ProcedureName = "DEL_PASSWORD_PR";

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_Id", password.Id);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public void DeleteByUserId(BaseDTO baseDto)
    {
        var user = baseDto as User;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_USER_PASS_PR"
        };

        sqlOperation.AddIntParam("P_Id", user.Id);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstPasswords = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_PASSWORDS_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var password = BuildPassword(row);
                lstPasswords.Add((T)Convert.ChangeType(password, typeof(T)));
            }

        return lstPasswords;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_PASSWORD_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        // Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retPassword = (T)Convert.ChangeType(BuildPassword(row), typeof(T));
            return retPassword;
        }

        return default;
    }

    public List<Password> RetrievePasswordsById(int id)
    {
        var sqlOperation = new SqlOperation
        {
            ProcedureName = "RE_LAST_5_PASSWORDS_BY_ID"
        };
        sqlOperation.AddIntParam("P_IdUser", id);
        var listaResultados = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        var passwords = new List<Password>();
        foreach (var row in listaResultados)
        {
            var readPassword = BuildPassword(row);
            passwords.Add(readPassword);
        }

        return passwords;
    }

    public override void Update(BaseDTO baseDto)
    {
        // Conversión del DTO base a Password
        var password = baseDto as Password;

        // Crear el instructivo para que el DAO pueda realizar un update en la base de datos
        var sqlOperation = new SqlOperation();

        // Set del nombre del procedimiento
        sqlOperation.ProcedureName = "UPD_PASSWORD_PR";

        // Agregamos los parámetros
        sqlOperation.AddIntParam("P_Id", password.Id);
        sqlOperation.AddIntParam("P_UserId", password.UserId);
        sqlOperation.AddStringParam("P_PasswordContent", password.PasswordContent);
        sqlOperation.AddDateTimeParam("P_Created", password.Created);

        // Ir al DAO a ejecutar
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Password BuildPassword(Dictionary<string, object> row)
    {
        var passwordToReturn = new Password
        {
            Id = (int)row["id"],
            UserId = (int)row["user_id"],
            PasswordContent = (string)row["password"],
            Created = (DateTime)row["created"]
        };
        return passwordToReturn;
    }

    #endregion
}