using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
