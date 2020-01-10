using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hh.fw.Standard
{
    public interface CalculateTemplateI
    {
        //bool Calculate(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string strError);
        bool Calculate(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IList<IDictionary<string, string>>> retData, ref string strError);
    }
}
