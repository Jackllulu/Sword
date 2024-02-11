using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sword.DataBase
{
    public static class DB
    {

        public static Asset QueryAsset(long id)
        {
            try
            {
                AssetDBContext assetDB = new AssetDBContext();
                Asset asset = assetDB.Assets.Where(l => l.ID == id).FirstOrDefault();
                return asset;
            }
            catch (Exception ex)
            { 

            }
            return null;
        }
        public static bool? AccountLogin(string username,string password="")
        {
            try
            {
                AccountDBContext db = new AccountDBContext();
                var accounts= db.Accounts.Where(l => l.UserName == username&&l.Password==password);
                if(accounts.Any())
                {
                    Account account= accounts.FirstOrDefault();
                    if (account.Online)
                    {
                        Console.WriteLine("Login Success!");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Login Fail! Please Check");
                        account.Online = true;
                        db.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    CreateAccount(username, password);
                    return null;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool CreateAccount(string username,string password)
        {
            try
            {
                AccountDBContext db = new AccountDBContext();
                Account account = new Account();
                account.ID = 0;
                account.UserName = username;
                account.Password = password;
                account.CreateTime = DateTime.Now;
                account.Online = false;
                db.Accounts.Add(account);
                db.SaveChanges();

                AssetDBContext assetDB = new AssetDBContext();
                Asset asset = new Asset();
                asset.ID = account.ID;
                asset.Coins = 0;
                asset.Dimends = 0;
                asset.Level = 1;
                assetDB.Assets.Add(asset);
                assetDB.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public static List<Account> GetAllAccounts()
        {
            AccountDBContext accountDBContext = new AccountDBContext();
            return accountDBContext.Accounts.ToList();
        }
        public static List<long> GetAllAccountIds()
        {
            List<long> ids = new List<long>();
            var accounts= GetAllAccounts();
            foreach (var account in accounts)
            {
                ids.Add(account.ID);
            }
            return ids;
        }
    }

    public class AccountDBContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文配置</param>
        public AccountDBContext() : base("name=SWORDconn1")
        {

        }
        public DbSet<Account> Accounts { get; set; }

    }

    public class AssetDBContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文配置</param>
        public AssetDBContext() : base("name=SWORDconn2")
        {

        }
        public DbSet<Asset> Assets { get; set; }
    }
}
