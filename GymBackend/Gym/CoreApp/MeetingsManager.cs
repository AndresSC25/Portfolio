using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class MeetingsManager
{
    public void Create(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();

        // Aquí se valida la reunión antes de crearla
        ValidateMeeting(meeting);

        mCrud.Create(meeting);
    }

    public void Update(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        mCrud.Update(meeting);
    }

    public void Delete(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        mCrud.Delete(meeting);
    }

    public List<Meetings> RetrieveAll()
    {
        var mCrud = new MeetingsCrudFactory();
        return mCrud.RetrieveAll<Meetings>();
    }

    public Meetings RetrieveById(int id)
    {
        var mCrud = new MeetingsCrudFactory();
        return mCrud.RetrieveById<Meetings>(id);
    }

    public List<Meetings> RetrieveAllWithName()
    {
        var mCrud = new MeetingsCrudFactory();
        return mCrud.RetrieveAllWithName();
    }

    #region Validations

    private void ValidateMeeting(Meetings meeting)
    {
        if (meeting.ClientId <= 0) throw new Exception("Por favor selecciona un cliente.");

        if (meeting.EmployeeId <= 0) throw new Exception("Por favor selecciona un entrenador.");

        if (meeting.TimeOfEntry == default) throw new Exception("Por favor ingresa una hora de entrada.");

        if (meeting.TimeOfExit == default) throw new Exception("Por favor ingresa una hora de salida.");

        if (meeting.TimeOfEntry >= meeting.TimeOfExit)
            throw new Exception("La hora de salida no puede ser antes que la hora de entrada.");

        if (meeting.ProgrammedDate == default) throw new Exception("Por favor elige una fecha.");

        if (meeting.ProgrammedDate.Date <= DateTime.Now.Date)
            throw new Exception("La cita debe ser una fecha en el futuro.");

        if (!WorksDayOfWeek(meeting))
            throw new Exception("Lo sentimos, el entrenador no trabaja este día de la semana.");

        if (!IsInWorkHours(meeting)) throw new Exception("Lo sentimos, el entrenador no trabaja a estas horas.");

        if (!NoGroupClassInterrupt(meeting))
            throw new Exception("Lo sentimos, el entrenador da una clase grupal a esta hora.");

        if (!NoMeasureAppointmentInterrupt(meeting))
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas.");

        if (!NoPersonalTrainingInterrupt(meeting))
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas.");
    }

    public bool WorksDayOfWeek(Meetings meeting)
    {
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        var daySchedule = schedule.DaysOfWeek;
        var dayWeekProg = meeting.ProgrammedDate.DayOfWeek;

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

    public bool IsInWorkHours(Meetings meeting)
    {
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        var start = schedule.TimeOfEntry;
        var end = schedule.TimeOfExit;
        var startProg = meeting.TimeOfEntry;
        var endProg = meeting.TimeOfExit;

        return startProg >= start && endProg <= end;
    }

    public bool NoGroupClassInterrupt(Meetings meeting)
    {
        var gcCrud = new GroupClassCrudFactory();
        var lstAllGC = gcCrud.RetrieveByUserId(meeting.EmployeeId);

        return !lstAllGC.Any(gc =>
            gc.ClassDate.Date == meeting.ProgrammedDate.Date &&
            ((meeting.TimeOfEntry < gc.EndTime && meeting.TimeOfEntry >= gc.StartTime) ||
             (meeting.TimeOfExit <= gc.EndTime && meeting.TimeOfExit > gc.StartTime))
        );
    }

    public bool NoMeasureAppointmentInterrupt(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        var lstAllMeetings = mCrud.RetrieveByUserId(meeting.EmployeeId);

        return !lstAllMeetings.Any(m =>
            m.ProgrammedDate.Date == meeting.ProgrammedDate.Date &&
            ((meeting.TimeOfEntry < m.TimeOfExit && meeting.TimeOfEntry >= m.TimeOfEntry) ||
             (meeting.TimeOfExit <= m.TimeOfExit && meeting.TimeOfExit > m.TimeOfEntry))
        );
    }

    public bool NoPersonalTrainingInterrupt(Meetings meeting)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        var lstAllPT = ptCrud.RetrieveByEmployeeId(meeting.EmployeeId);

        return !lstAllPT.Any(pt =>
            pt.ProgrammedDate.Date == meeting.ProgrammedDate.Date &&
            ((meeting.TimeOfEntry < pt.TimeOfExit && meeting.TimeOfEntry >= pt.TimeOfEntry) ||
             (meeting.TimeOfExit <= pt.TimeOfExit && meeting.TimeOfExit > pt.TimeOfEntry))
        );
    }

    public void CancelMeeting(Meetings meeting)
    {
        if (meeting.Id == 0) throw new Exception("Por favor elige una cita.");

        var currentDateTime = DateTime.Now;
        var meetingEndDateTime = new DateTime(meeting.ProgrammedDate.Year, meeting.ProgrammedDate.Month,
            meeting.ProgrammedDate.Day,
            meeting.TimeOfExit.Hour, meeting.TimeOfExit.Minute, meeting.TimeOfExit.Second);

        if (currentDateTime > meetingEndDateTime) throw new Exception("No se puede cancelar una cita que ya ocurrió.");

        var mCrud = new MeetingsCrudFactory();
        mCrud.CancelMeeting(meeting);
    }

    #endregion
}