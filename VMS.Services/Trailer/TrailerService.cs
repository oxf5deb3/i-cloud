using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VMS.DTO.Trailer;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class TrailerService : BaseReportService, ITrailerService
    {
        public bool AddTrailer(TrailerDTO trailerDTO)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into t_trailer(trailer_date, trailer_number, trailer_totalmotorcycle, trailer_tricycle, trailer_batterymotorcycle, trailer_totalvehicle, trailer_bigcar, trailer_smallcar,trailer_tractor,trailer_totaltrailer,create_id,create_date,isdelete,remark)");
                sql.Append(" values(@trailer_date, @trailer_number, @trailer_totalmotorcycle, @trailer_tricycle, @trailer_batterymotorcycle, @trailer_totalvehicle, @trailer_bigcar,");
                sql.Append(" @trailer_smallcar,@trailer_tractor, @trailer_totaltrailer,@create_id,@create_date,@isdelete,@remark)");
                SqlParam[] sqlParams = new SqlParam[] {
                    new SqlParam("@trailer_date", trailerDTO.trailerDate),
                    new SqlParam("@trailer_number", trailerDTO.number),
                    new SqlParam("@trailer_totalmotorcycle", trailerDTO.totalMotorcycle),
                    new SqlParam("@trailer_tricycle", trailerDTO.tricycle),
                    new SqlParam("@trailer_batterymotorcycle", trailerDTO.batteryMotorcycle),
                    new SqlParam("@trailer_totalvehicle", trailerDTO.totalVehicle),
                    new SqlParam("@trailer_bigcar", trailerDTO.bigCar),
                    new SqlParam("@trailer_smallcar", trailerDTO.smallCar),
                    new SqlParam("@trailer_tractor", trailerDTO.tractor),
                    new SqlParam("@trailer_totaltrailer", trailerDTO.totalTrailer),
                    new SqlParam("@create_id", trailerDTO.createId),
                    new SqlParam("@create_date", DateTime.Now),
                    new SqlParam("@isdelete", 0),
                    new SqlParam("@remark", trailerDTO.remark)
                };
                return DbContext.ExecuteBySql(sql, sqlParams.ToArray()) > 0;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool DeleteTrailer(string id)
        {
            string updateStr = $@"UPDATE dbo.t_trailer SET isdelete= {1},updateDate = '{DateTime.Now}' 
                                   WHERE trailer_no in ({id})";
            int result = DbContext.ExecuteSqlTran(updateStr);
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public List<TrailerDTO> QueryInfo(TrailerQueryDTO trailerQueryDTO)
        {
            List<TrailerDTO> list = new List<TrailerDTO>();
            StringBuilder stringBulider = new StringBuilder();
            stringBulider.Append("select trailer_no, trailer_date, trailer_number, trailer_totalmotorcycle, trailer_tricycle, trailer_batterymotorcycle, trailer_totalvehicle, trailer_bigcar, trailer_smallcar, trailer_tractor, trailer_totaltrailer,remark  from t_trailer WHERE 1 = 1 and isdelete = 0");
            if (!string.IsNullOrEmpty(trailerQueryDTO.startTime))
            {
                stringBulider.AppendFormat("AND trailer_date > '{0}'", trailerQueryDTO.startTime);
            }
            if (!string.IsNullOrEmpty(trailerQueryDTO.endTime))
            {
                stringBulider.AppendFormat("AND trailer_date < '{0}'", trailerQueryDTO.endTime);
            }

            DataSet dataSet = DbContext.Query(stringBulider.ToString());
            if (dataSet != null && dataSet.Tables != null && dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    TrailerDTO trailerDTO = new TrailerDTO();
                    this.GetModelFromDataRow(trailerDTO, dataRow);
                    list.Add(trailerDTO);
                }
            }
            return list;
        }

        private void GetModelFromDataRow(TrailerDTO trailerDTO, DataRow dataRow)
        {
            if (!string.IsNullOrEmpty(dataRow["trailer_no"].ToString()))
            {
                trailerDTO.trailerNo = int.Parse(dataRow["trailer_no"].ToString());
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_date"].ToString()))
            {
                trailerDTO.trailerDate = dataRow["trailer_date"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_number"].ToString()))
            {
                trailerDTO.number = dataRow["trailer_number"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_totalmotorcycle"].ToString()))
            {
                trailerDTO.totalMotorcycle = dataRow["trailer_totalmotorcycle"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_tricycle"].ToString()))
            {
                trailerDTO.tricycle = dataRow["trailer_tricycle"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_batterymotorcycle"].ToString()))
            {
                trailerDTO.batteryMotorcycle = dataRow["trailer_batterymotorcycle"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_totalvehicle"].ToString()))
            {
                trailerDTO.totalVehicle = dataRow["trailer_totalvehicle"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_bigcar"].ToString()))
            {
                trailerDTO.bigCar = dataRow["trailer_bigcar"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_smallcar"].ToString()))
            {
                trailerDTO.smallCar = dataRow["trailer_smallcar"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_tractor"].ToString()))
            {
                trailerDTO.tractor = dataRow["trailer_tractor"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["trailer_totaltrailer"].ToString()))
            {
                trailerDTO.totalTrailer = dataRow["trailer_totaltrailer"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["remark"].ToString()))
            {
                trailerDTO.remark = dataRow["remark"].ToString();
            }
        }


        public List<t_trailer> QueryTrailer(StringBuilder sql, IList<SqlParam> sqlParams, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder("select trailer_no, trailer_date, trailer_number, trailer_totalmotorcycle, trailer_tricycle, trailer_batterymotorcycle, trailer_totalvehicle, trailer_bigcar, trailer_smallcar,trailer_tractor,trailer_totaltrailer,remark from t_trailer where 1 = 1 and isdelete=0");
            var dt = DbContext.GetPageList(sb.Append(sql.ToString()).ToString(), sqlParams.ToArray(), "trailer_no", "asc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_trailer>(dt) as List<t_trailer>;
        }

        public bool UpdateTrailer(TrailerDTO trailerDTO)
        {
            StringBuilder stringBulider = new StringBuilder();
            stringBulider.Append($"UPDATE dbo.t_trailer SET");

            if (trailerDTO.trailerDate != null)
            {
                stringBulider.Append($@" trailer_date = '{trailerDTO.trailerDate}',");
            }
            if (trailerDTO.number != null)
            {
                stringBulider.Append($@"trailer_number = '{trailerDTO.number}',");
            }
            if (!string.IsNullOrEmpty(trailerDTO.totalMotorcycle))
            {
                stringBulider.Append($@" trailer_totalmotorcycle = '{trailerDTO.totalMotorcycle}',");
            }
            if (!string.IsNullOrEmpty(trailerDTO.tricycle))
            {
                stringBulider.Append($@" trailer_tricycle = '{trailerDTO.tricycle}',");
            }
            if (trailerDTO.batteryMotorcycle != null)
            {
                stringBulider.Append($@" trailer_batterymotorcycle = {trailerDTO.batteryMotorcycle},");
            }
            if (trailerDTO.totalVehicle != null)
            {
                stringBulider.Append($@" trailer_totalvehicle = {trailerDTO.totalVehicle},");
            }
            if (trailerDTO.bigCar != null)
            {
                stringBulider.Append($@" trailer_bigcar = {trailerDTO.bigCar},");
            }
            if (trailerDTO.smallCar != null)
            {
                stringBulider.Append($@" trailer_smallcar = {trailerDTO.smallCar},");
            }
            if (trailerDTO.tractor != null)
            {
                stringBulider.Append($@" trailer_tractor = {trailerDTO.tractor},");
            }
            if (trailerDTO.totalTrailer != null)
            {
                stringBulider.Append($@" trailer_totaltrailer = {trailerDTO.totalTrailer},");
            }
            if (trailerDTO.remark != null)
            {
                stringBulider.Append($@" remark = {trailerDTO.remark},");
            }

            stringBulider.Append($@" updateDate = '{DateTime.Now}'");
            stringBulider.Append($@" WHERE trailer_no = '{trailerDTO.trailerNo}'");

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
