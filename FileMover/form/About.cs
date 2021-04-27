using System;
using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;

namespace FileMover
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            labelHead.Text = Const.DefaultProgramName;
        }

        private void linkGit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/7zub/FileMover");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://instagram.com/kv.samir");
        }
    }
}
