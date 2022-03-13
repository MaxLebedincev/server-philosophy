using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace PhilosophyProject
{
    class Server
    {
        public EndPoint Ip; // представляет ip-адрес
        int Listen; // представляет наш port
        Socket Listener; // представляет объект, который ведет прослушивание
        public bool Active; // представляет состояние сервера, работает он(true) или нет(false)

        public Server(string ip, int port)
        {
            this.Listen = port;
            this.Ip = new IPEndPoint(IPAddress.Parse(ip), Listen);
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (!Active)
            {
                // тело условия
                Listener.Bind(Ip);
                Listener.Listen(16);
                Active = true;

                while (Active)
                {
                    ThreadPool.QueueUserWorkItem(
                            new WaitCallback(ClientThread),
                            Listener.Accept()
                            );
                }
            }
            else
                Console.WriteLine("Server was started");
        }

        public void Stop()
        {
            if (Active)
            {
                Listener.Close();
                Active = false;
            }
            else
                Console.WriteLine("Server was stopped");
        }

        public void ClientThread(object client)
        {
            new Client((Socket)client);
        }
    }
}
