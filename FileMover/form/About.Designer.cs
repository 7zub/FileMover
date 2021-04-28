
namespace FileMover
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelHead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkGit = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(17, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(573, 167);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Разработчик: Курбанов Самир";
            // 
            // labelHead
            // 
            this.labelHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHead.Location = new System.Drawing.Point(12, 9);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(598, 42);
            this.labelHead.TabIndex = 2;
            this.labelHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "при поддержке: ТФОМС РД";
            // 
            // linkGit
            // 
            this.linkGit.AutoSize = true;
            this.linkGit.Location = new System.Drawing.Point(231, 224);
            this.linkGit.Name = "linkGit";
            this.linkGit.Size = new System.Drawing.Size(52, 17);
            this.linkGit.TabIndex = 4;
            this.linkGit.TabStop = true;
            this.linkGit.Text = "GitHub";
            this.linkGit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGit_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "|";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(301, 224);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 17);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Instagram";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 282);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkGit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelHead);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "О программе";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkGit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

