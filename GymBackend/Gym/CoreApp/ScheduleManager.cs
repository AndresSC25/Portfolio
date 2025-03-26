using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class ScheduleManager
{
    public void Create(Schedule schedule)
    {
        var sCrud = new ScheduleCrudFactory();
        // Validaciones adicionales
        sCrud.Create(schedule);
    }

    public void Update(Schedule schedule)
    {
        var sCrud = new ScheduleCrudFactory();
        sCrud.Update(schedule);
    }

    public void Delete(Schedule schedule)
    {
        var sCrud = new ScheduleCrudFactory();
        sCrud.Delete(schedule);
    }

    public void DeleteByUserId(int userId)
    {
        var sCrud = new ScheduleCrudFactory();
        sCrud.DeleteByUserId(userId);
    }

    public List<Schedule> RetrieveAll()
    {
        var sCrud = new ScheduleCrudFactory();
        return sCrud.RetrieveAll<Schedule>();
    }

    public Schedule RetrieveById(int id)
    {
        var sCrud = new ScheduleCrudFactory();
        return sCrud.RetrieveById<Schedule>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}