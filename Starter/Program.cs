using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrarySiteWatcherCore;

namespace Starter
{
    class Program
    {
        private static SnifferManager manager = new SnifferManager();
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
           
            manager.Subscribe(ResultsSniff_CollectionChanged);
            manager.Load(urls);
          //  manager.Start();
            manager.Stop();
            Console.WriteLine(manager.ResultsSniffing.Count);
            
            Console.ReadLine();





        }

        private static void ResultsSniff_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<SniffResult> collection = sender as ObservableCollection<SniffResult>;
            if(collection.Count>10)
                manager.Stop();
        }
       
    }
}
