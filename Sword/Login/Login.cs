using Sword.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sword.Login
{
    public static class Login
    {
        public static List<long> IDs;
        public static void InitializeLogins()
        {
            IDs=DB.GetAllAccountIds();
        }
        public static bool LoginAccount(string username,string password)
        {
            long id = 0;
            try
            {
                bool? res= DB.AccountLogin(username, password);
                if(res==null)    //新建
                {
                    return true;
                }
                //
                else if(res==true) 
                {
                    return true;
                }
                else
                {
                    return false;   //已登录
                }
            }

            catch(Exception e) 
            { 
                return false;
            }
        }
        public static Asset QueryAsset(string ID)
        {
            long id = 0;
            try
            {
                id=Convert.ToInt64(ID);
                Asset asset = DB.QueryAsset(id);

                return asset;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
    }
}
