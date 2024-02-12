using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;

namespace SwordClient
{
    public class SwordClientSocket
    {
        private Socket _client;
        public void Initialize()
        {

            Thread.Sleep(300);
            // 建立Tcp客户端socket，名为client，用于监听客户端连接，和建立服务器的Socket一样的配方
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string ip =ConfigurationManager.AppSettings["IP"];
            string port=ConfigurationManager.AppSettings["Port"];
            // 配置端口，“127.0.0.1”、7777，均和服务器保持一致
            EndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));

            // 接入端口
            _client.Connect(endpoint);
            Thread recievethread = new Thread(new ThreadStart(ReceiveProcess));
            recievethread.Start();

            string content = "0,0,0,333,password,";
            // 用Encoding.UTF8.GetBytes()，将要发送的内容转化为字节
            byte[] data = Encoding.UTF8.GetBytes(content);

            // 发送字节

            for(int i = 0; i < 3; i++)
            {
                int sendCount= _client.Send(data);
                Console.WriteLine(sendCount);
                Thread.Sleep(300);
            }

        }
        public void ReceiveProcess()
        {
            while (true)
            {
                byte[] reply = new byte[1024];
                _client.Receive(reply);
                string replymes = Encoding.UTF8.GetString(reply);
                Process(replymes);
                Console.WriteLine(replymes);
                Thread.Sleep(100);
            }
        }
        public void Send(string mes)
        {
            byte[] data = Encoding.UTF8.GetBytes(mes);
            _client.Send(data);
        }
        public void Process(string mes)
        {

        }
    }
}
