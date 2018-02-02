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

        public void Load(string[] urlStrings)
        {
            string ret;
            foreach (string url in urlStrings)
            {
               
                    Task task = Task.Factory.StartNew(
                        () =>
                        {

                            Sniffer sniff = SnifferFactory.Instance().Create(url);
                            Console.WriteLine("strated");
                            while (true)
                            {
                                try
                                {
                                

                                      ret = sniff.Sniff().Result;
                                      
                                    var restResult = new SniffResult()
                                    {

                                        Url = url,
                                        DateTime = DateTime.Now,
                                        Result = ret

                                    };
                                    Console.WriteLine(restResult.Result);
                                    _resultsSniff.Add(restResult);

                                 

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

                            }

                        }, _cancellationToken);
                    this._sniffers.Add(task);
                
            }
            Task.WaitAll(this._sniffers.ToArray());

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