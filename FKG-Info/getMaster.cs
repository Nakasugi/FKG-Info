using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FKG_Info
{
    class getMaster
    {
        TcpListener Listener;

        Thread ListenerThread;


        public getMaster()
        {
            Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8778);

            ListenerThread = new Thread(ListenerProc);
            ListenerThread.Name = "HTTP Listener";
            ListenerThread.Start();
        }



        void ListenerProc()
        {
            Listener.Start();

            try
            {
                TcpClient client = Listener.AcceptTcpClient();

                int[] b = new int[4];
                while (true)
                {
                    b[3] = b[2]; b[2] = b[1]; b[1] = b[0];
                    b[0] = client.GetStream().ReadByte();

                    if (b[0] != 0x0A) continue;
                    if (b[1] != 0x0D) continue;
                    if (b[2] != 0x0A) continue;
                    if (b[3] != 0x0D) continue;

                    break;
                }

                string path = Program.DB.DataFolder + "\\getMaster" + System.DateTime.Now.ToString("yyMMddHHmmss") + ".bin";
                FileStream fs = new FileStream(path, FileMode.Create);
                client.GetStream().CopyTo(fs); // This is totally wrong!!! Infinite process, need abort from sender!
                fs.Close();
                client.Close();
            }
            catch { }

            Listener.Stop();
        }
    }
}
