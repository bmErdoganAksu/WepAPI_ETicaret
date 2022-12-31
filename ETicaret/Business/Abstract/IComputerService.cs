using Core.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IComputerService
{
    Task<IResult> Add(Computer computer);
    Task<IDataResult<Computer>> GetById(int id);
    Task<IDataResult<List<Computer>>> GetAll();
    Task<IDataResult<List<Computer>>> GetFromN11(int pageNumber);
    Task<IDataResult<List<Computer>>> GetFromTeknosa(int pageNumber);
    Task<IDataResult<List<Computer>>> GetFromEpey(int pageNumber);
    Task<IDataResult<List<Computer>>> Search(string term);
    Task<IResult> Update(Computer computer);
    Task<IResult> UpdatePrice(PriceByStore priceByStore);
    Task<IResult> DeletePrice(int id);
    Task<IResult> DeleteComputer(int id);
}
