namespace TowerDef
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.cann1 = new System.Windows.Forms.Button();
            this.cann2 = new System.Windows.Forms.Button();
            this.cann3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 12);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(990, 480);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            // 
            // cann1
            // 
            this.cann1.Location = new System.Drawing.Point(12, 505);
            this.cann1.Name = "cann1";
            this.cann1.Size = new System.Drawing.Size(60, 60);
            this.cann1.TabIndex = 2;
            this.cann1.UseVisualStyleBackColor = true;
            this.cann1.Click += new System.EventHandler(this.cann1_Click);
            // 
            // cann2
            // 
            this.cann2.Location = new System.Drawing.Point(78, 505);
            this.cann2.Name = "cann2";
            this.cann2.Size = new System.Drawing.Size(60, 60);
            this.cann2.TabIndex = 3;
            this.cann2.UseVisualStyleBackColor = true;
            this.cann2.Click += new System.EventHandler(this.cann2_Click);
            // 
            // cann3
            // 
            this.cann3.Location = new System.Drawing.Point(144, 505);
            this.cann3.Name = "cann3";
            this.cann3.Size = new System.Drawing.Size(60, 60);
            this.cann3.TabIndex = 4;
            this.cann3.UseVisualStyleBackColor = true;
            this.cann3.Click += new System.EventHandler(this.cann3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(752, 552);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(524, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 572);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cann3);
            this.Controls.Add(this.cann2);
            this.Controls.Add(this.cann1);
            this.Controls.Add(this.glControl1);
            this.Name = "Form1";
            this.Text = "Tower Defense";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Button cann1;
        private System.Windows.Forms.Button cann2;
        private System.Windows.Forms.Button cann3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

