using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Stolen;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class StolenService : BaseReportService, IStolenService
    {
        public bool AddStolen(StolenDTO stolenDTO)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_stolen(accepting_cases, total_stolencar_big, total_stolencar_small, total_stolencar_motorcycle, recover_bigcar, recover_smallcar, recover_motorcycle, undetected,remark,isdelete,updateDate,create_id,create_date)");
                sql.Append(" values(@accepting_cases, @total_stolencar_big, @total_stolencar_small, @total_stolencar_motorcycle, @recover_bigcar, @recover_smallcar, @recover_motorcycle,");
                sql.Append(" @undetected,@remark, @isdelete,@updateDate,@create_id,@create_date)");
                SqlParam[] sqlParams = new SqlParam[] {
                    new SqlParam("@accepting_cases", stolenDTO.acceptingCases),
                    new SqlParam("@total_stolencar_big", stolenDTO.totalStolenCarBig),
                    new SqlParam("@total_stolencar_small", stolenDTO.totalStolenCarSmall),
                    new SqlParam("@total_stolencar_motorcycle", stolenDTO.totalStolenCarMotorcycle),
                    new SqlParam("@recover_bigcar", stolenDTO.recoverBigCar),
                    new SqlParam("@recover_smallcar", stolenDTO.recoverSmallCar),
                    new SqlParam("@recover_motorcycle", stolenDTO.recoverMotorcyle),
                    new SqlParam("@undetected", stolenDTO.undetected),
                    new SqlParam("@remark", stolenDTO.remark),
                    new SqlParam("@isdelete", 0),
                    new SqlParam("@updateDate", DateTime.Now),
                    new SqlParam("@create_id", stolenDTO.createId),
                    new SqlParam("@create_date", DateTime.Now),
                };
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteStolen(string id)
        {
            string updateStr = $@"UPDATE dbo.t_stolen SET isdelete= {1},updateDate = '{DateTime.Now}' 
                                   WHERE stolen_id in ({id})";
            int result = DbContext.ExecuteSqlTran(updateStr);
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public List<t_stolen> QueryStolen(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder("select stolen_id, accepting_cases, total_stolencar_big, total_stolencar_small, total_stolencar_motorcycle, recover_bigcar, recover_smallcar, recover_motorcycle, undetected, remark from t_stolen WHERE 1 = 1 and isdelete = 0");
            var dt = DbContext.GetPageList(sb.Append(sql.ToString()).ToString(), sqlParams.ToArray(), "stolen_id", "asc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_stolen>(dt) as List<t_stolen>;
        }

        public bool UpdateStolen(StolenDTO stolenDTO)
        {
            StringBuilder stringBulider = new StringBuilder();
            stringBulider.Append($"UPDATE dbo.t_stolen SET");

            if (stolenDTO.acceptingCases != null)
            {
                stringBulider.Append($@" accepting_cases = '{stolenDTO.acceptingCases}',");
            }
            if (stolenDTO.totalStolenCarBig != null)
            {
                stringBulider.Append($@"total_stolencar_big = '{stolenDTO.totalStolenCarBig}',");
            }
            if (!string.IsNullOrEmpty(stolenDTO.totalStolenCarSmall))
            {
                stringBulider.Append($@" total_stolencar_small = '{stolenDTO.totalStolenCarSmall}',");
            }
            if (!string.IsNullOrEmpty(stolenDTO.totalStolenCarMotorcycle))
            {
                stringBulider.Append($@" total_stolencar_motorcycle = '{stolenDTO.totalStolenCarMotorcycle}',");
            }
            if (stolenDTO.recoverBigCar != null)
            {
                stringBulider.Append($@" recover_bigcar = {stolenDTO.recoverBigCar},");
            }
            if (stolenDTO.recoverSmallCar != null)
            {
                stringBulider.Append($@" recover_smallcar = {stolenDTO.recoverSmallCar},");
            }
            if (stolenDTO.recoverMotorcyle != null)
            {
                stringBulider.Append($@" recover_motorcycle = {stolenDTO.recoverMotorcyle},");
            }
            if (stolenDTO.undetected != null)
            {
                stringBulider.Append($@" undetected = {stolenDTO.undetected},");
            }
            if (stolenDTO.remark != null)
            {
                stringBulider.Append($@" remark = {stolenDTO.remark},");
            }

            stringBulider.Append($@" updateDate = '{DateTime.Now}'");
            stringBulider.Append($@" WHERE stolen_id = '{stolenDTO.stolenId}'");

            string updateStr = stringBulider.ToString();
            int result = DbContext.ExecuteSqlTran(updateStr);
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
