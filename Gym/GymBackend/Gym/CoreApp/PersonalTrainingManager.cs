using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class PersonalTrainingManager
{
    public void Create(PersonalTraining personalTraining)
    {
        ValidatePersonalTraining(personalTraining);

        if (!IsTrainingScheduleValid(personalTraining))
            throw new Exception("La cita no puede ser programada debido a conflictos de horario o días no laborables.");

        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Create(personalTraining);
    }

    public void Update(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Update(personalTraining);
    }

    public void Cancel(PersonalTraining personalTraining)
    {
        // Validar el objeto antes de proceder con la cancelación
        ValidateCancelPersonalTraining(personalTraining);

        // Si todas las validaciones pasan, proceder con la cancelación
        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Cancel(personalTraining);
    }

    public void Delete(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Delete(personalTraining);
    }

    public List<PersonalTraining> RetrieveAll()
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveAll<PersonalTraining>();
    }

    public PersonalTraining RetrieveById(int id)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveById<PersonalTraining>(id);
    }

    public List<PersonalTraining> RetrieveByEmployeeId(int id)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveByEmployeeId(id);
    }

    public List<PersonalTraining> RetrieveByClientId(int id)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveByClientId(id);
    }

    public List<PersonalTraining> RetrieveByClientIdPayable(int id)
    {
        //Esta es la logica para ver cuales entrenamientos personales se deben de pagar 
        // a la hora de renovar la membresia.

        var ptCrud = new PersonalTrainingCrudFactory();
        var msManager = new UserMembershipManager();
        var lastMembership = msManager.RetrieveNewestByUserId(id);
        var personalTrainings = ptCrud.RetrieveByClientId(id);
        var payable = new List<PersonalTraining>();
        foreach (var personalTraining in personalTrainings)
        {
            var dateOnly = new DateOnly(personalTraining.ProgrammedDate.Year, personalTraining.ProgrammedDate.Month,
                personalTraining.ProgrammedDate.Day);
            var ptDate = new DateTime(dateOnly, personalTraining.TimeOfEntry);
            if (ptDate >= lastMembership.Created && ptDate < DateTime.Now && personalTraining.IsCancelled == "no" &&
                personalTraining.IsPaid == "no") payable.Add(personalTraining);
        }

        return payable;
    }


    #region Validations

    private void ValidatePersonalTraining(PersonalTraining personalTraining)
    {
        // Validación: EmployeeId debe ser válido (distinto de cero)
        if (personalTraining.EmployeeId <= 0) throw new Exception("Por favor selecciona un entrenador de la tabla.");

        // Validación: TimeOfEntry y TimeOfExit no deben ser "00:00:00" y TimeOfExit no puede ser mayor a TimeOfEntry
        if (personalTraining.TimeOfEntry == TimeOnly.MinValue || personalTraining.TimeOfExit == TimeOnly.MinValue)
            throw new Exception("Las horas de entrada y salida deben ser válidas y diferentes a 00:00:00.");

        if (personalTraining.TimeOfExit <= personalTraining.TimeOfEntry)
            throw new Exception("La hora de salida debe ser mayor que la hora de entrada.");

        // Obtener la fecha y hora actual
        var currentDateTime = DateTime.Now;

        // Validación: ProgrammedDate debe ser una fecha válida y no en el pasado (mínimo un día en el futuro)
        if (personalTraining.ProgrammedDate <= currentDateTime.AddDays(1))
            throw new Exception("La fecha programada debe ser al menos un día en el futuro.");

        if (personalTraining.HourlyRate < 2500)
            throw new Exception(
                "La tarifa por hora mínima es de ₡2500, por favor ingresa la tarifa acordada con el entrenador.");
    }

    private bool IsTrainingScheduleValid(PersonalTraining personalTraining)
    {
        if (!WorksDayOfWeek(personalTraining)) throw new Exception("El entrenador no trabaja el día programado.");

        if (!IsInWorkHours(personalTraining))
            throw new Exception("El entrenamiento personal está fuera de las horas laborales del entrenador.");

        if (!NoGroupClassInterrupt(personalTraining))
            throw new Exception("El entrenador da una clase grupal a esta hora.");

        if (!NoMeasureAppointmentInterrupt(personalTraining))
            throw new Exception("El entrenador ya tiene programada una cita de medición a esta hora.");

        if (!NoPersonalTrainingInterrupt(personalTraining))
            throw new Exception("El entrenador ya tiene programada una cita de entrenamiento personal a esta hora.");

        return true;
    }

    public bool WorksDayOfWeek(PersonalTraining personalTraining)
    {
        // Esta funcion verifica que la cita para entrenamiento personal sea en un dia de la semana donde el entrenador si trabaja
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(personalTraining.EmployeeId);
        var DaySchedule = schedule.DaysOfWeek;
        var DayWeekProg = personalTraining.ProgrammedDate.DayOfWeek;

        foreach (var day in DaySchedule)
            if ((day == 'L' && DayWeekProg == DayOfWeek.Monday) ||
                (day == 'K' && DayWeekProg == DayOfWeek.Tuesday) ||
                (day == 'M' && DayWeekProg == DayOfWeek.Wednesday) ||
                (day == 'J' && DayWeekProg == DayOfWeek.Thursday) ||
                (day == 'V' && DayWeekProg == DayOfWeek.Friday) ||
                (day == 'S' && DayWeekProg == DayOfWeek.Saturday) ||
                (day == 'D' && DayWeekProg == DayOfWeek.Sunday))
                return true;
        return false;
    }

    public bool IsInWorkHours(PersonalTraining personalTraining)
    {
        // Esta funcion verifica que el entrenamiento personal sea dentro de las horas laborales del entrenador
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(personalTraining.EmployeeId);
        var start = schedule.TimeOfEntry;
        var end = schedule.TimeOfExit;
        var startProg = personalTraining.TimeOfEntry;
        var endProg = personalTraining.TimeOfExit;
        if (startProg >= start && endProg <= end) return true;
        return false;
    }

    public bool NoGroupClassInterrupt(PersonalTraining personalTraining)
    {
        var gcCrud = new GroupClassCrudFactory();
        var lstAllGC = gcCrud.RetrieveByUserId(personalTraining.EmployeeId);
        var lstSameDate = lstAllGC.Where(gc => gc.ClassDate.Date == personalTraining.ProgrammedDate.Date).ToList();

        return !lstSameDate.Any(groupClass => IsTimeOverlap(personalTraining.TimeOfEntry, personalTraining.TimeOfExit,
            groupClass.StartTime, groupClass.EndTime));
    }

    public bool NoMeasureAppointmentInterrupt(PersonalTraining personalTraining)
    {
        var mCrud = new MeetingsCrudFactory();
        var lstAllMeetings = mCrud.RetrieveByUserId(personalTraining.EmployeeId);
        var lstSameDate = lstAllMeetings
            .Where(meeting => meeting.ProgrammedDate.Date == personalTraining.ProgrammedDate.Date).ToList();

        return !lstSameDate.Any(meeting => IsTimeOverlap(personalTraining.TimeOfEntry, personalTraining.TimeOfExit,
            meeting.TimeOfEntry, meeting.TimeOfExit));
    }

    public bool NoPersonalTrainingInterrupt(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        var lstAllPT = ptCrud.RetrieveByEmployeeId(personalTraining.EmployeeId);
        var lstSameDate = lstAllPT.Where(pt => pt.ProgrammedDate.Date == personalTraining.ProgrammedDate.Date).ToList();

        return !lstSameDate.Any(pt =>
            IsTimeOverlap(personalTraining.TimeOfEntry, personalTraining.TimeOfExit, pt.TimeOfEntry, pt.TimeOfExit));
    }

    private bool IsTimeOverlap(TimeOnly start1, TimeOnly end1, TimeOnly start2, TimeOnly end2)
    {
        return (start1 < end2 && start1 >= start2) || (end1 <= end2 && end1 > start2);
    }

    private void ValidateCancelPersonalTraining(PersonalTraining personalTraining)
    {
        // Validación: Id debe ser válido (distinto de cero)
        if (personalTraining.Id <= 0) throw new Exception("Por favor selecciona una cita a cancelar");

        // Obtener la fecha y hora actual
        var currentDateTime = DateTime.Now;

        // Crear el DateTime completo de la cita usando ProgrammedDate y TimeOfEntry
        var appointmentDateTime = personalTraining.ProgrammedDate.Add(personalTraining.TimeOfEntry.ToTimeSpan());

        // Validación: Verificar si la cita ya pasó
        if (appointmentDateTime < currentDateTime)
            throw new Exception("No se puede cancelar una cita que ya ha pasado.");

        // Validación: Verificar si se está cancelando con al menos 24 horas de anticipación
        var cancellationDeadline = appointmentDateTime.AddHours(-24);
        if (currentDateTime > cancellationDeadline)
            throw new Exception("La cita debe cancelarse con al menos 24 horas de anticipación.");
    }

    #endregion
}