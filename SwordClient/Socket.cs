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

            string ip = "127.0.0.1";
            string port= "7777";
            // 配置端口，“127.0.0.1”、7777，均和服务器保持一致
            EndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));

            // 接入端口
            _client.Connect(endpoint);
            Thread recievethread = new Thread(new ThreadStart(ReceiveProcess));
            recievethread.Start();

            //string content = "0,0,0,333,password,CE,";
            // 用Encoding.UTF8.GetBytes()，将要发送的内容转化为字节
            //byte[] data = Encoding.UTF8.GetBytes(content);

            // 发送字节

            //for(int i = 0; i < 3; i++)
            //{
            //    int sendCount= _client.Send(data);
            //    Console.WriteLine(sendCount);
            //    Thread.Sleep(300);
            //}
        }

        public void ReceiveProcess()
        {
            Send("ReceiveProcess Start");
            while (true)
            {
                byte[] reply = new byte[1024];
                
                int length=_client.Receive(reply);
                string replymes = Encoding.UTF8.GetString(reply,0,length);
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
            string[] rawcontents = mes.Split(',');
            Send($"Received {mes},CE,");
            if (rawcontents == null || rawcontents.Length <= 1)
            {
                Send($"Quick return,CE,");
                return;
            }
            //else if (rawcontents.LastOrDefault() != "CE")
            //{
            //    return;
            //}
            List<List<string>> contents= new List<List<string>>();
            List<string> onecontent = new List<string>();
            Send($"Start Split,CE,");
            for (int i = 0; i < rawcontents.Length-1; i++)
            {
                onecontent.Add(rawcontents[i]);
                if (rawcontents[i] == "CE")
                {
                    contents.Add(onecontent);
                    onecontent = new List<string>();
                }
            }
            contents.Add(onecontent);
            Send($"contents count :{contents.Count},CE,");
            foreach (List<string> content in contents)
            {
                if (content == null || content.Count == 0)
                {
                    continue;
                }
                switch (content[0])
                {
                    case "0":
                        Send("Received 0,CE,");
                        break;

                    case "1":
                        long id = Convert.ToInt64(content[1]);
                        Send("Received 1,CE,");
                        ClientMethods.SetAccountId(id);
                        ClientMethods.SetAccountOnline(true);
                        break;

                    case "2":
                        Send("Received 2,CE,");
                        ClientMethods.SetAccountOnline(false);
                        break;

                    case "3":
                        Send("Received 3,CE,");
                        Asset asset = AssetManager.AccountAsset;
                        Send("GetAsset,CE,");
                        asset.ID= Convert.ToInt64(content[1]);
                        asset.Coins = Convert.ToInt32(content[2]);
                        asset.Dimends = Convert.ToInt32(content[3]);
                        asset.Level = Convert.ToInt32(content[4]);
                        Send("SetAsset,CE,");
                        break;

                    default:
                        break;
                }
            }

        }
    }
}
