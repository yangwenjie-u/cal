using CalDebugTools.Common.DBUtility;
using CalDebugTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalDebugTools
{
    public class TokenHeple
    {
        public static string GetToken(string username)
        {
            string token = Data.GetPost_http(Data.http_get_login, "{\"username\":\"" + username + "\",\"password\":\"99999\"}", "", null, "");
            TokenEntity te = JsonHelper.DeserializeJsonToObject<TokenEntity>(token);
            return te.data.token;
        }
    }
}