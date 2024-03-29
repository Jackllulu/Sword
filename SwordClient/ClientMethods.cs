﻿using SwordCore;
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
        public static bool LoginAccount()
        {
            if(AccountManager.LoginAccount())
            {
                int i = 0;
                do
                {
                    Thread.Sleep(100);
                    if (AccountManager.IsOnline)
                    {
                        Log.Info("Login Success");
                        return true;
                    }
                    i++;
                } 
                while (i < 20);
                Log.Error("Login Failed , Time out");
                return false;
            }
            Log.Error("Login Failed , Undo");
            return false;
        }
        public static bool LogoffAccount()
        {
            if (AccountManager.LogoffAccount())
            {
                int i = 0;
                do
                {
                    Thread.Sleep(100);
                    if (!AccountManager.IsOnline)
                    {
                        Log.Info("Logoff Success");
                        return true;
                    }
                    i++;
                }
                while (i < 20);
            }
            Log.Error("Logoff Failed");
            return false;
        }
        public static void SetAccountMes(string username, string password)
        {
            AccountManager.SetAccountMes(username, password);
        }

        public static bool SetAccountId(long id)
        {
            try
            {
                AccountManager.AccountID = id;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool SetAccountOnline(bool online)
        {
            try
            {
                AccountManager.IsOnline = online;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
