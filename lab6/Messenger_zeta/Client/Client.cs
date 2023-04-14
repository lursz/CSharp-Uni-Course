using System;

using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client
{
    public static void Main()
    {
        /* ------------------------------ Configuration ----------------------------- */
        System.Console.WriteLine("Client started");
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        //Connecting
        socket.Connect(localEndPoint);
        /* ------------------------------ Program loop ------------------------------ */
        while (true)
        {
            /* ---------------------------------- Read ---------------------------------- */
            Console.Write("> ");
            string input = Console.ReadLine();
            if (input == "")
            {
                continue;
            }

            /* ---------------------------------- Send ---------------------------------- */
            byte[] messageBytes = Encoding.UTF8.GetBytes(input);
            byte[] sizeBytes = BitConverter.GetBytes(messageBytes.Length);
            socket.Send(sizeBytes, SocketFlags.None);
            socket.Send(messageBytes, SocketFlags.None);

            /* --------------------------------- Receive -------------------------------- */
            byte[] responseSizeBytes = new byte[4];
            socket.Receive(responseSizeBytes, 4, SocketFlags.None);
            int responseSize = BitConverter.ToInt32(responseSizeBytes, 0);
            byte[] responseBytes = new byte[responseSize];
            int receivedTotal = 0;
            while (receivedTotal < responseSize)
            {
                int received = socket.Receive(responseBytes, receivedTotal, responseSize - receivedTotal, SocketFlags.None);
                receivedTotal += received;
            }
            string response = Encoding.UTF8.GetString(responseBytes, 0, responseSize);
            Console.WriteLine(response);


            if (input == "!end")
            {
                break;
            }
        }
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    }

}