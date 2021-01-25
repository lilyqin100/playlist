using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class PlaylistToBeRemoved
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
