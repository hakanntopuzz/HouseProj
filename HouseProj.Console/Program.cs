using System.Configuration;
using HouseProj.Data;
using HouseProj.Service;
using HouseProj.Service.Contracts;

namespace HouseProj.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] urls = {"http://www.evidea.com/duzen/c/103693", "http://www.evidea.com/banyo/c/13", "http://www.evidea.com/aydinlatma/c/3" };
            IHtmlParseService htmlParseService = new HtmlParseService();
            IElasticService elasticService = new ElasticService(new ElasticContext());

            foreach (string url in urls)
            {
                htmlParseService.SetProducts(url);
            }

            #region ElasticSearch

            var indexName = ConfigurationManager.AppSettings["ElasticSearchIndexName"];

            elasticService.CreateIndex(indexName);

            var productList = htmlParseService.GetProducts();
            elasticService.BulkIndex(indexName, productList);
            #endregion
        }
    }
}
