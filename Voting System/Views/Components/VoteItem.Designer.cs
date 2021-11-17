
namespace Voting_System.Views
{
    partial class VoteItem
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.pnlVoteItem = new System.Windows.Forms.Panel();
            this.pnlVoteItem.SuspendLayout();
            this.SuspendLayout();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đã vote:";
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
            // lbState
            // 
            this.lbState.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbState.Location = new System.Drawing.Point(96, 81);
            this.lbState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(87, 16);
            this.lbState.TabIndex = 1;
            this.lbState.Text = "Đã vote";
            this.lbState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlVoteItem
            // 
            this.pnlVoteItem.Controls.Add(this.lbState);
            this.pnlVoteItem.Controls.Add(this.lbDate);
            this.pnlVoteItem.Controls.Add(this.lbTitle);
            this.pnlVoteItem.Controls.Add(this.label2);
            this.pnlVoteItem.Controls.Add(this.label1);
            this.pnlVoteItem.Location = new System.Drawing.Point(-1, -1);
            this.pnlVoteItem.Margin = new System.Windows.Forms.Padding(4);
            this.pnlVoteItem.Name = "pnlVoteItem";
            this.pnlVoteItem.Size = new System.Drawing.Size(195, 138);
            this.pnlVoteItem.TabIndex = 2;
            this.pnlVoteItem.Click += new System.EventHandler(this.pnlVoteItem_Click);
            this.pnlVoteItem.MouseEnter += new System.EventHandler(this.VoteItem_MouseEnter);
            this.pnlVoteItem.MouseLeave += new System.EventHandler(this.VoteItem_MouseLeave);
            // 
            // VoteItem
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pnlVoteItem);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(194, 137);
            this.MinimumSize = new System.Drawing.Size(194, 137);
            this.Name = "VoteItem";
            this.Padding = new System.Windows.Forms.Padding(11, 20, 11, 20);
            this.Size = new System.Drawing.Size(192, 135);
            this.pnlVoteItem.ResumeLayout(false);
            this.pnlVoteItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDate;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Panel pnlVoteItem;
    }
}
