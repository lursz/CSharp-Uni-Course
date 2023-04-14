using System;

using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client
{
    public static void Main()
    {
        System.Console.WriteLine("Client started");
        //Server host
        IPHostEntry host = Dns.GetHostEntry("localhost");
        //Choosing first address from list
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        //Connecting
        socket.Connect(localEndPoint);

        while (true)
        {
            Console.Write("> ");
            string message = Console.ReadLine();
            if (message == "")
            {
                continue;
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] sizeBytes = BitConverter.GetBytes(messageBytes.Length);
            socket.Send(sizeBytes, SocketFlags.None);
            socket.Send(messageBytes, SocketFlags.None);

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
            if (message == "!end")
            {
                break;
            }
        Console.WriteLine(response);
        }


        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch { }

    }

}