using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserGroupClassManager
{
    public void Create(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        var gcCrud = new GroupClassCrudFactory();
        var umManager = new UserMembershipManager();
        var mCrud = new MembershipCrudFactory();
        // Validaciones adicionales
        var groupClass = gcCrud.RetrieveById<GroupClass>(userGroupClass.GroupClassId);

        if (userGroupClass.GroupClassId == 0) throw new Exception("Por favor elija una clase de la tabla.");
        if (groupClass.CurrentRegistered == groupClass.MaxCapacity)
            throw new Exception("Lo sentimos, esta clase esta llena.");

        //Logica para verificar si el usuario ya esta registrado en la clase
        var alreadyRegistered = ugcCrud.RetrieveByGroupClassId(userGroupClass.GroupClassId);
        foreach (var check in alreadyRegistered)
            if (check.ClientId == userGroupClass.ClientId)
                throw new Exception("Lo sentimos, usted ya esta registrado a esta clase.");

        //Logica para ver a cuantas clases se ha registrado 
        var sum = 0;


        var uMemberships = umManager.RetrieveByUserId(userGroupClass.ClientId);

        UserMembership Newest = new();

        if (uMemberships.Count != 0)
        {
            Newest = uMemberships[0];
            foreach (var uMembership in uMemberships)
                if (uMembership.Created > Newest.Created)
                    Newest = uMembership;
        }

        var membership = mCrud.RetrieveById<Membership>(Newest.MembershipId);
        var UserEnlisted = ugcCrud.RetrieveAll<UserGroupClass>();

        if (membership.AmountClassesAllowed == 0)
            throw new Exception(
                "Lo sentimos, la membresía básica no tiene acceso a la clases grupales. Mejora tu membresía a Regular o Premium para poder registrarte.");

        foreach (var check in UserEnlisted)
            if (check.ClientId == userGroupClass.ClientId && check.Created >= Newest.Created)
                sum += 1;

        if (sum >= membership.AmountClassesAllowed)
            throw new Exception(
                "Lo sentimos, ha llegado al límite de clases mensual. Mejora tu membresía a Premium para poder registrarte a más clases.");

        groupClass.CurrentRegistered += 1;
        gcCrud.Update(groupClass);
        ugcCrud.Create(userGroupClass);
    }

    public void Update(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        ugcCrud.Update(userGroupClass);
    }

    public void Delete(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        var gcCrud = new GroupClassCrudFactory();

        var groupClass = gcCrud.RetrieveById<GroupClass>(userGroupClass.GroupClassId);

        //Verificar si el usuario se encuentra registrado en la clase
        var alreadyRegistered = ugcCrud.RetrieveByGroupClassId(userGroupClass.GroupClassId);
        var isInClass = false;
        var toDelete = new UserGroupClass();

        foreach (var check in alreadyRegistered)
            if (check.ClientId == userGroupClass.ClientId)
            {
                isInClass = true;
                toDelete = check;
            }

        if (isInClass)
        {
            ugcCrud.Delete(toDelete);
            groupClass.CurrentRegistered -= 1;
            gcCrud.Update(groupClass);
        }
        else
        {
            throw new Exception("Lo sentimos, usted no se encuentra registrado en esta clase.");
        }
    }

    public List<UserGroupClass> RetrieveAll()
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveAll<UserGroupClass>();
    }

    public UserGroupClass RetrieveById(int id)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveById<UserGroupClass>(id);
    }

    public List<UserGroupClass> RetrieveByGroupClassWithName(int groupClassId)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveByGroupClassWithName(groupClassId);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}