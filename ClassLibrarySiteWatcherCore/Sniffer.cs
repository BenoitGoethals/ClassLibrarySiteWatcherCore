using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassLibrarySiteWatcherCore
{

    public class Sniffer
    {

        private static readonly HttpClient Client = new HttpClient();
        
        private readonly Uri _uri;
        
        public Sniffer(Uri url)
        {
            this._uri = url;
            
            
        }

        public async Task<string> Sniff()
        {

            using (Client)
            {
               
              return await Client.GetStringAsync(_uri);
            }
        }
       


    }
}