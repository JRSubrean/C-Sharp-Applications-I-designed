namespace Pilot
{
    partial class Pilot
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHitPoints = new System.Windows.Forms.Label();
            this.lblGold = new System.Windows.Forms.Label();
            this.lblExperience = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboWeapons = new System.Windows.Forms.ComboBox();
            this.ComboPotions = new System.Windows.Forms.ComboBox();
            this.ButtonUseWeapon = new System.Windows.Forms.Button();
            this.ButtonUsePotion = new System.Windows.Forms.Button();
            this.ButtonNorth = new System.Windows.Forms.Button();
            this.ButtonEast = new System.Windows.Forms.Button();
            this.ButtonSouth = new System.Windows.Forms.Button();
            this.ButtonWest = new System.Windows.Forms.Button();
            this.rtbLocation = new System.Windows.Forms.RichTextBox();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.dgvQuests = new System.Windows.Forms.DataGridView();
            this.ButtonTrade = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hit Points:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Gold:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Experience:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Level:";
            // 
            // lblHitPoints
            // 
            this.lblHitPoints.AutoSize = true;
            this.lblHitPoints.Location = new System.Drawing.Point(110, 20);
            this.lblHitPoints.Name = "lblHitPoints";
            this.lblHitPoints.Size = new System.Drawing.Size(0, 13);
            this.lblHitPoints.TabIndex = 4;
            // 
            // lblGold
            // 
            this.lblGold.AutoSize = true;
            this.lblGold.Location = new System.Drawing.Point(110, 45);
            this.lblGold.Name = "lblGold";
            this.lblGold.Size = new System.Drawing.Size(0, 13);
            this.lblGold.TabIndex = 5;
            // 
            // lblExperience
            // 
            this.lblExperience.AutoSize = true;
            this.lblExperience.Location = new System.Drawing.Point(110, 73);
            this.lblExperience.Name = "lblExperience";
            this.lblExperience.Size = new System.Drawing.Size(0, 13);
            this.lblExperience.TabIndex = 6;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(110, 99);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(0, 13);
            this.lblLevel.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(617, 531);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Select action";
            // 
            // ComboWeapons
            // 
            this.ComboWeapons.FormattingEnabled = true;
            this.ComboWeapons.Location = new System.Drawing.Point(369, 559);
            this.ComboWeapons.Name = "ComboWeapons";
            this.ComboWeapons.Size = new System.Drawing.Size(121, 21);
            this.ComboWeapons.TabIndex = 9;
            // 
            // ComboPotions
            // 
            this.ComboPotions.FormattingEnabled = true;
            this.ComboPotions.Location = new System.Drawing.Point(369, 593);
            this.ComboPotions.Name = "ComboPotions";
            this.ComboPotions.Size = new System.Drawing.Size(121, 21);
            this.ComboPotions.TabIndex = 10;
            // 
            // ButtonUseWeapon
            // 
            this.ButtonUseWeapon.Location = new System.Drawing.Point(611, 559);
            this.ButtonUseWeapon.Name = "ButtonUseWeapon";
            this.ButtonUseWeapon.Size = new System.Drawing.Size(84, 23);
            this.ButtonUseWeapon.TabIndex = 11;
            this.ButtonUseWeapon.Text = "Use Weapon";
            this.ButtonUseWeapon.UseVisualStyleBackColor = true;
            this.ButtonUseWeapon.Click += new System.EventHandler(this.ButtonUseWeapon_Click);
            // 
            // ButtonUsePotion
            // 
            this.ButtonUsePotion.Location = new System.Drawing.Point(611, 593);
            this.ButtonUsePotion.Name = "ButtonUsePotion";
            this.ButtonUsePotion.Size = new System.Drawing.Size(84, 23);
            this.ButtonUsePotion.TabIndex = 12;
            this.ButtonUsePotion.Text = "Use Potion";
            this.ButtonUsePotion.UseVisualStyleBackColor = true;
            this.ButtonUsePotion.Click += new System.EventHandler(this.ButtonUsePotion_Click);
            // 
            // ButtonNorth
            // 
            this.ButtonNorth.Location = new System.Drawing.Point(493, 433);
            this.ButtonNorth.Name = "ButtonNorth";
            this.ButtonNorth.Size = new System.Drawing.Size(75, 23);
            this.ButtonNorth.TabIndex = 13;
            this.ButtonNorth.Text = "North";
            this.ButtonNorth.UseVisualStyleBackColor = true;
            this.ButtonNorth.Click += new System.EventHandler(this.ButtonNorth_Click);
            // 
            // ButtonEast
            // 
            this.ButtonEast.Location = new System.Drawing.Point(573, 457);
            this.ButtonEast.Name = "ButtonEast";
            this.ButtonEast.Size = new System.Drawing.Size(75, 23);
            this.ButtonEast.TabIndex = 14;
            this.ButtonEast.Text = "East";
            this.ButtonEast.UseVisualStyleBackColor = true;
            this.ButtonEast.Click += new System.EventHandler(this.ButtonEast_Click);
            // 
            // ButtonSouth
            // 
            this.ButtonSouth.Location = new System.Drawing.Point(493, 487);
            this.ButtonSouth.Name = "ButtonSouth";
            this.ButtonSouth.Size = new System.Drawing.Size(75, 23);
            this.ButtonSouth.TabIndex = 15;
            this.ButtonSouth.Text = "South";
            this.ButtonSouth.UseVisualStyleBackColor = true;
            this.ButtonSouth.Click += new System.EventHandler(this.ButtonSouth_Click);
            // 
            // ButtonWest
            // 
            this.ButtonWest.Location = new System.Drawing.Point(412, 457);
            this.ButtonWest.Name = "ButtonWest";
            this.ButtonWest.Size = new System.Drawing.Size(75, 23);
            this.ButtonWest.TabIndex = 16;
            this.ButtonWest.Text = "West";
            this.ButtonWest.UseVisualStyleBackColor = true;
            this.ButtonWest.Click += new System.EventHandler(this.ButtonWest_Click);
            // 
            // rtbLocation
            // 
            this.rtbLocation.Location = new System.Drawing.Point(347, 19);
            this.rtbLocation.Name = "rtbLocation";
            this.rtbLocation.ReadOnly = true;
            this.rtbLocation.Size = new System.Drawing.Size(360, 105);
            this.rtbLocation.TabIndex = 17;
            this.rtbLocation.Text = "";
            // 
            // rtbMessages
            // 
            this.rtbMessages.Location = new System.Drawing.Point(347, 130);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.ReadOnly = true;
            this.rtbMessages.Size = new System.Drawing.Size(360, 286);
            this.rtbMessages.TabIndex = 18;
            this.rtbMessages.Text = "";
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AllowUserToResizeRows = false;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInventory.Enabled = false;
            this.dgvInventory.Location = new System.Drawing.Point(16, 130);
            this.dgvInventory.MultiSelect = false;
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.dgvInventory.RowHeadersVisible = false;
            this.dgvInventory.Size = new System.Drawing.Size(312, 309);
            this.dgvInventory.TabIndex = 19;
            this.dgvInventory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventory_CellContentClick);
            // 
            // dgvQuests
            // 
            this.dgvQuests.AllowUserToAddRows = false;
            this.dgvQuests.AllowUserToDeleteRows = false;
            this.dgvQuests.AllowUserToResizeRows = false;
            this.dgvQuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuests.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvQuests.Enabled = false;
            this.dgvQuests.Location = new System.Drawing.Point(16, 446);
            this.dgvQuests.MultiSelect = false;
            this.dgvQuests.Name = "dgvQuests";
            this.dgvQuests.ReadOnly = true;
            this.dgvQuests.RowHeadersVisible = false;
            this.dgvQuests.Size = new System.Drawing.Size(312, 189);
            this.dgvQuests.TabIndex = 20;
            /*ButtonTrade*/
            this.ButtonTrade.Location = new System.Drawing.Point(493, 620);
            this.ButtonTrade.Name = "ButtonTrade";
            this.ButtonTrade.Size = new System.Drawing.Size(75, 23);
            this.ButtonTrade.TabIndex = 21;
            this.ButtonTrade.Text = "Trade";
            this.ButtonTrade.UseVisualStyleBackColor = true;
            this.ButtonTrade.Click += new System.EventHandler(this.ButtonTrade_Click);
            // 
            // Pilot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 652);
            this.Controls.Add(this.ButtonTrade);
            this.Controls.Add(this.dgvQuests);
            this.Controls.Add(this.dgvInventory);
            this.Controls.Add(this.rtbMessages);
            this.Controls.Add(this.rtbLocation);
            this.Controls.Add(this.ButtonWest);
            this.Controls.Add(this.ButtonSouth);
            this.Controls.Add(this.ButtonEast);
            this.Controls.Add(this.ButtonNorth);
            this.Controls.Add(this.ButtonUsePotion);
            this.Controls.Add(this.ButtonUseWeapon);
            this.Controls.Add(this.ComboPotions);
            this.Controls.Add(this.ComboWeapons);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblExperience);
            this.Controls.Add(this.lblGold);
            this.Controls.Add(this.lblHitPoints);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Pilot";
            this.Text = "Pilot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Pilot_FormClosing);
            this.Load += new System.EventHandler(this.Pilot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHitPoints;
        private System.Windows.Forms.Label lblGold;
        private System.Windows.Forms.Label lblExperience;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboWeapons;
        private System.Windows.Forms.ComboBox ComboPotions;
        private System.Windows.Forms.Button ButtonUseWeapon;
        private System.Windows.Forms.Button ButtonUsePotion;
        private System.Windows.Forms.Button ButtonNorth;
        private System.Windows.Forms.Button ButtonEast;
        private System.Windows.Forms.Button ButtonSouth;
        private System.Windows.Forms.Button ButtonWest;
        private System.Windows.Forms.Button ButtonTrade;
        private System.Windows.Forms.RichTextBox rtbLocation;
        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.DataGridView dgvQuests;
    }
}

