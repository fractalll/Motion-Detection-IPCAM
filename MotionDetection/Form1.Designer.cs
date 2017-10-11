namespace IpCamMotionDetection
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
            this.btnTurnAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTurnAll
            // 
            this.btnTurnAll.Location = new System.Drawing.Point(12, 12);
            this.btnTurnAll.Name = "btnTurnAll";
            this.btnTurnAll.Size = new System.Drawing.Size(147, 23);
            this.btnTurnAll.TabIndex = 0;
            this.btnTurnAll.Text = "Включить все";
            this.btnTurnAll.UseVisualStyleBackColor = true;
            this.btnTurnAll.Click += new System.EventHandler(this.btnTurnAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 509);
            this.Controls.Add(this.btnTurnAll);
            this.Name = "Form1";
            this.Text = "Управление распознаванием";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTurnAll;
    }
}