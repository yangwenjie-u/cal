using System;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// WLB.xaml 的交互逻辑
    /// </summary>
    public partial class LSL : Window
    {
        public LSL()
        {
            InitializeComponent();
        }

        private void Button_Click_AQW(object sender, RoutedEventArgs e)
        {
            AQW a = new AQW();
            a.ShowDialog();
        }

        private void Button_Click_XJL(object sender, RoutedEventArgs e)
        {
            XJL a = new XJL();
            a.ShowDialog();
        }

        private void Button_Click_KF(object sender, RoutedEventArgs e)
        {
            KF a = new KF();
            a.ShowDialog();
        }

        private void Button_Click_YKJ(object sender, RoutedEventArgs e)
        {
            YKJ a = new YKJ();
            a.ShowDialog();
        }

        private void Button_Click_FM(object sender, RoutedEventArgs e)
        {
            FM a = new FM();
            a.ShowDialog();
        }

        private void Button_Click_SPA(object sender, RoutedEventArgs e)
        {
            SPA a = new SPA();
            a.ShowDialog();
        }

        private void Button_Click_CJL(object sender, RoutedEventArgs e)
        {
            CJL a = new CJL();
            a.ShowDialog();
        }

        private void Button_Click_WCX(object sender, RoutedEventArgs e)
        {
            WCX a = new WCX();
            a.ShowDialog();
        }

        private void Button_Click_SPH(object sender, RoutedEventArgs e)
        {
            SPH a = new SPH();
            a.ShowDialog();
        }

        private void Button_Click_BCR(object sender, RoutedEventArgs e)
        {
            BCR a = new BCR();
            a.ShowDialog();
        }

       
    }
}
