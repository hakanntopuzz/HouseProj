using System.Collections.Generic;
using System.Linq;
using HouseProj.Domain;
using HouseProj.Service.Contracts;
using HtmlAgilityPack;

namespace HouseProj.Service
{
    public class HtmlParseService : IHtmlParseService
    {
        private readonly List<Product> _productList;

        public HtmlParseService()
        {
            _productList = new List<Product>();
        }

        private HtmlDocument GetHtml(string url)
        {
            var web = new HtmlWeb();

            return web.Load(url);
        }

        public void SetProducts(string url)
        {
            HtmlDocument doc = GetHtml(url);

            var productNodes = doc.DocumentNode.SelectNodes("//*[contains(@class,'urunlist2_11')]");


            foreach (HtmlNode urun in productNodes)
            {
                string href = urun.SelectSingleNode("a").GetAttributeValue("href", string.Empty);

                if (!string.IsNullOrEmpty(href))
                {
                    href = $"http://www.evidea.com{href}";

                    HtmlDocument productDoc = GetHtml(href);

                    _productList.Add(new Product()
                    {
                        Name = productDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'urundetay_231')]").InnerText,
                        Price = productDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'current-price')]").InnerText,
                        Image = productDoc.DocumentNode.SelectSingleNode("//*[contains(@class,'elevatezoom-gallery')]").SelectSingleNode("img").GetAttributeValue("src",string.Empty)
                    });
                }
            }
        }

        public List<Product> GetProducts()
        {
            return _productList;
        }
    }
}
