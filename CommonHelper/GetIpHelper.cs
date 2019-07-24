using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class GetIpHelper
    {
        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>字符串</returns>
        //public string GetIp()
        //{
        //    string ip = "";
        //    string hostName = Dns.GetHostName();
        //    ip = Dns.GetHostEntry(hostName).AddressList.FirstOrDefault(d => d.AddressFamily.ToString().Equals("InterNetwork")).ToString();
        //    return ip;
        //}
        /// <summary>
        /// 获取客户端Ip
        /// </summary>
        /// <returns></returns>
        //public String GetClientIp()
        //{
        //    String clientIP = "";
        //    if (System.Web.HttpContext.Current != null)
        //    {
        //        clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //        if (string.IsNullOrEmpty(clientIP) || (clientIP.ToLower() == "unknown"))
        //        {
        //            clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
        //            if (string.IsNullOrEmpty(clientIP))
        //            {
        //                clientIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //            }
        //        }
        //        else
        //        {
        //            clientIP = clientIP.Split(',')[0];
        //        }
        //    }
        //    return clientIP;
        //}
        /// <summary>
        /// 获取web客户端ip
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {
            string userIP = "未获取用户IP";
            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP simone 090805
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }
            return userIP;
        }
 
    }
}
