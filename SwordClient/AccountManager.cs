using SwordCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordClient
{
    public static class AccountManager
    {
        public static long AccountID { get; set; } = 0;
        public static string Username { get; set; } = string.Empty;
        public static string Password { get; set;} = string.Empty;
        public static bool IsOnline { get; set; } = false;

        public static bool LoginAccount()
        {
            string username = Username;
            string password = Password;
            try
            {
                Singleton<SwordClientSocket>.Instance.Send($"0,0,0,{username},{password},CE");

                return true;
            }
            catch 
            { 
                return false; 
            }
        }
        public static bool LogoffAccount()
        {
            try
            {
                Singleton<SwordClientSocket>.Instance.Send($"0,0,1,{AccountID},CE");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetAccountMes(string username,string password)
        {
            Username = username;
            Password = password;
        }
    }
}
