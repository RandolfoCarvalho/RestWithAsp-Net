
using System.Text.Json.Serialization;

namespace RestWithAspNet.Data.VO
{

    public class PersonVO
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
    }
}
