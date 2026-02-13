using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
