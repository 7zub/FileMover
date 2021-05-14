
namespace FileMover
{
    partial class RulesEdit
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RulesEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDd = new System.Windows.Forms.TextBox();
            this.buttonChoseDirDs = new System.Windows.Forms.Button();
            this.buttonChoseDirDd = new System.Windows.Forms.Button();
            this.comboBoxOp = new System.Windows.Forms.ComboBox();
            this.textBoxMask = new System.Windows.Forms.TextBox();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.comboBoxIf = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Папка поиска:";
            // 
            // textBoxDs
            // 
            this.textBoxDs.Location = new System.Drawing.Point(18, 35);
            this.textBoxDs.MaxLength = 2000;
            this.textBoxDs.Name = "textBoxDs";
            this.textBoxDs.Size = new System.Drawing.Size(318, 22);
            this.textBoxDs.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Папка назначения:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Операция:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Маска поиска (*, ?):";
            // 
            // textBoxDd
            // 
            this.textBoxDd.Location = new System.Drawing.Point(18, 91);
            this.textBoxDd.MaxLength = 2000;
            this.textBoxDd.Name = "textBoxDd";
            this.textBoxDd.Size = new System.Drawing.Size(318, 22);
            this.textBoxDd.TabIndex = 6;
            // 
            // buttonChoseDirDs
            // 
            this.buttonChoseDirDs.Location = new System.Drawing.Point(342, 34);
            this.buttonChoseDirDs.Name = "buttonChoseDirDs";
            this.buttonChoseDirDs.Size = new System.Drawing.Size(33, 23);
            this.buttonChoseDirDs.TabIndex = 7;
            this.buttonChoseDirDs.Text = "...";
            this.buttonChoseDirDs.UseVisualStyleBackColor = true;
            this.buttonChoseDirDs.Click += new System.EventHandler(this.buttonChoseDirDs_Click);
            // 
            // buttonChoseDirDd
            // 
            this.buttonChoseDirDd.Location = new System.Drawing.Point(342, 90);
            this.buttonChoseDirDd.Name = "buttonChoseDirDd";
            this.buttonChoseDirDd.Size = new System.Drawing.Size(33, 23);
            this.buttonChoseDirDd.TabIndex = 8;
            this.buttonChoseDirDd.Text = "...";
            this.buttonChoseDirDd.UseVisualStyleBackColor = true;
            this.buttonChoseDirDd.Click += new System.EventHandler(this.buttonChoseDirDd_Click);
            // 
            // comboBoxOp
            // 
            this.comboBoxOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOp.FormattingEnabled = true;
            this.comboBoxOp.Location = new System.Drawing.Point(215, 129);
            this.comboBoxOp.Name = "comboBoxOp";
            this.comboBoxOp.Size = new System.Drawing.Size(160, 24);
            this.comboBoxOp.TabIndex = 9;
            // 
            // textBoxMask
            // 
            this.textBoxMask.Location = new System.Drawing.Point(215, 165);
            this.textBoxMask.MaxLength = 255;
            this.textBoxMask.Name = "textBoxMask";
            this.textBoxMask.Size = new System.Drawing.Size(160, 22);
            this.textBoxMask.TabIndex = 10;
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Checked = true;
            this.checkBoxEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnable.Location = new System.Drawing.Point(18, 236);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(180, 21);
            this.checkBoxEnable.TabIndex = 11;
            this.checkBoxEnable.Text = "Активировать правило";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // comboBoxIf
            // 
            this.comboBoxIf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIf.FormattingEnabled = true;
            this.comboBoxIf.Items.AddRange(new object[] {
            "Перезаписать",
            "Переименовать",
            "Игнорировать"});
            this.comboBoxIf.Location = new System.Drawing.Point(215, 202);
            this.comboBoxIf.Name = "comboBoxIf";
            this.comboBoxIf.Size = new System.Drawing.Size(160, 24);
            this.comboBoxIf.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Если файл уже существует:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Если файл уже существует:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(278, 259);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(97, 28);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // RulesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 298);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxIf);
            this.Controls.Add(this.checkBoxEnable);
            this.Controls.Add(this.textBoxMask);
            this.Controls.Add(this.comboBoxOp);
            this.Controls.Add(this.buttonChoseDirDd);
            this.Controls.Add(this.buttonChoseDirDs);
            this.Controls.Add(this.textBoxDd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RulesEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор правил";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDd;
        private System.Windows.Forms.Button buttonChoseDirDs;
        private System.Windows.Forms.Button buttonChoseDirDd;
        private System.Windows.Forms.ComboBox comboBoxOp;
        private System.Windows.Forms.TextBox textBoxMask;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.ComboBox comboBoxIf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSave;
    }
}

