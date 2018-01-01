using System.Collections.Generic;

namespace HouseProj.Data.Dtos
{
    public class SearchResponseDto<T>
    {
        public IEnumerable<T> Documents { get; set; }
    }
}
