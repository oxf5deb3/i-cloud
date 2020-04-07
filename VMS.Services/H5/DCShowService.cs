using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class DCShowService : ServiceBase, IDCShowService
    {
        public DCShowDTO FindOne(IDictionary<string, dynamic> qcondition, ref string err)
        {
            var cid = qcondition["cid"] != null ? qcondition["cid"] : "-1";
            var did = qcondition["did"] != null ? qcondition["did"] : "-1";
            var dto = new DCShowDTO();
            //驾驶证
            var sql = new StringBuilder();
            sql.Append("SELECT     a.*" +
                                  ", b.region_name,c.type_name as permitted_car_type_name");
            sql.Append(" from t_normal_driver_license a");
            sql.Append(" left join t_bd_region  b on a.region_no = b.region_no");
            sql.Append(" left join t_bd_permitted_car_type c on a.permitted_card_type_no= c.type_no");
            sql.Append(" where id=@did ");
            var lstParams = new List<SqlParam>();
            lstParams.Add(new SqlParam("@did", did));
            var dlist = DbContext.GetDataListBySQL<DriverLicenseDTO>(sql, lstParams.ToArray());

            //行驶证
            sql.Clear();
            sql.Append("select * from t_normal_car_license where id =@cid ");
            lstParams.Clear();
            lstParams.Add(new SqlParam("@cid", cid));
            var clist = DbContext.GetDataListBySQL<CarLicenseDTO>(sql, lstParams.ToArray());

            if (dlist.Count > 0)
            {
                dto.dl = dlist[0] as DriverLicenseDTO;
            }

            if (clist.Count > 0)
            {
                dto.dp = clist[0] as CarLicenseDTO;
                if (!string.IsNullOrEmpty(dto.dp.user_photo_path))
                {
                    dto.dp.car_1_value = dto.dp.car_1_img_path == null ? "" : FileUtils.fileToBase64(dto.dp.car_1_img_path);
                    dto.dp.car_2_value = dto.dp.car_2_img_path == null ? "" : FileUtils.fileToBase64(dto.dp.car_2_img_path);
                    dto.dp.vin_no_value = dto.dp.vin_no_img_path == null ? "" : FileUtils.fileToBase64(dto.dp.vin_no_img_path);
                    dto.dp.engine_no_value = dto.dp.engine_no_img_path == null ? "" : FileUtils.fileToBase64(dto.dp.engine_no_img_path);
                    dto.dp.user_photo_base64 = FileUtils.fileToBase64(dto.dp.user_photo_path);
                }
            }
            return dto;
        }
        public string CreateQRCode(string content)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                QRCodeEncoder qrEncoder = new QRCodeEncoder();
                //二维码类型
                qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //二维码尺寸
                qrEncoder.QRCodeScale = 3;
                //二维码版本
                qrEncoder.QRCodeVersion = 7;
                //二维码容错程度
                qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                //字体与背景颜色
                qrEncoder.QRCodeBackgroundColor = Color.White;
                qrEncoder.QRCodeForegroundColor = Color.Black;
                //UTF-8编码类型
                //Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);
                Bitmap image = qrEncoder.Encode(content);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                var base64 = Convert.ToBase64String(arr);
                image.Dispose();
                return "data:image/jpeg;base64," + base64;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ms.Close();
            }
        }

        public string QiCodeMake(IDictionary<string, dynamic> param, ref string err)
        {
            var type = param["type"];
            var cid = param["cid"];
            var did = param["did"];
            var http = param["http"];
            var content = string.Format("{0}?{1}{2}{3}", http, "type=" + type, "&cid=" + cid, "&did=" + did);
            var serverPath = CreateQRCode(content);
            return serverPath;
        }
    }
}
