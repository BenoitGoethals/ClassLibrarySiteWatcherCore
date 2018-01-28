using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrarySiteWatcherCore;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] urls = new String[]
            {
                "http://www.mil.be",
                "http://www.mil.be",
                "http://www.mil.be",
                "https://code.i-harness.com/en/q/1d64607",
                "https://blog.angular-university.io/angular-jwt-authentication/",
                "http://www.mil.be",
                "http://www.mil.be",
                "https://code.i-harness.com/en/q/1d64607",
                "http://www.mil.be",
                "https://blog.angular-university.io/angular-jwt-authentication/",
                "http://www.mil.be",
                "http://www.mil.be",
                "https://code.i-harness.com/en/q/1d64607",
                "http://www.mil.be",
                "https://blog.angular-university.io/angular-jwt-authentication/",
                "http://www.mil.be",
                "http://www.mil.be",
                "https://code.i-harness.com/en/q/1d64607",
                "http://www.mil.be",
                "https://blog.angular-university.io/angular-jwt-authentication/",

            };
            SnifferManager manager = new SnifferManager();
            manager.Subscribe(ResultsSniff_CollectionChanged);
            manager.AddUrl(urls);
            manager.Start();
            manager.Stop();



         


        }

        private static void ResultsSniff_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.NewItems);
        }
       
    }
}
