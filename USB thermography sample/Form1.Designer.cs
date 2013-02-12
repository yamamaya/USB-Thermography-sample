namespace USB_thermography_sample {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose( bool disposing ) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAverage = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPTAT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxVcp = new System.Windows.Forms.TextBox();
            this.buttonReadRawData = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.buttonReadEEPROM = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(642, 103);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(536, 122);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(119, 23);
            this.buttonRead.TabIndex = 1;
            this.buttonRead.Text = "Read temperature";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "average:";
            // 
            // textBoxAverage
            // 
            this.textBoxAverage.Location = new System.Drawing.Point(67, 122);
            this.textBoxAverage.Name = "textBoxAverage";
            this.textBoxAverage.Size = new System.Drawing.Size(66, 19);
            this.textBoxAverage.TabIndex = 3;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Location = new System.Drawing.Point(16, 151);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.Size = new System.Drawing.Size(639, 103);
            this.dataGridView2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "PTAT:";
            // 
            // textBoxPTAT
            // 
            this.textBoxPTAT.Location = new System.Drawing.Point(56, 260);
            this.textBoxPTAT.Name = "textBoxPTAT";
            this.textBoxPTAT.Size = new System.Drawing.Size(66, 19);
            this.textBoxPTAT.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Vcp:";
            // 
            // textBoxVcp
            // 
            this.textBoxVcp.Location = new System.Drawing.Point(173, 260);
            this.textBoxVcp.Name = "textBoxVcp";
            this.textBoxVcp.Size = new System.Drawing.Size(66, 19);
            this.textBoxVcp.TabIndex = 8;
            // 
            // buttonReadRawData
            // 
            this.buttonReadRawData.Location = new System.Drawing.Point(536, 260);
            this.buttonReadRawData.Name = "buttonReadRawData";
            this.buttonReadRawData.Size = new System.Drawing.Size(119, 23);
            this.buttonReadRawData.TabIndex = 9;
            this.buttonReadRawData.Text = "Read raw data";
            this.buttonReadRawData.UseVisualStyleBackColor = true;
            this.buttonReadRawData.Click += new System.EventHandler(this.buttonReadRawData_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.ColumnHeadersVisible = false;
            this.dataGridView3.Location = new System.Drawing.Point(16, 289);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 21;
            this.dataGridView3.Size = new System.Drawing.Size(639, 221);
            this.dataGridView3.TabIndex = 10;
            // 
            // buttonReadEEPROM
            // 
            this.buttonReadEEPROM.Location = new System.Drawing.Point(536, 516);
            this.buttonReadEEPROM.Name = "buttonReadEEPROM";
            this.buttonReadEEPROM.Size = new System.Drawing.Size(119, 23);
            this.buttonReadEEPROM.TabIndex = 11;
            this.buttonReadEEPROM.Text = "Read EEPROM";
            this.buttonReadEEPROM.UseVisualStyleBackColor = true;
            this.buttonReadEEPROM.Click += new System.EventHandler(this.buttonReadEEPROM_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 551);
            this.Controls.Add(this.buttonReadEEPROM);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.buttonReadRawData);
            this.Controls.Add(this.textBoxVcp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPTAT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.textBoxAverage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "USB Thermography sample";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAverage;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPTAT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVcp;
        private System.Windows.Forms.Button buttonReadRawData;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button buttonReadEEPROM;
    }
}

