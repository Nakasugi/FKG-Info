using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FKG_Info
{
    class MiniServer
    {
        TcpListener Listener;
        Thread ServerThread;

        const string getMasterURL = "http://web.flower-knight-girls.co.jp/api/v1/master/getMaster";

        public MiniServer()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Listener = new TcpListener(ip, 8483);
            //Listener.Start();

            ServerThread = new Thread(ServerProc);
            ServerThread.Name = "Waiting for master data.";
            ServerThread.Start();
        }

        private void ServerProc()
        {
            Listener.Start();

            TcpClient cl = Listener.AcceptTcpClient();
            NetworkStream cs = cl.GetStream();

            FileStream fs = new FileStream("F:\\getMaster_POST.txt", FileMode.Create);

            cs.CopyTo(fs);

            fs.Close();
            cl.Close();
            Listener.Stop();
        }
    }
}
