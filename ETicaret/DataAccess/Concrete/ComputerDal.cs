using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class ComputerDal : IComputerDal
{
    private readonly AppDbContext _appDbContext;

    public ComputerDal(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Add(Computer computer)
    {
        await _appDbContext.Computers.AddAsync(computer);
    }

    public async Task BulkAdd(List<Computer> computers)
    {
        await _appDbContext.Computers.AddRangeAsync(computers);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteComputer(int id)
    {
        var comp = await _appDbContext.Computers.Where(s => s.Id == id).FirstOrDefaultAsync();
        _appDbContext.Computers.Remove(comp);
    }

    public async Task DeletePrice(int id)
    {
        var price = await _appDbContext.PriceByStores.Where(s => s.Id == id).FirstOrDefaultAsync();
        _appDbContext.PriceByStores.Remove(price);
    }

    public async Task<List<Computer>> GetAll()
    {
        return await _appDbContext.Computers.Include(s => s.PriceByStore.OrderBy(s => s.Price)).ToListAsync();
    }

    public async Task<Computer> GetById(int id)
    {
        return await _appDbContext.Computers.Where(s => s.Id == id).Include(s => s.PriceByStore.OrderBy(s => s.Price)).FirstOrDefaultAsync();
    }

    public async Task<PriceByStore> GetPriceById(int id)
    {
        return await _appDbContext.PriceByStores.Where(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChanges()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<Computer>> Search(string term)
    {
        return await _appDbContext.Computers.Include(s => s.PriceByStore)
            .Where(s =>
            s.Caption.Contains(term) ||
            s.ModelName.Contains(term) ||
            s.PriceByStore.Any(s => s.StoreName.Contains(term))
            )
            .ToListAsync();
    }
}
