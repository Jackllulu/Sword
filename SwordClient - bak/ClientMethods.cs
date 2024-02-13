using SwordCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwordData;
using System.Threading;

namespace SwordClient
{
    public static class ClientMethods
    {
        public static void Initialize()
        {
            Singleton<SwordClientSocket>.Instance.Initialize();
        }
        public static bool LoginAccount(string username,string password)
        {
            if(AccountManager.LoginAccount(username, password))
            {
                int i = 0;
                do
                {
                    Thread.Sleep(100);
                    if (AccountManager.IsOnline)
                    {
                        return true;
                    }
                    i++;
                } 
                while (i > 20);

                return false;
            }
            return false;
        }
    }
}
