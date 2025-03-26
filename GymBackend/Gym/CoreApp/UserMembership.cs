using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserMembershipManager
{
    public void Create(UserMembership userMembership)
    {
        ValidateUserMembership(userMembership);

        var umCrud = new UserMembershipCrud();
        umCrud.Create(userMembership);
    }

    public void Update(UserMembership userMembership)
    {
        var umCrud = new UserMembershipCrud();
        umCrud.Update(userMembership);
    }

    public void Delete(UserMembership userMembership)
    {
        var umCrud = new UserMembershipCrud();
        umCrud.Delete(userMembership);
    }

    public List<UserMembership> RetrieveAll()
    {
        var umCrud = new UserMembershipCrud();
        return umCrud.RetrieveAll<UserMembership>();
    }

    public UserMembership RetrieveById(int id)
    {
        var umCrud = new UserMembershipCrud();
        return umCrud.RetrieveById<UserMembership>(id);
    }

    public List<UserMembership> RetrieveByUserId(int userId)
    {
        ValidateId(userId);

        var umCrud = new UserMembershipCrud();
        var Memberships = umCrud.RetrieveByUserId(userId);
        return Memberships;
    }

    public UserMembership RetrieveNewestByUserId(int userId)
    {
        ValidateId(userId);

        var umCrud = new UserMembershipCrud();
        var Memberships = umCrud.RetrieveByUserId(userId);
        UserMembership Newest = new();
        if (Memberships.Count != 0)
        {
            Newest = Memberships[0];
            foreach (var membership in Memberships)
                if (membership.Created > Newest.Created)
                    Newest = membership;
            ShouldPay(Newest);
        }

        return Newest;
    }

    public UserMembership RetrieveByUserIdStatusChange(int userId)
    {
        ValidateId(userId);

        var umCrud = new UserMembershipCrud();
        var Memberships = umCrud.RetrieveByUserIdStatusChange(userId);
        UserMembership Newest = new();
        if (Memberships.Count != 0)
        {
            Newest = Memberships[0];
            foreach (var membership in Memberships)
                if (membership.Created > Newest.Created)
                    Newest = membership;
        }

        return Newest;
    }

    private void ShouldPay(UserMembership newest)
    {
        // Calcula la fecha un mes después de la fecha de creación
        var oneMonthLater = newest.Created.AddMonths(1);

        // Compara la fecha actual con la fecha en la que vence la membresía
        if (DateTime.Now.Date < oneMonthLater.Date)
            throw new Exception("A este usuario aún no se le vence la membresía. Vence hasta el: " +
                                oneMonthLater.Date.ToShortDateString());
    }


    #region Validations

    private void ValidateId(int userId)
    {
        if (userId <= 0) throw new Exception("Por favor selecione un cliente.");
    }

    private void ValidateUserMembership(UserMembership userMembership)
    {
        if (userMembership.UserId <= 0) throw new Exception("Por favor selecione un cliente.");

        if (userMembership.MembershipId <= 0) throw new Exception("El ID de membresía no es válido.");
    }

    #endregion
}