using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Utils
{
    public class EmailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="fromName"></param>
        /// <param name="to"></param>
        /// <param name="mailServerName">smtp.163.com</param>
        /// <param name="mailAccount"></param>
        /// <param name="mailPwd"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool Send(string from,string fromName,string to,string mailServerName,string mailAccount,string mailPwd,string subject,string body)
        {
            try
            {
                MailMessage mail = new MailMessage();//邮件发送类
                mail.From = new MailAddress(from, fromName);//是谁发送的邮件
                mail.To.Add(to);
                //mail.To.Add(new MailAddress(to, toname));//发送给谁 或者简短的用 mail.To.Add(to)
                mail.Subject = subject; //标题
                mail.BodyEncoding = Encoding.Default; //内容编码
                mail.Priority = MailPriority.Normal;//发送优先级
                mail.Body = body;//邮件内容
                mail.IsBodyHtml = true;//是否HTML形式发送

                SmtpClient smtp = new SmtpClient(mailServerName, 25);//邮件服务器和端口
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //指定发送方式
                smtp.Credentials = new System.Net.NetworkCredential(mailAccount, mailPwd);//指定登录名和授权码 uhpveokkuwhebeeb
                smtp.Timeout = 10000;//超时时间
                //smtp.EnableSsl = true;//经过ssl加密
                smtp.Send(mail);
                //return "send ok";
                return true;
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("邮件发送失败，错误信息:" + ex.Message, ex);
                return false;
            }
        }
    }
}
