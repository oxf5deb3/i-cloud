using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.DTO.FireControl;
using VMS.Utils;
using System.Text.RegularExpressions;
using VMS.Model;
using System.Linq.Expressions;
using SqlSugar;

namespace VMS.Services
{
    public class FireControlService : BaseReportService, IFireControlService
    {
        public bool AddFireAccident(FireAccidentDTO dto)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_fire_accident_records(happen_date, happen_addr, accident_desc, out_police_cars, out_police_mans, process_results, oper_id, oper_date, img_url,name,sex,age,folk,addr,phone,loss,finance_loss,casualties)");
                sql.Append(" values(@happen_date, @happen_addr, @accident_desc, @out_police_cars, @out_police_mans, @process_results, @oper_id, @oper_date, @img_url,@name,@sex,@age,@folk,@addr,@phone,@loss,@finance_loss,@casualties)");
                List<SqlParam> sqlParams = new List<SqlParam>();
                sqlParams.Add(new SqlParam("@happen_date", dto.datetime));
                sqlParams.Add(new SqlParam("@happen_addr", dto.address));
                sqlParams.Add(new SqlParam("@accident_desc", dto.desc));
                sqlParams.Add(new SqlParam("@out_police_cars", dto.cars));
                sqlParams.Add(new SqlParam("@out_police_mans", dto.names));
                sqlParams.Add(new SqlParam("@process_results", dto.result));
                sqlParams.Add(new SqlParam("@oper_id", dto.operId));
                sqlParams.Add(new SqlParam("@oper_date", DateTime.Now.ToString()));
                sqlParams.Add(new SqlParam("@img_url", dto.imgs));


                sqlParams.Add(new SqlParam("@name", dto.name));
                sqlParams.Add(new SqlParam("@sex", dto.sex));
                sqlParams.Add(new SqlParam("@age", dto.age));
                sqlParams.Add(new SqlParam("@folk", dto.folk));
                sqlParams.Add(new SqlParam("@addr", dto.addr));
                sqlParams.Add(new SqlParam("@phone", dto.phone));
                sqlParams.Add(new SqlParam("@loss", dto.loss));
                sqlParams.Add(new SqlParam("@finance_loss", dto.finance_loss));
                sqlParams.Add(new SqlParam("@casualties", dto.casualties));



                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool AddFireEquipment(FireEquipmentDTO dto)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_fire_equipment_register(eq_name, install_addr, install_date, usage_desc, person_liable, oper_id, oper_date, img_url)");
                sql.Append(" values(@eq_name, @install_addr, @install_date, @usage_desc, @person_liable, @oper_id, @oper_date, @img_url)");
                List<SqlParam> sqlParams = new List<SqlParam>();
                sqlParams.Add(new SqlParam("@eq_name", dto.name));
                sqlParams.Add(new SqlParam("@install_addr", dto.address));
                sqlParams.Add(new SqlParam("@install_date", dto.datetime));
                sqlParams.Add(new SqlParam("@usage_desc", dto.desc));
                sqlParams.Add(new SqlParam("@person_liable", dto.liable));
                sqlParams.Add(new SqlParam("@oper_id", dto.operId));
                sqlParams.Add(new SqlParam("@oper_date", DateTime.Now.ToString()));
                sqlParams.Add(new SqlParam("@img_url", dto.imgs));
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public List<t_fire_accident_records> ListAccident(StringBuilder sqlWhere, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count) {
            StringBuilder sb = new StringBuilder("select id, happen_date, happen_addr, accident_desc, out_police_cars, out_police_mans, process_results, oper_id, oper_date, modify_oper_id, modify_date, img_url,name,sex,age,folk,addr,phone,loss,finance_loss,casualties from t_fire_accident_records where 1=1 ");
            var dt = DbContext.GetPageList(sb.Append(sqlWhere.ToString()).ToString() , sqlParams.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_fire_accident_records>(dt) as List<t_fire_accident_records>;
        }

        public bool ModifyAccident(FireAccidentDTO entity){
            try
            {
                StringBuilder sb = new StringBuilder("update t_fire_accident_records set happen_date = @happen_date, happen_addr = @happen_addr, accident_desc = @accident_desc, out_police_cars = @out_police_cars, out_police_mans = @out_police_mans");
                sb.Append(",name=@name,sex=@sex,age=@age,folk=@folk,addr=@addr,phone=@phone,loss=@loss,finance_loss=@finance_loss,casualties=@casualties");
                sb.Append(", process_results = @process_results, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
                List<SqlParam> sqlParams = new List<SqlParam>();
                sqlParams.Add(new SqlParam("@happen_date", entity.datetime));
                sqlParams.Add(new SqlParam("@happen_addr", entity.address));
                sqlParams.Add(new SqlParam("@accident_desc", entity.desc));
                sqlParams.Add(new SqlParam("@out_police_cars", entity.cars));
                sqlParams.Add(new SqlParam("@out_police_mans", entity.names));
                sqlParams.Add(new SqlParam("@process_results", entity.result));
                sqlParams.Add(new SqlParam("@modify_oper_id", entity.modifyOperId));
                sqlParams.Add(new SqlParam("@modify_date", DateTime.Now.ToString()));
                sqlParams.Add(new SqlParam("@name", entity.name));
                sqlParams.Add(new SqlParam("@sex", entity.sex));
                sqlParams.Add(new SqlParam("@age", entity.age));
                sqlParams.Add(new SqlParam("@folk", entity.folk));
                sqlParams.Add(new SqlParam("@addr", entity.addr));
                sqlParams.Add(new SqlParam("@phone", entity.phone));
                sqlParams.Add(new SqlParam("@loss", entity.loss));
                sqlParams.Add(new SqlParam("@finance_loss", entity.finance_loss));
                sqlParams.Add(new SqlParam("@casualties", entity.casualties));
                sqlParams.Add(new SqlParam("@id", entity.id));
                return DbContext.ExecuteBySql(sb, sqlParams.ToArray()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool ModifyImgs(FireAccidentDTO dto) {
            StringBuilder sb = new StringBuilder("update t_fire_accident_records set img_url = @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgs),
                new SqlParam("@modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

            return DbContext.ExecuteBySql(sb, sqlParams) > 0;
        }

        public bool AppendImgs(FireAccidentDTO dto) {
            StringBuilder sb = new StringBuilder("update t_fire_accident_records set img_url = img_url + @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgs),
                new SqlParam("modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

            return DbContext.ExecuteBySql(sb, sqlParams) > 0;
        }

        public bool DelAccident(string ids) {
            try
            {
                return DbContext.BatchDeleteData("t_fire_accident_records", "id", Regex.Split(ids,",", RegexOptions.IgnoreCase)) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<t_fire_equipment_register> ListEquipment(StringBuilder sqlWhere, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder("select id, eq_name, install_addr, usage_desc, install_date, person_liable, oper_id, oper_date, modify_oper_id, modify_date, img_url from t_fire_equipment_register where 1=1 ");
            var dt = DbContext.GetPageList(sb.Append(sqlWhere.ToString()).ToString(), sqlParams.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_fire_equipment_register>(dt) as List<t_fire_equipment_register>;
        }
        public List<t_fire_equipment_register> ListEquipmentNew(IDictionary<string, dynamic> conditions, string orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count, ref string err)
        {
            List<Expression<Func<t_fire_equipment_register, bool>>> wheres = new List<Expression<Func<t_fire_equipment_register, bool>>>();
            wheres.AddRange(ListEquipmentNewCreateWhere(conditions));

            Expression<Func<t_fire_equipment_register, object>> orderbys = ListEquipmentNewCreateOrderby(orderby);

            var q = SqlSugarDbContext.Db.Queryable<t_fire_equipment_register>();

            var lst = SqlSugarDbContext.GetPageList<t_fire_equipment_register, t_fire_equipment_register>(q, wheres, orderbys, isAsc, pageIndex, pageSize, ref count);

            var dtos = TempCarLicenseConvert2DTO(lst);

            return dtos;
        }
        public virtual List<Expression<Func<t_fire_equipment_register, bool>>> ListEquipmentNewCreateWhere(IDictionary<string, dynamic> conditions)
        {
            var where = new List<Expression<Func<t_fire_equipment_register, bool>>>();
            var id = conditions["id"] != null ? (decimal)conditions["id"] : -1;
            var name = conditions["name"] != null ? (string)conditions["name"] : "";
            var address = conditions["address"] != null ? (string)conditions["address"] : "";
            DateTime? timeBegin = conditions["timeBegin"] != null ? (DateTime?)conditions["timeBegin"] : null;
            DateTime? timeEnd = conditions["timeEnd"] != null ? (DateTime?)conditions["timeEnd"] : null;
            var liable = conditions["liable"] != null ? (string)conditions["liable"] : "";
            //var id_no = conditions["id_no"] != null ? (string)conditions["id_no"] : "";
            if (id>-1)
            {
                where.Add(e => e.id==(id));
            }
            if (!string.IsNullOrEmpty(name))
            {
                where.Add(e => e.eq_name.StartsWith(name));
            }
            if (!string.IsNullOrEmpty(address))
            {
                where.Add(e => e.install_addr.StartsWith(address));
            }
            if (timeBegin!=null)
            {
                where.Add(e => e.install_date>timeBegin.Value);
            }
            if (timeEnd != null)
            {
                where.Add(e => e.install_date< timeEnd.Value);
            }
            if (!string.IsNullOrEmpty(liable))
            {
                where.Add(e => e.person_liable.StartsWith(liable));
            }
            return where;
        }
        public virtual Expression<Func<t_fire_equipment_register, object>> ListEquipmentNewCreateOrderby(string orderby)
        {
            Expression<Func<t_fire_equipment_register, object>> by = null;
            switch (orderby)
            {
                case "eq_name": by = o => o.eq_name; break;
                case "eq_type": by = o => o.eq_type; break;
                case "eq_qty": by = o => o.eq_qty; break;
                case "install_addr": by = o => o.install_addr; break;
                case "usage_desc": by = o => o.usage_desc; break;
                case "person_liable": by = o => o.person_liable; break;
                case "oper_id": by = o => o.oper_id; break;
                case "modify_oper_id": by = o => o.modify_oper_id; break;
                case "img_url": by = o => o.img_url; break;
                case "img0_url": by = o => o.img0_url; break;
                case "modify_date": by = o => new { o.modify_date }; break;
                case "install_date": by = o => new { o.install_date }; break;
                case "oper_date": by = o => new { o.oper_date }; break;
                case "img1_url": by = o => o.img1_url; break;
                default: by = o => o.id; break;
            }
            return by;
        }
        public virtual List<t_fire_equipment_register> TempCarLicenseConvert2DTO(ISugarQueryable<t_fire_equipment_register> q)
        {
            var dtos = q.ToList();
            return dtos;
        }
        public bool ModifyEquipment(FireEquipmentDTO entity)
        {
            try
            {
                StringBuilder sb = new StringBuilder("update t_fire_equipment_register set eq_name = @eq_name, install_addr = @install_addr, usage_desc = @usage_desc");
                sb.Append(", install_date = @install_date, person_liable = @person_liable, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
                List<SqlParam> sqlParams = new List<SqlParam>();
                sqlParams.Add(new SqlParam("@eq_name", entity.name));
                sqlParams.Add(new SqlParam("@install_addr", entity.address));
                sqlParams.Add(new SqlParam("@usage_desc", entity.desc));
                sqlParams.Add(new SqlParam("@install_date", entity.datetime));
                sqlParams.Add(new SqlParam("@person_liable", entity.liable));
                sqlParams.Add(new SqlParam("@modify_oper_id", entity.modifyOperId));
                sqlParams.Add(new SqlParam("@modify_date", DateTime.Now.ToString()));
                sqlParams.Add(new SqlParam("@id", entity.id));
                return DbContext.ExecuteBySql(sb, sqlParams.ToArray()) > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool ModifyImgs(FireEquipmentDTO dto)
        {
            StringBuilder sb = new StringBuilder("update t_fire_equipment_register set img_url = @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgs),
                new SqlParam("modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

            return DbContext.ExecuteBySql(sb, sqlParams) > 0;
        }

        public bool AppendImgs(FireEquipmentDTO dto)
        {
            StringBuilder sb = new StringBuilder("update t_fire_equipment_register set img_url = img_url + @img_url, modify_oper_id = @modify_oper_id, modify_date = @modify_date where id = @id");
            SqlParam[] sqlParams = new SqlParam[]{
                new SqlParam("@img_url", dto.imgs),
                new SqlParam("modify_oper_id", dto.modifyOperId),
                new SqlParam("@modify_date", DateTime.Now.ToString()),
                new SqlParam("@id", dto.id)
            };

            return DbContext.ExecuteBySql(sb, sqlParams) > 0;
        }

        public bool DelEquipment(string ids)
        {
            try
            {
                return DbContext.BatchDeleteData("t_fire_equipment_register", "id", Regex.Split(ids, ",", RegexOptions.IgnoreCase)) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
