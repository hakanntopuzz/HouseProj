using System.Collections.Generic;
using HouseProj.Domain;

namespace HouseProj.Service.Contracts
{
    public interface IHtmlParseService
    {
        void SetProducts(string url);
        List<Product> GetProducts();
    }
}
