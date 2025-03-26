using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class UserRoleFactory : CrudFactory
{
    public UserRoleFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var userRole = baseDto as UserRole;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_USER_ROL_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("P_User_ID", userRole.UserId);
        sqlOperation.AddIntParam("P_Rol_ID", userRole.RoleId);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Update(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        //var userRole = baseDto as UserRole;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        //var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        //sqlOperation.ProcedureName = "UPD_USER_ROL_PR";

        //Agregamos los parametros
        //sqlOperation.AddIntParam("P_User_ID", userRole.UserId);
        //sqlOperation.AddIntParam("P_Rol_ID", userRole.RoleId);

        //Ir al DAO a ejecutor
        //_sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        var userRol = baseDto as UserRole;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_USER_ROL_PR"
        };

        sqlOperation.AddIntParam("P_Id", userRol.Id);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public void DeleteByUserId(BaseDTO baseDto)
    {
        var user = baseDto as User;

        var sqlOperation = new SqlOperation
        {
            ProcedureName = "DEL_USER_ROL_PR"
        };

        sqlOperation.AddIntParam("P_Id", user.Id);


        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_USER_ROL_BY_ID_PR" };
        sqlOperation.AddIntParam("P_ID", id);

        //Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retUserRol = (T)Convert.ChangeType(BuildUserRole(row), typeof(T));
            return retUserRol;
        }

        return default;
    }

    public List<Rol> RetrieveByUserId(int userId)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_BY_IDUSER_PR" };
        sqlOperation.AddIntParam("@P_ID_User", userId);

        //Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        var roles = new List<Rol>();
        foreach (var row in lstResults)
        {
            var readRole = BuildRole(row);
            roles.Add(readRole);
        }

        return roles;
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstUserRole = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_USER_ROL_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var userRole = BuildUserRole(row);
                lstUserRole.Add((T)Convert.ChangeType(userRole, typeof(T)));
            }

        return lstUserRole;
    }

    private UserRole BuildUserRole(Dictionary<string, object> row)
    {
        var userRoleToReturn = new UserRole
        {
            Id = (int)row["id"],
            UserId = (int)row["user_id"],
            RoleId = (int)row["rol_id"]
        };
        return userRoleToReturn;
    }

    private Rol BuildRole(Dictionary<string, object> row)
    {
        var roleToReturn = new Rol
        {
            Id = (int)row["id"],
            Name = (string)row["name"]
        };
        return roleToReturn;
    }
}