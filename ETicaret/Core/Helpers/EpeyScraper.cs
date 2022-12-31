using Entities.Concrete;
using HtmlAgilityPack;

namespace Core.Helpers;

public class EpeyScraper
{
    private static readonly string _url = "https://www.epey.com/laptop/e/YToxOntzOjQ6Im96ZWwiO2E6MTp7aTowO3M6Nzoic2F0aXN0YSI7fX1fTjs=/";
    private HtmlWeb _htmlWeb;
    private HtmlDocument _htmlDoc;
    public EpeyScraper()
    {
        _htmlWeb = new();

    }
    public async Task<List<Computer>> GetComputers(int pageNumber)
    {
        var computers = new List<Computer>();
        var links = new List<string>();
        for (int i = 1; i <= pageNumber; i++)
        {
            _htmlDoc = _htmlWeb.Load(_url + i);
            var nodes = _htmlDoc.DocumentNode.SelectNodes("//*[@class=\"detay cell\"]/a");
            foreach (var node in nodes)
            {
                links.Add(node.GetAttributeValue("href", string.Empty));
            }
        }

        foreach (var link in links)
        {
            _htmlDoc = _htmlWeb.Load(link.Trim());
            var computer = new Computer()
            {
                ModelName = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"baslik\"]/h1")?.InnerText.Trim().Replace("\n", "") ?? "",
                ProcessorType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1364\"]/span")?.InnerText.Trim().Replace("\n", "") ?? "",
                Ram = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1027\"]/span")?.InnerText.Trim().Replace("\n", "") ?? "",
                Brand = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1600\"]/span")?.InnerText.Trim().Replace("\n", "") ?? "",
                ScreenSize = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1426\"]/span")?.InnerText.Trim().Replace("\n", "") ?? "",
                OS = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1038\"]/span")?.InnerText.Trim().Replace("\n", "") ?? "",
                DiscType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id5936\"]/span")?.InnerText.Replace("\n", "") ?? "",
                DiscSpace = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1422\"]/span")?.InnerText.Replace("\n", "") ?? "",
                ModelNo = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1396\"]/span")?.InnerText.Replace("\n", "") ?? "",
                Point = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"basic\"]")?.GetAttributeValue("data-average", string.Empty).Replace("\n", "") ?? "",
                ScrapeLink = link,
                Caption = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"fiyat fiyat-15\"]/a/span[@class=\"urun_adi\"]")?.InnerText.Replace("\n", "") ?? "",
                ProductImage = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"galerim cell\"]/a/img")?.GetAttributeValue("src", string.Empty) ?? "",
                ProcessorGeneration = _htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"id1369\"]/span")?.InnerText.Replace("\n", "") ?? "",
            };
            var prices = _htmlDoc.DocumentNode.SelectNodes("//*[@class=\"fiyat fiyat-15\"]/a");
            if (prices is not null)
            {
                foreach (var price in prices)
                {
                    computer.PriceByStore.Add(new()
                    {
                        Price = Convert.ToDouble(price.SelectSingleNode("span[@class=\"urun_fiyat\"]").GetAttributeValue("data-sort", string.Empty)) / 100,
                        StoreName = price.SelectSingleNode("span[@class=\"site_logo\"]/img").GetAttributeValue("src", string.Empty),
                        Link = price.GetAttributeValue("data-link", string.Empty)
                    });
                }
            }
            if (computer.ModelName.Trim() != "")
                computers.Add(computer);
        }


        return computers;
    }
}
