namespace L6_7
{
    partial class Form1
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
            this.tbFor = new System.Windows.Forms.TextBox();
            this.tbLexems = new System.Windows.Forms.TextBox();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbFor
            // 
            this.tbFor.Location = new System.Drawing.Point(12, 12);
            this.tbFor.Name = "tbFor";
            this.tbFor.Size = new System.Drawing.Size(639, 20);
            this.tbFor.TabIndex = 0;
            this.tbFor.Text = "for (i := 0; i < 10; i++) do";
            // 
            // tbLexems
            // 
            this.tbLexems.Location = new System.Drawing.Point(334, 58);
            this.tbLexems.Multiline = true;
            this.tbLexems.Name = "tbLexems";
            this.tbLexems.Size = new System.Drawing.Size(317, 228);
            this.tbLexems.TabIndex = 1;
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(12, 58);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(307, 228);
            this.tbInfo.TabIndex = 2;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.btnAnalyze.Location = new System.Drawing.Point(12, 303);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(639, 34);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Анализировать";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 349);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.tbLexems);
            this.Controls.Add(this.tbFor);
            this.Name = "Form1";
            this.Text = "Лабораторная работа №6-7";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFor;
        private System.Windows.Forms.TextBox tbLexems;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Button btnAnalyze;
    }
}

