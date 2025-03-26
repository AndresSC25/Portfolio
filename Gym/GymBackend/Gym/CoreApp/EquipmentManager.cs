using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class EquipmentManager
{
    public void Create(Equipment equipment)
    {
        //Validaciones adicionales
        if (equipment.Name == "empty") throw new Exception("Por favor ingresa el nombre.");
        if (equipment.Location == "empty") throw new Exception("Por favor ingresa la ubicación.");
        if (equipment.Description == "empty") throw new Exception("Por favor ingresa la descripción.");
        var eCrud = new EquipmentCrudFactory();
        eCrud.Create(equipment);
    }

    public void Update(Equipment equipment)
    {
        //Validaciones adicionales
        if (equipment.Id == 0) throw new Exception("Por favor elige un equipo de la tabla.");

        var eCrud = new EquipmentCrudFactory();
        eCrud.Update(equipment);
    }

    public void Delete(Equipment equipment)
    {
        var eCrud = new EquipmentCrudFactory();
        eCrud.Delete(equipment);
    }

    public List<Equipment> RetrieveAll()
    {
        var eCrud = new EquipmentCrudFactory();
        return eCrud.RetrieveAll<Equipment>();
    }

    public Equipment RetrieveById(int id)
    {
        var eCrud = new EquipmentCrudFactory();
        return eCrud.RetrieveById<Equipment>(id);
    }


    //Aqui irian las validaciones

    #region Validations

    #endregion
}