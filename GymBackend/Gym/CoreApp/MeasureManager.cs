using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class MeasuresManager
{
    public void Create(Measures measure)
    {
        ValidateMeasure(measure);

        var mCrud = new MeasureCrud();
        mCrud.Create(measure);
    }

    public void Update(Measures measure)
    {
        var mCrud = new MeasureCrud();
        mCrud.Update(measure);
    }

    public void Delete(Measures measure)
    {
        var mCrud = new MeasureCrud();
        mCrud.Delete(measure);
    }

    public List<Measures> RetrieveAll()
    {
        var mCrud = new MeasureCrud();
        return mCrud.RetrieveAll<Measures>();
    }

    public Measures RetrieveById(int id)
    {
        var mCrud = new MeasureCrud();
        return mCrud.RetrieveById<Measures>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    public void ValidateMeasure(Measures measure)
    {
        if (measure.ClientId <= 0) throw new Exception("Por favor elige un cliente.");

        if (measure.Weight <= 0 || measure.Weight > 500)
            throw new Exception("El peso ingresado no es válido. Debe ser un valor entre 0 y 500 kg).");

        if (measure.Height <= 0 || measure.Height > 272)
            throw new Exception("La altura no es válida. Debe ser un valor entre 0 y 272 centímetros.");

        if (measure.AverageOfFat < 2 || measure.AverageOfFat > 80)
            throw new Exception("El porcentaje de grasa no es válido. Debe ser un valor entre 2% y 80%.");
    }

    public List<Measures> RetrieveByUserId(int id)
    {
        var mCrud = new MeasureCrud();
        return mCrud.RetrieveByUserId(id);
    }

    #endregion
}