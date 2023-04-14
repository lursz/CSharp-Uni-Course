using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class Server
{
    public static void Main()
    {
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
        //Begin listening
        socketSerwera.Listen(100);
        //Awaiting connections
        Socket socketKlienta = socketSerwera.Accept();

        byte[] sizeBuf = new byte[4];
        int received = socketKlienta.Receive(sizeBuf, 4, SocketFlags.None);
        // Decode the message size
        int messageSize = BitConverter.ToInt32(sizeBuf, 0);
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
        string response = "Received: " + message;
        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
        // Encode the response message size
        byte[] responseSizeBytes = BitConverter.GetBytes(responseBytes.Length);
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


