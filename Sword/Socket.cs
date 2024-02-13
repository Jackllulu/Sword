using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using Sword.Login;
using Sword.DataBase;

namespace Sword
{
    public class SwordSocket
    {
        // Reply Code   header + Parameters + CE
        //  0:  ERROR , information
        //  1:  LoginSuccess , AccountID


        public List<Socket> socketServerManager;

        public SwordSocket() { }
        public void Initialize()
        {
            Login.Login.InitializeLogins();
            socketServerManager = new List<Socket>();
            Thread thread = new Thread(new ThreadStart(SocketListen));
            thread.Start();
            Thread reply = new Thread(new ThreadStart(Reply));
            reply.Start();
        }
        public void Reply()
        {
            while (true)
            {
                // 当有客户端接入时，新建名为serverManager的Socket，用于接收客户端的消息。
                // 前面的server是接收客户端消息用，而这里的serverManager是接收客户端消息用，各司其职。
                foreach (Socket socket in socketServerManager)
                {
                    // 设置接收字节的容器，容量大小为1024个字节，客户端发来的字节都存在这里
                    byte[] buffer = new byte[1024];
                    int length = socket.Receive(buffer);

                    if (length == 0)
                    {
                        continue;
                    }
                    // 用Encoding.UTF8.Getstring()，将收到的实际长度的字节转化为string类型
                    string mes = Encoding.UTF8.GetString(buffer, 0, length);
                    Console.WriteLine("收到了" + length + "个字节，内容为：" + mes);
                    string replyContent = Process(mes);
                    byte[] data = Encoding.UTF8.GetBytes(replyContent);
                    socket.Send(data);
                }

                // 返回收到的字节的实际长度


                Thread.Sleep(100);
            }
        }

        public void SocketListen()
        {

            // 建立Tcp服务器socket，名为server，用于监听客户端连接
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 配置端口。“127.0.0.1”是本机IP地址，也可在命令提示符（黑窗口）输入ipconfig，查看ipv4地址
            // 7777是端口号、可在1025~65535随便选。
            EndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777);

            // 绑定端口
            server.Bind(endpoint);

            // server开始监听客户端连接。10，是指服务器能够同时允许10个客户端连接，可写其他
            server.Listen(10);
            Console.WriteLine("服务器开启成功，开始监听~");
            while (true)
            {
                Socket serverManager = server.Accept();
                socketServerManager.Add(serverManager);
                Thread.Sleep(100);
            }

        }

        /// <summary>
        /// 信息处理
        /// 0：登录
        /// 1：注销
        /// 2：操作
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public string Process(string mes)
        {
            if(mes!=null||mes.Length!=0)
            {
                long logId;

                string[] dataArray=mes.Split(',');
                string id = dataArray[0];
                string certify = dataArray[1];
                switch(dataArray[2])
                {
                    case "0":
                        string username = dataArray[3];
                        string password = dataArray[4];
                        
                        if(Login.Login.LoginAccount(username, password,out logId))
                        {
                            Asset asset= Login.Login.QueryAsset(logId);
                            return $"1,{logId},CE,";
                        }
                        else
                        {
                            return "0,WARN: this account is still online!,CE,";
                        }
                    case "1":
                        string strlogId = dataArray[3];
                        logId = Convert.ToInt64(strlogId);
                        if (Login.Login.LogoffAccount(logId))
                        {
                            return "2,,CE";
                        }
                        else
                        {
                            return "0,Logoff Failed!,CE";
                        }
                    default:
                        break;
                }
            }
            string replyMes = "随便写点";
            return replyMes;
        }
    }
}
