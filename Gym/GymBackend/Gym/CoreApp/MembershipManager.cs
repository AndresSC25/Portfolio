using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class MembershipManager
{
    public void Create(Membership membership)
    {
        var meCrud = new MembershipCrudFactory();

        if (!IsValidType(membership)) throw new Exception("Tipo de membresía no válido.");

        if (!IsValidAmountClassesAllowed(membership)) throw new Exception("Cantidad de clases permitidas no válida.");

        if (!IsValidMonthlyCost(membership)) throw new Exception("Costo mensual no válido.");

        meCrud.Create(membership);
    }

    public void Update(Membership membership)
    {
        var meCrud = new MembershipCrudFactory();

        if (!IsValidType(membership)) throw new Exception("Tipo de membresía no válido.");

        if (!IsValidAmountClassesAllowed(membership)) throw new Exception("Cantidad de clases permitidas no válida.");

        if (!IsValidMonthlyCost(membership)) throw new Exception("Costo mensual no válido.");

        meCrud.Update(membership);
    }

    public void Delete(Membership membership)
    {
        var meCrud = new MembershipCrudFactory();
        meCrud.Delete(membership);
    }

    public List<Membership> RetrieveAll()
    {
        var meCrud = new MembershipCrudFactory();
        return meCrud.RetrieveAll<Membership>();
    }

    public Membership RetrieveById(int id)
    {
        var meCrud = new MembershipCrudFactory();
        return meCrud.RetrieveById<Membership>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    public bool IsValidType(Membership membership)
    {
        return !string.IsNullOrWhiteSpace(membership.Type) &&
               membership.Type.Length >= 2 &&
               membership.Type.Length <= 50;
    }

    public bool IsValidAmountClassesAllowed(Membership membership)
    {
        return membership.AmountClassesAllowed >= 0;
    }

    public bool IsValidMonthlyCost(Membership membership)
    {
        return membership.MonthlyCost >= 0;
    }

    public bool IsValidMembership(Membership membership)
    {
        return IsValidType(membership) &&
               IsValidAmountClassesAllowed(membership) &&
               IsValidMonthlyCost(membership);
    }

    #endregion
}