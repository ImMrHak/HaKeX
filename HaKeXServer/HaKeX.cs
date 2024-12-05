using HaKeXServer.HaKeXTCP;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using Tesseract;

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

        private void btnITC_Click(object sender, EventArgs e)
        {
            if (pictureArea.Image == null)
            {
                MessageBox.Show("No image to process.");
                return;
            }

            // Perform OCR on the image in pictureArea
            try
            {
                string tessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var ms = new MemoryStream())
                    {
                        // Save the pictureArea image to a memory stream
                        pictureArea.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        ms.Seek(0, SeekOrigin.Begin);

                        using (var img = Pix.LoadFromMemory(ms.ToArray()))
                        {
                            using (var page = engine.Process(img))
                            {
                                string text = page.GetText();

                                if (!string.IsNullOrWhiteSpace(text))
                                {
                                    // Copy the extracted text to the clipboard
                                    Clipboard.SetText(text);
                                    MessageBox.Show("Text copied to clipboard!");
                                }
                                else
                                {
                                    MessageBox.Show("No text found in the image.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during OCR: {ex.Message}");
            }
        }
    }
}
