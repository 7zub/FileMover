using System;
using System.Linq;
using System.Windows.Forms;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public partial class Rules : Form
    {
        private ListRulesContext rulesContext;

        public Rules(ListRulesContext rulesContext)
        {
            InitializeComponent();
            this.rulesContext = rulesContext;
            this.Text = rulesContext.frules.Program_name + ": " + this.Text;
        }

        private void Rules_Load(object sender, EventArgs e)
        {
            GridRefresh();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            new RulesEdit(rulesContext, ControlRules.value.add).ShowDialog();
            GridRefresh();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (GridRules.SelectedRows.Count == 1)
            {
                new RulesEdit(
                    rulesContext,
                    ControlRules.value.edit,
                    (int)GridRules.CurrentRow.Cells["Id"].Value
                ).ShowDialog();
                GridRefresh();
            }
            else
            {
                MessageBox.Show(
                    "Не выбрано правило для редактирования!",
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation
                );
            }

        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            if (GridRules.SelectedRows.Count == 1)
            {
                rulesContext.frules.Item.Find(x => x.Id == (int)GridRules.CurrentRow.Cells["Id"].Value).Status =
               !rulesContext.frules.Item.Find(x => x.Id == (int)GridRules.CurrentRow.Cells["Id"].Value).Status;
                rulesContext.EditRule();
                GridRefresh();
            }
            else
            {
                MessageBox.Show(
                    "Не выбрано правило для включения/отключения!",
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation
                );
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (GridRules.SelectedRows.Count == 1)
            {
                DialogResult dialogResult = MessageBox.Show(
                                            "Вы уверены что хотите удалить правило?",
                                            "Предупреждение",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning
                                        );
                if (dialogResult == DialogResult.Yes)
                    try
                    {
                        rulesContext.frules.Item.RemoveAll(x => x.Id == (int)GridRules.CurrentRow.Cells["Id"].Value);
                        rulesContext.EditRule();
                        GridRefresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                           "Ошибка удаления правила!\n" + ex.Message,
                           "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                       );
                    }
            }
            else
            {
                MessageBox.Show(
                    "Не выбрано правило для удаления!",
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Exclamation
                );
            }
        }

        public void GridRefresh()
        {
            GridRules.DataSource = (from rc in rulesContext.frules.Item
                                    join o in EnumToDic<op>() on rc.Operation.ToString() equals o.Key
                                    join i in EnumToDic<ifEx>() on rc.IfExist.ToString() equals i.Key
                                    select new
                                    {
                                        Id = rc.Id,
                                        Маска = rc.FileMask,
                                        Поиск = rc.DirStart,
                                        Назначение = rc.DirDest,
                                        Действие = o.Value,
                                        Замена = i.Value,
                                        Статус = rc.Status
                                    }).ToList();
        }

        private void GridRules_SelectionChanged(object sender, EventArgs e)
        {
            if (GridRules.SelectedRows.Count == 1)
            {
                buttonStatus.Enabled = true;

                switch (rulesContext.frules.Item.Find(x => x.Id == (int)GridRules.CurrentRow.Cells["Id"].Value).Status)
                {
                    case true:
                        buttonStatus.Text = "Отключить";
                        break;

                    case false:
                        buttonStatus.Text = "Включить";
                        break;
                }
            }
            else
                buttonStatus.Enabled = false;

        }

        private void GridRules_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRules.Columns["Id"].Visible = false;
            GridRules.ClearSelection();
        }
    }
}