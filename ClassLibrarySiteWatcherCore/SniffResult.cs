using System;

namespace ClassLibrarySiteWatcherCore
{
    public sealed class SniffResult
    {
        
        public string Url { get; set; }
        public string Result { get; set; }

        public DateTime DateTime { get; set; }


        public SniffResult()
        {
        }


        private bool Equals(SniffResult other)
        {
            return string.Equals(Url, other.Url);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SniffResult && Equals((SniffResult) obj);
        }

        public override int GetHashCode()
        {
            return (Url != null ? Url.GetHashCode() : 0);
        }

       

       

        public override string ToString()
        {
            return $"{nameof(Url)}: {Url}, {nameof(Result)}: {Result}, {nameof(DateTime)}: {DateTime}";
        }
    }
}