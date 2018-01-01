using System.Collections.Generic;
using HouseProj.Data.Dtos;
using Nest;

namespace HouseProj.Data.Contracts
{
    public interface IElasticContext
    {
        bool CreateIndex<T>(string indexName, string aliasName) where T : class;
        bool BulkIndex<T>(string indexName, List<T> document) where T : class;
        SearchResponseDto<T> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
