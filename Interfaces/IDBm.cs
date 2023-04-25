using core.Models;

namespace core.Interfaces;
public interface IDBm
{
    Task<dynamic> FindAll();
    Task<dynamic> Insert(DBManagerObject obj);
    Task<dynamic> Upsert(dynamic obj);
    Task<dynamic> FindById(DBManagerObject obj);
    Task<dynamic> DeleteById(DBManagerObject obj);

}
