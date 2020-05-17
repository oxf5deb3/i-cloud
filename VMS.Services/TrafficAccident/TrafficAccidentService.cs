using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.DTO.TrafficAccident;
using VMS.Model;
using VMS.Utils;
using System.Text.RegularExpressions;

namespace VMS.Services
{
    public class TrafficAccidentService : BaseReportService, ITrafficAccidentService
    {
        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool AddTrafficAccident(TrafficAccidentDTO dto)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_accident_records(happen_date, happen_addr, first_party_man, first_party_addr, second_party_man, second_party_addr, accident_desc, mediation_unit,");
                sql.Append(" mediation_date, draw_recorder, accident_mediator, oper_id, oper_date, img_url,duty,dingPartyAddr,dingPartyMan,bingPartyAddr,bingPartyMan,firstPartyCarNo,secondPartyCarNo)");
                sql.Append(" values(@happen_date, @happen_addr, @first_party_man, @first_party_addr, @second_party_man, @second_party_addr, @accident_desc, @mediation_unit,");
                sql.Append(" @mediation_date, @draw_recorder, @accident_mediator, @oper_id, @oper_date, @img_url,@duty,@dingPartyAddr,@dingPartyMan,@bingPartyAddr,@bingPartyMan,@firstPartyCarNo,@secondPartyCarNo)");
                SqlParam[] sqlParams = new SqlParam[] { 
                    new SqlParam("@happen_date", dto.happenDate),
                    new SqlParam("@happen_addr", dto.happenAddr),
                    new SqlParam("@first_party_man", dto.firstPartyMan),
                    new SqlParam("@first_party_addr", dto.firstPartyAddr),
                    new SqlParam("@second_party_man", dto.secondPartyMan),
                      new SqlParam("@img_url", dto.imgUrl.ToString()),

                    
                    new SqlParam("@second_party_addr", dto.secondPartyAddr),
                    new SqlParam("@accident_desc", dto.accidentDesc),
                    new SqlParam("@mediation_unit", dto.mediationUnit),
                    new SqlParam("@mediation_date", dto.mediationDate),
                    new SqlParam("@draw_recorder", dto.drawRecorder),
                    
                    new SqlParam("@accident_mediator", dto.accidentMediator),
                    new SqlParam("@oper_id", dto.operId),
                    new SqlParam("@oper_date", DateTime.Now.ToString()),

                    new SqlParam("@duty", dto.duty),
                    new SqlParam("@dingPartyAddr", dto.dingPartyAddr),
                    new SqlParam("@dingPartyMan", dto.dingPartyMan),
                    new SqlParam("@bingPartyAddr", dto.bingPartyAddr),
                    new SqlParam("@bingPartyMan", dto.bingPartyMan),
                    new SqlParam("@firstPartyCarNo", dto.firstPartyCarNo),
                    new SqlParam("@secondPartyCarNo", dto.secondPartyCarNo),

                };
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 综合查询
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <param name="sqlParams"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<t_accident_records> ListAccident(StringBuilder sqlWhere, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder("select id, happen_date, happen_addr, first_party_man, first_party_addr, second_party_man, second_party_addr, accident_desc, mediation_unit, ");
            sb.Append("mediation_date, draw_recorder, accident_mediator, oper_id, oper_date, modify_oper_id, modify_date, img_url,duty,dingPartyAddr,dingPartyMan,bingPartyAddr,bingPartyMan,firstPartyCarNo,secondPartyCarNo from t_accident_records where 1 = 1");
            var dt = DbContext.GetPageList(sb.Append(sqlWhere.ToString()).ToString(), sqlParams.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_accident_records>(dt) as List<t_accident_records>;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool ModifyAccident(TrafficAccidentDTO dto)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update t_accident_records set happen_date = @happen_date, happen_addr = @happen_addr, first_party_man = @first_party_man, first_party_addr = @first_party_addr, second_party_man = @second_party_man, second_party_addr = @second_party_addr, accident_desc = @accident_desc, mediation_unit = @mediation_unit,");
                sql.Append(" mediation_date = @mediation_date, draw_recorder = @draw_recorder, accident_mediator = @accident_mediator, modify_oper_id = @modify_oper_id, modify_date = @modify_date,duty=@duty,dingPartyAddr=@dingPartyAddr,dingPartyMan=@dingPartyMan,bingPartyAddr=@bingPartyAddr,bingPartyMan=@bingPartyMan,firstPartyCarNo=@firstPartyCarNo,secondPartyCarNo=@secondPartyCarNo where id = @id");

                SqlParam[] sqlParams = new SqlParam[] { 
                    new SqlParam("@happen_date", dto.happenDate),
                    new SqlParam("@happen_addr", dto.happenAddr),
                    new SqlParam("@first_party_man", dto.firstPartyMan),
                    new SqlParam("@first_party_addr", dto.firstPartyAddr),
                    new SqlParam("@second_party_man", dto.secondPartyMan),

                    new SqlParam("@second_party_addr", dto.secondPartyAddr),
                    new SqlParam("@accident_desc", dto.accidentDesc),
                    new SqlParam("@mediation_unit", dto.mediationUnit),
                    new SqlParam("@mediation_date", dto.mediationDate),
                    new SqlParam("@draw_recorder", dto.drawRecorder),
                    
                    new SqlParam("@accident_mediator", dto.accidentMediator),
                    new SqlParam("@modify_oper_id", dto.modifyOperId),
                    new SqlParam("@modify_date", DateTime.Now.ToString()),
                     new SqlParam("@duty", dto.duty),
                    new SqlParam("@dingPartyAddr", dto.dingPartyAddr),
                    new SqlParam("@dingPartyMan", dto.dingPartyMan),
                    new SqlParam("@bingPartyAddr", dto.bingPartyAddr),
                    new SqlParam("@bingPartyMan", dto.bingPartyMan),
                     new SqlParam("@firstPartyCarNo", dto.firstPartyCarNo),
                    new SqlParam("@secondPartyCarNo", dto.secondPartyCarNo),
                    new SqlParam("@id", dto.id)
                };
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch (Exception e)
            {
                
                return false;
            }
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelAccident(string ids) {
            try
            {
                return DbContext.BatchDeleteData("t_accident_records", "id", Regex.Split(ids, ",", RegexOptions.IgnoreCase)) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 修改图片路径
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool ModifyImgs(TrafficAccidentDTO dto) {
            try
            {
                StringBuilder sb = new StringBuilder("update t_accident_records set img_url = @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
                SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgUrl),
                new SqlParam("modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

            return DbContext.ExecuteBySql(sb, sqlParams) > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool AppendImgs(TrafficAccidentDTO dto) {
            try
            {
                StringBuilder sb = new StringBuilder("update t_accident_records set img_url = img_url + @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
                SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgUrl),
                new SqlParam("modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

                return DbContext.ExecuteBySql(sb, sqlParams) > 0;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
