namespace TetrisForm
{
    partial class OpenGameForm
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
            this.SavePointsDGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.SavePointsDGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SavePointsDGridView
            // 
            this.SavePointsDGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SavePointsDGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.SavePointsDGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SavePointsDGridView.Location = new System.Drawing.Point(12, 12);
            this.SavePointsDGridView.Name = "SavePointsDGridView";
            this.SavePointsDGridView.ReadOnly = true;
            this.SavePointsDGridView.Size = new System.Drawing.Size(426, 436);
            this.SavePointsDGridView.TabIndex = 0;
            this.SavePointsDGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SavePointsDGridView_CellDoubleClick);
            // 
            // SavePointsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 459);
            this.Controls.Add(this.SavePointsDGridView);
            this.Name = "SavePointsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Points";
            ((System.ComponentModel.ISupportInitialize)(this.SavePointsDGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView SavePointsDGridView;
    }
}