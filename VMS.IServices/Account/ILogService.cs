using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;

namespace VMS.IServices
{
    public interface ILogService : IService
    {
        bool WriteLoginLog(LoginLogDTO log);

        bool WriteOperateLog(OperateLogDTO log);
    }
}
