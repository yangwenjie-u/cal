using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CalDebugTools
{
    public class Data
    {
        public static string http_getapiurl = ConfigurationManager.AppSettings["http_getapiurl"];
        public static string http_setapiurl = ConfigurationManager.AppSettings["http_setapiurl"];
        public static string http_get_sylbapiurl = ConfigurationManager.AppSettings["http_get_sylbapiurl"];
        public static string http_get_parameterapiurl = ConfigurationManager.AppSettings["http_getparameterapiurl"];
        public static string http_get_login = ConfigurationManager.AppSettings["http_get_login"];

        public static string getMd5(string ConvertString)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(provider.ComputeHash(Encoding.Default.GetBytes(ConvertString))).Replace("-", "").ToLower();
        }

        public static string GetHtmlByPost(string url, string indata, string referer, CookieContainer myCookieContainer,string header)
        {
            string str = string.Empty;
            try
            {
                str = GetPost_http(url, indata, referer, myCookieContainer, header);
            }
            catch (Exception)
            {
                try
                {
                    str = GetPost_http(url, indata, referer, myCookieContainer, header);
                }
                catch (Exception)
                {
                    try
                    {
                        str = GetPost_http(url, indata, referer, myCookieContainer, header);
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    return str;
                }
            }
            return str;
        }

        public static string GetPost_http(string url, string indata, string referer, CookieContainer myCookieContainer,string header)
        {
            string str2;
            try
            {
                string str = "";
                byte[] bytes = Encoding.UTF8.GetBytes(indata);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Headers.Add("Accept-Language", "zh-cn");
                request.Headers.Add("UA-CPU", "x86");
                request.Headers.Add("Cache-Control", "no-cache");
                if (!string.IsNullOrEmpty(header))
                {
                    request.Headers.Add("Authorization", "Bearer " + header);
                }
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Referer = referer;
                request.CookieContainer = myCookieContainer;
                request.KeepAlive = true;
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                if (url.Contains("https:"))
                {
                    ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Data.ValidateServerCertificate));
                    request.ServicePoint.Expect100Continue = false;
                }
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                str2 = str;
            }
            catch (WebException exception)
            {
                throw exception;
            }
            return str2;
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        public static string GetHtml(string url,string header)
        {
            string result = string.Empty;
            WebClient wb = new WebClient(); if (!string.IsNullOrEmpty(header))
            {
                wb.Headers.Add("Authorization", "Bearer " + header);
            }
            Stream response = wb.OpenRead(url);
            StreamReader reader = new StreamReader(response, System.Text.Encoding.UTF8, true, 256000);
            string alljsonresult = reader.ReadToEnd();
            return alljsonresult;
        }
    }
}
