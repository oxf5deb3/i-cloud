using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Trailer;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class DetentionService : BaseReportService, IDetentionService
    {
        public bool AddDetention(DetentionDTO detentionDTO)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_detention(total_detention,total_detention_boy,total_detention_girl,already_release,now_detention_total,now_detention_boy,now_detention_girl, remark, isdelete, updateDate,create_id,create_date,traffic_accident_detention,not_cooperate_detention,devolve_police_total,devolve_police_boy,devolve_police_girl,devolve_procuratorate_total,devolve_procuratorate_boy,devolve_procuratorate_girl)");
                sql.Append(" values(@total_detention,@total_detention_boy,@total_detention_girl, @already_release, @now_detention_total,@now_detention_boy,@now_detention_girl, @remark, @isdelete, @updateDate,@create_id,@create_date,@traffic_accident_detention,@not_cooperate_detention,@devolve_police_total,@devolve_police_boy,@devolve_police_girl,@devolve_procuratorate_total,@devolve_procuratorate_boy,@devolve_procuratorate_girl)");
                SqlParam[] sqlParams = new SqlParam[] {
                    new SqlParam("@total_detention", detentionDTO.totalDetention),
                    new SqlParam("@total_detention_boy", detentionDTO.totalDetentionBoy),
                    new SqlParam("@total_detention_girl", detentionDTO.totalDetentionGirl),
                    new SqlParam("@already_release", detentionDTO.alreadyRelease),
                    new SqlParam("@now_detention_total", detentionDTO.nowDetentionTotal),
                    new SqlParam("@now_detention_boy", detentionDTO.nowDetentionBoy),
                    new SqlParam("@now_detention_girl", detentionDTO.nowDetentionGirl),
                    new SqlParam("@remark", detentionDTO.remark),
                    new SqlParam("@updateDate", DateTime.Now),
                    new SqlParam("@isdelete", 0),
                    new SqlParam("@create_id", detentionDTO.createId),
                    new SqlParam("@create_date", DateTime.Now),
                    new SqlParam("@traffic_accident_detention", detentionDTO.trafficAccidentDetention),
                    new SqlParam("@not_cooperate_detention", detentionDTO.notCooperateDetention),
                    new SqlParam("@devolve_police_total", detentionDTO.devolvePoliceTotal),
                    new SqlParam("@devolve_police_boy", detentionDTO.devolvePoliceBoy),
                    new SqlParam("@devolve_police_girl", detentionDTO.devolvePoliceGirl),
                    new SqlParam("@devolve_procuratorate_total", detentionDTO.devolveProcuratorateTotal),
                    new SqlParam("@devolve_procuratorate_boy", detentionDTO.devolveProcuratorateBoy),
                    new SqlParam("@devolve_procuratorate_girl", detentionDTO.devolveProcuratorateGirl)
                };
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteDetention(string id)
        {
            string updateStr = $@"UPDATE dbo.t_detention SET isdelete= {1},updateDate = '{DateTime.Now}' 
                                   WHERE detention_id in ({id})";
            int result = DbContext.ExecuteSqlTran(updateStr);
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public List<t_detention> QueryDetention(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder("select detention_id, total_detention,total_detention_boy,total_detention_girl, already_release, now_detention_total,now_detention_boy,now_detention_girl,traffic_accident_detention,not_cooperate_detention,devolve_police_total,devolve_police_boy,devolve_police_girl,devolve_procuratorate_total,devolve_procuratorate_boy,devolve_procuratorate_girl,remark  from t_detention WHERE 1 = 1 and isdelete = 0");
            var dt = DbContext.GetPageList(sb.Append(sql.ToString()).ToString(), sqlParams.ToArray(), "detention_id", "asc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_detention>(dt) as List<t_detention>;
        }

        public bool UpdateDetention(DetentionDTO detentionDTO)
        {
            StringBuilder stringBulider = new StringBuilder();
            stringBulider.Append($"UPDATE dbo.t_detention SET");

            if (!string.IsNullOrEmpty(detentionDTO.totalDetention))
            {
                stringBulider.Append($@" total_detention = '{detentionDTO.totalDetention}',");
            }
            if (!string.IsNullOrEmpty(detentionDTO.totalDetentionBoy))
            {
                stringBulider.Append($@" total_detention_boy = '{detentionDTO.totalDetentionBoy}',");
            }
            if (!string.IsNullOrEmpty(detentionDTO.totalDetentionGirl))
            {
                stringBulider.Append($@" total_detention_girl = '{detentionDTO.totalDetentionGirl}',");
            }
            if (!string.IsNullOrEmpty(detentionDTO.alreadyRelease))
            {
                stringBulider.Append($@" already_release = '{detentionDTO.alreadyRelease}',");
            }
            if (detentionDTO.nowDetentionTotal != null)
            {
                stringBulider.Append($@" now_detention_total = '{detentionDTO.nowDetentionTotal}',");
            }
            if (detentionDTO.nowDetentionBoy != null)
            {
                stringBulider.Append($@" now_detention_boy = '{detentionDTO.nowDetentionBoy}',");
            }
            if (detentionDTO.nowDetentionGirl != null)
            {
                stringBulider.Append($@" now_detention_girl = '{detentionDTO.nowDetentionGirl}',");
            }
            if (detentionDTO.remark != null)
            {
                stringBulider.Append($@" remark = '{detentionDTO.remark}',");
            }

            if (detentionDTO.trafficAccidentDetention != null)
            {
                stringBulider.Append($@" traffic_accident_detention = '{detentionDTO.trafficAccidentDetention}',");
            }
            if (detentionDTO.notCooperateDetention != null)
            {
                stringBulider.Append($@" not_cooperate_detention = '{detentionDTO.notCooperateDetention}',");
            }
            if (detentionDTO.devolvePoliceTotal != null)
            {
                stringBulider.Append($@" devolve_police_total = '{detentionDTO.devolvePoliceTotal}',");
            }
            if (detentionDTO.devolvePoliceBoy != null)
            {
                stringBulider.Append($@" devolve_police_boy = '{detentionDTO.devolvePoliceBoy}',");
            }
            if (detentionDTO.devolvePoliceGirl != null)
            {
                stringBulider.Append($@" devolve_police_girl = '{detentionDTO.devolvePoliceGirl}',");
            }
            if (detentionDTO.devolveProcuratorateTotal != null)
            {
                stringBulider.Append($@" devolve_procuratorate_total = '{detentionDTO.devolveProcuratorateTotal}',");
            }
            if (detentionDTO.devolveProcuratorateBoy != null)
            {
                stringBulider.Append($@" devolve_procuratorate_boy = '{detentionDTO.devolveProcuratorateBoy}',");
            }
            if (detentionDTO.devolveProcuratorateGirl != null)
            {
                stringBulider.Append($@" devolve_procuratorate_girl = '{detentionDTO.devolveProcuratorateGirl}',");
            }
            stringBulider.Append($@" updateDate = '{DateTime.Now}'");
            stringBulider.Append($@" WHERE detention_id = '{detentionDTO.detentionId}'");

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
