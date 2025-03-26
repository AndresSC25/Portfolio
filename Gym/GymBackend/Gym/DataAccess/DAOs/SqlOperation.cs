using Microsoft.Data.SqlClient;

namespace DataAccess.DAOs;

public class SqlOperation

{
    public SqlOperation()
    {
        Parameters = new List<SqlParameter>();
    }

    public string ProcedureName { get; set; }
    public List<SqlParameter> Parameters { get; set; }

    public void AddStringParam(string paramName, string paramValue)
    {
        Parameters.Add(new SqlParameter(paramName, paramValue));
    }

    public void AddIntParam(string paramName, int? paramValue)
    {
        Parameters.Add(new SqlParameter(paramName, paramValue));
    }

    public void AddDateTimeParam(string paramName, DateTime paramValue)
    {
        Parameters.Add(new SqlParameter(paramName, paramValue));
    }

    public void AddDoubleParam(string paramName, double paramValue)
    {
        Parameters.Add(new SqlParameter(paramName, paramValue));
    }

    public void AddTimeParam(string paramName, TimeOnly paramValue)
    {
        Parameters.Add(new SqlParameter(paramName, paramValue));
    }
}