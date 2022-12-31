using Entities.Concrete;
using HtmlAgilityPack;

namespace Core.Helpers;

public class N11Scraper
{
    private static readonly string _url = "https://www.n11.com/searchCategoryForPagination/1000271?&pg=";
    private HtmlWeb _htmlWeb;
    private HtmlDocument _htmlDoc;
    public N11Scraper()
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
            var nodes = _htmlDoc.DocumentNode.SelectNodes("//*[@class='pro']/a");
            foreach (var node in nodes)
            {
                links.Add(node.GetAttributeValue("href", string.Empty));                
            }
        }

        foreach (var link in links) 
        {
            _htmlDoc = _htmlWeb.Load(link.Trim());
            computers.Add(new Computer() { 
                ModelName = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"proName\"]").InnerText.Trim(),
                ProcessorType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][1]/strong").InnerText.Trim(),
                Ram = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][2]/strong").InnerText.Trim(),
                Brand = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][3]/strong").InnerText.Trim(),
                ScreenSize= _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][4]/strong").InnerText.Trim(),
                OS = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][5]/strong").InnerText.Trim(),
                DiscType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][6]/strong")?.InnerText.Replace("\n", "") ?? "",
                DiscSpace = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][12]/strong")?.InnerText.Replace("\n", "") ?? "",
                ModelNo = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][10]/strong")?.InnerText.Replace("\n", "") ?? "",
                Point = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][9]/strong")?.GetAttributeValue("data-average", string.Empty).Replace("\n", "") ?? "",
                ScrapeLink = link,
                Caption = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][10]/strong")?.InnerText.Replace("\n", "") ?? "",
                ProductImage = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][11]/strong")?.GetAttributeValue("src", string.Empty) ?? "",
                ProcessorGeneration = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"unf-attribute-label\"][12]/strong")?.InnerText.Replace("\n", "") ?? "",

            });
        }


        return computers;
    }
}
