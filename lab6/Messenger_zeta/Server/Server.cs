using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Server
{

    public static void Main()
    {
        /* ------------------------------ Configuration ----------------------------- */
        System.Console.WriteLine("Server started");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        //Choosing first address from list
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        //Socket listening on TCP/IP
        Socket socketSerwera = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        //Port reservation
        socketSerwera.Bind(localEndPoint);
        socketSerwera.Listen(100);
        //Awaiting connections
        Socket socketKlienta = socketSerwera.Accept();
        string my_dir = Directory.GetCurrentDirectory();

        /* ------------------------------ Program loop ------------------------------ */
        while (true)
        {
            Console.WriteLine($"Current Directory: {my_dir}");

            /* --------------------------------- Receive -------------------------------- */
            // Message size
            byte[] sizeBuf = new byte[4];
            int received = socketKlienta.Receive(sizeBuf, 4, SocketFlags.None);
            int messageSize = BitConverter.ToInt32(sizeBuf, 0);
            // Buffer for message
            byte[] buffer = new byte[messageSize];

            // Blocking call, waiting for message
            int receivedTotal = 0;
            while (receivedTotal < messageSize)
            {
                received = socketKlienta.Receive(buffer, receivedTotal, messageSize - receivedTotal, SocketFlags.None);
                receivedTotal += received;
            }
            String message = Encoding.UTF8.GetString(buffer, 0, messageSize);
            Console.WriteLine(message);

            /* -------------------------------- Commands -------------------------------- */
            string response = "";
            if (message == "!end")
            {
                response = "Server shutting down...";
                break;
            }
            else if (message == "list")
            {
                response = fileAndDir(my_dir);
            }
            else if (message.StartsWith("in "))
            {
                try
                {
                    string name = message.Substring(3);
                    if (name == "...")
                    {
                        my_dir = Directory.GetParent(my_dir).FullName;
                    }
                    else
                    {
                        my_dir = Path.Combine(my_dir, name);
                    }
                    response = fileAndDir(my_dir);
                }
                catch (Exception e)
                {
                    response = e.Message;
                }
            }
            else
            {
                response = "unknown command";
            }

            /* ---------------------------------- Send ---------------------------------- */
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            byte[] responseSizeBytes = BitConverter.GetBytes(responseBytes.Length);

            // Send the response message size and message
            socketKlienta.Send(responseSizeBytes, SocketFlags.None);
            socketKlienta.Send(responseBytes, SocketFlags.None);
            try
            {
                socketSerwera.Shutdown(SocketShutdown.Both);
                socketSerwera.Close();
            }
            catch { }

        }
    }


    public static String fileAndDir(String my_dir)
    {

        string[] files = Directory.GetFiles(my_dir);
        string[] directories = Directory.GetDirectories(my_dir);
        StringBuilder sb = new StringBuilder();

        sb.Append($"Current Directory: {my_dir}\n\nDirectories:\n");
        foreach (var directory in directories)
        {
            sb.Append(Path.GetFileName(directory) + ", ");
        }
        sb.Append("\nFiles:\n");
        foreach (var file in files)
        {
            sb.Append(Path.GetFileName(file) + ", ");
        }
        String response = sb.ToString();
        return response;
    }
}
