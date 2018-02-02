using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace ClassLibrarySiteWatcherCore
{
    public class SnifferFactory
    {

        private readonly ConcurrentDictionary<String,Sniffer> _sniffers=new ConcurrentDictionary<String, Sniffer>();

        private static readonly SnifferFactory instance = new SnifferFactory();

        public Sniffer Create(string uri)
        {


            return _sniffers.GetOrAdd(uri, new Sniffer(new Uri(uri)));


        }

    
        public static SnifferFactory Instance() => instance;

   
    }
}