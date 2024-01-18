using System.Text;

namespace RestWithAspNet.Hypermedia
{
    public class HyperMediaLink
    {
        public string Rel { get; set; }
        private string href;
        public string Href
        { get
            {
                //Isso é útil para evitar condições de corrida em ambientes multithread.
                object _lock = new object();
                lock (_lock)
                {
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString();
                }
            }
            set
            {
                href = value;
            }
        }
        public string Type { get; set; }
        public string Action { get; set; }
    }
}
