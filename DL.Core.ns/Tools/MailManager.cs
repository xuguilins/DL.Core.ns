using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using DL.Core.ns.ResultFactory;
using DL.Core.ns.Logging;
using DL.Core.ns.Extensiton;
using System.Linq;
using System.IO;

namespace DL.Core.ns.Tools
{
    /// <summary>
    /// 简单邮件发送类库
    /// </summary>
    public static class MailManager
    {
        private static ILogger logger = LogManager.GetLogger();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromUser">发送人邮箱</param>
        /// <param name="toUser">接受人邮箱,多个以","分隔</param>
        /// <param name="subTitle">标题</param>
        /// <param name="body">内容</param>
        /// <param name="ccPairs">抄送人</param>
        /// <param name="ishtml">是否为html</param>
        /// <paramref name="attachmemts">存放文件路径</paramref>
        /// <returns></returns>
        public static ReturnResult SendMail(string fromUser, string toUser, string subTitle, string body, Dictionary<string, string> ccPairs, bool ishtml = false, List<string> attachmemts = null)
        {
            return Send(fromUser, toUser, subTitle, body, ccPairs, ishtml, true, null, null, attachmemts);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromUser">发送人邮箱</param>
        /// <param name="toUser">接受人邮箱,多个以","分隔</param>
        /// <param name="subTitle">标题</param>
        /// <param name="body">内容</param>
        /// <paramref name="attachmemts">存放文件路径</paramref>
        /// <returns></returns>
        public static ReturnResult SendMail(string fromUser, string toUser, string subTitle, string body, List<string> attachmemts = null)
        {
            return Send(fromUser, toUser, subTitle, body, null, false, true, null, null, attachmemts);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromUser">发送人邮箱</param>
        /// <param name="toUser">接受人邮箱,多个以","分隔</param>
        /// <param name="subTitle">标题</param>
        /// <param name="body">内容</param>
        /// <param name="ccPairs">抄送人</param>
        /// <param name="ishtml">是否为html</param>
        /// <param name="useSsl">采用ssl</param>
        /// <paramref name="attachmemts">存放文件路径</paramref>
        /// <returns></returns>
        public static ReturnResult SendMail(string fromUser, string toUser, string subTitle, string body, Dictionary<string, string> ccPairs = null, bool ishtml = false, bool useSsl = true, List<string> attachmemts = null)
        {
            return Send(fromUser, toUser, subTitle, body, ccPairs, ishtml, useSsl, null, null, attachmemts);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromUser">发送人邮箱/param>
        /// <param name="toUser">接受人邮箱,多个以","分隔</param>
        /// <param name="subTitle">标题</param>
        /// <param name="body">内容</param>
        /// <param name="ccPairs">抄送人</param>
        /// <param name="ishtml">是否为html</param>
        /// <param name="sendpass">邮箱授权码</param>
        /// <param name="senduser">发件人邮箱,单个邮箱！</param>
        /// <param name="useSsl">采用ssl默认为true</param>
        /// <paramref name="attachmemts">存放文件路径</paramref>
        /// <returns></returns>
        public static ReturnResult SendMail(string fromUser, string toUser, string subTitle, string body,
            string senduser, string sendpass, Dictionary<string, string> ccPairs = null, bool ishtml = false, bool useSsl = true, List<string> attachmemts = null)
        {
            return Send(fromUser, toUser, subTitle, body, ccPairs, ishtml, useSsl, senduser, sendpass, attachmemts);
        }

        private static ReturnResult Send(string fromUser, string toUser, string subTitle, string body, Dictionary<string, string> ccPairs = null, bool ishtml = false, bool useSsl = true, string senduser = null, string sendpass = null, List<string> attachmemts = null)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                var config = Configer.ConfigerManager.Instance.getCofiger()?.CodeConfig;
                if (config != null)
                {
                    smtp.Host = config.StmpHost;
                    if (useSsl)
                    {
                        smtp.Port = string.IsNullOrWhiteSpace(config.StmpPort) ? 587 : Convert.ToInt32(config.StmpPort);
                    }
                    else
                    {
                        smtp.Port = string.IsNullOrWhiteSpace(config.StmpPort) ? 25 : Convert.ToInt32(config.StmpPort);
                    }
                    if (string.IsNullOrWhiteSpace(config.SendUser) && string.IsNullOrWhiteSpace(config.SendPass))
                    {
                        smtp.Credentials = new NetworkCredential(senduser, sendpass);
                    }
                    else
                    {
                        smtp.Credentials = new NetworkCredential(config.SendUser, config.SendPass);
                    }
                }
                smtp.EnableSsl = useSsl;
                MailMessage message = new MailMessage();
                if (!string.IsNullOrWhiteSpace(toUser))
                {
                    var arry = toUser.Split(',');
                    foreach (var item in arry)
                    {
                        message.To.Add(new MailAddress(item));
                    }
                }
                MailAddress fromms = new MailAddress(fromUser);
                message.From = fromms;
                if (ccPairs != null)
                {
                    foreach (var item in ccPairs)
                    {
                        message.CC.Add(new MailAddress(item.Key, item.Value));
                    }
                }
                message.IsBodyHtml = ishtml;
                message.Subject = subTitle;
                message.Body = body;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                FileStream fs = null;
                if (attachmemts != null && attachmemts.Any())
                {
                    foreach (var item in attachmemts)
                    {
                        if (File.Exists(item))
                        {
                            var fileName = Path.GetFileName(item);
                            fs = new FileStream(item, FileMode.Open);
                            message.Attachments.Add(new Attachment(fs, fileName));
                        }
                    }
                }
                smtp.Send(message);
                fs.Close();
                return new ReturnResult(ReturnResultCode.Success, null, "邮件发送成功");
            }
            catch (Exception ex)
            {
                logger.Error($"邮件发送异常,错误原因：{ex.Message}");
                return new ReturnResult(ReturnResultCode.Failed, null, "邮件发送失败");
            }
        }
    }
}