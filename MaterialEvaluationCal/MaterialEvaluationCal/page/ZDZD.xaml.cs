using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// ZDZD.xaml 的交互逻辑
    /// </summary>
    public partial class ZDZD : Window
    {
        public ZDZD()
        {

            InitializeComponent();
            tbzz.Text = "SITEM\\[\"(.+?)\\]";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string t = tb.Text;
            string s = ",";
            string r = tbzz.Text;
            if (r.Length <= 0)
            {
                r = "SITEM\\[\"(.+?)\\]";
            }

            Regex rg1 = new Regex("//(.+?)\r\n");
            t = rg1.Replace(t, "");
            t = t.Replace(" ", "");

            Regex rg = new Regex(r, RegexOptions.Multiline | RegexOptions.Singleline);
            //MatchCollection matches = Regex.Matches(t.ToUpper(), r, RegexOptions.IgnoreCase);
            MatchCollection matches = rg.Matches(t.ToUpper());

            foreach (Match item in matches)
            {
                try
                {
                    if (!s.Contains("," + item.Groups[1].Value.Replace("\"", "") + ","))
                    {
                        s = s + item.Groups[1].Value.Replace("\"", "") + ",";
                    }
                }
                catch
                {

                }

            }
            if (s.Length >= 2)
            {
                tb1.Text = s.Substring(1, s.Length - 2);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string s = txQcOld.Text;
            string strNew = "";
            string[] strOld = s.Split(',');
            ArrayList nStr = new ArrayList();
            for (int i = 0; i < strOld.Length; i++)
            {
                if (!nStr.Contains(strOld[i]))
                {
                    nStr.Add(strOld[i]);
                }
            }

            foreach (var i in nStr)
            {
                strNew += i + ",";
            }
            txQcNew.Text = strNew.Substring(0, strNew.Length - 1);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CGL cGL = new CGL();
            cGL.ShowDialog();
        }

    }
}
