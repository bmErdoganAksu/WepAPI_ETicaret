using Business.Abstract;
using Core.Helpers;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class ComputerService : IComputerService
{
    private readonly IComputerDal _computerDal;
    private readonly N11Scraper _n11Scraper;
    private readonly TeknosaScraper _teknosaScraper;
    private readonly EpeyScraper _epeyScraper;
    public ComputerService(IComputerDal computerDal, N11Scraper n11Scraper, TeknosaScraper teknosaScraper, EpeyScraper epeyScraper)
    {
        _computerDal = computerDal;
        _n11Scraper = n11Scraper;
        _teknosaScraper = teknosaScraper;
        _epeyScraper = epeyScraper;
    }

    public async Task<IResult> Add(Computer computer)
    {
        await _computerDal.Add(computer);
        var result = await _computerDal.SaveChanges();
        return result ? new SuccessResult("Ekleme Başarılı") : new ErrorResult("Ekleme İşlemi Başarısız");
    }

    public async Task<IDataResult<List<Computer>>> GetAll()
    {
        var result = await _computerDal.GetAll();
        return new SuccessDataResult<List<Computer>>(result,"Veriler Başarıyla Çekildi"); 
    }

    public async Task<IDataResult<Computer>> GetById(int id)
    {
        var computer = await _computerDal.GetById(id);
        return new SuccessDataResult<Computer>(computer);
    }

    public async Task<IDataResult<List<Computer>>> GetFromN11(int pageNumber)
    {
        var computers = await _n11Scraper.GetComputers(pageNumber);
        return new SuccessDataResult<List<Computer>>(computers,"Veri Çekme İşlemi Başarılı");
    }

    public async Task<IDataResult<List<Computer>>> GetFromTeknosa(int pageNumber)
    {
        var computers = await _teknosaScraper.GetComputers(pageNumber);
        return new SuccessDataResult<List<Computer>>(computers, "Veri Çekme İşlemi Başarılı");
    }

    public async Task<IDataResult<List<Computer>>> GetFromEpey(int pageNumber)
    {
        var computers = await _epeyScraper.GetComputers(pageNumber);
        await _computerDal.BulkAdd(computers);
        return new SuccessDataResult<List<Computer>>(computers, "Veriler Başarıyla Kaydedildi");
    }

    public async Task<IDataResult<List<Computer>>> Search(string term)
    {
        var computers = await _computerDal.Search(term);
        return new SuccessDataResult<List<Computer>>(computers, "Veriler Başarıyla Kaydedildi");

    }

    public async Task<IResult> Update(Computer computer)
    {
        var c = await _computerDal.GetById(computer.Id);
        c.Brand = computer.Brand;
        c.DiscSpace = computer.DiscSpace;
        c.ProcessorGeneration = computer.ProcessorGeneration;
        c.OS = computer.OS;
        c.Point = computer.Point;
        c.ScrapeLink = computer.ScrapeLink;
        c.ScreenSize = computer.ScreenSize;
        //c.PriceByStore = computer.PriceByStore;
        c.Ram = computer.Ram;
        c.Caption = computer.Caption;
        c.DiscType = computer.DiscType;
        c.ModelName = computer.ModelName;
        c.ModelNo = computer.ModelNo;
        c.ProcessorType = computer.ProcessorType;
        var result = await _computerDal.SaveChanges();
        return result ? new SuccessResult("Güncelleme Başarılı") : new ErrorResult("Güncelleme İşlemi Başarısız");
    }

    public async Task<IResult> UpdatePrice(PriceByStore priceByStore)
    {
        var p = await _computerDal.GetPriceById(priceByStore.Id);
        p.Price = priceByStore.Price;
        var result = await _computerDal.SaveChanges();
        return result ? new SuccessResult("Fiyat Güncelleme Başarılı") : new ErrorResult("Fiyat Güncelleme İşlemi Başarısız");

    }

    public async Task<IResult> DeletePrice(int id)
    {
        await _computerDal.DeletePrice(id);
        var result = await _computerDal.SaveChanges();
        return result ? new SuccessResult("Fiyat Silme Başarılı") : new ErrorResult("Fiyat Silme İşlemi Başarısız");
    }

    public async Task<IResult> DeleteComputer(int id)
    {
        await _computerDal.DeleteComputer(id);
        var result = await _computerDal.SaveChanges();
        return result ? new SuccessResult("Bilgisayar Silme Başarılı") : new ErrorResult("Bilgisayar Silme İşlemi Başarısız");
    }
}
