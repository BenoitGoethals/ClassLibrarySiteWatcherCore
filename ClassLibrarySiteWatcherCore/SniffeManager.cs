using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrarySiteWatcherCore
{
    public class SnifferManager
    {


        private readonly ObservableCollection<SniffResult> _resultsSniff = new ObservableCollection<SniffResult>();

        private readonly ConcurrentBag<Task> _sniffers = new ConcurrentBag<Task>();


        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;
   
        public void Subscribe(NotifyCollectionChangedEventHandler changedEventHandler)
        {
            _resultsSniff.CollectionChanged += changedEventHandler;
        }
        public SnifferManager()
        {
         

            _cancellationToken = _tokenSource.Token;

        }


        public ObservableCollection<SniffResult> ResultsSniffing => _resultsSniff;

        public void AddUrl(string[] urlStrings)
        {
            
            foreach (string url in urlStrings)
            {
                _sniffers.Add(new Task(action: async () =>
             {

                 Sniffer sniff = SnifferFactory.Instance().Create(url);
                 try
                 {
                     var ret = sniff.Sniff().Result;

                     _resultsSniff.Add(new SniffResult()
                     {

                         Url = url,
                         DateTime = DateTime.Now,
                         Result = ret

                     });
                 }
                 catch (HttpRequestException ie)
                 {
                     Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
                 }
                 catch (AggregateException e)
                 {
                     Console.WriteLine("Exception messages:");
                     foreach (var ie in e.InnerExceptions)
                         Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
                 }
                
                     

             }, cancellationToken: _cancellationToken));
            }
        }

        public void Start()
        {
            int t = 0;
            try
            {
                do
                {
                    foreach (var task in _sniffers)
                    {
                                  
                        if(task.Status!=TaskStatus.RanToCompletion && !task.IsCompleted)
                        task.Start();

                    }

                    Task.WaitAll(this._sniffers.ToArray());
                } while (++t<20);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);

           
            }
           

            Console.WriteLine("end");

        }


        public void Stop()
        {
            try
            {
            
                        _tokenSource.Cancel();

            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");
                foreach (var ie in e.InnerExceptions)
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);

               
            }
            finally
            {
               
                _tokenSource?.Dispose();
            }
        }


        public bool IsFinshed()
        {
            foreach (var sniffer in _sniffers)
            {
                if (!sniffer.IsCanceled || !sniffer.IsCompleted) return false;
            }
            return true;
        }
    }
}