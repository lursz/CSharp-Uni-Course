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


        //Sending message to server in UTF8
        string message = System.Console.ReadLine();
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        // Encode the message size
        byte[] sizeBytes = BitConverter.GetBytes(messageBytes.Length);
        socket.Send(sizeBytes, SocketFlags.None);
        socket.Send(messageBytes, SocketFlags.None);

        // Buffer for response message size, 4 bytes
        var responseSizeBuf = new byte[4];
        int received = socket.Receive(responseSizeBuf, 4, SocketFlags.None);
        int responseSize = BitConverter.ToInt32(responseSizeBuf, 0);

        // Buffer for response message
        var responseBuf = new byte[responseSize];
        int receivedTotal = 0;
        while (receivedTotal < responseSize)
        {
            received = socket.Receive(responseBuf, receivedTotal, responseSize - receivedTotal, SocketFlags.None);
            receivedTotal += received;
        }
        String response = Encoding.UTF8.GetString(responseBuf, 0, responseSize);

        Console.WriteLine(response);
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch { }

    }

}