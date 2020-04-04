using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class BaseTResponseDTO<T>:BaseResponseDTO
        where T:new()
    {
        public BaseTResponseDTO() {
            data = new T();
        }
        public T data { get; set; }
    }
}
