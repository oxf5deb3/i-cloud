using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.Model;

namespace VMS.IServices
{
    public interface IService : IDisposable
    {
        #region 打印模板
        bool AddPrintTemplate(PrintTemplateDTO dto);

        bool DeletePrintTemplate(PrintTemplateDTO dto);

        bool UpdatePrintTemaplte(PrintTemplateDTO dto);

        List<PrintTemplateDTO> LoadTemplateByOperId(int type, string oper_id);

        #endregion
    }
}
