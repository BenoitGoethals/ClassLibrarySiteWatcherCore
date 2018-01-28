using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrarySiteWatcherCore;
using Xunit;

namespace ClassLibrarySiteWatcherCoreTests
{
    public class SnifferFactoryTests
    {
        [Fact()]
        public void CreateTest()
        {
            string a = null;


            Task task = Task.Factory.StartNew(() =>
            {
                Sniffer sniffer = SnifferFactory.Instance().Create("http://www.mil.be");
                a = sniffer.Sniff().Result;
            });

            Task.WaitAny(task);

            Assert.NotNull(a);
        }

        [Fact()]
        public void CreateDifferentTest()
        {
            string[] url = new String[]
            {
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
                "http://www.mil.be",
                "http://www.mil.be",
                "https://code.i-harness.com/en/q/1d64607",
                "http://www.mil.be",
                "https://blog.angular-university.io/angular-jwt-authentication/",

            };

            var tasks = new Task[url.Length];
            var reStrings = new String[url.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                var i1 = i;
                Sniffer sniffer = SnifferFactory.Instance().Create(url[i1]);

                Task task = Task.Factory.StartNew(() =>
                {
                   
                    reStrings[i1] = sniffer.Sniff().Result;
                    Task.Delay(200 * i1);
                });
                tasks[i] = task;

            }

            try
            {
                Task.WhenAll(tasks);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                Debug.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Console.WriteLine("\n-------------------------------------------------\n{0}",
                        e.InnerExceptions[j].ToString());
                    Debug.WriteLine("\n-------------------------------------------------\n{0}",
                        e.InnerExceptions[j].ToString());
                }

                foreach (var reString in reStrings)
                {
                    Assert.NotNull(reString);
                }
            }

        }
    }
}
