using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SwordCore;

namespace Sword
{
    class Program
    {

        static void Main(string[] args)
        {
            Singleton<SwordSocket>.Instance.Initialize();

        }

    }
}

