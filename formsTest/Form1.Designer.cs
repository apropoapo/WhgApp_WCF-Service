namespace formsTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSchreiben = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.usersTestBtn = new System.Windows.Forms.Button();
            this.testLbl = new System.Windows.Forms.Label();
            this.lstBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDoIt
            // 
            this.btnDoIt.Location = new System.Drawing.Point(12, 602);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size(258, 26);
            this.btnDoIt.TabIndex = 0;
            this.btnDoIt.Text = "btnAbrufen";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(13, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(595, 584);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSchreiben
            // 
            this.btnSchreiben.Location = new System.Drawing.Point(522, 602);
            this.btnSchreiben.Name = "btnSchreiben";
            this.btnSchreiben.Size = new System.Drawing.Size(228, 23);
            this.btnSchreiben.TabIndex = 3;
            this.btnSchreiben.Text = "btnSchreiben";
            this.btnSchreiben.UseVisualStyleBackColor = true;
            this.btnSchreiben.Click += new System.EventHandler(this.btnSchreiben_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(276, 602);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(131, 26);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // usersTestBtn
            // 
            this.usersTestBtn.Location = new System.Drawing.Point(413, 602);
            this.usersTestBtn.Name = "usersTestBtn";
            this.usersTestBtn.Size = new System.Drawing.Size(103, 23);
            this.usersTestBtn.TabIndex = 5;
            this.usersTestBtn.Text = "usersTestBtn";
            this.usersTestBtn.UseVisualStyleBackColor = true;
            this.usersTestBtn.Click += new System.EventHandler(this.usersTestBtn_Click);
            // 
            // testLbl
            // 
            this.testLbl.AutoSize = true;
            this.testLbl.Location = new System.Drawing.Point(756, 609);
            this.testLbl.Name = "testLbl";
            this.testLbl.Size = new System.Drawing.Size(35, 13);
            this.testLbl.TabIndex = 7;
            this.testLbl.Text = "label1";
            // 
            // lstBox1
            // 
            this.lstBox1.FormattingEnabled = true;
            this.lstBox1.Location = new System.Drawing.Point(614, 13);
            this.lstBox1.Name = "lstBox1";
            this.lstBox1.Size = new System.Drawing.Size(565, 589);
            this.lstBox1.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Interval = 600;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 640);
            this.Controls.Add(this.lstBox1);
            this.Controls.Add(this.testLbl);
            this.Controls.Add(this.usersTestBtn);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSchreiben);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnDoIt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSchreiben;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button usersTestBtn;
        private System.Windows.Forms.Label testLbl;
        private System.Windows.Forms.ListBox lstBox1;
        private System.Windows.Forms.Timer timer1;
    }
}

