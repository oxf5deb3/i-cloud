using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.IServices
{
    public interface IPermittedCarTypeService : IService
    {
        bool AddPermittedCarType(t_bd_permitted_car_type info);

        bool EditPermittedCarType(t_bd_permitted_car_type info);

        bool BatchDeletePermittedCarType(List<string> pkValues);

        t_bd_permitted_car_type FindByTypeNo(string type_no);

        List<t_bd_permitted_car_type> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);

    }
}
