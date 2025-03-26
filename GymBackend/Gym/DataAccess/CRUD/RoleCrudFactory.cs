using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class RoleCrudFactory : CrudFactory
{
    public RoleCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var rol = baseDto as Rol;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_ROL_PR";

        //Agregamos los parametros
        sqlOperation.AddStringParam("P_Name", rol.Name);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override void Delete(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int idUsuario, int idRol)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "DEL_ROL_PR" };
        sqlOperation.AddIntParam("P_IDUsuario", idUsuario);
        sqlOperation.AddIntParam("P_IDRol", idRol);
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ROL_BY_ID_PR" };
        sqlOperation.AddIntParam("P_ID", id);

        //Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retRol = (T)Convert.ChangeType(BuildRol(row), typeof(T));
            return retRol;
        }

        return default;
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstRoles = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var role = BuildRol(row);
                lstRoles.Add((T)Convert.ChangeType(role, typeof(T)));
            }

        return lstRoles;
    }

    public List<Rol> RetrieveAllRolesByUserId(int idUser)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_BY_IDUSER_PR" };
        sqlOperation.AddIntParam("P_ID_User", idUser);

        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count <= 0) return default;
        List<Rol> listaDeRolesMapeados = new();

        foreach (var item in lstResults)
        {
            var itemMapeado = BuildRol(item);
            listaDeRolesMapeados.Add(itemMapeado);
        }

        return listaDeRolesMapeados;
    }

    private Rol BuildRol(Dictionary<string, object> row)
    {
        var rolToReturn = new Rol
        {
            Id = (int)row["id"],
            Name = (string)row["name"]
        };
        return rolToReturn;
    }
}