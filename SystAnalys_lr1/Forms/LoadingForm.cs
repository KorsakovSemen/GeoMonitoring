using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1.Forms
{
    public partial class LoadingForm : MetroForm
    {
        public LoadingForm()
        {
            InitializeComponent();
        }
        public bool close = false;
        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
            {
                ControlBox = false;
                base.OnClosing(e);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }

        }
    }
}
