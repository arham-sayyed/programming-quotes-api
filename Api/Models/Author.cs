using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProgrammingQuotesApi.Models
{
    public class Author
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int QuoteCount { get; set; }
        [Url]
        [Required]
        public string QuotesUrl { get; set; }
        [Url]
        public string WikiUrl { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Quote> Quotes { get; set; }
    }
}