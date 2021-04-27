using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static FileMover.context.EnumContext.ControlRules;

namespace FileMover
{
    public partial class Panel : Form
    {
        public ListRulesContext rulesContext;
        public ListHistoryContext historyContext;
        public Dictionary<int, FileWatcher> DFW = new Dictionary<int, FileWatcher>();
        private ExecFW execFW = ExecFW.enable;
        public Panel()
        {
            InitializeComponent();
        }

        private void Panel_Load(object sender, EventArgs e)
        {
            try
            {
                rulesContext = new ListRulesContext();
                historyContext = new ListHistoryContext();
                this.Text = rulesContext.frules.Program_name;
                notifyIcon1.Text = rulesContext.frules.Program_name;
                GridRefresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (execFW == ExecFW.disable)
                buttonStart.PerformClick();
            new Rules(rulesContext).ShowDialog();
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ContextMenuStrip = contextMenuNotify;
                notifyIcon1.BalloonTipTitle = rulesContext.frules.Program_name;
                notifyIcon1.BalloonTipText = "Программа работает в фоновом режиме";
                notifyIcon1.ShowBalloonTip(4000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Settings"] == null)
                new Settings().ShowDialog();
            else
                Application.OpenForms["Settings"].Focus();
        }

        private void программеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["About"] == null)
                new About().ShowDialog();
            else
                Application.OpenForms["About"].Focus();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Panel));

                switch (execFW)
                {
                    case ExecFW.enable:
                        foreach (RuleItem i in rulesContext.frules.Item.Where(i => i.Status))
                            DFW.Add(i.Id, new FileWatcher(i, historyContext, this));

                        execFW = ExecFW.disable;
                        buttonStart.Image = (Image)resources.GetObject("bStopImage");
                        buttonStart.Text = "Остановить";

                        MessageBox.Show(
                            String.Join("\n", DFW.Select(i => i.Value.res.Message)),
                            "Сообщение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        break;

                    case ExecFW.disable:
                        foreach (RuleItem i in rulesContext.frules.Item.Where(i => i.Status))
                        {
                            DFW[i.Id].Dispose();
                            DFW.Remove(i.Id);
                        }

                        execFW = ExecFW.enable;
                        buttonStart.Image = (Image)resources.GetObject("buttonStart.Image");
                        buttonStart.Text = "Запустить";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка запуска службы\n" + ex.Message,
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                );
            }
        }

        private void настройкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }

        public void GridRefresh()
        {
            GridHistory.DataSource
                = (from hc in historyContext.history.Item
                select new
                {
                    Дата = hc.DateMove,
                    Файл = hc.Filename,
                    Поиск = hc.DirStart,
                    Назначение = hc.DirDest,
                    Длит = hc.Duration + " мс",
                    Размер = $"{Math.Round(hc.FileSize / (hc.FileSize >= Math.Pow(1024, 2) ? Math.Pow(1024, 2) : 1024), 2)} {(hc.FileSize >= Math.Pow(1024, 2) ? "МБ" : "КБ")}",
                    Итог = hc.result.Message
                }).ToList();
        }
    }
}