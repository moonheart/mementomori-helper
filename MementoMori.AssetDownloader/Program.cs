using MementoMori;
using MessagePack;
using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<MementoNetworkManager>();
            builder.Services.AddHostedService<AssetDownloader>();

            IHost host = builder.Build();
            host.Run();
        }
    }
}