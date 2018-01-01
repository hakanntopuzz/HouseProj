using System;
using System.Collections.Generic;
using HouseProj.Data.Contracts;
using HouseProj.Domain;
using HouseProj.Service.Contracts;
using Nest;

namespace HouseProj.Service
{
    public class ElasticService : IElasticService
    {
        private readonly IElasticContext _elasticContext;
        private readonly IQueryContainer QueryContainer;
        private readonly IQueryStringQuery QueryStringQuery;

        public ElasticService(IElasticContext elasticContext)
        {
            _elasticContext = elasticContext;
            QueryContainer = new QueryContainer();
            QueryStringQuery = new QueryStringQuery();
        }

        public IEnumerable<Product> SearchProduct(string indexName, string keyword)
        {
            var response = _elasticContext.Search<Product>(new SearchRequest(indexName, typeof(Product))
            {
                Size = 1000,
                Query = AddQuery(keyword)
            });

            return response.Documents;
        }

        public bool BulkIndex(string indexName, List<Product> productList)
        {
            return _elasticContext.BulkIndex(indexName, productList);
        }

        public bool CreateIndex(string indexName)
        {
            string aliasName = indexName;
            indexName = $"{indexName}_{DateTime.Now:yyyyMMddHHss}";

            return _elasticContext.CreateIndex<Product>(indexName, aliasName);
        }

        private QueryContainer AddQuery(string keyword)
        {
            QueryStringQuery.DefaultOperator = Operator.And; //yazılan her kelimeyi içermesi gerektiği için default operatörü and olarak atandı.
            QueryStringQuery.Query = $@"\*:({keyword})"; //aramayı bütün fieldlar üzerinde yapıyor.

            QueryContainer.QueryString = QueryStringQuery;

            return (QueryContainer)QueryContainer;
        }
    }
}
