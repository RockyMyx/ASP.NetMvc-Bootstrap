using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace MvcBootstrapManage.Helpers
{
    public class MailHelper
    {
        private string _smtpService;
        private string _loginName;
        private string _password;
        private bool _enableSSL;
        private List<Attachment> _attachments;

        public MailHelper(string smtpService, string loginName, string password, bool enableSSL, List<Attachment> attachments)
        {
            this._smtpService = smtpService;
            this._loginName = loginName;
            this._password = password;
            this._enableSSL = enableSSL;
            this._attachments = attachments;
        }

        public MailHelper(string smtpService, string loginName, string password, List<Attachment> attachments)
            : this(smtpService, loginName, password, true, attachments)
        {
        }

        public MailHelper(string smtpService, string loginName, string password)
            : this(smtpService, loginName, password, true, null)
        {
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="senderName">发送人姓名，默认为SenderName</param>
        /// <param name="address">发件的目的地址</param>
        /// <param name="title">邮件标题，默认为Title</param>
        /// <param name="content">发送的内容，默认为Content</param>
        public bool Send(string senderName, string address, string title,
    string content)
        {
            try
            {
                senderName = senderName ?? "SenderName";
                title = title ?? "Title";
                content = content ?? "Content";

                MailMessage mail = new MailMessage();
                mail.To.Add(address);
                mail.From = new MailAddress(this._loginName, senderName, Encoding.UTF8);
                mail.Subject = title;
                mail.Body = content;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = false;
                mail.Priority = MailPriority.High;
                if (this._attachments != null)
                {
                    foreach (Attachment a in _attachments)
                    {
                        mail.Attachments.Add(a);
                    }
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(this._loginName, this._password);
                smtp.Host = this._smtpService;
                smtp.EnableSsl = this._enableSSL;
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    #region 各大邮箱信息
    /*
        >新浪邮箱smtp服务器
        外发服务器:smtp.vip.sina.com
        收件服务器:pop3.vip.sina.com
        新浪免费邮件
        外发服务器:smtp.sina.com.cn
        收件服务器:pop3.sina.com.cn
         
        >163邮箱smtp服务器
        pop： pop.163.com
        smtp： smtp.163.com
         
        >QQ邮箱smtp服务器及端口
        接收邮件服务器：imap.qq.com，使用SSL，端口号993
        发送邮件服务器：smtp.qq.com，使用SSL，端口号465或587

        >yahoo邮箱smtp服务器
        接：pop.mail.yahoo.com.cn
        发：smtp.mail.yahoo.com
         
        >126邮箱smtp服务器
        pop： pop.126.com
        smtp： smtp.126.com
        新浪免费邮箱
        POP3：pop.sina.com
        SMTP：smtp.sina.com
        SMTP端口号：25
         
        新浪VIP邮箱
        POP3：pop3.vip.sina.com
        SMTP：smtp.vip.sina.com
        SMTP端口号：25
         
        新浪企业邮箱
        POP3：pop.sina.com
        SMTP：smtp.sina.com
        SMTP端口号：25
         
        雅虎邮箱
        POP3：pop.mail.yahoo.cn
        SMTP：smtp.mail.yahoo.cn
        SMTP端口号：25
          
        搜狐邮箱
        POP3：pop3.sohu.com
        SMTP：smtp.sohu.com
         SMTP端口号：25
         
        TOM邮箱
        POP3：pop.tom.com
        SMTP：smtp.tom.com
         SMTP端口号：25
         
        Gmail邮箱
        POP3：pop.gmail.com
        SMTP：smtp.gmail.com
        SMTP端口号：587 或 25
         
        QQ邮箱
        POP3：pop.qq.com
        SMTP：smtp.qq.com
        SMTP端口号：25
         
         
        263邮箱
        域名：263.net
        POP3：263.net
        SMTP：smtp.263.net
        SMTP端口号：25
         
        域名：x263.net
        POP3：pop.x263.net
        SMTP：smtp.x263.net
        SMTP端口号：25
         
        域名：263.net.cn
        POP3：263.net.cn
        SMTP：263.net.cn
        SMTP端口号：25
         
        域名：炫我型
        POP3：pop.263xmail.com
        SMTP：smtp.263xmail.com
        SMTP端口号：25
         
        21CN  免费邮箱
        POP3：pop.21cn.com
        SMTP：smtp.21cn.com
        IMAP：imap.21cn.com
        SMTP端口号：25
         
        21CN  经济邮邮箱
        POP3：pop.21cn.com
        SMTP：smtp.21cn.com
        SMTP端口号：25
         
        21CN  商务邮邮箱
        POP3：pop.21cn.net
        SMTP：smtp.21cn.net
        SMTP端口号：25
          
        21CN  快感邮箱
        POP3：vip.21cn.com
        SMTP：vip.21cn.com
        SMTP端口号：25
         
        21CN  Y邮箱
        POP3：pop.y.vip.21cn.com
        SMTP：smtp.y.vip.21cn.com
        SMTP端口号：25
         
        中华网任我邮邮箱
        POP3：rwpop.china.com
        SMTP：rwsmtp.china.com
         SMTP端口号：25
         
        中华网时尚、商务邮箱
        POP3：pop.china.com
        SMTP：smtp.china.com
        SMTP端口号：25
     */
    #endregion
}