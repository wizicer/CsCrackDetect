namespace CrackDetect
{
    partial class EnhancedPictureBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // EnhancedPictureBox
            // 
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EnhancedPictureBox_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EnhancedPictureBox_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EnhancedPictureBox_MouseUp);
            this.OnImageChange += new ImageChanged(EnhancedPictureBox_OnImageChange);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
