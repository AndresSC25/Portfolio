using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class RolManager
{
    public void Create(Rol rol)
    {
        var rCrud = new RoleCrudFactory();
        // Validaciones adicionales
        rCrud.Create(rol);
    }

    public void Update(Rol rol)
    {
        var rCrud = new RoleCrudFactory();
        rCrud.Update(rol);
    }

    public void Delete(int idUsuario, int idRol)
    {
        var uCrud = new UserCrudFactory();
        if (uCrud.RetrieveById<User>(idUsuario) == null) throw new Exception("Usuario por eliminar rol no existe");

        if (AlreadyDeleted(idUsuario, idRol))
            throw new Exception("No se puede eliminar por que el usuario no tiene ese rol");
        var rCrud = new RoleCrudFactory();
        rCrud.DeleteById(idUsuario, idRol);
        var sM = new ScheduleManager();
        sM.DeleteByUserId(idUsuario);
    }

    public List<Rol> RetrieveAll()
    {
        var uRol = new RoleCrudFactory();
        return uRol.RetrieveAll<Rol>();
    }

    public Rol RetrieveById(int id)
    {
        var rCrud = new RoleCrudFactory();
        return rCrud.RetrieveById<Rol>(id);
    }

    public List<Rol> RetrieveAllRolesByUserId(int userId)
    {
        var rCrud = new RoleCrudFactory();
        return rCrud.RetrieveAllRolesByUserId(userId);
    }

    // Aquí irían las validaciones

    #region Validations

    public bool AlreadyDeleted(int id, int rolId)
    {
        var rCrud = new RoleCrudFactory();
        if (rCrud.RetrieveAllRolesByUserId(id) == null)
            return true;
        foreach (var item in rCrud.RetrieveAllRolesByUserId(id))
            if (rolId == item.Id)
                return false;
        return true;
    }

    #endregion
}