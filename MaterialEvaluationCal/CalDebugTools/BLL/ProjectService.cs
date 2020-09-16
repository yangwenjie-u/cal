using CalDebugTools.Common.DBUtility;
using CalDebugTools.DAL;
using Renci.SshNet.Messages;using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools.BLL
{
    public class ProjectService
    {
        ProjectServiceDao _Dao = new ProjectServiceDao();
        public void CreateProjectTable(string jcjgAbbrevition, string projectCode, bool addJcjt, bool addJcjg, out string oMsg)
        {
            oMsg = "";
            if (string.IsNullOrEmpty(projectCode))
            {
                oMsg = "err：请输入项目代号。";
                return;
            }

            _Dao.CreateProjectTable(jcjgAbbrevition, projectCode, addJcjt, addJcjg, out oMsg);
         
        }
    }
}
