using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrarySiteWatcherCore
{

    public class Sniffer
    {
     
        private static readonly HttpClient Client = new HttpClient(handler:new HttpClientHandler(),disposeHandler:false);
        
        private readonly Uri _uri;
        
        public Sniffer(Uri url)
        {
            this._uri = url;
            
            
        }

        public async Task<string> Sniff()
        {
            string ret;
            using (Client)
            {

                //Client.Timeout=TimeSpan.FromSeconds(15);
                 ret= await Client.GetStringAsync(_uri);
                 Console.WriteLine(ret);
                
            }
            return ret;
        }
       


    }
}