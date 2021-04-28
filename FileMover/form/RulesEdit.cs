using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public partial class RulesEdit : Form
    {
        private ListRulesContext rulesContext;
        private ControlRules.value cv;
        private CommonOpenFileDialog folderDailog;
        private int Id;

        public RulesEdit(ListRulesContext rulesContext, ControlRules.value cv, int Id = 0)
        {
            InitializeComponent();
            this.rulesContext = rulesContext;

            this.cv = cv;
            this.Id = Id;

            folderDailog = new CommonOpenFileDialog();
            folderDailog.IsFolderPicker = true;
            BindingSource bindOp = new BindingSource(EnumToDic<op>(), null);
            comboBoxOp.DataSource = bindOp;
            comboBoxOp.DisplayMember = "Value";
            comboBoxOp.ValueMember = "Key";
            BindingSource bindIf = new BindingSource(EnumToDic<ifEx>(), null);
            comboBoxIf.DataSource = bindIf;
            comboBoxIf.DisplayMember = "Value";
            comboBoxIf.ValueMember = "Key";

            if (cv == ControlRules.value.edit)
            {
                RuleItem rule = rulesContext.frules.Item.Find(x => x.Id == Id);
                textBoxDs.Text = rule.DirStart;
                textBoxDd.Text = rule.DirDest;
                comboBoxOp.SelectedValue = rule.Operation.ToString();
                comboBoxIf.SelectedValue = rule.IfExist.ToString();
                textBoxMask.Text = rule.FileMask;
                checkBoxEnable.Checked = rule.Status;
            }
        }

        private void buttonChoseDirDs_Click(object sender, EventArgs e)
        {
            if (folderDailog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBoxDs.Text = folderDailog.FileName;
            }
        }

        private void buttonChoseDirDd_Click(object sender, EventArgs e)
        {
            if (folderDailog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBoxDd.Text = folderDailog.FileName;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                RuleItem rule;

                if (!Directory.Exists(textBoxDs.Text))
                    throw new Exception("Указана неверная папка поиска!");

                if (!Directory.Exists(textBoxDd.Text))
                    throw new Exception("Указана неверная поиска назначения!");

                if (textBoxMask.TextLength > textBoxMask.MaxLength ||
                    textBoxMask.TextLength == 0 ||
                    !Regex.Match(textBoxMask.Text, @"^[a-zA-Zа-яА-Я0-9\*\?\.\\_\-]*$").Success)
                    throw new Exception("Указана неверная маска поиска!");

                if (textBoxDs.Text.EndsWith(@"\"))
                    textBoxDs.Text = textBoxDs.Text.Remove(textBoxDs.Text.Length - 1);

                if (textBoxDd.Text.EndsWith(@"\"))
                    textBoxDd.Text = textBoxDd.Text.Remove(textBoxDd.Text.Length - 1);

                switch (cv)
                {
                    case ControlRules.value.add:
                        rule = new RuleItem()
                        {
                            Id = rulesContext.frules.Item.Count > 0 ? rulesContext.frules.Item.Max(f => f.Id) + 1 : 1,
                            Status = checkBoxEnable.Checked,
                            DirStart = textBoxDs.Text,
                            DirDest = textBoxDd.Text,
                            Operation = (op)Enum.Parse(typeof(op), comboBoxOp.SelectedValue.ToString()),
                            IfExist = (ifEx)Enum.Parse(typeof(ifEx), comboBoxIf.SelectedValue.ToString()),
                            FileMask = textBoxMask.Text
                        };
                        rulesContext.frules.Item.Add(rule);
                        rulesContext.EditRule();
                        break;

                    case ControlRules.value.edit:
                        rule = rulesContext.frules.Item.Find(x => x.Id == Id);
                        rule.Status = checkBoxEnable.Checked;
                        rule.DirStart = textBoxDs.Text;
                        rule.DirDest = textBoxDd.Text;
                        rule.Operation = (op)Enum.Parse(typeof(op), comboBoxOp.SelectedValue.ToString());
                        rule.IfExist = (ifEx)Enum.Parse(typeof(ifEx), comboBoxIf.SelectedValue.ToString());
                        rule.FileMask = textBoxMask.Text;
                        rulesContext.EditRule();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                   "Ошибка сохранения:\n" + ex.Message,
                   "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            MessageBox.Show("Правило успешно сохранено!");
            this.Close();
        }
    }
}