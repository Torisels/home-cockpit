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
            this.buttonSendEvent = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxMcpAlt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEventEnter = new System.Windows.Forms.TextBox();
            this.buttonSendEventTextBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richResponse
            // 
            this.richResponse.Location = new System.Drawing.Point(12, 12);
            this.richResponse.Name = "richResponse";
            this.richResponse.Size = new System.Drawing.Size(260, 96);
            this.richResponse.TabIndex = 2;
            this.richResponse.Text = "";
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
            // buttonSendEvent
            // 
            this.buttonSendEvent.Location = new System.Drawing.Point(12, 444);
            this.buttonSendEvent.Name = "buttonSendEvent";
            this.buttonSendEvent.Size = new System.Drawing.Size(75, 23);
            this.buttonSendEvent.TabIndex = 5;
            this.buttonSendEvent.Text = "Send Event";
            this.buttonSendEvent.UseVisualStyleBackColor = true;
            this.buttonSendEvent.Click += new System.EventHandler(this.buttonSendEvent_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 127);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(415, 29);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // textBoxMcpAlt
            // 
            this.textBoxMcpAlt.Location = new System.Drawing.Point(89, 162);
            this.textBoxMcpAlt.Name = "textBoxMcpAlt";
            this.textBoxMcpAlt.Size = new System.Drawing.Size(100, 20);
            this.textBoxMcpAlt.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "MCP Altitude:";
            // 
            // textBoxEventEnter
            // 
            this.textBoxEventEnter.Location = new System.Drawing.Point(15, 203);
            this.textBoxEventEnter.Name = "textBoxEventEnter";
            this.textBoxEventEnter.Size = new System.Drawing.Size(207, 20);
            this.textBoxEventEnter.TabIndex = 9;
            this.textBoxEventEnter.Text = "70010 1";
            // 
            // buttonSendEventTextBox
            // 
            this.buttonSendEventTextBox.Location = new System.Drawing.Point(243, 201);
            this.buttonSendEventTextBox.Name = "buttonSendEventTextBox";
            this.buttonSendEventTextBox.Size = new System.Drawing.Size(75, 23);
            this.buttonSendEventTextBox.TabIndex = 10;
            this.buttonSendEventTextBox.Text = "button1";
            this.buttonSendEventTextBox.UseVisualStyleBackColor = true;
            this.buttonSendEventTextBox.Click += new System.EventHandler(this.buttonSendEventTextBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 479);
            this.Controls.Add(this.buttonSendEventTextBox);
            this.Controls.Add(this.textBoxEventEnter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMcpAlt);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonSendEvent);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.richResponse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richResponse;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonSendEvent;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxMcpAlt;
        private System.Windows.Forms.TextBox textBoxEventEnter;
        private System.Windows.Forms.Button buttonSendEventTextBox;
    }
}

