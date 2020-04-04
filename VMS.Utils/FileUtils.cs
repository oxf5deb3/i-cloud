using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class FileUtils
    {

        public const String certificateType_LSJZ = "_lsjz";
        public const String certificateType_ZSJZ = "_zsjz";
        public const String _certificateType_LSXS = "_lsxs";
        public const String certificateType_ZSXS = "_zsxs";


        public const String PHOTO_TYPE_USERINFO="_userinfo";

        public const String PHOTO_TYPE_fadongji = "_fadongji";
        public const String PHOTO_TYPE_car_1 = "_car_1";
        public const String PHOTO_TYPE_car_2 = "_car_2";
        public const String PHOTO_TYPE_chejia = "_chejia";

        /**
         * base64转换为文件
         * */
        public static bool Base64ToFileAndSave(string base64,string path)
        {
            bool bTrue = false;             
            try            
            {
                byte[] buffer = Convert.FromBase64String(base64);
                FileStream fs = new FileStream(path, FileMode.CreateNew);                
                fs.Write(buffer, 0, buffer.Length);                
                fs.Close();                bTrue = true;            
            }            
            catch (Exception ex)            
            {                
                throw ex;            
            }             
            
            return bTrue;


        }

        public static bool Base64ToFileAndSave(string base64, string path, FileMode mode)
        {
            bool bTrue = false;
            try
            {
                byte[] buffer = Convert.FromBase64String(base64);
                FileStream fs = new FileStream(path, mode);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close(); bTrue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bTrue;


        }


        /**
         * 将文件转换成base64
         * */
        public static string fileToBase64(string path)
        {
            string strRet = null;
            FileStream fs = null;
            try           
            {               
                fs = new FileStream(path, FileMode.Open);           
                byte[] bt = new byte[fs.Length];            
                fs.Read(bt, 0, bt.Length);             
                strRet = Convert.ToBase64String(bt);          
                fs.Close();           
            }           
            catch (Exception ex)           
            {
                //throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }           
            return strRet;


        }

        /**
         * 生成文件名
         * @username 用户姓名
         * @id_no 证件编码
         * @certificateType 证件类型
         * @photoType 照片类型
         * */
        public static string generateFileName(string username,string id_no,string certificateType,string photoType )
        {
            return username + "_" + id_no + "_" + certificateType + photoType+".png";
        }

    }
}
