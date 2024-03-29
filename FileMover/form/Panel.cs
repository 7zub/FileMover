﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FileMover.context.EnumContext.ControlRules;

namespace FileMover
{
    public partial class Panel : Form
    {
        public ListRulesContext rulesContext;
        public ListHistoryContext historyContext;
        public ListSettingsContext settingsContext;
        public Dictionary<int, FileWatcher> DFW;
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
                settingsContext = new ListSettingsContext();
                this.Text = settingsContext.settings.Program_name;
                notifyIcon1.Text = settingsContext.settings.Program_name;
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
            new Rules(rulesContext, settingsContext).ShowDialog();
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ContextMenuStrip = contextMenuNotify;
                notifyIcon1.BalloonTipTitle = settingsContext.settings.Program_name;
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
                new Settings(settingsContext).ShowDialog();
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
            if (rulesContext.frules.Item.Where(i => i.Status).Count() == 0)
            {
                MessageBox.Show(
                    "Отсутствуют активные правила для запуска",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            try
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Panel));

                switch (execFW)
                {
                    case ExecFW.enable:
                        execFW = ExecFW.disable;
                        buttonStart.Image = (Image)resources.GetObject("bStopImage");
                        buttonStart.Text = "Остановить";
                        DFW = new Dictionary<int, FileWatcher>();

                        Task.Factory.StartNew(() =>
                        {
                            foreach (RuleItem i in rulesContext.frules.Item.Where(i => i.Status))
                                DFW.Add(i.Id, new FileWatcher(i, historyContext, this, settingsContext));

                            foreach (var i in DFW)
                                i.Value.FWExecuteRule();

                            MessageBox.Show(
                                String.Join("\n", DFW.Select(i => i.Value.res.Message)),
                                "Сообщение",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        });

                        break;

                    case ExecFW.disable:
                        execFW = ExecFW.enable;
                        buttonStart.Image = (Image)resources.GetObject("buttonStart.Image");
                        buttonStart.Text = "Запустить";

                        foreach (RuleItem i in rulesContext.frules.Item.Where(i => i.Status))
                        {
                            DFW[i.Id].Dispose();
                            DFW.Remove(i.Id);
                        }

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
            new Settings(settingsContext).ShowDialog();
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
                }).Reverse().ToList();

            for (int i = 0; i < GridHistory.Columns.Count; i++)
            {
                GridHistory.Columns[i].Width = settingsContext.settings.WidthColHistory[i];
            }

            GridHistory.ColumnWidthChanged += new DataGridViewColumnEventHandler(GridHistory_ColumnWidthChanged);
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            historyContext.history.Item.Clear();
            historyContext.EditHistory();
            GridRefresh();
        }

        private void Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (execFW == ExecFW.disable)
            {
                var f = MessageBox.Show(
                    "Закрыть программу? Мониторинг папок будет остановлен.\n" +
                    "Если хотите продолжить мониторинг сверните программу в трей.",
                    "Предупреждение",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning
                );

                if (f == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Panel_Activated(object sender, EventArgs e)
        {
            this.Text = settingsContext.settings.Program_name;
        }

        private void GridHistory_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            for (int i = 0; i < GridHistory.Columns.Count; i++)
            {
                settingsContext.settings.WidthColHistory[i] = GridHistory.Columns[i].Width;
                settingsContext.EditSettings();
            }
        }
    }
}