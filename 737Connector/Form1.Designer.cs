namespace _737Connector
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
            this.richResponse = new System.Windows.Forms.RichTextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxMcpAlt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEventEnter = new System.Windows.Forms.TextBox();
            this.buttonSendEventTextBox = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnPin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnEvent = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxSerialTab = new System.Windows.Forms.RichTextBox();
            this.textBoxSerialSend = new System.Windows.Forms.TextBox();
            this.btnSerialSend = new System.Windows.Forms.Button();
            this.btnSerialConnect = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // richResponse
            // 
            this.richResponse.Location = new System.Drawing.Point(390, 144);
            this.richResponse.Name = "richResponse";
            this.richResponse.Size = new System.Drawing.Size(260, 96);
            this.richResponse.TabIndex = 2;
            this.richResponse.Text = "";
            this.richResponse.TextChanged += new System.EventHandler(this.richResponse_TextChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(570, 444);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(651, 444);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 4;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(415, 35);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // textBoxMcpAlt
            // 
            this.textBoxMcpAlt.Location = new System.Drawing.Point(85, 152);
            this.textBoxMcpAlt.Name = "textBoxMcpAlt";
            this.textBoxMcpAlt.Size = new System.Drawing.Size(100, 20);
            this.textBoxMcpAlt.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "MCP Altitude:";
            // 
            // textBoxEventEnter
            // 
            this.textBoxEventEnter.Location = new System.Drawing.Point(11, 187);
            this.textBoxEventEnter.Name = "textBoxEventEnter";
            this.textBoxEventEnter.Size = new System.Drawing.Size(207, 20);
            this.textBoxEventEnter.TabIndex = 9;
            this.textBoxEventEnter.Text = "70010";
            // 
            // buttonSendEventTextBox
            // 
            this.buttonSendEventTextBox.Location = new System.Drawing.Point(224, 184);
            this.buttonSendEventTextBox.Name = "buttonSendEventTextBox";
            this.buttonSendEventTextBox.Size = new System.Drawing.Size(75, 23);
            this.buttonSendEventTextBox.TabIndex = 10;
            this.buttonSendEventTextBox.Text = "SendEvent";
            this.buttonSendEventTextBox.UseVisualStyleBackColor = true;
            this.buttonSendEventTextBox.Click += new System.EventHandler(this.buttonSendEventTextBox_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(476, 444);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Empty";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(738, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 438);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Controls.Add(this.textBoxMcpAlt);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.buttonSendEventTextBox);
            this.tabPage1.Controls.Add(this.textBoxEventEnter);
            this.tabPage1.Controls.Add(this.richResponse);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPin,
            this.ColumnMode,
            this.ColumnEvent});
            this.dataGridView1.Location = new System.Drawing.Point(8, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(345, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColumnPin
            // 
            this.ColumnPin.HeaderText = "Pin";
            this.ColumnPin.Name = "ColumnPin";
            this.ColumnPin.ReadOnly = true;
            // 
            // ColumnMode
            // 
            this.ColumnMode.HeaderText = "Mode";
            this.ColumnMode.Name = "ColumnMode";
            this.ColumnMode.ReadOnly = true;
            // 
            // ColumnEvent
            // 
            this.ColumnEvent.HeaderText = "Event";
            this.ColumnEvent.Name = "ColumnEvent";
            this.ColumnEvent.ReadOnly = true;
            this.ColumnEvent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSerialConnect);
            this.tabPage3.Controls.Add(this.btnSerialSend);
            this.tabPage3.Controls.Add(this.textBoxSerialSend);
            this.tabPage3.Controls.Add(this.richTextBoxSerialTab);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(674, 412);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Serial Control";
            // 
            // richTextBoxSerialTab
            // 
            this.richTextBoxSerialTab.Location = new System.Drawing.Point(421, 13);
            this.richTextBoxSerialTab.Name = "richTextBoxSerialTab";
            this.richTextBoxSerialTab.Size = new System.Drawing.Size(247, 96);
            this.richTextBoxSerialTab.TabIndex = 1;
            this.richTextBoxSerialTab.Text = "";
            // 
            // textBoxSerialSend
            // 
            this.textBoxSerialSend.Location = new System.Drawing.Point(8, 89);
            this.textBoxSerialSend.Name = "textBoxSerialSend";
            this.textBoxSerialSend.Size = new System.Drawing.Size(240, 20);
            this.textBoxSerialSend.TabIndex = 2;
            // 
            // btnSerialSend
            // 
            this.btnSerialSend.Location = new System.Drawing.Point(11, 115);
            this.btnSerialSend.Name = "btnSerialSend";
            this.btnSerialSend.Size = new System.Drawing.Size(75, 23);
            this.btnSerialSend.TabIndex = 3;
            this.btnSerialSend.Text = "Send";
            this.btnSerialSend.UseVisualStyleBackColor = true;
            this.btnSerialSend.Click += new System.EventHandler(this.btnSerialSend_Click);
            // 
            // btnSerialConnect
            // 
            this.btnSerialConnect.Location = new System.Drawing.Point(8, 345);
            this.btnSerialConnect.Name = "btnSerialConnect";
            this.btnSerialConnect.Size = new System.Drawing.Size(75, 23);
            this.btnSerialConnect.TabIndex = 4;
            this.btnSerialConnect.Text = "Connect";
            this.btnSerialConnect.UseVisualStyleBackColor = true;
            this.btnSerialConnect.Click += new System.EventHandler(this.btnSerialConnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 479);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richResponse;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxMcpAlt;
        private System.Windows.Forms.TextBox textBoxEventEnter;
        private System.Windows.Forms.Button buttonSendEventTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPin;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnMode;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnEvent;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSerialSend;
        private System.Windows.Forms.TextBox textBoxSerialSend;
        private System.Windows.Forms.RichTextBox richTextBoxSerialTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSerialConnect;
    }
}

