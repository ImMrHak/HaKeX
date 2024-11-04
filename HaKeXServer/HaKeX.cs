using HaKeXServer.HaKeXTCP;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace HaKeXServer
{
    public partial class HaKeX : Form
    {
        private TcpServerHaKeX _server;
        public HaKeX()
        {
            InitializeComponent();
        }

        private void btnStartTcpServer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServerHost.Text))
            {
                MessageBox.Show("Fill Host IP");
                return;
            }

            if (string.IsNullOrEmpty(txtServerPort.Text))
            {
                MessageBox.Show("Fill Host PORT");
                return;
            }

            // Initialize the server with the specified IP and PORT
            _server = new TcpServerHaKeX(txtServerHost.Text, Convert.ToInt32(txtServerPort.Text));

            // Start the server on a new task to avoid blocking the UI
            Task serverTask = Task.Run(() =>
            {
                _server.StartServerAndReceiveImages((Bitmap receivedImage) =>
                {
                    Invoke((Action)(() =>
                    {
                        // Ensure to dispose of the existing image before replacing it
                        pictureArea.Image?.Dispose();
                        pictureArea.Image = new Bitmap(receivedImage);
                    }));
                });
            });

            // Optionally, you can disable the start button to prevent multiple clicks
            btnStartTcpServer.Enabled = false;

            // Handle server task completion (optional)
            serverTask.ContinueWith(t =>
            {
                // If the server stops, you can re-enable the button or show a message
                Invoke((Action)(() =>
                {
                    btnStartTcpServer.Enabled = true; // Re-enable the button
                    MessageBox.Show("Server has stopped.");
                }));
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnStopTcpServer_Click(object sender, EventArgs e)
        {
            _server.StopServer();
        }
    }
}
