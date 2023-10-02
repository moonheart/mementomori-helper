using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MementoMori.Account;

public class AccountManager
{
    private readonly IFreeSql _fsql;

    public AccountManager(IFreeSql fsql)
    {
        _fsql = fsql;
    }

    public void CreateAccount(string name, string password)
    {
        var info = _fsql.Select<UserInfo>().Where(d => d.Name == name).First();
        if (info != null) return;
        var passwordHash = MD5.HashData(Encoding.UTF8.GetBytes(password));
        var passwordHashString = string.Join("", passwordHash.Select(x => x.ToString("x2")));
        var userInfo = new UserInfo
        {
            Name = name,
            Password = passwordHashString,
            CreateTime = DateTime.Now
        };
        _fsql.Insert(userInfo).ExecuteInserted();
    }

    public bool Login(string name, string password)
    {
        return false;
    }
}