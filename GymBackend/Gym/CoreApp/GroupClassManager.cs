using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class GroupClassManager
{
    public void Create(GroupClass groupClass)
    {
        GeneralValidations(groupClass);
        ScheduleValidations(groupClass);
        var gcCrud = new GroupClassCrudFactory();
        gcCrud.Create(groupClass);
    }


    public void Update(GroupClass groupClass)
    {
        GeneralValidations(groupClass);
        ScheduleValidations(groupClass);
        var gcCrud = new GroupClassCrudFactory();
        gcCrud.Update(groupClass);
    }

    public void Delete(GroupClass groupClass)
    {
        var DateToday = DateTime.Now;
        if (groupClass.ClassDate <= DateToday) throw new Exception("No se puede eliminar una clase que ya pasó.");
        var gcCrud = new GroupClassCrudFactory();
        var uGcCrud = new UserGroupClassCrudFactory();
        var lstUserGroupClass = uGcCrud.RetrieveByGroupClassId(groupClass.Id);
        foreach (var grpClass in lstUserGroupClass) uGcCrud.Delete(grpClass);
        gcCrud.Delete(groupClass);
    }

    public List<GroupClass> RetrieveAll()
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveAll<GroupClass>();
    }

    public GroupClass RetrieveById(int id)
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveById<GroupClass>(id);
    }

    public List<GroupClass> RetrieveAllWithName()
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveAllWithName();
    }

    public List<GroupClass> RetrieveAvailableWithName()
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveAvailableWithName();
    }

    // Aquí irían las validaciones

    #region Validations

    // Aquí puedes agregar validaciones adicionales para GroupClass, como asegurarte de que las fechas y horas sean válidas, etc.
    public void GeneralValidations(GroupClass groupClass)
    {
        if (groupClass.ClassName == "") throw new Exception("Por favor ingrese un nombre");
        if (groupClass.EmployeeId == 0) throw new Exception("Por favor elige un entrenador.");
        if (groupClass.MaxCapacity <= 0) throw new Exception("Por favor elige la capacidad máxima para la clase.");
        if (groupClass.MaxCapacity > 40)
            throw new Exception(
                "El area de clases grupales tiene un espacio para máximo 40 personas, por elige otra cantidad.");
        var today = DateTime.Now;
        var date = new DateTime(2000, 1, 1);
        if (groupClass.ClassDate.Date == date.Date) throw new Exception("Por favor elige una fecha.");
        if (groupClass.ClassDate.Date <= today.Date)
            throw new Exception("La clases se deben programar a futuro con 1 día de anticipación.");
        var time = new TimeOnly(0, 0, 0);
        if (groupClass.StartTime == time) throw new Exception("Por favor ingrega una hora de entrada.");
        if (groupClass.EndTime == time) throw new Exception("Por favor ingrega una hora de salida.");
        if (groupClass.EndTime <= groupClass.StartTime)
            throw new Exception("La hora de inicio debe ser antes que la hora de salida.");
        var duracion = groupClass.EndTime - groupClass.StartTime;
        if (duracion.TotalMinutes < 30) throw new Exception("La clase debe durar al menos 30 minutos.");
    }

    private void ScheduleValidations(GroupClass groupClass)
    {
        if (!WorksDayOfWeek(groupClass))
            throw new Exception("Lo sentimos, el entrenador no trabaja este día de la semana.");

        if (!IsInWorkHours(groupClass)) throw new Exception("Lo sentimos, el entrenador no trabaja a estas horas.");

        if (!NoGroupClassInterrupt(groupClass))
            throw new Exception("Lo sentimos, el entrenador da una clase grupal a esta hora.");

        if (!NoMeasureAppointmentInterrupt(groupClass))
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas.");

        if (!NoPersonalTrainingInterrupt(groupClass))
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas.");
    }

    public bool WorksDayOfWeek(GroupClass groupClass)
    {
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(groupClass.EmployeeId);
        var daySchedule = schedule.DaysOfWeek;
        var dayWeekProg = groupClass.ClassDate.DayOfWeek;

        var daysMap = new Dictionary<char, DayOfWeek>
        {
            { 'L', DayOfWeek.Monday },
            { 'K', DayOfWeek.Tuesday },
            { 'M', DayOfWeek.Wednesday },
            { 'J', DayOfWeek.Thursday },
            { 'V', DayOfWeek.Friday },
            { 'S', DayOfWeek.Saturday },
            { 'D', DayOfWeek.Sunday }
        };

        return daySchedule.Any(day => daysMap.TryGetValue(day, out var mappedDay) && mappedDay == dayWeekProg);
    }

    public bool IsInWorkHours(GroupClass groupClass)
    {
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(groupClass.EmployeeId);
        var start = schedule.TimeOfEntry;
        var end = schedule.TimeOfExit;
        var startProg = groupClass.StartTime;
        var endProg = groupClass.EndTime;

        return startProg >= start && endProg <= end;
    }

    public bool NoGroupClassInterrupt(GroupClass groupClass)
    {
        var gcCrud = new GroupClassCrudFactory();
        var lstAllGC = gcCrud.RetrieveByUserId(groupClass.EmployeeId);

        return !lstAllGC.Any(gc =>
            gc.ClassDate.Date == groupClass.ClassDate.Date && gc.Id != groupClass.Id &&
            ((groupClass.StartTime < gc.EndTime && groupClass.StartTime >= gc.StartTime) ||
             (groupClass.EndTime <= gc.EndTime && groupClass.EndTime > gc.StartTime))
        );
    }

    public bool NoMeasureAppointmentInterrupt(GroupClass groupClass)
    {
        var mCrud = new MeetingsCrudFactory();
        var lstAllMeetings = mCrud.RetrieveByUserId(groupClass.EmployeeId);

        return !lstAllMeetings.Any(m =>
            m.ProgrammedDate.Date == groupClass.ClassDate.Date &&
            ((groupClass.StartTime < m.TimeOfExit && groupClass.StartTime >= m.TimeOfEntry) ||
             (groupClass.EndTime <= m.TimeOfExit && groupClass.EndTime > m.TimeOfEntry))
        );
    }

    public bool NoPersonalTrainingInterrupt(GroupClass groupClass)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        var lstAllPT = ptCrud.RetrieveByEmployeeId(groupClass.EmployeeId);

        return !lstAllPT.Any(pt =>
            pt.ProgrammedDate.Date == groupClass.ClassDate.Date &&
            ((groupClass.StartTime < pt.TimeOfExit && groupClass.StartTime >= pt.TimeOfEntry) ||
             (groupClass.EndTime <= pt.TimeOfExit && groupClass.EndTime > pt.TimeOfEntry))
        );
    }

    #endregion
}