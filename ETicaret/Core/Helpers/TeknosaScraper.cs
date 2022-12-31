using Entities.Concrete;
using HtmlAgilityPack;

namespace Core.Helpers;

public class TeknosaScraper
{
    private static readonly string _url = "https://www.teknosa.com/laptop-notebook-c-116004?page=";
    private HtmlWeb _htmlWeb;
    private HtmlDocument _htmlDoc;
    public TeknosaScraper()
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
            var nodes = _htmlDoc.DocumentNode.SelectNodes("//*[@id='product-item']/a");
            foreach (var node in nodes)
            {
                links.Add("https://www.teknosa.com"+node.GetAttributeValue("href", string.Empty));                
            }
        }

        foreach (var link in links) 
        {
            _htmlDoc = _htmlWeb.Load(link.Trim());
            computers.Add(new Computer() { 
                ModelName = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"pdp-title\"]").InnerText.Trim(),
                ProcessorType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[4]/tbody/tr[2]/td[1]").InnerText.Trim(),
                Ram = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[6]/tbody/tr[2]/td[1]").InnerText.Trim(),
                Brand = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"pdp-title\"]/b").InnerText.Trim(),
                ScreenSize= _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[1]/tbody/tr[2]/td[3]").InnerText.Trim(),
                OS = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[5]/tbody/tr[2]/td[1]").InnerText.Trim(),
                DiscType = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[3]/tbody/tr[2]/td[4]").InnerText,
                DiscSpace = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[1]/tbody/tr[2]/td[1]").InnerText,
                ModelNo = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[2]/tbody/tr[2]/td[4]").InnerText,
                Point = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"bv_numReviews_text\"]").InnerText,
                //Price = Convert.ToDouble(_htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"prc prc-last\"]").InnerText),
                ScrapeLink = link,
                ProcessorGeneration = _htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"ptf-body\"]/table[4]/tbody/tr[2]/td[1]").InnerText,

            });
        }


        return computers;
    }
}
