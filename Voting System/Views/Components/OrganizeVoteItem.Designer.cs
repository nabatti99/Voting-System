
namespace Voting_System.Views.Components
{
    partial class OrganizeVoteItem
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
            this.pnlOrganizeVoteItem = new System.Windows.Forms.Panel();
            this.lbNumVoteTurns = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlOrganizeVoteItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOrganizeVoteItem
            // 
            this.pnlOrganizeVoteItem.Controls.Add(this.lbNumVoteTurns);
            this.pnlOrganizeVoteItem.Controls.Add(this.lbDate);
            this.pnlOrganizeVoteItem.Controls.Add(this.lbTitle);
            this.pnlOrganizeVoteItem.Controls.Add(this.label2);
            this.pnlOrganizeVoteItem.Controls.Add(this.label1);
            this.pnlOrganizeVoteItem.Location = new System.Drawing.Point(0, 0);
            this.pnlOrganizeVoteItem.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOrganizeVoteItem.Name = "pnlOrganizeVoteItem";
            this.pnlOrganizeVoteItem.Size = new System.Drawing.Size(195, 138);
            this.pnlOrganizeVoteItem.TabIndex = 3;
            this.pnlOrganizeVoteItem.Click += new System.EventHandler(this.pnlOrganizeVoteItem_Click);
            this.pnlOrganizeVoteItem.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.pnlOrganizeVoteItem.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // lbNumVoteTurns
            // 
            this.lbNumVoteTurns.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumVoteTurns.Location = new System.Drawing.Point(96, 81);
            this.lbNumVoteTurns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumVoteTurns.Name = "lbNumVoteTurns";
            this.lbNumVoteTurns.Size = new System.Drawing.Size(87, 16);
            this.lbNumVoteTurns.TabIndex = 1;
            this.lbNumVoteTurns.Text = "15/20";
            this.lbNumVoteTurns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDate
            // 
            this.lbDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.Location = new System.Drawing.Point(96, 108);
            this.lbDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(87, 16);
            this.lbDate.TabIndex = 1;
            this.lbDate.Text = "20/10/2021";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.Location = new System.Drawing.Point(12, 12);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(171, 47);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Bầu ban cán sự";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tổng số:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hết hạn:";
            // 
            // OrganizeVoteItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlOrganizeVoteItem);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(194, 137);
            this.MinimumSize = new System.Drawing.Size(194, 137);
            this.Name = "OrganizeVoteItem";
            this.Padding = new System.Windows.Forms.Padding(11, 20, 11, 20);
            this.Size = new System.Drawing.Size(192, 135);
            this.pnlOrganizeVoteItem.ResumeLayout(false);
            this.pnlOrganizeVoteItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOrganizeVoteItem;
        private System.Windows.Forms.Label lbNumVoteTurns;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
