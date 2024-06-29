namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.wslog = new System.Windows.Forms.ListBox();
            this.SendQQ = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.SendtextBox = new System.Windows.Forms.TextBox();
            this.blockingWordsBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MontextBox = new System.Windows.Forms.TextBox();
            this.MonGroupGrid = new System.Windows.Forms.DataGridView();
            this.seq = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nums = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GetGroupBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TransTab = new System.Windows.Forms.TabPage();
            this.transBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.transRTB = new System.Windows.Forms.RichTextBox();
            this.orginRTB = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.SendQQ.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonGroupGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.TransTab.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wslog
            // 
            this.wslog.ForeColor = System.Drawing.Color.Indigo;
            this.wslog.FormattingEnabled = true;
            this.wslog.ItemHeight = 12;
            this.wslog.Location = new System.Drawing.Point(0, 0);
            this.wslog.Name = "wslog";
            this.wslog.Size = new System.Drawing.Size(794, 424);
            this.wslog.TabIndex = 1;
            this.wslog.SelectedIndexChanged += new System.EventHandler(this.wslog_SelectedIndexChanged);
            // 
            // SendQQ
            // 
            this.SendQQ.Controls.Add(this.tabPage1);
            this.SendQQ.Controls.Add(this.tabPage2);
            this.SendQQ.Controls.Add(this.TransTab);
            this.SendQQ.Controls.Add(this.tabPage3);
            this.SendQQ.Location = new System.Drawing.Point(0, -1);
            this.SendQQ.Name = "SendQQ";
            this.SendQQ.SelectedIndex = 0;
            this.SendQQ.Size = new System.Drawing.Size(802, 452);
            this.SendQQ.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.SendtextBox);
            this.tabPage1.Controls.Add(this.blockingWordsBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.MontextBox);
            this.tabPage1.Controls.Add(this.MonGroupGrid);
            this.tabPage1.Controls.Add(this.GetGroupBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(794, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "监控QQ群";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "发生QQ群";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // SendtextBox
            // 
            this.SendtextBox.Location = new System.Drawing.Point(218, 33);
            this.SendtextBox.Multiline = true;
            this.SendtextBox.Name = "SendtextBox";
            this.SendtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SendtextBox.Size = new System.Drawing.Size(188, 171);
            this.SendtextBox.TabIndex = 8;
            this.SendtextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // blockingWordsBox
            // 
            this.blockingWordsBox.Location = new System.Drawing.Point(9, 225);
            this.blockingWordsBox.Multiline = true;
            this.blockingWordsBox.Name = "blockingWordsBox";
            this.blockingWordsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.blockingWordsBox.Size = new System.Drawing.Size(397, 196);
            this.blockingWordsBox.TabIndex = 7;
            this.blockingWordsBox.Leave += new System.EventHandler(this.blockingWordsBox_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 210);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "屏蔽关键词(#号分割)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(709, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "监控QQ";
            // 
            // MontextBox
            // 
            this.MontextBox.Location = new System.Drawing.Point(9, 33);
            this.MontextBox.Multiline = true;
            this.MontextBox.Name = "MontextBox";
            this.MontextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MontextBox.Size = new System.Drawing.Size(188, 171);
            this.MontextBox.TabIndex = 3;
            // 
            // MonGroupGrid
            // 
            this.MonGroupGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MonGroupGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.id,
            this.name,
            this.nums});
            this.MonGroupGrid.Location = new System.Drawing.Point(427, 33);
            this.MonGroupGrid.Name = "MonGroupGrid";
            this.MonGroupGrid.RowHeadersWidth = 51;
            this.MonGroupGrid.RowTemplate.Height = 23;
            this.MonGroupGrid.Size = new System.Drawing.Size(361, 387);
            this.MonGroupGrid.TabIndex = 2;
            this.MonGroupGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MonGroupGrid_CellContentClick);
            // 
            // seq
            // 
            this.seq.HeaderText = "#";
            this.seq.MinimumWidth = 6;
            this.seq.Name = "seq";
            this.seq.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.seq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.seq.Width = 20;
            // 
            // id
            // 
            this.id.HeaderText = "群号";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Width = 125;
            // 
            // name
            // 
            this.name.HeaderText = "群昵称";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.Width = 125;
            // 
            // nums
            // 
            this.nums.HeaderText = "群人数";
            this.nums.MinimumWidth = 6;
            this.nums.Name = "nums";
            this.nums.Width = 125;
            // 
            // GetGroupBtn
            // 
            this.GetGroupBtn.Location = new System.Drawing.Point(427, 6);
            this.GetGroupBtn.Name = "GetGroupBtn";
            this.GetGroupBtn.Size = new System.Drawing.Size(75, 23);
            this.GetGroupBtn.TabIndex = 1;
            this.GetGroupBtn.Text = "获取群列表";
            this.GetGroupBtn.UseVisualStyleBackColor = true;
            this.GetGroupBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.wslog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(794, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TransTab
            // 
            this.TransTab.Controls.Add(this.transBtn);
            this.TransTab.Controls.Add(this.label4);
            this.TransTab.Controls.Add(this.label3);
            this.TransTab.Controls.Add(this.transRTB);
            this.TransTab.Controls.Add(this.orginRTB);
            this.TransTab.Location = new System.Drawing.Point(4, 22);
            this.TransTab.Name = "TransTab";
            this.TransTab.Padding = new System.Windows.Forms.Padding(3);
            this.TransTab.Size = new System.Drawing.Size(794, 426);
            this.TransTab.TabIndex = 2;
            this.TransTab.Text = "转链";
            this.TransTab.UseVisualStyleBackColor = true;
            // 
            // transBtn
            // 
            this.transBtn.Location = new System.Drawing.Point(541, 28);
            this.transBtn.Name = "transBtn";
            this.transBtn.Size = new System.Drawing.Size(75, 23);
            this.transBtn.TabIndex = 5;
            this.transBtn.Text = "开始转换";
            this.transBtn.UseVisualStyleBackColor = true;
            this.transBtn.Click += new System.EventHandler(this.transBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "转链接后";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "原始内容";
            // 
            // transRTB
            // 
            this.transRTB.Location = new System.Drawing.Point(279, 31);
            this.transRTB.Name = "transRTB";
            this.transRTB.Size = new System.Drawing.Size(256, 237);
            this.transRTB.TabIndex = 1;
            this.transRTB.Text = "";
            // 
            // orginRTB
            // 
            this.orginRTB.Location = new System.Drawing.Point(8, 31);
            this.orginRTB.Name = "orginRTB";
            this.orginRTB.Size = new System.Drawing.Size(256, 237);
            this.orginRTB.TabIndex = 0;
            this.orginRTB.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(794, 426);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "QQ发送设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(258, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 36);
            this.button3.TabIndex = 0;
            this.button3.Text = "一键排列至左上角";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SendQQ);
            this.Name = "MainForm";
            this.Text = "小驴车";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SendQQ.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonGroupGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.TransTab.ResumeLayout(false);
            this.TransTab.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox wslog;
        private System.Windows.Forms.TabControl SendQQ;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button GetGroupBtn;
        private System.Windows.Forms.DataGridView MonGroupGrid;
        private System.Windows.Forms.TabPage TransTab;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn nums;
        private System.Windows.Forms.TextBox MontextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox transRTB;
        private System.Windows.Forms.RichTextBox orginRTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button transBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox blockingWordsBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SendtextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button3;
    }
}

