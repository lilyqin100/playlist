using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HighSpot
{
    public class Playlist
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("user_id")]
        public string User_Id { get; set; }

        [JsonPropertyName("song_ids")]
        public List<string> Song_Ids { get; set; }
    }
}
