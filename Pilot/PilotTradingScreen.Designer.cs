namespace Pilot
{
    partial class PilotTradingScreen
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
            this.LabelMyInventory = new System.Windows.Forms.Label();
            this.LabelVendorInventory = new System.Windows.Forms.Label();
            this.dgvMyItems = new System.Windows.Forms.DataGridView();
            this.dgvVendorItems = new System.Windows.Forms.DataGridView();
            this.ButtonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorItems)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelMyInventory
            // 
            this.LabelMyInventory.AutoSize = true;
            this.LabelMyInventory.Location = new System.Drawing.Point(99, 13);
            this.LabelMyInventory.Name = "LabelMyInventory";
            this.LabelMyInventory.Size = new System.Drawing.Size(68, 13);
            this.LabelMyInventory.TabIndex = 0;
            this.LabelMyInventory.Text = "My Inventory";
            // 
            // LabelVendorInventory
            // 
            this.LabelVendorInventory.AutoSize = true;
            this.LabelVendorInventory.Location = new System.Drawing.Point(349, 13);
            this.LabelVendorInventory.Name = "LabelVendorInventory";
            this.LabelVendorInventory.Size = new System.Drawing.Size(95, 13);
            this.LabelVendorInventory.TabIndex = 1;
            this.LabelVendorInventory.Text = "Vendor\'s Inventory";
            // 
            // dgvMyItems
            // 
            this.dgvMyItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyItems.Location = new System.Drawing.Point(13, 43);
            this.dgvMyItems.Name = "dgvMyItems";
            this.dgvMyItems.Size = new System.Drawing.Size(240, 216);
            this.dgvMyItems.TabIndex = 2;
            // 
            // dgvVendorItems
            // 
            this.dgvVendorItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVendorItems.Location = new System.Drawing.Point(276, 43);
            this.dgvVendorItems.Name = "dgvVendorItems";
            this.dgvVendorItems.Size = new System.Drawing.Size(240, 216);
            this.dgvVendorItems.TabIndex = 3;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(441, 274);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 4;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // PilotTradingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 311);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.dgvVendorItems);
            this.Controls.Add(this.dgvMyItems);
            this.Controls.Add(this.LabelVendorInventory);
            this.Controls.Add(this.LabelMyInventory);
            this.Name = "PilotTradingScreen";
            this.Text = "Trade";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelMyInventory;
        private System.Windows.Forms.Label LabelVendorInventory;
        private System.Windows.Forms.DataGridView dgvMyItems;
        private System.Windows.Forms.DataGridView dgvVendorItems;
        private System.Windows.Forms.Button ButtonClose;
    }
}