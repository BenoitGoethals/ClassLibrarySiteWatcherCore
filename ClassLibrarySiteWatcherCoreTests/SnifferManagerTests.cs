using Xunit;
using ClassLibrarySiteWatcherCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrarySiteWatcherCore.Tests
{
    public class SnifferManagerTests
    {
        [Fact()]
        public void SnifferManagerTest()
        {
            string[] urls = new String[]
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
            SnifferManager manager=new SnifferManager();
            manager.Subscribe(ResultsSniff_CollectionChanged);
           manager.Load(urls);
      //      manager.Start();
           manager.Stop();

      
           
                Assert.Equal(20,manager.ResultsSniffing.Count);
           
           

        }

        private void ResultsSniff_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           Assert.NotNull(e.NewItems);
        }
        [Fact()]
        public void AddUrlTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void StartTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void StopTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}