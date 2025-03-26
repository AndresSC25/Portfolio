using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class EquipmentCrudFactory : CrudFactory
{
    public EquipmentCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var equipment = baseDto as Equipment;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_EQUIPMENT_PR";

        //Agregamos los parametros
        sqlOperation.AddStringParam("P_Name", equipment.Name);
        sqlOperation.AddStringParam("P_Location", equipment.Location);
        sqlOperation.AddStringParam("P_Description", equipment.Description);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override void Delete(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var equipment = baseDto as Equipment;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "DEL_EQUIPMENT_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("P_Id", equipment.Id);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        var lstEquipments = new List<T>();
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_EQUIPMENTS_PR" };
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
            foreach (var row in lstResults)
            {
                var equipment = BuildEquipment(row);
                lstEquipments.Add((T)Convert.ChangeType(equipment, typeof(T)));
            }

        return lstEquipments;
    }

    public override T RetrieveById<T>(int id)
    {
        var sqlOperation = new SqlOperation { ProcedureName = "RET_EQUIPMENT_BY_ID_PR" };
        sqlOperation.AddIntParam("P_Id", id);

        //Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retProduct = (T)Convert.ChangeType(BuildEquipment(row), typeof(T));
            return retProduct;
        }

        return default;
    }

    public override void Update(BaseDTO baseDto)
    {
        //Conversion del DTO base a equipment
        var equipment = baseDto as Equipment;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "UPD_EQUIPMENT_PR";

        //Agregamos los parametros
        sqlOperation.AddIntParam("P_Id", equipment.Id);
        sqlOperation.AddStringParam("P_Name", equipment.Name);
        sqlOperation.AddStringParam("P_Description", equipment.Description);
        sqlOperation.AddStringParam("P_Location", equipment.Location);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
    }

    #region Funciones extras

    private Equipment BuildEquipment(Dictionary<string, object> row)
    {
        var equipmentToReturn = new Equipment
        {
            Id = (int)row["id"],
            Name = (string)row["name"],
            Location = (string)row["location"],
            Description = (string)row["description"],
            Created = (DateTime)row["created"]
        };
        return equipmentToReturn;
    }

    #endregion
}