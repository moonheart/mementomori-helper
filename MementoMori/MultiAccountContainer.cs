using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoMori;

internal class MultiAccountContainer<T>
{
    private Dictionary<string, T> _accounts = new();

    public T GetData(string account)
    {
        return _accounts.TryGetValue(account, out var value) ? value : default;
    }

    public void SetData(string account, T data)
    {
        _accounts[account] = data;
    }
}