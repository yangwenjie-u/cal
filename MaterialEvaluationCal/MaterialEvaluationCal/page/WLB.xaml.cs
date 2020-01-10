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
    /// WLB.xaml 的交互逻辑
    /// </summary>
    public partial class WLB : Window
    {
        public WLB()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    CGL cGL = new CGL();
        //    cGL.ShowDialog();
        //}

        private void Button_Click_WZ(object sender, RoutedEventArgs e)
        {
            WZ wZ = new WZ();
            wZ.ShowDialog();
        }

        private void Button_Click_XC(object sender, RoutedEventArgs e)
        {
            XC xC = new XC();
            xC.ShowDialog();
        }

        private void Button_Click_XS(object sender, RoutedEventArgs e)
        {
            XS xS = new XS();
            xS.ShowDialog();
        }

        private void Button_Click_YJ(object sender, RoutedEventArgs e)
        {
            YJ yJ = new YJ();
            yJ.ShowDialog();
        }

        private void Button_Click_GGH(object sender, RoutedEventArgs e)
        {
            GGH gGH = new GGH();
            gGH.ShowDialog();
        }

        private void Button_Click_WB(object sender, RoutedEventArgs e)
        {
            WB wB = new WB();
            wB.ShowDialog();
        }

        private void Button_Click_TB(object sender, RoutedEventArgs e)
        {
            TB tB = new TB();
            tB.ShowDialog();
        }

        private void Button_Click_GX(object sender, RoutedEventArgs e)
        {
            GX gX = new GX();
            gX.ShowDialog();
        }

        private void Button_Click_GXF(object sender, RoutedEventArgs e)
        {
            GXF gXF = new GXF();
            gXF.ShowDialog();
        }

        private void Button_Click_ZZQ(object sender, RoutedEventArgs e)
        {
            ZZQ zZQ = new ZZQ();
            zZQ.ShowDialog();
        }

        private void Button_Click_XJ(object sender, RoutedEventArgs e)
        {
            XJ xJ = new XJ();
            xJ.ShowDialog();
        }

        private void Button_Click_MZ(object sender, RoutedEventArgs e)
        {
            MZ mZ = new MZ();
            mZ.ShowDialog();
        }

        private void Button_Click_JL(object sender, RoutedEventArgs e)
        {
            JL jL = new JL();
            jL.ShowDialog();
        }

        private void Button_Click_BTS(object sender, RoutedEventArgs e)
        {
            BTS bTS = new BTS();
            bTS.ShowDialog();
        }

        private void Button_Click_CZ(object sender, RoutedEventArgs e)
        {
            CZ cZ = new CZ();
            cZ.ShowDialog();
        }

        private void Button_Click_CZJ(object sender, RoutedEventArgs e)
        {
            CZJ cZJ = new CZJ();
            cZJ.ShowDialog();
        }

        private void Button_Click_FP(object sender, RoutedEventArgs e)
        {
            FP fP = new FP();
            fP.ShowDialog();
        }

        private void Button_Click_FQ(object sender, RoutedEventArgs e)
        {
            FQ fQ = new FQ();
            fQ.ShowDialog();
        }

        private void Button_Click_NCL(object sender, RoutedEventArgs e)
        {
            NCL nCL = new NCL();
            nCL.ShowDialog();
        }

        private void Button_Click_SJX(object sender, RoutedEventArgs e)
        {
            SJX sJX = new SJX();
            sJX.ShowDialog();
        }
    }
}
