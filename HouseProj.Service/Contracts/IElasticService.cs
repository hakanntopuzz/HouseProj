using System.Collections.Generic;
using HouseProj.Domain;

namespace HouseProj.Service.Contracts
{
    public interface IElasticService
    {
        bool BulkIndex(string indexName, List<Product> productList);
        bool CreateIndex(string indexName);
        IEnumerable<Product> SearchProduct(string indexName, string keyword);
    }
}
