
namespace BrakingBad
{
    partial class surface
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
            this.SuspendLayout();
            // 
            // surface
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "surface";
            this.Load += new System.EventHandler(this.surface_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gamePlate;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button gameBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.TextBox DEBUG_TXT;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button button1;
    }
}

