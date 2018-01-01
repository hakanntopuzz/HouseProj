using System;
using System.Collections.Generic;
using System.Configuration;
using HouseProj.Data.Contracts;
using HouseProj.Data.Dtos;
using Nest;

namespace HouseProj.Data
{
    public class ElasticContext : IElasticContext
    {
        private readonly ElasticClient _elasticClient;

        public ElasticContext()
        {
            _elasticClient = new ElasticClient(new Uri(ConfigurationManager.AppSettings["ElasticSearchURI"]));
        }

        public bool CreateIndex<T>(string indexName, string aliasName) where T : class
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                .Mappings(ms => ms
                    .Map<T>(m => m.AutoMap())
                )
                .Aliases(a => a.Alias(aliasName));

            var response = _elasticClient.CreateIndex(createIndexDescriptor);

            return response.IsValid;
        }

        public bool BulkIndex<T>(string indexName, List<T> document) where T : class
        {
            var response = _elasticClient.IndexMany(document, indexName);

            return response.IsValid;
        }

        public SearchResponseDto<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            var response = _elasticClient.Search<T>(searchRequest);

            return new SearchResponseDto<T>()
            {
                Documents = response.Documents
            };
        }
    }
}
