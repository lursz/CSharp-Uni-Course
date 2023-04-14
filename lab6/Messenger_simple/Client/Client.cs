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
        // read input from user
        string wiadomosc = Console.ReadLine();
        byte[] wiadomoscBajty = Encoding.UTF8.GetBytes(wiadomosc);
        socket.Send(wiadomoscBajty, SocketFlags.None);

        //Buffer for message, max 1024 bytes
        var bufor = new byte[1_024];
        //Receiving message from server
        int liczbaBajtów = socket.Receive(bufor, SocketFlags.None);
        String odpowiedzSerwera = Encoding.UTF8.GetString(bufor, 0, liczbaBajtów);

        Console.WriteLine(odpowiedzSerwera);
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch { }

    }

}