using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserRolManager
{
    public void Create(UserRole userRole)
    {
        var urCrud = new UserRoleFactory();
        if (AlreadyHave(userRole)) throw new Exception("El usuario ya tiene asignado el rol que se ingreso");
        if (urCrud.RetrieveByUserId(userRole.UserId) == null) throw new Exception("El Usuario ingresado no existe");
        urCrud.Create(userRole);

        if (!string.IsNullOrEmpty(userRole.DaysOfWeek))
        {
            var newSchedule = new Schedule
            {
                DaysOfWeek = userRole.DaysOfWeek,
                TimeOfEntry = userRole.TimeOfEntry ?? default(TimeOnly),
                TimeOfExit = userRole.TimeOfExit ?? default(TimeOnly),
                EmployeeId = userRole.UserId
            };
            var sM = new ScheduleManager();
            sM.Create(newSchedule);
        }
    }

    public void Update(UserRole userRole)
    {
        //No necesario por ahora preguntar
    }

    public void Delete(UserRole userRole)
    {
        var urCrud = new UserRoleFactory();
        urCrud.Delete(userRole);

        if (!string.IsNullOrEmpty(userRole.DaysOfWeek))
        {
            var sM = new ScheduleManager();
            sM.DeleteByUserId(userRole.UserId);
        }
    }

    public List<UserRole> RetrieveAll()
    {
        var urCrud = new UserRoleFactory();
        return urCrud.RetrieveAll<UserRole>();
    }

    public UserRole RetrieveById(int id)
    {
        var urCrud = new UserRoleFactory();
        return urCrud.RetrieveById<UserRole>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    public bool AlreadyHave(UserRole userRole)
    {
        var rCrud = new RoleCrudFactory();
        if (rCrud.RetrieveAllRolesByUserId(userRole.UserId) == null)
            return false;
        foreach (var item in rCrud.RetrieveAllRolesByUserId(userRole.UserId))
            if (userRole.RoleId == item.Id)
                return true;
        return false;
    }

    #endregion
}