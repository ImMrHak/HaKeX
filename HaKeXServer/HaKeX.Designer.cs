namespace HaKeXServer
{
    partial class HaKeX
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            txtServerPort = new TextBox();
            txtServerHost = new TextBox();
            btnStopTcpServer = new Button();
            btnStartTcpServer = new Button();
            groupBox2 = new GroupBox();
            pictureArea = new PictureBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureArea).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(txtServerPort);
            groupBox1.Controls.Add(txtServerHost);
            groupBox1.Controls.Add(btnStopTcpServer);
            groupBox1.Controls.Add(btnStartTcpServer);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1068, 68);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Actions";
            // 
            // txtServerPort
            // 
            txtServerPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtServerPort.Location = new Point(757, 32);
            txtServerPort.Name = "txtServerPort";
            txtServerPort.PlaceholderText = "SERVER PORT";
            txtServerPort.Size = new Size(177, 23);
            txtServerPort.TabIndex = 3;
            // 
            // txtServerHost
            // 
            txtServerHost.Location = new Point(134, 32);
            txtServerHost.Name = "txtServerHost";
            txtServerHost.PlaceholderText = "SERVER HOST";
            txtServerHost.Size = new Size(177, 23);
            txtServerHost.TabIndex = 2;
            // 
            // btnStopTcpServer
            // 
            btnStopTcpServer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStopTcpServer.Location = new Point(940, 22);
            btnStopTcpServer.Name = "btnStopTcpServer";
            btnStopTcpServer.Size = new Size(122, 40);
            btnStopTcpServer.TabIndex = 1;
            btnStopTcpServer.Text = "Stop TCP Server";
            btnStopTcpServer.UseVisualStyleBackColor = true;
            btnStopTcpServer.Click += btnStopTcpServer_Click;
            // 
            // btnStartTcpServer
            // 
            btnStartTcpServer.Location = new Point(6, 22);
            btnStartTcpServer.Name = "btnStartTcpServer";
            btnStartTcpServer.Size = new Size(122, 40);
            btnStartTcpServer.TabIndex = 0;
            btnStartTcpServer.Text = "Start TCP Server";
            btnStartTcpServer.UseVisualStyleBackColor = true;
            btnStartTcpServer.Click += btnStartTcpServer_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(pictureArea);
            groupBox2.Location = new Point(12, 86);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1068, 520);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "ScreenShare";
            // 
            // pictureArea
            // 
            pictureArea.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureArea.Location = new Point(6, 22);
            pictureArea.Name = "pictureArea";
            pictureArea.Size = new Size(1056, 492);
            pictureArea.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureArea.TabIndex = 0;
            pictureArea.TabStop = false;
            // 
            // HaKeX
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 618);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "HaKeX";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HaKeX Server";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureArea).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox txtServerPort;
        private TextBox txtServerHost;
        private Button btnStopTcpServer;
        private Button btnStartTcpServer;
        private PictureBox pictureArea;
    }
}
