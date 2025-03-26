using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

//Padre abstracto de todos los crud
public abstract class CrudFactory
{
    protected SqlDao _sqlDao;

    //Definimos metodos que se deben implementar CREATE READ UPDATE DELETE
    public abstract void Create(BaseDTO baseDto);
    public abstract void Update(BaseDTO baseDto);
    public abstract void Delete(BaseDTO baseDto);
    public abstract T Retrieve<T>();
    public abstract T RetrieveById<T>(int id);
    public abstract List<T> RetrieveAll<T>();
}