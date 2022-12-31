using Entities.Abstract;

namespace Entities.Concrete;

public class Computer : IEntity
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string ModelName { get; set; }
    public string ModelNo { get; set; }
    public string Caption { get; set; }
    public string OS { get; set; }
    public string ProcessorType { get; set; }
    public string ProcessorGeneration { get; set; }
    public string Ram { get; set; }
    public string DiscSpace { get; set; }
    public string DiscType { get; set; }
    public string ScreenSize { get; set; }
    public string Point { get; set; }
    public List<PriceByStore> PriceByStore { get; set; } = new();
    public string ScrapeLink { get; set; }
    public string ProductImage { get; set; }
}
