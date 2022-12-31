using Entities.Abstract;

namespace Entities.Concrete;

public class PriceByStore : IEntity
{
    public int Id { get; set; }
    public string StoreName { get; set; }
    public double Price { get; set; }
    public string Link { get; set; }
}
