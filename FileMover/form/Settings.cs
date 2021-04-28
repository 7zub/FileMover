using System;
using System.Resources;
using System.Windows.Forms;

namespace FileMover
{
    public partial class Settings : Form
    {
        private ListSettingsContext settingsContext;

        public Settings(ListSettingsContext settingsContext)
        {
            InitializeComponent();

            this.settingsContext = settingsContext;
            textBox1.Text = this.settingsContext.settings.Program_name;
            textBox2.Text = this.settingsContext.settings.RenameFile;
            textBox3.Text = this.settingsContext.settings.MaxCountHistory.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                settingsContext.settings.Program_name = textBox1.Text;
                settingsContext.settings.RenameFile = textBox2.Text;
                settingsContext.settings.MaxCountHistory = Convert.ToInt32(textBox3.Text);
                settingsContext.EditSettings();

                MessageBox.Show(
                    "Настрйоки успешно сохранены",
                    "Уведомление",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Неизвестня ошибка сохранения\n" + ex.Message,
                    "Ошибка",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error
                );
            }
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
    }
}