using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HaKeXServer.HaKeXTCP
{
    internal class TcpServerHaKeX
    {
        private readonly string _host;
        private readonly int _port;
        private TcpListener _listener;

        public TcpServerHaKeX(string hostIP, int hostPORT)
        {
            _host = hostIP;
            _port = hostPORT;
        }

        // Method to start the server and continuously receive images
        public void StartServerAndReceiveImages(Action<Bitmap> onImageReceived)
        {
            _listener = new TcpListener(IPAddress.Parse(_host), _port);
            _listener.Start();
            Console.WriteLine($"Server started on {_host}:{_port} and waiting for connections...");

            while (true)
            {
                try
                {
                    using (TcpClient client = _listener.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        Console.WriteLine("Client connected.");
                        while (true)
                        {
                            // Receive the length of the incoming image
                            byte[] lengthBuffer = new byte[4];
                            int bytesRead = stream.Read(lengthBuffer, 0, lengthBuffer.Length);
                            if (bytesRead == 0)
                            {
                                break; // End of stream, client disconnected
                            }

                            int imageLength = BitConverter.ToInt32(lengthBuffer, 0);

                            // Receive the image data
                            byte[] imageBuffer = new byte[imageLength];
                            int totalBytesRead = 0;
                            while (totalBytesRead < imageLength)
                            {
                                bytesRead = stream.Read(imageBuffer, totalBytesRead, imageLength - totalBytesRead);
                                if (bytesRead == 0)
                                {
                                    throw new IOException("Unexpected disconnection during image transfer.");
                                }
                                totalBytesRead += bytesRead;
                            }

                            // Convert received data to an image
                            using (MemoryStream ms = new MemoryStream(imageBuffer))
                            {
                                Bitmap receivedImage = new Bitmap(ms);

                                // Trigger the callback with the received image
                                onImageReceived?.Invoke(receivedImage);
                            }

                            Console.WriteLine("Image received.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    break; // Break out of the loop on error
                }
            }
        }

        // Method to stop the server
        public void StopServer()
        {
            if (_listener != null)
            {
                _listener.Stop();
                Console.WriteLine("Server stopped.");
            }
        }
    }
}
