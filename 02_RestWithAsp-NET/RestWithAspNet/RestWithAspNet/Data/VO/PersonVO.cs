
using RestWithAspNet.Hypermedia;
using RestWithAspNet.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace RestWithAspNet.Data.VO
{

    public class PersonVO : ISupportHypermedia
    {
        [JsonPropertyName("code")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } 
        public string Adress { get; set; }
        [JsonPropertyName("sex")]
        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
