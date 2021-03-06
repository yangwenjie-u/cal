﻿using CalDebugTools.Common.DBUtility;
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
            string token = Data.GetPost_http(Data.http_get_login, "{\"username\":\"" + username + "\",\"password\":\"88888\"}", "", null, "");
            TokenEntity te = JsonHelper.DeserializeJsonToObject<TokenEntity>(token);
            if (te.data == null)
            {
                throw new Exception(te.message);
            }
            return te.data.token;
        }
    }
}