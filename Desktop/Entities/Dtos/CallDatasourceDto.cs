using System.Collections.Generic;

namespace Entities.Dtos
{
    public class CallDatasourceDto
    {
        public List<CallDto> Calls { get; set; }

        public int TotalRecords { get; set; }
    }
}
