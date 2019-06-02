namespace xblocks
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_info = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.label_block_count = new System.Windows.Forms.Label();
            this.label_score = new System.Windows.Forms.Label();
            this.label_level = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label_info.Font = new System.Drawing.Font("微軟正黑體", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_info.ForeColor = System.Drawing.Color.Red;
            this.label_info.Location = new System.Drawing.Point(141, 271);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(0, 61);
            this.label_info.TabIndex = 200;
            this.label_info.Click += new System.EventHandler(this.label_info_Click);
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.BackColor = System.Drawing.SystemColors.MenuText;
            this.label213.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label213.ForeColor = System.Drawing.Color.White;
            this.label213.Location = new System.Drawing.Point(515, 179);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(60, 26);
            this.label213.TabIndex = 214;
            this.label213.Text = "Next";
            // 
            // label_block_count
            // 
            this.label_block_count.AutoSize = true;
            this.label_block_count.BackColor = System.Drawing.SystemColors.MenuText;
            this.label_block_count.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_block_count.ForeColor = System.Drawing.Color.White;
            this.label_block_count.Location = new System.Drawing.Point(503, 230);
            this.label_block_count.Name = "label_block_count";
            this.label_block_count.Size = new System.Drawing.Size(84, 26);
            this.label_block_count.TabIndex = 215;
            this.label_block_count.Text = "Blocks:";
            // 
            // label_score
            // 
            this.label_score.AutoSize = true;
            this.label_score.BackColor = System.Drawing.SystemColors.MenuText;
            this.label_score.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_score.ForeColor = System.Drawing.Color.White;
            this.label_score.Location = new System.Drawing.Point(508, 270);
            this.label_score.Name = "label_score";
            this.label_score.Size = new System.Drawing.Size(74, 26);
            this.label_score.TabIndex = 216;
            this.label_score.Text = "Score:";
            // 
            // label_level
            // 
            this.label_level.AutoSize = true;
            this.label_level.BackColor = System.Drawing.SystemColors.MenuText;
            this.label_level.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_level.ForeColor = System.Drawing.Color.White;
            this.label_level.Location = new System.Drawing.Point(511, 310);
            this.label_level.Name = "label_level";
            this.label_level.Size = new System.Drawing.Size(69, 26);
            this.label_level.TabIndex = 217;
            this.label_level.Text = "Level:";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Navy;
            this.button5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(508, 355);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 45);
            this.button5.TabIndex = 221;
            this.button5.Text = "暫停";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Navy;
            this.button6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(508, 415);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 45);
            this.button6.TabIndex = 222;
            this.button6.Text = "增加Level";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Navy;
            this.button7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(508, 475);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(122, 45);
            this.button7.TabIndex = 223;
            this.button7.Text = "減少Level";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Navy;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(508, 536);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 45);
            this.button1.TabIndex = 225;
            this.button1.Text = "重新";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(697, 652);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label_level);
            this.Controls.Add(this.label_score);
            this.Controls.Add(this.label_block_count);
            this.Controls.Add(this.label213);
            this.Controls.Add(this.label_info);
            this.Name = "Form1";
            this.Text = "俄羅斯方塊";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.Label label213;
        private System.Windows.Forms.Label label_block_count;
        private System.Windows.Forms.Label label_score;
        private System.Windows.Forms.Label label_level;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
    }
}

