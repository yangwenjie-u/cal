using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Qsf.xaml 的交互逻辑
    /// </summary>
    public partial class Qsf : Window
    {
        public Qsf()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CGL cGL = new CGL();
            cGL.ShowDialog();
        }

        private void Button_Click_GYF(object sender, RoutedEventArgs e)
        {
            GYF gYF = new GYF();
            gYF.ShowDialog();
        }

        private void Button_SC(object sender, RoutedEventArgs e)
        {
            SC sC= new SC();
            sC.ShowDialog();
        }

        private void Button_QNX(object sender, RoutedEventArgs e)
        {
            QNX qNX = new QNX();
            qNX.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SSJ sSJ = new SSJ();
            sSJ.ShowDialog();
        }

        private void Button_Click_RF(object sender, RoutedEventArgs e)
        {
            RF rF = new RF();
            rF.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LKH lKH = new LKH();
            lKH.ShowDialog();
        }
        private void Button_Click_QX(object sender, RoutedEventArgs e)
        {
            //QX qX = new QX();
            //qX.ShowDialog();
        }
        private void Button_Click_test(object sender, RoutedEventArgs e)
        {
            //List<string> l = new List<string>();
            //for (int i = 0; i < 5; i++)
            //{
            //    l.Add(i.ToString());
            //    if (i==3)
            //    {
            //        i = 2;
            //    }
            //}
            //List<string> ll = new List<string>();
            //ll = l;
            ZDZD zdzd = new ZDZD();
            zdzd.ShowDialog();

        }

        private void Button_Click_LKH(object sender, RoutedEventArgs e)
        {
            LKH lKH = new LKH();
            lKH.ShowDialog();
        }
    }
}
