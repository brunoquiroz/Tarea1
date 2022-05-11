using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tarea1
{
    internal class ProgramBase
    {

        static void Main(string[] args)
        {
            server();
        }
        public static void server()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress iPAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(iPAddress, 2050);

            try
            {
                Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Conectado");
                Socket hander = listener.Accept();



                while (true)
                {

                    string data = null;
                    byte[] bytes = null;


                    while (true)
                    {
                    bytes = new byte[1024];
                    int byteRec = hander.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, byteRec);


                    if (data.IndexOf("<EOF>") > -1)
                        break;
                    }

                    Console.WriteLine("Mensaje del cliente : " + data.Replace("<EOF>", ""));
                    byte[] msg = Encoding.ASCII.GetBytes("Recibido");
                    hander.Send(msg);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}