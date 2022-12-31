using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IComputerDal
{
    Task Add(Computer computer);
    Task BulkAdd(List<Computer> computers);
    Task<Computer> GetById(int id);
    Task<PriceByStore> GetPriceById(int id);
    Task<List<Computer>> GetAll();
    Task<List<Computer>> Search(string term);
    Task<bool> SaveChanges();
    Task DeletePrice(int id);
    Task DeleteComputer(int id);
}
