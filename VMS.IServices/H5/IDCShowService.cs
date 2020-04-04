using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;

namespace VMS.IServices
{
    public interface IDCShowService : IService
    {

        DCShowDTO FindOne(IDictionary<string, dynamic> qcondition,ref string err);

        string QiCodeMake(IDictionary<string, dynamic> param,ref string err);
    }
}
