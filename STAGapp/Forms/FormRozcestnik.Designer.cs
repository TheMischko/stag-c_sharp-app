namespace STAGApp.Forms
{
    partial class FormRozcestnik
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.b1_prezentace = new System.Windows.Forms.Button();
            this.b2SpustitNahravku = new System.Windows.Forms.Button();
            this.bKalendar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b1_prezentace
            // 
            this.b1_prezentace.Location = new System.Drawing.Point(135, 65);
            this.b1_prezentace.Name = "b1_prezentace";
            this.b1_prezentace.Size = new System.Drawing.Size(132, 48);
            this.b1_prezentace.TabIndex = 0;
            this.b1_prezentace.Text = "Prezentovat";
            this.b1_prezentace.UseVisualStyleBackColor = true;
            this.b1_prezentace.Click += new System.EventHandler(this.buttonShowPrezentovat_Click);
            // 
            // b2SpustitNahravku
            // 
            this.b2SpustitNahravku.Location = new System.Drawing.Point(135, 193);
            this.b2SpustitNahravku.Name = "b2SpustitNahravku";
            this.b2SpustitNahravku.Size = new System.Drawing.Size(132, 48);
            this.b2SpustitNahravku.TabIndex = 1;
            this.b2SpustitNahravku.Text = "Spustit Nahrávku";
            this.b2SpustitNahravku.UseVisualStyleBackColor = true;
            this.b2SpustitNahravku.Click += new System.EventHandler(this.buttonShowPlayer_Click);
            // 
            // bKalendar
            // 
            this.bKalendar.Location = new System.Drawing.Point(135, 130);
            this.bKalendar.Name = "bKalendar";
            this.bKalendar.Size = new System.Drawing.Size(132, 48);
            this.bKalendar.TabIndex = 2;
            this.bKalendar.Text = "Kalendar";
            this.bKalendar.UseVisualStyleBackColor = true;
            this.bKalendar.Click += new System.EventHandler(this.buttonShowCalendar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 315);
            this.Controls.Add(this.bKalendar);
            this.Controls.Add(this.b2SpustitNahravku);
            this.Controls.Add(this.b1_prezentace);
            this.Name = "FormRozcestnik";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormRozcestnik_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b1_prezentace;
        private System.Windows.Forms.Button b2SpustitNahravku;
        private System.Windows.Forms.Button bKalendar;
    }
}

