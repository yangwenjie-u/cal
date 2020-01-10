using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialEvaluationCal.page
{
    public partial class FormYWJ : Form
    {
        public FormYWJ()
        {
            InitializeComponent();
        }

        private void btn_GHF_Click(object sender, EventArgs e)
        {
            GHF gHF = new GHF();
            gHF.Show();
        }
    }
}
