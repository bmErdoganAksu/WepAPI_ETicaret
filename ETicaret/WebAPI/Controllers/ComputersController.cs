using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputersController : ControllerBase
{
    private readonly IComputerService _computerService;

    public ComputersController(IComputerService computerService)
    {
        _computerService = computerService;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll() 
    {
        var result = await _computerService.GetAll();
        return Ok(result);
    }


    [HttpGet("ScrapeFromN11/{pageNumber}")]
    public async Task<IActionResult> GetFromN11(int pageNumber) 
    {
        var result = await _computerService.GetFromN11(pageNumber);
        return Ok(result);
    }


    [HttpGet("ScrapeFromTeknosa/{pageNumber}")]
    public async Task<IActionResult> GetFromTeknosa(int pageNumber)
    {
        var result = await _computerService.GetFromTeknosa(pageNumber);
        return Ok(result);
    }


    [HttpGet("ScrapeFromEpey/{pageNumber}")]
    public async Task<IActionResult> GetFromEpey(int pageNumber)
    {
        var result = await _computerService.GetFromEpey(pageNumber);
        return Ok(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _computerService.GetById(id);
        return Ok(result);
    }

    [HttpGet("Search/{term}")]
    public async Task<IActionResult> Search(string term)
    {
        var result = await _computerService.Search(term);
        return Ok(result);
    }


    [HttpPost("Update")]
    public async Task<IActionResult> Update(Computer computer)
    {
        var result = await _computerService.Update(computer);
        return Ok(result);
    }

    [HttpPost("UpdatePrice")]
    public async Task<IActionResult> UpdatePrice(PriceByStore priceByStore)
    {
        var result = await _computerService.UpdatePrice(priceByStore);
        return Ok(result);
    }

    [HttpPost("DeletePrice/{id}")]
    public async Task<IActionResult> DeletePrice(int id)
    {
        var result = await _computerService.DeletePrice(id);
        return Ok(result);
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(Computer computer)
    {
        var result = await _computerService.Add(computer);
        return Ok(result);
    }

    [HttpPost("DeleteComputer/{id}")]
    public async Task<IActionResult> DeleteComputer(int id)
    {
        var result = await _computerService.DeleteComputer(id);
        return Ok(result);
    }
}
