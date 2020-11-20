using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string strIPAddress,strPort;
            IPAddress IPaddress = null;
            int nPort;
            try
            {
                Console.WriteLine("IP DEL SERVER");
                strIPAddress = Console.ReadLine();
                Console.WriteLine("PORTA DEL SERVER");
                strPort = Console.ReadLine();
                if(!IPAddress.TryParse(strIPAddress.Trim(),out IPaddress))
                {
                    Console.WriteLine("Ip non valido.");
                    return;
                }

                if (!int.TryParse(strPort,out nPort))
                {
                    Console.WriteLine("Porta non valido.");
                    return;
                }
                if (nPort<=0 || nPort>=65535)
                {
                    Console.WriteLine("Porta non valido.");
                    return;
                }
                Console.WriteLine("Endpoint del server" + IPaddress.ToString() + " " + nPort);

                client.Connect(IPaddress, nPort);

                byte[] buff = new byte[128];
                string sendstring="", receivedstring="";
                int receivedByte = 0;
                while (true)
                {
                    Console.WriteLine("Manda un messaggio:");
                    Encoding.ASCII.GetBytes(sendstring.CopyTo(buff,0));
                    client.Send(buff);
                    if (sendstring.ToUpper().Trim()=="QUIT")
                    {
                        break;
                    }
                    Array.Clear(buff, 0, buff.Length);
                    receivedByte = client.Receive(buff);
                    receivedstring = Encoding.ASCII.GetString(buff, 0, receivedByte);
                    Console.WriteLine("S: "+receivedstring);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }

            Console.ReadLine();
        }
    }
}
