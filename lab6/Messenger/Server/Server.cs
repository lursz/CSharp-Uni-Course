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
        // Buffer for message, max 1024 bytes
        byte[] bufor = new byte[1_024];

        //Blocking call, waiting for message
        int received = socketKlienta.Receive(bufor, SocketFlags.None);
        String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine(wiadomoscKlienta);
        string odpowiedz = "odpowiedź serwera";
        var echoBytes = Encoding.UTF8.GetBytes(odpowiedz);
        socketKlienta.Send(echoBytes, 0);
        try
        {
            socketSerwera.Shutdown(SocketShutdown.Both);
            socketSerwera.Close();
        }
        catch { }
    }

}


