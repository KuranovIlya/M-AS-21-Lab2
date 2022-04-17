
namespace CannyEdgeDetection
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbInputImage = new System.Windows.Forms.PictureBox();
            this.pbCannyImage = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCanny = new System.Windows.Forms.Button();
            this.processState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbInputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCannyImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbInputImage
            // 
            this.pbInputImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbInputImage.Location = new System.Drawing.Point(13, 13);
            this.pbInputImage.Name = "pbInputImage";
            this.pbInputImage.Size = new System.Drawing.Size(484, 548);
            this.pbInputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInputImage.TabIndex = 0;
            this.pbInputImage.TabStop = false;
            // 
            // pbCannyImage
            // 
            this.pbCannyImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbCannyImage.Location = new System.Drawing.Point(630, 13);
            this.pbCannyImage.Name = "pbCannyImage";
            this.pbCannyImage.Size = new System.Drawing.Size(484, 548);
            this.pbCannyImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCannyImage.TabIndex = 1;
            this.pbCannyImage.TabStop = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(503, 100);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(121, 44);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(503, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCanny
            // 
            this.btnCanny.Location = new System.Drawing.Point(503, 224);
            this.btnCanny.Name = "btnCanny";
            this.btnCanny.Size = new System.Drawing.Size(121, 44);
            this.btnCanny.TabIndex = 4;
            this.btnCanny.Text = "Canny";
            this.btnCanny.UseVisualStyleBackColor = true;
            this.btnCanny.Click += new System.EventHandler(this.btnCanny_Click);
            // 
            // processState
            // 
            this.processState.AutoSize = true;
            this.processState.Location = new System.Drawing.Point(503, 282);
            this.processState.Name = "processState";
            this.processState.Size = new System.Drawing.Size(0, 15);
            this.processState.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 573);
            this.Controls.Add(this.processState);
            this.Controls.Add(this.btnCanny);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.pbCannyImage);
            this.Controls.Add(this.pbInputImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Алгоритм выделения границ и контуров Canny";
            ((System.ComponentModel.ISupportInitialize)(this.pbInputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCannyImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbInputImage;
        private System.Windows.Forms.PictureBox pbCannyImage;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCanny;
        private System.Windows.Forms.Label processState;
    }
}

