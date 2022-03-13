using System;
using System.Threading;
using PhilosophyDB;
using PhilosophyServer;

namespace PhilosophyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            new Config();

            DataBase db = new DataBase();

            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMinThreads(4, 4);
            Server server = new Server("127.0.0.1", 80);
            server.Start();
        }
    }
}
